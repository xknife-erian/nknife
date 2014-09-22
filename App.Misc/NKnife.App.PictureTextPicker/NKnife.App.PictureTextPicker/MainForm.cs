using System;
using System.IO;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Views;
using NKnife.Ioc;
using NKnife.Logging.Base;
using NLog;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker
{
    public partial class MainForm : Form
    {
        private Logger _Logger = LogManager.GetCurrentClassLogger();

        private string _DockPath = Path.Combine(Application.StartupPath, "dockpanel.config");
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _ProjectView = DI.Get<ProjectView>();
        private readonly DockContent _PropertyGridView = DI.Get<PropertyGridView>();
        private readonly DockContent _RectangleListView = DI.Get<RectangleListView>();
        private readonly DockContent _LogView = DI.Get<LogView>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, System.EventArgs e)
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
            base.OnFormClosing(e);
        }

        private void InitializeDockPanel()
        {
            Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingMdi;
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

            _Logger.Info("DockPanel初始化完成");
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof(ProjectView).ToString())
                return _ProjectView;
            if (xml == typeof(PropertyGridView).ToString())
                return _PropertyGridView;
            if (xml == typeof(RectangleListView).ToString())
                return _RectangleListView;
            if (xml == typeof (LogView).ToString())
                return _LogView;
            return null;
        }

        private void InitializeDefaultViews()
        {
            _ProjectView.Show(_DockPanel, DockState.DockLeft);
            _RectangleListView.Show(_DockPanel, DockState.DockRight);
            _PropertyGridView.Show(_DockPanel, DockState.DockRight);
            _LogView.Show(_DockPanel,DockState.DockBottom);
        }

        #region 菜单
        private void ExitToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            Close();
        }
        #endregion


    }
}
