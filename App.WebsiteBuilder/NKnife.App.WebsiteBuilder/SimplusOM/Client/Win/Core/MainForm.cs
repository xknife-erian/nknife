using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client
{
    public partial class MainForm : Form
    {
        public DockPanel _MainDockPanel
        {
            get { return MainDockPanel; }
        }

        public ToolStrip MainToolStrip
        {
            get { return mainToolStrip; }
        }
        public MainForm()
        {
            InitializeComponent();
            MainDockPanel.ActiveDocumentChanged += new EventHandler(MainDockPanel_ActiveDocumentChanged);
        }



        void MainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (MainDockPanel.ActiveDocument==null)
            return;
            string nameSpace="Jeelu.SimplusOM.Client";
            string typestr= MainDockPanel.ActiveDocument.GetType().ToString().Remove(0,nameSpace.Length+1);
            switch (typestr)
            {
                case "Form1":
                    {
                       
                        mainToolStrip.Items["EditTSButton"].Visible = true;
                        mainToolStrip.Items["FrozedTSButton"].Visible = true;
                        break;
                    }
                case "LoginInfoForm":
                    {
                        mainToolStrip.Items["NewTSButton"].Text = "新增管理者";
                        mainToolStrip.Items["EditTSButton"].Visible = false;
                        mainToolStrip.Items["FrozedTSButton"].Visible = false;
                        break;
                    }
                default:
                    break;
            }
        }

        private void MainForm4_Load(object sender, EventArgs e)
        {
            LeftTreeView leftTree = new LeftTreeView();
            leftTree.Show(MainDockPanel, DockState.DockLeft);

            OMWorkBench.CreateForm(new SelfManagerForm(OMWorkBench.MangerId));//  AgentInfoForm(OMWorkBench.AgentId));
        }

        #region 菜单
        /// <summary>
        /// 客户管理：代理商、用户、Jeelu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerManageToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        /// <summary>
        /// 其他（网盟，消息，新闻）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void otherToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        #endregion 

        #region 工具栏
        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "NewTSButton": ((IOMCommand)this.ActiveMdiChild).NewCmd(); break;
                case "EditTSButton": ((IOMCommand)this.ActiveMdiChild).EditCmd(); break;
                case "FrozedTSButton": ((IOMCommand)this.ActiveMdiChild).FrozedCmd(); break;
                case "DeleteTSButton": ((IOMCommand)this.ActiveMdiChild).DeleteCmd(); break;
                case "CancelTSButton": ((IOMCommand)this.ActiveMdiChild).CancelCmd(); break;
                case "SaveTSButton": ((IOMCommand)this.ActiveMdiChild).SaveCmd(); break;

                case "ChargeTSButton": ((IOMCommand)this.ActiveMdiChild).ChargeCmd(); break;
                case "ReturnSetTSButton": ((IOMCommand)this.ActiveMdiChild).ReturnSetCmd(); break;
                case "ReturnTSButton": ((IOMCommand)this.ActiveMdiChild).ReturnCmd(); break;

                case "CheckTSButton": ((IOMCommand)this.ActiveMdiChild).CheckCmd(); break;
                case "CloseTSButton": ((IOMCommand)this.ActiveMdiChild).CloseCmd(); break;
            }
        }
        #endregion

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "jeeluToolStripMenuItem":
                    {
                        OMWorkBench.CreateForm(new AgentInfoForm(OMWorkBench.AgentId));
                        break;
                    }
                case "managerToolStripMenuItem":
                    {
                        OMWorkBench.CreateForm(new ViewManagerForm(OMWorkBench.AgentId, OMWorkBench.AgentName));
                        break;
                    }
                case "agentToolStripMenuItem":
                    {
                        OMWorkBench.CreateForm(new ViewAgentForm(OMWorkBench.AgentId, OMWorkBench.MangerId, OMWorkBench.AgentName, false));
                        break;
                    }
                case "userToolStripMenuItem":
                    {
                        OMWorkBench.CreateForm(new ViewUserForm(false));
                        break;
                    }
                case "webUnionToolStripMenuItem":
                    {
                        OMWorkBench.CreateForm(new ViewWebUnionForm());
                        break;
                    }
                case "newsToolStripMenuItem":
                    {
                         // OMWorkBench.CreateForm(new ViewNewsForm());
                        break;
                    }
                case "msgToolStripMenuItem":
                    {
                        break;
                    }
            }
        }





    }
}
