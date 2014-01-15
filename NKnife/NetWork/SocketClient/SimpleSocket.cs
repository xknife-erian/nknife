using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Gean.Net.SocketClient
{
    public class SimpleSocket
    {
        private const int BUFFERLENGTH = 10240;
        private const int TIMEOUT = 3000;
        private const int FOUR = 4;

        private readonly string _Host;
        private readonly int _Port;

        /// <summary>
        /// 与服务器的连接
        /// </summary>
        protected Socket _Socket;

        public SimpleSocket(string hostStr, int hostPort)
        {
            _Host = hostStr;
            _Port = hostPort;
            //创建一个客户端套接字，它是Login的一个公共属性，
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

        private void Connect()
        {
            _Socket.Connect(IPAddress.Parse(_Host), _Port);
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
        /// 发送命令串并同步返回结果
        /// </summary>
        /// <param name="protocol">需发送的数据(字符串)</param>
        /// <param name="needSendRevierse">发送时，头部(数据长度表示)是否需要反转</param>
        /// <param name="needReciveReverse">接收时，头部(数据长度表示)是否需要反转</param>
        /// <param name="readMode">是否一次完整Buffer读取。0:不读取;1,一次读取;2,一直读取,直到完成;>10超时时长</param>
        /// <returns></returns>
        public List<byte[]> SendTo(string protocol, bool needSendRevierse = false, bool needReciveReverse = false, int readMode = 1)
        {
            try
            {
                int t = 0;
                if (readMode > 10)
                    t = readMode;
                _Socket = BuildSocket(t);
                Connect();
                Byte[] outbytes = Encoding.UTF8.GetBytes(protocol);
                int dataLen = outbytes.Length;
                var bytes2Send = new byte[outbytes.Length + FOUR];
                Byte[] dataLenBytes = BitConverter.GetBytes(dataLen);
                if (needSendRevierse)
                    Array.Reverse(dataLenBytes);

                Array.Copy(dataLenBytes, 0, bytes2Send, 0, FOUR);
                Array.Copy(outbytes, 0, bytes2Send, FOUR, outbytes.Length);

                _Socket.Send(bytes2Send);
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("发送数据异常:{0}", e.Message));
            }

            var receives = new List<byte[]>();
            var mainRecvBytes = new byte[BUFFERLENGTH];
            try
            {
                switch (readMode)
                {
                    case 1:
                    {
                        _Socket.Receive(mainRecvBytes, 0, BUFFERLENGTH, SocketFlags.None);
                        var r = new byte[BUFFERLENGTH - FOUR];
                        Array.Copy(mainRecvBytes, FOUR, r, 0, BUFFERLENGTH - FOUR);
                        receives.Add(r);
                        break;
                    }
                    case 2:
                    {
                        WhileGetValue(needReciveReverse, receives, mainRecvBytes);
                        break;
                    }
                }
                if (readMode > 50)
                {
                    _Socket.Receive(mainRecvBytes, 0, BUFFERLENGTH, SocketFlags.None);
                    var r = new byte[BUFFERLENGTH - FOUR];
                    Array.Copy(mainRecvBytes, FOUR, r, 0, BUFFERLENGTH - FOUR);
                    receives.Add(r);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("接收数据异常。", e);
            }
            return receives;
        }

        private void WhileGetValue(bool needReciveReverse, List<byte[]> receives, byte[] mainRecvBytes)
        {
            while (_Socket.Receive(mainRecvBytes, 0, BUFFERLENGTH, SocketFlags.None) > 0)
            {
                int offset = 0;
                while (offset + FOUR <= mainRecvBytes.Length)
                {
                    var head = new byte[FOUR];
                    Array.Copy(mainRecvBytes, offset, head, 0, FOUR);
                    if (needReciveReverse)
                        Array.Reverse(head);
                    int len = BitConverter.ToInt32(head, 0);
                    if (len > 0 && offset + FOUR + len < mainRecvBytes.Length)
                    {
                        var r = new byte[len];
                        Array.Copy(mainRecvBytes, offset + FOUR, r, 0, len);
                        receives.Add(r);
                    }
                    offset = offset + FOUR + len;
                }
            }
        }
    }
}