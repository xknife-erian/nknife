using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    /// <summary>
    /// 有一个复选框的MessageBox
    /// </summary>
    static public class MessageCheckBox
    {
        static public DialogResult Show(string message, ref bool isChecked)
        {
            return Show(message, ref isChecked, "", "");
        }

        static public DialogResult Show(string message, ref bool isChecked,string checkboxText,string title)
        {
            MessageCheckBoxForm form = new MessageCheckBoxForm(message);

            form.IsChecked = isChecked;
            form.CheckBoxText = checkboxText;
            form.Title = title;

            DialogResult result = form.ShowDialog();
            isChecked = form.IsChecked;
            return result;
        }
    }
}
