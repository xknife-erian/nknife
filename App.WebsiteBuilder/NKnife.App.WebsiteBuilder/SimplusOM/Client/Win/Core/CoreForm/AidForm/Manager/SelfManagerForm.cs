using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class SelfManagerForm : OMBaseForm
    {
        int _managerId = -1;
        public SelfManagerForm(int managerId)
        {
            InitializeComponent();

            _managerId = managerId;
        }

        protected override void OnLoad(EventArgs e)
        {
                DataSet managerDS = OMWorkBench.DataAgent.GetSingleManager(_managerId);
                //if (_managerId!=OMWorkBench.MangerId)
                this.TabText = "员工信息__" + managerDS.Tables["manager"].Rows[0]["name"].ToString();

                DataRow managerDR = managerDS.Tables["manager"].Rows[0];
                DataRow managerStaDR = managerDS.Tables["managerSta"].Rows[0];
                IDLab.Text = managerDR["code"].ToString();
                NameLab.Text = managerDR["name"].ToString();
                TelLab.Text = managerDR["phone"].ToString();
                ParentAgentLab.Text = managerDR["orgName"].ToString();

                SubAgentNumLab.Text = managerStaDR["orgCout"].ToString();
                SubUserNumLab.Text = managerStaDR["userCout"].ToString();
                ChargeCountLab.Text = managerStaDR["chargeCount"].ToString();
                ChargeSumLab.Text = Convert.ToDecimal(managerStaDR["chargeSum"] == DBNull.Value ? 0 : managerStaDR["chargeSum"]).ToString("#.##");

                ReturnCountLab.Text = managerStaDR["returnCount"].ToString();
                ReturnSumLab.Text = Convert.ToDecimal(managerStaDR["returnSum"] == DBNull.Value ? 0 : managerStaDR["returnSum"]).ToString("#.##");
            //}
            //catch
            //{ }
            base.OnLoad(e);
        }

        private void Lab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;

            switch (linkLabel.Name)
            {
                case "SubAgentNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewAgentForm(OMWorkBench.AgentId, OMWorkBench.MangerId, OMWorkBench.AgentName,true));
                        break;
                    }
                case "SubUserNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewUserForm(true));//OMWorkBench.AgentId, OMWorkBench.MangerId, OMWorkBench.AgentName));
                        
                        break;
                    }
                case "ChargeCountLab": break;
                case "ChargeSumLab": break;

            }
        }


    }
}
