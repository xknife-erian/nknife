using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public class FontToolStripComboBox : ToolStripComboBox
    {
        public FontToolStripComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            BindingSystemFont();
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void BindingSystemFont()
        {
            this.ComboBox.DataSource = FontDataTable();
            this.ComboBox.DisplayMember = "text";
            this.ComboBox.ValueMember = "value";
        }

        /// <summary>
        /// 返回系统字体集合的数据表
        /// </summary>
        /// <returns></returns>
        private DataTable FontDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            FontFamily[] MyFamilies = FontFamily.Families;
            foreach (FontFamily MyFamily in MyFamilies)
            {
                dt.Rows.Add(MyFamily.Name, MyFamily.Name);
            }
            return dt;
        }
    }
}
