using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace System.Windows
{
    public static class WindowExtension
    {
        /// <summary>
        /// 以模式方式打开一个窗口，并且在新打开的窗口关闭后返回
        /// </summary>
        /// <param name="win"></param>
        /// <param name="owner">拥有模式窗口的父级</param>
        public static bool? ShowDialog(this Window win, Window owner)
        {
            win.ShowInTaskbar = false;
            win.Topmost = true;
            win.ResizeMode = ResizeMode.NoResize;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //win.Background = "{DynamicResource {x:Static SystemColors.ControlBrushKey}}";
            win.Owner = owner;
            return win.ShowDialog();
        }
    }
}
