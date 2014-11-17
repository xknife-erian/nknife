using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Didaku.Engine.Timeaxis.Base.Controls.Menus
{
    public class CounterContextMenuStrip : ContextMenuStrip
    {
        private readonly string _Counter;
        private CallMenuItem _CallMenuItem;
        public CounterContextMenuStrip(string counter)
        {
            _Counter = counter;
        }

        /// <summary>设置呼叫的菜单项及项的点击方法
        /// </summary>
        /// <param name="callMethod">The call method.</param>
        public void SetCallMethod(Action<string> callMethod)
        {
            _CallMenuItem = new CallMenuItem(_Counter, callMethod);
            Items.Add(_CallMenuItem);
        }
    }
}
