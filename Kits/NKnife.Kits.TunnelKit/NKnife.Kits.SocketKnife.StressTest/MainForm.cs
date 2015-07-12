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
using NKnife.IoC;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Kits.SocketKnife.StressTest.View;
using NKnife.NLog3.Controls;
using NKnife.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest
{
    public partial class MainForm : Form
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private readonly string _DockPath = Path.Combine(Application.StartupPath, "dockpanel.config");
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _LogView = DI.Get<LogView>();
        private readonly DockContent _ServerView = DI.Get<ServerView>();
        private readonly DockContent _MockClientView = DI.Get<MockClientView>();
        private readonly DockContent _ProtocolView = DI.Get<ProtocolView>();
        public MainForm()
        {
            InitializeComponent();
            //LogPanel.AppendLogPanelToContainer(MainLogPanel);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            InitializeDockPanel();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _DockPanel.SaveAsXml(_DockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }
            _ServerView.Close();
            base.OnFormClosing(e);
        }

        private void InitializeDockPanel()
        {
            MainPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            try
            {
                var deserialize = new DeserializeDockContent(GetContentForm);
                _DockPanel.LoadFromXml(_DockPath, deserialize);
            }
            catch (Exception)
            {
                // 配置文件不存在或配置文件有问题时 按系统默认规则加载子窗体
                InitializeDefaultViews();
            }
            _logger.Info("DockPanel初始化完成");
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof(LogView).ToString())
                return _LogView;
            if (xml == typeof(ServerView).ToString())
                return _ServerView;
            if (xml == typeof(MockClientView).ToString())
                return _MockClientView;
            if (xml == typeof(ProtocolView).ToString())
                return _ProtocolView;
            return null;
        }

        private void InitializeDefaultViews()
        {
            _LogView.HideOnClose = true;
            _LogView.Show(_DockPanel, DockState.DockBottom);
            _ServerView.HideOnClose = true;
            _ServerView.Show(_DockPanel, DockState.Document);
            _MockClientView.HideOnClose = true;
            _MockClientView.Show(_DockPanel, DockState.Document);
            _ProtocolView.HideOnClose = true;
            _ProtocolView.Show(_DockPanel, DockState.DockRight);
        }






        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }



        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void ServiceSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dlg = new ServiceSettingForm();
            dlg.ShowDialog();
        }

        private void LogViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(_LogView !=null)
                _LogView.Show();
        }

        private void ServerViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(_ServerView !=null)
                _ServerView.Show();
        }

        private void ClientViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_MockClientView != null)
                _MockClientView.Show();
        }

        private void ProtocolViewToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(_ProtocolView !=null)
                _ProtocolView.Show();
        }






    }
}
