using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ModifyWebSiteForm : Form
    {
        DataRow _siteRow;
        FormUseMode _userMode;
        DataTable _unionTable = null;
        public ModifyWebSiteForm(DataRow siteRow,DataTable unionTable, FormUseMode userMode)
        {
            InitializeComponent();

            _siteRow = siteRow;
            _userMode = userMode;
            _unionTable = unionTable;

            if (userMode == FormUseMode.Edit)
                SetForEdit();
        }


        protected override void OnLoad(EventArgs e)
        {
            DataTable _areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            BindingSource areaSource = new BindingSource();
            areaSource.DataSource = _areaTable;
            areaSource.Filter = "level=1";

            areaComboBox.ValueMember = "id";
            areaComboBox.DisplayMember = "name";
            areaComboBox.DataSource = areaSource;


            DataTable industryTable = OMWorkBench.BaseInfoDS.Tables["industry"];

            BindingSource unionBDS = new BindingSource();
            unionBDS.DataSource = _unionTable;

            webUnionComboBox.ValueMember = "id";
            webUnionComboBox.DisplayMember = "name";
            webUnionComboBox.DataSource = unionBDS;
            industryControl1.IndustrySource = OMWorkBench.BaseInfoDS.Tables["industry"];
            industryControl1.Industry1ComboBox.ValueMember = "id";
            industryControl1.Industry1ComboBox.DisplayMember = "name";
           // industryControl1.Industry1ComboBox.DataSource = industrySource;
            base.OnLoad(e);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            _siteRow["name"] = nameTextBox.Text;
            _siteRow["web_code"] = codeTextBox.Text;
            _siteRow["domain_url"] = urlTextBox.Text;
            _siteRow["principal_name"] = linkmanTextBox.Text;
            _siteRow["phone"] = telTextBox.Text;
            //_siteRow[""] =areaTextBox.Text;
            _siteRow["qq"] =qqTextBox .Text;
            _siteRow["msn"] = msnTextBox.Text;
            _siteRow["email"] = emailTextBox.Text;
            _siteRow["back_indu_code"] = OMWorkBench.BaseInfoDS.Tables["industry"].Select("id="+Convert.ToInt32(industryControl1.Industry2ComboBox.SelectedValue))[0]["code"].ToString();
            _siteRow["web_union_id"] =  webUnionComboBox.SelectedValue;
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SetForEdit()
        {
            this.Text = "修改";
            nameTextBox.Enabled = false;

            nameTextBox.Text = _siteRow["name"].ToString();
            codeTextBox.Text = _siteRow["web_code"].ToString();
            urlTextBox.Text = _siteRow["domain_url"].ToString();
            linkmanTextBox.Text = _siteRow["principal_name"].ToString();
            telTextBox.Text = _siteRow["phone"].ToString();
            //areaTextBox.Text = _siteRow[""].ToString();
            msnTextBox.Text = _siteRow["msn"].ToString();
            qqTextBox.Text = _siteRow["qq"].ToString();
            emailTextBox.Text = _siteRow["email"].ToString();
            industryControl1.Industry1ComboBox.SelectedValue =Convert.ToInt32(OMWorkBench.BaseInfoDS.Tables["industry"].Select("code='" +_siteRow["back_indu_code"].ToString()+"'")[0]["id"]); 
            webUnionComboBox.SelectedValue = _siteRow["web_union_id"].ToString();
        }
    }
}
