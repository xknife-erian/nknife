using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ReturnSetForm : Form
    {
        int _agentId = -1;
        DataSet returnDS = null;
        DataRow monthReturnRow = null;
        DataRow seasonReturnRow = null;

        public ReturnSetForm(int agentId)
        {
            InitializeComponent();

            _agentId = agentId;
        }

        protected override void OnLoad(EventArgs e)
        {
            returnDS = OMWorkBench.DataAgent.GetReturnData(_agentId);

            if (returnDS.Tables["monthReturn"].Rows.Count > 0)
            {
                monthReturnRow = returnDS.Tables["monthReturn"].Rows[0];

                validateTextBox1.Text = monthReturnRow["rate_d"].ToString();
                validateTextBox2.Text = monthReturnRow["rate_c"].ToString();
                validateTextBox3.Text = monthReturnRow["rate_b"].ToString();
                validateTextBox4.Text = monthReturnRow["rate_a"].ToString();
            }
            if (returnDS.Tables["seasonReturn"].Rows.Count > 0)
            {
                seasonReturnRow = returnDS.Tables["seasonReturn"].Rows[0];
                validateTextBox5.Text = seasonReturnRow["rate_base"].ToString();
                validateTextBox6.Text = seasonReturnRow["rate_inc"].ToString();
            }

            base.OnLoad(e);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            monthReturnRow["rate_d"] = Convert.ToDecimal(validateTextBox1.Text);
            monthReturnRow["rate_c"] = Convert.ToDecimal(validateTextBox2.Text);
            monthReturnRow["rate_b"] = Convert.ToDecimal(validateTextBox3.Text);
            monthReturnRow["rate_a"] = Convert.ToDecimal(validateTextBox4.Text);

            seasonReturnRow["rate_base"] = Convert.ToDecimal(validateTextBox5.Text);
            seasonReturnRow["rate_inc"] = Convert.ToDecimal(validateTextBox6.Text);

            int m = OMWorkBench.DataAgent.UpdateReturnData(returnDS);

            this.DialogResult = DialogResult.OK;
        }

    }
}
