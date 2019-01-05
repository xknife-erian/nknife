using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using Ninject;
using NKnife.ChannelKnife.ViewModel;
using NKnife.IoC;
using ReactiveUI;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.ChannelKnife.Views
{
    public partial class Workbench : Form, IViewFor<WorkbenchViewModel>
    {
        private static readonly ILog _Logger = LogManager.GetLogger<Workbench>();

        [Inject]
        public Workbench()
        {
            InitializeComponent();
            InitializeDockPanel();
#if !DEBUG
            WindowState = FormWindowState.Maximized;
#endif
            this.WhenActivated(b => { b(this.OneWayBind(ViewModel, vm => vm.Version, v => v._VersionStatusLabel.Text)); });
            _LoggerMenuItem.Click += (sender, args) =>
            {
                var loggerForm = DI.Get<LoggerView>();
                loggerForm.Show(_dockPanel, DockState.DockBottom);
            };
            _dockPanel.ActiveDocumentChanged += (sender, args) => { };
        }

        [Inject] public Dialogs Dialogs { get; set; }


        private void _NewPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = Dialogs.PortSelectorDialog;
            dialog.ShowDialog(this);
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            MessageBox.Show(this, $"ChannelKnife 2019\r\nVersion:{version}\r\n\r\nEmail: lukan@xknife.net\r\nhttp://www.xknife.net", "关于",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region DockPanel

        private const string DOCK_PANEL_CONFIG = "dockpanel3.config";
        private readonly DockPanel _dockPanel = new DockPanel();

        private static string GetLayoutConfigFile()
        {
            var dir = Path.GetDirectoryName(Application.ExecutablePath);
            return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
        }

        private void InitializeDockPanel()
        {
            SuspendLayout();
            _StripContainer.ContentPanel.Controls.Add(_dockPanel);

            _dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _dockPanel.Theme = new VS2015BlueTheme();
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.BringToFront();

            var loggerForm = DI.Get<LoggerView>();
            loggerForm.Show(_dockPanel, DockState.DockBottomAutoHide);

            DockPanelLoadFromXml();

            PerformLayout();
            ResumeLayout(false);
        }

        /// <summary>
        ///     控件提供了一个保存布局状态的方法，它默认是没有保存的。
        /// </summary>
        private void DockPanelSaveAsXml()
        {
            _dockPanel.SaveAsXml(GetLayoutConfigFile());
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            var configFile = GetLayoutConfigFile();
            if (File.Exists(configFile)) _dockPanel.LoadFromXml(configFile, deserializeDockContent);
        }

        private IDockContent GetViewFromPersistString(string persistString)
        {
//            if (persistString == typeof(LoggerView).ToString())
//            {
//                if (_LoggerViewMenuItem.Checked)
//                    return _LoggerView;
//            }
//            if (persistString == typeof(InterfaceTreeView).ToString())
//            {
//                if (_InterfaceTreeViewMenuItem.Checked)
//                    return _InterfaceTreeView;
//            }
//            if (persistString == typeof(DataMangerView).ToString())
//            {
//                if (_DataManagerViewMenuItem.Checked)
//                    return _DataManagerView;
//            }
//            if (persistString == typeof(CommandConsoleView).ToString())
//            {
//                if (_CommandConsoleViewMenuItem.Checked)
//                    return _CommandConsoleView;
//            }
            return null;
        }

        #endregion

        #region IViewFor

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as WorkbenchViewModel;
        }

        [Inject] public WorkbenchViewModel ViewModel { get; set; }

        #endregion
    }
}