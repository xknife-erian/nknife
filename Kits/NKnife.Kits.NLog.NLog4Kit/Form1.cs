using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.NLog.Controls;
using NKnife.Wrapper;

namespace NKnife.Kits.NLog.NLog4Kit
{
    public partial class Form1 : Form
    {
        private readonly LogPanel _LogPanel = LogPanel.Instance;
        private static readonly ILog _logger = LogManager.GetLogger<Form1>();
        private IdGenerator _IdGenerator = new IdGenerator();

        public Form1()
        {
            InitializeComponent();
            Controls.Add(_LogPanel);
            _LogPanel.BringToFront();
            _LogPanel.Dock = DockStyle.Fill;
        }

        private int _Count = 0;
        private void Input100LogButton_Click(object sender, EventArgs e)
        {
            _Count = 0;
            for (int i = 0; i < 100; i++)
            {
                _logger.Trace($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
                _logger.Debug($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
                _logger.Info($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
                _logger.Warn($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
                _logger.Error($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
                _logger.Fatal($"{_Count} ////>> {_IdGenerator.Generate()}"); _Count++;
            }
        }

        private void _SetDebugModeButton_Click(object sender, EventArgs e)
        {
            _IsDebug = !_IsDebug;
            _LogPanel.SetDebugMode(_IsDebug);
        }

        private bool _IsDebug = false;
    }
}
