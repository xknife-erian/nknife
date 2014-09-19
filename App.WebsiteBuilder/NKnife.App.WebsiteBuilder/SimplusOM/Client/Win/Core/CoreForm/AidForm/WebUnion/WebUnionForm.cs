using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class WebUnionForm : Form
    {
        DataRow _unionRow = null;
        FormUseMode _mode;
        public WebUnionForm(DataRow unionRow, FormUseMode mode)
        {
            InitializeComponent();

            _unionRow = unionRow;
            _mode = mode;

            if (mode == FormUseMode.Edit)
                SetForEdit();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            _unionRow["code"] = codeTextBox.Text;
            _unionRow["name"] = nameTextBox.Text;
            _unionRow["password"] = psw1TextBox.Text;
            _unionRow["address"] = adressTextBox.Text;
            _unionRow["email"] = emailTextBox.Text;
            _unionRow["qq"] = QQTextBox.Text;
            _unionRow["msn"] = msnTextBox.Text;
            _unionRow["principal_name"] = linkManTextBox.Text;
            _unionRow["phone"] = telTextBox.Text;
            _unionRow["postcode"] = postCodeTextBox.Text;
            _unionRow["url"] = urlTextBox.Text;
            _unionRow["iframe_address"] = iframeTextBox.Text;
            _unionRow["regtime"] = DateTime.Today;

            this.DialogResult = DialogResult.OK;
        }

        void SetForEdit()
        {
            this.Text = "修改";
            codeTextBox.Enabled = false;
            psw1TextBox.Enabled = false;
            psw2TextBox.Enabled = false;

            codeTextBox.Text = _unionRow["code"].ToString();
            nameTextBox.Text = _unionRow["name"].ToString();
            psw1TextBox.Text = psw2TextBox.Text = _unionRow["password"].ToString();
            adressTextBox.Text = _unionRow["address"].ToString();
            emailTextBox.Text = _unionRow["email"].ToString();
            QQTextBox.Text = _unionRow["qq"].ToString();
            msnTextBox.Text = _unionRow["msn"].ToString();
            linkManTextBox.Text = _unionRow["principal_name"].ToString();
            telTextBox.Text = _unionRow["phone"].ToString();
            postCodeTextBox.Text = _unionRow["postcode"].ToString();
            urlTextBox.Text = _unionRow["url"].ToString();
            iframeTextBox.Text = _unionRow["iframe_address"].ToString();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            
            Close();
        }





 


    }
}
