using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class EditAgentForm :Form
    {
        DataRow _agentRow = null;
        public EditAgentForm(DataRow agentRow)
        {
            InitializeComponent();

            _agentRow = agentRow;
            areaControl1.areaSource = OMWorkBench.BaseInfoDS.Tables["area"];
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            areaTextBox.Text = OMWorkBench.BaseInfoDS.Tables["area"].Select("id="+_agentRow["area_id"])[0]["name"].ToString();
            AgentIDTextBox.Text = _agentRow["code"].ToString();
            agentNameTextBox.Text = _agentRow["name"].ToString();
            websiteTextBox.Text = _agentRow["website_url"].ToString();
            linkManTextBox.Text = _agentRow["principal_name"].ToString();
            TelTextBox.Text = _agentRow["phone"].ToString();
            mobileTextBox.Text = _agentRow["mobile"].ToString();
            addressTextBox.Text = _agentRow["address"].ToString();
            QQTextBox.Text = _agentRow["qq"].ToString();
            MSNTextBox.Text = _agentRow["msn"].ToString();
            EmailTextBox.Text = _agentRow["email"].ToString();
            string ss = _agentRow["managerName"].ToString();
            parentManagerComboBox.Text = ss;// _agentRow["managerName"].ToString();
            parentAgentComboBox.Text = "";
            areaControl1.Area3ComboBox.SelectedValue = _agentRow["areaCode"].ToString();
            switch (_agentRow["grade"].ToString())
            {
                case "1": firstRadioButton.Checked = true; break;
                case "2": secordRadioButton.Checked = true; break;
                case "3": industryRadioButton.Checked = true; break;
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            //代理商表
            DataRow editAgentRow = _agentRow;
            editAgentRow["code"] = AgentIDTextBox.Text;
            editAgentRow["name"] = agentNameTextBox.Text;
            editAgentRow["website_url"] = websiteTextBox.Text;
            editAgentRow["area_id"] = Convert.ToInt32(areaControl1.Area3ComboBox.SelectedValue);
            editAgentRow["principal_name"] = linkManTextBox.Text;
            editAgentRow["phone"] = TelTextBox.Text;
            editAgentRow["qq"] = QQTextBox.Text;
            editAgentRow["msn"] = MSNTextBox.Text;
            editAgentRow["email"] = EmailTextBox.Text;
            editAgentRow["parent_id"] =Convert.ToInt32(parentAgentComboBox.SelectedValue);
            editAgentRow["manager_id"] = Convert.ToInt32(parentManagerComboBox.SelectedValue);
            editAgentRow["grade"] = OMWorkBench.Grade + 1;

            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void parentAgentComboBox_DropDown(object sender, EventArgs e)
        {
            int grade = -1;
            int areaId = -1;
            int areaId2 = -1;
            if (firstRadioButton.Checked) grade = 0;
            if (industryRadioButton.Checked) grade = 0;
            if (secordRadioButton.Checked)
            {
                grade = 1;
                areaId = Convert.ToInt32(areaControl1.Area1ComboBox.SelectedValue);
                areaId2 = Convert.ToInt32(areaControl1.Area2ComboBox.SelectedValue);
            }
            DataTable agentTable = OMWorkBench.DataAgent.GetAgent(grade, areaId, areaId2);

            parentAgentComboBox.DataSource = agentTable;
            parentAgentComboBox.ValueMember = "id";
            parentAgentComboBox.DisplayMember = "name";
        }

        private void parentManagerComboBox_DropDown(object sender, EventArgs e)
        {
            int agentId = Convert.ToInt32(parentAgentComboBox.SelectedValue);
            DataTable manager = OMWorkBench.DataAgent.GetManager(agentId);

            parentManagerComboBox.DataSource = manager;
            parentManagerComboBox.ValueMember = "id";
            parentManagerComboBox.DisplayMember = "name";
        }

    }


}
