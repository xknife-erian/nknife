using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Chinese.TouchInput
{
    class Notify
    {
        private readonly NotifyIcon _NotifyIcon;

        public Notify()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add(_ShowMenuItem);
            menu.Items.Add(_ExitMenu);

            _NotifyIcon = new NotifyIcon();
            _NotifyIcon.ContextMenuStrip = new ContextMenuStrip();
            _NotifyIcon.Icon = Properties.Resources.Main;
            _NotifyIcon.MouseClick += _NotifyIcon_MouseClick;

        }

        void _NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            _NotifyIcon.ContextMenuStrip.Show();
        }

        private readonly ToolStripItem _ShowMenuItem = new ToolStripMenuItem("显示窗体");
        private readonly ToolStripItem _ExitMenu = new ToolStripMenuItem("退出");

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
