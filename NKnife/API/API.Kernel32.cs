using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NKnife.Platform.Win32;

namespace NKnife.API
{
   public sealed partial class API
    {
       /// <summary>
        /// 面向C#使用API的封装:对Kernel32.dll的封装
       /// </summary>
       public class Kernel32
       {
           #region WINUSER.H

           public static int WM_USER = 0x0400;

           #endregion

           [DllImport("kernel32.dll", SetLastError = true)]
           public static extern int SetLocalTime(ref SysDateTime lpSystemTime);

           [DllImport("kernel32.dll", SetLastError = true)]
           public static extern int GetLocalTime(ref SysDateTime lpSystemTime);
       }
    }
}
