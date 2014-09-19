using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class FrozedAgentForm : Form
    {
        string _frozedReson = "";
        DataTable _reasonTable = null;
        public string FrozedResonStr
        {
            get { return frozedReasonTextBox.Text; }
            set { frozedReasonTextBox.Text = value; }
        }

        public FrozedAgentForm(DataTable reasonTable)
        {
            InitializeComponent();

            _reasonTable = reasonTable;
            foreach (DataRow dr in reasonTable.Rows)
            {
                string date = dr["change_time"].ToString();
                frozedhistoryListBox.Items.Add(date);
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(frozedReasonTextBox.Text.Trim()))
                MessageBox.Show("请输入原因！！");
            else
                this.DialogResult = DialogResult.OK;
        }

        private void frozedhistoryListBox_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("原因:" + _reasonTable.Rows[frozedhistoryListBox.SelectedIndex]["reason"].ToString());
        }


    }
}
