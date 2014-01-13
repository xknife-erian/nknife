using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class ControlExtension
    {
        public static bool IsEmptyText(this Control control)
        {
            return string.IsNullOrWhiteSpace(control.Text);
        }

        public delegate void InvokeHandler();

        /// <summary>非本线程安全访问控件
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="handler">The handler.</param>
        public static void SafeInvoke(this Control control, InvokeHandler handler)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(handler);
            }
            else
            {
                handler();
            }
        }
    }
}
