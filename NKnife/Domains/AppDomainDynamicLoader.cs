﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Common.Logging;

namespace NKnife.Domains
{
    public abstract class AppDomainDynamicLoader<T>
    {
        private readonly AppDomainProxy _remoteLoader;

        private AppDomain _appDomain;
        protected static readonly TcpClient TcpClient = new TcpClient();

        public abstract AppDomainCommunicationMessages Messages { get; set; }

        protected AppDomainDynamicLoader()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string assemblyName = Assembly.GetCallingAssembly().GetName().FullName;
            var proxyTypename = typeof(T).FullName ?? String.Empty;
            var setup = new AppDomainSetup
                            {
                                ApplicationName = Messages.DomainName,
                                ApplicationBase = baseDir,
                                PrivateBinPath = baseDir,
                                ShadowCopyFiles = "true",
                                CachePath = Path.Combine(baseDir, "Cache\\"), 
                                ShadowCopyDirectories = baseDir,
                            };
            try
            {
                _appDomain = AppDomain.CreateDomain(Messages.DomainName, null, setup);

                //关键，通过 CreateInstanceAndUnwrap 方法启动新的 Domain 中的类似Main函数的类型及方法
                _remoteLoader = (AppDomainProxy)_appDomain.CreateInstanceAndUnwrap(assemblyName, proxyTypename);
            }
            catch (Exception e)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Fatal("呼叫主服务域异常", e);
            }
        }

        public virtual void InvokeMethod(string assemblyFullName, string className, string methodName, string[] args)
        {
            _remoteLoader.InvokeMethod(assemblyFullName, className, methodName, args);
        }

        public virtual void Unload()
        {
            try
            {
                AppDomain.Unload(_appDomain);
                _appDomain = null;
            }
            catch (CannotUnloadAppDomainException ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error("Cannot Unload AppDomain Exception!", ex);
            }
        }

        protected virtual void Send(string message)
        {
            try
            {
                TcpClient.Connect("127.0.0.1", 12340);
                Byte[] data = Encoding.Default.GetBytes(message);
                NetworkStream stream = TcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                TcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("无法连接守护线程。{0}", e.Message);
            }
        }

    }
}