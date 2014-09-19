using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public class ParaToolStripComboBox : ToolStripComboBox
    {
        public ParaToolStripComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox.Width = 20;
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

            dt.Rows.Add("P", "段落");
            dt.Rows.Add("H1", "标题1");
            dt.Rows.Add("H2", "标题2");
            dt.Rows.Add("H3", "标题3");
            dt.Rows.Add("H4", "标题4");
            dt.Rows.Add("H5", "标题5");
            dt.Rows.Add("H6", "标题6");

            return dt;
        }
    }
}
