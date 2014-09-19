using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class CommunicateForm : Form
    {
        public string Context
        {
            get
            {
                return contentTextBox.Text;
            }
            set
            {
                contentTextBox.Text=value;
            }
        }

        public CommunicateForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            //DataSet agentTable = OMWorkBench.DataAgent.GetChildAgent(OMWorkBench.AgentId, OMWorkBench.MangerId, 0);
            //DataSet userTable = OMWorkBench.DataAgent.GetUser(OMWorkBench.AgentId, OMWorkBench.MangerId, 0);
            base.OnLoad(e);
        }

        private void addAgentBtn_Click(object sender, EventArgs e)
        {
             
        }

    }
}
