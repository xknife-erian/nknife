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
using NKnife.NLog.WinForm;
using NKnife.Wrapper;
using NKnife.IoC;

namespace NKnife.Kits.NLog.NLog4Kit
{
    public partial class Form1 : Form
    {
        
        private static readonly ILog _logger = LogManager.GetLogger<Form1>();
        private readonly IdGenerator _IdGenerator = new IdGenerator();
        private readonly LoggerGridView _LogPanel = LoggerGridView.Instance;

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
            for (int i = 0; i < 20; i++)
            {
                Thread thread = new Thread(Add200Log) {Name = $"T{i}"};
                thread.Start();
            }
            Add200Log();
        }

        private void Add200Log()
        {
            _Count = 0;
            for (int i = 0; i < 200; i++)
            {
                _logger.Trace($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Debug($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Info($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Warn($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Error($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Fatal($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _Count++;
            }
        }

        private void _SetDebugModeButton_Click(object sender, EventArgs e)
        {
            _IsDebug = !_IsDebug;
            _LogPanel.SetDebugMode(_IsDebug);
        }

        private bool _IsDebug = false;

        private void _SimpleTestButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                _logger.Trace($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Debug($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Info($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Warn($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Error($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
                _logger.Fatal($"{_Count} >> {Thread.CurrentThread.Name}{_IdGenerator.Generate()}");
            }
        }
    }
}
