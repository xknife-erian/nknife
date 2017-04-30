using System;
using System.IO;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Channels.SerialKnife.Dialogs;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.NLog;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Channels.SerialKnife.Views
{
    public partial class Workbench : Form
    {
        private const string DOCK_PANEL_CONFIG = "dockpanel3.config";
        private static readonly ILog _logger = LogManager.GetLogger<Workbench>();
        private readonly WorkbenchViewModel _ViewModel = DI.Get<WorkbenchViewModel>();
        private readonly DockPanel _DockPanel = new DockPanel();

        public Workbench()
        {
            InitializeComponent();
            _VersionStatusLabel.Text = DI.Get<IAbout>().AssemblyVersion.ToString();
            _TotalStatusLabel.Text = string.Empty;
            _CurrentPortStatusLabel.Text = string.Empty;

            _logger.Info("主窗体构建完成");
            InitializeDockPanel();
            _logger.Info("添加Dock面板完成");

            _LoggerMenuItem.Click += (sender, args) =>
            {
                var loggerForm = new NLogForm {TopMost = true};
                loggerForm.Show();
            };
            _ViewModel.SerialChannelService.ChannelCountChanged += (sender, args) =>
            {
                this.ThreadSafeInvoke(() => { _TotalStatusLabel.Text = $"Total: {_ViewModel.SerialChannelService.Count}"; });
            };
            _DockPanel.ActiveDocumentChanged += (sender, args) =>
            {
                _CurrentPortStatusLabel.Text = _DockPanel.ActiveDocument != null ? ((Control) _DockPanel.ActiveDocument).Text : string.Empty;
            };
        }

        private void _NewPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SerialPortSelectorDialog();
            var ds = dialog.ShowDialog(this);
            if (ds == DialogResult.OK && dialog.SerialPort > 0)
            {
                var view = new SerialPortView();
                view.Text = $"COM{dialog.SerialPort}";
                view.ViewModel.Port = dialog.SerialPort;
                view.Show(_DockPanel);
                _CurrentPortStatusLabel.Text = view.Text;
            }
        }

        #region DockPanel

        private static string GetLayoutConfigFile()
        {
            var dir = Path.GetDirectoryName(Application.ExecutablePath);
            return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);

            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            DockPanelLoadFromXml();
        }

        /// <summary>
        ///     控件提供了一个保存布局状态的方法，它默认是没有保存的。
        /// </summary>
        private void DockPanelSaveAsXml()
        {
            _DockPanel.SaveAsXml(GetLayoutConfigFile());
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            var configFile = GetLayoutConfigFile();
            if (File.Exists(configFile))
            {
                _DockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
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
    }
}