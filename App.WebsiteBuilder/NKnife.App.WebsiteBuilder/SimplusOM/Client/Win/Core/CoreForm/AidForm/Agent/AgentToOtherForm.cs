using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class UserToOtherForm : Form
    {
        int _userId = -1;
        DataTable managerTable = null;
        DataTable agentTable = null;

        public UserToOtherForm(int userId)
        {
            InitializeComponent();

            _userId = userId;
            DataSet agentDS = OMWorkBench .DeserializeDataSet(OMWorkBench.DataAgent.GetChildAgents(OMWorkBench.AgentId, OMWorkBench.MangerId, 0));
            agentTable=agentDS.Tables[0];
            managerTable = OMWorkBench.DataAgent.GetManager(OMWorkBench.AgentId);

            toOtherComboBox.DataSource = managerTable;
            toOtherComboBox.ValueMember = "id";
            toOtherComboBox.DisplayMember = "name";
        }

        private void toManagerRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (toManagerRButton.Checked)
            {
                toOtherComboBox.DataSource = managerTable;
                toOtherComboBox.ValueMember = "id";
                toOtherComboBox.DisplayMember = "name";
            }
        }

        private void toAgentRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (toAgentRButton.Checked)
            {
                toOtherComboBox.DataSource = agentTable;
                toOtherComboBox.ValueMember = "id";
                toOtherComboBox.DisplayMember = "name";
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (toManagerRButton.Checked)
            {
                DataRow dr=managerTable.Select("id="+toOtherComboBox.SelectedValue)[0];
                int agentId=Convert.ToInt32(dr["organization_id"]);
                int managerId=Convert.ToInt32(toOtherComboBox.SelectedValue);
                int s = OMWorkBench.DataAgent.TranUser(_userId, agentId, managerId);
            }
            else
            {
                int agentId = Convert.ToInt32(toOtherComboBox.SelectedValue);
                int s = OMWorkBench.DataAgent.TranUser(_userId, agentId, -1);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
