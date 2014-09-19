using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu
{
    internal partial class CloseAllWindowSaveForm : Form
    {
        public CloseAllWindowSaveForm(string strMsg)
        {
            InitializeComponent();

            lblShowMsg.Text = strMsg;
            this.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.title}");
            btnYes.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.buttonYes}");
            btnAllYes.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.buttonAllYes}");
            btnNo.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.buttonNo}");
            btnAllNo.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.buttonAllNo}");
            btnCancel.Text = StringParserService.Parse("${res:closeAllWindowSaveOption.form.buttonCancel}");
        }

        private CloseAllWindowOptionResult _result = CloseAllWindowOptionResult.None;
        public CloseAllWindowOptionResult Result
        {
            get { return _result; }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            _result = CloseAllWindowOptionResult.Yes;
            this.Close();
        }

        private void btnAllYes_Click(object sender, EventArgs e)
        {
            _result = CloseAllWindowOptionResult.AllYes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            _result = CloseAllWindowOptionResult.No;
            this.Close();
        }

        private void btnAllNo_Click(object sender, EventArgs e)
        {
            _result = CloseAllWindowOptionResult.AllNo;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public enum CloseAllWindowOptionResult
    {
        None            = 0,
        Yes             = 1,
        AllYes          = 2,
        No              = 3,
        AllNo           = 4,
    }
}