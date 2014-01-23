using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Net
{
    public class SimpleSocketClient
    {
        private SocketWrapper _SocketWrapper;
        private readonly string _Ip;
        private readonly string _Port;

        public SimpleSocketClient(string ip, string port)
        {
            _Ip = ip;
            _Port = port;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public bool ConnectServer()
        {
            try
            {
                if (_SocketWrapper != null)
                {
                    _SocketWrapper.TcpClient.Close();
                    _SocketWrapper = null;
                }
                _SocketWrapper = new SocketWrapper(_Ip, _Port);
                if (_SocketWrapper != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendCommand(string cmd)
        {
            return _SocketWrapper.SendStr(cmd);
        }

        /// <summary>
        /// 接收从服务器发回的信息，根据不同的命令，执行相应的操作
        /// </summary>
        /// <returns></returns>
        public string ReceiveData()
        {
            //定义一个byte数组，用于接收从服务器端发送来的数据，
            //每次所能接收的数据包的最大长度为1024个字节
            var buff = new byte[1024];
            try
            {
                if (!_SocketWrapper.Stream.CanRead)
                {
                    return "";
                }

                //从流中得到数据，并存入到buff字符数组中
                int len;
                try
                {
                    _SocketWrapper.Stream.ReadTimeout = 30 * 1000;
                    len = _SocketWrapper.Stream.Read(buff, 0, buff.Length);
                }
                catch
                {
                    len = 0;
                }

                if (len < 1) //没有收到任何指令的时候
                {
                    return "";
                }

                //将字符数组转化为字符串
                string msg = Encoding.Default.GetString(buff, 0, len);
                msg.Trim();

                //关闭连接
                _SocketWrapper.Stream.Close();
                _SocketWrapper.TcpClient.Close();
                _SocketWrapper = null;
                return msg;
            }
            catch (Exception err)
            {
                return "";
            }
        }

        public class SocketWrapper
        {
            /// <summary>
            /// 与服务器数据交互的流通道
            /// </summary>
            public NetworkStream Stream { get; private set; }

            public TcpClient TcpClient { get; private set; }

            public SocketWrapper(string hostStr, string hostPort)
            {
                //创建一个客户端套接字
                TcpClient = new TcpClient { ReceiveTimeout = 5000, SendTimeout = 5000 };
                //向指定的IP地址的服务器发出连接请求
                TcpClient.Connect(IPAddress.Parse(hostStr), Int32.Parse(hostPort));
                //获得与服务器数据交互的流通道（NetworkStream)
                Stream = TcpClient.GetStream();
                Stream.ReadTimeout = 5000;
                Stream.WriteTimeout = 5000;
            }

            /// <summary>
            /// 发送命令串
            /// </summary>
            public bool SendStr(string cmd)
            {
                try
                {
                    //将字符串转化为字符数组
                    Byte[] outbytes = Encoding.Default.GetBytes(
                        cmd.ToCharArray());
                    Stream.Write(outbytes, 0, outbytes.Length);
                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }
    }
}
