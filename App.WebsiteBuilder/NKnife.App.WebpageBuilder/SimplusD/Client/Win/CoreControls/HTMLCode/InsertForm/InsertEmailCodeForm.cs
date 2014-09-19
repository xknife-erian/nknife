using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class InsertEmailCodeForm : BaseForm
    {
        public InsertEmailCodeForm()
        {
            InitializeComponent();
            Text = ResourceService.GetResourceText("email.title");
            emailLinkGroupBox.Text = ResourceService.GetResourceText("email.groupboxtext");
            lblEmail.Text = ResourceService.GetResourceText("email.caption");
            lblEmailSubject.Text = ResourceService.GetResourceText("email.subject");
            btnOK.Text = ResourceService.GetResourceText("email.okbtn");
            btnCancel.Text = ResourceService.GetResourceText("email.cancelbtn");

            this.ImeMode = ImeMode.On;
        }

        #region  Ù–‘
        public string TextMailto
        {
            get { return emailComboBox.Text; }
            set { emailComboBox.Text = value; }
        }
        public string TextSubject
        {
            get { return txtEmailSubject.Text; }
            set { txtEmailSubject.Text = value; }
        }
        #endregion
    }
}