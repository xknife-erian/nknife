using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class LeftTreeView : OMBaseForm
    {
        DataTable remindTable = null;
        BindingSource remindSource = new BindingSource();
        public LeftTreeView()
        {
            InitializeComponent();
            mainToolStrip.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            newBtn.Enabled = OMWorkBench.NewsManager;
            managerBtn.Enabled = OMWorkBench.ViewManager;
            agentBtn.Enabled = (OMWorkBench.AgentViewAll || OMWorkBench.AgentViewUnder || OMWorkBench.AgentViewUnderSub);
            userBtn.Enabled = (OMWorkBench.UserViewAll || OMWorkBench.UserViewUnderCorp || OMWorkBench.UserViewUnderManager || OMWorkBench.UserViewUnderManagerSub);


            remindTable = OMWorkBench.DataAgent.GetManagerRemind(OMWorkBench.MangerId);
            remindSource.DataSource = remindTable;
            if (remindSource.Count > 0)
                richTextBox1.Text = ((DataRowView)remindSource.Current).Row["description"].ToString();
            base.OnLoad(e);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "agentBtn":
                    {
                        OMWorkBench.CreateForm(new ViewAgentForm(OMWorkBench.AgentId,OMWorkBench.MangerId,OMWorkBench.AgentName,false));
                        break;
                    }
                case "userBtn":
                    {
                        OMWorkBench.CreateForm(new ViewUserForm(false));
                        break;
                    }
                case "jeeluBtn":
                    {
                        OMWorkBench.CreateForm(new AgentInfoForm(OMWorkBench.AgentId));
                        break;
                    }
                case "webUnionBtn":
                    {
                        OMWorkBench.CreateForm(new ViewWebUnionForm());
                        break;
                    }
                case "newBtn":
                    {
                        OMWorkBench.CreateForm(new ViewNewsForm());
                        break;
                    }
                case "managerBtn":
                    {
                        OMWorkBench.CreateForm(new ViewManagerForm(OMWorkBench.AgentId, OMWorkBench.AgentName));
                        break;
                    }
                case "selfDefinedRetrunBtn":
                    {
                        OMWorkBench.CreateForm(new ViewCustomTask());
                        break;
                    }
                default:
                    break;
            }

        }

        private void firstBtn_Click(object sender, EventArgs e)
        {
            remindSource.MoveFirst();
            richTextBox1.Text = ((DataRowView)remindSource.Current).Row["description"].ToString();
        }

        private void priorBtn_Click(object sender, EventArgs e)
        {
            remindSource.MovePrevious();
            richTextBox1.Text = ((DataRowView)remindSource.Current).Row["description"].ToString();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            remindSource.MoveNext();
            richTextBox1.Text = ((DataRowView)remindSource.Current).Row["description"].ToString();
        }

        private void lastBtn_Click(object sender, EventArgs e)
        {
            remindSource.MoveLast();
            richTextBox1.Text = ((DataRowView)remindSource.Current).Row["description"].ToString();
        }
    }
}
