using System;
using System.Collections.Generic;
using System.Text;
using WinForms = System.Windows.Forms;
using System.Diagnostics;

namespace Gean
{
    /// <summary>
    /// 相关键盘的键(Key)封装的类
    /// </summary>
    public static class UtilityKeys
    {
        /// <summary>
        /// 解析一个描述键值的字符串为键，例如: ctrl+shift+a
        /// </summary>
        /// <param name="shortcutChar">描述键值的字符串,例如: ctrl+shift+a</param>
        /// <returns>Windows的键</returns>
        public static WinForms.Keys ParseByShortcutChar(string shortcutChar)
        {
            if (string.IsNullOrEmpty(shortcutChar))
            {
                return WinForms.Keys.None;
            }

            WinForms.Keys key = WinForms.Keys.None;
            string[] strs = shortcutChar.Split('+');
            foreach (string str in strs)
            {
                string firstchar;
                string output;

                //用Switch处理一部份可能会简写的键
                switch (str.ToLowerInvariant())
                {
                    case "ctrl":
                        output = "Control";
                        break;
                    case "esc":
                        output = "Escape";
                        break;
                    case "del":
                        output = "Delete";
                        break;
                    default:
                        firstchar = str.Substring(0, 1).ToUpperInvariant();
                        output = firstchar + str.ToLowerInvariant().Substring(1);
                        break;
                }
                try
                {
                    key |= (WinForms.Keys)Enum.Parse(typeof(WinForms.Keys), output);
                }
                catch (Exception ex)//如解析失败，将返回的是未按键
                {
                    Debug.Fail(ex.Message);
                }
            }
            return key;
        }

    }
}
