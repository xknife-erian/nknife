using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public partial class TestCaseResultDialog : Form
    {
        public TestCaseResultDialog(string message)
        {
            InitializeComponent();
            MessageTextBox.Text = message;
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
