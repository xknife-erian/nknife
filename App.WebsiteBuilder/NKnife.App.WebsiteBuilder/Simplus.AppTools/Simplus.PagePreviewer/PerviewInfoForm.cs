using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusPagePreviewer
{
    public partial class PerviewInfoForm : BaseForm
    {
        public PerviewInfoForm(string index, string port, string projectName)
        {

            InitializeComponent();
            this.labPort.Text = port;
            this.labRequest.Text = index;
            this.labProName.Text = projectName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
