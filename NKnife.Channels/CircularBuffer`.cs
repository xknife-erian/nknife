using System;
using System.Threading;

namespace NKnife.Channels
{
    /**
        * 缓冲区的可能状态:
        * <code>
        * (1)
        * ----====== data ======-----rspace----
        *     |                 |             |
        *     _ReadNext         _WriteNext    _Capacity-1
        * (2)
        * ==ldata==-------------==== rdata ====
        *          |            |             |
        *          _WriteNext   _ReadNext     _Capacity-1
        * (3)
        * ===ldata=============rdata===========(full of data)
        *             |
        *             _WriteNext(_ReadNext)
        * (4)
        * -------------------------------------(empty)
        *           |
        *           _WriteNext(_ReadNext)
        * </code>
        */

    /// <summary>
    ///     环形缓冲区. 该缓冲区把该数组看作是一个环,
    ///     支持在一块固定的数组上的无限次读和写, 数组的大小不会自动变化.
    /// </summary>
    /// <typeparam name="T">所缓冲的数据类型.</typeparam>
    public class CircularBuffer<T>
    {
        /// <summary>
        ///     默认大小.
        /// </summary>
        private const int DefaultSize = 8 * 1024;

        /// <summary>
        ///     缓冲区所使用的数组.
        /// </summary>
        private T[] _dataBuffer;

        /// <summary>
        ///     下一次读取接收缓冲区的指针.
        /// </summary>
        private int _readPointer;

        /// <summary>
        ///     下一次要将数据写入缓冲区的指针.
        /// </summary>
        private int _writePointer;

        /// <summary>
        ///     如果当前缓冲区中有数据可读, 它将会被设置.
        /// </summary>
        private readonly Semaphore _readSemaphore = new Semaphore(0, 1);

        private readonly Semaphore _writeSemaphore = new Semaphore(1, 1);

        /// <summary>
        ///     缓冲区的容量。注意：_Capacity 和 buffer.Length 可以不相同, 前者小于或者等于后者.
        /// </summary>
        private int _capacity;

        private readonly object _bufferLock = new object();

        /// <summary>
        ///     创建一个具体默认容量的缓冲区.
        /// </summary>
        public CircularBuffer()
            : this(DefaultSize)
        {
        }

        /// <summary>
        ///     创建一个指定容量的缓冲区.
        /// </summary>
        /// <param name="capacity">缓冲区的容量.</param>
        public CircularBuffer(int capacity)
            : this(new T[capacity])
        {
        }

        /// <summary>
        ///     使用指定的数组来创建一个缓冲区, 且该数组已经包含数据.
        /// </summary>
        /// <param name="buffer">缓冲区将要使用的数组.</param>
        /// <param name="offset">数据在数组中的偏移.</param>
        /// <param name="size">数据的字节数.</param>
        public CircularBuffer(T[] buffer, int offset = 0, int size = 0)
        {
            _dataBuffer = buffer;
            _capacity = buffer.Length;
            Available = size;
            Space = _capacity - Available;
            _readPointer = offset;
            _writePointer = offset + size;
        }

        /// <summary>
        ///     缓冲区还能容纳的元素数目.
        /// </summary>
        public int Space { get; private set; }

        /// <summary>
        ///     缓冲区中可供读取的数据的元素数目
        /// </summary>
        public int Available { get; private set; }

        /// <summary>
        ///     接收缓冲区的大小(元素数目). 默认值为8K.
        ///     Capacity 不能设置为小于 Available 的值(实现会忽略这样的值).
        /// </summary>
        public int Capacity
        {
            get => _capacity;
            set
            {
                lock (_bufferLock)
                {
                    if (value < Available || value == 0)
                    {
                        return;
                        //throw new ApplicationException("Capacity must be larger than Available.");
                    }
                    if (value == _capacity)
                    {
                        return;
                    }
                    if (value > _capacity && Space == 0)
                    {
                        // 可写空间变为非空, 释放可写信号.
                        _writeSemaphore.Release();
                    }

                    var buf = new T[value];
                    if (Available > 0)
                    {
                        Available = CopyTo(buf, 0, buf.Length);
                    }
                    _dataBuffer = buf;
                    _capacity = value;
                    Space = _capacity - Available;
                    _readPointer = 0;
                    // 当容量缩小时, 可能导致变化后可写空间为0, 这时wr_nxt=0.
                    _writePointer = (Space == 0) ? 0 : Available;
                }
            }
        }

        /// <summary>
        ///     Read 方法的超时时间(单位毫秒). 默认为 -1, 表示无限长.
        /// </summary>
        public int ReadTimeout { get; } = -1;

        /// <summary>
        ///     Write 方法的超时时间(单位毫秒). 默认为 -1, 表示无限长.
        /// </summary>
        public int WriteTimeout { get; } = -1;

        /// <summary>
        ///     清空本缓冲区.
        /// </summary>
        public void Clear()
        {
            lock (_bufferLock)
            {
                Available = 0;
                Space = _capacity;
                _readPointer = 0;
                _writePointer = 0;
            }
        }

        /*
        /// <summary>
        /// 将读指针向前移动 num 个单元. 如果 num 大于 Avalable,
        /// 将抛出异常.
        /// </summary>
        /// <param name="num">读指针要向前的单元个数.</param>
        /// <exception cref="ApplicationException">num 大于 Avalable.</exception>
        public void Seek(int num)
        {
        }
        */

        /// <summary>
        ///     从缓冲区中读取数据. 读取的字节数一定是 buffer.Length 和 Available 的较小者.
        /// </summary>
        /// <param name="buffer">存储接收到的数据的缓冲区.</param>
        /// <returns>已经读取的字节数. 一定是 size 和 Available 的较小者.</returns>
        public int Read(T[] buffer)
        {
            return Read(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     从缓冲区中读取数据. 读取的字节数一定是 size 和 Available 的较小者.
        ///     本方法是线程安全的.
        /// </summary>
        /// <param name="buffer">存储接收到的数据的缓冲区.</param>
        /// <param name="offset">buffer 中存储所接收数据的位置.</param>
        /// <param name="size">要读取的字节数.</param>
        /// <returns>已经读取的字节数. 一定是 size 和 Available 的较小者.</returns>
        public int Read(T[] buffer, int offset, int size)
        {
            if (!_readSemaphore.WaitOne(ReadTimeout, false))
                throw new ApplicationException("Read timeout.");

            lock (_bufferLock)
            {
                var copyTo = CopyTo(buffer, offset, size);
                if (Space == 0)
                {
                    // 释放可写信号.
                    _writeSemaphore.Release();
                }
                Space += copyTo;
                Available -= copyTo;
                if (Available > 0)
                {
                    // 释放一个信号, 以便下一次再读.
                    _readSemaphore.Release();
                }
                return copyTo;
            }
        }

        /// <summary>
        ///     把本缓冲区的数据复制指定的数组中, 并移动读指针.
        /// </summary>
        private int CopyTo(T[] buffer, int offset, int size)
        {
            var countFormCopy = (Available >= size) ? size : Available;
            // 当 _ReadPointer 在 _WritePointer 的左边时, 缓冲的右边包含的数量.
            var readData = _capacity - _readPointer;
            if (_readPointer < _writePointer || readData >= countFormCopy /*隐含_ReadNext >= _WritePointer*/)
            {
                Array.Copy(_dataBuffer, _readPointer, buffer, offset, countFormCopy);
                _readPointer += countFormCopy;
            }
            else
            {
                // 两次拷贝.
                Array.Copy(_dataBuffer, _readPointer, buffer, offset, readData);
                _readPointer = countFormCopy - readData;
                Array.Copy(_dataBuffer, 0, buffer, offset + readData, _readPointer);
            }
            return countFormCopy;
        }

        /// <summary>
        ///     写入数据到缓冲区.
        /// </summary>
        /// <param name="buffer">要写入的数据的缓冲区.</param>
        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     写入数据到缓冲区. 注意: 本方法不是线程安全的.
        /// </summary>
        /// <param name="buffer">要写入的数据的缓冲区.</param>
        /// <param name="offset">数据缓冲区中要写入数据的起始位置.</param>
        /// <param name="size">要写入的字节数.</param>
        /// <exception cref="ApplicationException">如果空间不足, 会抛出异常.</exception>
        public void Write(byte[] buffer, int offset, int size)
        {
            var nLeft = size;
            var nOffset = offset;
            while (nLeft > 0)
            {
                // 这样的超时控制并不准确!
                if (!_writeSemaphore.WaitOne(WriteTimeout, false))
                {
                    throw new ApplicationException("Write timeout.");
                }

                lock (_bufferLock)
                {
                    var nWrite = (Space >= nLeft) ? nLeft : Space;
                    // 当 _ReadPointer 在 _WritePointer 的左边时, 缓冲的右边可以放置的缓冲数据数量.
                    var rSpace = _capacity - _writePointer;
                    if (_writePointer < _readPointer || rSpace >= nWrite /*隐含_WriteNext >= _ReadPointer*/)
                    {
                        Array.Copy(buffer, nOffset, _dataBuffer, _writePointer, nWrite);
                        _writePointer += nWrite;
                        if (_writePointer == _capacity)
                        {
                            _writePointer = 0;
                        }
                    }
                    else
                    {
                        // 两次拷贝.
                        Array.Copy(buffer, nOffset, _dataBuffer, _writePointer, rSpace);
                        _writePointer = nWrite - rSpace; // 是调用下一句之后的_WriteNext值.
                        Array.Copy(buffer, nOffset + rSpace, _dataBuffer, 0, _writePointer);
                    }
                    if (Available == 0)
                    {
                        _readSemaphore.Release();
                    }
                    Space -= nWrite;
                    Available += nWrite;
                    if (Space > 0)
                    {
                        // 释放可写信号.
                        _writeSemaphore.Release();
                    }

                    nOffset += nWrite;
                    nLeft -= nWrite;
                }
            } // end while
        }
    }
}