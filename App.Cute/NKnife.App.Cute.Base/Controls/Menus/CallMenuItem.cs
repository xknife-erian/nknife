using System;
using System.Windows.Forms;

namespace Didaku.Engine.Timeaxis.Base.Controls.Menus
{
    public sealed class CallMenuItem : ToolStripMenuItem
    {
        private string _Counter;
        private Action<string> _CallMethod;
        public CallMenuItem(string counter, Action<string> callMethod)
        {
            Text = "КєНа";

            _Counter = counter;
            _CallMethod = callMethod;
            Click += delegate { callMethod.Invoke(counter); };
        }
    }
}