using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NKnife.Win.Forms;

namespace NKnife.SerialBox
{
    partial class Workbench
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
            this._Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._ProtocolTabControl = new System.Windows.Forms.TabControl();
            this.singleTab = new System.Windows.Forms.TabPage();
            this.tbxSendPeriod = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxSendData = new System.Windows.Forms.TextBox();
            this.ckbSendNewLine = new System.Windows.Forms.CheckBox();
            this.ckbHexSend = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbTimeSend = new System.Windows.Forms.CheckBox();
            this.multiTab = new System.Windows.Forms.TabPage();
            this.panelMultiSend1 = new System.Windows.Forms.Panel();
            this.btnMultiSend2 = new System.Windows.Forms.Button();
            this.btnMultiSend1 = new System.Windows.Forms.Button();
            this.tbxMultiSend2 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend4 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend3 = new System.Windows.Forms.TextBox();
            this.btnMultiSend3 = new System.Windows.Forms.Button();
            this.btnMultiSend4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxMultiSend1 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend1 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend10 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend2 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend9 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend3 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend9 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend4 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend10 = new System.Windows.Forms.TextBox();
            this.btnMultiSend5 = new System.Windows.Forms.Button();
            this.btnMultiSend10 = new System.Windows.Forms.Button();
            this.btnMultiSend6 = new System.Windows.Forms.Button();
            this.btnMultiSend9 = new System.Windows.Forms.Button();
            this.tbxMultiSend6 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend8 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend8 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend7 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend7 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend6 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend7 = new System.Windows.Forms.Button();
            this.ckbMultiSend5 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend8 = new System.Windows.Forms.Button();
            this.tbxMultiSend5 = new System.Windows.Forms.TextBox();
            this.panelMultiSend2 = new System.Windows.Forms.Panel();
            this.btnMultiSend12 = new System.Windows.Forms.Button();
            this.btnMultiSend11 = new System.Windows.Forms.Button();
            this.tbxMultiSend12 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend14 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend13 = new System.Windows.Forms.TextBox();
            this.btnMultiSend13 = new System.Windows.Forms.Button();
            this.btnMultiSend14 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxMultiSend11 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend11 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend20 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend12 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend19 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend13 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend19 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend14 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend20 = new System.Windows.Forms.TextBox();
            this.btnMultiSend15 = new System.Windows.Forms.Button();
            this.btnMultiSend20 = new System.Windows.Forms.Button();
            this.btnMultiSend16 = new System.Windows.Forms.Button();
            this.btnMultiSend19 = new System.Windows.Forms.Button();
            this.tbxMultiSend16 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend18 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend18 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend17 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend17 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend16 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend17 = new System.Windows.Forms.Button();
            this.ckbMultiSend15 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend18 = new System.Windows.Forms.Button();
            this.tbxMultiSend15 = new System.Windows.Forms.TextBox();
            this.panelMultiSend3 = new System.Windows.Forms.Panel();
            this.btnMultiSend22 = new System.Windows.Forms.Button();
            this.btnMultiSend21 = new System.Windows.Forms.Button();
            this.tbxMultiSend22 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend24 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend23 = new System.Windows.Forms.TextBox();
            this.btnMultiSend23 = new System.Windows.Forms.Button();
            this.btnMultiSend24 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxMultiSend21 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend21 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend30 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend22 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend29 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend23 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend29 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend24 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend30 = new System.Windows.Forms.TextBox();
            this.btnMultiSend25 = new System.Windows.Forms.Button();
            this.btnMultiSend30 = new System.Windows.Forms.Button();
            this.btnMultiSend26 = new System.Windows.Forms.Button();
            this.btnMultiSend29 = new System.Windows.Forms.Button();
            this.tbxMultiSend26 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend28 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend28 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend27 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend27 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend26 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend27 = new System.Windows.Forms.Button();
            this.ckbMultiSend25 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend28 = new System.Windows.Forms.Button();
            this.tbxMultiSend25 = new System.Windows.Forms.TextBox();
            this.panelMultiSend4 = new System.Windows.Forms.Panel();
            this.btnMultiSend32 = new System.Windows.Forms.Button();
            this.btnMultiSend31 = new System.Windows.Forms.Button();
            this.tbxMultiSend32 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend34 = new System.Windows.Forms.TextBox();
            this.tbxMultiSend33 = new System.Windows.Forms.TextBox();
            this.btnMultiSend33 = new System.Windows.Forms.Button();
            this.btnMultiSend34 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxMultiSend31 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend31 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend40 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend32 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend39 = new System.Windows.Forms.CheckBox();
            this.ckbMultiSend33 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend39 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend34 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend40 = new System.Windows.Forms.TextBox();
            this.btnMultiSend35 = new System.Windows.Forms.Button();
            this.btnMultiSend40 = new System.Windows.Forms.Button();
            this.btnMultiSend36 = new System.Windows.Forms.Button();
            this.btnMultiSend39 = new System.Windows.Forms.Button();
            this.tbxMultiSend36 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend38 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend38 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend37 = new System.Windows.Forms.CheckBox();
            this.tbxMultiSend37 = new System.Windows.Forms.TextBox();
            this.ckbMultiSend36 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend37 = new System.Windows.Forms.Button();
            this.ckbMultiSend35 = new System.Windows.Forms.CheckBox();
            this.btnMultiSend38 = new System.Windows.Forms.Button();
            this.tbxMultiSend35 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnRemarkMultiSend = new System.Windows.Forms.Button();
            this.btnMultiEndPage = new System.Windows.Forms.Button();
            this.btnMultiNextPage = new System.Windows.Forms.Button();
            this.btnMultiLastPage = new System.Windows.Forms.Button();
            this.btnMultiFirstPage = new System.Windows.Forms.Button();
            this.ckbMultiSendNewLine = new System.Windows.Forms.CheckBox();
            this.ckbRelateKeyBoard = new System.Windows.Forms.CheckBox();
            this.ckbMultiHexSend = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxMutilSendPeriod = new System.Windows.Forms.TextBox();
            this.ckbMultiAutoSend = new System.Windows.Forms.CheckBox();
            this._SendFileTabPage = new System.Windows.Forms.TabPage();
            this._FileContentTextBox = new System.Windows.Forms.TextBox();
            this.tbxSendFile = new System.Windows.Forms.TextBox();
            this.btnStopSendFile = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnOpenSendFile = new System.Windows.Forms.Button();
            this._MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this._MessageTabControl = new System.Windows.Forms.TabControl();
            this._MessageTabPage = new System.Windows.Forms.TabPage();
            this._TextPanel = new System.Windows.Forms.Panel();
            this.tbxRecvData = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCheckBox1 = new NKnife.Win.Forms.ToolStripCheckBox();
            this.toolStripCheckBox2 = new NKnife.Win.Forms.ToolStripCheckBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this._ChartTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStripCom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblSendCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLblRevCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SerialInfoStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._OpenSerialPortStripButton = new System.Windows.Forms.ToolStripButton();
            this._CloseSerialPortStripButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this._Chart)).BeginInit();
            this._ProtocolTabControl.SuspendLayout();
            this.singleTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbxSendPeriod)).BeginInit();
            this.multiTab.SuspendLayout();
            this.panelMultiSend1.SuspendLayout();
            this.panelMultiSend2.SuspendLayout();
            this.panelMultiSend3.SuspendLayout();
            this.panelMultiSend4.SuspendLayout();
            this._SendFileTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).BeginInit();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.Panel2.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            this._MessageTabControl.SuspendLayout();
            this._MessageTabPage.SuspendLayout();
            this._TextPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this._ChartTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this.statusStripCom.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Chart
            // 
            this._Chart.BackColor = System.Drawing.Color.LavenderBlush;
            this._Chart.BorderlineColor = System.Drawing.SystemColors.ControlDark;
            this._Chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this._Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Chart.Location = new System.Drawing.Point(0, 0);
            this._Chart.Name = "_Chart";
            this._Chart.Size = new System.Drawing.Size(841, 651);
            this._Chart.TabIndex = 0;
            // 
            // _ProtocolTabControl
            // 
            this._ProtocolTabControl.Controls.Add(this.singleTab);
            this._ProtocolTabControl.Controls.Add(this.multiTab);
            this._ProtocolTabControl.Controls.Add(this._SendFileTabPage);
            this._ProtocolTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ProtocolTabControl.ItemSize = new System.Drawing.Size(60, 30);
            this._ProtocolTabControl.Location = new System.Drawing.Point(0, 0);
            this._ProtocolTabControl.Name = "_ProtocolTabControl";
            this._ProtocolTabControl.Padding = new System.Drawing.Point(8, 3);
            this._ProtocolTabControl.SelectedIndex = 0;
            this._ProtocolTabControl.Size = new System.Drawing.Size(433, 896);
            this._ProtocolTabControl.TabIndex = 40;
            // 
            // singleTab
            // 
            this.singleTab.BackColor = System.Drawing.Color.Transparent;
            this.singleTab.Controls.Add(this.groupBox1);
            this.singleTab.Controls.Add(this.btnClearSend);
            this.singleTab.Controls.Add(this.btnSend);
            this.singleTab.Controls.Add(this.tbxSendData);
            this.singleTab.Controls.Add(this.ckbSendNewLine);
            this.singleTab.Controls.Add(this.ckbHexSend);
            this.singleTab.Controls.Add(this.label7);
            this.singleTab.Controls.Add(this.label6);
            this.singleTab.Location = new System.Drawing.Point(4, 34);
            this.singleTab.Name = "singleTab";
            this.singleTab.Padding = new System.Windows.Forms.Padding(3);
            this.singleTab.Size = new System.Drawing.Size(425, 858);
            this.singleTab.TabIndex = 0;
            this.singleTab.Text = "单条发送";
            // 
            // tbxSendPeriod
            // 
            this.tbxSendPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSendPeriod.Location = new System.Drawing.Point(6, 27);
            this.tbxSendPeriod.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.tbxSendPeriod.Name = "tbxSendPeriod";
            this.tbxSendPeriod.Size = new System.Drawing.Size(65, 23);
            this.tbxSendPeriod.TabIndex = 47;
            this.tbxSendPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxSendPeriod.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(71, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 17);
            this.label16.TabIndex = 46;
            this.label16.Text = "ms";
            // 
            // btnClearSend
            // 
            this.btnClearSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSend.Location = new System.Drawing.Point(321, 202);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(97, 48);
            this.btnClearSend.TabIndex = 43;
            this.btnClearSend.Text = "清除";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(321, 151);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(97, 48);
            this.btnSend.TabIndex = 39;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.SendSingleButton_Click);
            // 
            // tbxSendData
            // 
            this.tbxSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSendData.Location = new System.Drawing.Point(5, 5);
            this.tbxSendData.Multiline = true;
            this.tbxSendData.Name = "tbxSendData";
            this.tbxSendData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxSendData.Size = new System.Drawing.Size(310, 850);
            this.tbxSendData.TabIndex = 38;
            // 
            // ckbSendNewLine
            // 
            this.ckbSendNewLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbSendNewLine.AutoSize = true;
            this.ckbSendNewLine.Checked = true;
            this.ckbSendNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSendNewLine.Location = new System.Drawing.Point(321, 31);
            this.ckbSendNewLine.Name = "ckbSendNewLine";
            this.ckbSendNewLine.Size = new System.Drawing.Size(75, 21);
            this.ckbSendNewLine.TabIndex = 37;
            this.ckbSendNewLine.Text = "发送新行";
            this.ckbSendNewLine.UseVisualStyleBackColor = true;
            // 
            // ckbHexSend
            // 
            this.ckbHexSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbHexSend.AutoSize = true;
            this.ckbHexSend.Location = new System.Drawing.Point(321, 7);
            this.ckbHexSend.Name = "ckbHexSend";
            this.ckbHexSend.Size = new System.Drawing.Size(89, 21);
            this.ckbHexSend.TabIndex = 36;
            this.ckbHexSend.Text = "16进制发送";
            this.ckbHexSend.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.Location = new System.Drawing.Point(94, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 14);
            this.label7.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label6.Location = new System.Drawing.Point(174, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 14);
            this.label6.TabIndex = 34;
            // 
            // ckbTimeSend
            // 
            this.ckbTimeSend.AutoSize = true;
            this.ckbTimeSend.Location = new System.Drawing.Point(7, 54);
            this.ckbTimeSend.Name = "ckbTimeSend";
            this.ckbTimeSend.Size = new System.Drawing.Size(75, 21);
            this.ckbTimeSend.TabIndex = 32;
            this.ckbTimeSend.Text = "定时发送";
            this.ckbTimeSend.UseVisualStyleBackColor = true;
            // 
            // multiTab
            // 
            this.multiTab.BackColor = System.Drawing.Color.Transparent;
            this.multiTab.Controls.Add(this.panelMultiSend1);
            this.multiTab.Controls.Add(this.panelMultiSend2);
            this.multiTab.Controls.Add(this.panelMultiSend3);
            this.multiTab.Controls.Add(this.panelMultiSend4);
            this.multiTab.Controls.Add(this.label21);
            this.multiTab.Controls.Add(this.btnRemarkMultiSend);
            this.multiTab.Controls.Add(this.btnMultiEndPage);
            this.multiTab.Controls.Add(this.btnMultiNextPage);
            this.multiTab.Controls.Add(this.btnMultiLastPage);
            this.multiTab.Controls.Add(this.btnMultiFirstPage);
            this.multiTab.Controls.Add(this.ckbMultiSendNewLine);
            this.multiTab.Controls.Add(this.ckbRelateKeyBoard);
            this.multiTab.Controls.Add(this.ckbMultiHexSend);
            this.multiTab.Controls.Add(this.label14);
            this.multiTab.Controls.Add(this.tbxMutilSendPeriod);
            this.multiTab.Controls.Add(this.ckbMultiAutoSend);
            this.multiTab.Location = new System.Drawing.Point(4, 34);
            this.multiTab.Name = "multiTab";
            this.multiTab.Padding = new System.Windows.Forms.Padding(3);
            this.multiTab.Size = new System.Drawing.Size(425, 858);
            this.multiTab.TabIndex = 1;
            this.multiTab.Text = "多条发送";
            // 
            // panelMultiSend1
            // 
            this.panelMultiSend1.Controls.Add(this.btnMultiSend2);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend1);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend2);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend4);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend3);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend3);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend4);
            this.panelMultiSend1.Controls.Add(this.label13);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend1);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend1);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend10);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend2);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend9);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend3);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend9);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend4);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend10);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend5);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend10);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend6);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend9);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend6);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend8);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend8);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend7);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend7);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend6);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend7);
            this.panelMultiSend1.Controls.Add(this.ckbMultiSend5);
            this.panelMultiSend1.Controls.Add(this.btnMultiSend8);
            this.panelMultiSend1.Controls.Add(this.tbxMultiSend5);
            this.panelMultiSend1.Location = new System.Drawing.Point(74, 468);
            this.panelMultiSend1.Name = "panelMultiSend1";
            this.panelMultiSend1.Size = new System.Drawing.Size(293, 265);
            this.panelMultiSend1.TabIndex = 70;
            // 
            // btnMultiSend2
            // 
            this.btnMultiSend2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend2.Location = new System.Drawing.Point(253, 25);
            this.btnMultiSend2.Name = "btnMultiSend2";
            this.btnMultiSend2.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend2.TabIndex = 6;
            this.btnMultiSend2.Text = "1";
            this.btnMultiSend2.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend1
            // 
            this.btnMultiSend1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend1.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend1.Name = "btnMultiSend1";
            this.btnMultiSend1.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend1.TabIndex = 2;
            this.btnMultiSend1.Text = "0";
            this.btnMultiSend1.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend2
            // 
            this.tbxMultiSend2.Location = new System.Drawing.Point(24, 25);
            this.tbxMultiSend2.Name = "tbxMultiSend2";
            this.tbxMultiSend2.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend2.TabIndex = 7;
            this.tbxMultiSend2.Text = "1";
            // 
            // tbxMultiSend4
            // 
            this.tbxMultiSend4.Location = new System.Drawing.Point(24, 75);
            this.tbxMultiSend4.Name = "tbxMultiSend4";
            this.tbxMultiSend4.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend4.TabIndex = 8;
            this.tbxMultiSend4.Text = "3";
            // 
            // tbxMultiSend3
            // 
            this.tbxMultiSend3.Location = new System.Drawing.Point(24, 50);
            this.tbxMultiSend3.Name = "tbxMultiSend3";
            this.tbxMultiSend3.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend3.TabIndex = 9;
            this.tbxMultiSend3.Text = "2";
            // 
            // btnMultiSend3
            // 
            this.btnMultiSend3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend3.Location = new System.Drawing.Point(253, 49);
            this.btnMultiSend3.Name = "btnMultiSend3";
            this.btnMultiSend3.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend3.TabIndex = 10;
            this.btnMultiSend3.Text = "2";
            this.btnMultiSend3.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend4
            // 
            this.btnMultiSend4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend4.Location = new System.Drawing.Point(253, 73);
            this.btnMultiSend4.Name = "btnMultiSend4";
            this.btnMultiSend4.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend4.TabIndex = 11;
            this.btnMultiSend4.Text = "3";
            this.btnMultiSend4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(588, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 17);
            this.label13.TabIndex = 27;
            this.label13.Text = "间隔：";
            // 
            // tbxMultiSend1
            // 
            this.tbxMultiSend1.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend1.Name = "tbxMultiSend1";
            this.tbxMultiSend1.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend1.TabIndex = 30;
            this.tbxMultiSend1.Text = "0";
            this.tbxMultiSend1.WordWrap = false;
            // 
            // ckbMultiSend1
            // 
            this.ckbMultiSend1.AutoSize = true;
            this.ckbMultiSend1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend1.Location = new System.Drawing.Point(6, 9);
            this.ckbMultiSend1.Name = "ckbMultiSend1";
            this.ckbMultiSend1.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend1.TabIndex = 31;
            this.ckbMultiSend1.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend10
            // 
            this.ckbMultiSend10.AutoSize = true;
            this.ckbMultiSend10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend10.Location = new System.Drawing.Point(5, 232);
            this.ckbMultiSend10.Name = "ckbMultiSend10";
            this.ckbMultiSend10.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend10.TabIndex = 56;
            this.ckbMultiSend10.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend2
            // 
            this.ckbMultiSend2.AutoSize = true;
            this.ckbMultiSend2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend2.Location = new System.Drawing.Point(6, 32);
            this.ckbMultiSend2.Name = "ckbMultiSend2";
            this.ckbMultiSend2.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend2.TabIndex = 32;
            this.ckbMultiSend2.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend9
            // 
            this.ckbMultiSend9.AutoSize = true;
            this.ckbMultiSend9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend9.Location = new System.Drawing.Point(5, 209);
            this.ckbMultiSend9.Name = "ckbMultiSend9";
            this.ckbMultiSend9.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend9.TabIndex = 55;
            this.ckbMultiSend9.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend3
            // 
            this.ckbMultiSend3.AutoSize = true;
            this.ckbMultiSend3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend3.Location = new System.Drawing.Point(6, 55);
            this.ckbMultiSend3.Name = "ckbMultiSend3";
            this.ckbMultiSend3.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend3.TabIndex = 33;
            this.ckbMultiSend3.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend9
            // 
            this.tbxMultiSend9.Location = new System.Drawing.Point(24, 200);
            this.tbxMultiSend9.Name = "tbxMultiSend9";
            this.tbxMultiSend9.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend9.TabIndex = 54;
            this.tbxMultiSend9.Text = "8";
            // 
            // ckbMultiSend4
            // 
            this.ckbMultiSend4.AutoSize = true;
            this.ckbMultiSend4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend4.Location = new System.Drawing.Point(6, 78);
            this.ckbMultiSend4.Name = "ckbMultiSend4";
            this.ckbMultiSend4.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend4.TabIndex = 34;
            this.ckbMultiSend4.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend10
            // 
            this.tbxMultiSend10.Location = new System.Drawing.Point(24, 225);
            this.tbxMultiSend10.Name = "tbxMultiSend10";
            this.tbxMultiSend10.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend10.TabIndex = 49;
            this.tbxMultiSend10.Text = "9";
            // 
            // btnMultiSend5
            // 
            this.btnMultiSend5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend5.Location = new System.Drawing.Point(253, 97);
            this.btnMultiSend5.Name = "btnMultiSend5";
            this.btnMultiSend5.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend5.TabIndex = 35;
            this.btnMultiSend5.Text = "4";
            this.btnMultiSend5.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend10
            // 
            this.btnMultiSend10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend10.Location = new System.Drawing.Point(252, 217);
            this.btnMultiSend10.Name = "btnMultiSend10";
            this.btnMultiSend10.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend10.TabIndex = 48;
            this.btnMultiSend10.Text = "9";
            this.btnMultiSend10.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend6
            // 
            this.btnMultiSend6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend6.Location = new System.Drawing.Point(252, 121);
            this.btnMultiSend6.Name = "btnMultiSend6";
            this.btnMultiSend6.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend6.TabIndex = 36;
            this.btnMultiSend6.Text = "5";
            this.btnMultiSend6.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend9
            // 
            this.btnMultiSend9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend9.Location = new System.Drawing.Point(252, 193);
            this.btnMultiSend9.Name = "btnMultiSend9";
            this.btnMultiSend9.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend9.TabIndex = 47;
            this.btnMultiSend9.Text = "8";
            this.btnMultiSend9.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend6
            // 
            this.tbxMultiSend6.Location = new System.Drawing.Point(24, 125);
            this.tbxMultiSend6.Name = "tbxMultiSend6";
            this.tbxMultiSend6.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend6.TabIndex = 37;
            this.tbxMultiSend6.Text = "5";
            // 
            // ckbMultiSend8
            // 
            this.ckbMultiSend8.AutoSize = true;
            this.ckbMultiSend8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend8.Location = new System.Drawing.Point(5, 186);
            this.ckbMultiSend8.Name = "ckbMultiSend8";
            this.ckbMultiSend8.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend8.TabIndex = 46;
            this.ckbMultiSend8.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend8
            // 
            this.tbxMultiSend8.Location = new System.Drawing.Point(24, 175);
            this.tbxMultiSend8.Name = "tbxMultiSend8";
            this.tbxMultiSend8.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend8.TabIndex = 38;
            this.tbxMultiSend8.Text = "7";
            // 
            // ckbMultiSend7
            // 
            this.ckbMultiSend7.AutoSize = true;
            this.ckbMultiSend7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend7.Location = new System.Drawing.Point(5, 163);
            this.ckbMultiSend7.Name = "ckbMultiSend7";
            this.ckbMultiSend7.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend7.TabIndex = 45;
            this.ckbMultiSend7.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend7
            // 
            this.tbxMultiSend7.Location = new System.Drawing.Point(24, 150);
            this.tbxMultiSend7.Name = "tbxMultiSend7";
            this.tbxMultiSend7.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend7.TabIndex = 39;
            this.tbxMultiSend7.Text = "6";
            // 
            // ckbMultiSend6
            // 
            this.ckbMultiSend6.AutoSize = true;
            this.ckbMultiSend6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend6.Location = new System.Drawing.Point(5, 140);
            this.ckbMultiSend6.Name = "ckbMultiSend6";
            this.ckbMultiSend6.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend6.TabIndex = 44;
            this.ckbMultiSend6.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend7
            // 
            this.btnMultiSend7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend7.Location = new System.Drawing.Point(252, 145);
            this.btnMultiSend7.Name = "btnMultiSend7";
            this.btnMultiSend7.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend7.TabIndex = 40;
            this.btnMultiSend7.Text = "6";
            this.btnMultiSend7.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend5
            // 
            this.ckbMultiSend5.AutoSize = true;
            this.ckbMultiSend5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend5.Location = new System.Drawing.Point(6, 101);
            this.ckbMultiSend5.Name = "ckbMultiSend5";
            this.ckbMultiSend5.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend5.TabIndex = 43;
            this.ckbMultiSend5.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend8
            // 
            this.btnMultiSend8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend8.Location = new System.Drawing.Point(252, 169);
            this.btnMultiSend8.Name = "btnMultiSend8";
            this.btnMultiSend8.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend8.TabIndex = 41;
            this.btnMultiSend8.Text = "7";
            this.btnMultiSend8.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend5
            // 
            this.tbxMultiSend5.Location = new System.Drawing.Point(24, 100);
            this.tbxMultiSend5.Name = "tbxMultiSend5";
            this.tbxMultiSend5.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend5.TabIndex = 42;
            this.tbxMultiSend5.Text = "4";
            // 
            // panelMultiSend2
            // 
            this.panelMultiSend2.Controls.Add(this.btnMultiSend12);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend11);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend12);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend14);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend13);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend13);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend14);
            this.panelMultiSend2.Controls.Add(this.label18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend11);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend11);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend20);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend12);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend19);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend13);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend19);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend14);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend20);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend15);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend20);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend16);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend19);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend16);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend18);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend17);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend17);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend16);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend17);
            this.panelMultiSend2.Controls.Add(this.ckbMultiSend15);
            this.panelMultiSend2.Controls.Add(this.btnMultiSend18);
            this.panelMultiSend2.Controls.Add(this.tbxMultiSend15);
            this.panelMultiSend2.Location = new System.Drawing.Point(8, 153);
            this.panelMultiSend2.Name = "panelMultiSend2";
            this.panelMultiSend2.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend2.TabIndex = 71;
            this.panelMultiSend2.Visible = false;
            // 
            // btnMultiSend12
            // 
            this.btnMultiSend12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend12.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend12.Name = "btnMultiSend12";
            this.btnMultiSend12.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend12.TabIndex = 6;
            this.btnMultiSend12.Text = "11";
            this.btnMultiSend12.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend11
            // 
            this.btnMultiSend11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend11.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend11.Name = "btnMultiSend11";
            this.btnMultiSend11.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend11.TabIndex = 2;
            this.btnMultiSend11.Text = "10";
            this.btnMultiSend11.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend12
            // 
            this.tbxMultiSend12.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend12.Name = "tbxMultiSend12";
            this.tbxMultiSend12.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend12.TabIndex = 7;
            this.tbxMultiSend12.Text = "1";
            // 
            // tbxMultiSend14
            // 
            this.tbxMultiSend14.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend14.Name = "tbxMultiSend14";
            this.tbxMultiSend14.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend14.TabIndex = 8;
            this.tbxMultiSend14.Text = "3";
            // 
            // tbxMultiSend13
            // 
            this.tbxMultiSend13.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend13.Name = "tbxMultiSend13";
            this.tbxMultiSend13.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend13.TabIndex = 9;
            this.tbxMultiSend13.Text = "2";
            // 
            // btnMultiSend13
            // 
            this.btnMultiSend13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend13.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend13.Name = "btnMultiSend13";
            this.btnMultiSend13.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend13.TabIndex = 10;
            this.btnMultiSend13.Text = "12";
            this.btnMultiSend13.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend14
            // 
            this.btnMultiSend14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend14.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend14.Name = "btnMultiSend14";
            this.btnMultiSend14.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend14.TabIndex = 11;
            this.btnMultiSend14.Text = "13";
            this.btnMultiSend14.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(588, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 17);
            this.label18.TabIndex = 27;
            this.label18.Text = "间隔：";
            // 
            // tbxMultiSend11
            // 
            this.tbxMultiSend11.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend11.Name = "tbxMultiSend11";
            this.tbxMultiSend11.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend11.TabIndex = 30;
            this.tbxMultiSend11.Text = "0";
            this.tbxMultiSend11.WordWrap = false;
            // 
            // ckbMultiSend11
            // 
            this.ckbMultiSend11.AutoSize = true;
            this.ckbMultiSend11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend11.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend11.Name = "ckbMultiSend11";
            this.ckbMultiSend11.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend11.TabIndex = 31;
            this.ckbMultiSend11.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend20
            // 
            this.ckbMultiSend20.AutoSize = true;
            this.ckbMultiSend20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend20.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend20.Name = "ckbMultiSend20";
            this.ckbMultiSend20.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend20.TabIndex = 56;
            this.ckbMultiSend20.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend12
            // 
            this.ckbMultiSend12.AutoSize = true;
            this.ckbMultiSend12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend12.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend12.Name = "ckbMultiSend12";
            this.ckbMultiSend12.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend12.TabIndex = 32;
            this.ckbMultiSend12.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend19
            // 
            this.ckbMultiSend19.AutoSize = true;
            this.ckbMultiSend19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend19.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend19.Name = "ckbMultiSend19";
            this.ckbMultiSend19.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend19.TabIndex = 55;
            this.ckbMultiSend19.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend13
            // 
            this.ckbMultiSend13.AutoSize = true;
            this.ckbMultiSend13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend13.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend13.Name = "ckbMultiSend13";
            this.ckbMultiSend13.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend13.TabIndex = 33;
            this.ckbMultiSend13.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend19
            // 
            this.tbxMultiSend19.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend19.Name = "tbxMultiSend19";
            this.tbxMultiSend19.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend19.TabIndex = 54;
            this.tbxMultiSend19.Text = "8";
            // 
            // ckbMultiSend14
            // 
            this.ckbMultiSend14.AutoSize = true;
            this.ckbMultiSend14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend14.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend14.Name = "ckbMultiSend14";
            this.ckbMultiSend14.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend14.TabIndex = 34;
            this.ckbMultiSend14.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend20
            // 
            this.tbxMultiSend20.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend20.Name = "tbxMultiSend20";
            this.tbxMultiSend20.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend20.TabIndex = 49;
            this.tbxMultiSend20.Text = "9";
            // 
            // btnMultiSend15
            // 
            this.btnMultiSend15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend15.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend15.Name = "btnMultiSend15";
            this.btnMultiSend15.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend15.TabIndex = 35;
            this.btnMultiSend15.Text = "14";
            this.btnMultiSend15.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend20
            // 
            this.btnMultiSend20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend20.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend20.Name = "btnMultiSend20";
            this.btnMultiSend20.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend20.TabIndex = 48;
            this.btnMultiSend20.Text = "19";
            this.btnMultiSend20.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend16
            // 
            this.btnMultiSend16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend16.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend16.Name = "btnMultiSend16";
            this.btnMultiSend16.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend16.TabIndex = 36;
            this.btnMultiSend16.Text = "15";
            this.btnMultiSend16.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend19
            // 
            this.btnMultiSend19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend19.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend19.Name = "btnMultiSend19";
            this.btnMultiSend19.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend19.TabIndex = 47;
            this.btnMultiSend19.Text = "18";
            this.btnMultiSend19.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend16
            // 
            this.tbxMultiSend16.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend16.Name = "tbxMultiSend16";
            this.tbxMultiSend16.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend16.TabIndex = 37;
            this.tbxMultiSend16.Text = "5";
            // 
            // ckbMultiSend18
            // 
            this.ckbMultiSend18.AutoSize = true;
            this.ckbMultiSend18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend18.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend18.Name = "ckbMultiSend18";
            this.ckbMultiSend18.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend18.TabIndex = 46;
            this.ckbMultiSend18.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend18
            // 
            this.tbxMultiSend18.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend18.Name = "tbxMultiSend18";
            this.tbxMultiSend18.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend18.TabIndex = 38;
            this.tbxMultiSend18.Text = "7";
            // 
            // ckbMultiSend17
            // 
            this.ckbMultiSend17.AutoSize = true;
            this.ckbMultiSend17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend17.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend17.Name = "ckbMultiSend17";
            this.ckbMultiSend17.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend17.TabIndex = 45;
            this.ckbMultiSend17.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend17
            // 
            this.tbxMultiSend17.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend17.Name = "tbxMultiSend17";
            this.tbxMultiSend17.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend17.TabIndex = 39;
            this.tbxMultiSend17.Text = "6";
            // 
            // ckbMultiSend16
            // 
            this.ckbMultiSend16.AutoSize = true;
            this.ckbMultiSend16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend16.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend16.Name = "ckbMultiSend16";
            this.ckbMultiSend16.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend16.TabIndex = 44;
            this.ckbMultiSend16.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend17
            // 
            this.btnMultiSend17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend17.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend17.Name = "btnMultiSend17";
            this.btnMultiSend17.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend17.TabIndex = 40;
            this.btnMultiSend17.Text = "16";
            this.btnMultiSend17.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend15
            // 
            this.ckbMultiSend15.AutoSize = true;
            this.ckbMultiSend15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend15.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend15.Name = "ckbMultiSend15";
            this.ckbMultiSend15.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend15.TabIndex = 43;
            this.ckbMultiSend15.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend18
            // 
            this.btnMultiSend18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend18.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend18.Name = "btnMultiSend18";
            this.btnMultiSend18.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend18.TabIndex = 41;
            this.btnMultiSend18.Text = "17";
            this.btnMultiSend18.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend15
            // 
            this.tbxMultiSend15.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend15.Name = "tbxMultiSend15";
            this.tbxMultiSend15.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend15.TabIndex = 42;
            this.tbxMultiSend15.Text = "4";
            // 
            // panelMultiSend3
            // 
            this.panelMultiSend3.Controls.Add(this.btnMultiSend22);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend21);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend22);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend24);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend23);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend23);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend24);
            this.panelMultiSend3.Controls.Add(this.label19);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend21);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend21);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend30);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend22);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend29);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend23);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend29);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend24);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend30);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend25);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend30);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend26);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend29);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend26);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend28);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend28);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend27);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend27);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend26);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend27);
            this.panelMultiSend3.Controls.Add(this.ckbMultiSend25);
            this.panelMultiSend3.Controls.Add(this.btnMultiSend28);
            this.panelMultiSend3.Controls.Add(this.tbxMultiSend25);
            this.panelMultiSend3.Location = new System.Drawing.Point(24, 274);
            this.panelMultiSend3.Name = "panelMultiSend3";
            this.panelMultiSend3.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend3.TabIndex = 71;
            this.panelMultiSend3.Visible = false;
            // 
            // btnMultiSend22
            // 
            this.btnMultiSend22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend22.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend22.Name = "btnMultiSend22";
            this.btnMultiSend22.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend22.TabIndex = 6;
            this.btnMultiSend22.Text = "21";
            this.btnMultiSend22.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend21
            // 
            this.btnMultiSend21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend21.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend21.Name = "btnMultiSend21";
            this.btnMultiSend21.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend21.TabIndex = 2;
            this.btnMultiSend21.Text = "20";
            this.btnMultiSend21.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend22
            // 
            this.tbxMultiSend22.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend22.Name = "tbxMultiSend22";
            this.tbxMultiSend22.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend22.TabIndex = 7;
            this.tbxMultiSend22.Text = "1";
            // 
            // tbxMultiSend24
            // 
            this.tbxMultiSend24.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend24.Name = "tbxMultiSend24";
            this.tbxMultiSend24.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend24.TabIndex = 8;
            this.tbxMultiSend24.Text = "3";
            // 
            // tbxMultiSend23
            // 
            this.tbxMultiSend23.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend23.Name = "tbxMultiSend23";
            this.tbxMultiSend23.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend23.TabIndex = 9;
            this.tbxMultiSend23.Text = "2";
            // 
            // btnMultiSend23
            // 
            this.btnMultiSend23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend23.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend23.Name = "btnMultiSend23";
            this.btnMultiSend23.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend23.TabIndex = 10;
            this.btnMultiSend23.Text = "22";
            this.btnMultiSend23.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend24
            // 
            this.btnMultiSend24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend24.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend24.Name = "btnMultiSend24";
            this.btnMultiSend24.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend24.TabIndex = 11;
            this.btnMultiSend24.Text = "23";
            this.btnMultiSend24.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(588, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 17);
            this.label19.TabIndex = 27;
            this.label19.Text = "间隔：";
            // 
            // tbxMultiSend21
            // 
            this.tbxMultiSend21.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend21.Name = "tbxMultiSend21";
            this.tbxMultiSend21.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend21.TabIndex = 30;
            this.tbxMultiSend21.Text = "0";
            this.tbxMultiSend21.WordWrap = false;
            // 
            // ckbMultiSend21
            // 
            this.ckbMultiSend21.AutoSize = true;
            this.ckbMultiSend21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend21.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend21.Name = "ckbMultiSend21";
            this.ckbMultiSend21.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend21.TabIndex = 31;
            this.ckbMultiSend21.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend30
            // 
            this.ckbMultiSend30.AutoSize = true;
            this.ckbMultiSend30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend30.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend30.Name = "ckbMultiSend30";
            this.ckbMultiSend30.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend30.TabIndex = 56;
            this.ckbMultiSend30.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend22
            // 
            this.ckbMultiSend22.AutoSize = true;
            this.ckbMultiSend22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend22.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend22.Name = "ckbMultiSend22";
            this.ckbMultiSend22.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend22.TabIndex = 32;
            this.ckbMultiSend22.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend29
            // 
            this.ckbMultiSend29.AutoSize = true;
            this.ckbMultiSend29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend29.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend29.Name = "ckbMultiSend29";
            this.ckbMultiSend29.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend29.TabIndex = 55;
            this.ckbMultiSend29.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend23
            // 
            this.ckbMultiSend23.AutoSize = true;
            this.ckbMultiSend23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend23.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend23.Name = "ckbMultiSend23";
            this.ckbMultiSend23.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend23.TabIndex = 33;
            this.ckbMultiSend23.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend29
            // 
            this.tbxMultiSend29.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend29.Name = "tbxMultiSend29";
            this.tbxMultiSend29.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend29.TabIndex = 54;
            this.tbxMultiSend29.Text = "8";
            // 
            // ckbMultiSend24
            // 
            this.ckbMultiSend24.AutoSize = true;
            this.ckbMultiSend24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend24.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend24.Name = "ckbMultiSend24";
            this.ckbMultiSend24.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend24.TabIndex = 34;
            this.ckbMultiSend24.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend30
            // 
            this.tbxMultiSend30.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend30.Name = "tbxMultiSend30";
            this.tbxMultiSend30.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend30.TabIndex = 49;
            this.tbxMultiSend30.Text = "9";
            // 
            // btnMultiSend25
            // 
            this.btnMultiSend25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend25.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend25.Name = "btnMultiSend25";
            this.btnMultiSend25.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend25.TabIndex = 35;
            this.btnMultiSend25.Text = "24";
            this.btnMultiSend25.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend30
            // 
            this.btnMultiSend30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend30.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend30.Name = "btnMultiSend30";
            this.btnMultiSend30.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend30.TabIndex = 48;
            this.btnMultiSend30.Text = "29";
            this.btnMultiSend30.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend26
            // 
            this.btnMultiSend26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend26.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend26.Name = "btnMultiSend26";
            this.btnMultiSend26.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend26.TabIndex = 36;
            this.btnMultiSend26.Text = "25";
            this.btnMultiSend26.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend29
            // 
            this.btnMultiSend29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend29.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend29.Name = "btnMultiSend29";
            this.btnMultiSend29.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend29.TabIndex = 47;
            this.btnMultiSend29.Text = "28";
            this.btnMultiSend29.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend26
            // 
            this.tbxMultiSend26.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend26.Name = "tbxMultiSend26";
            this.tbxMultiSend26.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend26.TabIndex = 37;
            this.tbxMultiSend26.Text = "5";
            // 
            // ckbMultiSend28
            // 
            this.ckbMultiSend28.AutoSize = true;
            this.ckbMultiSend28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend28.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend28.Name = "ckbMultiSend28";
            this.ckbMultiSend28.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend28.TabIndex = 46;
            this.ckbMultiSend28.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend28
            // 
            this.tbxMultiSend28.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend28.Name = "tbxMultiSend28";
            this.tbxMultiSend28.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend28.TabIndex = 38;
            this.tbxMultiSend28.Text = "7";
            // 
            // ckbMultiSend27
            // 
            this.ckbMultiSend27.AutoSize = true;
            this.ckbMultiSend27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend27.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend27.Name = "ckbMultiSend27";
            this.ckbMultiSend27.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend27.TabIndex = 45;
            this.ckbMultiSend27.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend27
            // 
            this.tbxMultiSend27.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend27.Name = "tbxMultiSend27";
            this.tbxMultiSend27.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend27.TabIndex = 39;
            this.tbxMultiSend27.Text = "6";
            // 
            // ckbMultiSend26
            // 
            this.ckbMultiSend26.AutoSize = true;
            this.ckbMultiSend26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend26.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend26.Name = "ckbMultiSend26";
            this.ckbMultiSend26.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend26.TabIndex = 44;
            this.ckbMultiSend26.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend27
            // 
            this.btnMultiSend27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend27.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend27.Name = "btnMultiSend27";
            this.btnMultiSend27.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend27.TabIndex = 40;
            this.btnMultiSend27.Text = "26";
            this.btnMultiSend27.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend25
            // 
            this.ckbMultiSend25.AutoSize = true;
            this.ckbMultiSend25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend25.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend25.Name = "ckbMultiSend25";
            this.ckbMultiSend25.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend25.TabIndex = 43;
            this.ckbMultiSend25.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend28
            // 
            this.btnMultiSend28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend28.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend28.Name = "btnMultiSend28";
            this.btnMultiSend28.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend28.TabIndex = 41;
            this.btnMultiSend28.Text = "27";
            this.btnMultiSend28.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend25
            // 
            this.tbxMultiSend25.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend25.Name = "tbxMultiSend25";
            this.tbxMultiSend25.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend25.TabIndex = 42;
            this.tbxMultiSend25.Text = "4";
            // 
            // panelMultiSend4
            // 
            this.panelMultiSend4.Controls.Add(this.btnMultiSend32);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend31);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend32);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend34);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend33);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend33);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend34);
            this.panelMultiSend4.Controls.Add(this.label20);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend31);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend31);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend40);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend32);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend39);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend33);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend39);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend34);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend40);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend35);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend40);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend36);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend39);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend36);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend38);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend38);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend37);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend37);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend36);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend37);
            this.panelMultiSend4.Controls.Add(this.ckbMultiSend35);
            this.panelMultiSend4.Controls.Add(this.btnMultiSend38);
            this.panelMultiSend4.Controls.Add(this.tbxMultiSend35);
            this.panelMultiSend4.Location = new System.Drawing.Point(8, 15);
            this.panelMultiSend4.Name = "panelMultiSend4";
            this.panelMultiSend4.Size = new System.Drawing.Size(582, 114);
            this.panelMultiSend4.TabIndex = 71;
            this.panelMultiSend4.Visible = false;
            // 
            // btnMultiSend32
            // 
            this.btnMultiSend32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend32.Location = new System.Drawing.Point(253, 24);
            this.btnMultiSend32.Name = "btnMultiSend32";
            this.btnMultiSend32.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend32.TabIndex = 6;
            this.btnMultiSend32.Text = "31";
            this.btnMultiSend32.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend31
            // 
            this.btnMultiSend31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend31.Location = new System.Drawing.Point(253, 1);
            this.btnMultiSend31.Name = "btnMultiSend31";
            this.btnMultiSend31.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend31.TabIndex = 2;
            this.btnMultiSend31.Text = "30";
            this.btnMultiSend31.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend32
            // 
            this.tbxMultiSend32.Location = new System.Drawing.Point(24, 23);
            this.tbxMultiSend32.Name = "tbxMultiSend32";
            this.tbxMultiSend32.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend32.TabIndex = 7;
            this.tbxMultiSend32.Text = "1";
            // 
            // tbxMultiSend34
            // 
            this.tbxMultiSend34.Location = new System.Drawing.Point(24, 69);
            this.tbxMultiSend34.Name = "tbxMultiSend34";
            this.tbxMultiSend34.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend34.TabIndex = 8;
            this.tbxMultiSend34.Text = "3";
            // 
            // tbxMultiSend33
            // 
            this.tbxMultiSend33.Location = new System.Drawing.Point(24, 46);
            this.tbxMultiSend33.Name = "tbxMultiSend33";
            this.tbxMultiSend33.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend33.TabIndex = 9;
            this.tbxMultiSend33.Text = "2";
            // 
            // btnMultiSend33
            // 
            this.btnMultiSend33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend33.Location = new System.Drawing.Point(253, 47);
            this.btnMultiSend33.Name = "btnMultiSend33";
            this.btnMultiSend33.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend33.TabIndex = 10;
            this.btnMultiSend33.Text = "32";
            this.btnMultiSend33.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend34
            // 
            this.btnMultiSend34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend34.Location = new System.Drawing.Point(253, 70);
            this.btnMultiSend34.Name = "btnMultiSend34";
            this.btnMultiSend34.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend34.TabIndex = 11;
            this.btnMultiSend34.Text = "33";
            this.btnMultiSend34.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(588, 97);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(44, 17);
            this.label20.TabIndex = 27;
            this.label20.Text = "间隔：";
            // 
            // tbxMultiSend31
            // 
            this.tbxMultiSend31.Location = new System.Drawing.Point(24, 0);
            this.tbxMultiSend31.Name = "tbxMultiSend31";
            this.tbxMultiSend31.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend31.TabIndex = 30;
            this.tbxMultiSend31.Text = "0";
            this.tbxMultiSend31.WordWrap = false;
            // 
            // ckbMultiSend31
            // 
            this.ckbMultiSend31.AutoSize = true;
            this.ckbMultiSend31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend31.Location = new System.Drawing.Point(5, 2);
            this.ckbMultiSend31.Name = "ckbMultiSend31";
            this.ckbMultiSend31.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend31.TabIndex = 31;
            this.ckbMultiSend31.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend40
            // 
            this.ckbMultiSend40.AutoSize = true;
            this.ckbMultiSend40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend40.Location = new System.Drawing.Point(299, 97);
            this.ckbMultiSend40.Name = "ckbMultiSend40";
            this.ckbMultiSend40.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend40.TabIndex = 56;
            this.ckbMultiSend40.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend32
            // 
            this.ckbMultiSend32.AutoSize = true;
            this.ckbMultiSend32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend32.Location = new System.Drawing.Point(5, 25);
            this.ckbMultiSend32.Name = "ckbMultiSend32";
            this.ckbMultiSend32.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend32.TabIndex = 32;
            this.ckbMultiSend32.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend39
            // 
            this.ckbMultiSend39.AutoSize = true;
            this.ckbMultiSend39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend39.Location = new System.Drawing.Point(299, 74);
            this.ckbMultiSend39.Name = "ckbMultiSend39";
            this.ckbMultiSend39.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend39.TabIndex = 55;
            this.ckbMultiSend39.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend33
            // 
            this.ckbMultiSend33.AutoSize = true;
            this.ckbMultiSend33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend33.Location = new System.Drawing.Point(5, 48);
            this.ckbMultiSend33.Name = "ckbMultiSend33";
            this.ckbMultiSend33.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend33.TabIndex = 33;
            this.ckbMultiSend33.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend39
            // 
            this.tbxMultiSend39.Location = new System.Drawing.Point(320, 71);
            this.tbxMultiSend39.Name = "tbxMultiSend39";
            this.tbxMultiSend39.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend39.TabIndex = 54;
            this.tbxMultiSend39.Text = "8";
            // 
            // ckbMultiSend34
            // 
            this.ckbMultiSend34.AutoSize = true;
            this.ckbMultiSend34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend34.Location = new System.Drawing.Point(5, 71);
            this.ckbMultiSend34.Name = "ckbMultiSend34";
            this.ckbMultiSend34.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend34.TabIndex = 34;
            this.ckbMultiSend34.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend40
            // 
            this.tbxMultiSend40.Location = new System.Drawing.Point(320, 94);
            this.tbxMultiSend40.Name = "tbxMultiSend40";
            this.tbxMultiSend40.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend40.TabIndex = 49;
            this.tbxMultiSend40.Text = "9";
            // 
            // btnMultiSend35
            // 
            this.btnMultiSend35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend35.Location = new System.Drawing.Point(253, 93);
            this.btnMultiSend35.Name = "btnMultiSend35";
            this.btnMultiSend35.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend35.TabIndex = 35;
            this.btnMultiSend35.Text = "34";
            this.btnMultiSend35.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend40
            // 
            this.btnMultiSend40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend40.Location = new System.Drawing.Point(548, 93);
            this.btnMultiSend40.Name = "btnMultiSend40";
            this.btnMultiSend40.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend40.TabIndex = 48;
            this.btnMultiSend40.Text = "39";
            this.btnMultiSend40.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend36
            // 
            this.btnMultiSend36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend36.Location = new System.Drawing.Point(548, 1);
            this.btnMultiSend36.Name = "btnMultiSend36";
            this.btnMultiSend36.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend36.TabIndex = 36;
            this.btnMultiSend36.Text = "35";
            this.btnMultiSend36.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend39
            // 
            this.btnMultiSend39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend39.Location = new System.Drawing.Point(548, 70);
            this.btnMultiSend39.Name = "btnMultiSend39";
            this.btnMultiSend39.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend39.TabIndex = 47;
            this.btnMultiSend39.Text = "38";
            this.btnMultiSend39.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend36
            // 
            this.tbxMultiSend36.Location = new System.Drawing.Point(320, 2);
            this.tbxMultiSend36.Name = "tbxMultiSend36";
            this.tbxMultiSend36.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend36.TabIndex = 37;
            this.tbxMultiSend36.Text = "5";
            // 
            // ckbMultiSend38
            // 
            this.ckbMultiSend38.AutoSize = true;
            this.ckbMultiSend38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend38.Location = new System.Drawing.Point(299, 51);
            this.ckbMultiSend38.Name = "ckbMultiSend38";
            this.ckbMultiSend38.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend38.TabIndex = 46;
            this.ckbMultiSend38.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend38
            // 
            this.tbxMultiSend38.Location = new System.Drawing.Point(320, 48);
            this.tbxMultiSend38.Name = "tbxMultiSend38";
            this.tbxMultiSend38.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend38.TabIndex = 38;
            this.tbxMultiSend38.Text = "7";
            // 
            // ckbMultiSend37
            // 
            this.ckbMultiSend37.AutoSize = true;
            this.ckbMultiSend37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend37.Location = new System.Drawing.Point(299, 28);
            this.ckbMultiSend37.Name = "ckbMultiSend37";
            this.ckbMultiSend37.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend37.TabIndex = 45;
            this.ckbMultiSend37.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend37
            // 
            this.tbxMultiSend37.Location = new System.Drawing.Point(320, 25);
            this.tbxMultiSend37.Name = "tbxMultiSend37";
            this.tbxMultiSend37.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend37.TabIndex = 39;
            this.tbxMultiSend37.Text = "6";
            // 
            // ckbMultiSend36
            // 
            this.ckbMultiSend36.AutoSize = true;
            this.ckbMultiSend36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend36.Location = new System.Drawing.Point(299, 5);
            this.ckbMultiSend36.Name = "ckbMultiSend36";
            this.ckbMultiSend36.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend36.TabIndex = 44;
            this.ckbMultiSend36.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend37
            // 
            this.btnMultiSend37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend37.Location = new System.Drawing.Point(548, 24);
            this.btnMultiSend37.Name = "btnMultiSend37";
            this.btnMultiSend37.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend37.TabIndex = 40;
            this.btnMultiSend37.Text = "36";
            this.btnMultiSend37.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSend35
            // 
            this.ckbMultiSend35.AutoSize = true;
            this.ckbMultiSend35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbMultiSend35.Location = new System.Drawing.Point(5, 94);
            this.ckbMultiSend35.Name = "ckbMultiSend35";
            this.ckbMultiSend35.Size = new System.Drawing.Size(15, 14);
            this.ckbMultiSend35.TabIndex = 43;
            this.ckbMultiSend35.UseVisualStyleBackColor = true;
            // 
            // btnMultiSend38
            // 
            this.btnMultiSend38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMultiSend38.Location = new System.Drawing.Point(548, 47);
            this.btnMultiSend38.Name = "btnMultiSend38";
            this.btnMultiSend38.Size = new System.Drawing.Size(30, 22);
            this.btnMultiSend38.TabIndex = 41;
            this.btnMultiSend38.Text = "37";
            this.btnMultiSend38.UseVisualStyleBackColor = true;
            // 
            // tbxMultiSend35
            // 
            this.tbxMultiSend35.Location = new System.Drawing.Point(24, 92);
            this.tbxMultiSend35.Name = "tbxMultiSend35";
            this.tbxMultiSend35.Size = new System.Drawing.Size(220, 23);
            this.tbxMultiSend35.TabIndex = 42;
            this.tbxMultiSend35.Text = "4";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(594, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 17);
            this.label21.TabIndex = 71;
            this.label21.Text = "周期:";
            // 
            // btnRemarkMultiSend
            // 
            this.btnRemarkMultiSend.Location = new System.Drawing.Point(596, 119);
            this.btnRemarkMultiSend.Name = "btnRemarkMultiSend";
            this.btnRemarkMultiSend.Size = new System.Drawing.Size(95, 23);
            this.btnRemarkMultiSend.TabIndex = 69;
            this.btnRemarkMultiSend.Text = "导入导出条目";
            this.btnRemarkMultiSend.UseVisualStyleBackColor = true;
            // 
            // btnMultiEndPage
            // 
            this.btnMultiEndPage.Location = new System.Drawing.Point(218, 804);
            this.btnMultiEndPage.Name = "btnMultiEndPage";
            this.btnMultiEndPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiEndPage.TabIndex = 66;
            this.btnMultiEndPage.Text = "尾页";
            this.btnMultiEndPage.UseVisualStyleBackColor = true;
            // 
            // btnMultiNextPage
            // 
            this.btnMultiNextPage.Location = new System.Drawing.Point(152, 804);
            this.btnMultiNextPage.Name = "btnMultiNextPage";
            this.btnMultiNextPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiNextPage.TabIndex = 65;
            this.btnMultiNextPage.Text = "下一页";
            this.btnMultiNextPage.UseVisualStyleBackColor = true;
            // 
            // btnMultiLastPage
            // 
            this.btnMultiLastPage.Location = new System.Drawing.Point(86, 804);
            this.btnMultiLastPage.Name = "btnMultiLastPage";
            this.btnMultiLastPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiLastPage.TabIndex = 64;
            this.btnMultiLastPage.Text = "上一页";
            this.btnMultiLastPage.UseVisualStyleBackColor = true;
            // 
            // btnMultiFirstPage
            // 
            this.btnMultiFirstPage.Location = new System.Drawing.Point(20, 804);
            this.btnMultiFirstPage.Name = "btnMultiFirstPage";
            this.btnMultiFirstPage.Size = new System.Drawing.Size(60, 23);
            this.btnMultiFirstPage.TabIndex = 63;
            this.btnMultiFirstPage.Text = "首页";
            this.btnMultiFirstPage.UseVisualStyleBackColor = true;
            // 
            // ckbMultiSendNewLine
            // 
            this.ckbMultiSendNewLine.AutoSize = true;
            this.ckbMultiSendNewLine.Checked = true;
            this.ckbMultiSendNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMultiSendNewLine.Location = new System.Drawing.Point(596, 5);
            this.ckbMultiSendNewLine.Name = "ckbMultiSendNewLine";
            this.ckbMultiSendNewLine.Size = new System.Drawing.Size(75, 21);
            this.ckbMultiSendNewLine.TabIndex = 62;
            this.ckbMultiSendNewLine.Text = "发送新行";
            this.ckbMultiSendNewLine.UseVisualStyleBackColor = true;
            // 
            // ckbRelateKeyBoard
            // 
            this.ckbRelateKeyBoard.AutoSize = true;
            this.ckbRelateKeyBoard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ckbRelateKeyBoard.Location = new System.Drawing.Point(596, 51);
            this.ckbRelateKeyBoard.Name = "ckbRelateKeyBoard";
            this.ckbRelateKeyBoard.Size = new System.Drawing.Size(99, 21);
            this.ckbRelateKeyBoard.TabIndex = 61;
            this.ckbRelateKeyBoard.Text = "关联数字键盘";
            this.ckbRelateKeyBoard.UseVisualStyleBackColor = true;
            // 
            // ckbMultiHexSend
            // 
            this.ckbMultiHexSend.AutoSize = true;
            this.ckbMultiHexSend.Location = new System.Drawing.Point(596, 27);
            this.ckbMultiHexSend.Name = "ckbMultiHexSend";
            this.ckbMultiHexSend.Size = new System.Drawing.Size(89, 21);
            this.ckbMultiHexSend.TabIndex = 60;
            this.ckbMultiHexSend.Text = "16进制发送";
            this.ckbMultiHexSend.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(674, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 17);
            this.label14.TabIndex = 29;
            this.label14.Text = "ms";
            // 
            // tbxMutilSendPeriod
            // 
            this.tbxMutilSendPeriod.Location = new System.Drawing.Point(635, 94);
            this.tbxMutilSendPeriod.Name = "tbxMutilSendPeriod";
            this.tbxMutilSendPeriod.Size = new System.Drawing.Size(35, 23);
            this.tbxMutilSendPeriod.TabIndex = 28;
            this.tbxMutilSendPeriod.Text = "1000";
            // 
            // ckbMultiAutoSend
            // 
            this.ckbMultiAutoSend.AutoSize = true;
            this.ckbMultiAutoSend.Location = new System.Drawing.Point(596, 75);
            this.ckbMultiAutoSend.Name = "ckbMultiAutoSend";
            this.ckbMultiAutoSend.Size = new System.Drawing.Size(99, 21);
            this.ckbMultiAutoSend.TabIndex = 26;
            this.ckbMultiAutoSend.Text = "自动循环发送";
            this.ckbMultiAutoSend.UseVisualStyleBackColor = true;
            // 
            // _SendFileTabPage
            // 
            this._SendFileTabPage.Controls.Add(this._FileContentTextBox);
            this._SendFileTabPage.Controls.Add(this.tbxSendFile);
            this._SendFileTabPage.Controls.Add(this.btnStopSendFile);
            this._SendFileTabPage.Controls.Add(this.btnSendFile);
            this._SendFileTabPage.Controls.Add(this.btnOpenSendFile);
            this._SendFileTabPage.Location = new System.Drawing.Point(4, 34);
            this._SendFileTabPage.Name = "_SendFileTabPage";
            this._SendFileTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._SendFileTabPage.Size = new System.Drawing.Size(425, 858);
            this._SendFileTabPage.TabIndex = 5;
            this._SendFileTabPage.Text = "文件发送";
            this._SendFileTabPage.UseVisualStyleBackColor = true;
            // 
            // _FileContentTextBox
            // 
            this._FileContentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._FileContentTextBox.Location = new System.Drawing.Point(6, 39);
            this._FileContentTextBox.MaxLength = 132767;
            this._FileContentTextBox.Multiline = true;
            this._FileContentTextBox.Name = "_FileContentTextBox";
            this._FileContentTextBox.ReadOnly = true;
            this._FileContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._FileContentTextBox.Size = new System.Drawing.Size(305, 818);
            this._FileContentTextBox.TabIndex = 49;
            // 
            // tbxSendFile
            // 
            this.tbxSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSendFile.BackColor = System.Drawing.SystemColors.Control;
            this.tbxSendFile.Location = new System.Drawing.Point(6, 10);
            this.tbxSendFile.Name = "tbxSendFile";
            this.tbxSendFile.ReadOnly = true;
            this.tbxSendFile.Size = new System.Drawing.Size(411, 23);
            this.tbxSendFile.TabIndex = 48;
            // 
            // btnStopSendFile
            // 
            this.btnStopSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopSendFile.Location = new System.Drawing.Point(317, 127);
            this.btnStopSendFile.Name = "btnStopSendFile";
            this.btnStopSendFile.Size = new System.Drawing.Size(100, 40);
            this.btnStopSendFile.TabIndex = 47;
            this.btnStopSendFile.Text = "停止发送";
            this.btnStopSendFile.UseVisualStyleBackColor = true;
            this.btnStopSendFile.Click += new System.EventHandler(this.btnStopSendFile_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendFile.Location = new System.Drawing.Point(317, 83);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(100, 40);
            this.btnSendFile.TabIndex = 46;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnOpenSendFile
            // 
            this.btnOpenSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSendFile.Location = new System.Drawing.Point(317, 39);
            this.btnOpenSendFile.Name = "btnOpenSendFile";
            this.btnOpenSendFile.Size = new System.Drawing.Size(100, 40);
            this.btnOpenSendFile.TabIndex = 45;
            this.btnOpenSendFile.Text = "打开文件";
            this.btnOpenSendFile.UseVisualStyleBackColor = true;
            this.btnOpenSendFile.Click += new System.EventHandler(this.btnOpenSendFile_Click);
            // 
            // _MainSplitContainer
            // 
            this._MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._MainSplitContainer.Name = "_MainSplitContainer";
            // 
            // _MainSplitContainer.Panel1
            // 
            this._MainSplitContainer.Panel1.Controls.Add(this._MessageTabControl);
            // 
            // _MainSplitContainer.Panel2
            // 
            this._MainSplitContainer.Panel2.Controls.Add(this._ProtocolTabControl);
            this._MainSplitContainer.Size = new System.Drawing.Size(1292, 896);
            this._MainSplitContainer.SplitterDistance = 855;
            this._MainSplitContainer.TabIndex = 42;
            // 
            // _MessageTabControl
            // 
            this._MessageTabControl.Controls.Add(this._MessageTabPage);
            this._MessageTabControl.Controls.Add(this._ChartTabPage);
            this._MessageTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MessageTabControl.ItemSize = new System.Drawing.Size(67, 30);
            this._MessageTabControl.Location = new System.Drawing.Point(0, 0);
            this._MessageTabControl.Name = "_MessageTabControl";
            this._MessageTabControl.Padding = new System.Drawing.Point(8, 3);
            this._MessageTabControl.SelectedIndex = 0;
            this._MessageTabControl.Size = new System.Drawing.Size(855, 896);
            this._MessageTabControl.TabIndex = 2;
            // 
            // _MessageTabPage
            // 
            this._MessageTabPage.Controls.Add(this._TextPanel);
            this._MessageTabPage.Controls.Add(this.toolStrip2);
            this._MessageTabPage.Location = new System.Drawing.Point(4, 34);
            this._MessageTabPage.Name = "_MessageTabPage";
            this._MessageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._MessageTabPage.Size = new System.Drawing.Size(847, 858);
            this._MessageTabPage.TabIndex = 0;
            this._MessageTabPage.Text = "消息监控台";
            this._MessageTabPage.UseVisualStyleBackColor = true;
            // 
            // _TextPanel
            // 
            this._TextPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._TextPanel.Controls.Add(this.tbxRecvData);
            this._TextPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TextPanel.Location = new System.Drawing.Point(3, 34);
            this._TextPanel.Name = "_TextPanel";
            this._TextPanel.Size = new System.Drawing.Size(841, 821);
            this._TextPanel.TabIndex = 1;
            // 
            // tbxRecvData
            // 
            this.tbxRecvData.BackColor = System.Drawing.Color.Black;
            this.tbxRecvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxRecvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxRecvData.ForeColor = System.Drawing.Color.Chartreuse;
            this.tbxRecvData.Location = new System.Drawing.Point(0, 0);
            this.tbxRecvData.Name = "tbxRecvData";
            this.tbxRecvData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbxRecvData.Size = new System.Drawing.Size(839, 819);
            this.tbxRecvData.TabIndex = 0;
            this.tbxRecvData.Text = "abcd efg hijklmn opq rst uvmxyz";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripCheckBox1,
            this.toolStripCheckBox2,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(841, 31);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 28);
            this.toolStripButton1.Text = "字体";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripCheckBox1
            // 
            this.toolStripCheckBox1.Margin = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.toolStripCheckBox1.Name = "toolStripCheckBox1";
            this.toolStripCheckBox1.Size = new System.Drawing.Size(89, 27);
            this.toolStripCheckBox1.Text = "16进制显示";
            this.toolStripCheckBox1.ToolStripCheckBoxEnabled = true;
            // 
            // toolStripCheckBox2
            // 
            this.toolStripCheckBox2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripCheckBox2.Name = "toolStripCheckBox2";
            this.toolStripCheckBox2.Size = new System.Drawing.Size(87, 27);
            this.toolStripCheckBox2.Text = "显示时间戳";
            this.toolStripCheckBox2.ToolStripCheckBoxEnabled = true;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(84, 28);
            this.toolStripButton4.Text = "清除消息";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(84, 28);
            this.toolStripButton5.Text = "保存消息";
            // 
            // _ChartTabPage
            // 
            this._ChartTabPage.Controls.Add(this.splitContainer1);
            this._ChartTabPage.Location = new System.Drawing.Point(4, 34);
            this._ChartTabPage.Name = "_ChartTabPage";
            this._ChartTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._ChartTabPage.Size = new System.Drawing.Size(847, 858);
            this._ChartTabPage.TabIndex = 1;
            this._ChartTabPage.Text = "数据折线";
            this._ChartTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._Chart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(841, 852);
            this.splitContainer1.SplitterDistance = 651;
            this.splitContainer1.TabIndex = 1;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.AliceBlue;
            this.chart1.BorderlineColor = System.Drawing.SystemColors.ControlDark;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(841, 197);
            this.chart1.TabIndex = 1;
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.BottomToolStripPanel
            // 
            this._ToolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStripCom);
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._MainSplitContainer);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1292, 896);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(1292, 953);
            this._ToolStripContainer.TabIndex = 43;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStripCom
            // 
            this.statusStripCom.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripCom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStripCom.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripCom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblSendCnt,
            this.toolStripSeparator3,
            this.toolStripStatusLblRevCnt,
            this.toolStripSeparator4,
            this.SerialInfoStatusLabel,
            this.toolStripSeparator5,
            this.toolStripProgressBar1});
            this.statusStripCom.Location = new System.Drawing.Point(0, 0);
            this.statusStripCom.Name = "statusStripCom";
            this.statusStripCom.Size = new System.Drawing.Size(1292, 26);
            this.statusStripCom.SizingGrip = false;
            this.statusStripCom.TabIndex = 42;
            this.statusStripCom.Text = "statusStrip1";
            // 
            // toolStripStatusLblSendCnt
            // 
            this.toolStripStatusLblSendCnt.AutoSize = false;
            this.toolStripStatusLblSendCnt.Name = "toolStripStatusLblSendCnt";
            this.toolStripStatusLblSendCnt.Size = new System.Drawing.Size(100, 21);
            this.toolStripStatusLblSendCnt.Text = "S:0";
            this.toolStripStatusLblSendCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripStatusLblRevCnt
            // 
            this.toolStripStatusLblRevCnt.AutoSize = false;
            this.toolStripStatusLblRevCnt.Name = "toolStripStatusLblRevCnt";
            this.toolStripStatusLblRevCnt.Size = new System.Drawing.Size(100, 21);
            this.toolStripStatusLblRevCnt.Text = "R:0";
            this.toolStripStatusLblRevCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // SerialInfoStatusLabel
            // 
            this.SerialInfoStatusLabel.AutoSize = false;
            this.SerialInfoStatusLabel.Name = "SerialInfoStatusLabel";
            this.SerialInfoStatusLabel.Size = new System.Drawing.Size(130, 21);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 20);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OpenSerialPortStripButton,
            this._CloseSerialPortStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(180, 31);
            this.toolStrip1.TabIndex = 0;
            // 
            // _OpenSerialPortStripButton
            // 
            this._OpenSerialPortStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_OpenSerialPortStripButton.Image")));
            this._OpenSerialPortStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OpenSerialPortStripButton.Name = "_OpenSerialPortStripButton";
            this._OpenSerialPortStripButton.Size = new System.Drawing.Size(84, 28);
            this._OpenSerialPortStripButton.Text = "打开串口";
            this._OpenSerialPortStripButton.Click += new System.EventHandler(this.OpenSerialPortStripButton_Click);
            // 
            // _CloseSerialPortStripButton
            // 
            this._CloseSerialPortStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_CloseSerialPortStripButton.Image")));
            this._CloseSerialPortStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._CloseSerialPortStripButton.Name = "_CloseSerialPortStripButton";
            this._CloseSerialPortStripButton.Size = new System.Drawing.Size(84, 28);
            this._CloseSerialPortStripButton.Text = "关闭串口";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbxSendPeriod);
            this.groupBox1.Controls.Add(this.ckbTimeSend);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(321, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(96, 85);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "周期";
            // 
            // Workbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1292, 953);
            this.Controls.Add(this._ToolStripContainer);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Workbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Workbench";
            ((System.ComponentModel.ISupportInitialize)(this._Chart)).EndInit();
            this._ProtocolTabControl.ResumeLayout(false);
            this.singleTab.ResumeLayout(false);
            this.singleTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbxSendPeriod)).EndInit();
            this.multiTab.ResumeLayout(false);
            this.multiTab.PerformLayout();
            this.panelMultiSend1.ResumeLayout(false);
            this.panelMultiSend1.PerformLayout();
            this.panelMultiSend2.ResumeLayout(false);
            this.panelMultiSend2.PerformLayout();
            this.panelMultiSend3.ResumeLayout(false);
            this.panelMultiSend3.PerformLayout();
            this.panelMultiSend4.ResumeLayout(false);
            this.panelMultiSend4.PerformLayout();
            this._SendFileTabPage.ResumeLayout(false);
            this._SendFileTabPage.PerformLayout();
            this._MainSplitContainer.Panel1.ResumeLayout(false);
            this._MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._MainSplitContainer)).EndInit();
            this._MainSplitContainer.ResumeLayout(false);
            this._MessageTabControl.ResumeLayout(false);
            this._MessageTabPage.ResumeLayout(false);
            this._MessageTabPage.PerformLayout();
            this._TextPanel.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this._ChartTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this._ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this.statusStripCom.ResumeLayout(false);
            this.statusStripCom.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl _ProtocolTabControl;
        private System.Windows.Forms.TabPage singleTab;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnClearSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxSendData;
        private System.Windows.Forms.CheckBox ckbSendNewLine;
        private System.Windows.Forms.CheckBox ckbHexSend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbTimeSend;
        private System.Windows.Forms.TabPage multiTab;
        private System.Windows.Forms.Panel panelMultiSend1;
        private System.Windows.Forms.Button btnMultiSend2;
        private System.Windows.Forms.Button btnMultiSend1;
        private System.Windows.Forms.TextBox tbxMultiSend2;
        private System.Windows.Forms.TextBox tbxMultiSend4;
        private System.Windows.Forms.TextBox tbxMultiSend3;
        private System.Windows.Forms.Button btnMultiSend3;
        private System.Windows.Forms.Button btnMultiSend4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbxMultiSend1;
        private System.Windows.Forms.CheckBox ckbMultiSend1;
        private System.Windows.Forms.CheckBox ckbMultiSend10;
        private System.Windows.Forms.CheckBox ckbMultiSend2;
        private System.Windows.Forms.CheckBox ckbMultiSend9;
        private System.Windows.Forms.CheckBox ckbMultiSend3;
        private System.Windows.Forms.TextBox tbxMultiSend9;
        private System.Windows.Forms.CheckBox ckbMultiSend4;
        private System.Windows.Forms.TextBox tbxMultiSend10;
        private System.Windows.Forms.Button btnMultiSend5;
        private System.Windows.Forms.Button btnMultiSend10;
        private System.Windows.Forms.Button btnMultiSend6;
        private System.Windows.Forms.Button btnMultiSend9;
        private System.Windows.Forms.TextBox tbxMultiSend6;
        private System.Windows.Forms.CheckBox ckbMultiSend8;
        private System.Windows.Forms.TextBox tbxMultiSend8;
        private System.Windows.Forms.CheckBox ckbMultiSend7;
        private System.Windows.Forms.TextBox tbxMultiSend7;
        private System.Windows.Forms.CheckBox ckbMultiSend6;
        private System.Windows.Forms.Button btnMultiSend7;
        private System.Windows.Forms.CheckBox ckbMultiSend5;
        private System.Windows.Forms.Button btnMultiSend8;
        private System.Windows.Forms.TextBox tbxMultiSend5;
        private System.Windows.Forms.Panel panelMultiSend2;
        private System.Windows.Forms.Button btnMultiSend12;
        private System.Windows.Forms.Button btnMultiSend11;
        private System.Windows.Forms.TextBox tbxMultiSend12;
        private System.Windows.Forms.TextBox tbxMultiSend14;
        private System.Windows.Forms.TextBox tbxMultiSend13;
        private System.Windows.Forms.Button btnMultiSend13;
        private System.Windows.Forms.Button btnMultiSend14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxMultiSend11;
        private System.Windows.Forms.CheckBox ckbMultiSend11;
        private System.Windows.Forms.CheckBox ckbMultiSend20;
        private System.Windows.Forms.CheckBox ckbMultiSend12;
        private System.Windows.Forms.CheckBox ckbMultiSend19;
        private System.Windows.Forms.CheckBox ckbMultiSend13;
        private System.Windows.Forms.TextBox tbxMultiSend19;
        private System.Windows.Forms.CheckBox ckbMultiSend14;
        private System.Windows.Forms.TextBox tbxMultiSend20;
        private System.Windows.Forms.Button btnMultiSend15;
        private System.Windows.Forms.Button btnMultiSend20;
        private System.Windows.Forms.Button btnMultiSend16;
        private System.Windows.Forms.Button btnMultiSend19;
        private System.Windows.Forms.TextBox tbxMultiSend16;
        private System.Windows.Forms.CheckBox ckbMultiSend18;
        private System.Windows.Forms.TextBox tbxMultiSend18;
        private System.Windows.Forms.CheckBox ckbMultiSend17;
        private System.Windows.Forms.TextBox tbxMultiSend17;
        private System.Windows.Forms.CheckBox ckbMultiSend16;
        private System.Windows.Forms.Button btnMultiSend17;
        private System.Windows.Forms.CheckBox ckbMultiSend15;
        private System.Windows.Forms.Button btnMultiSend18;
        private System.Windows.Forms.TextBox tbxMultiSend15;
        private System.Windows.Forms.Panel panelMultiSend3;
        private System.Windows.Forms.Button btnMultiSend22;
        private System.Windows.Forms.Button btnMultiSend21;
        private System.Windows.Forms.TextBox tbxMultiSend22;
        private System.Windows.Forms.TextBox tbxMultiSend24;
        private System.Windows.Forms.TextBox tbxMultiSend23;
        private System.Windows.Forms.Button btnMultiSend23;
        private System.Windows.Forms.Button btnMultiSend24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxMultiSend21;
        private System.Windows.Forms.CheckBox ckbMultiSend21;
        private System.Windows.Forms.CheckBox ckbMultiSend30;
        private System.Windows.Forms.CheckBox ckbMultiSend22;
        private System.Windows.Forms.CheckBox ckbMultiSend29;
        private System.Windows.Forms.CheckBox ckbMultiSend23;
        private System.Windows.Forms.TextBox tbxMultiSend29;
        private System.Windows.Forms.CheckBox ckbMultiSend24;
        private System.Windows.Forms.TextBox tbxMultiSend30;
        private System.Windows.Forms.Button btnMultiSend25;
        private System.Windows.Forms.Button btnMultiSend30;
        private System.Windows.Forms.Button btnMultiSend26;
        private System.Windows.Forms.Button btnMultiSend29;
        private System.Windows.Forms.TextBox tbxMultiSend26;
        private System.Windows.Forms.CheckBox ckbMultiSend28;
        private System.Windows.Forms.TextBox tbxMultiSend28;
        private System.Windows.Forms.CheckBox ckbMultiSend27;
        private System.Windows.Forms.TextBox tbxMultiSend27;
        private System.Windows.Forms.CheckBox ckbMultiSend26;
        private System.Windows.Forms.Button btnMultiSend27;
        private System.Windows.Forms.CheckBox ckbMultiSend25;
        private System.Windows.Forms.Button btnMultiSend28;
        private System.Windows.Forms.TextBox tbxMultiSend25;
        private System.Windows.Forms.Panel panelMultiSend4;
        private System.Windows.Forms.Button btnMultiSend32;
        private System.Windows.Forms.Button btnMultiSend31;
        private System.Windows.Forms.TextBox tbxMultiSend32;
        private System.Windows.Forms.TextBox tbxMultiSend34;
        private System.Windows.Forms.TextBox tbxMultiSend33;
        private System.Windows.Forms.Button btnMultiSend33;
        private System.Windows.Forms.Button btnMultiSend34;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbxMultiSend31;
        private System.Windows.Forms.CheckBox ckbMultiSend31;
        private System.Windows.Forms.CheckBox ckbMultiSend40;
        private System.Windows.Forms.CheckBox ckbMultiSend32;
        private System.Windows.Forms.CheckBox ckbMultiSend39;
        private System.Windows.Forms.CheckBox ckbMultiSend33;
        private System.Windows.Forms.TextBox tbxMultiSend39;
        private System.Windows.Forms.CheckBox ckbMultiSend34;
        private System.Windows.Forms.TextBox tbxMultiSend40;
        private System.Windows.Forms.Button btnMultiSend35;
        private System.Windows.Forms.Button btnMultiSend40;
        private System.Windows.Forms.Button btnMultiSend36;
        private System.Windows.Forms.Button btnMultiSend39;
        private System.Windows.Forms.TextBox tbxMultiSend36;
        private System.Windows.Forms.CheckBox ckbMultiSend38;
        private System.Windows.Forms.TextBox tbxMultiSend38;
        private System.Windows.Forms.CheckBox ckbMultiSend37;
        private System.Windows.Forms.TextBox tbxMultiSend37;
        private System.Windows.Forms.CheckBox ckbMultiSend36;
        private System.Windows.Forms.Button btnMultiSend37;
        private System.Windows.Forms.CheckBox ckbMultiSend35;
        private System.Windows.Forms.Button btnMultiSend38;
        private System.Windows.Forms.TextBox tbxMultiSend35;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnRemarkMultiSend;
        private System.Windows.Forms.Button btnMultiEndPage;
        private System.Windows.Forms.Button btnMultiNextPage;
        private System.Windows.Forms.Button btnMultiLastPage;
        private System.Windows.Forms.Button btnMultiFirstPage;
        private System.Windows.Forms.CheckBox ckbMultiSendNewLine;
        private System.Windows.Forms.CheckBox ckbRelateKeyBoard;
        private System.Windows.Forms.CheckBox ckbMultiHexSend;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxMutilSendPeriod;
        private System.Windows.Forms.CheckBox ckbMultiAutoSend;
        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private System.Windows.Forms.NumericUpDown tbxSendPeriod;
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.StatusStrip statusStripCom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblSendCnt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblRevCnt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel SerialInfoStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel _TextPanel;
        private System.Windows.Forms.RichTextBox tbxRecvData;
        private System.Windows.Forms.TabControl _MessageTabControl;
        private System.Windows.Forms.TabPage _MessageTabPage;
        private System.Windows.Forms.TabPage _ChartTabPage;
        private System.Windows.Forms.TabPage _SendFileTabPage;
        private System.Windows.Forms.Button btnStopSendFile;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnOpenSendFile;
        private System.Windows.Forms.TextBox tbxSendFile;
        private System.Windows.Forms.TextBox _FileContentTextBox;

        private Chart _Chart;
        private SplitContainer splitContainer1;
        private Chart chart1;
        private ToolStripButton _OpenSerialPortStripButton;
        private ToolStripButton _CloseSerialPortStripButton;
        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripCheckBox toolStripCheckBox1;
        private ToolStripCheckBox toolStripCheckBox2;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private GroupBox groupBox1;
    }
}