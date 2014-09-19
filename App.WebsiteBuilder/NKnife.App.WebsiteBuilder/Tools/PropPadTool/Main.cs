using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Jeelu.SimplusD;
using Jeelu.Win;

namespace Jeelu.Tools.PropPadTool
{
    public partial class Main : Form
    {
        PropertyPadAttribute needObject;
        ContextMenuStrip menu;
        public Main()
        {
            InitializeComponent();
            menu = new ContextMenuStrip();

            ToolStripMenuItem item;
            item = new ToolStripMenuItem("全选(&A)");
            item.Click += new EventHandler(AllSelect_Click);
            menu.Items.Add(item);
            item = new ToolStripMenuItem("拷贝(&C)");
            item.Click += new EventHandler(Copy_Click);
            menu.Items.Add(item);
            item = new ToolStripMenuItem("还原(&F)");
            item.Click += new EventHandler(Init_Click);
            menu.Items.Add(item);
            _textBox.ContextMenuStrip = menu;

            this.Text = "属性面板定制特性代码生成器 by Lukan@Jeelu.com, 2008.2.2 12:45";

            InitData();
        }

        void Init_Click(object sender, EventArgs e)
        {
            InitData();
        }

        void Copy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        void AllSelect_Click(object sender, EventArgs e)
        {
            _textBox.SelectAll();
        }

        void InitData()
        {
            needObject = new PropertyPadAttribute(0, 0, string.Empty);
            this.propertyGrid.SelectedObject = needObject;
            
            StringBuilder sb = new StringBuilder();
            sb.Append("[PropertyPad(").Append(needObject.GroupBoxIndex).Append(", ").
               Append(needObject.MainControlIndex).Append(", null)]");
            
            this._textBox.Text = sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this._textBox.Text = needObject.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void Copy()
        {
            if (string.IsNullOrEmpty(this._textBox.Text))
            {
                MessageBox.Show("Not Data!");
                return;
            }
            else
            {
                Clipboard.SetDataObject(this._textBox.Text, true);
                MessageBox.Show(this, "定制特性设置代码已置入剪贴板。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}