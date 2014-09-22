using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ReturnRecordsForm : Form
    {
        public ReturnRecordsForm(DataTable returnTable)
        {
            InitializeComponent();

            returnRecordDGV.AutoGenerateColumns = false;
            returnRecordDGV.DataSource = returnTable;
            returnId.DataPropertyName = "id";
            returnDate.DataPropertyName = "trade_time";
            returnSend.DataPropertyName = "sendname";
            returnReceive.DataPropertyName = "receivename";
            returnAmount.DataPropertyName = "amount";

            int returnCount = 0;
            decimal returnSum = 0;
            for (int i = 0; i < returnTable.Rows.Count; i++)
            {
                DataRow dr = returnTable.Rows[i];
                returnCount++;
                returnSum += Convert.ToDecimal(dr["amount"]);
            }
            returnCountLabel.Text = returnCount.ToString();
            returnAmountLabel.Text = returnSum.ToString();
        }
    }
}
