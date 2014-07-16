using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NLog;

namespace NKnife.Chinese.TouchInput.Common
{
    internal class Client
    {
        private const int BUFFERLENGTH = 64;
        private const int TIMEOUT = 500;

        private const string HOST = "127.0.0.1";
        private const int PORT = 22033;
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     与服务器的连接
        /// </summary>
        protected Socket _Socket;

        public Client()
        {
            _Socket = BuildSocket();
        }

        public bool Close()
        {
            try
            {
                if (_Socket != null)
                {
                    _Socket.Shutdown(SocketShutdown.Both);
                    _Socket.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Connect()
        {
            _Socket.Connect(IPAddress.Parse(HOST), PORT);
            _Logger.Info("连接{0}:{1}成功", HOST, PORT);
        }

        private static Socket BuildSocket(int timeout = 0)
        {
            int t = (timeout <= 0) ? TIMEOUT : timeout;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = t,
                ReceiveTimeout = t,
                SendBufferSize = BUFFERLENGTH,
                ReceiveBufferSize = BUFFERLENGTH
            };
            return socket;
        }

        /// <summary>
        ///     发送命令串并同步返回结果
        /// </summary>
        public void SendTo(string command)
        {
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(command);
                _Socket.Send(bytes);
            }
            catch (Exception e)
            {
                _Logger.Warn("发送数据异常:{0}", command);
            }
        }
    }
}