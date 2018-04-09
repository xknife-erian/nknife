using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.Properties;
using NKnife.Kits.ChannelKit.Services;
using NKnife.Kits.ChannelKit.Views;
using NKnife.Wrapper;

namespace NKnife.Kits.ChannelKit
{
    public class AppEnvironment : ApplicationContext
    {
        #region 单件实例

        private static readonly object _lock = new object();
        private static AppEnvironment _instance;

        public static AppEnvironment Instance(string[] args)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new AppEnvironment(args);
            }

            return _instance;
        }

        public static AppEnvironment Instance()
        {
            return Instance(new string[] { });
        }

        #endregion

        private AppEnvironment(string[] args)
        {
            // 注册应用程序事件
            Application.ApplicationExit += OnApplicationExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainDomainUnload;

            GetMainFormSize(out var w, out var h);
            var startFrom = new Form
            {
                BackgroundImage = Resources.Welcome,
                AutoScaleMode = AutoScaleMode.Font,
                Font = new Font("Verdana", 8.25F, FontStyle.Bold | FontStyle.Italic),
                ClientSize = new Size(480, 270),
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.None,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false,
                SizeGripStyle = SizeGripStyle.Hide,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            var myAbout = new MyAbout();
            var versionLabel = new Label
            {
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(20, startFrom.Height - 80),
                Text = $"v.{myAbout.AssemblyVersion}"
            };
            startFrom.Controls.Add(versionLabel);
            startFrom.Shown += (s1, e1) =>
            {
                var mainForm = new MainForm {Width = w, Height = h};
                mainForm.Shown += (s2, e2) =>
                {
                    LoadServices();
                    startFrom.Close();
                };
                mainForm.FormClosed += (s3, e3) => { Application.Exit(); };
                mainForm.Show();
                mainForm.Refresh();
            };
            startFrom.Show();
            startFrom.Refresh();
        }

        private void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            OnApplicationExit(sender, e);
        }

        private static void GetMainFormSize(out int width, out int height)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            if (w > h) //横屏
            {
                width = (int) (w * 0.8);
                height = (int) (h * 0.9);
            }
            else //竖屏
            {
                width = (int) (w * 0.9);
                height = (int) (h * 0.8);
            }
        }

        private void LoadServices()
        {
            DI.Initialize();
            DI.Get<ChannelService>().Initialize();
            Thread.Sleep(1000);
        }

        private void UnloadServices()
        {
        }

        /// <summary> 应用程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
        }

        public class MyAbout : About
        {
        }
    }
}