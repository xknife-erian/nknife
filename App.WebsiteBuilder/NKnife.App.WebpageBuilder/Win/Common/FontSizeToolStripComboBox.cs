using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public class FontSizeToolStripComboBox : ToolStripComboBox
    {
        public FontSizeToolStripComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox.Width = 15;
            BindingSystemFont();
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void BindingSystemFont()
        {
            for (int i = 1; i <= 7; i++)
            {
                this.ComboBox.Items.Add(i);
            }
        }
    }
}
