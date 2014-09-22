using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NKnife.Text;

namespace NKnife.Net
{
    public class QuickUdpClient : IDisposable
    {

        /// <summary>
        ///     Udp对象
        /// </summary>
        private UdpClient _ReceiveClient;

        private readonly Encoding _Encoding = Encoding.ASCII;

        /// <summary>
        ///     定义一个接受线程
        /// </summary>
        private Thread _RecvThread;

        /// <summary>
        ///     下载标志
        /// </summary>
        public bool Flag { get; set; }


        /// <summary>
        ///      本地通讯端口(默认8888)
        /// </summary>
        public int LocalPort { get; set; }

        public event EventHandler<SocketArrivedEventArgs> SocketArrived;

        /// <summary>
        ///     断开接收
        /// </summary>
        public bool Done { get; set; }


        /// <summary>
        ///     构造函数设置各项默认值
        /// </summary>
        public QuickUdpClient()
        {
            Done = false;
            Flag = false;
            LocalPort = 7777;
        }

        /// <summary>
        ///     析构函数
        /// </summary>
        ~QuickUdpClient()
        {
            Dispose();
        }


        /// <summary>
        ///     关闭对象
        /// </summary>
        public void Dispose()
        {
            DisConnection();
        }


        /// <summary>
        ///     关闭UDP对象
        /// </summary>
        public void DisConnection()
        {
            if (_ReceiveClient != null)
            {
                Done = true;
                if (_RecvThread != null)
                {
                    _RecvThread.Abort();
                }
                _ReceiveClient.Close();
                _ReceiveClient = null;
            }
        }


        /// <summary>
        /// 发送接收
        /// </summary>
        public void SendAndReceive(string remoteIp, int remotePort, string message)
        {
            var udp = new UdpClient(6666);
            IPEndPoint endpoint = null;
            try
            {
                udp.Connect(remoteIp, remotePort);
                // 连接后传送一个消息给ip主机 
                byte[] sendBytes = _Encoding.GetBytes(message);
                udp.Send(sendBytes, sendBytes.Length);

                //                byte[] data = _ReceiveClient.Receive(ref endpoint);
                //                //得到数据的ACSII的字符串形式 
                //                var strData = _Encoding.GetString(data);
                //                return strData;
            }
            catch
            {
                return;
            }
            finally
            {
                udp.Close();
                udp = null;
            }
        }

        /// <summary>
        /// 发送接收
        /// </summary>
        public void SendAndReceive(string remoteIp, int remotePort, byte[] sendBytes)
        {
            var udp = new UdpClient(6666);
            IPEndPoint endpoint = null;
            try
            {
                udp.Connect(remoteIp, remotePort);
                // 连接后传送一个消息给ip主机 
                udp.Send(sendBytes, sendBytes.Length);

                //                Byte[] data = _ReceiveClient.Receive(ref endpoint);
                //                return data;
            }
            catch
            {
                return;
            }
            finally
            {
                udp.Close();
                udp = null;
            }
        }


        /// <summary>
        ///     侦听线程
        /// </summary>
        public void StartRecvThreadListener()
        {
            try
            {
                _ReceiveClient = new UdpClient(LocalPort);
                // 启动等待连接的线程 
                _RecvThread = new Thread(Received) { Priority = ThreadPriority.Normal };
                _RecvThread.Start();
            }
            catch
            {
            }
        }


        /// <summary>
        ///     循环接收
        /// </summary>
        private void Received()
        {
            Thread.Sleep(10); //防止系统资源耗尽 
            var endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (!Done)
            {

                if (_ReceiveClient != null && _RecvThread.IsAlive)
                {
                    //接收数据   
                    try
                    {
                        Byte[] data = _ReceiveClient.Receive(ref endpoint);
                        //得到数据的ACSII的字符串形式 
                        String strData = data.ToHexString();
                        InvokeSocketArrived(strData);
                    }
                    catch (Exception ex)
                    {
                        InvokeSocketArrived(ex.Message);
                    }
                }
                Thread.Sleep(10); //防止系统资源耗尽 
            }
        }

        private void InvokeSocketArrived(string message)
        {
            var handler = SocketArrived;
            if (handler != null)
            {
                handler.Invoke(this, new SocketArrivedEventArgs(message));
            }
        }
    }



    public class SocketArrivedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public SocketArrivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
