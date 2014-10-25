using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NKnife.App.UpdateAssemblyInfo
{
    public partial class KernelForm : Form
    {
        public KernelForm()
        {
            Regex.CacheSize = 100;

            InitializeComponent();
        }

        private void _OpenMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "解决方案文件(*.sln)|*.sln|所有文件(*.*)|*.*";
            dialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"\\..\\..\\");
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                
            }
        }
    }
}
