using System;
using System.Collections.ObjectModel;
using System.IO;
using Ninject.Modules;
using NKnife.Adapters;
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
        }

        public override void Load()
        {
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

            Bind<ILoggerFactory>().To<NLogLoggerFactory>().InSingletonScope();
            Bind<ObservableCollection<LogMessage>>().To<LogMessageObservableCollection>().InSingletonScope();
            Bind<LogPanel>().To<LogPanel>().InSingletonScope();
            Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
            Bind<LogMessageFilter>().ToSelf().InSingletonScope();
        }
    }
}