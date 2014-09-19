using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    /// <summary>
    /// 服务器核心类。
    /// </summary>
    public class ServerCore
    {
        /// <summary>
        /// 绑定用户的字典
        /// </summary>
        static Dictionary<TcpClient, string> _dicUserClient = new Dictionary<TcpClient, string>();
        private static Dictionary<UserInfo, string> _dicUserInfos = new Dictionary<UserInfo, string>();
        public static Dictionary<UserInfo, string> DicUserInfos
        {
            get { return _dicUserInfos; }
        }

        static public Dictionary<TcpClient, string> DicUserClient
        {
            get
            {
                return _dicUserClient;
            }
        }
        /// <summary>
        /// 绑定用户传输文件
        /// </summary>
        private static Dictionary<string, string> _dicFileInfos = new Dictionary<string, string>();
        public static Dictionary<string, string> DicFileInfos
        {
            get { return _dicFileInfos; }
        }

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<TcpClient, string> _dicFileClient = new Dictionary<TcpClient, string>();

        static public Dictionary<TcpClient, string> DicFileClient
        {
            get
            {
                return _dicFileClient;
            }
        }
        TcpListener _listener;

        static int Port;

        /// <summary>
        /// 监听并处理客户端请求
        /// </summary>
        public void Listen()
        {
            ///读取配置文件，获取端口号
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string serverPort = config.AppSettings.Settings["ServerPort"].Value;
            Port = Convert.ToInt32(serverPort);

            ///初始化PathService
            SimplusD.PathService.Initialize(Application.StartupPath);

            try
            {
                ///初始化Tcp服务端
                _listener = new TcpListener(IPAddress.Any, Port);
                _listener.Start();

                while (true)
                {
                    ///接收客户端请求
                    TcpClient tcpClient = _listener.AcceptTcpClient();
                    
                    ///启动一个线程来处理这此客户端请求
                    ClientThread clientThread = new ClientThread(tcpClient);
                    clientThread._publisherIP = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();

                    Thread thread = new Thread(new ThreadStart(clientThread.ThreadCallback));
                    thread.Start();

                    LogService.WriteServerRunLog(LogLevel.Info, "接收来自" + clientThread._publisherIP + "一个请求,线程启动");
                }
            }
            catch (Exception e)
            {
                ExceptionService.WriteExceptionLog(e);
                MessageService.Show(e.Message);
                Application.Exit();
            }
        }
    }
}
