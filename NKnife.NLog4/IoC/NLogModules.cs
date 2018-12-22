using System;
using System.IO;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using Ninject.Modules;
using NKnife.NLog.Properties;
using NKnife.NLog.WinForm;
using NKnife.NLog.WPF;
using NLog;
using LogManager = Common.Logging.LogManager;

namespace NKnife.NLog.IoC
{
    public class NLogModules : NinjectModule
    {
        public enum AppStyle
        {
            WinForm,
            Wpf
        }

        private const string ConfigFileName = "nlog.config";

        static NLogModules()
        {
            Style = AppStyle.WinForm;
        }

        public static AppStyle Style { get; set; }

        public override void Load()
        {
            //当发现程序目录中无NLog的配置文件时，根据程序的模式（WinForm或者WPF）自动释放不同NLog的配置文件
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
            if (!File.Exists(file))
            {
                string configContent;
                switch (Style)
                {
                    case AppStyle.Wpf:
                        configContent = OwnResources.nlog_wpf_config;
                        break;
                    default:
                        configContent = OwnResources.nlog_winform_config;
                        break;
                }
                using (StreamWriter write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }

            //配置Common.Logging适配器
            var properties = new NameValueCollection();
            properties["configType"] = "FILE";
            properties["configFile"] = $"~/{ConfigFileName}";
            LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);


            /****日志组件相关的IoC实例****/
            switch (Style)
            {
                case AppStyle.Wpf:
                {
                    Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
                    Bind<LogMessageFilter>().ToSelf().InSingletonScope();
                    break;
                }
                case AppStyle.WinForm:
                {
                    Bind<LoggerInfoDetailForm>().ToSelf().InSingletonScope();
                    Bind<LoggerCollectionViewModel>().ToSelf();
                    Bind<CustomLogInfo>().ToSelf();
                    break;
                }
            }
        }
    }
}