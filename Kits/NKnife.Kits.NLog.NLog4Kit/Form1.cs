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
using NKnife.NLog;

namespace NKnife.Kits.NLog.NLog4Kit
{
    public partial class Form1 : Form
    {
        
        private static readonly ILog _Logger = LogManager.GetLogger<Form1>();
        private readonly IdGenerator _idGenerator = new IdGenerator();
        private readonly LoggerListView _logPanel = LoggerListView.Instance;

        public Form1()
        {
            InitializeComponent();
            Controls.Add(_logPanel);
            _logPanel.BringToFront();
            _logPanel.Dock = DockStyle.Fill;
        }

        private int _count = 0;
        private void Input100LogButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                Thread thread = new Thread(() => AddLogs(50)) {Name = $"T{i}"};
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void AddLogs(int count)
        {
            _count = 0;
            for (int i = 0; i < count; i++)
            {
                _Logger.Trace($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _Logger.Debug($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _Logger.Info($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _Logger.Warn($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _Logger.Error($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _Logger.Fatal($"{_count} >> {Thread.CurrentThread.Name}{_idGenerator.Generate()}");
                _count++;
            }
        }

        private void _SetDebugModeButton_Click(object sender, EventArgs e)
        {
            _isDebug = !_isDebug;
            _logPanel.SetDebugMode(_isDebug);
        }

        private bool _isDebug = false;

        private void _SimpleTestButton_Click(object sender, EventArgs e)
        {
            AddLogs(5);
        }

        private void _LoggerFormButton_Click(object sender, EventArgs e)
        {
            var loggerForm = new NLogForm { TopMost = true };
            loggerForm.Show();
        }
    }
}
