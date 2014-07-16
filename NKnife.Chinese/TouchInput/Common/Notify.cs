using System.Windows.Forms;
using NKnife.Logging.Base;

namespace NKnife.Chinese.TouchInput.Common
{
    internal class Notify
    {
        private readonly NotifyIcon _NotifyIcon;
        private readonly Client _Client;
        private bool _IsConnection = false;

        public Notify()
        {
            _NotifyIcon = new NotifyIcon();
            _NotifyIcon.ContextMenuStrip = BuildContextMenuStrip();
            _NotifyIcon.Icon = Properties.Resources.Main;
            _NotifyIcon.MouseClick += _NotifyIcon_MouseClick;
            _Client = new Client();
        }

        private ContextMenuStrip BuildContextMenuStrip()
        {
            ToolStripItem hide = new ToolStripMenuItem("隐藏");
            hide.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(0, 0, 0);
            };

            ToolStripItem a = new ToolStripMenuItem("中文拼音");
            a.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(1, 100, 100);
            };

            ToolStripItem b = new ToolStripMenuItem("手写");
            b.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(1, 100, 100);
            };

            ToolStripItem c = new ToolStripMenuItem("大写英语");
            c.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(1, 100, 100);
            };


            ToolStripItem logger = new ToolStripMenuItem("日志");
            logger.Click += delegate
            {
                var loggerForm = new LoggerForm();
                loggerForm.Show();
            };

            ToolStripItem exit = new ToolStripMenuItem("退出");
            exit.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(-1, 0, 0);
            };

            var menu = new ContextMenuStrip();
            menu.Items.Add(hide);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(a);
            menu.Items.Add(b);
            menu.Items.Add(c);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(logger);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(exit);
            return menu;
        }

        private void _NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            _NotifyIcon.ContextMenuStrip.Show();
        }


        public void Show()
        {
            _NotifyIcon.Visible = true;
        }

        public void Hide()
        {
            _NotifyIcon.Visible = false;
        }
    }
}