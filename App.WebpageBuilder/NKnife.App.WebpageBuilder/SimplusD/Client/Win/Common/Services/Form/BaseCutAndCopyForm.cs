using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class BaseCutAndCopyForm : Form
    {
        public BaseCutAndCopyForm()
        {
            InitializeComponent();
        }

        public string NewName
        {
            get { return newNameTextBox.Text; }
        }
        private void BaseCutAndCopyForm_Load(object sender, EventArgs e)
        {
            this.label1.Text = StringParserService.Parse("${res:cutOrCopyError.error}");
            this.label1.TextAlign = ContentAlignment.BottomCenter;
            this.label1.ForeColor = Color.Red;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = StringParserService.Parse("${res:tagError.error}");
        }
    }
}
