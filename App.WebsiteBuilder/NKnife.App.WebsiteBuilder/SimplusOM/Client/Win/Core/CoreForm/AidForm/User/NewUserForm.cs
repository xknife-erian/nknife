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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            areaControl1.areaSource = OMWorkBench.BaseInfoDS.Tables["area"];
            FirstChargeComboBox.DataSource = OMWorkBench.BaseInfoDS.Tables["product_template"];
            FirstChargeComboBox.DisplayMember = "user_price";
            FirstChargeComboBox.ValueMember = "organization_price";

            //areaTextBox.Text = OMControl.CurrentInfo.Tables["organization"].Rows[0]["areaName"].ToString();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DataSet userDS = OMWorkBench.BaseInfoDS.Clone();
            DataTable userTable = userDS.Tables["user"];
            DataTable userCashFlow = userDS.Tables["user_cash_flow"];
            DataTable orgCashFlow = userDS.Tables["organize_cash_flow"];

            bool isReserve = directRadioButton.Checked;
            int agentId = Convert.ToInt32(parentAgentComboBox.SelectedValue);
            DataRow newUserRow = userTable.NewRow();
            newUserRow["is_reserve"] = isReserve ? "n" : "y";
            newUserRow["username"] = IdTextBox.Text;
            newUserRow["realname"] = nameTextBox.Text;
            newUserRow["password"] = "21218cca77804d2ba1922c33e0151105";// psw1TextBox.Text;
            newUserRow["web"] = websiteTextBox.Text;
            newUserRow["nickname"] = LinkManTextBox.Text;
            newUserRow["phone"] = TelTextBox.Text;
            newUserRow["mobi"] = mobileTextBox.Text;
            newUserRow["qq"] = QQTextBox.Text;
            newUserRow["msn"] = MSNTextBox.Text;
            newUserRow["detail_addr"] = addressTextBox.Text;
            newUserRow["email"] = emailTextBox.Text;
            newUserRow["city_id"] = Convert.ToInt32(areaControl1.Area3ComboBox.SelectedValue);
            newUserRow["organization_id"] = agentId;
            newUserRow["manager_id"] = Convert.ToInt32(parentManagerComboBox.SelectedValue);
            if (isReserve)
            {
                newUserRow["balance"] = 0;// FirstChargeComboBox.Text;
                newUserRow["total_charged"] = 0;
                newUserRow["total_charge_count"] = 0;// FirstChargeComboBox.Text;
                newUserRow["is_experience"] = (FirstChargeComboBox.SelectedIndex != 0) ? "n" : "y";
            }

            userTable.LoadDataRow(newUserRow.ItemArray, false);
            if (isReserve)
            {
                DataRow newUserCashFlow = userCashFlow.NewRow();

                //newUserCashFlow["trade_amount"] = 0;// FirstChargeComboBox.Text;
                //newUserCashFlow["trade_time"] = DateTime.Today;
                //newUserCashFlow["send_organization_id"] = agentId;
                //userCashFlow.LoadDataRow(newUserCashFlow.ItemArray, false);

                DataRow newAgentCashFlow = orgCashFlow.NewRow();
                newAgentCashFlow["amount"] = FirstChargeComboBox.SelectedValue;
                newAgentCashFlow["finance_type"] = 3;
                newAgentCashFlow["trade_time"] = DateTime.Today;
                newAgentCashFlow["send_organize_id"] = agentId;
                newAgentCashFlow["send_manager_id"] = OMWorkBench.MangerId;
                orgCashFlow.LoadDataRow(newAgentCashFlow.ItemArray, false);
            }
            OMWorkBench.DataAgent.UpdateUser(userDS);
            //DataTable currentAgentTable = OMWorkBench.CurrentInfo.Tables["login"];
            //decimal oBalance = Convert.ToDecimal(currentAgentTable.Rows[0]["balance"]);
            //decimal charge = Convert.ToDecimal(FirstChargeComboBox.SelectedValue);
            //currentAgentTable.Rows[0]["balance"] = oBalance - charge;
            //currentAgentTable.AcceptChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void YESBtn_Click(object sender, EventArgs e)
        {
            DataAgent dataAgent = DataAgentFactory.GetDataAgent();
            DataTable dt = dataAgent.GetExistUser(userIdTextBox.Text);
            if (dt.Rows.Count > 0)
            {
                int A = dataAgent.AddExistUser(OMWorkBench.AgentId, OMWorkBench.MangerId, userIdTextBox.Text);
                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                userExistLabel.Text = "此用户不存在！";
                userExistLabel.ForeColor = Color.Red;
            }
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            DataAgent dataAgent = DataAgentFactory.GetDataAgent();
            DataTable dt = dataAgent.GetExistUser(userIdTextBox.Text);
            if (dt.Rows.Count > 0)
            {
                userNameTextBox.Text = dt.Rows[0]["nickname"].ToString();
                userExistLabel.Text = "可以添加此用户！";
                userExistLabel.ForeColor = Color.Green;
            }
            else
            {
                userExistLabel.Text = "此用户不存在！";
                userExistLabel.ForeColor = Color.Red;
            }

        }

        private void psw2TextBox_Validated(object sender, EventArgs e)
        {
            vlidatepsdLabel.Visible = (psw2TextBox.Text != psw1TextBox.Text);
        }


        private void parentAgentComboBox_DropDown(object sender, EventArgs e)
        {
            int grade = -1;
            int areaId = Convert.ToInt32(areaControl1.Area1ComboBox.SelectedValue);
            int areaId2 = Convert.ToInt32(areaControl1.Area2ComboBox.SelectedValue);

            DataTable agentTable = OMWorkBench.DataAgent.GetAgent(grade, areaId, areaId2);

            parentAgentComboBox.DataSource = agentTable;
            parentAgentComboBox.ValueMember = "id";
            parentAgentComboBox.DisplayMember = "name";
        }


        private void parentManagerComboBox_DropDown(object sender, EventArgs e)
        {
            int agentId = Convert.ToInt32(parentAgentComboBox.SelectedValue);
            DataTable manager = OMWorkBench.DataAgent.GetManager(agentId);

            parentManagerComboBox.DataSource = manager;
            parentManagerComboBox.ValueMember = "id";
            parentManagerComboBox.DisplayMember = "name";
        }

        private void directRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetForUserTypeChange(directRadioButton.Checked);
        }

        void SetForUserTypeChange(bool isReserve)
        {
            addTimeTimePicker.Enabled = isReserve;
            FirstChargeComboBox.Enabled = isReserve;
        }
    }
}
