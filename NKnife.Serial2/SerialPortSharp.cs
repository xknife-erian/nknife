using System;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SerialKnife2
{
    /// <summary>
    ///     Class that allows for serial communication in Windows.
    /// </summary>
    public class SerialPortSharp : IDisposable //Need to implement dispose?
    {
        private readonly int _BaudRate;
        private readonly byte _ByteSize;
        private IntPtr _Handle = IntPtr.Zero;

        private Thread _Thread;

        public SerialPortSharp(int baudRate = 9600, StopBits stopBits = StopBits.One, Parity parity = Parity.None, byte byteSize = 8)
        {
            if (stopBits == StopBits.None)
                throw new ArgumentException("stopBits cannot be StopBits.None", nameof(stopBits));
            if (byteSize < 5 || byteSize > 8)
                throw new ArgumentOutOfRangeException("The number of data bits must be 5 to 8 bits.", nameof(byteSize));
            if (baudRate < 110 || baudRate > 256000)
                throw new ArgumentOutOfRangeException("Invalid baud rate specified.", nameof(baudRate));
            if ((byteSize == 5 && stopBits == StopBits.Two) || (stopBits == StopBits.OnePointFive && byteSize > 5))
                throw new ArgumentException("The use of 5 data bits with 2 stop bits is an invalid combination as is 6, 7, or 8 data bits with 1.5 stop bits.");

            _BaudRate = baudRate;
            _ByteSize = byteSize;
            StopBits = stopBits;
            Parity = parity;

            var hEvent = CreateEvent(IntPtr.Zero, true, false, string.Empty);

            if (hEvent != IntPtr.Zero)
            {
                _Overlapped.hEvent = hEvent;
            }
        }

        public Parity Parity { get; }

        public bool IsOpen { get; set; } //Not Acutally implemented

        public string PortName { get; set; }

        public StopBits StopBits { get; }

        public Encoding Encoding { get; set; } = Encoding.ASCII;

        public int BlockingSize { get; set; } = 1024*10;

        public int StreamBufferSize { get; set; } = 1024;

        public void Dispose()
        {
            IsOpen = false;
            if (_Handle != IntPtr.Zero)
            {
                CloseHandle(_Handle);
                _Handle = IntPtr.Zero;
            }
        }

        public event EventHandler<RecievedDataEventArgs> DataRecieved;

        protected virtual void OnDataRecieved(RecievedDataEventArgs e)
        {
            DataRecieved?.Invoke(this, e);
        }

        public void DiscardInBuffer()
        {
            ReadExisting();
        }

        public bool Flush()
        {
            FailIfNotConnected("Fail flushing");

            const int PURGE_RXCLEAR = 0x0008; // input buffer
            const int PURGE_TXCLEAR = 0x0004; // output buffer
            return PurgeComm(_Handle, PURGE_RXCLEAR | PURGE_TXCLEAR);
        }

        public bool Open()
        {
            _Handle = CreateFile("//./" + PortName, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open, FILE_FLAG_OVERLAPPED, IntPtr.Zero);

            if (_Handle == IntPtr.Zero)
                return false;

            if (ConfigureSerialPort())
            {
                IsOpen = true; //Needs reworking
                return true;
            }
            Dispose();
            return false;
        }

        public unsafe int Read(byte[] buffer, int index, int count)
        {
            if (null == buffer)
                throw new ArgumentOutOfRangeException(nameof(buffer), "buffer == null");
            //int n = 0;
            var n = index;
            uint lpNumberOfBytesWritten;
            fixed (byte* p = buffer)
            {
                //ReadFile2(_Handle, p, buffer.Length, &n, ref _NativeOverlapped);
                ReadFile2(_Handle, p, count, &n, ref _NativeOverlapped);
                GetOverlappedResult(_Handle, ref _NativeOverlapped, out lpNumberOfBytesWritten, true);
            }
            return (int) lpNumberOfBytesWritten;
        }

        public byte ReadByte()
        {
            if (BytesToRead() < 1)
                throw new Exception("Error in readByte");

            var bytes = new byte[1];
            var numBytes = Read(bytes, 0, 1);
            return bytes[0];
        }

        public string ReadString(int maxBytesToRead)
        {
            if (maxBytesToRead < 1)
                throw new ArgumentOutOfRangeException(nameof(maxBytesToRead), "maxBytesToRead < 1");

            if (BytesToRead() < 1)
                return string.Empty;

            var bytes = new byte[maxBytesToRead];
            var numBytes = Read(bytes, 0, maxBytesToRead);
            var data = Encoding.GetString(bytes, 0, numBytes);
            return data;
        }

        public string ReadExisting()
        {
            if (BytesToRead() < 1)
                return string.Empty;

            return ReadExistingBlocking();
        }

        public string ReadExistingBlocking()
        {
            return ReadString(BlockingSize);
        }

        public int Write(byte[] data)
        {
            FailIfNotConnected("write");
            if (data == null)
                return 0;

            int bytesWritten;
            if (WriteFile2(_Handle, data, data.Length, out bytesWritten, ref _Overlapped))
                return bytesWritten;
            return -1;
        }

        public int Write(byte[] data, int offset, int count)
        {
            //_OVERLAPPED_ENTRY _Overlapped = new _OVERLAPPED_ENTRY ();
            FailIfNotConnected("write[]");
            if (data == null)
                return 0;

            int bytesWritten;

            if (WriteFile2(_Handle, data, count, out bytesWritten, ref _Overlapped))
                return bytesWritten;
            return -1;
        }

        public int Write(string data)
        {
            FailIfNotConnected("write string");

            byte[] bytes;
            if (data == null)
                bytes = null;
            else
                bytes = Encoding.GetBytes(data);
            return Write(bytes);
        }

        public int WriteLine(string data)
        {
            if (data != null && !data.EndsWith("\r\n"))
                data += "\r\n";
            return Write(data);
        }

        public int BytesToRead()
        {
            var err = 0;
            var comstatStruct = new COMSTAT();
            ClearCommError(_Handle, ref err, ref comstatStruct);
            return (int) comstatStruct.cbInQue;
        }

        public unsafe void StreamIn()
        {
            var buffer = new byte[StreamBufferSize];

            SetCommMask(_Handle, EV_RXCHAR | EV_CTS | EV_DSR | EV_RLSD | EV_RING);

            while (_Handle != INVALID_HANDLE_VALUE)
            {
                uint dwCommModemStatus;
                WaitCommEvent(_Handle, out dwCommModemStatus, IntPtr.Zero);
                SetCommMask(_Handle, EV_RXCHAR | EV_CTS | EV_DSR | EV_RING | EV_BREAK | EV_RLSD);

                if (dwCommModemStatus == EV_RXCHAR)
                {
                    int dwBytesTransferred;
                    do
                    {
                        fixed (byte* p = buffer)
                        {
                            var e = new RecievedDataEventArgs();
                            ReadFile2(_Handle, p, buffer.Length, &dwBytesTransferred, ref _NativeOverlapped);
                            uint lpNumberOfBytesWritten;
                            GetOverlappedResult(_Handle, ref _NativeOverlapped, out lpNumberOfBytesWritten, true);
                            e.RecievedData = Encoding.GetString(buffer, 0, (int) lpNumberOfBytesWritten);
                            OnDataRecieved(e);
                        }
                    } while (dwBytesTransferred > 0);
                }
            }
        }

        public void RunStream()
        {
            _Thread?.Abort();
            _Thread = new Thread(StreamIn);
            _Thread.Start();
        }

        public void EndStream()
        {
            _Thread.Abort();
            Dispose();
        }

        private bool ConfigureSerialPort()
        {
            var serialConfig = new DCB();

            if (GetCommState(_Handle, ref serialConfig))
            {
                serialConfig.BaudRate = (uint) _BaudRate;
                serialConfig.ByteSize = _ByteSize;
                serialConfig.fBinary = 1; // must be true
                serialConfig.fDtrControl = 1;
                // DTR_CONTROL_ENABLE "Enables the DTR line when the device is opened and leaves it on." changed from 1 to 0 per mark
                serialConfig.fAbortOnError = 0; // false
                serialConfig.fTXContinueOnXoff = 0; // false
                serialConfig.fParity = 1; // true so that the Parity member is looked at

                switch (Parity)
                {
                    case Parity.Even:
                        serialConfig.Parity = 2;
                        break;
                    case Parity.Mark:
                        serialConfig.Parity = 3;
                        break;
                    case Parity.Odd:
                        serialConfig.Parity = 1;
                        break;
                    case Parity.Space:
                        serialConfig.Parity = 4;
                        break;
                    case Parity.None:
                    default:
                        serialConfig.Parity = 0;
                        break;
                }

                switch (StopBits)
                {
                    case StopBits.One:
                        serialConfig.StopBits = 0;
                        break;
                    case StopBits.OnePointFive:
                        serialConfig.StopBits = 1;
                        break;
                    case StopBits.Two:
                        serialConfig.StopBits = 2;
                        break;
                    case StopBits.None:
                    default:
                        throw new ArgumentException("stopBits cannot be StopBits.None");
                }

                if (SetCommState(_Handle, ref serialConfig))
                {
                    // set the serial connection timeouts
                    var timeouts = new COMMTIMEOUTS
                    {
                        //ReadIntervalTimeout = 1, //Changed from 1 sean d.
                        ReadIntervalTimeout = 0,
                        ReadTotalTimeoutMultiplier = 0,
                        ReadTotalTimeoutConstant = 0,
                        WriteTotalTimeoutMultiplier = 0,
                        WriteTotalTimeoutConstant = 0
                    };

                    return SetCommTimeouts(_Handle, ref timeouts);
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// COMMTIMEOUTS结构的成员都以毫秒为单位。
        /// 如果ReadTotalTimeoutMultiplier和ReadTotalTimeoutConstant都为0，则在读操作时忽略总超时数。
        /// 如果WriteTotalTimeoutMultiplier和WriteTotalTimeoutConstant都为0，则在写操作时忽略总超时数。
        /// 用户设置通讯超时后，如没有出错，串口已经被打开。
        /// </summary>
        /// <param name="readIntervalTimeout">读间隔超时。接收时，两字符间最大的时延。即打算去读下一个字符之前的等待时间。
        /// 指定通讯线上两个字符到达的最大时延，以毫秒为单位。在ReadFile操作期间，时间周期从第一个字符接收到算起。
        /// 如果收到的两个字符之间的间隔超过该值，ReadFile操作完毕并返回所有缓冲数据。如果ReadIntervalTimeout为0，则该值不起作用。
        /// 如果值为MAXDWORD,并且ReadTotalTimeoutConstant和ReadTotalTimeoutMultiplier两个值都为0, 则指定读操作携带已经收到的字符立即返回，即使没有收到任何字符。
        /// </param>
        /// <param name="readTotalTimeoutMultiplier">读时间系数。读取每字节的超时。读取一个字符时的超时时间。指定以毫秒为单位的累积值。用于计算读操作时的超时总数。对于每次读操作，该值与所要读的字节数相乘。</param>
        /// <param name="readTotalTimeoutConstant">指定以毫秒为单位的常数。用于计算读操作时的超时总数。
        /// 对于每次读操作，ReadTotalTimeoutMultiplier与所要读的字节数相乘后与该值相加。
        /// </param>
        /// <param name="writeTotalTimeoutMultiplier">写一个字符的超时时间。指定以毫秒为单位的累积值。用于计算写操作时的超时总数。对于每次写操作，该值与所要写的字节数相乘。</param>
        /// <param name="writeTotalTimeoutConstant"> 指定以毫秒为单位的常数。用于计算写操作时的超时总数。对于每次写操作，WriteTotalTimeoutMultiplier与所要写的字节数相乘后与该值相加。</param>
        /// <returns></returns>
        public bool SetTimeouts(uint readIntervalTimeout, uint readTotalTimeoutMultiplier, uint readTotalTimeoutConstant, uint writeTotalTimeoutMultiplier, uint writeTotalTimeoutConstant)
        {
            var timeouts = new COMMTIMEOUTS
            {
                ReadIntervalTimeout = readIntervalTimeout,
                ReadTotalTimeoutMultiplier = readTotalTimeoutMultiplier,
                ReadTotalTimeoutConstant = readTotalTimeoutConstant,
                WriteTotalTimeoutMultiplier = writeTotalTimeoutMultiplier,
                WriteTotalTimeoutConstant = writeTotalTimeoutConstant
            };

            return SetCommTimeouts(_Handle, ref timeouts);
        }

        /// <summary>
        ///     Helper that throws a InvalidOperationException if we don'_Thread have a serial connection.
        /// </summary>
        private void FailIfNotConnected(string errSource)
        {
            if (_Handle == IntPtr.Zero)
            {
                Dispose();
                Open();
                throw new InvalidOperationException($"You must be connected to the serial port before performing this operation: {errSource}");
            }
        }

        #region COMMTIMEOUTS

        /// <summary>
        /**
            COMMTIMEOUTS在用ReadFile和WriteFile读写串行口时，需要考虑超时问题。
            如果在指定的时间内没有读出或写入指定数量的字符，那么ReadFile或WriteFile的操作就会结束。
            要查询当前的超时设置应调用GetCommTimeouts函数，该函数会填充一个COMMTIMEOUTS结构。调用SetCommTimeouts可以用某一个COMMTIMEOUTS结构的内容来设置超时。 
            有两种超时：间隔超时和总超时。间隔超时是指在接收时两个字符之间的最大时延，总超时是指读写操作总共花费的最大时间。写操作只支持总超时，而读操作两种超时
            均支持。
            用COMMTIMEOUTS结构可以规定读/写操作的超时，该结构的定义为：
　　            typedef struct _COMMTIMEOUTS {
　　                  DWORD ReadIntervalTimeout; // 读间隔超时。 接收时，两字符间最大的时延。
　　                  DWORD ReadTotalTimeoutMultiplier; // 读时间系数。 读取每字节的超时。
　　                  DWORD ReadTotalTimeoutConstant; // 读时间常量。 读串口数据的固定超时。
                    // 总超时 = ReadTotalTimeoutMultiplier * 字节数 + ReadTotalTimeoutConstant  
　　                  DWORD WriteTotalTimeoutMultiplier; // 写时间系数。写每字节的超时。
　　                  DWORD WriteTotalTimeoutConstant; // 写时间常量。写串口数据的固定超时。
                    // 总超时 = WriteTotalTimeoutMultiplier * 字节数 + WriteTotalTimeoutConstant 
　　            } COMMTIMEOUTS,*LPCOMMTIMEOUTS;

                COMMTIMEOUTS   comTimeOut;    //   COMMTIMEOUTS对象 
                SetCommTimeouts(handlePort_,&comTimeOut);  //   将超时参数写入设备控制

            ReadIntervalTimeout：
                指定通讯线上两个字符到达的最大时延，以毫秒为单位。在ReadFile操作期间，时间周期从第一个字符接收到算起。
                如果收到的两个字符之间的间隔超过该值，ReadFile操作完毕并返回所有缓冲数据。如果ReadIntervalTimeout为0，
                则该值不起作用。
                如果值为MAXDWORD,   并且ReadTotalTimeoutConstant和ReadTotalTimeoutMultiplier两个值都为0，则指定
                读操作携带已经收到的字符立即返回，即使没有收到任何字符。

            ReadTotalTimeoutMultiplier：
                指定以毫秒为单位的累积值。用于计算读操作时的超时总数。对于每次读操作，该值与所要读的字节数相乘。

            ReadTotalTimeoutConstant ： 
                指定以毫秒为单位的常数。用于计算读操作时的超时总数。对于每次读操作，ReadTotalTimeoutMultiplier与所要读的字节数相乘后与该值相加。
                如果ReadTotalTimeoutMultiplier和ReadTotalTimeoutConstant都为0，则在读操作时忽略总超时数。

            WriteTotalTimeoutMultiplier：  
                指定以毫秒为单位的累积值。用于计算写操作时的超时总数。对于每次写操作，该值与所要写的字节数相乘。

            WriteTotalTimeoutConstant：
                指定以毫秒为单位的常数。用于计算写操作时的超时总数。对于每次写操作，WriteTotalTimeoutMultiplier与所要写的字节数相乘后与该值相加。
                如果 WriteTotalTimeoutMultiplier 和 WriteTotalTimeoutConstant都为0，则在写操作时忽略总超时数。
                    提示：用户设置通讯超时后，如没有出错，串口已经被打开。

            COMMTIMEOUTS结构的成员都以毫秒为单位。
            总超时的计算公式是：
                总超时 = 时间系数 × 要求读/写的字符数 + 时间常量
                    例如，如果要读入10个字符，那么读操作的总超时的计算公式为：
                    读总超时 ＝ ReadTotalTimeoutMultiplier × 10 + ReadTotalTimeoutConstant
                    可以看出，间隔超时和总超时的设置是不相关的，这可以方便通信程序灵活地设置各种超时。如果所有写超时参数均为0，那么就不使用写超时。
                    如果ReadIntervalTimeout为0，那么就不使用读间隔超时，如果 ReadTotalTimeoutMultiplier 和 ReadTotalTimeoutConstant 都为0，
                    则不使用读总超时。如果读间隔超时被设置成MAXDWORD并且两个读总超时为0，那么在读一次输入缓冲区中的内容后读操作就立即完成，而不管
                    是否读入了要求的字符。 在用重叠方式读写串行口时，虽然ReadFile和WriteFile在完成操作以前就可能返回，但超时仍然是起作用的。在这
                    种情况下，超时规定的是操作的完成时间，而不是ReadFile和WriteFile的返回时间。
         */
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public uint ReadIntervalTimeout;
            public uint ReadTotalTimeoutMultiplier;
            public uint ReadTotalTimeoutConstant;
            public uint WriteTotalTimeoutMultiplier;
            public uint WriteTotalTimeoutConstant;
        }

        #endregion

        #region Win API Calls
        private NativeOverlapped _NativeOverlapped;
        private Overlapped _Overlapped;

        //-----------------------------------------------------------------------------------------

        private const int EV_BREAK = 0x0040; //A break was detected on input.
        private const int EV_CTS = 0x0008; //The CTS (clear-to-send) signal changed state.
        private const int EV_DSR = 0x0010; //The DSR (data-set-ready) signal changed state.
        private const int EV_ERR = 0x0080; //A line-status error occurred. Line-status errors are CE_FRAME, CE_OVERRUN, and CE_RXPARITY.
        private const int EV_RING = 0x0100; //A ring indicator was detected.
        private const int EV_RLSD = 0x0020; //The RLSD (receive-line-signal-detect) signal changed state.
        private const int EV_RXCHAR = 0x0001; //A character was received and placed in the input buffer.

        private const int EV_RXFLAG = 0x0002;
        //The event character was received and placed in the input buffer. The event character is specified in the device's DCB structure, which is applied to a serial port by using the SetCommState function.

        private const int EV_TXEMPTY = 0x0004; //The last character in the output buffer was sent.
        private readonly IntPtr INVALID_HANDLE_VALUE = IntPtr.Zero;

        private const int FILE_FLAG_OVERLAPPED = 0x40000000;

        // Used to get a handle to the serial port so that we can read/write to it.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile
            (
            string fileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            int flags,
            IntPtr template
            );

        // Used to close the handle to the serial port.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        // Used to get the state of the serial port so that we can configure it.
        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(IntPtr hFile, ref DCB lpDCB);

        // Used to configure the serial port.
        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(IntPtr hFile, [In] ref DCB lpDCB);

        // Used to set the connection timeouts on our serial connection.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCommTimeouts(IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

        // Used to read buffered data
        [DllImport("kernel32", SetLastError = true)]
        private static extern unsafe bool ReadFile(IntPtr hFile, void* pBuffer, int NumberOfBytesToRead, int* pNumberOfBytesRead, int Overlapped);

        // Used to read buffered data
        [DllImport("kernel32", SetLastError = true, EntryPoint = "ReadFile")]
        private static extern unsafe bool ReadFile2(IntPtr hFile, void* pBuffer, int NumberOfBytesToRead, int* pNumberOfBytesRead,
            [In] ref NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern bool SetCommMask(IntPtr hFile, uint dwEvtMask);

        [DllImport("kernel32.dll")]
        private static extern bool WaitCommEvent(IntPtr hFile, out uint lpEvtMask, IntPtr lpOverlapped);

        // Used to write bytes to the serial connection.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteFile
            (
            IntPtr hFile,
            byte[] lpBuffer,
            int nNumberOfBytesToWrite,
            out int lpNumberOfBytesWritten,
            int lpOverlapped
            );

        // Used to write bytes to the serial connection.
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "WriteFile")]
        private static extern bool WriteFile2
            (
            IntPtr hFile,
            byte[] lpBuffer,
            int nNumberOfBytesToWrite,
            out int lpNumberOfBytesWritten,
            ref Overlapped flag
            );

        // Used to flush the I/O buffers.
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool PurgeComm(IntPtr hFile, int dwFlags);

        // Used to populate the COMSTAT structure
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool ClearCommError(IntPtr hFile, ref int lpErrors, ref COMSTAT lpStat);

        // InBufferBytes and OutBufferBytes directly expose cbInQue and cbOutQue to reading, respectively.
        internal struct COMSTAT
        {
            public uint Flags;
            public uint cbInQue;
            public uint cbOutQue;
        }

        // Defines the control setting for a serial communications device.
        [StructLayout(LayoutKind.Sequential)]
        private struct DCB
        {
            public readonly int DCBlength;
            public uint BaudRate;
            public readonly uint Flags;
            public readonly ushort wReserved;
            public readonly ushort XonLim;
            public readonly ushort XoffLim;
            public byte ByteSize;
            public byte Parity;
            public byte StopBits;
            public readonly sbyte XonChar;
            public readonly sbyte XoffChar;
            public readonly sbyte ErrorChar;
            public readonly sbyte EofChar;
            public readonly sbyte EvtChar;
            public readonly ushort wReserved1;
            public uint fBinary;
            public uint fParity;
            public readonly uint fOutxCtsFlow;
            public readonly uint fOutxDsrFlow;
            public uint fDtrControl;
            public readonly uint fDsrSensitivity;
            public uint fTXContinueOnXoff;
            public readonly uint fOutX;
            public readonly uint fInX;
            public readonly uint fErrorChar;
            public readonly uint fNull;
            public readonly uint fRtsControl;
            public uint fAbortOnError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Overlapped
        {
            public IntPtr intrnal;
            public IntPtr internalHigh;
            public int offset;
            public int offsetHigh;
            public IntPtr hEvent;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetOverlappedResult(IntPtr hFile,
            [In] ref NativeOverlapped lpOverlapped,
            out uint lpNumberOfBytesTransferred, bool bWait);

        #endregion
    }

    public class RecievedDataEventArgs : EventArgs
    {
        public string RecievedData { get; set; }
    }
}