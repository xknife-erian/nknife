using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NKnife.Upgrade4Github.App;
using NKnife.Upgrade4Github.Base;
using NKnife.Upgrade4Github.Util;
using NKnife.Upgrade4Github.Util.Zip;

namespace NKnife.Upgrade4Github.App
{
    /// <summary>
    /// 更新主窗体
    /// </summary>
    partial class UpdaterWorkbench : Form
    {
        private readonly UpdateService _updateService;

        public UpdaterWorkbench(string[] args)
        {
            InitializeComponent();
            if (args == null || args.Length <= 0)
                _packageDropDownButton.Visible = true;
            else
                _packageDropDownButton.Visible = false;
            _updateService = new UpdateService();
//            var a = "--u xknife-erian --p nknife.win.updaterFromGitHub --t NKnife.Win.UpdaterFromGitHub.Example.exe --v 1.2";
//            var cmdHelper = new CommandLineArgsHelper(a.Split(' '));
            var cmdHelper = new CommandLineArgsHelper(args);
            foreach (var item in cmdHelper.Parameters)
            {
                InitializeUpdateInfo(item.Key, item.Value);
            }

            _updateService.UpdateStatusChanged += OnUpdateStatusChanged;
            _versionLabel.Text = $"{Assembly.GetEntryAssembly()?.GetName().Version}";
            if (IsAdministrator())
                AddShieldToButton(_updateButton);
            else
                DeleteShieldFromButton(_updateButton);
            //在Form_Load里会调用一个访问，返回更新到界面
        }

        private void InitializeUpdateInfo(string key, string value)
        {
            switch (key)
            {
                case "username":
                case "u":
                    _updateService.UpdateArgs.Username = value.Trim();
                    break;
                case "project":
                case "p":
                    _updateService.UpdateArgs.Project = value.Trim();
                    break;
                case "title":
                case "t":
                    _updateService.UpdateArgs.Parent.Title = value.Trim();
                    break;
                case "version":
                case "v":
                    if (Version.TryParse(value.Trim(), out var version))
                        _updateService.UpdateArgs.Parent.Version = version;
                    break;
                case "run":
                case "r":
                    _updateService.UpdateArgs.Parent.Runner = value.Trim();
                    break;
                case "autoRun":
                case "ar":
                    if (bool.TryParse(value.Trim(), out var b))
                        _updateService.UpdateArgs.Parent.IsAutoRun = b;
                    break;
                default:
                    break;
            }
        }

        private void OnUpdateStatusChanged(object sender, UpdateStatusChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                switch (e.Status)
                {
                    case UpdateStatus.Start:
                        _infoLabel.Text = e.Message;
                        break;
                    case UpdateStatus.GetLatestReleaseCompleted:
                        _updateInfoTextBox.Text = e.Message;
                        _infoLabel.Text = "获取更新信息完成";
                        _updateButton.Enabled = true;
                        break;
                    case UpdateStatus.FileDownloading:
                        _updateProgressBar.Style = ProgressBarStyle.Blocks;
                        _infoLabel.Text = e.Message;
                        if (e.Message.LastIndexOf(':') > 0)
                        {
                            var p = e.Message.Substring(e.Message.LastIndexOf(':') + 1).TrimEnd('%');
                            _updateProgressBar.Value = int.Parse(p);
                        }
                        break;
                    case UpdateStatus.Update:
                        _infoLabel.Text = e.Message;
                        break;
                    case UpdateStatus.Done:
                        _infoLabel.Text = e.Message;
                        Application.DoEvents();
                        var ds = MessageBox.Show("是否运行更新后的程序？", "更新完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (ds == DialogResult.Yes)
                        {
                            StartParent();
                        }

                        Application.Exit();
                        break;
                    case UpdateStatus.Error:
                        MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                Application.DoEvents();
            }));
        }

        private void StartParent()
        {
            if (_updateService.UpdateArgs.Parent != null && !string.IsNullOrEmpty(_updateService.UpdateArgs.Parent.Runner))
                Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    _updateService.UpdateArgs.Parent.Runner));
        }

        #region 窗体响应

        private void Form_OnLoad(object sender, EventArgs e)
        {
            _updateService.GetLatestReleaseInfo();
            _closeButton.Enabled = true;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            Text = $"{_updateService.UpdateArgs.Parent.Title} V{Application.ProductVersion}";
            _infoLabel.Text = "正在获取更新信息……";
            _updateProgressBar.Value = 0;
            _updateInfoTextBox.Clear();
            _updateButton.Enabled = false;
            _updateService.GetLatestReleaseInfo();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _refreshButton.Enabled = false;
            _updateButton.Enabled = false;
            _updateProgressBar.Style = ProgressBarStyle.Marquee;
            _updateService.StartAsync();
            _updateProgressBar.Focus();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        #endregion

        #region Overrides of Form

        /// <summary>
        ///   引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。
        /// </summary>
        /// <param name="e">
        ///   包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。
        /// </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            _updateService.Stop();
            base.OnClosing(e);
        }

        #endregion

        #region UAC

        /// <summary>
        /// 给目标按钮添加上UAC盾牌标志
        /// </summary>
        private void AddShieldToButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.System;
            SendMessage(btn.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

        /// <summary>
        /// 给目标按钮去除掉UAC盾牌标志
        /// </summary>
        private void DeleteShieldFromButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.System;
            SendMessage(btn.Handle, BCM_SETSHIELD, 0, 0x0);
        }

        private bool IsAdministrator()
        {
            var wp = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());
            return (wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator));
        }

        [DllImport("user32")]
        private static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        private const int BCM_FIRST = 0x1600;
        private const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        #endregion

        private void _packageMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.RestoreDirectory = true;
            var ds = dialog.ShowDialog(this);
            if (ds == DialogResult.OK)
            {
                var fis = new List<FileInfo>();
                foreach (var fileName in dialog.FileNames)
                {
                    var fi = new FileInfo(fileName);
                    fis.Add(fi);
                }

                var dir = fis[0].DirectoryName;
                GZip.Compress(fis.ToArray(), dir, dir, "package.zip");
            }
        }

        private void _unPackageMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            var ds = dialog.ShowDialog(this);
            if (ds == DialogResult.OK && !string.IsNullOrEmpty(dialog.FileName))
            {
                var fi = new FileInfo(dialog.FileName);
                var dir = fi.DirectoryName;
                GZip.Decompress(dir, Path.Combine(dir, "pg"), fi.Name);
            }
        }
    }
}
