using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.Billboard.Server
{
    public class ServerCore
    {
        #region 字段定义
        int idLength = 32;
        int versionLength = 6;

        int port;
        TcpListener listener;
        string passpart;
        string version;
        string mac;
        string incFilePath;
        #endregion

        /// <summary>
        /// 监听客户请求
        /// </summary>
        public void Listen()
        {
            ///读取配置文件，获取端口号
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string serverPort = config.AppSettings.Settings["ServerPort"].Value;
            incFilePath = Path.Combine(Application.StartupPath, "IncFile");
            port = Convert.ToInt32(serverPort);

            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    NetworkStream stream = client.GetStream();

                    //接收消息包
                    MessageBag recBag = ReceiveMessageBag(stream);

                    //解析消息包
                    AnalyzeMessageBag(stream, recBag);
                }

            }
            catch (Exception ex)
            {
                //Debug.Fail(ex.Message);
                ExceptionRecord.Record(ex.Message + ex.Source + ex.TargetSite + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 接收消息包
        /// </summary>
        /// <param name="stream"></param>
        private MessageBag ReceiveMessageBag(NetworkStream stream)
        {
            MessageBag bag = null;
            try
            {
                byte[] buffer = Utility.Stream.ReadStream(stream, MessageHead.HeadLength);
                MessageHead head = MessageHead.Parse(buffer);
                if (head.BodyLength > 0)
                {
                    byte[] bytesBody = Utility.Stream.ReadStream(stream, head.BodyLength);

                    bag = new MessageBag(head, bytesBody);
                }
                else
                {
                    bag = new MessageBag(head);
                }
            }
            catch (Exception ex)
            {
                ExceptionRecord.Record(ex.Message + ex.Source + ex.TargetSite + ex.StackTrace);
                throw;
            }
            return bag;
        }

        /// <summary>
        /// 解析消息包
        /// </summary>
        /// <param name="stream"></param>
        private void AnalyzeMessageBag(NetworkStream stream, MessageBag bag)
        {
            KeywordMessageType type = (KeywordMessageType)bag.Head.Type;
            switch (type)
            {
                case KeywordMessageType.None:
                    break;
                case KeywordMessageType.User:
                    {
                        byte[] body = bag.BytesBody;
                        UserCheck(stream, body, KeywordMessageType.User);
                        break;
                    }
                case KeywordMessageType.Upload:
                    {
                        int state = bag.Head.State;
                        if (state == 1)
                        {
                            //首次上传校验信息
                            byte[] body = bag.BytesBody;
                            string val = UserCheck(stream, body, KeywordMessageType.Upload);
                            
                            //引处校验成功后，将等着接收更新字符串
                            if (!string.IsNullOrEmpty(val) && val == "sameversion")
                            {
                                MessageBag recBag = ReceiveMessageBag(stream);

                                AnalyzeMessageBag(stream, recBag);
                            }
                        }
                        else
                        {
                            //二次传入更新信息
                            byte[] body = bag.BytesBody;
                            string updateStr = Encoding.UTF8.GetString(body);

                            //更新本地词库
                            UpdateServerDict(updateStr);

                            //更改服务器版本号，并保存新增文件
                            string versionR = GetIncVersion();
                            Service.DictVersion = versionR;

                            string fileName = versionR; //不加扩展名
                            SaveFile(fileName, updateStr);

                            //返回"版本号与更新字符串"
                            MessageHead head = new MessageHead((int)KeywordMessageType.Upload, 0, 2);
                            string updateCmd = GetUpdateCmd();
                            byte[] updateBody = GetBodyBytes( Service.DictVersion, updateCmd);
                            MessageBag returnBag = new MessageBag(head, updateBody);
                            SendMessageBag(stream, returnBag);

                        }
                        break;
                    }
                case KeywordMessageType.Update:
                    {
                        byte[] body = bag.BytesBody;
                        UserCheck(stream, body, KeywordMessageType.Update);
                        break;
                    }
                default:
                    break;
            }

        }

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="body"></param>
        private string UserCheck(NetworkStream stream, byte[] body, KeywordMessageType msgType)
        {
            string resultVal = "";
            string all = Encoding.UTF8.GetString(body);
            string[] arrstr = all.Split(new[] { '&' }, StringSplitOptions.None);
            passpart = arrstr[0];
            version = arrstr[1];
            mac = arrstr[2];

            int cmd = 0;

            //验证密码是否正确 //成功，为真；失败为假
            string errorInfo = "";
            bool value = isSuccess(msgType, passpart, mac, out errorInfo);
            if (!value)
            {
                MessageHead head = new MessageHead((int)msgType, 0, 0);
                byte[] info = Encoding.UTF8.GetBytes(errorInfo);
                MessageBag bag = new MessageBag(head, info);

                SendMessageBag(stream, bag);
                resultVal = "";
            }
            else
            {
                string newId = Service.GetSessionId();
                Service.SetSessionId(mac, newId);

                //验证版本号
                if (string.Compare(version, Service.DictVersion) == 0)
                {
                    if (msgType == KeywordMessageType.Upload)
                    {
                        cmd = 1;
                    }
                    MessageHead head = new MessageHead((int)msgType, cmd, 1);

                    byte[] idBody = Encoding.UTF8.GetBytes(newId);
                    MessageBag bag = new MessageBag(head, idBody);

                    SendMessageBag(stream, bag);
                    resultVal = "sameversion";
                }
                else
                {
                    if (msgType == KeywordMessageType.Upload)
                        cmd = 2;

                    MessageHead head = new MessageHead((int)msgType, cmd, 2);

                    string updateCmd = GetUpdateCmd();
                    byte[] updateBody = GetBodyBytes(newId, Service.DictVersion, updateCmd);
                    MessageBag bag = new MessageBag(head, updateBody);

                    SendMessageBag(stream, bag);
                    resultVal = "";
                }
            }
            return resultVal;
        }

        private bool isSuccess(KeywordMessageType msgType, string passpart, string mac, out string errorInfo)
        {
            errorInfo = "";
            if (msgType == KeywordMessageType.User)
            {
                if (string.Compare(passpart, Service.UserLoginPassword) == 0)
                {
                    Service.SetMac(mac);
                    return true;
                }
                else
                {
                    errorInfo = "密码输入错误";
                    return false;
                }
            }
            else
            {
                string val = "";
                bool b = Service.dic.TryGetValue(mac, out val);
                if (b && string.Compare(val, passpart) == 0)
                {
                    return true;
                }

                errorInfo = "用户验证错误";
                return false;
            }
        }

        /// <summary>
        /// 发送消息包
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bag"></param>
        private void SendMessageBag(NetworkStream stream, MessageBag bag)
        {
            try
            {
                byte[] buffer = bag.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
               // Debug.Fail(ex.Message);
                ExceptionRecord.Record(ex.Message + ex.Source + ex.TargetSite + ex.StackTrace);
                throw;
            }
        }

        #region 方法
        /// <summary>
        /// 得到服务器上最新的词库
        /// </summary>
        /// <returns></returns>
        private string GetUpdateCmd()
        {
            //得到服务器上更新的词库
            //看version是否为整型//todo:

            int clientVersion = int.Parse(version) + 1;
            int serverVersion = int.Parse(Service.DictVersion);


            StringBuilder builder = new StringBuilder();
            string path = "";
            do
            {
                path = Path.Combine(incFilePath, clientVersion.ToString());

                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);

                    string updateStr = reader.ReadLine();
                    builder.Append(updateStr);
                    reader.Close();
                }


                clientVersion += 1;
            } while (clientVersion == serverVersion);


            return builder.ToString();
        }

        /// <summary>
        /// 返回"sessionid+版本号+更新字符串"字节
        /// </summary>
        /// <param name="newId"></param>
        /// <param name="version"></param>
        /// <param name="updateCmd"></param>
        /// <returns></returns>
        private byte[] GetBodyBytes(string newId, string version, string updateCmd)
        {
            byte[] idByte = Encoding.UTF8.GetBytes(newId);
            byte[] verByte = Encoding.UTF8.GetBytes(version);
            byte[] updateStr = Encoding.UTF8.GetBytes(updateCmd);

            byte[] bytes = new byte[idLength + versionLength + updateStr.Length];

            int currentIndex = 0;
            Array.Copy(idByte, 0, bytes, 0, idLength);
            currentIndex += idLength;
            Array.Copy(verByte, 0, bytes, currentIndex, versionLength);
            currentIndex += versionLength;
            Array.Copy(updateStr, 0, bytes, currentIndex, updateStr.Length);

            return bytes;
        }

        private byte[] GetBodyBytes(string version, string updateCmd)
        {
            byte[] verByte = Encoding.UTF8.GetBytes(version);

            byte[] updateStr = Encoding.UTF8.GetBytes(updateCmd);

            byte[] bytes = new byte[versionLength + updateStr.Length];

            int currentIndex = 0;
            Array.Copy(verByte, 0, bytes, 0, versionLength);
            currentIndex += versionLength;
            Array.Copy(updateStr, 0, bytes, currentIndex, updateStr.Length);
            return bytes;
        }

        /// <summary>
        /// 更新服务器端词库
        /// </summary>
        /// <param name="updateStr"></param>
        private void UpdateServerDict(string updateStr)
        {
            try
            {
                WordSqlXml sql = new WordSqlXml();
                sql.LoadXml(updateStr);

                foreach (var item in sql)
                {
                    WordSqlXmlAction word = (WordSqlXmlAction)item;
                    word.Run();
                }
            }
            catch (Exception ex)
            {
                ExceptionRecord.Record(ex.Message + ex.Source + ex.TargetSite + ex.StackTrace);
            }
    
        }
        /// <summary>
        /// 词库版本号加1
        /// </summary>
        /// <returns></returns>
        private string GetIncVersion()
        {
            int i = int.Parse(Service.DictVersion) + 1;
            return i.ToString();
        }

        /// <summary>
        /// 保存新增文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="updateStr"></param>
        private void SaveFile(string fileName, string updateStr)
        {
            string path = Path.Combine(incFilePath, fileName);
            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine(updateStr);
            writer.Close();
        }

        #endregion

    }
}
