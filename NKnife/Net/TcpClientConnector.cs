using System;
using System.Net.Sockets;
using System.Threading;

namespace NKnife.Net
{
    public class TcpClientConnector
    {
        ///   <summary>
        ///   在指定时间内尝试连接指定主机上的指定端口。
        ///   </summary>
        ///   <param   name= "hostname "> 要连接到的远程主机的   DNS   名。 </param>
        ///   <param   name= "port "> 要连接到的远程主机的端口号。 </param>
        ///   <param   name= "millisecondsTimeout "> 要等待的毫秒数，或   -1   表示无限期等待。 </param>
        ///   <returns> 已连接的一个   TcpClient   实例。 </returns>
        ///   <remarks> 本方法可能抛出的异常与   TcpClient   的构造函数重载之一
        ///   public   TcpClient(string,   int)   相同，并若指定的等待时间是个负数且不等于
        ///   -1，将会抛出   ArgumentOutOfRangeException。 </remarks>
        public static TcpClient Connect(string hostname, int port, int millisecondsTimeout)
        {
            ConnectorState cs = new ConnectorState();
            cs.Hostname = hostname;
            cs.Port = port;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectThreaded), cs);
            if (cs.Completed.WaitOne(millisecondsTimeout, false))
            {
                if (cs.TcpClient != null) return cs.TcpClient;
                throw cs.Exception;
            }
            else
            {
                cs.Abort();
                throw new SocketException(11001);   //   cannot   connect
            }
        }

        private static void ConnectThreaded(object state)
        {
            ConnectorState cs = (ConnectorState)state;
            cs.Thread = Thread.CurrentThread;
            try
            {
                TcpClient tc = new TcpClient(cs.Hostname, cs.Port);
                if (cs.Aborted)
                {
                    try { tc.GetStream().Close(); }
                    catch { }
                    try { tc.Close(); }
                    catch { }
                }
                else
                {
                    cs.TcpClient = tc;
                    cs.Completed.Set();
                }
            }
            catch (Exception e)
            {
                cs.Exception = e;
                cs.Completed.Set();
            }
        }

        private class ConnectorState
        {
            public string Hostname;
            public int Port;
            public volatile Thread Thread;
            public readonly ManualResetEvent Completed = new ManualResetEvent(false);
            public volatile TcpClient TcpClient;
            public volatile Exception Exception;
            public volatile bool Aborted;
            public void Abort()
            {
                if (Aborted != true)
                {
                    Aborted = true;
                    try { Thread.Abort(); }
                    catch { }
                }
            }
        } 
    }
}
