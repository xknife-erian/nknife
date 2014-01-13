using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Gean.Network
{
    public class QuickSocket
    {
        private const int FOUR = 4;
        private readonly int _BufferLength = 10240;

        /// <summary>
        /// 与服务器的连接
        /// </summary>
        protected readonly Socket _Socket;

        private bool IsConnectionSuccessful = false;
        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);

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
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        {
                            SendTimeout = timeout,
                            ReceiveTimeout = timeout,
                            SendBufferSize = _BufferLength,
                            ReceiveBufferSize = _BufferLength
                        };
            //向指定的IP地址的服务器发出连接请求
            try
            {
                TimeoutObject.Reset();
                s.BeginConnect(IPAddress.Parse(hostStr), hostPort, CallBackMethod, s);
                TimeoutObject.WaitOne(timeout, false);
               
                //s.Connect(IPAddress.Parse(hostStr), hostPort);
            }
            catch
            {
                //throw new ApplicationException(string.Format("向指定的IP地址的服务器发出连接请求失败！:" + e.Message));
            }
            return s;
        }

        protected void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                var s = asyncresult.AsyncState as Socket;
                if (s != null)
                {
                    s.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch
            {
                IsConnectionSuccessful = false;
            }
            finally
            {
                TimeoutObject.Set();
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
            var mainRecvBytes = new byte[_BufferLength];
            try
            {
                if (oneRead)
                {
                    _Socket.Receive(mainRecvBytes, 0, _BufferLength, SocketFlags.None);
                    var r = new byte[_BufferLength - FOUR];
                    Array.Copy(mainRecvBytes, FOUR, r, 0, _BufferLength - FOUR);
                    receives.Add(r);
                }
                else
                {
                    while (_Socket.Receive(mainRecvBytes, 0, _BufferLength, SocketFlags.None) > 0)
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
            catch (Exception e)
            {
                throw new ApplicationException("接收数据异常。", e);
            }
            return receives;
        }
    }
}