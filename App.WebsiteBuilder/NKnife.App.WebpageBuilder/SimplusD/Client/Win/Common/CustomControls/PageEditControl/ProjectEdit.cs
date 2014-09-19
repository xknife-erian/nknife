using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProjectEdit : Form
    {
        public ProjectPart projectPartList;
       
        public ProjectEdit()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建";
            projectPartList = new ProjectPart();
        }
        public ProjectEdit(ProjectPart part)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑";
            projectPartList = part;
            MadeEdit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        private void MadeAdd()
        {
            projectPartList.PartName = txtName.Text;
            projectPartList.PartStartTime = Convert.ToDateTime(conStartTime.Text);
            projectPartList.PartEndTime = Convert.ToDateTime(conEndTime.Text);
            if (!string.IsNullOrEmpty(txtCost.Text))
            {
                projectPartList.partCost = Convert.ToDouble(txtCost.Text);
            }
            if (this.conDoing.Checked)
            {
                projectPartList.IsDoing = true;
            }
        }
        private void MadeEdit()
        {
            txtName.Text = projectPartList.PartName;
            conStartTime.Text = projectPartList.PartStartTime.ToString();
            conEndTime.Text = projectPartList.PartEndTime.ToString();
            txtCost.Text = projectPartList.partCost.ToString();
            if (projectPartList.IsDoing)
            {
                this.conDoing.Checked = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MadeAdd();
            this.DialogResult = DialogResult.OK;
            
        }
        
    }
}
