using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    internal partial class MessageCheckBoxForm : BaseForm
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public bool IsShowCheckBox { get; set; }

        public string CheckBoxText { get; set; }

        public bool IsChecked { get; set; }

        public MessageCheckBoxForm(string message)
        {
            this.Message = message;
            IsShowCheckBox = true;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Text = Title;
            this.lblShow.Text = Message;
            checkbox.Visible = this.IsShowCheckBox;
            checkbox.Text = CheckBoxText;
            checkbox.Checked = this.IsChecked;

            base.OnLoad(e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsChecked = checkbox.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsChecked = checkbox.Checked;
        }
    }
}
