namespace NKnife.Kits.StarterKit.Forms
{
    partial class ThreadTimerTestForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._ListBox = new System.Windows.Forms.ListBox();
            this._StartButton = new System.Windows.Forms.Button();
            this._ChangeButton = new System.Windows.Forms.Button();
            this._PeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._BaseNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._DuTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._StopBbutton = new System.Windows.Forms.Button();
            this._ClearLogButton = new System.Windows.Forms.Button();
            this._CheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._PeriodNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._BaseNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._DuTimeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // _ListBox
            // 
            this._ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListBox.FormattingEnabled = true;
            this._ListBox.Location = new System.Drawing.Point(15, 14);
            this._ListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ListBox.Name = "_ListBox";
            this._ListBox.Size = new System.Drawing.Size(340, 485);
            this._ListBox.TabIndex = 0;
            // 
            // _StartButton
            // 
            this._StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._StartButton.Location = new System.Drawing.Point(363, 65);
            this._StartButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(111, 45);
            this._StartButton.TabIndex = 1;
            this._StartButton.Text = "Start";
            this._StartButton.UseVisualStyleBackColor = true;
            // 
            // _ChangeButton
            // 
            this._ChangeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ChangeButton.Enabled = false;
            this._ChangeButton.Location = new System.Drawing.Point(364, 348);
            this._ChangeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ChangeButton.Name = "_ChangeButton";
            this._ChangeButton.Size = new System.Drawing.Size(111, 45);
            this._ChangeButton.TabIndex = 2;
            this._ChangeButton.Text = "Change";
            this._ChangeButton.UseVisualStyleBackColor = true;
            // 
            // _PeriodNumericUpDown
            // 
            this._PeriodNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._PeriodNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this._PeriodNumericUpDown.Location = new System.Drawing.Point(365, 321);
            this._PeriodNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._PeriodNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._PeriodNumericUpDown.Name = "_PeriodNumericUpDown";
            this._PeriodNumericUpDown.Size = new System.Drawing.Size(110, 21);
            this._PeriodNumericUpDown.TabIndex = 5;
            this._PeriodNumericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // _BaseNumericUpDown
            // 
            this._BaseNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BaseNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this._BaseNumericUpDown.Location = new System.Drawing.Point(364, 14);
            this._BaseNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._BaseNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._BaseNumericUpDown.Name = "_BaseNumericUpDown";
            this._BaseNumericUpDown.Size = new System.Drawing.Size(110, 21);
            this._BaseNumericUpDown.TabIndex = 6;
            this._BaseNumericUpDown.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // _DuTimeNumericUpDown
            // 
            this._DuTimeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DuTimeNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this._DuTimeNumericUpDown.Location = new System.Drawing.Point(365, 294);
            this._DuTimeNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._DuTimeNumericUpDown.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this._DuTimeNumericUpDown.Name = "_DuTimeNumericUpDown";
            this._DuTimeNumericUpDown.Size = new System.Drawing.Size(110, 21);
            this._DuTimeNumericUpDown.TabIndex = 7;
            this._DuTimeNumericUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // _StopBbutton
            // 
            this._StopBbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._StopBbutton.Enabled = false;
            this._StopBbutton.Location = new System.Drawing.Point(364, 453);
            this._StopBbutton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._StopBbutton.Name = "_StopBbutton";
            this._StopBbutton.Size = new System.Drawing.Size(111, 45);
            this._StopBbutton.TabIndex = 8;
            this._StopBbutton.Text = "Stop";
            this._StopBbutton.UseVisualStyleBackColor = true;
            // 
            // _ClearLogButton
            // 
            this._ClearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearLogButton.Location = new System.Drawing.Point(364, 423);
            this._ClearLogButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._ClearLogButton.Name = "_ClearLogButton";
            this._ClearLogButton.Size = new System.Drawing.Size(111, 24);
            this._ClearLogButton.TabIndex = 9;
            this._ClearLogButton.Text = "Clear Log";
            this._ClearLogButton.UseVisualStyleBackColor = true;
            // 
            // _CheckBox
            // 
            this._CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._CheckBox.AutoSize = true;
            this._CheckBox.Location = new System.Drawing.Point(365, 42);
            this._CheckBox.Name = "_CheckBox";
            this._CheckBox.Size = new System.Drawing.Size(69, 17);
            this._CheckBox.TabIndex = 10;
            this._CheckBox.Text = "逢5切换";
            this._CheckBox.UseVisualStyleBackColor = true;
            // 
            // ThreadTimerTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 510);
            this.Controls.Add(this._CheckBox);
            this.Controls.Add(this._ClearLogButton);
            this.Controls.Add(this._StopBbutton);
            this.Controls.Add(this._DuTimeNumericUpDown);
            this.Controls.Add(this._BaseNumericUpDown);
            this.Controls.Add(this._PeriodNumericUpDown);
            this.Controls.Add(this._ChangeButton);
            this.Controls.Add(this._StartButton);
            this.Controls.Add(this._ListBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ThreadTimerTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThreadTimerTest";
            ((System.ComponentModel.ISupportInitialize)(this._PeriodNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._BaseNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._DuTimeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _ListBox;
        private System.Windows.Forms.Button _StartButton;
        private System.Windows.Forms.Button _ChangeButton;
        private System.Windows.Forms.NumericUpDown _PeriodNumericUpDown;
        private System.Windows.Forms.NumericUpDown _BaseNumericUpDown;
        private System.Windows.Forms.NumericUpDown _DuTimeNumericUpDown;
        private System.Windows.Forms.Button _StopBbutton;
        private System.Windows.Forms.Button _ClearLogButton;
        private System.Windows.Forms.CheckBox _CheckBox;
    }
}

