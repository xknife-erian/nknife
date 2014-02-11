﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Ioc;
using NLog;

namespace NKnife.Logging.Base
{
    /// <summary>日志详细信息展现窗体
    /// </summary>
    public sealed class LogDetailForm : Form
    {
        public static void Show(LogEventInfo info)
        {
            var form = DI.Get<LogDetailForm>();
            form.Size = new Size(600, 480);
            form.FillLogInfo(info);
            form.ShowDialog();
        }

        public LogDetailForm()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Global.Culture);
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
            _CloseButton.Click += CloseButtonClick;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FillLogInfo(LogEventInfo info)
        {
            _LevelTextBox.Text = info.Level.Name;
            _TimeTextBox.Text = info.TimeStamp.ToString();
            _LogInfoTextBox.Text = info.FormattedMessage;
            _SourceTextBox.Text = info.LoggerName;
            switch (info.Level.Name)
            {
                case "Trace":
                case "Debug":
                case "Info":
                    _ExInfoPage.Text = "详细信息";
                    break;
                case "Warn":
                case "Error":
                case "Fatal":
                default:
                    _ExInfoPage.Text = "异常详细信息";
                    break;
            }
            _LogStackTracePropertyGrid.SelectedObject = info;
            _MainTabControl.SelectedIndex = 0;
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private readonly IContainer components;

        private Button _CloseButton;
        private PropertyGrid _LogStackTracePropertyGrid;
        private TextBox _LevelTextBox;
        private TextBox _LogInfoTextBox;
        private TextBox _SourceTextBox;
        private TextBox _TimeTextBox;
        private Label _Label1;
        private Label _Label2;
        private Label _Label3;
        private Label _Label4;
        private TabControl _MainTabControl;
        private TabPage _MainPage;
        private TabPage _ExInfoPage;

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
            _Label1 = new Label();
            _Label2 = new Label();
            _Label3 = new Label();
            _Label4 = new Label();
            _LevelTextBox = new TextBox();
            _TimeTextBox = new TextBox();
            _SourceTextBox = new TextBox();
            _LogInfoTextBox = new TextBox();
            _LogStackTracePropertyGrid = new PropertyGrid();
            _CloseButton = new Button();
            _MainTabControl = new TabControl();
            _MainPage = new TabPage();
            _ExInfoPage = new TabPage();
            _MainTabControl.SuspendLayout();
            _MainPage.SuspendLayout();
            _ExInfoPage.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            _Label1.AutoSize = true;
            _Label1.Location = new Point(24, 18);
            _Label1.Size = new Size(59, 12);
            _Label1.TabIndex = 0;
            _Label1.Text = "日志级别:";
            // 
            // label2
            // 
            _Label2.AutoSize = true;
            _Label2.Location = new Point(24, 43);
            _Label2.Size = new Size(59, 12);
            _Label2.TabIndex = 1;
            _Label2.Text = "发生时间:";
            // 
            // label3
            // 
            _Label3.AutoSize = true;
            _Label3.Location = new Point(36, 66);
            _Label3.Size = new Size(47, 12);
            _Label3.TabIndex = 2;
            _Label3.Text = "日志源:";
            // 
            // label4
            // 
            _Label4.AutoSize = true;
            _Label4.Location = new Point(24, 91);
            _Label4.Size = new Size(59, 12);
            _Label4.TabIndex = 3;
            _Label4.Text = "日志信息:";
            // 
            // _LevelTextBox
            // 
            _LevelTextBox.Anchor = ((((AnchorStyles.Top | AnchorStyles.Left)| AnchorStyles.Right)));
            _LevelTextBox.Location = new Point(89, 13);
            _LevelTextBox.ReadOnly = true;
            _LevelTextBox.Size = new Size(364, 21);
            _LevelTextBox.TabIndex = 5;
            // 
            // _TimeTextBox
            // 
            _TimeTextBox.Anchor = ((((AnchorStyles.Top | AnchorStyles.Left)
                                     | AnchorStyles.Right)));
            _TimeTextBox.Location = new Point(89, 38);
            _TimeTextBox.ReadOnly = true;
            _TimeTextBox.Size = new Size(364, 21);
            _TimeTextBox.TabIndex = 6;
            // 
            // _SourceTextBox
            // 
            _SourceTextBox.Anchor = ((((AnchorStyles.Top | AnchorStyles.Left)
                                       | AnchorStyles.Right)));
            _SourceTextBox.Location = new Point(89, 63);
            _SourceTextBox.Name = "_SourceTextBox";
            _SourceTextBox.ReadOnly = true;
            _SourceTextBox.Size = new Size(364, 21);
            _SourceTextBox.TabIndex = 7;
            // 
            // _LogInfoTextBox
            // 
            _LogInfoTextBox.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
                                         | AnchorStyles.Left)
                                        | AnchorStyles.Right)));
            _LogInfoTextBox.Location = new Point(89, 88);
            _LogInfoTextBox.Multiline = true;
            _LogInfoTextBox.Name = "_LogInfoTextBox";
            _LogInfoTextBox.ReadOnly = true;
            _LogInfoTextBox.ScrollBars = ScrollBars.Vertical;
            _LogInfoTextBox.Size = new Size(364, 102);
            _LogInfoTextBox.TabIndex = 8;
            // 
            // _LogStackTracePropertyGrid
            // 
            _LogStackTracePropertyGrid.Dock = DockStyle.Fill;
            _LogStackTracePropertyGrid.HelpVisible = false;
            // 
            // _CloseButton
            // 
            _CloseButton.Anchor = (((AnchorStyles.Bottom | AnchorStyles.Right)));
            _CloseButton.Location = new Point(359, 251);
            _CloseButton.Name = "_CloseButton";
            _CloseButton.Size = new Size(132, 27);
            _CloseButton.TabIndex = 10;
            _CloseButton.Text = "关闭(&X)";
            _CloseButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            _MainTabControl.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
                                     | AnchorStyles.Left)
                                    | AnchorStyles.Right)));
            _MainTabControl.Controls.Add(_MainPage);
            _MainTabControl.Controls.Add(_ExInfoPage);
            _MainTabControl.Location = new Point(12, 12);
            _MainTabControl.Name = "tabControl1";
            _MainTabControl.SelectedIndex = 0;
            _MainTabControl.Size = new Size(479, 237);
            _MainTabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            _MainPage.Controls.Add(_Label4);
            _MainPage.Controls.Add(_Label1);
            _MainPage.Controls.Add(_Label2);
            _MainPage.Controls.Add(_LogInfoTextBox);
            _MainPage.Controls.Add(_Label3);
            _MainPage.Controls.Add(_SourceTextBox);
            _MainPage.Controls.Add(_LevelTextBox);
            _MainPage.Controls.Add(_TimeTextBox);
            _MainPage.Location = new Point(4, 21);
            _MainPage.Name = "tabPage1";
            _MainPage.Padding = new Padding(3);
            _MainPage.Size = new Size(471, 212);
            _MainPage.TabIndex = 0;
            _MainPage.Text = "信息";
            _MainPage.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            _ExInfoPage.Controls.Add(_LogStackTracePropertyGrid);
            _ExInfoPage.Location = new Point(4, 21);
            _ExInfoPage.Name = "tabPage2";
            _ExInfoPage.Padding = new Padding(3);
            _ExInfoPage.Size = new Size(471, 212);
            _ExInfoPage.TabIndex = 1;
            _ExInfoPage.Text = "异常";
            _ExInfoPage.UseVisualStyleBackColor = true;
            // 
            // LogInfoForm
            // 
            AcceptButton = _CloseButton;
            AutoScaleDimensions = new SizeF(6F, 12F);
            CancelButton = _CloseButton;
            ClientSize = new Size(503, 290);
            Controls.Add(_MainTabControl);
            Controls.Add(_CloseButton);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LogDetailForm";
            _MainTabControl.ResumeLayout(false);
            _MainPage.ResumeLayout(false);
            _MainPage.PerformLayout();
            _ExInfoPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}