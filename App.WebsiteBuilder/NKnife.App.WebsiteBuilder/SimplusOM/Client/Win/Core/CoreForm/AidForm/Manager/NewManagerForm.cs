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
    public partial class NewManagerForm : Form
    {
        Manager admin = null;
        string rightStr = null;
        DataTable addTable = null;
        int _agentId = -1;
        Dictionary<int, string> areaPairs = new Dictionary<int, string>();
        public Manager Admin
        {
            get { return admin; }
            set { admin = value; }
        }
        public NewManagerForm(int agentId)
        {
            InitializeComponent();

            _agentId = agentId;
            admin = new Manager();

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
            if (string.IsNullOrEmpty(IDTextBox.Text.Trim()))
            {
                MessageBox.Show("ID不能为空！！");
                return false;
            }
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
            if (string.IsNullOrEmpty(rightStr)) 
            {
                MessageBox.Show("还没有为该用户设置权限！！");
                return false;
            }
            return true;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (!Verify())
                return;
            DataSet managerDS = new DataSet();
            DataTable managerTable = OMWorkBench.BaseInfoDS.Tables["manager"].Clone();
            DataTable managerAreaTable = OMWorkBench.BaseInfoDS.Tables["manager_area"].Clone();

            DataRow newRow = managerTable.NewRow();
            newRow["code"] = IDTextBox.Text;
            newRow["name"] = NameTextBox.Text;
            newRow["password"] = PassWord1TextBox.Text;
            newRow["organization_id"] = _agentId;
           // newRow["manager_type"] = 0;
            newRow["current_state"] = 0;
            newRow["rights"] = rightStr;
            managerTable.LoadDataRow(newRow.ItemArray, false);

            foreach (int areaId  in areaPairs.Keys)
            {
                DataRow newAreaRow = managerAreaTable.NewRow();
                newAreaRow["area_id"] = areaId;
                managerAreaTable.LoadDataRow(newAreaRow.ItemArray, false);
            }

            managerDS.Tables.Add(managerTable);
            managerDS.Tables.Add(managerAreaTable);
            OMWorkBench.DataAgent.NewManager(managerDS);

            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
 

        private void RightBtn_Click(object sender, EventArgs e)
        {
            RightSetForm right = new RightSetForm("ÿÿÿÿÿÿÿÿÿÿùw");
            if (right.ShowDialog(this) == DialogResult.OK)
            {
                string bitRight = right.BitArrayStr;
                rightStr = OMWorkBench.BitStrToStr(bitRight);
            }
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
