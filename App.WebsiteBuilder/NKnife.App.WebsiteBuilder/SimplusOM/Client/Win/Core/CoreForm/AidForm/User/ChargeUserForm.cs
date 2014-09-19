using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ChargeUserForm : Form
    {
        //实际充值数量
        public decimal chargeAmount
        {
            get
            {
                return Convert.ToDecimal(chargeAmountComboBox.SelectedValue);
            }
        }
        //礼品包数量
        public decimal chargeProductAmount
        {
            get
            {
                return Convert.ToDecimal(chargeAmountComboBox.Text);
            }
        }
        //描述
        public string chargeDescription
        {
            get
            {
                return desTextBox.Text;
            }
        }

        DataTable _chargeTable = null;

        public ChargeUserForm(DataTable chargeTable, decimal balance)
        {
            InitializeComponent();

            _chargeTable = chargeTable;

            banlanceLabel.Text = balance.ToString("#.##");

            chargeAmountComboBox.DataSource = OMWorkBench.BaseInfoDS.Tables["product_template"];
            chargeAmountComboBox.DisplayMember = "user_price";
            chargeAmountComboBox.ValueMember = "organization_price";
        }


        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void chargeHistoryListBox_DoubleClick(object sender, EventArgs e)
        {
            ChargeAgentHistForm historyForm = new ChargeAgentHistForm(_chargeTable);
            historyForm.DataGridSelectedIndex = chargeHistoryListBox.SelectedIndex;
            historyForm.ShowDialog(this);
        }

        private void chargeAmountComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (chargeAmountComboBox.SelectedIndex > -1)
                {
                    decimal balance = decimal.Parse(banlanceLabel.Text);
                    decimal chargeAmount = decimal.Parse(chargeAmountComboBox.SelectedValue.ToString());

                    decimal laterBalance = balance - chargeAmount;
                    if (laterBalance > 0)
                        laterBanlanceLabel.Text = laterBalance.ToString("#.##");
                    else
                    {
                        MessageBox.Show("余额不足！");
                        chargeAmountComboBox.SelectedIndex = -1;
                    }
                }
            }
            catch
            { }
        }
    }
}
