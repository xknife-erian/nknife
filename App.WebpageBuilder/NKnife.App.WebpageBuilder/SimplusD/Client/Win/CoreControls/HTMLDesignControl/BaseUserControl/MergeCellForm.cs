using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    class MergeCellForm : Form
    {
        private Button OKBtn;

        private void InitializeComponent()
        {
            this.OKBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(243, 65);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 1;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(29, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 36);
            this.panel1.TabIndex = 2;
            // 
            // MergeCellForm
            // 
            this.ClientSize = new System.Drawing.Size(335, 108);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OKBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MergeCellForm";
            this.Text = "合并单元格";
            this.ResumeLayout(false);

        }

        private Panel panel1;

        int _colNum = 0;
        public MergeCellForm(int colNum)
        {
            _colNum = colNum;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            for (int i = 0; i < _colNum; i++)
            {
                CheckBox cb = new CheckBox();
                panel1.Controls.Add(cb);
                cb.Location = new Point(10+i*22, 20);
                cb.Size = new Size(20, 21);
            }
            base.OnLoad(e);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
