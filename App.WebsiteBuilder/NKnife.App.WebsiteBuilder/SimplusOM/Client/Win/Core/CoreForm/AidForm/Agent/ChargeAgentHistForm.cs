using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ChargeAgentHistForm : Form
    {
        BindingSource hisBDS = new BindingSource();
        DataTable _chargeAgentHistoryTable = null;
        public Button CheckButton
        {
            get
            {
                return CheckBtn;
            }
        }
        public int DataGridSelectedIndex
        {
            set
            {
                hisBDS.Position = value;
            }
        }
        public ChargeAgentHistForm(DataTable chargeAgentHistoryTable)
        {
            InitializeComponent();
            chargeAgentHistoryDGV.AutoGenerateColumns = false;
            _chargeAgentHistoryTable = chargeAgentHistoryTable;
            hisBDS.DataSource = chargeAgentHistoryTable;
            chargeAgentHistoryDGV.DataSource = hisBDS;

            decimal chargeAountCount = 0;
            for (int i = 0; i < chargeAgentHistoryTable.Rows.Count; i++)
            {
                chargeAountCount += Convert.ToDecimal(chargeAgentHistoryTable.Rows[i]["amount"]);
            }
            chargeCountLabel.Text = chargeAgentHistoryTable.Rows.Count.ToString();
            chargeAmountLabel.Text = chargeAountCount.ToString("#.##");

            chargeId.DataPropertyName = "id";
            chargeTime.DataPropertyName = "trade_time";
            chargeSend.DataPropertyName = "sendOrgName";
            chargeReceive.DataPropertyName = "receiveOrgName";
            chargeChecked.DataPropertyName = "checked";
            chargeAmount.DataPropertyName = "amount";
            chargeManager.DataPropertyName = "chargerName";
            chargeChecker.DataPropertyName = "checkerName";
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            int chargeId = Convert.ToInt32(chargeAgentHistoryDGV.CurrentRow.Cells["chargeId"].Value);
            int managerId = OMWorkBench.MangerId;
            if (chargeAgentHistoryDGV.CurrentRow.Cells["chargeChecked"].Value.ToString() == "1")
            {
                MessageBox.Show("已经审核！！");
                return;
            }
            if (MessageBox.Show("您确定要审核此笔充值吗？", "审核确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                int w = OMWorkBench.DataAgent.CheckAgentCharge(chargeId, managerId);
                if (w > 0)
                {
                    chargeAgentHistoryDGV.CurrentRow.Cells["chargeChecked"].Value = 1;
                    chargeAgentHistoryDGV.CurrentRow.Cells["chargeChecker"].Value = OMWorkBench.MangerName; 
                }
            }
        }
    }
}
