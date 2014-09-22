using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Sockets;
using System.Net;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class User
        {
            static private Action _showLoginForm;
            static public void Initialize(Action showLoginForm)
            {
                _showLoginForm = showLoginForm;
            }

            //TODO:User目录下面的一些文件，还没有针对User的更改做处理

            static public bool IsLogined
            {
                get
                {
                    return !string.IsNullOrEmpty(UserID);
                }
            }

            /// <summary>
            /// 登陆时服务器生成的一个Guid,上传时使用此值和用户名一起来验证用户名的有效性.
            /// </summary>
            static public string Passport { get; private set; }

            /// <summary>
            /// 存储用户账号
            /// </summary>
            static public string UserID { get; private set; }

            static public void Logout()
            {
                ///给服务器发一个退出消息;目的是清空服务器上用户的记录
                //SendMessage();

                UserID = "";
            }
            /// <summary>
            /// 将服务器端发送一条用户退出的指令,前同时删除掉
            /// </summary>
            private static void SendMessage()
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string serverIp = config.AppSettings.Settings["ServerIp"].Value;
                string serverPort = config.AppSettings.Settings["ServerPort"].Value;
                IPAddress _ipAddress = IPAddress.Parse(serverIp);
                int _port = Convert.ToInt32(serverPort);
                try
                {
                    TcpClient _tcpClient = new TcpClient();
                    _tcpClient.Connect(_ipAddress, _port);

                    NetworkStream ns = _tcpClient.GetStream();

                    MessageHead head = new MessageHead((int)MessageType.User, 0, 2);
                    MessageBag bag = new MessageBag(head);
                    bag.BytesBody = Encoding.UTF8.GetBytes(UserID);

                    byte[] buffer = bag.ToBytes();
                    ns.Write(buffer, 0, buffer.Length);
                    ns.Close();
                    _tcpClient.Close();
                }
                catch
                { }
            }
            static public void RecordLogin(string userId, string passport)
            {
                ///若是处于登陆状态,先退出.
                if (IsLogined)
                {
                    Logout();
                }

                string oldUserId = userId;
                UserID = userId.ToLower();
                Passport = passport;

                OnUserIDChanged(new ChangedEventArgs<string>(oldUserId, userId));
            }

            static public event EventHandler<ChangedEventArgs<string>> UserIDChanged;
            static void OnUserIDChanged(ChangedEventArgs<string> e)
            {
                if (UserIDChanged != null)
                {
                    UserIDChanged(null, e);
                }
            }

            static public void ShowLoginForm()
            {
                _showLoginForm();
            }
        }
    }
}