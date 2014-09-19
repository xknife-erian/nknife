using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ChargeUserHistForm : Form
    {
        BindingSource hisBDS = new BindingSource();
        public ChargeUserHistForm(DataTable chargeUserHistoryTable)
        {
            InitializeComponent();

            chargeUserHistoryDGV.AutoGenerateColumns = false;
            hisBDS.DataSource = chargeUserHistoryTable;
            chargeUserHistoryDGV.DataSource = hisBDS;

            decimal chargeAountCount = 0;
            for (int i = 0; i < chargeUserHistoryTable.Rows.Count; i++)
            {
                chargeAountCount += Convert.ToDecimal(chargeUserHistoryTable.Rows[i]["amount"]);
            }
            chargeCountLabel.Text = chargeUserHistoryTable.Rows.Count.ToString();
            chargeAmountLabel.Text = chargeAountCount.ToString("#.##");

            chargeId.DataPropertyName = "id";
            chargeTime.DataPropertyName = "trade_time";
            chargeSend.DataPropertyName = "orgName";
            chargeReceive.DataPropertyName = "receiveOrgName";
            chargeChecked.DataPropertyName = "check_state";
            chargeAmount.DataPropertyName = "amount";
            chargeManager.DataPropertyName = "manangerName";
            chargeChecker.DataPropertyName = "checkerName";
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = chargeUserHistoryDGV.CurrentRow.Cells["chargeChecked"];
            if (cell.Value!=null && cell.Value.ToString() == "1")
            {
                MessageBox.Show("已审核，不需要再审核！");
                return;
            }
            int chargeId = Convert.ToInt32(chargeUserHistoryDGV.CurrentRow.Cells["chargeId"].Value);
            int managerId = OMWorkBench.MangerId;
            int effRows= OMWorkBench.DataAgent.CheckUserCharge(chargeId, managerId);
            if (effRows > 0)
            {
                chargeUserHistoryDGV.CurrentRow.Cells["chargeChecked"].Value = 1;
                chargeUserHistoryDGV.CurrentRow.ReadOnly = true;
            }
        }
    }
}
