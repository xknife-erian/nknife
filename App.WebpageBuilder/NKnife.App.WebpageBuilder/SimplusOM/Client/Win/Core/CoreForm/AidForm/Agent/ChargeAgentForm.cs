using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ChargeAgentForm : Form
    {
        public decimal ChargeAmount
        {
            get
            {
                return decimal.Parse(chargeAmountTextBox.Text);
            }
            set
            {
                chargeAmountTextBox.Text = value.ToString();
            }
        }
        public string ChargeDescription
        {
            get
            {
                return desTextBox.Text;
            }
            set
            {
                desTextBox.Text = value;
            }
        }

        DataTable _chargeTable = null;
        public ChargeAgentForm(DataTable chargeTable,decimal balance)
        {
            InitializeComponent();

            _chargeTable = chargeTable;
            banlanceLabel.Text = laterBanlanceLabel.Text = balance.ToString("#.##");
            foreach (DataRow dr in chargeTable.Rows)
            {
                chargeHistoryListBox.Items.Add(dr["trade_time"].ToString());
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chargeAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal balance = decimal.Parse(banlanceLabel.Text);
                decimal chargeAmount = 0;
                if (!string.IsNullOrEmpty(chargeAmountTextBox.Text))
                {
                    chargeAmount = decimal.Parse(chargeAmountTextBox.Text);
                }

                decimal laterBalance = balance - chargeAmount;
                if (laterBalance > 0)
                    laterBanlanceLabel.Text = laterBalance.ToString("#.##");
                else
                {
                    laterBanlanceLabel.Text = banlanceLabel.Text;
                    chargeAmountTextBox.Text = "";
                }
            }
            catch { }
        }

        private void chargeHistoryListBox_DoubleClick(object sender, EventArgs e)
        {
            ChargeAgentHistForm historyForm = new ChargeAgentHistForm(_chargeTable);
            historyForm.DataGridSelectedIndex = chargeHistoryListBox.SelectedIndex;
            historyForm.ShowDialog(this);
        }
    }
}