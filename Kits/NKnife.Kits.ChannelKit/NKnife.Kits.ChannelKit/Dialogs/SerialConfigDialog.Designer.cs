namespace NKnife.Kits.ChannelKit.Dialogs
{
    partial class SerialConfigDialog
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
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._IsFormatTextViewCheckBox = new System.Windows.Forms.CheckBox();
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
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._BufferSpaceBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(256, 297);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 29);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._AcceptButton.Location = new System.Drawing.Point(161, 297);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(87, 29);
            this._AcceptButton.TabIndex = 2;
            this._AcceptButton.Text = "确定(&A)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._IsFormatTextViewCheckBox);
            this.groupBox2.Controls.Add(this._IsHexViewCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(27, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 73);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // _IsFormatTextViewCheckBox
            // 
            this._IsFormatTextViewCheckBox.AutoSize = true;
            this._IsFormatTextViewCheckBox.Enabled = false;
            this._IsFormatTextViewCheckBox.Location = new System.Drawing.Point(50, 42);
            this._IsFormatTextViewCheckBox.Name = "_IsFormatTextViewCheckBox";
            this._IsFormatTextViewCheckBox.Size = new System.Drawing.Size(110, 17);
            this._IsFormatTextViewCheckBox.TabIndex = 9;
            this._IsFormatTextViewCheckBox.Text = "以格式文本显示";
            this._IsFormatTextViewCheckBox.UseVisualStyleBackColor = true;
            // 
            // _IsHexViewCheckBox
            // 
            this._IsHexViewCheckBox.AutoSize = true;
            this._IsHexViewCheckBox.Checked = true;
            this._IsHexViewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._IsHexViewCheckBox.Location = new System.Drawing.Point(50, 23);
            this._IsHexViewCheckBox.Name = "_IsHexViewCheckBox";
            this._IsHexViewCheckBox.Size = new System.Drawing.Size(121, 17);
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
            this.groupBox1.Location = new System.Drawing.Point(27, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 82);
            this.groupBox1.TabIndex = 28;
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
            this._IsDTRCheckBox.Size = new System.Drawing.Size(50, 17);
            this._IsDTRCheckBox.TabIndex = 9;
            this._IsDTRCheckBox.Text = "DTR";
            this._IsDTRCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "接收缓冲区大小:";
            // 
            // _IsRTSCheckBox
            // 
            this._IsRTSCheckBox.AutoSize = true;
            this._IsRTSCheckBox.Location = new System.Drawing.Point(50, 27);
            this._IsRTSCheckBox.Name = "_IsRTSCheckBox";
            this._IsRTSCheckBox.Size = new System.Drawing.Size(49, 17);
            this._IsRTSCheckBox.TabIndex = 0;
            this._IsRTSCheckBox.Text = "RTS";
            this._IsRTSCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(193, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "byte";
            // 
            // _StopBitsesComboBox
            // 
            this._StopBitsesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._StopBitsesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._StopBitsesComboBox.FormattingEnabled = true;
            this._StopBitsesComboBox.Location = new System.Drawing.Point(77, 101);
            this._StopBitsesComboBox.MaxDropDownItems = 6;
            this._StopBitsesComboBox.Name = "_StopBitsesComboBox";
            this._StopBitsesComboBox.Size = new System.Drawing.Size(264, 21);
            this._StopBitsesComboBox.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "停止位:";
            // 
            // _DatabitComboBox
            // 
            this._DatabitComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._DatabitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._DatabitComboBox.FormattingEnabled = true;
            this._DatabitComboBox.Location = new System.Drawing.Point(77, 74);
            this._DatabitComboBox.MaxDropDownItems = 6;
            this._DatabitComboBox.Name = "_DatabitComboBox";
            this._DatabitComboBox.Size = new System.Drawing.Size(264, 21);
            this._DatabitComboBox.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "数据位:";
            // 
            // _ParitysComboBox
            // 
            this._ParitysComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ParitysComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ParitysComboBox.FormattingEnabled = true;
            this._ParitysComboBox.Location = new System.Drawing.Point(77, 47);
            this._ParitysComboBox.MaxDropDownItems = 6;
            this._ParitysComboBox.Name = "_ParitysComboBox";
            this._ParitysComboBox.Size = new System.Drawing.Size(264, 21);
            this._ParitysComboBox.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "校验位:";
            // 
            // _BaudRatesComboBox
            // 
            this._BaudRatesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._BaudRatesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._BaudRatesComboBox.FormattingEnabled = true;
            this._BaudRatesComboBox.Location = new System.Drawing.Point(77, 20);
            this._BaudRatesComboBox.MaxDropDownItems = 6;
            this._BaudRatesComboBox.Name = "_BaudRatesComboBox";
            this._BaudRatesComboBox.Size = new System.Drawing.Size(264, 21);
            this._BaudRatesComboBox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "波特率:";
            // 
            // SerialConfigDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(368, 344);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._StopBitsesComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._DatabitComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._ParitysComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._BaudRatesComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialConfigDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "端口连接配置";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._BufferSpaceBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox _IsFormatTextViewCheckBox;
        private System.Windows.Forms.CheckBox _IsHexViewCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown _BufferSpaceBox;
        private System.Windows.Forms.CheckBox _IsDTRCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox _IsRTSCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox _StopBitsesComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _DatabitComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _ParitysComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _BaudRatesComboBox;
        private System.Windows.Forms.Label label1;
    }
}