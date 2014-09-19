using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.Win
{
    public partial class InsertEmailCodeForm : BaseForm
    {
        string insertEmailHTML = "";
        public string InsertEmailHTML
        {
            get { return insertEmailHTML; }
            set { insertEmailHTML = value; }
        }

        public string TextMailto
        {
            get { return emailComboBox.Text; }
        }
        public string TextSubject
        {
            get { return txtEmailSubject.Text; }
        }

        public InsertEmailCodeForm()
        {
            InitializeComponent();
            Text = "ÓÊ¼þ";
            emailLinkGroupBox.Text = "";
            lblEmail.Text = "";
            lblEmailSubject.Text = "";
            btnOK.Text = "";
            btnCancel.Text = "";
            this.ImeMode = ImeMode.On;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string mailtotext = emailComboBox.Text;
            string subtext = txtEmailSubject.Text;
            Email insEmail = new Email();
            insertEmailHTML = insEmail.EmailHtml(subtext, mailtotext);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}