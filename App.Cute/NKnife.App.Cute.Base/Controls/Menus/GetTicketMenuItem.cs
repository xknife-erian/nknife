using System;
using System.Windows.Forms;

namespace Didaku.Engine.Timeaxis.Base.Controls.Menus
{
    public sealed class GetTicketMenuItem : ToolStripMenuItem
    {
        private string _Queue;
        private Action<string> _GetTicketMethod;
        public GetTicketMenuItem(string queue, Action<string> getTicketMethod)
        {
            Text = "预约";

            _Queue = queue;
            _GetTicketMethod = getTicketMethod;
            Click += delegate { getTicketMethod.Invoke(queue); };
        }
    }
}