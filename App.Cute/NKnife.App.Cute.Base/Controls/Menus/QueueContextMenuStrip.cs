using System;
using System.Windows.Forms;

namespace Didaku.Engine.Timeaxis.Base.Controls.Menus
{
    public class QueueContextMenuStrip : ContextMenuStrip
    {
        private readonly string _Queue;
        private GetTicketMenuItem _GetTicketMenuItem;
        public QueueContextMenuStrip(string counter)
        {
            _Queue = counter;
        }

        /// <summary>设置“预约”的菜单项及项的点击方法
        /// </summary>
        public void SetGetTicketMethod(Action<string> getTicketMethod)
        {
            _GetTicketMenuItem = new GetTicketMenuItem(_Queue, getTicketMethod);
            Items.Add(_GetTicketMenuItem);
        }
    }
}