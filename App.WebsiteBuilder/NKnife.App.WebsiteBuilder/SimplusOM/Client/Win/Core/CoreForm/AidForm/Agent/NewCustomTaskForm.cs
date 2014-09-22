using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class NewCustomTaskForm : Form
    {       
        DataSet _customDS = null;
        public NewCustomTaskForm(DataSet customDS)
        {
            InitializeComponent();

            _customDS = customDS;

            addDelAreaControl1.AreaTable = OMWorkBench.BaseInfoDS.Tables["area"];

            DataSet _childDS = OMWorkBench .DeserializeDataSet(OMWorkBench.DataAgent.GetChildAgents(OMWorkBench.AgentId, OMWorkBench.MangerId, 0));
            addDelAgentControl1.AreaTable = _childDS.Tables["organization"];
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DataTable _customDT =_customDS.Tables[0];
            DataRow dr = _customDT.NewRow();
            dr["task_name"] = nameTextBox.Text;
            dr["start_time"] = beginDTP.Value.ToShortDateString();
            dr["end_time"] = endDTP.Value.ToShortDateString();
            dr["description"] = descriptionTextBox.Text;
            dr["rate_base"] = returnRateTextBox.Text;
            dr["rate_inc"] = retrunIncTextBox.Text;
            dr["default_amount"] = defalutTaskTextBox.Text;
            dr["is_effect"]='n';
            _customDT.LoadDataRow(dr.ItemArray, false);

            foreach (int agentId in addDelAgentControl1.AreaPairs.Keys)
            {
                DataTable _agwentCustomDT = _customDS.Tables[1];
                DataRow orgdr = _agwentCustomDT.NewRow();
                orgdr["organization_id"] = agentId;
                orgdr["task_amount"] = defalutTaskTextBox.Text;
                _agwentCustomDT.LoadDataRow(orgdr.ItemArray, false);
            }
            int d = OMWorkBench.DataAgent.UpdateCustomTask(_customDS.GetChanges());
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
