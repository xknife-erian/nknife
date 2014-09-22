using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu
{
    public static class MessageService
    {
        static private Form _mainForm;
        public static void Initialize(Form mainForm)
        {
            _mainForm = mainForm;
        }
        /// <summary>
        /// msg为解析字符串。其内部调用了StringParserString.Parse方法.
        /// ${res:MainMenu.FileMenu.Name}表示资源文件(Debug\CHS\ResourceText.Xml)中的字符串。
        /// ${pro:Main}表示全局属性的值。
        /// ${Date}表示当前日期。
        /// ${Time}表示当前时间。
        /// 其它的原封不动输出。
        /// </summary>
        public static DialogResult Show(string msg, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string caption)
        {
            return MessageBox.Show(StringParserService.Parse(msg), caption, buttons, icon, defaultButton);
        }
        /// <summary>
        /// msg为解析字符串。其内部调用了StringParserString.Parse方法.
        /// ${res:MainMenu.FileMenu.Name}表示资源文件(Debug\CHS\ResourceText.Xml)中的字符串。
        /// ${pro:Main}表示全局属性的值。
        /// ${Date}表示当前日期。
        /// ${Time}表示当前时间。
        /// 其它的原封不动输出。
        /// </summary>
        public static DialogResult Show(string msg, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(msg, buttons, icon, MessageBoxDefaultButton.Button1, @"SimplusD!");
        }
        /// <summary>
        /// msg为解析字符串。其内部调用了StringParserString.Parse方法.
        /// ${res:MainMenu.FileMenu.Name}表示资源文件(Debug\CHS\ResourceText.Xml)中的字符串。
        /// ${pro:Main}表示全局属性的值。
        /// ${Date}表示当前日期。
        /// ${Time}表示当前时间。
        /// 其它的原封不动输出。
        /// </summary>
        public static DialogResult Show(string msg, MessageBoxButtons buttons)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (buttons != MessageBoxButtons.OK)
            {
                icon = MessageBoxIcon.Question;
            }
            return Show(msg, buttons, icon);
        }
        /// <summary>
        /// msg为解析字符串。其内部调用了StringParserString.Parse方法.
        /// ${res:MainMenu.FileMenu.Name}表示资源文件(Debug\CHS\ResourceText.Xml)中的字符串。
        /// ${pro:Main}表示全局属性的值。
        /// ${Date}表示当前日期。
        /// ${Time}表示当前时间。
        /// 其它的原封不动输出。
        /// </summary>
        public static DialogResult Show(string msg)
        {
            return Show(msg, MessageBoxButtons.OK);
        }

        public static CloseAllWindowOptionResult ShowAdvance(string msg)
        {
            CloseAllWindowSaveForm form = new CloseAllWindowSaveForm(msg);
            form.ShowDialog();
            return form.Result;
        }
    }
}
