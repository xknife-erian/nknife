using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.GUI.WinForm;
using NKnife.NLog3.Controls;

namespace NKnife.Kits.NLog.NLog3Kit
{
    public partial class Form1 : SimpleForm
    {
        private readonly NLog3.Controls.LogPanel _LogPanel = LogPanel.Instance;
        private static readonly ILog _logger = LogManager.GetLogger<Form1>();

        public Form1()
        {
            InitializeComponent();
            Controls.Add(_LogPanel);
            _LogPanel.Dock = DockStyle.Fill;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                _logger.Trace(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}", i));
                _logger.Debug(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}", i));
                _logger.Info(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}", i));
                _logger.Warn(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}", i));
                _logger.Error(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}", i));
            }
        }
    }
}
