using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Jeelu.SimplusSoftwareUpdate
{
    public partial class SetupFinish : Form
    {
        public SetupFinish()
        {
            InitializeComponent();
        }

        public SetupFinish(UpdateClass update)
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetupFinish_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chkRunSimplusD.Checked)
            {
                string simplusdFile = Path.Combine(Application.StartupPath, MainClass.SimplusDExe);
                UpdateClass.End(true);
            }
        }
    }
}
