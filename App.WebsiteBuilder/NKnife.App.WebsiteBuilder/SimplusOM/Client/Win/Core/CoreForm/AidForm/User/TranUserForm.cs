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
    public partial class TranUserForm : Form
    {

        DataTable _userTable = null;
        int _userId = -1;

        public TranUserForm(int userId)
        {
            InitializeComponent();

            _userId = userId;
        }

        protected override void OnLoad(EventArgs e)
        {
            _userTable = OMWorkBench.DataAgent.GetSingleUser(_userId,true);

            IdTextBox.Text = _userTable.Rows[0]["username"].ToString();
            nameTextBox.Text = _userTable.Rows[0]["realname"].ToString();

            FirstChargeComboBox.DataSource = OMWorkBench.BaseInfoDS.Tables["product_template"];
            FirstChargeComboBox.DisplayMember = "user_price";
            FirstChargeComboBox.ValueMember = "organization_price";
            base.OnLoad(e);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DataSet userDS = OMWorkBench.BaseInfoDS.Clone();
            userDS.Tables["user"].Merge(this._userTable.Copy());
            DataTable userTable = userDS.Tables["user"];
            DataTable userCashFlow = userDS.Tables["user_cash_flow"];
            DataTable orgCashFlow = userDS.Tables["organize_cash_flow"];

            // int agentId = Convert.ToInt32(parentAgentComboBox.SelectedValue);
            DataRow userRow = userTable.Rows[0];
            userRow["is_reserve"] = "n";
            userRow["balance"] = FirstChargeComboBox.Text;
            userRow["total_charged"] = 1;
            userRow["total_charge_count"] = FirstChargeComboBox.Text;
            userRow["is_experience"] = (FirstChargeComboBox.SelectedIndex != 0) ? "n" : "y";

            DataRow newUserCashFlow = userCashFlow.NewRow();

            newUserCashFlow["trade_amount"] = FirstChargeComboBox.Text;
            newUserCashFlow["trade_time"] = DateTime.Today;
            //newUserCashFlow["send_organization_id"] = agentId;
            userCashFlow.LoadDataRow(newUserCashFlow.ItemArray, false);

            DataRow newAgentCashFlow = orgCashFlow.NewRow();
            newAgentCashFlow["amount"] = FirstChargeComboBox.SelectedValue;
            newAgentCashFlow["finance_type"] = 3;
            newAgentCashFlow["trade_time"] = DateTime.Today;
            // newAgentCashFlow["send_organize_id"] = agentId;
            newAgentCashFlow["send_manager_id"] = OMWorkBench.MangerId;
            orgCashFlow.LoadDataRow(newAgentCashFlow.ItemArray, false);

            OMWorkBench.DataAgent.UpdateUser(userDS);
            DataTable currentAgentTable = OMWorkBench.CurrentInfo.Tables["login"];
            decimal oBalance = Convert.ToDecimal(currentAgentTable.Rows[0]["balance"]);
            decimal charge = Convert.ToDecimal(FirstChargeComboBox.SelectedValue);
            currentAgentTable.Rows[0]["balance"] = oBalance - charge;
            currentAgentTable.AcceptChanges();
            this.DialogResult = DialogResult.OK;
        }

    }
}
