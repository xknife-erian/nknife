using System;
using System.IO;
using NKnife.NLog.WinForm.Properties;

namespace NKnife.NLog.WinForm
{
    public class NLogConfigFileSimpleCreate
    {
        private const string CONFIG_FILE_NAME = "nlog.config";

        public static void Load()
        {
            //当发现程序目录中无NLog的配置文件时，自动释放NLog的基础配置文件
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);
            if (!File.Exists(file))
            {
                string configContent = Resources.nlog_winform_config;
                using (StreamWriter write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }
        }
    }
}