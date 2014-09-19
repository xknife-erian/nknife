using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class CheckWebSiteForm : Form
    {
        public int CheckType
        {
            get
            {
               return Convert.ToInt32(typeComboBox.SelectedValue);
            }
        }
        DataRow _siteRow = null;
        public CheckWebSiteForm(DataRow siteRow)
        {
            InitializeComponent();

            _siteRow = siteRow;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataTable industryTable = OMWorkBench.BaseInfoDS.Tables["industry"];
            BindingSource industryBDS = new BindingSource();
            industryBDS.DataSource = industryTable;
            industryBDS.Filter = "level=1";

            typeComboBox.ValueMember = "code";
            typeComboBox.DisplayMember = "name";
            typeComboBox.DataSource = industryBDS;

            nameTextBox.Enabled = false;
            codeTextBox.Enabled = false;
            urlTextBox.Enabled = false;

            nameTextBox.Text = _siteRow["name"].ToString();
            codeTextBox.Text = _siteRow["web_code"].ToString();
            urlTextBox.Text = _siteRow["domain_url"].ToString();
            typeComboBox.SelectedValue = _siteRow["back_indu_code"].ToString();
        }
    }
}
