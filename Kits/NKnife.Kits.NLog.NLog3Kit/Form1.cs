using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife.NLog3.Controls;
using NKnife.Wrapper;

namespace NKnife.Kits.NLog.NLog3Kit
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
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(Add200Log) {Name = $"T{i}"};
                thread.Start();
            }
            Add200Log();
        }

        private void Add200Log()
        {
            _Count = 0;
            for (int i = 0; i < 50; i++)
            {
                var log = _IdGenerator.Generate();
                _logger.Trace($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _logger.Debug($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _logger.Info($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _logger.Warn($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _logger.Error($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _logger.Fatal($"{_Count} >> {Thread.CurrentThread.Name}{log}");
                _Count++;
                Thread.Sleep(10);
            }
        }

        private void _SetDebugModeButton_Click(object sender, EventArgs e)
        {
            _IsDebug = !_IsDebug;
            //_LogPanel.SetDebugMode(_IsDebug);
        }

        private bool _IsDebug = false;
    }
}
