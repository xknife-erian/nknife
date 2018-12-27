using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NKnife.IoC;
using NKnife.ShareResources;
using NKnife.Utility;
using NLog;

namespace NKnife.NLog.WinForm
{
    /// <summary>日志详细信息展现窗体
    /// </summary>
    public sealed class LoggerInfoDetailForm : Form
    {
        public static void Show(LogEventInfo info)
        {
            var form = DI.Get<LoggerInfoDetailForm>();
            form.Size = new Size(600, 480);
            form.FillLogInfo(info);
            form.ShowDialog();
        }

        public LoggerInfoDetailForm()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Global.Culture);
            components = new Container();
            SuspendLayout();
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Tahoma", 8.25F);
            MinimumSize = new Size(320, 300);
            Size = new Size(640, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "日志详细信息";
            ShowIcon = false;
            ShowInTaskbar = false;
            ControlBox = false;
            ResumeLayout(false);
            InitializeComponent();
            _closeButton.Click += CloseButtonClick;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FillLogInfo(LogEventInfo info)
        {
            _levelTextBox.Text = info.Level.Name;
            _timeTextBox.Text = info.TimeStamp.ToString(CultureInfo.InvariantCulture);
            _logInfoTextBox.Text = info.FormattedMessage;
            _sourceTextBox.Text = info.LoggerName;
            switch (info.Level.Name)
            {
                case "Trace":
                case "Debug":
                case "Info":
                    _exInfoPage.Text = UtilityResource.GetString(StringResource.ResourceManager, "Exception_TabName_Simple");
                    break;
                case "Warn":
                case "Error":
                case "Fatal":
                default:
                    _exInfoPage.Text = UtilityResource.GetString(StringResource.ResourceManager, "Exception_TabName_Error");
                    break;
            }
            _logStackTracePropertyGrid.SelectedObject = info;
            _mainTabControl.SelectedIndex = 0;
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private readonly IContainer components;

        private Button _closeButton;
        private PropertyGrid _logStackTracePropertyGrid;
        private TextBox _levelTextBox;
        private TextBox _logInfoTextBox;
        private TextBox _sourceTextBox;
        private TextBox _timeTextBox;
        private Label _label1;
        private Label _label2;
        private Label _label3;
        private Label _label4;
        private TabControl _mainTabControl;
        private TabPage _mainPage;
        private TabPage _exInfoPage;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerInfoDetailForm));
            this._label1 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label3 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._levelTextBox = new System.Windows.Forms.TextBox();
            this._timeTextBox = new System.Windows.Forms.TextBox();
            this._sourceTextBox = new System.Windows.Forms.TextBox();
            this._logInfoTextBox = new System.Windows.Forms.TextBox();
            this._logStackTracePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._closeButton = new System.Windows.Forms.Button();
            this._mainTabControl = new System.Windows.Forms.TabControl();
            this._mainPage = new System.Windows.Forms.TabPage();
            this._exInfoPage = new System.Windows.Forms.TabPage();
            this._mainTabControl.SuspendLayout();
            this._mainPage.SuspendLayout();
            this._exInfoPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _label1
            // 
            resources.ApplyResources(this._label1, "_label1");
            this._label1.Name = "_label1";
            // 
            // _label2
            // 
            resources.ApplyResources(this._label2, "_label2");
            this._label2.Name = "_label2";
            // 
            // _label3
            // 
            resources.ApplyResources(this._label3, "_label3");
            this._label3.Name = "_label3";
            // 
            // _label4
            // 
            resources.ApplyResources(this._label4, "_label4");
            this._label4.Name = "_label4";
            // 
            // _levelTextBox
            // 
            resources.ApplyResources(this._levelTextBox, "_levelTextBox");
            this._levelTextBox.Name = "_levelTextBox";
            this._levelTextBox.ReadOnly = true;
            // 
            // _timeTextBox
            // 
            resources.ApplyResources(this._timeTextBox, "_timeTextBox");
            this._timeTextBox.Name = "_timeTextBox";
            this._timeTextBox.ReadOnly = true;
            // 
            // _sourceTextBox
            // 
            resources.ApplyResources(this._sourceTextBox, "_sourceTextBox");
            this._sourceTextBox.Name = "_sourceTextBox";
            this._sourceTextBox.ReadOnly = true;
            // 
            // _logInfoTextBox
            // 
            resources.ApplyResources(this._logInfoTextBox, "_logInfoTextBox");
            this._logInfoTextBox.Name = "_logInfoTextBox";
            this._logInfoTextBox.ReadOnly = true;
            // 
            // _logStackTracePropertyGrid
            // 
            resources.ApplyResources(this._logStackTracePropertyGrid, "_logStackTracePropertyGrid");
            this._logStackTracePropertyGrid.Name = "_logStackTracePropertyGrid";
            // 
            // _closeButton
            // 
            resources.ApplyResources(this._closeButton, "_closeButton");
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Name = "_closeButton";
            this._closeButton.UseVisualStyleBackColor = true;
            // 
            // _mainTabControl
            // 
            resources.ApplyResources(this._mainTabControl, "_mainTabControl");
            this._mainTabControl.Controls.Add(this._mainPage);
            this._mainTabControl.Controls.Add(this._exInfoPage);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            // 
            // _mainPage
            // 
            this._mainPage.Controls.Add(this._label4);
            this._mainPage.Controls.Add(this._label1);
            this._mainPage.Controls.Add(this._label2);
            this._mainPage.Controls.Add(this._logInfoTextBox);
            this._mainPage.Controls.Add(this._label3);
            this._mainPage.Controls.Add(this._sourceTextBox);
            this._mainPage.Controls.Add(this._levelTextBox);
            this._mainPage.Controls.Add(this._timeTextBox);
            resources.ApplyResources(this._mainPage, "_mainPage");
            this._mainPage.Name = "_mainPage";
            this._mainPage.UseVisualStyleBackColor = true;
            // 
            // _exInfoPage
            // 
            this._exInfoPage.Controls.Add(this._logStackTracePropertyGrid);
            resources.ApplyResources(this._exInfoPage, "_exInfoPage");
            this._exInfoPage.Name = "_exInfoPage";
            this._exInfoPage.UseVisualStyleBackColor = true;
            // 
            // LoggerInfoDetailForm
            // 
            this.AcceptButton = this._closeButton;
            this.CancelButton = this._closeButton;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this._mainTabControl);
            this.Controls.Add(this._closeButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoggerInfoDetailForm";
            this._mainTabControl.ResumeLayout(false);
            this._mainPage.ResumeLayout(false);
            this._mainPage.PerformLayout();
            this._exInfoPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}