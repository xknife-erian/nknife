using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusOM.Client.Win;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewAgentForm : OMBaseForm
    {
        int currentAgentId = -1;//表格中当前行代理商Id
        int _parentAgentId = -1;//父代理商Id
        string _parentAgentName = "";//父代理商名称
        int _parentMangerId = -1;//父代理商管理员

        DataSet _childDS = null;
        DataTable _areaTable = null;
        bool _isSelfAgent = false;

        List<int> underAgent = new List<int>();
        List<int> underSubAgent = new List<int>();

        public ViewAgentForm(int agentId, int mangerId, string agentName, bool isSelfAgent)
        {
            InitializeComponent();

            _isSelfAgent = isSelfAgent;
            AgentDGV.AutoGenerateColumns = false;
            _parentAgentId = agentId;
            _parentMangerId = mangerId;
            _parentAgentName = agentName;

            this.TabText += "__" + _parentAgentName;
            mainToolStrip.Items["NewTSButton"].Visible = (OMWorkBench.AgentAddAll || OMWorkBench.AgentAddUnder || OMWorkBench.AgentAddUnderSub || OMWorkBench.AgentAddAllLatent || OMWorkBench.AgentAddUnderLatent || OMWorkBench.AgentAddUnderSubLatent);
            mainToolStrip.Items["EditTSButton"].Visible = (OMWorkBench.AgentEditUnder || OMWorkBench.AgentEidtAll || OMWorkBench.AgentEditUnderSub);
            mainToolStrip.Items["FrozedTSButton"].Visible = (OMWorkBench.AgentFrozedAll || OMWorkBench.AgentFrozedUnder || OMWorkBench.AgentFrozedUnderSub);
            mainToolStrip.Items["DeleteTSButton"].Visible = (OMWorkBench.AgentDeleteAll || OMWorkBench.AgentDeleteUnder || OMWorkBench.AgentDeleteUnderSub);
            mainToolStrip.Items["ChargeTSButton"].Visible = (OMWorkBench.AgentChargeAll || OMWorkBench.AgentChargeUnder || OMWorkBench.AgentChargeUnderSub);
            mainToolStrip.Items["CancelTSButton"].Visible = (OMWorkBench.AgentTranStandardAll || OMWorkBench.AgentTranStandardUnder || OMWorkBench.AgentTranStandardUnderSub);
            mainToolStrip.Items["CancelTSButton"].Text = "转正";
            mainToolStrip.Items["SaveTSButton"].Visible = OMWorkBench.TranAgent;
            mainToolStrip.Items["SaveTSButton"].Text = "转移";
            mainToolStrip.Items["ReturnSetTSButton"].Visible = (OMWorkBench.AgentReturnSetAll || OMWorkBench.AgentReturnSetUnder || OMWorkBench.AgentReturnSetUnderSub);
            mainToolStrip.Items["ReturnTSButton"].Visible = OMWorkBench.ReturnSetCheck;
            mainToolStrip.Items["ReturnTSButton"].Text = "返点设置审核";
            mainToolStrip.Items["CheckTSButton"].Visible = OMWorkBench.ChargeCheck;
            mainToolStrip.Items["CheckTSButton"].Text = "充值审核";
        }

        protected override void OnLoad(EventArgs e)
        {
            byte[] agentByte = null;
            if (OMWorkBench.AgentViewAll)
                agentByte = OMWorkBench.DataAgent.GetChildAgents(_parentAgentId, _parentMangerId, 0);
            else if (OMWorkBench.AgentViewUnderSub)
                agentByte = OMWorkBench.DataAgent.GetChildAgents(_parentAgentId, _parentMangerId, 1);
            else if (OMWorkBench.AgentViewUnder)
                agentByte = OMWorkBench.DataAgent.GetChildAgents(_parentAgentId, _parentMangerId, 2);
            _childDS = OMWorkBench.DeserializeDataSet(agentByte);

            agentBDS.DataSource = _childDS.Tables["organization"];
            agentBDS.Filter = "1=1 ";
            if (_isSelfAgent)
                agentBDS.Filter += "and  managerName='" + OMWorkBench.MangerName + "'";
            AgentDGV.DataSource = agentBDS;

            id.DataPropertyName = "id";
            code.DataPropertyName = "code";
            agentName.DataPropertyName = "name";
            manager.DataPropertyName = "managerName";
            area.DataPropertyName = "areaName";
            isReserve.DataPropertyName = "isReserve";
            communicate.DataPropertyName = "comCount";

            _areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            BindingSource areaBDS = new BindingSource();
            areaBDS.DataSource = _areaTable;
            areaBDS.Filter = "level=1";
            areaComboBox.DataSource = _areaTable;
            areaComboBox.DisplayMember = "name";
            areaComboBox.ValueMember = "code";


            agentCountLinkLabel.Text = _childDS.Tables["organization"].Rows.Count.ToString();
            agentCount2LinkLabel.Text = _childDS.Tables["organization"].Select("grade=1").GetLength(0).ToString();
            agentCount3LinkLabel.Text = _childDS.Tables["organization"].Select("grade=2").GetLength(0).ToString();

            foreach (DataRow dr in _childDS.Tables["organization"].Rows)
            {
                if (Convert.ToInt32(dr["manager_id"]) == OMWorkBench.MangerId)
                {
                    underAgent.Add(Convert.ToInt32(dr["id"]));
                }
            }

            foreach (DataRow dr in _childDS.Tables["organization"].Rows)
            {
                foreach (int agentId in underAgent)
                {
                    if (Convert.ToInt32(dr["parent_id"]) == agentId)
                    {
                        underSubAgent.Add(Convert.ToInt32(dr["id"]));
                    }
                }
            }
            underSubAgent.AddRange(underAgent);

            base.OnLoad(e);
        }

        public override void NewCmd()
        {
            NewAgentForm newAgent = new NewAgentForm(AgentType.Standard);
            if (newAgent.ShowDialog() == DialogResult.OK)
            {
                _childDS = OMWorkBench.DeserializeDataSet(OMWorkBench.DataAgent.GetChildAgents(_parentAgentId, _parentMangerId, 0));
                agentBDS.DataSource = _childDS.Tables["organization"];
                AgentDGV.DataSource = agentBDS;
                int agentCount = Convert.ToInt32(agentCountLinkLabel.Text);
                agentCount++;
                agentCountLinkLabel.Text = agentCount.ToString();

                int agentZCount = Convert.ToInt32(agentCount2LinkLabel.Text);
            }
            base.NewCmd();
        }

        public override void EditCmd()
        {
            DataRow agentRow = _childDS.Tables["organization"].Select("id=" + currentAgentId)[0];
            if (agentRow["current_state"].ToString() != "0")
            {
                MessageBox.Show("非正常状态不可以修改！");
                return;
            }
            EditAgentForm editAgent = new EditAgentForm(agentRow);
            if (editAgent.ShowDialog() == DialogResult.OK)
            {
                DataSet changeDS = _childDS.GetChanges();
                OMWorkBench.DataAgent.UpdateAgent(changeDS);
                _childDS.AcceptChanges();
            }
            base.EditCmd();
        }

        public override void FrozedCmd()
        {
            if (mainToolStrip.Items["FrozedTSButton"].Text == "解冻")
            {
                OMWorkBench.DataAgent.ChangeAgentSatate(OMWorkBench.AgentId, currentAgentId, "解冻", 0);
                _childDS.Tables["organization"].Select("id=" + currentAgentId)[0]["current_state"] = 0;
                MessageBox.Show("已解冻！");
                SetForFrozed(true);
            }
            else
            {
                DataTable reasonTable = OMWorkBench.DataAgent.GetStateChangeReason(currentAgentId, 0);
                FrozedAgentForm frozedAgent = new FrozedAgentForm(reasonTable);
                if (frozedAgent.ShowDialog() == DialogResult.OK)
                {
                    DataAgent dataAgent = DataAgentFactory.GetDataAgent();
                    dataAgent.ChangeAgentSatate(OMWorkBench.AgentId, Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value), frozedAgent.FrozedResonStr, 1);
                    _childDS.Tables["organization"].Select("id=" + currentAgentId)[0]["current_state"] = 1;
                    SetForFrozed(false);
                }
            }
        }

        public override void DeleteCmd()
        {
            DataTable reasonTable = OMWorkBench.DataAgent.GetStateChangeReason(currentAgentId, 0);
            FrozedAgentForm frozedAgent = new FrozedAgentForm(reasonTable);
            frozedAgent.Text = "删除";
            if (frozedAgent.ShowDialog() == DialogResult.OK)
            {
                DataAgent dataAgent = DataAgentFactory.GetDataAgent();
                dataAgent.ChangeAgentSatate(OMWorkBench.AgentId, Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value), frozedAgent.FrozedResonStr, 2);
                _childDS.Tables["organization"].Select("id=" + currentAgentId)[0]["current_state"] = 2;
                agentBDS.Filter += " and current_state<2";
                AgentDGV.DataSource = agentBDS;
            }
            base.DeleteCmd();
        }

        public override void ChargeCmd()
        {
            int agentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            DataRow orgRow = _childDS.Tables["organization"].Select("id=" + agentId)[0];
            decimal orgBalance = Convert.ToDecimal(orgRow["balance"]);
            DataTable chargeDT = OMWorkBench.DataAgent.GetAgentChargeRecords(agentId);
            ChargeAgentForm chargeAgent = new ChargeAgentForm(chargeDT, OMWorkBench.Balance);// orgBalance);

            if (chargeAgent.ShowDialog() == DialogResult.OK)
            {
                DataRow chargeRow = chargeDT.NewRow();
                chargeRow["send_organize_id"] = OMWorkBench.AgentId;
                chargeRow["receive_organize_id"] = agentId;
                chargeRow["amount"] = chargeAgent.ChargeAmount;
                chargeRow["description"] = chargeAgent.ChargeDescription;
                chargeRow["send_manager_id"] = OMWorkBench.MangerId;
                chargeRow["trade_time"] = DateTime.Today;
                chargeRow["finance_type"] = 1;
                chargeDT.LoadDataRow(chargeRow.ItemArray, false);
                OMWorkBench.DataAgent.ChargeAgent(chargeDT.GetChanges());
                chargeDT.AcceptChanges();
            }
        }
        /// <summary>
        /// 潜在代理商转换为正式代理商
        /// </summary>
        public override void CancelCmd()
        {
            int agentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            DataRow currentRow = _childDS.Tables["organization"].Select("id=" + agentId)[0];
            TranAgentForm newAgent = new TranAgentForm(agentId);
            if (newAgent.ShowDialog() == DialogResult.OK)
            {
                currentRow["is_reserve"] = 'n';
                currentRow["isReserve"] = 0;
                AgentDGV.CurrentRow.Cells["isReserve"].Value = 0;
                _childDS.AcceptChanges();
            }
        }

        public override void SaveCmd()
        {
            int agentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            AgentToOtherForm tranForm = new AgentToOtherForm(agentId);
            if (tranForm.ShowDialog() == DialogResult.OK)
            {
                OnLoad(null);
            }
            base.SaveCmd();
        }
        /// <summary>
        /// 返点审核
        /// </summary>
        public override void ReturnSetCmd()
        {
            int agentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            ReturnSetForm returnSetForm = new ReturnSetForm(agentId);
            if (returnSetForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        /// <summary>
        /// 充值审核
        /// </summary>
        public override void CheckCmd()
        {
            int currentAgentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            DataTable dt = OMWorkBench.DataAgent.GetAgentChargeRecords(currentAgentId);
            ChargeAgentHistForm hisForm = new ChargeAgentHistForm(dt);
            hisForm.CheckButton.Visible = true;
            hisForm.ShowDialog(this);
        }
        /// <summary>
        /// 返点设置审核
        /// </summary>
        public override void ReturnCmd()
        {
            int currentAgentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            ReturnSetCheckForm checkForm = new ReturnSetCheckForm(currentAgentId);
            checkForm.ShowDialog(this);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string filter = "1=1 and current_state<2 ";
            if (!string.IsNullOrEmpty(CodeTextBox.Text))
                filter += "and code like '%" + CodeTextBox.Text + "%'";
            if (areaComboBox.SelectedValue != null)
                filter += " and areaCode=" + Convert.ToInt32(areaComboBox.SelectedValue);
            if (!string.IsNullOrEmpty(managerTextBox.Text))
                filter += " and managerName like '%" + managerTextBox.Text + "%'";
            switch (typeCombobox.SelectedIndex)
            {
                case 0:
                    filter += " and is_reserve='y'";
                    break;
                case 1:
                    filter += " and is_reserve='n'";
                    break;
            }
            agentBDS.Filter = filter;
            agentBDS.ResetBindings(false);
        }

        private void AgentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AgentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (AgentDGV.CurrentRow == null)
                return;
            currentAgentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
            int currentManagerId = Convert.ToInt32(_childDS.Tables[0].Select("id=" + currentAgentId)[0]["manager_id"]);
            string colName = AgentDGV.CurrentCell.OwningColumn.Name;
            switch (colName)
            {
                case "code":
                case "agentName":
                    {
                        OMWorkBench.CreateForm(new ChildAgentInfoForm(currentAgentId));
                        break;
                    }
                case "communicate":
                    {
                        OMWorkBench.CreateForm(new CommunicateLoginForm(currentAgentId, -1, currentManagerId));
                        break;
                    }
                case "managerMind":
                    {
                        OMWorkBench.CreateForm(new ManagerMindForm(currentManagerId));
                        break;
                    }
            }
        }
        private void AgentDGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (AgentDGV.CurrentRow != null)
                {
                    currentAgentId = Convert.ToInt32(AgentDGV.CurrentRow.Cells["id"].Value);
                    DataRow agentDr = _childDS.Tables["organization"].Select("id=" + currentAgentId)[0];
                    if (currentAgentId > 0)
                    {
                        mainToolStrip.Items["ReturnTSButton"].Enabled =
                        mainToolStrip.Items["ChargeTSButton"].Enabled =
                        mainToolStrip.Items["CheckTSButton"].Enabled =
                        mainToolStrip.Items["ReturnSetTSButton"].Enabled =
                        (agentDr["is_reserve"].ToString() == "n");
                    }
                    else
                    {
                        string state = _childDS.Tables["organization"].Select("id=" + currentAgentId)[0]["current_state"].ToString();
                        SetForFrozed(state == "0" && mainToolStrip.Items["FrozedTSButton"].Enabled);
                    }
                }
            }
            catch { }

        }

        void SetOperateRights(int currentAgentId)
        {
            if (OMWorkBench.AgentEidtAll)
                mainToolStrip.Items["EditTSButton"].Visible = true;
            else if (OMWorkBench.AgentEditUnderSub)
                mainToolStrip.Items["EditTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentEditUnder)
                mainToolStrip.Items["EditTSButton"].Visible = (underAgent.Contains(currentAgentId));
            else mainToolStrip.Items["EditTSButton"].Visible = false;

            if (OMWorkBench.AgentTranStandardAll)
                mainToolStrip.Items["CancelTSButton"].Visible = true;
            else if (OMWorkBench.AgentTranStandardUnder)
                mainToolStrip.Items["CancelTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentTranStandardUnderSub)
                mainToolStrip.Items["CancelTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else mainToolStrip.Items["CancelTSButton"].Visible = false;

            if (OMWorkBench.AgentFrozedAll)
                mainToolStrip.Items["FrozedTSButton"].Visible = true;
            else if (OMWorkBench.AgentFrozedUnderSub)
                mainToolStrip.Items["FrozedTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentFrozedUnder)
                mainToolStrip.Items["FrozedTSButton"].Visible = (underAgent.Contains(currentAgentId));
            else mainToolStrip.Items["FrozedTSButton"].Visible = false;

            if (OMWorkBench.AgentDeleteAll)
                mainToolStrip.Items["DeleteTSButton"].Visible = true;
            else if (OMWorkBench.AgentDeleteUnderSub)
                mainToolStrip.Items["DeleteTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentDeleteUnder)
                mainToolStrip.Items["DeleteTSButton"].Visible = (underAgent.Contains(currentAgentId));
            else mainToolStrip.Items["DeleteTSButton"].Visible = false;

            if (OMWorkBench.AgentChargeAll)
                mainToolStrip.Items["ChargeTSButton"].Visible = true;
            else if (OMWorkBench.AgentChargeUnderSub)
                mainToolStrip.Items["ChargeTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentChargeUnder)
                mainToolStrip.Items["ChargeTSButton"].Visible = (underAgent.Contains(currentAgentId));
            else mainToolStrip.Items["ChargeTSButton"].Visible = false;

            if (OMWorkBench.AgentReturnSetAll)
                mainToolStrip.Items["ReturnSetTSButton"].Visible = true;
            else if (OMWorkBench.AgentReturnSetUnderSub)
                mainToolStrip.Items["ReturnSetTSButton"].Visible = (underSubAgent.Contains(currentAgentId));
            else if (OMWorkBench.AgentReturnSetUnder)
                mainToolStrip.Items["ReturnSetTSButton"].Visible = (underAgent.Contains(currentAgentId));
            else mainToolStrip.Items["ReturnSetTSButton"].Visible = false;

        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLabel = sender as LinkLabel;
            switch (thisLabel.Name)
            {
                case "agentCountLinkLabel":
                    {
                        agentBDS.Filter = "1=1 ";
                        break;
                    }
                case "agentCount2LinkLabel":
                    {
                        agentBDS.Filter = "and grade=1";
                        break;
                    }
                case "agentCount3LinkLabel":
                    {
                        agentBDS.Filter = "and grade=2";
                        break;
                    }
            }
        }

        void SetForFrozed(bool froze)
        {
            mainToolStrip.Items["NewTSButton"].Enabled = froze;
            mainToolStrip.Items["EditTSButton"].Enabled = froze;
            mainToolStrip.Items["DeleteTSButton"].Enabled = froze;
            mainToolStrip.Items["ChargeTSButton"].Enabled = froze;
            mainToolStrip.Items["CancelTSButton"].Enabled = froze;
            mainToolStrip.Items["ReturnSetTSButton"].Enabled = froze;
            mainToolStrip.Items["ReturnTSButton"].Enabled = froze;


            mainToolStrip.Items["FrozedTSButton"].Text = froze ? "冻结" : "解冻";
        }


    }
}