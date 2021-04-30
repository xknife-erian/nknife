using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NKnife.NLog.WinForm.Util;
using NLog;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     日志显示面板控件，日志等级通过左上角的日志等级列表进行筛选
    /// </summary>
    public partial class LoggerListView : UserControl
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private const string FONT_FAMILY_NAME = "Microsoft YaHei UI";
        private readonly LoggerListViewViewModel _viewModel;

        private Tuple<Color, Color> _debugColor = new Tuple<Color, Color>(Color.DarkSlateBlue, Color.White);
        private Tuple<Color, Color> _errorColor = new Tuple<Color, Color>(Color.Black, Color.Orange);
        private Tuple<Color, Color> _fatalColor = new Tuple<Color, Color>(Color.White, Color.OrangeRed);
        private Tuple<Color, Color> _infoColor = new Tuple<Color, Color>(Color.Black, Color.White);
        private Tuple<Color, Color> _trackColor = new Tuple<Color, Color>(Color.CornflowerBlue, Color.White);
        private Tuple<Color, Color> _warnColor = new Tuple<Color, Color>(Color.Black, Color.Khaki);

        private Level _selfLevel;

        public LoggerListView()
        {
            _viewModel = LoggerListViewViewModel.Instance;
            _selfLevel = _viewModel.CurrentLevel;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font(FONT_FAMILY_NAME, 8.25F);

            #region Menu Checked

            _TraceMenuItem.Checked = _selfLevel.HasFlag(Level.Trace);
            _DebugMenuItem.Checked = _selfLevel.HasFlag(Level.Debug);
            _InfoMenuItem.Checked = _selfLevel.HasFlag(Level.Info);
            _WarnMenuItem.Checked = _selfLevel.HasFlag(Level.Warn);
            _ErrorMenuItem.Checked = _selfLevel.HasFlag(Level.Error);
            _FatalMenuItem.Checked = _selfLevel.HasFlag(Level.Fatal);

            #endregion

            SizeChanged += (s, e) => { SetViewColumnSize(); };
            _MaxViewCountTextBox.Text = MaxDisplayCount.ToString();
            _ListView.MouseDoubleClick += LoggerListViewDouble_Click;
            _MaxViewCountTextBox.TextChanged += MaxViewCountTextBox_TextChanged;
            _ListView.Font = new Font(FONT_FAMILY_NAME, 9.5F);
            _viewModel.LogInfos.CollectionChanged += LogInfos_CollectionChanged;
            _viewModel.MaxViewCountChanged += (s, e) =>
            {
                try
                {
                    if (_MaxViewCountTextBox?.Text != null && !MaxDisplayCount.ToString().Equals(_MaxViewCountTextBox.Text))
                        _MaxViewCountTextBox.Text = MaxDisplayCount.ToString();
                }
                catch (Exception exception)
                {
                    _Logger.Warn($"更新 MaxDisplayCount 的值时异常，{exception.Message}");
                }
            };
            _viewModel.CurrentLevelChanged += (s, e) => { SetLevelButtonState(); };
        }

        private void SetLevelButtonState()
        {
            var level = _viewModel.CurrentLevel;
            if (_selfLevel != level)
            {
                _selfLevel = level;
                _TraceMenuItem.Checked = level.HasFlag(Level.Trace);
                _DebugMenuItem.Checked = level.HasFlag(Level.Debug);
                _InfoMenuItem.Checked = level.HasFlag(Level.Info);
                _WarnMenuItem.Checked = level.HasFlag(Level.Warn);
                _ErrorMenuItem.Checked = level.HasFlag(Level.Error);
                _FatalMenuItem.Checked = level.HasFlag(Level.Fatal);
            }
        }

        /// <summary>
        ///     是否显示工具栏
        /// </summary>
        public bool ToolStripVisible
        {
            get => _ToolStrip.Visible;
            set => _ToolStrip.Visible = value;
        }

        /// <summary>
        ///     最大显示日志的数量
        /// </summary>
        public uint MaxDisplayCount
        {
            get => _viewModel.MaxViewCount;
            set => _viewModel.MaxViewCount = value;
        }

        /// <summary>
        /// 快速设置日志级别
        /// </summary>
        /// <param name="isDebug">是否是Debug状态，非Debug状态时，不显示trace,debug级别的日志</param>
        public void SetDebugMode(bool isDebug)
        {
            if (isDebug)
                _viewModel.CurrentLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
            else
                _viewModel.CurrentLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
        }

        /// <summary>
        /// 设置不同级别日志的前景与背景色
        /// </summary>
        public void SetColors(
            Tuple<Color, Color> trace,
            Tuple<Color, Color> debug,
            Tuple<Color, Color> info,
            Tuple<Color, Color> warn,
            Tuple<Color, Color> error,
            Tuple<Color, Color> fatal)
        {
            _trackColor = trace;
            _debugColor = debug;
            _infoColor = info;
            _warnColor = warn;
            _errorColor = error;
            _fatalColor = fatal;
        }

        /// <summary>
        ///     双击一条日志弹出详细窗口
        /// </summary>
        private void LoggerListViewDouble_Click(object sender, MouseEventArgs e)
        {
            var si = _ListView.HitTest(e.X, e.Y);
            var info = (LogEventInfo) si.Item?.Tag;
            if (info != null)
                LoggerInfoDetailForm.Show(info);
        }

        /// <summary>
        /// 当日志级别按钮Check时发生
        /// </summary>
        private void SetLevelGroupToolButton_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            item.Checked = !item.Checked;
            SetViewModelCurrentLevel();
        }

        private void SetViewModelCurrentLevel()
        {
            var level = Level.None;
            if (_TraceMenuItem.Checked)
                level = Level.Trace;
            if (_DebugMenuItem.Checked)
                level |= Level.Debug;
            if (_InfoMenuItem.Checked)
                level |= Level.Info;
            if (_WarnMenuItem.Checked)
                level |= Level.Warn;
            if (_ErrorMenuItem.Checked)
                level |= Level.Error;
            if (_FatalMenuItem.Checked)
                level |= Level.Fatal;
            _selfLevel = level;
            _viewModel.CurrentLevel = level;
        }

        /// <summary>
        /// 当点击清理按钮时发生，清除所有显示的日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolButton_Click(object sender, EventArgs e)
        {
            lock (_viewModel.LogInfos)
            {
                _viewModel.LogInfos.Clear();
            }
        }

        private void MaxViewCountTextBox_TextChanged(object sender, EventArgs e)
        {
            var text = _MaxViewCountTextBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                text = "1";
            }

            if (!uint.TryParse(text, out var v))
                v = uint.Parse(Regex.Replace(text, @"[^\d]*", ""));
            _MaxViewCountTextBox.Text = v.ToString();
            MaxDisplayCount = v;
        }

        /// <summary>
        /// 当集合发生变化时
        /// </summary>
        private void LogInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    var items = new List<ListViewItem>();
                    foreach (CustomLogInfo info in e.NewItems)
                    {
                        var viewItem = BuildItem(info);
                        items.Add(viewItem);
                    }

                    _ListView.ThreadSafeInvoke(() =>
                    {
                        _ListView.BeginUpdate();
                        //向ListView中添加日志
                        for (int i = items.Count - 1; i >= 0; i--)
                            _ListView.Items.Insert(0, items[i]);
                        _ListView.EndUpdate();
                    });

                    break;
                }

                case NotifyCollectionChangedAction.Remove:
                {
                    _ListView.ThreadSafeInvoke(() =>
                    {
                        _ListView.BeginUpdate();
                        //判断ListView的显示行数，如果大于MaxViewCount设置，移除最早的日志
                        while (_ListView.Items.Count >= _viewModel.MaxViewCount)
                            _ListView.Items.RemoveAt((int) _viewModel.MaxViewCount - 1);
                        _ListView.EndUpdate();
                    });
                    break;
                }

                case NotifyCollectionChangedAction.Reset:
                {
                    if (_viewModel.LogInfos.Count <= 0)
                        _ListView.ThreadSafeInvoke(() => { _ListView.Items.Clear(); });
                    break;
                }
            }
        }

        private ListViewItem BuildItem(CustomLogInfo info)
        {
            var viewItem = new ListViewItem();
            viewItem.Font = new Font("Microsoft YaHei Mono", 10F);
            viewItem.Tag = info.LogInfo;
            viewItem.Text = info.DateTime.ToString("HH:mm:ss.fff");
            viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem, info.LogInfo.FormattedMessage));
            viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem, LogUtil.ParseLoggerName(info.Source)));
            switch (info.LogLevel.Name)
            {
                case "Trace":
                    viewItem.ForeColor = _trackColor.Item1;
                    viewItem.BackColor = _trackColor.Item2;
                    break;
                case "Debug":
                    viewItem.ForeColor = _debugColor.Item1;
                    viewItem.BackColor = _debugColor.Item2;
                    break;
                case "Info":
                    viewItem.ForeColor = _infoColor.Item1;
                    viewItem.BackColor = _infoColor.Item2;
                    break;
                case "Warn":
                    viewItem.ForeColor = _warnColor.Item1;
                    viewItem.BackColor = _warnColor.Item2;
                    break;
                case "Error":
                    viewItem.ForeColor = _errorColor.Item1;
                    viewItem.BackColor = _errorColor.Item2;
                    break;
                case "Fatal":
                    viewItem.ForeColor = _fatalColor.Item1;
                    viewItem.BackColor = _fatalColor.Item2;
                    break;
            }

            return viewItem;
        }

        /// <summary>
        ///     设置Log显示的ListView中各列的宽度
        /// </summary>
        private void SetViewColumnSize()
        {
            if (_ListView.Columns.Count >= 2)
            {
                if (_ListView.Columns[0].Width != 0)
                    _ListView.Columns[0].Width = NotFlickerListView.DefaultTimeHeaderWidth;
                if (_ListView.Columns[2].Width != 0)
                    _ListView.Columns[2].Width = NotFlickerListView.DefaultLoggerNameHeader;
                _ListView.Columns[1].Width = Width - _ListView.Columns[0].Width - _ListView.Columns[2].Width - 22;
            }
        }

        #region 静态辅助方法-创建实例

        /// <summary>
        ///     初始化LogPanel到指定容器
        /// </summary>
        /// <param name="container"></param>
        public static LoggerListView AppendLogPanelToContainer(Panel container)
        {
            var logPanel = new LoggerListView();
            logPanel.Dock = DockStyle.Fill;
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        public static LoggerListView AppendLogPanelToContainer(Form container)
        {
            var logPanel = new LoggerListView();
            logPanel.Dock = DockStyle.Fill;
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        #endregion
    }
}