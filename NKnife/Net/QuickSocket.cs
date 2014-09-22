using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NKnife.Net
{
    public class QuickSocket
    {
        private const int HEAD_LENGTH = 4;
        private readonly int _BufferLength = 10240;
        
        /// <summary>
        /// 与服务器的连接
        /// </summary>
        protected readonly Socket _Socket;

        private readonly ManualResetEvent _TimeoutResetEvent = new ManualResetEvent(false);

        public QuickSocket(string hostStr, int hostPort, int timeout = 3000, int bufferLength = 10240)
        {
            _BufferLength = bufferLength;
            _Socket = BuildSocket(hostStr, hostPort, timeout);
        }

        public bool Close()
        {
            try
            {
                if (_Socket != null)
                    _Socket.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected Socket BuildSocket(string hostStr, int hostPort, int timeout = 3000)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        {
                            SendTimeout = timeout,
                            ReceiveTimeout = timeout,
                            SendBufferSize = _BufferLength,
                            ReceiveBufferSize = _BufferLength
                        };
            //向指定的IP地址的服务器发出连接请求
            try
            {
                _TimeoutResetEvent.Reset();
                socket.BeginConnect(IPAddress.Parse(hostStr), hostPort, CallBackMethod, socket);
                _TimeoutResetEvent.WaitOne(timeout, false);
            }
            catch(Exception e)
            {
                throw new ApplicationException(string.Format("向指定的IP地址的服务器发出连接请求失败:{0}", e.Message));
            }
            return socket;
        }

        protected void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                var s = asyncresult.AsyncState as Socket;
                if (s != null)
                {
                    s.EndConnect(asyncresult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Socket回调异常:{0}", e.Message);
            }
            finally
            {
                _TimeoutResetEvent.Set();
            }
        }

        /// <summary>
        /// 简单发送协议，并同步返回数据
        /// </summary>
        /// <param name="protocol">即将发送的协议数据</param>
        /// <param name="encoding">发送时的字符编码</param>
        /// <returns></returns>
        public string SendTo(string protocol, Encoding encoding)
        {
            _Socket.Send(encoding.GetBytes(protocol));
            var recvBytes = new byte[_BufferLength];
            _Socket.Receive(recvBytes);
            string replay = encoding.GetString(recvBytes);
            return replay;
        }

        /// <summary>
        /// 发送命令串并同步返回结果
        /// </summary>
        /// <param name="protocol">需发送的数据(字符串)</param>
        /// <param name="needSendRevierse">发送时，头部(数据长度表示)是否需要反转</param>
        /// <param name="needReciveReverse">接收时，头部(数据长度表示)是否需要反转</param>
        /// <param name="oneRead">是否一次完整Buffer读取</param>
        /// <returns></returns>
        public List<byte[]> SendTo(string protocol, bool needSendRevierse = false, bool needReciveReverse = false, bool oneRead = false)
        {
            try
            {
                Byte[] outbytes = Encoding.UTF8.GetBytes(protocol);
                int dataLen = outbytes.Length;
                var bytes2Send = new byte[outbytes.Length + HEAD_LENGTH];
                Byte[] dataLenBytes = BitConverter.GetBytes(dataLen);
                if (needSendRevierse)
                    Array.Reverse(dataLenBytes);

                Array.Copy(dataLenBytes, 0, bytes2Send, 0, HEAD_LENGTH);
                Array.Copy(outbytes, 0, bytes2Send, HEAD_LENGTH, outbytes.Length);

                _Socket.Send(bytes2Send);
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("发送数据异常:{0}", e.Message));
            }

            var receives = new List<byte[]>();
            var mainRecvBytes = new byte[_BufferLength];
            try
            {
                if (oneRead)
                {
                    _Socket.Receive(mainRecvBytes, 0, _BufferLength, SocketFlags.None);
                    var r = new byte[_BufferLength - HEAD_LENGTH];
                    Array.Copy(mainRecvBytes, HEAD_LENGTH, r, 0, _BufferLength - HEAD_LENGTH);
                    receives.Add(r);
                }
                else
                {
                    while (_Socket.Receive(mainRecvBytes, 0, _BufferLength, SocketFlags.None) > 0)
                    {
                        int offset = 0;
                        while (offset + HEAD_LENGTH <= mainRecvBytes.Length)
                        {
                            var head = new byte[HEAD_LENGTH];
                            Array.Copy(mainRecvBytes, offset, head, 0, HEAD_LENGTH);
                            if (needReciveReverse)
                                Array.Reverse(head);
                            int len = BitConverter.ToInt32(head, 0);
                            if (len > 0 && offset + HEAD_LENGTH + len < mainRecvBytes.Length)
                            {
                                var r = new byte[len];
                                Array.Copy(mainRecvBytes, offset + HEAD_LENGTH, r, 0, len);
                                receives.Add(r);
                            }
                            offset = offset + HEAD_LENGTH + len;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("接收数据异常。", e);
            }
            return receives;
        }
    }
}