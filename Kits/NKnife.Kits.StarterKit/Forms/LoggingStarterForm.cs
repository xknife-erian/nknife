using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.NLog3.Controls;
using NKnife.Utility;

namespace NKnife.Kits.StarterKit.Forms
{
    public partial class LoggingStarterForm : Form
    {
        private readonly ILog _Logger = LogManager.GetCurrentClassLogger();

        private Thread _Thread;
        private bool _IsLogger = true;

        public LoggingStarterForm()
        {
            InitializeComponent();

            var logPanel = DI.Get<LogPanel>();//重点, LogPanel是单例的, 所以LogPanel的初始化是特殊的.
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);         
            Controls.Add(logPanel);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // 通过单独的线程随机生成日志
            _Thread = new Thread(() =>
            {
                var random = new UtilityRandom();

                while (_IsLogger)
                {
                    int level = random.Next(0, 6);
                    int sleep = random.Next(100, 500);
                    int length = random.Next(10, 200);
                    string log = random.GetString(length, UtilityRandom.RandomCharType.All);
                    switch (level)
                    {
                        case 0:
                            _Logger.Debug(log);
                            break;
                        case 1:
                            _Logger.Error(log);
                            break;
                        case 2:
                            _Logger.Fatal(log);
                            break;
                        case 3:
                            _Logger.Info(log);
                            break;
                        case 4:
                            _Logger.Trace(log);
                            break;
                        default:
                            _Logger.Warn(log);
                            break;
                    }
                    Thread.Sleep(sleep);
                }
            });
            _Thread.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            _IsLogger = false;
            base.OnClosed(e);
        }
    }
}
