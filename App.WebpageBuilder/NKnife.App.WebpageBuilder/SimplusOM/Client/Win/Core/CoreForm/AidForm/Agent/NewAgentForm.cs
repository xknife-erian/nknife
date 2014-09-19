using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class NewAgentForm : Form
    {
        public NewAgentForm(AgentType agentType)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataTable _areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            areaControl1.areaSource = _areaTable;

            areaTextBox.Text = _areaTable.Select("id=" + OMWorkBench.AreaId)[0]["name"].ToString();
            
            bool lateRight=(OMWorkBench.AgentAddAllLatent || OMWorkBench.AgentAddUnderLatent || OMWorkBench.AgentAddUnderSubLatent);
            bool direRight=(OMWorkBench.AgentAddAll || OMWorkBench.AgentAddUnder || OMWorkBench.AgentAddUnderSub);
            if (!lateRight)//没有添加潜在代理的权限
            {
                standardRadioButton.Checked = true;
                latencyRadioButton.Enabled = false;
            }
            if (!direRight)
            {
                latencyRadioButton.Checked = true;
                standardRadioButton.Enabled = false;
            }
        }

        bool Verify()
        {
            if (string.IsNullOrEmpty(AgentIDTextBox.Text.Trim())) return false;
            if (parentAgentComboBox.SelectedValue == null) return false;
            if (parentManagerComboBox.SelectedValue == null) return false;
            return true;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (!Verify())
                return;
            DataSet changeDS = OMWorkBench.BaseInfoDS.Clone();

            DataTable agentTable = changeDS.Tables["organization"];
            DataTable managerTable = changeDS.Tables["manager"];
            DataTable monthTaskTable = changeDS.Tables["month_task"];
            DataTable seasonTaskTable = changeDS.Tables["season_task"];
            DataTable orgCashFlowTable = changeDS.Tables["organize_cash_flow"];

            //代理商表
            DataRow newAgentRow = agentTable.NewRow();
            newAgentRow["code"] = AgentIDTextBox.Text;
            newAgentRow["password"] = "21218cca77804d2ba1922c33e0151105";
            newAgentRow["name"] = agentNameTextBox.Text;
            newAgentRow["website_url"] = websiteTextBox.Text;
            newAgentRow["area_id"] = OMWorkBench.AreaId;
            newAgentRow["principal_name"] = linkManTextBox.Text;
            newAgentRow["phone"] = TelTextBox.Text;
            newAgentRow["mobile"] = mobileTextBox.Text;
            newAgentRow["qq"] = QQTextBox.Text;
            newAgentRow["msn"] = MSNTextBox.Text;
            newAgentRow["email"] = EmailTextBox.Text;
            newAgentRow["address"] = addressTextBox.Text;
            newAgentRow["parent_id"] = Convert.ToInt32(parentAgentComboBox.SelectedValue);
            newAgentRow["manager_id"] = Convert.ToInt32(parentManagerComboBox.SelectedValue);
            newAgentRow["grade"] = OMWorkBench.Grade + 1;
            newAgentRow["regtime"] = AddTimeDateTimePicker.Value;
            newAgentRow["is_reserve"] = latencyRadioButton.Checked ? 'y' : 'n';
            agentTable.LoadDataRow(newAgentRow.ItemArray, false);
            //管理员表
            DataRow newManagerRow = managerTable.NewRow();
            newManagerRow["code"] = AgentIDTextBox.Text;
            newManagerRow["name"] = agentNameTextBox.Text;
            newManagerRow["password"] = "21218cca77804d2ba1922c33e0151105";
            // newManagerRow["manager_type"] = "0";
            newManagerRow["current_state"] = "0";
            newManagerRow["rights"] = "111111111111";
            managerTable.LoadDataRow(newManagerRow.ItemArray, false);
            if (standardRadioButton.Checked)
            {
                //月任务表
                DataRow monthTaskRow = monthTaskTable.NewRow();
                monthTaskRow["start_date"] = AddTimeDateTimePicker.Value;
                monthTaskRow["rate_d"] = return1TextBox.Text;
                monthTaskRow["rate_c"] = return2TextBox.Text;
                monthTaskRow["rate_b"] = return3TextBox.Text;
                monthTaskRow["rate_a"] = return4TextBox.Text;
                monthTaskRow["task_amount"] = MonthTaskTextBox.Text;
                monthTaskRow["finished_amount"] = 0;// firstChargeTextBox.Text;
                monthTaskTable.LoadDataRow(monthTaskRow.ItemArray, false);
                //季任务表
                DataRow seasonTaskRow = seasonTaskTable.NewRow();
                seasonTaskRow["start_date"] = AddTimeDateTimePicker.Value;
                seasonTaskRow["rate_base"] = quarterReturn1TextBox.Text;
                seasonTaskRow["rate_inc"] = quarterReturn2TextBox.Text;
                seasonTaskRow["task_amount"] = quarterTaskTextBox.Text;
                seasonTaskRow["finished_amount"] = 0;// firstChargeTextBox.Text;
                seasonTaskTable.LoadDataRow(seasonTaskRow.ItemArray, false);
                //代理商现金流量表
                DataRow orgCashFlowRow = orgCashFlowTable.NewRow();
                orgCashFlowRow["amount"] = firstChargeTextBox.Text;
                orgCashFlowRow["finance_type"] = 1;
                orgCashFlowRow["trade_time"] = AddTimeDateTimePicker.Value;
                orgCashFlowRow["send_organize_id"] = OMWorkBench.AgentId;
                orgCashFlowRow["send_manager_id"] = OMWorkBench.MangerId;
                orgCashFlowTable.LoadDataRow(orgCashFlowRow.ItemArray, false);
            }
            OMWorkBench.DataAgent.NewAgent(changeDS);
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MonthTaskTextBox_TextChanged(object sender, EventArgs e)
        {
            quarterTaskTextBox.Text = Convert.ToString(decimal.Parse(MonthTaskTextBox.Text) * 3);
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

        /// <summary>
        /// 潜在代理商不予充值和返点设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void latencyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            cashFlowGroupBox.Enabled = !latencyRadioButton.Checked;
        }



    }
}
