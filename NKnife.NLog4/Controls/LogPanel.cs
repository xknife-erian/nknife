using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace NKnife.NLog.Controls
{
    /// <summary>
    ///     日志显示面板控件，日志等级通过左上角的日志等级列表进行筛选
    /// </summary>
    public sealed partial class LogPanel : UserControl
    {
        #region 单件实例

        /// <summary>
        ///     获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static LogPanel Instance
        {
            get { return _instance.Value; }
        }

        private static readonly Lazy<LogPanel> _instance = new Lazy<LogPanel>(() => new LogPanel());

        #endregion

        #region 辅助方法

        /// <summary>
        ///     初始化LogPanel到指定容器
        /// </summary>
        /// <param name="container"></param>
        public static void AppendLogPanelToContainer(Panel container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
        }

        public static void AppendLogPanelToContainer(Form container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.HeaderStyle = ColumnHeaderStyle.Clickable;
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
        }

        #endregion

        private LogPanel()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Global.Culture);
            SetStyle
                (
                    ControlStyles.DoubleBuffer |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint,
                    true
                );
            UpdateStyles();
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Tahoma", 8.25F);

#if DEBUG
            _TraceMenuItem.Checked = true;
            _DebugMenuItem.Checked = true;
#else
            _TraceMenuItem.Checked = false;
            _DebugMenuItem.Checked = false;
#endif
            _InfoMenuItem.Checked = true;
            _WarnMenuItem.Checked = true;
            _ErrorMenuItem.Checked = true;
            _FatalMenuItem.Checked = true;
        }

        /// <summary>
        ///     是否显示工具栏
        /// </summary>
        public bool ToolStripVisible
        {
            get { return _ToolStrip.Visible; }
            set { _ToolStrip.Visible = value; }
        }

        /// <summary>
        ///     设置View中需隐藏的列
        /// </summary>
        public void SetColumnVisible(params ushort[] columnIndexs)
        {
            foreach (var index in columnIndexs)
            {
                if (_LogView.Columns.Count > index)
                    _LogView.Columns[index].Width = 0;
            }
        }

        /// <summary>
        ///     日志View的列标头的样式
        /// </summary>
        public ColumnHeaderStyle HeaderStyle
        {
            get { return _LogView.HeaderStyle; }
            set { _LogView.HeaderStyle = value; }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            try
            {
                base.OnSizeChanged(e);
                SetViewColumnSize();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        ///     设置Log显示的ListView中各列的宽度
        /// </summary>
        private void SetViewColumnSize()
        {
            if (_LogView.Columns.Count >= 2)
            {
                if (_LogView.Columns[0].Width != 0)
                    _LogView.Columns[0].Width = 80;
                if (_LogView.Columns[2].Width != 0)
                    _LogView.Columns[2].Width = 200;
                _LogView.Columns[1].Width = Width - _LogView.Columns[0].Width - _LogView.Columns[2].Width - 22;
            }
        }

        /// <summary>
        ///     增加一条新的日志
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        internal void AddLog(LogEventInfo logEvent)
        {
            var level = GetTopLevel(logEvent.Level);
            if (_CurrLevel.HasFlag(level))
            {
                lock (_LogView)
                {
                    _LogView.AddLog(logEvent);
                }
            }
        }

        private Level GetTopLevel(LogLevel logLevel)
        {
            Level result;
            if (!Enum.TryParse(logLevel.Name, out result))
            {
                result = Level.None;
            }
            return result;
        }

#if DEBUG
        private Level _CurrLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
#else
        private Level _CurrLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
#endif

        [Flags]
        public enum Level : byte
        {
            None = 0,
            Trace = 1,
            Debug = 2,
            Info = 4,
            Warn = 8,
            Error = 16,
            Fatal = 32
        }

        public void SetDebugMode(bool isDebug)
        {
            if (isDebug)
            {
                _CurrLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = true;
                _DebugMenuItem.Checked = true;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
            else
            {
                _CurrLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = false;
                _DebugMenuItem.Checked = false;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
        }

        private void LevelToolButton_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            item.Checked = !item.Checked;
            _CurrLevel = Level.None;
            if (_TraceMenuItem.Checked)
                _CurrLevel = Level.Trace;
            if (_DebugMenuItem.Checked)
                _CurrLevel = _CurrLevel | Level.Debug;
            if (_InfoMenuItem.Checked)
                _CurrLevel = _CurrLevel | Level.Info;
            if (_WarnMenuItem.Checked)
                _CurrLevel = _CurrLevel | Level.Warn;
            if (_ErrorMenuItem.Checked)
                _CurrLevel = _CurrLevel | Level.Error;
            if (_FatalMenuItem.Checked)
                _CurrLevel = _CurrLevel | Level.Fatal;
        }

        private void ClearToolButton_Click(object sender, EventArgs e)
        {
            lock (_LogView)
            {
                _LogView.Items.Clear();
                _LogView.Update();
            }
        }
    }
}