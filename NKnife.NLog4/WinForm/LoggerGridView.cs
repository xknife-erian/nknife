using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     日志显示面板控件，日志等级通过左上角的日志等级列表进行筛选
    /// </summary>
    public sealed partial class LoggerGridView : UserControl
    {
        private LoggerGridView()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Global.Culture);
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint,
                true);
            UpdateStyles();
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Tahoma", 8.25F);
            BuildLoggerInfoColumn();

            #region Menu Checked

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

            #endregion
        }

        public LoggerCollectionViewModel ViewModel { get; set; }

        /// <summary>
        ///     是否显示工具栏
        /// </summary>
        public bool ToolStripVisible
        {
            get { return _ToolStrip.Visible; }
            set { _ToolStrip.Visible = value; }
        }

        private void BuildLoggerInfoColumn()
        {
            var timeColumn = new DataGridViewTextBoxColumn();
            var levelColumn = new DataGridViewTextBoxColumn();
            var infoColumn = new DataGridViewTextBoxColumn();
            var sourceColumn = new DataGridViewTextBoxColumn();

            timeColumn.Name = "time";
            timeColumn.HeaderText = "时间";
            timeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            timeColumn.DataPropertyName = nameof(CustomLogInfo.DateTime);
            timeColumn.Width = 76;
            timeColumn.ReadOnly = true;
            timeColumn.DefaultCellStyle.Format = "HH:mm:ss fff";
            //---------
            levelColumn.Name = "level";
            levelColumn.HeaderText = string.Empty;
            levelColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            levelColumn.DataPropertyName = nameof(CustomLogInfo.LogLevel);
            levelColumn.Width = 40;
            levelColumn.ReadOnly = true;
            //---------
            infoColumn.Name = "info";
            infoColumn.HeaderText = "日志";
            infoColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            infoColumn.DataPropertyName = nameof(CustomLogInfo.LogInfo);
            infoColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            infoColumn.ReadOnly = true;
            //---------
            sourceColumn.Name = "source";
            sourceColumn.HeaderText = "日志源";
            sourceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            sourceColumn.DataPropertyName = nameof(CustomLogInfo.Source);
            sourceColumn.Width = 160;
            sourceColumn.ReadOnly = true;
            //---------
            _LogGridView.Columns.AddRange(timeColumn, levelColumn, infoColumn, sourceColumn);
            _LogGridView.CellFormatting += LogGridView_CellFormatting;
            _LogGridView.DataBindingComplete += LogGridView_DataBindingComplete;
            _LogGridView.CellDoubleClick += LogGridView_CellDoubleClick;
        }

        private void LogGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var info = ViewModel.LogInfos[rowIndex];
            if (info?.LogInfo != null)
            {
                LoggerInfoDetailForm.Show(info.LogInfo);
            }
        }

        private void LogGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_LogGridView.Rows.Count > 0)
                _LogGridView.Rows[0].Selected = true;
        }

        private void LogGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (_LogGridView.Columns[e.ColumnIndex].Name)
            {
                case "level":
                    switch (e.Value.ToString())
                    {
                        case nameof(Level.Trace):
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.CornflowerBlue;
                            break;
                        case nameof(Level.Debug):
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                            break;
                        case nameof(Level.Info):
                            break;
                        case nameof(Level.Warn):
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
                            break;
                        case nameof(Level.Error):
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                            break;
                        case nameof(Level.Fatal):
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                            _LogGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            break;
                    }
                    break;
                case "info":
                    var loginfo = (LogEventInfo) e.Value;
                    e.Value = loginfo.FormattedMessage;
                    break;
                case "source":
                    e.Value = LogUtil.ParseLoggerName(e.Value.ToString());
                    break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ViewModel = new LoggerCollectionViewModel();
            _LogGridView.DataSource = ViewModel.LogInfos;
        }

        public void SetDebugMode(bool isDebug)
        {
            if (isDebug)
            {
                ViewModel.CurrentLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = true;
                _DebugMenuItem.Checked = true;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
            else
            {
                ViewModel.CurrentLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = false;
                _DebugMenuItem.Checked = false;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
        }

        private void SetLevelGroupToolButton_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            item.Checked = !item.Checked;
            ViewModel.CurrentLevel = Level.None;
            if (_TraceMenuItem.Checked)
                ViewModel.CurrentLevel = Level.Trace;
            if (_DebugMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Debug;
            if (_InfoMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Info;
            if (_WarnMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Warn;
            if (_ErrorMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Error;
            if (_FatalMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Fatal;
        }

        private void ClearToolButton_Click(object sender, EventArgs e)
        {
            lock (ViewModel.LogInfos)
            {
                ViewModel.LogInfos.Clear();
            }
        }

        #region 静态辅助方法-创建实例

        /// <summary>
        ///     初始化LogPanel到指定容器
        /// </summary>
        /// <param name="container"></param>
        public static LoggerGridView AppendLogPanelToContainer(Panel container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        public static LoggerGridView AppendLogPanelToContainer(Form container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        #endregion

        #region 单件实例

        /// <summary>
        ///     获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static LoggerGridView Instance => _instance.Value;

        private static readonly Lazy<LoggerGridView> _instance = new Lazy<LoggerGridView>(() => new LoggerGridView());

        #endregion
    }
}