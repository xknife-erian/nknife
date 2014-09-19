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
        /// msgΪ�����ַ��������ڲ�������StringParserString.Parse����.
        /// ${res:MainMenu.FileMenu.Name}��ʾ��Դ�ļ�(Debug\CHS\ResourceText.Xml)�е��ַ�����
        /// ${pro:Main}��ʾȫ�����Ե�ֵ��
        /// ${Date}��ʾ��ǰ���ڡ�
        /// ${Time}��ʾ��ǰʱ�䡣
        /// ������ԭ�ⲻ�������
        /// </summary>
        public static DialogResult Show(string msg, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string caption)
        {
            return MessageBox.Show(StringParserService.Parse(msg), caption, buttons, icon, defaultButton);
        }
        /// <summary>
        /// msgΪ�����ַ��������ڲ�������StringParserString.Parse����.
        /// ${res:MainMenu.FileMenu.Name}��ʾ��Դ�ļ�(Debug\CHS\ResourceText.Xml)�е��ַ�����
        /// ${pro:Main}��ʾȫ�����Ե�ֵ��
        /// ${Date}��ʾ��ǰ���ڡ�
        /// ${Time}��ʾ��ǰʱ�䡣
        /// ������ԭ�ⲻ�������
        /// </summary>
        public static DialogResult Show(string msg, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(msg, buttons, icon, MessageBoxDefaultButton.Button1, @"SimplusD!");
        }
        /// <summary>
        /// msgΪ�����ַ��������ڲ�������StringParserString.Parse����.
        /// ${res:MainMenu.FileMenu.Name}��ʾ��Դ�ļ�(Debug\CHS\ResourceText.Xml)�е��ַ�����
        /// ${pro:Main}��ʾȫ�����Ե�ֵ��
        /// ${Date}��ʾ��ǰ���ڡ�
        /// ${Time}��ʾ��ǰʱ�䡣
        /// ������ԭ�ⲻ�������
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
        /// msgΪ�����ַ��������ڲ�������StringParserString.Parse����.
        /// ${res:MainMenu.FileMenu.Name}��ʾ��Դ�ļ�(Debug\CHS\ResourceText.Xml)�е��ַ�����
        /// ${pro:Main}��ʾȫ�����Ե�ֵ��
        /// ${Date}��ʾ��ǰ���ڡ�
        /// ${Time}��ʾ��ǰʱ�䡣
        /// ������ԭ�ⲻ�������
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
