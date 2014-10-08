using System;
using System.IO;
using System.Windows.Forms.VisualStyles;
using Ninject.Modules;
using NKnife.Adapters;
using NKnife.NLog3.Properties;

namespace NKnife.NLog3.Ioc
{
    public class NLogModules : NinjectModule
    {
        public static AppStyle Style = AppStyle.WinForm;

        private const string CONFIG_FILE_NAME = "nlog.config";
        public override void Load()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);
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
                using (var write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }

            Bind<ILoggerFactory>().To<NLogLoggerFactory>().InSingletonScope();
        }

        public enum AppStyle
        {
            WinForm, WPF
        }

    }

}