using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.NLog3.Controls;

namespace NKnife.Kits.SocketKnife.StressTest
{
    public partial class MainForm : Form
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private MainTestCase _Test;
        private bool _OnTesting;
        public MainForm()
        {
            InitializeComponent();
            LogPanel.AppendLogPanelToContainer(LogContainerPanel);
        }

        private void StartTestButtonClick(object sender, EventArgs e)
        {
            _logger.Info("SocketKnife压力测试开始");
            if(_Test == null)  _Test = new MainTestCase();

            if (_OnTesting)
            {
                _Test.Stop();
                _OnTesting = false;
                StartTestButton.Text = "开始测试";
            }
            else
            {
                var testOption = new MainTestOption();
                var testMonitorFilter = new TestServerMonitorFilter();
                testMonitorFilter.StateChanged += StateChanged;
                if (VerifyTestOption(testOption))
                {
                    _Test.Start(testOption, testMonitorFilter);
                    _OnTesting = true;
                    StartTestButton.Text = "停止测试";
                }
            }
        }

        private void StateChanged(object sender, ServerStateEventArgs serverStateEventArgs)
        {
            SessionCountLabel.ThreadSafeInvoke(() =>
            {
                SessionCountLabel.Text = string.Format("{0}", serverStateEventArgs.SessionCount);
            });

        }

        /// <summary>
        /// 校验测试参数
        /// </summary>
        /// <param name="testOption"></param>
        private bool VerifyTestOption(MainTestOption testOption)
        {
            int clientCount;
            if (!ClientCountTextBox.Text.IsInteger(out clientCount, 1, 100))
            {
                return false;
            }
            testOption.ClientCount = clientCount;

            int sendInterval = 1000;
            if (!ClientSendIntervalTextBox.Text.IsInteger(out sendInterval, 50))
            {
                return false;
            }
            testOption.SendInterval = sendInterval;

            return true;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {

        }
    }
}
