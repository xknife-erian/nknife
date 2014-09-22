using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gean.Net
{
    public class Tcps
    {
        /// <summary>
        /// KeepAlive包，只有很简单的一些TCP信息。
        /// </summary>
        /// <returns></returns>
        public static byte[] GetKeepAliveInfo()
        {
            uint dummy = 0;
            var inOptionValues = new byte[Marshal.SizeOf(dummy)*3];
            BitConverter.GetBytes((uint) 1).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint) 15000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint) 15000).CopyTo(inOptionValues, Marshal.SizeOf(dummy)*2);
            return inOptionValues;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /*
             private void Readme()  
             {  
                 NativeFunc.MIB_TCPTABLE tcpTableData = new NativeFunc.MIB_TCPTABLE();  
                 tcpTableData = NativeFunc.GetTcpTableInfo();  
                 for (int i = 0; i < tcpTableData.dwNumEntries; i++)  
                 {  
                     this.richTextBox1.AppendText(string.Format("{0}:{1}-->>{2}:{3}\n",  
                         NativeFunc.GetIpAddress(tcpTableData.table[i].dwLocalAddr),  
                         NativeFunc.GetTcpPort(tcpTableData.table[i].dwLocalPort).ToString(),  
                         NativeFunc.GetIpAddress(tcpTableData.table[i].dwRemoteAddr),  
                         NativeFunc.GetTcpPort(tcpTableData.table[i].dwRemotePort).ToString()));  
                 }  
             }  
         */
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        [DllImport("Iphlpapi.dll")]
        private static extern int GetTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder);

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ipaddr);

        [DllImport("Ws2_32.dll")]
        private static extern ushort ntohs(ushort netshort);

        /// <summary>
        /// SendArp获取MAC地址  
        /// </summary>
        /// <param name="macip"></param>
        /// <returns></returns>
        public static string GetMacAddress(string macip)
        {
            var strReturn = new StringBuilder();
            try
            {
                Int32 remote = inet_addr(macip);
                var macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);
                string temp = Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();
                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        strReturn.Append(temp.Substring(x - 2, 2));
                    }
                    else
                    {
                        strReturn.Append(temp.Substring(x - 2, 2) + ":");
                    }
                    x -= 2;
                }
                return strReturn.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool IsHostAlive(string strHostIP)
        {
            string strHostMac = GetMacAddress(strHostIP);
            return !string.IsNullOrEmpty(strHostMac);
        }

        public static MIB_TCPTABLE GetTcpTableInfo()
        {
            //声明一个指针准备接受Tcp连接信息  
            IntPtr hTcpTableData = IntPtr.Zero;

            //声明hTcpTableData指针所指向的内存缓冲区大小  
            int iBufferSize = 0;

            //声明MIB_TCPTABLE对象，作为返回值  
            var tcpTable = new MIB_TCPTABLE();

            //声明一个List对象来临时存放MIB_TCPROW对象  
            var lstTcpRows = new List<MIB_TCPROW>();

            //调用API来获得真正的缓冲区大小，iBufferSize默认为0，  
            //这时调用API GetTcpTable会触发一个异常ERROR_INSUFFICIENT_BUFFER  
            //通过这个异常系统会把真正的缓冲长度返回  
            GetTcpTable(hTcpTableData, ref iBufferSize, false);

            //为托管指针在堆上分配内存  
            hTcpTableData = Marshal.AllocHGlobal(iBufferSize);

            //求得MIB_TCPROW对象的内存字节数  
            int iTcpRowLen = Marshal.SizeOf(typeof (MIB_TCPROW));

            //根据上面得到的缓冲区大小来推算MIB_TCPTABLE里的MIB_TCPROW数组长度  
            //下面用缓冲长度-sizeof(int)也就是去掉MIB_TCPTABLE里的成员dwNumEntries所占用的内存字节数  
            var aryTcpRowLength = (int) System.Math.Ceiling((double) (iBufferSize - sizeof (int))/iTcpRowLen);

            //重新取得TcpTable的数据  
            GetTcpTable(hTcpTableData, ref iBufferSize, false);

            //下面是关键，由于MIB_TCPTABLE里的成员有一个是数组，而这个数组长度起初我们是不能确定的  
            //所以这里我们只能根据分配的指针来进行一些运算来推算出我们所要的数据  
            for (int i = 0; i < aryTcpRowLength; i++)
            {
                //hTcpTableData是指向MIB_TCPTABLE缓冲区的内存起始区域，由于其成员数据在内存中是顺序排列  
                //所以我们可以推断hTcpTableData+4(也就是sizeof(dwNumEntries)的长度)后就是MIB_TCPROW数组的第一个元素  
                var hTempTableRow = new IntPtr(hTcpTableData.ToInt32() + 4 + i*iTcpRowLen);
                var tcpRow = new MIB_TCPROW();
                tcpRow.dwLocalAddr = 0;
                tcpRow.dwLocalPort = 0;
                tcpRow.dwRemoteAddr = 0;
                tcpRow.dwRemotePort = 0;
                tcpRow.dwState = 0;

                //把指针数据拷贝到我们的结构对象里。  
                Marshal.PtrToStructure(hTempTableRow, tcpRow);
                lstTcpRows.Add(tcpRow);
            }

            tcpTable.dwNumEntries = lstTcpRows.Count;
            tcpTable.table = new MIB_TCPROW[lstTcpRows.Count];
            lstTcpRows.CopyTo(tcpTable.table);
            return tcpTable;
        }

        public static string GetIpAddress(long ipAddrs)
        {
            try
            {
                var ipAddress = new IPAddress(ipAddrs);
                return ipAddress.ToString();
            }
            catch
            {
                return ipAddrs.ToString();
            }
        }

        public static ushort GetTcpPort(int tcpPort)
        {
            return ntohs((ushort) tcpPort);
        }

        public static bool IsPortBusy(int port)
        {
            MIB_TCPTABLE tcpTableData = GetTcpTableInfo();
            return false;
        }

        #region Nested type: MIB_TCPROW

        [StructLayout(LayoutKind.Sequential)]
        public class MIB_TCPROW
        {
            public int dwState;
            public int dwLocalAddr;
            public int dwLocalPort;
            public int dwRemoteAddr;
            public int dwRemotePort;
        }

        #endregion

        #region Nested type: MIB_TCPTABLE

        [StructLayout(LayoutKind.Sequential)]
        public class MIB_TCPTABLE
        {
            public int dwNumEntries;
            public MIB_TCPROW[] table;
        }

        #endregion
    }
}