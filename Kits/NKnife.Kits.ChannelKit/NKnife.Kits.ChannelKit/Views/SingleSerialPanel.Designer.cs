namespace NKnife.Kits.ChannelKit.Views
{
    partial class SingleSerialPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleSerialPanel));
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._ChoosePortButton = new System.Windows.Forms.ToolStripButton();
            this._OperatingPortButton = new System.Windows.Forms.ToolStripButton();
            this._ConfigurePortButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._PauseButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._QuestionsEditorButton = new System.Windows.Forms.ToolStripButton();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._InfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._SpaceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PortLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ListBox = new System.Windows.Forms.ListBox();
            this._ToolStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ChoosePortButton,
            this._ConfigurePortButton,
            this._OperatingPortButton,
            this.toolStripSeparator2,
            this._StartButton,
            this._PauseButton,
            this._StopButton,
            this.toolStripSeparator1,
            this._QuestionsEditorButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(367, 25);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // _ChoosePortButton
            // 
            this._ChoosePortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ChoosePortButton.Image = ((System.Drawing.Image)(resources.GetObject("_ChoosePortButton.Image")));
            this._ChoosePortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ChoosePortButton.Name = "_ChoosePortButton";
            this._ChoosePortButton.Size = new System.Drawing.Size(36, 22);
            this._ChoosePortButton.Text = "选择";
            this._ChoosePortButton.Click += new System.EventHandler(this._ChoosePortButton_Click);
            // 
            // _OperatingPortButton
            // 
            this._OperatingPortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._OperatingPortButton.Image = ((System.Drawing.Image)(resources.GetObject("_OperatingPortButton.Image")));
            this._OperatingPortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OperatingPortButton.Name = "_OperatingPortButton";
            this._OperatingPortButton.Size = new System.Drawing.Size(36, 22);
            this._OperatingPortButton.Text = "打开";
            this._OperatingPortButton.Click += new System.EventHandler(this._OperatingPortButton_Click);
            // 
            // _ConfigurePortButton
            // 
            this._ConfigurePortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ConfigurePortButton.Image = ((System.Drawing.Image)(resources.GetObject("_ConfigurePortButton.Image")));
            this._ConfigurePortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ConfigurePortButton.Name = "_ConfigurePortButton";
            this._ConfigurePortButton.Size = new System.Drawing.Size(36, 22);
            this._ConfigurePortButton.Text = "配置";
            this._ConfigurePortButton.Click += new System.EventHandler(this._ConfigurePortButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _StartButton
            // 
            this._StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._StartButton.Image = ((System.Drawing.Image)(resources.GetObject("_StartButton.Image")));
            this._StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(36, 22);
            this._StartButton.Text = "开始";
            this._StartButton.Click += new System.EventHandler(this._StartButton_Click);
            // 
            // _PauseButton
            // 
            this._PauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._PauseButton.Image = ((System.Drawing.Image)(resources.GetObject("_PauseButton.Image")));
            this._PauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PauseButton.Name = "_PauseButton";
            this._PauseButton.Size = new System.Drawing.Size(36, 22);
            this._PauseButton.Text = "暂停";
            this._PauseButton.Click += new System.EventHandler(this._PauseButton_Click);
            // 
            // _StopButton
            // 
            this._StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._StopButton.Image = ((System.Drawing.Image)(resources.GetObject("_StopButton.Image")));
            this._StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(36, 22);
            this._StopButton.Text = "停止";
            this._StopButton.Click += new System.EventHandler(this._StopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _QuestionsEditorButton
            // 
            this._QuestionsEditorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._QuestionsEditorButton.Image = ((System.Drawing.Image)(resources.GetObject("_QuestionsEditorButton.Image")));
            this._QuestionsEditorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._QuestionsEditorButton.Name = "_QuestionsEditorButton";
            this._QuestionsEditorButton.Size = new System.Drawing.Size(36, 22);
            this._QuestionsEditorButton.Text = "协议";
            this._QuestionsEditorButton.Click += new System.EventHandler(this._QuestionsEditorButton_Click);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._InfoLabel,
            this._SpaceLabel,
            this._PortLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 409);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(367, 22);
            this._StatusStrip.TabIndex = 1;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // _InfoLabel
            // 
            this._InfoLabel.Name = "_InfoLabel";
            this._InfoLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _SpaceLabel
            // 
            this._SpaceLabel.Name = "_SpaceLabel";
            this._SpaceLabel.Size = new System.Drawing.Size(307, 17);
            this._SpaceLabel.Spring = true;
            // 
            // _PortLabel
            // 
            this._PortLabel.Name = "_PortLabel";
            this._PortLabel.Size = new System.Drawing.Size(45, 17);
            this._PortLabel.Text = "COM1";
            // 
            // _ListBox
            // 
            this._ListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListBox.FormattingEnabled = true;
            this._ListBox.ItemHeight = 12;
            this._ListBox.Location = new System.Drawing.Point(0, 25);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(367, 384);
            this._ListBox.TabIndex = 2;
            // 
            // SimpleSerialPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListBox);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.Name = "SimpleSerialPanel";
            this.Size = new System.Drawing.Size(367, 431);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ListBox _ListBox;
        private System.Windows.Forms.ToolStripButton _ChoosePortButton;
        private System.Windows.Forms.ToolStripButton _StartButton;
        private System.Windows.Forms.ToolStripButton _PauseButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _ConfigurePortButton;
        private System.Windows.Forms.ToolStripStatusLabel _InfoLabel;
        private System.Windows.Forms.ToolStripStatusLabel _SpaceLabel;
        private System.Windows.Forms.ToolStripStatusLabel _PortLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _QuestionsEditorButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private System.Windows.Forms.ToolStripButton _OperatingPortButton;
    }
}
