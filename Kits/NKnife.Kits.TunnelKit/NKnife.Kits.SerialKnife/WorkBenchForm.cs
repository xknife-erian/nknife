using System;
using System.IO;
using System.Windows.Forms;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife
{
    public partial class WorkBenchForm : Form
    {
        private static readonly ILog _Logger = LogManager.GetLogger<WorkBenchForm>();
        private readonly DockContent _controlPanelView = Di.Get<ControlPanelView>();
        private readonly DockPanel _dockPanel = new DockPanel();
        private readonly string _dockPath = Path.Combine(Application.StartupPath, "dockpanel.config");
        private readonly DockContent _logView = Di.Get<LogView>();
        private readonly DockContent _mockDataConnectorView = Di.Get<MockSerialDataConnectorView>();

        #region 初始化

        public WorkBenchForm()
        {
            InitializeComponent();
        }

        private void WorkBenchForm_Load(object sender, EventArgs e)
        {
            InitializeDockPanel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _dockPanel.SaveAsXml(_dockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }

            _controlPanelView.Close();
            base.OnFormClosing(e);
        }

        private void InitializeDockPanel()
        {
            MainPanel.Controls.Add(_dockPanel);
            _dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.BringToFront();

            try
            {
                var deserialize = new DeserializeDockContent(GetContentForm);
                _dockPanel.LoadFromXml(_dockPath, deserialize);
            }
            catch (Exception)
            {
                // 配置文件不存在或配置文件有问题时 按系统默认规则加载子窗体
                InitializeDefaultViews();
            }
            _Logger.Info("DockPanel初始化完成");
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof (ControlPanelView).ToString())
                return _controlPanelView;
            if (xml == typeof (LogView).ToString())
                return _logView;
            if (xml == typeof (MockSerialDataConnectorView).ToString())
                return _mockDataConnectorView;
            return null;
        }

        private void InitializeDefaultViews()
        {
            _logView.HideOnClose = true;
            _logView.Show(_dockPanel, DockState.DockBottom);
            _mockDataConnectorView.HideOnClose = true;
            _mockDataConnectorView.Show(_dockPanel, DockState.DockRight);
            _controlPanelView.HideOnClose = true;
            _controlPanelView.Show(_dockPanel, DockState.Document);
        }

        #endregion

        #region 菜单工具栏

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 数据连接器设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var setting = new SettingForm();
            setting.ShowDialog();
        }

        private void 操作面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_controlPanelView != null)
                _controlPanelView.Show();
        }

        private void 日志面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_logView != null)
                _logView.Show();
        }

        private void 模拟串口连接器面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mockDataConnectorView != null)
                _mockDataConnectorView.Show();
        }

        #endregion
    }
}