namespace NKnife.ChannelKnife.Views.Controls
{
    partial class ConfigPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._IsFormatTextCheckBox = new System.Windows.Forms.CheckBox();
            this._IsHexViewCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._BufferSpaceBox = new System.Windows.Forms.NumericUpDown();
            this._IsDTRCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this._IsRTSCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this._StopBitsesComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._DatabitComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._ParitysComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._BaudRatesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._BufferSpaceBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this._StopBitsesComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._DatabitComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this._ParitysComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._BaudRatesComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 294);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._IsFormatTextCheckBox);
            this.groupBox2.Controls.Add(this._IsHexViewCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(27, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 73);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // _IsFormatTextCheckBox
            // 
            this._IsFormatTextCheckBox.AutoSize = true;
            this._IsFormatTextCheckBox.Enabled = false;
            this._IsFormatTextCheckBox.Location = new System.Drawing.Point(50, 42);
            this._IsFormatTextCheckBox.Name = "_IsFormatTextCheckBox";
            this._IsFormatTextCheckBox.Size = new System.Drawing.Size(110, 17);
            this._IsFormatTextCheckBox.TabIndex = 9;
            this._IsFormatTextCheckBox.Text = "以格式文本显示";
            this._IsFormatTextCheckBox.UseVisualStyleBackColor = true;
            // 
            // _IsHexViewCheckBox
            // 
            this._IsHexViewCheckBox.AutoSize = true;
            this._IsHexViewCheckBox.Checked = true;
            this._IsHexViewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._IsHexViewCheckBox.Location = new System.Drawing.Point(50, 23);
            this._IsHexViewCheckBox.Name = "_IsHexViewCheckBox";
            this._IsHexViewCheckBox.Size = new System.Drawing.Size(113, 17);
            this._IsHexViewCheckBox.TabIndex = 0;
            this._IsHexViewCheckBox.Text = "HEX(16进制)显示";
            this._IsHexViewCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._BufferSpaceBox);
            this.groupBox1.Controls.Add(this._IsDTRCheckBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._IsRTSCheckBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(27, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 82);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "高级配置";
            // 
            // _BufferSpaceBox
            // 
            this._BufferSpaceBox.Location = new System.Drawing.Point(140, 47);
            this._BufferSpaceBox.Maximum = new decimal(new int[] {
            20480,
            0,
            0,
            0});
            this._BufferSpaceBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._BufferSpaceBox.Name = "_BufferSpaceBox";
            this._BufferSpaceBox.Size = new System.Drawing.Size(52, 21);
            this._BufferSpaceBox.TabIndex = 11;
            this._BufferSpaceBox.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // _IsDTRCheckBox
            // 
            this._IsDTRCheckBox.AutoSize = true;
            this._IsDTRCheckBox.Location = new System.Drawing.Point(96, 27);
            this._IsDTRCheckBox.Name = "_IsDTRCheckBox";
            this._IsDTRCheckBox.Size = new System.Drawing.Size(46, 17);
            this._IsDTRCheckBox.TabIndex = 9;
            this._IsDTRCheckBox.Text = "DTR";
            this._IsDTRCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "接收缓冲区大小:";
            // 
            // _IsRTSCheckBox
            // 
            this._IsRTSCheckBox.AutoSize = true;
            this._IsRTSCheckBox.Location = new System.Drawing.Point(50, 27);
            this._IsRTSCheckBox.Name = "_IsRTSCheckBox";
            this._IsRTSCheckBox.Size = new System.Drawing.Size(45, 17);
            this._IsRTSCheckBox.TabIndex = 0;
            this._IsRTSCheckBox.Text = "RTS";
            this._IsRTSCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(193, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "byte";
            // 
            // _StopBitsesComboBox
            // 
            this._StopBitsesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._StopBitsesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._StopBitsesComboBox.FormattingEnabled = true;
            this._StopBitsesComboBox.Location = new System.Drawing.Point(77, 99);
            this._StopBitsesComboBox.MaxDropDownItems = 6;
            this._StopBitsesComboBox.Name = "_StopBitsesComboBox";
            this._StopBitsesComboBox.Size = new System.Drawing.Size(260, 21);
            this._StopBitsesComboBox.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "停止位:";
            // 
            // _DatabitComboBox
            // 
            this._DatabitComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DatabitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._DatabitComboBox.FormattingEnabled = true;
            this._DatabitComboBox.Location = new System.Drawing.Point(77, 72);
            this._DatabitComboBox.MaxDropDownItems = 6;
            this._DatabitComboBox.Name = "_DatabitComboBox";
            this._DatabitComboBox.Size = new System.Drawing.Size(260, 21);
            this._DatabitComboBox.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "数据位:";
            // 
            // _ParitysComboBox
            // 
            this._ParitysComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ParitysComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ParitysComboBox.FormattingEnabled = true;
            this._ParitysComboBox.Location = new System.Drawing.Point(77, 45);
            this._ParitysComboBox.MaxDropDownItems = 6;
            this._ParitysComboBox.Name = "_ParitysComboBox";
            this._ParitysComboBox.Size = new System.Drawing.Size(260, 21);
            this._ParitysComboBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "校验位:";
            // 
            // _BaudRatesComboBox
            // 
            this._BaudRatesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._BaudRatesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._BaudRatesComboBox.FormattingEnabled = true;
            this._BaudRatesComboBox.Location = new System.Drawing.Point(77, 18);
            this._BaudRatesComboBox.MaxDropDownItems = 6;
            this._BaudRatesComboBox.Name = "_BaudRatesComboBox";
            this._BaudRatesComboBox.Size = new System.Drawing.Size(260, 21);
            this._BaudRatesComboBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "波特率:";
            // 
            // SerialConfigPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SerialConfigPanel";
            this.Size = new System.Drawing.Size(360, 294);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._BufferSpaceBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox _IsFormatTextCheckBox;
        private System.Windows.Forms.CheckBox _IsHexViewCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _IsDTRCheckBox;
        private System.Windows.Forms.CheckBox _IsRTSCheckBox;
        private System.Windows.Forms.ComboBox _StopBitsesComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _DatabitComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _ParitysComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _BaudRatesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _BufferSpaceBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
