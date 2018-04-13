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
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._InfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._SpaceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PortLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ListBox = new System.Windows.Forms.ListBox();
            this._ChoosePortButton = new System.Windows.Forms.ToolStripButton();
            this._ConfigurePortButton = new System.Windows.Forms.ToolStripButton();
            this._OpenPortButton = new System.Windows.Forms.ToolStripButton();
            this._ClosePortButton = new System.Windows.Forms.ToolStripButton();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._PauseButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this._QuestionsEditorButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ChoosePortButton,
            this._OpenPortButton,
            this._ClosePortButton,
            this._QuestionsEditorButton,
            this._ConfigurePortButton,
            this.toolStripSeparator2,
            this._StartButton,
            this._PauseButton,
            this._StopButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(367, 34);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
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
            this._ListBox.Location = new System.Drawing.Point(0, 34);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(367, 375);
            this._ListBox.TabIndex = 2;
            // 
            // _ChoosePortButton
            // 
            this._ChoosePortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ChoosePortButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.select;
            this._ChoosePortButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ChoosePortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ChoosePortButton.Margin = new System.Windows.Forms.Padding(3);
            this._ChoosePortButton.Name = "_ChoosePortButton";
            this._ChoosePortButton.Size = new System.Drawing.Size(28, 28);
            this._ChoosePortButton.Text = "选择串口";
            this._ChoosePortButton.Click += new System.EventHandler(this._ChoosePortButton_Click);
            // 
            // _ConfigurePortButton
            // 
            this._ConfigurePortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ConfigurePortButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.config;
            this._ConfigurePortButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ConfigurePortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ConfigurePortButton.Margin = new System.Windows.Forms.Padding(3);
            this._ConfigurePortButton.Name = "_ConfigurePortButton";
            this._ConfigurePortButton.Size = new System.Drawing.Size(28, 28);
            this._ConfigurePortButton.Text = "编辑串口配置";
            this._ConfigurePortButton.Click += new System.EventHandler(this._ConfigurePortButton_Click);
            // 
            // _OpenPortButton
            // 
            this._OpenPortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._OpenPortButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.open;
            this._OpenPortButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._OpenPortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OpenPortButton.Margin = new System.Windows.Forms.Padding(3);
            this._OpenPortButton.Name = "_OpenPortButton";
            this._OpenPortButton.Size = new System.Drawing.Size(28, 28);
            this._OpenPortButton.Text = "打开串口";
            this._OpenPortButton.Click += new System.EventHandler(this._OpenPortButton_Click);
            // 
            // _ClosePortButton
            // 
            this._ClosePortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ClosePortButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.close;
            this._ClosePortButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._ClosePortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ClosePortButton.Margin = new System.Windows.Forms.Padding(3);
            this._ClosePortButton.Name = "_ClosePortButton";
            this._ClosePortButton.Size = new System.Drawing.Size(28, 28);
            this._ClosePortButton.Text = "关闭串口";
            this._ClosePortButton.Click += new System.EventHandler(this._ClosePortButton_Click);
            // 
            // _StartButton
            // 
            this._StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.start;
            this._StartButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartButton.Margin = new System.Windows.Forms.Padding(3);
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(28, 28);
            this._StartButton.Text = "开始 发送与监听数据";
            this._StartButton.Click += new System.EventHandler(this._StartButton_Click);
            // 
            // _PauseButton
            // 
            this._PauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PauseButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.pause;
            this._PauseButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._PauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PauseButton.Margin = new System.Windows.Forms.Padding(3);
            this._PauseButton.Name = "_PauseButton";
            this._PauseButton.Size = new System.Drawing.Size(28, 28);
            this._PauseButton.Text = "暂停 发送与监听数据";
            this._PauseButton.Click += new System.EventHandler(this._PauseButton_Click);
            // 
            // _StopButton
            // 
            this._StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.stop;
            this._StopButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopButton.Margin = new System.Windows.Forms.Padding(3);
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(28, 28);
            this._StopButton.Text = "停止 发送与监听数据";
            this._StopButton.Click += new System.EventHandler(this._StopButton_Click);
            // 
            // _QuestionsEditorButton
            // 
            this._QuestionsEditorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._QuestionsEditorButton.Image = global::NKnife.Kits.ChannelKit.Properties.Resources.protocol;
            this._QuestionsEditorButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._QuestionsEditorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._QuestionsEditorButton.Margin = new System.Windows.Forms.Padding(3);
            this._QuestionsEditorButton.Name = "_QuestionsEditorButton";
            this._QuestionsEditorButton.Size = new System.Drawing.Size(28, 28);
            this._QuestionsEditorButton.Text = "编辑发送的数据";
            this._QuestionsEditorButton.Click += new System.EventHandler(this._QuestionsEditorButton_Click);
            // 
            // SingleSerialPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListBox);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.Name = "SingleSerialPanel";
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
        private System.Windows.Forms.ToolStripButton _ConfigurePortButton;
        private System.Windows.Forms.ToolStripStatusLabel _InfoLabel;
        private System.Windows.Forms.ToolStripStatusLabel _SpaceLabel;
        private System.Windows.Forms.ToolStripStatusLabel _PortLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _QuestionsEditorButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private System.Windows.Forms.ToolStripButton _OpenPortButton;
        private System.Windows.Forms.ToolStripButton _ClosePortButton;
    }
}
