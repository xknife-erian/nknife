using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class RemindForm : Form
    {
        DataSet agentDS = null;
        DataSet userDS = null;

        DataTable _addAgentTable = null;
        DataTable _addUserTable = null;

        BindingSource allAgentBDS = new BindingSource();
        BindingSource allUserBDS = new BindingSource();
        BindingSource addAgentBDS = new BindingSource();
        BindingSource addUserBDS = new BindingSource();

        Dictionary<int, string> agentPairs = new Dictionary<int, string>();
        Dictionary<int, string> userPairs = new Dictionary<int, string>();
   
        public string Context
        {
            get
            {
                string contentStr = "";
                foreach (string agentName in agentPairs.Values)
                {
                    contentStr += agentName.ToString() + ",";
                }
                foreach (string userName in userPairs.Values)
                {
                    contentStr += userName.ToString() + ",";
                }
                contentStr += contentTextBox.Text;
                return contentStr;
            }
            set
            {
                contentTextBox.Text=value;
            }
        }

        public RemindForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            agentDS = OMWorkBench .DeserializeDataSet(OMWorkBench.DataAgent.GetChildAgents(OMWorkBench.AgentId, OMWorkBench.MangerId, 0));
            userDS =OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 0);

            _addAgentTable = agentDS.Tables["organization"].Clone();
            _addUserTable = userDS.Tables["user"].Clone();

            allAgentBDS.DataSource = agentDS.Tables["organization"];
            allAgentListBox.DataSource = allAgentBDS;
            allAgentListBox.ValueMember = "id";
            allAgentListBox.DisplayMember = "name";

            allUserBDS.DataSource=userDS.Tables["user"];
            allUserListBox.DataSource = allUserBDS;
            allUserListBox.ValueMember = "id";
            allUserListBox.DisplayMember = "name";

            addAgentBDS.DataSource = _addAgentTable;
            addAgentListBox.DataSource = addAgentBDS;
            addAgentListBox.ValueMember = "id";
            addAgentListBox.DisplayMember = "name";

            addUserBDS.DataSource = userDS.Tables["user"];
            addUserListBox.DataSource = addUserBDS;
            addUserListBox.ValueMember = "id";
            addUserListBox.DisplayMember = "name";

            base.OnLoad(e);
        }

        private void addAgentBtn_Click(object sender, EventArgs e)
        {
            if (allAgentListBox.SelectedValue != null)
            {
                int agentId = Convert.ToInt32(allAgentListBox.SelectedValue);
                DataRow allRow = agentDS.Tables["organization"].Select("id=" + agentId)[0];
                if (!agentPairs.ContainsKey(agentId))
                {
                    _addAgentTable.LoadDataRow(allRow.ItemArray, false);
                    agentPairs.Add(Convert.ToInt32(allRow["id"]), allRow["name"].ToString());
                }
                if (allAgentListBox.SelectedIndex < allAgentListBox.Items.Count - 1)
                    allAgentListBox.SelectedIndex += 1;
                else
                    allAgentListBox.SelectedIndex = 0;
            }
        }

        private void delAgentBtn_Click(object sender, EventArgs e)
        {
            if (addAgentListBox.SelectedValue != null)
            {
                int agentId = Convert.ToInt32(addAgentListBox.SelectedValue);
                agentPairs.Remove(agentId);
                DataRow addRows =_addAgentTable.Select("id=" + agentId)[0];
                addRows.Delete();
            }
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            if (allUserListBox.SelectedValue !=null)
            { 
               int userId = Convert.ToInt32(allUserListBox.SelectedValue);
            
                DataRow allRow = agentDS.Tables["user"].Select("id=" + userId)[0];
                if (!agentPairs.ContainsKey(userId))
                {
                    _addUserTable.LoadDataRow(allRow.ItemArray, false);
                    userPairs.Add(Convert.ToInt32(allRow["id"]), allRow["name"].ToString());
                }
                if (allUserListBox.SelectedIndex < allUserListBox.Items.Count - 1)
                    allUserListBox.SelectedIndex += 1;
                else
                    allUserListBox.SelectedIndex = 0;
            }
        }

        private void delUserBtn_Click(object sender, EventArgs e)
        {
            if (addUserListBox.SelectedValue != null)
            {
                int agentId = Convert.ToInt32(addUserListBox.SelectedValue);
               userPairs.Remove(agentId);
                DataRow addRows = _addUserTable.Select("id=" + agentId)[0];
                addRows.Delete();
            }
        }


    }
}
