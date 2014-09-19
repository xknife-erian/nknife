using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusOM.Client.Win;

namespace Jeelu.SimplusOM.Client
{
    public partial class EditManagerForm : Form
    {
        string rightStr = null;
        DataTable addTable = null;
        int _managerId = -1;
        Dictionary<int, string> areaPairs = new Dictionary<int, string>();

        public string ManagerName
        {
            get
            {
                return NameTextBox.Text;
            }
        }

        public string ManagerPassWord
        {
            get
            {
                return PassWord1TextBox.Text;
            }
        }

        public EditManagerForm(DataRow managerDR)
        {
            InitializeComponent();

            IDTextBox.Text = managerDR["code"].ToString();
            NameTextBox.Text = managerDR["name"].ToString();
            PassWord1TextBox.Text = PassWord2TextBox.Text = managerDR["password"].ToString();

            BindingSource allSource=new BindingSource();
            allSource.DataSource=OMWorkBench.BaseInfoDS.Tables["area"];

            allSource.Filter="level=1";
            AllAreaListBox.DataSource = allSource;
            AllAreaListBox.ValueMember = "id";
            AllAreaListBox.DisplayMember = "name";

            addTable = OMWorkBench.BaseInfoDS.Tables["area"].Clone();
            AddAreaListBox.DataSource = addTable;
            AddAreaListBox.ValueMember = "id";
            AddAreaListBox.DisplayMember = "name";
        }

        bool Verify()
        {
            if (string.IsNullOrEmpty(NameTextBox.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空！！");
                return false;
            }
            if (PassWord1TextBox.Text != PassWord2TextBox.Text)
            {
                MessageBox.Show("密码不一致！！");
                return false;
            }
            return true;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (!Verify())
                return;

            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
 

        private void AddAreaBtn_Click(object sender, EventArgs e)
        {
            
            int areaId = Convert.ToInt32(AllAreaListBox.SelectedValue);
            DataRow allRow = OMWorkBench.BaseInfoDS.Tables["area"].Select("id=" + areaId)[0];
            if (!areaPairs.ContainsKey(areaId))
            {
                addTable.LoadDataRow(allRow.ItemArray, false);
                areaPairs.Add(Convert.ToInt32(allRow["id"]),allRow["name"].ToString());
            }
            if (AllAreaListBox.SelectedIndex < AllAreaListBox.Items.Count - 1)
                AllAreaListBox.SelectedIndex += 1;
            else
                AllAreaListBox.SelectedIndex = 0;
        }

        private void DelAreaBtn_Click(object sender, EventArgs e)
        {
            if (AddAreaListBox.SelectedValue != null)
            {
                int areaId = Convert.ToInt32(AddAreaListBox.SelectedValue);
                areaPairs.Remove(areaId);
                DataRow addRows = addTable.Select("id=" + areaId)[0];
                addRows.Delete();
            }
        }

        private void PassWordTextBox_Validated(object sender, EventArgs e)
        {
            label5.Visible = (PassWord1TextBox.Text != PassWord2TextBox.Text);
        }



        
    }
}
