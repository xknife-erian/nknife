using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.NLog3.Controls;
using NKnife.Utility;

namespace NKnife.Kits.SocketKnife.StressTest
{
    public partial class MainForm : Form
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private MainTestCase _Test;
        private bool _OnTesting;
        private bool _OnLogConsole;
        public MainForm()
        {
            InitializeComponent();
            //LogPanel.AppendLogPanelToContainer(MainLogPanel);
        }

        /// <summary>
        /// 启动/停止测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTestButtonClick(object sender, EventArgs e)
        {
            _logger.Info("SocketKnife压力测试开始");
            if(_Test == null)  _Test = new MainTestCase();

            if (_OnTesting)
            {
                _Test.Stop();
                _OnTesting = false;
                StartTestButton.Text = "开始测试";
                AddTalkButton.Enabled = false;
                RemoveTalkButton.Enabled = false;
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
                    AddTalkButton.Enabled = true;
                    RemoveTalkButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 增加对讲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTalkButtonClick(object sender, EventArgs e)
        {
            if (_Test != null)
                _Test.AddTalk();
        }

        /// <summary>
        /// 隐藏对讲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RemoveTalkButtonClick(object sender, EventArgs e)
        {
            if(_Test !=null)
                _Test.RemoveTalk();
        }

        /// <summary>
        /// 显示隐藏日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleLogButton_Click(object sender, EventArgs e)
        {
            if (_OnLogConsole)
            {
                //隐藏
                UtilityConsole.CloseConsole();
                _OnLogConsole = false;
                ToggleLogButton.Text = "显示日志";
            }
            else
            {
                //显示
                UtilityConsole.OpenConsole();
                _OnLogConsole = true;
                ToggleLogButton.Text = "隐藏日志";
            }
        }

        private void StateChanged(object sender, ServerStateEventArgs serverStateEventArgs)
        {
            SessionCountLabel.ThreadSafeInvoke(() =>
            {
                SessionCountLabel.Text = string.Format("{0}", serverStateEventArgs.SessionCount);
                TalkCountLabel.Text = string.Format("{0}", serverStateEventArgs.TalkCount);
            });

        }

        /// <summary>
        /// 校验测试参数
        /// </summary>
        /// <param name="testOption"></param>
        private bool VerifyTestOption(MainTestOption testOption)
        {
            int clientCount;
            if (!ClientCountTextBox.Text.IsInteger(out clientCount, 1, 1000))
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
            if (_OnLogConsole)
            {
                //隐藏
                UtilityConsole.CloseConsole();
            }
            if (_OnTesting)
            {
                _Test.Stop();
            }
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_OnLogConsole)
            {
                //隐藏
                UtilityConsole.CloseConsole();
            }
            if (_OnTesting)
            {
                _Test.Stop();
            }
            base.OnFormClosing(e);
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {

        }




    }
}
