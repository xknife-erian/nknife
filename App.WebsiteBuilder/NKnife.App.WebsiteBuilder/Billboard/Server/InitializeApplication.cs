using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Data;
using Jeelu.WordSegmentor;
using System.Windows.Forms;

namespace Jeelu.Billboard.Server
{
    /// <summary>
    /// 窗体应用程序使用前针对各种环境的初始化
    /// </summary>
    internal static class InitializeApplication
    {
        /// <summary>
        /// 窗体应用程序初始化的核心方法
        /// </summary>

        private static Timer timer = new Timer();
        private static DateTime _prevSendDate;
        internal static void InitializeBeforeRun()
        {
            ///初始资源文件的使用环境
            ResourcesReader.InitializeResources(Service.Path.Directory.ConfigLanguage, null);
            ///初始软件选项
            Service.Option.InitializeOption(Service.Path.File.OptionFile);
            ///初始化数据库服务
            Service.DbHelper = DbHelper.Creator(Service.Path.File.SqlXml);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000 *60 * 30;
            timer.Start();
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            if (_prevSendDate != DateTime.Today)
            {
                if (DateTime.Now.Hour == 3)
                {
                    SendEmail.Send();
                    _prevSendDate = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// 初始Jeelu分词组件的工作环境
        /// </summary>
        internal static void InitializeWords()
        {
            ///初始Jeelu分词组件的工作环境
            Service.JWordSegmentor = JWordSegmentorService.Creator(Service.Path.Directory.Config, Service.Path.Directory.SegData);
        }

    }
}
