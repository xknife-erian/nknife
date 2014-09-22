using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using Jeelu.Billboard;
using System.Diagnostics;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 客户端与服务器端连接，发送/接收消息，分析消息
    /// </summary>
    public class ClientServer
    {
        #region 字段定义
        TcpClient _tcpClient;
        IPAddress _serverIp;
        int _serverPort;
        NetworkStream _stream;
        /// <summary>
        /// sessionId长度 32
        /// </summary>
        int sessionIdLength = 32;
        /// <summary>
        /// 词库版本号长度 6
        /// </summary>
        int versionLength = 6;
        #endregion

        public ClientServer()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string strServerIp = config.AppSettings.Settings["ServerIp"].Value;
            string strServerPort = config.AppSettings.Settings["ServerPort"].Value;
            _serverIp = IPAddress.Parse(strServerIp);
            _serverPort = Convert.ToInt32(strServerPort);
        }
        /// <summary>
        /// 开始连接
        /// </summary>
        public void StartConn()
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(_serverIp, _serverPort);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                //throw;
            }
            _stream = _tcpClient.GetStream();
        }

        /// <summary>
        /// 发送用户验证消息/发送更新字符串消息
        /// </summary>
        public void SendMessage(MessageBag bag)
        {
            byte[] bodybyte = bag.ToBytes();
            _stream.Write(bodybyte, 0, bodybyte.Length);
        }

        /// <summary>
        /// 接收服务器端返回的消息包
        /// </summary>
        public MessageBag ReceiveMessage()
        {
            byte[] buffer = Utility.Stream.ReadStream(_stream, MessageHead.HeadLength);

            if (buffer.Length == 0)
            {
                return null;
            }

            MessageHead head = MessageHead.Parse(buffer);
            if (head.BodyLength > 0)
            {
                byte[] bytesBody = Utility.Stream.ReadStream(_stream, head.BodyLength);

                return new MessageBag(head, bytesBody);
            }

            return new MessageBag(head);
        }

        /// <summary>
        /// 解析服务器返回的用户验证后的消息包
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        public string AnalyzeResponeMessage(MessageBag bag)
        {
            int state = 0;
            KeywordMessageType type = (KeywordMessageType)bag.Head.Type;
            if (type == KeywordMessageType.User || type == KeywordMessageType.Update)
            {
                state = bag.Head.State;
                switch (state)
                {
                    case 0:    //失败
                        byte[] bytes = bag.BytesBody;
                        return Encoding.UTF8.GetString(bytes);
                        
                    case 1:    //成功，版本一样
                        byte[] idBytes = bag.BytesBody;
                        Service.SessionId = Encoding.UTF8.GetString(idBytes);
                        return "true";

                    case 2:    //成功，版本不一样
                        AnalyzeBodyBytes(bag);
                        return "true";

                    default:
                        break;
                }
            }
            if (type == KeywordMessageType.Upload)
            {
                //1为验证; 2为更新
                state = bag.Head.State;
                if (state == 1)
                {
                    //0,表失败; 1表成功，并版本号一样; 2表成功，版本号不一样
                    int cmd = bag.Head.Cmd;
                    switch (cmd)
                    {
                        case 0:
                            byte[] bytes = bag.BytesBody;
                            return Encoding.UTF8.GetString(bytes);
                            
                        case 1:  //可以上传更新
                            byte[] idBytes = bag.BytesBody;
                            Service.SessionId = Encoding.UTF8.GetString(idBytes);
                            return "versionSameOfSuccess";

                        case 2: //更新本地版本，重新显示
                            AnalyzeBodyBytes(bag);
                            return "versionDifferentOfSuccess";

                        default:
                            break;
                    }
                }
                else //状态为2 ,表示上传更新
                {
                    byte[] Bytes = bag.BytesBody;
                    //解析版本号
                    byte[] versionByte = new byte[versionLength];
                    Array.Copy(Bytes, 0, versionByte, 0, versionLength);
                    Service.DictVersion = Encoding.UTF8.GetString(versionByte);

                    //更新本地词库的字符串
                    int leg = versionLength + sessionIdLength;
                    int length = Bytes.Length - versionLength;
                    byte[] bodyBytes = new byte[length];
                    Array.Copy(Bytes, versionLength, bodyBytes, 0, length);
                    string body = Encoding.UTF8.GetString(bodyBytes);

                    Service.UpdateLocalDict(body);

                    return "true"; //更新本地词库成功
                }
            }
            return "";
        }
        /// <summary>
        /// 解析返回来的消息包。格式为:sessionId + version + updateCmd
        /// sessionId 32位 
        /// version 6位
        /// </summary>
        /// <param name="bag"></param>
        private void AnalyzeBodyBytes(MessageBag bag)
        {
            byte[] bodyBytes = bag.BytesBody;

            //1.修改sessionId
            byte[] idByte = new byte[sessionIdLength];
            Array.Copy(bodyBytes, 0, idByte, 0, sessionIdLength);
            Service.SessionId = Encoding.UTF8.GetString(idByte);

            //2.更新词库,修改词库版本号
            byte[] versionByte = new byte[versionLength];
            Array.Copy(bodyBytes, sessionIdLength, versionByte, 0, versionLength);
            Service.DictVersion = Encoding.UTF8.GetString(versionByte);

            //3更新词库(版本一样，则不需要更新;否则，需要更新)
            int leg = idByte.Length + versionByte.Length;
            int bodyLenth = bodyBytes.Length - leg;
            byte[] body = new byte[bodyLenth];
            Array.Copy(bodyBytes, leg, body, 0, bodyLenth);
            string updateCmd = Encoding.UTF8.GetString(body);

            Service.UpdateLocalDict(updateCmd);

        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConn()
        {
            _stream.Close();
            _tcpClient.Close();
        }

    }
}
