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
            _NotifyIcon = new NotifyIcon();
            _NotifyIcon.Icon = Properties.Resources.Main;

            _NotifyIcon.ContextMenu = new ContextMenu();
            _NotifyIcon.ContextMenu.MenuItems.Add(this.showDlgMenu);
            _NotifyIcon.ContextMenu.MenuItems.Add(this.exitMenu);
        }

        private MenuItem showDlgMenu = new MenuItem("显示窗体");
        private MenuItem exitMenu = new MenuItem("退出");

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
