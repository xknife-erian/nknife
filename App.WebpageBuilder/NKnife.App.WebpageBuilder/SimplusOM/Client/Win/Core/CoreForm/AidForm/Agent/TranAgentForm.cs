using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class TranAgentForm : Form
    {
        DataTable agentTable = null;
        int _agentId = -1;
        public TranAgentForm(int agentId)
        {
            InitializeComponent();
            _agentId = agentId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataSet agentDS =OMWorkBench.DeserializeDataSet( OMWorkBench.DataAgent.GetSingleAgent(_agentId));
            DataRow agentDR = agentDS.Tables[0].Rows[0];

            agentIDTextBox.Text = agentDR["code"].ToString();
            agentNameTextBox.Text = agentDR["name"].ToString();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DataSet changeDS = OMWorkBench.BaseInfoDS.Clone();

            DataTable agentTable =OMWorkBench.DeserializeDataSet( OMWorkBench.DataAgent.GetSingleAgent(_agentId)).Tables[0];// changeDS.Tables["organization"];
            DataTable managerTable = OMWorkBench.DataAgent.GetManager(_agentId);// changeDS.Tables["manager"];
            changeDS.Tables["organization"].Merge(agentTable.Copy());
            changeDS.Tables["manager"].Merge(managerTable);
            DataTable orgTable = changeDS.Tables["organization"];
            DataTable monthTaskTable = changeDS.Tables["month_task"];
            DataTable seasonTaskTable = changeDS.Tables["season_task"];
            DataTable orgCashFlowTable = changeDS.Tables["organize_cash_flow"];

            orgTable.Rows[0]["is_reserve"] = 'n';
            //月任务表
            DataRow monthTaskRow = monthTaskTable.NewRow();
            monthTaskRow["start_date"] = AddTimeDateTimePicker.Value;
            monthTaskRow["rate_d"] = return1TextBox.Text;
            monthTaskRow["rate_c"] = return2TextBox.Text;
            monthTaskRow["rate_b"] = return3TextBox.Text;
            monthTaskRow["rate_a"] = return4TextBox.Text;
            monthTaskRow["task_amount"] = MonthTaskTextBox.Text;
            monthTaskRow["finished_amount"] = firstChargeTextBox.Text;
            monthTaskTable.LoadDataRow(monthTaskRow.ItemArray, false);
            //季任务表
            DataRow seasonTaskRow = seasonTaskTable.NewRow();
            seasonTaskRow["start_date"] = AddTimeDateTimePicker.Value;
            seasonTaskRow["rate_base"] = quarterReturn1TextBox.Text;
            seasonTaskRow["rate_inc"] = quarterReturn2TextBox.Text;
            seasonTaskRow["task_amount"] = quarterTaskTextBox.Text;
            seasonTaskRow["finished_amount"] = firstChargeTextBox.Text;
            seasonTaskTable.LoadDataRow(seasonTaskRow.ItemArray, false);
            //代理商现金流量表
            DataRow orgCashFlowRow = orgCashFlowTable.NewRow();
            orgCashFlowRow["amount"] = firstChargeTextBox.Text;
            orgCashFlowRow["finance_type"] = 1;
            orgCashFlowRow["trade_time"] = AddTimeDateTimePicker.Value;
            orgCashFlowRow["send_organize_id"] = OMWorkBench.AgentId;
            orgCashFlowRow["send_manager_id"] = OMWorkBench.MangerId;
            orgCashFlowTable.LoadDataRow(orgCashFlowRow.ItemArray, false);

            OMWorkBench.DataAgent.NewAgent(changeDS);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MonthTaskTextBox_TextChanged(object sender, EventArgs e)
        {
            quarterTaskTextBox.Text = Convert.ToString(decimal.Parse(MonthTaskTextBox.Text) * 3);
        }



    }
}
