using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using NKnife.Adapters;

namespace NKnife.Domains
{
    public class DynamicLoader
    {
        /// <summary>服务的域名
        /// </summary>
        public static readonly string DomainName = String.Format("{0}.AppDomain", typeof(DynamicLoader).Namespace);
        /// <summary>退出服务的协议
        /// </summary>
        public static readonly string ExitProtocol = string.Format("{0}@exit_application", DomainName);

        public static readonly string SplashMessage = string.Format("{0}@splash_message", DomainName);
        public static readonly string StartFormShown = string.Format("{0}@app_loaded", DomainName);

        private readonly AppDomainProxy _RemoteLoader;

        private AppDomain _AppDomain;

        public DynamicLoader()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var sep = Path.DirectorySeparatorChar;
            string assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            var proxyTypename = typeof(AppDomainProxy).FullName ?? String.Empty;
            var setup = new AppDomainSetup
                            {
                                ApplicationName = DomainName,
                                ApplicationBase = baseDir,
                                PrivateBinPath = baseDir,
                                ShadowCopyFiles = "true",
                                //CachePath = Path.Combine(baseDir, "Cache" + sep), 
                                //ShadowCopyDirectories = baseDir,
                            };
            try
            {
                _AppDomain = AppDomain.CreateDomain(DomainName, null, setup);

                //关键，通过 CreateInstanceAndUnwrap 方法启动新的 Domain 中的类似Main函数的类型及方法
                _RemoteLoader = (AppDomainProxy)_AppDomain.CreateInstanceAndUnwrap(assemblyName, proxyTypename);
            }
            catch (Exception e)
            {
                var logger = LogFactory.GetCurrentClassLogger();
                logger.Fatal("呼叫主服务域异常", e);
            }
        }

        public void InvokeMethod(string assemblyFullName, string className, string methodName, string[] args)
        {
            _RemoteLoader.InvokeMethod(assemblyFullName, className, methodName, args);
        }

        public void Unload()
        {
            try
            {
                AppDomain.Unload(_AppDomain);
                _AppDomain = null;
            }
            catch (CannotUnloadAppDomainException ex)
            {
                var logger = LogFactory.GetCurrentClassLogger();
                logger.Error("Cannot Unload AppDomain Exception!", ex);
            }
        }

        private static void Send(string message)
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 12340);
                Byte[] data = Encoding.Default.GetBytes(message);
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("无法连接守护线程。{0}", e.Message);
            }
        }

        public static void SendExitServiceCommand()
        {
            Send(ExitProtocol);
        }

        public static void SendSplashMessage(string splashMsg)
        {
            var msg = string.Format("{0}@{1}", SplashMessage, splashMsg);
            Send(msg);
        }

        public static void SendStartFormShown()
        {
            Send(StartFormShown);
        }

    }
}