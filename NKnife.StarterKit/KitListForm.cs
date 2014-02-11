using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.StarterKit.Forms;

namespace NKnife.StarterKit
{
    public partial class KitListForm : Form
    {
        public KitListForm()
        {
            InitializeComponent();
        }

        private void _LogPanelTestMenuItem_Click(object sender, EventArgs e)
        {
            var form = new LoggingStarterForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
