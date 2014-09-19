using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewUserForm : OMBaseForm
    {
        DataSet _userDS = null;
        BindingSource _userBDS = new BindingSource();
        bool _isSelfUser = false;

        List<int> underCorp = null;
        List<int> underManager = null;
        List<int> underManagerSub = null;

        public ViewUserForm(bool isSelfUser)
        {
            InitializeComponent();
            userDGV.AutoGenerateColumns = false;
            _isSelfUser = isSelfUser;

            mainToolStrip.Items["NewTSButton"].Visible = (OMWorkBench.UserAddAll || OMWorkBench.UserAddAllLatent || OMWorkBench.UserAddJHAll
                || OMWorkBench.UserAddJHUnderCorp || OMWorkBench.UserAddJHUnderManager || OMWorkBench.UserAddJHUnderManagerSub || OMWorkBench.UserAddUnderCorp
                || OMWorkBench.UserAddUnderCorpLatent || OMWorkBench.UserAddUnderManager || OMWorkBench.UserAddUnderManagerLatent || OMWorkBench.UserAddUnderManagerSub
                || OMWorkBench.UserAddUnderManagerSubLatent);
            mainToolStrip.Items["FrozedTSButton"].Visible = (OMWorkBench.UserFrozedAll || OMWorkBench.UserFrozedUnderCorp || OMWorkBench.UserFrozedUnderManager || OMWorkBench.UserFrozedUnderManagerSub);
            mainToolStrip.Items["DeleteTSButton"].Visible = (OMWorkBench.UserDeleteAll || OMWorkBench.UserDeleteUnderCorp || OMWorkBench.UserDeleteUnderManager || OMWorkBench.UserDeleteUnderManagerSub);
            mainToolStrip.Items["chargeTSButton"].Visible = (OMWorkBench.UserChargeAll || OMWorkBench.UserChargeUnderCorp || OMWorkBench.UserChargeUnderManager || OMWorkBench.UserChargeUnderManagerSub);
            mainToolStrip.Items["cancelTSButton"].Visible = (OMWorkBench.UserTranStandardAll || OMWorkBench.UserTranStandardUnderCorp || OMWorkBench.UserTranStandardUnderManager || OMWorkBench.UserTranStandardUnderManagerSub);
            mainToolStrip.Items["cancelTSButton"].Text = "转正";
            mainToolStrip.Items["saveTSButton"].Visible = true;
            mainToolStrip.Items["saveTSButton"].Text = "提醒设置";

            mainToolStrip.Items["ReturnSetTSButton"].Visible = OMWorkBench.TranUser;
            mainToolStrip.Items["ReturnSetTSButton"].Text = "转移";

            mainToolStrip.Items["ReturnTSButton"].Visible = true;// OMWorkBench.TranUser;
            mainToolStrip.Items["ReturnTSButton"].Text = "充值审核";

            mainToolStrip.Items["CheckTSButton"].Visible = (OMWorkBench.UserCheckAdAll || OMWorkBench.UserCheckAdUnderCorp || OMWorkBench.UserCheckAdUnderManager || OMWorkBench.UserCheckAdUnderManagerSub);
            mainToolStrip.Items["CheckTSButton"].Text = "广告管理";//审核";
        }

        protected override void OnLoad(EventArgs e)
        {
           if (OMWorkBench.UserViewAll)
               _userDS = OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 0);
           else if (OMWorkBench.UserViewUnderCorp)
               _userDS = OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 1);
           else if (OMWorkBench.UserViewUnderManagerSub)
               _userDS = OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 2);
           else if (OMWorkBench.UserViewUnderManager)
               _userDS = OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 3);

            _userDS.WriteXml(@"c:\xx.xml");

            _userBDS.DataSource = _userDS.Tables["user"];
            userDGV.DataSource = _userBDS;

            id.DataPropertyName = "id";
            userId.DataPropertyName = "username";
            userName.DataPropertyName = "realname";
            userArea.DataPropertyName = "areaName";
            ownerOrg.DataPropertyName = "orgName";
            ownerManager.DataPropertyName = "managerName";
            communicate.DataPropertyName = "comCount";
            isReserve.DataPropertyName = "isReserve";

            _userBDS.Filter = "1=1 ";
            if (_isSelfUser)
                _userBDS.Filter += "and managerName='"+OMWorkBench.MangerName+"'";

            userDGV.DataSource = _userBDS;

            DataTable _areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            BindingSource areaBDS = new BindingSource();
            areaBDS.DataSource = _areaTable;
            areaBDS.Filter = "level=1";
            areaComboBox.DataSource = _areaTable;
            areaComboBox.DisplayMember = "name";
            areaComboBox.ValueMember = "code";

            userCountLinkLabel.Text = _userDS.Tables["user"].Rows.Count.ToString();
            userCount2LinkLabel.Text = _userDS.Tables["user"].Select("manageState=1").GetLength(0).ToString();
            userCount3LinkLabel.Text = _userDS.Tables["user"].Select("manageState=0").GetLength(0).ToString();



            underCorp = new List<int>();
            underManager = new List<int>();
            underManagerSub = new List<int>();
            foreach (DataRow dr in _userDS.Tables["user"].Rows)
            {
                if (Convert.ToInt32(dr["manager_id"]) == OMWorkBench.MangerId)
                {
                    underManager.Add(Convert.ToInt32(dr["id"]));
                }
            }

            foreach (DataRow dr in _userDS.Tables["user"].Rows)
            {
                int orgId=Convert.ToInt32(dr["organization_id"]);
                if (!underManager.Contains(orgId) && orgId == OMWorkBench.AgentId)
                {
                    underCorp.Add(Convert.ToInt32(dr["id"]));
                }
            }

            DataSet subAgentDS =OMWorkBench .DeserializeDataSet(OMWorkBench.DataAgent.GetChildAgents(OMWorkBench.AgentId, OMWorkBench.MangerId, 2));
            foreach (DataRow pdr in _userDS.Tables["user"].Rows)
            {
                int porgId=Convert.ToInt32(pdr["id"]);
                if (!(underManager.Contains(porgId) || underCorp.Contains(porgId)))
                {
                    foreach (DataRow dr in subAgentDS.Tables["organization"].Rows)
                    {
                        int orgId=Convert.ToInt32(dr["organization_id"]);
                        if (orgId == porgId)
                        {
                            underManagerSub.Add(Convert.ToInt32(pdr["id"]));
                        }
                    }
                }
            }
            underManagerSub.AddRange(underManager);

            base.OnLoad(e);
        }

        public override void NewCmd()
        {
            NewUserForm newUser = new NewUserForm();
            if (newUser.ShowDialog() == DialogResult.OK)
            {
                _userDS =OMWorkBench.DataAgent.GetChildUsers(OMWorkBench.AgentId, OMWorkBench.MangerId, 0);

                _userBDS.DataSource = _userDS.Tables["user"];
                userDGV.DataSource = _userBDS;
            }
            else if (newUser.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("Test");
            }
        }

        public override void FrozedCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            if (mainToolStrip.Items["FrozedTSButton"].Text == "解冻")
            {
                OMWorkBench.DataAgent.ChangeUserSatate(OMWorkBench.AgentId, Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value), "解冻",0);
                _userDS.Tables["user"].Select("id=" + userId)[0]["current_state"] = 0;
                MessageBox.Show("已解冻！");
                SetForFrozed(true);
            }
            else
            {
                DataTable reasonTable = OMWorkBench.DataAgent.GetStateChangeReason(userId, 1);
                FrozedAgentForm frozedAgent = new FrozedAgentForm(reasonTable);
                if (frozedAgent.ShowDialog() == DialogResult.OK)
                {
                    OMWorkBench.DataAgent.ChangeUserSatate(OMWorkBench.AgentId, Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value), frozedAgent.FrozedResonStr, 1);
                    _userDS.Tables["user"].Select("id=" + userId)[0]["current_state"] = 1;
                    SetForFrozed(false);
                }
            }
        }

        public override void DeleteCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            DataTable reasonTable = OMWorkBench.DataAgent.GetStateChangeReason(userId, 0);
            FrozedAgentForm frozedAgent = new FrozedAgentForm(reasonTable);
            frozedAgent.Text = "删除";
            if (frozedAgent.ShowDialog() == DialogResult.OK)
            {
                OMWorkBench.DataAgent.ChangeUserSatate(OMWorkBench.AgentId, Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value), frozedAgent.FrozedResonStr, 2);
                _userDS.Tables["user"].Select("id=" + userId)[0]["current_state"] = 2;
                _userBDS.Filter += " and current_state<2";
                userDGV.DataSource = _userBDS;
            }
            base.DeleteCmd();
        }

        /// <summary>
        ///转正
        /// </summary>
        public override void CancelCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            TranUserForm tranUser = new TranUserForm(userId);
            if (tranUser.ShowDialog() == DialogResult.OK)
            {
                userDGV.CurrentRow.Cells["isReserve"].Value = "正式";
                _userDS.Tables["user"].Select("id=" + userId)[0]["is_reserve"] = 'n';
                _userDS.AcceptChanges();
            }
        }

        /// <summary>
        /// 提醒设置
        /// </summary>
        public override void SaveCmd()
        {
            RemindForm remindForm = new RemindForm();
            int _managerId = OMWorkBench.MangerId;
            if (remindForm.ShowDialog() == DialogResult.OK)
            {
                DataTable remindTable = OMWorkBench.DataAgent.GetManagerRemind(_managerId);
                DataRow newRow = remindTable.NewRow();
                newRow["manager_id"] = _managerId;
                newRow["remind_time"] = DateTime.Now;

                newRow["description"] = remindForm.Context;
                remindTable.LoadDataRow(newRow.ItemArray, false);
                int i = OMWorkBench.DataAgent.UpdateManagerRemind(remindTable.GetChanges());
                remindTable = OMWorkBench.DataAgent.GetManagerRemind(_managerId);
            }
        }

        public override void ChargeCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            decimal orgBalance = OMWorkBench.Balance;
            DataTable chargeDT = OMWorkBench.DataAgent.GetUserCharge(userId);

            ChargeUserForm chargeUser = new ChargeUserForm(chargeDT, orgBalance);
            if (chargeUser.ShowDialog() == DialogResult.OK)
            {
                DataTable orgCashFlow = _userDS.Tables["organize_cash_flow"];

                DataRow newAgentCashFlow = orgCashFlow.NewRow();
                newAgentCashFlow["amount"] = chargeUser.chargeAmount;
                newAgentCashFlow["finance_type"] = 3;
                newAgentCashFlow["trade_time"] = DateTime.Today;
                newAgentCashFlow["send_organize_id"] = OMWorkBench.AgentId;
                newAgentCashFlow["receive_user_id"] = userId;
                newAgentCashFlow["send_manager_id"] = OMWorkBench.MangerId;
                orgCashFlow.LoadDataRow(newAgentCashFlow.ItemArray, false);

                OMWorkBench.DataAgent.ChargeUser(orgCashFlow);
                _userDS.AcceptChanges();
            }
        }
        /// <summary>
        /// 广告主转移
        /// </summary>
        public override void ReturnSetCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            UserToOtherForm tranForm = new UserToOtherForm(userId);
            if (tranForm.ShowDialog() == DialogResult.OK)
            {
                OnLoad(null);
            }
        }

        public override void ReturnCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            DataTable chargeUserHistory = OMWorkBench.DataAgent.GetUserCharge(userId);
            ChargeUserHistForm chargeUser = new ChargeUserHistForm(chargeUserHistory);
            chargeUser.ShowDialog(this);
        }

        /// <summary>
        /// 广告管理
        /// </summary>
        public override void CheckCmd()
        {
            int userId = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            string userName = userDGV.CurrentRow.Cells["userName"].Value.ToString();
            OMWorkBench.CreateForm(new ViewAdvSiteForm(userId,userName));
        }


        private void OKBtn_Click(object sender, EventArgs e)
        {
            string filter = "1=1  and current_state<2 ";
            if (!string.IsNullOrEmpty(IDTextBox.Text))
                filter += "and username like '%" + IDTextBox.Text + "%'";
            //if (areaComboBox.SelectedValue != null)
            //    filter += " and areaName like '%" +areaComboBox.Text+"%'";
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
            _userBDS.Filter = filter;
        }


        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLabel = sender as LinkLabel;
            switch (thisLabel.Name)
            {
                case "userCountLinkLabel":
                    {
                      _userBDS.Filter = "";
                        break;
                    }
                case "userCount2LinkLabel":
                    {
                        _userBDS.Filter = "manageState=0";
                        break;
                    }
                case "userCount3LinkLabel":
                    {
                        _userBDS.Filter = "manageState=1";
                        break;
                    }

            }
        }



        private void userDGV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (userDGV.CurrentRow != null)
                {
                    int userID = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
                    if (userID > 0)
                    {

                        SetForOperateRights(userID);
                        string state = _userDS.Tables["user"].Select("id=" + userID)[0]["current_state"].ToString();
                        SetForFrozed(state == "0" && mainToolStrip.Items["FrozedTSButton"].Visible);
                    }
                }
            }
            catch { }
        }


        void SetForOperateRights(int userID)
        {
            if (OMWorkBench.UserEditAll)
                mainToolStrip.Items["EditTSButton"].Visible = true;
            else if (OMWorkBench.UserEditUnderCorp)
                mainToolStrip.Items["EditTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserEditUnderManagerSub)
                mainToolStrip.Items["EditTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserEditUnderManager)
                mainToolStrip.Items["EditTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["EditTSButton"].Visible = false;

            if (OMWorkBench.UserTranStandardAll)
                mainToolStrip.Items["cancelTSButton"].Visible = true;
            else if (OMWorkBench.UserEditUnderCorp)
                mainToolStrip.Items["cancelTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserTranStandardUnderManagerSub)
                mainToolStrip.Items["cancelTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserTranStandardUnderManager)
                mainToolStrip.Items["cancelTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["cancelTSButton"].Visible = false;

            if (OMWorkBench.UserFrozedAll)
                mainToolStrip.Items["FrozedTSButton"].Visible = true;
            else if (OMWorkBench.UserFrozedUnderCorp)
                mainToolStrip.Items["FrozedTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserFrozedUnderManagerSub)
                mainToolStrip.Items["FrozedTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserFrozedUnderManager)
                mainToolStrip.Items["FrozedTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["FrozedTSButton"].Visible = false;

            if (OMWorkBench.UserDeleteAll)
                mainToolStrip.Items["DeleteTSButton"].Visible = true;
            else if (OMWorkBench.UserDeleteUnderCorp)
                mainToolStrip.Items["DeleteTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserDeleteUnderManagerSub)
                mainToolStrip.Items["DeleteTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserDeleteUnderManager)
                mainToolStrip.Items["DeleteTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["DeleteTSButton"].Visible = false;

            if (OMWorkBench.UserChargeAll)
                mainToolStrip.Items["ChargeTSButton"].Visible = true;
            else if (OMWorkBench.UserChargeUnderCorp)
                mainToolStrip.Items["ChargeTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserChargeUnderManagerSub)
                mainToolStrip.Items["ChargeTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserChargeUnderManager)
                mainToolStrip.Items["ChargeTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["ChargeTSButton"].Visible = false;

            if (OMWorkBench.UserCheckAdAll)
                mainToolStrip.Items["CheckTSButton"].Visible = true;
            else if (OMWorkBench.UserCheckAdUnderCorp)
                mainToolStrip.Items["CheckTSButton"].Visible = underCorp.Contains(userID);
            else if (OMWorkBench.UserCheckAdUnderManagerSub)
                mainToolStrip.Items["CheckTSButton"].Visible = underManagerSub.Contains(userID);
            else if (OMWorkBench.UserCheckAdUnderManager)
                mainToolStrip.Items["CheckTSButton"].Visible = underManager.Contains(userID);
            else mainToolStrip.Items["CheckTSButton"].Visible = false;
        }

        void SetForFrozed(bool froze)
        {
            mainToolStrip.Items["NewTSButton"].Enabled = froze;
            mainToolStrip.Items["DeleteTSButton"].Enabled = froze;
            mainToolStrip.Items["chargeTSButton"].Enabled = froze;
            mainToolStrip.Items["cancelTSButton"].Enabled = froze;
            mainToolStrip.Items["saveTSButton"].Enabled = froze;

            mainToolStrip.Items["FrozedTSButton"].Text = froze ? "冻结" : "解冻";
        }

        private void userDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (userDGV.CurrentRow == null)
                return;
            int userID = Convert.ToInt32(userDGV.CurrentRow.Cells["id"].Value);
            bool isreserve = (userDGV.CurrentRow.Cells["isReserve"].Value.ToString() == "潜在");
            int currentManagerId = Convert.ToInt32(_userDS.Tables["user"].Select("id=" + userID)[0]["manager_id"]);
            string colName = userDGV.CurrentCell.OwningColumn.Name;
            switch (colName)
            {
                case "userId":
                case "userName":
                    {
                        ViewSingleUserForm viewSingleUser = new ViewSingleUserForm(userID, isreserve);
                        viewSingleUser.ShowDialog(this);
                        break;
                    }
                case "communicate":
                    {
                        OMWorkBench.CreateForm(new CommunicateLoginForm(-1, userID, currentManagerId));
                        break;
                    }
                case "managerRemind":
                    {
                        OMWorkBench.CreateForm(new ManagerMindForm(currentManagerId));
                        break;
                    }
            }
        }


    }
}
