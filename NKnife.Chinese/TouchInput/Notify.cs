using System.Windows.Forms;

namespace NKnife.Chinese.TouchInput
{
    internal class Notify
    {
        private readonly NotifyIcon _NotifyIcon;

        public Notify()
        {
            _NotifyIcon = new NotifyIcon();
            _NotifyIcon.ContextMenuStrip = BuildContextMenuStrip();
            _NotifyIcon.Icon = Properties.Resources.Main;
            _NotifyIcon.MouseClick += _NotifyIcon_MouseClick;
        }

        private static ContextMenuStrip BuildContextMenuStrip()
        {
            ToolStripItem hide = new ToolStripMenuItem("隐藏");
            hide.Click += delegate { };

            ToolStripItem a = new ToolStripMenuItem("中文拼音");
            hide.Click += delegate { };

            ToolStripItem b = new ToolStripMenuItem("手写");
            hide.Click += delegate { };

            ToolStripItem c = new ToolStripMenuItem("大写英语");
            hide.Click += delegate { };


            ToolStripItem logger = new ToolStripMenuItem("日志");
            logger.Click += delegate { };

            ToolStripItem exit = new ToolStripMenuItem("退出");
            exit.Click += delegate { };

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