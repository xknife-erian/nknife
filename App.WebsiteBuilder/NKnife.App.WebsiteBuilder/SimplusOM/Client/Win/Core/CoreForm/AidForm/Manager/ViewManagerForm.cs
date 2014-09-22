using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewManagerForm : OMBaseForm
    {
        bool canModifyOthers = false;
        bool canAddManager = false;
        DataTable _managerTable = null;
        string _agentName = "";
        int _agentId = -1;
        public ViewManagerForm(int agentId,string agentName)
        {
            InitializeComponent();

            _agentName = agentName;
            _agentId = agentId;
            managerDGV.AutoGenerateColumns = false;

            this.TabText += "__" + _agentName;
            canModifyOthers = OMWorkBench.ModifyManager;
            canAddManager = OMWorkBench.AddManager;

            mainToolStrip.Items["NewTSButton"].Visible = canAddManager;
            mainToolStrip.Items["EditTSButton"].Visible = canModifyOthers;
            mainToolStrip.Items["DeleteTSButton"].Visible = canModifyOthers;
        }

        private void ViewManagerForm_Load(object sender, EventArgs e)
        {
            _managerTable = OMWorkBench.DataAgent.GetManager(_agentId);
            
            if (!canModifyOthers)
            managerBDS.Filter = "id="+OMWorkBench.MangerId;
            managerBDS.DataSource = _managerTable;
            managerDGV.DataSource = managerBDS;
            id.DataPropertyName = "id";
            code.DataPropertyName = "code";
            name.DataPropertyName = "name";
            rights.DataPropertyName = "viewRights";
            log.DataPropertyName = "viewOpLog";
            remind.DataPropertyName = "remindCount";
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            managerBDS.Filter = "code LIKE '%"+IDTextBox.Text+"%' AND NAME LIKE '%"+NameTextBox.Text+"%'";
            if (!OMWorkBench.ModifyManager)
                managerBDS.Filter = "id="+OMWorkBench.MangerId;
        }

        private void managerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int managerID=Convert.ToInt32(managerDGV.CurrentRow.Cells["id"].Value);
            DataRow currentRow = _managerTable.Select("id=" + managerID)[0];
            switch (managerDGV.CurrentCell.OwningColumn.Name)
            {
                case "name":
                    {
                        SelfManagerForm managerForm = new SelfManagerForm(managerID);
                        OMWorkBench.CreateForm(managerForm);
                        break;
                    }
                case "log": break;
                case "rights":
                    {
                        if (canModifyOthers)
                        {
                            string rights = currentRow["rights"].ToString();
                            RightSetForm rightForm = new RightSetForm(rights);
                            if (rightForm.ShowDialog() == DialogResult.OK)
                            {
                                currentRow["rights"] = OMWorkBench.BitStrToStr(rightForm.BitArrayStr);
                                int I = OMWorkBench.DataAgent.UpdateManager(_managerTable.GetChanges());
                                _managerTable.AcceptChanges();

                            }
                        }
                        break;
                    }
                case "remind":
                    {
                        OMWorkBench.CreateForm(new ManagerMindForm(managerID));
                        break;
                    }
                default:break;
            }
        }

        #region CMD
        public override void NewCmd()
        {
            NewManagerForm newManager = new NewManagerForm(_agentId);
            if (newManager.ShowDialog() == DialogResult.OK)
            {
                _managerTable = OMWorkBench.DataAgent.GetManager(_agentId);
                managerBDS.DataSource = _managerTable;
            }
            base.NewCmd();
        }

        public override void EditCmd()
        {
            int managerID = Convert.ToInt32(managerDGV.CurrentRow.Cells["id"].Value);
            DataRow currentRow = _managerTable.Select("id=" + managerID)[0];
            EditManagerForm editManagerForm = new EditManagerForm(currentRow);



            if (editManagerForm.ShowDialog() == DialogResult.OK)
            {
                currentRow["name"] = editManagerForm.ManagerName;
                currentRow["password"] = editManagerForm.ManagerPassWord;

                int I = OMWorkBench.DataAgent.UpdateManager(_managerTable.GetChanges());
                _managerTable.AcceptChanges();
            }
        }

        public override void DeleteCmd()
        {
            int managerID = Convert.ToInt32(managerDGV.CurrentRow.Cells["id"].Value);
            DataRow currentRow = _managerTable.Select("id=" + managerID)[0];
            currentRow.Delete();
            int I = OMWorkBench.DataAgent.UpdateManager(_managerTable.GetChanges());
            _managerTable.AcceptChanges();
        }
        #endregion
    }
}
