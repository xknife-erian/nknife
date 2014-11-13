using System;
using System.Collections.ObjectModel;
using System.IO;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using Ninject.Modules;
using Common.Logging;
using NKnife.NLog3.Controls;
using NKnife.NLog3.Controls.WPF;
using NKnife.NLog3.Properties;

namespace NKnife.NLog3.IoC
{
    public class NLogModules : NinjectModule
    {
        public enum AppStyle
        {
            WinForm,
            WPF
        }

        private const string CONFIG_FILE_NAME = "nlog.config";
        public static AppStyle Style { get; set; }

        static NLogModules()
        {
            Style = AppStyle.WinForm;

            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);
            if (!File.Exists(file))
            {
                string configContent;
                switch (Style)
                {
                    case AppStyle.WPF:
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

            var properties = new NameValueCollection();
            properties["configType"] = "FILE";
            properties["configFile"] = "~/NLog.config";
            LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);
        }

        public override void Load()
        {


                Bind<ObservableCollection<LogMessage>>().To<LogMessageObservableCollection>().InSingletonScope();
                Bind<LogPanel>().To<LogPanel>().InSingletonScope();
                Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
                Bind<LogMessageFilter>().ToSelf().InSingletonScope();
            
        }
    }
}