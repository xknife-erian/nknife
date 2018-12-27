namespace NKnife.ChannelKnife.Views.Controls
{
    sealed partial class QuestionsEditorPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._IsHexDisplayCheckBox = new System.Windows.Forms.CheckBox();
            this._QuestionTabControl = new System.Windows.Forms.TabControl();
            this._SinglePage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this._LoopTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._IsSingleLoopCheckBox = new System.Windows.Forms.CheckBox();
            this._SingleAskButton = new System.Windows.Forms.Button();
            this._SingleQuestionTextbox = new System.Windows.Forms.TextBox();
            this._MultitermPage = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._UserPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._QuestionTabControl.SuspendLayout();
            this._SinglePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopTimeNumericUpDown)).BeginInit();
            this._MultitermPage.SuspendLayout();
            this._UserPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 221);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._IsHexDisplayCheckBox);
            this.groupBox1.Controls.Add(this._QuestionTabControl);
            this.groupBox1.Location = new System.Drawing.Point(27, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送";
            // 
            // _IsHexDisplayCheckBox
            // 
            this._IsHexDisplayCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._IsHexDisplayCheckBox.AutoSize = true;
            this._IsHexDisplayCheckBox.Location = new System.Drawing.Point(10, 191);
            this._IsHexDisplayCheckBox.Name = "_IsHexDisplayCheckBox";
            this._IsHexDisplayCheckBox.Size = new System.Drawing.Size(89, 17);
            this._IsHexDisplayCheckBox.TabIndex = 5;
            this._IsHexDisplayCheckBox.Text = "HEX(16进制)";
            this._IsHexDisplayCheckBox.UseVisualStyleBackColor = true;
            // 
            // _QuestionTabControl
            // 
            this._QuestionTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._QuestionTabControl.Controls.Add(this._SinglePage);
            this._QuestionTabControl.Controls.Add(this._MultitermPage);
            this._QuestionTabControl.Controls.Add(this._UserPage);
            this._QuestionTabControl.ItemSize = new System.Drawing.Size(58, 22);
            this._QuestionTabControl.Location = new System.Drawing.Point(6, 20);
            this._QuestionTabControl.Name = "_QuestionTabControl";
            this._QuestionTabControl.SelectedIndex = 0;
            this._QuestionTabControl.Size = new System.Drawing.Size(302, 170);
            this._QuestionTabControl.TabIndex = 4;
            // 
            // _SinglePage
            // 
            this._SinglePage.Controls.Add(this.label1);
            this._SinglePage.Controls.Add(this._LoopTimeNumericUpDown);
            this._SinglePage.Controls.Add(this._IsSingleLoopCheckBox);
            this._SinglePage.Controls.Add(this._SingleAskButton);
            this._SinglePage.Controls.Add(this._SingleQuestionTextbox);
            this._SinglePage.Location = new System.Drawing.Point(4, 26);
            this._SinglePage.Name = "_SinglePage";
            this._SinglePage.Padding = new System.Windows.Forms.Padding(6);
            this._SinglePage.Size = new System.Drawing.Size(294, 140);
            this._SinglePage.TabIndex = 0;
            this._SinglePage.Text = "单条";
            this._SinglePage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ms";
            // 
            // _LoopTimeNumericUpDown
            // 
            this._LoopTimeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._LoopTimeNumericUpDown.Location = new System.Drawing.Point(55, 114);
            this._LoopTimeNumericUpDown.Maximum = new decimal(new int[] {
            864000000,
            0,
            0,
            0});
            this._LoopTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._LoopTimeNumericUpDown.Name = "_LoopTimeNumericUpDown";
            this._LoopTimeNumericUpDown.Size = new System.Drawing.Size(75, 21);
            this._LoopTimeNumericUpDown.TabIndex = 3;
            this._LoopTimeNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // _IsSingleLoopCheckBox
            // 
            this._IsSingleLoopCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._IsSingleLoopCheckBox.AutoSize = true;
            this._IsSingleLoopCheckBox.Location = new System.Drawing.Point(6, 116);
            this._IsSingleLoopCheckBox.Name = "_IsSingleLoopCheckBox";
            this._IsSingleLoopCheckBox.Size = new System.Drawing.Size(54, 17);
            this._IsSingleLoopCheckBox.TabIndex = 2;
            this._IsSingleLoopCheckBox.Text = "定时:";
            this._IsSingleLoopCheckBox.UseVisualStyleBackColor = true;
            // 
            // _SingleAskButton
            // 
            this._SingleAskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._SingleAskButton.Enabled = false;
            this._SingleAskButton.Location = new System.Drawing.Point(214, 111);
            this._SingleAskButton.Name = "_SingleAskButton";
            this._SingleAskButton.Size = new System.Drawing.Size(75, 26);
            this._SingleAskButton.TabIndex = 1;
            this._SingleAskButton.Text = "发送(&S)";
            this._SingleAskButton.UseVisualStyleBackColor = true;
            // 
            // _SingleQuestionTextbox
            // 
            this._SingleQuestionTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._SingleQuestionTextbox.Location = new System.Drawing.Point(6, 6);
            this._SingleQuestionTextbox.MaxLength = 327670;
            this._SingleQuestionTextbox.Multiline = true;
            this._SingleQuestionTextbox.Name = "_SingleQuestionTextbox";
            this._SingleQuestionTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._SingleQuestionTextbox.Size = new System.Drawing.Size(281, 103);
            this._SingleQuestionTextbox.TabIndex = 0;
            // 
            // _MultitermPage
            // 
            this._MultitermPage.Controls.Add(this.checkBox1);
            this._MultitermPage.Location = new System.Drawing.Point(4, 26);
            this._MultitermPage.Name = "_MultitermPage";
            this._MultitermPage.Padding = new System.Windows.Forms.Padding(3);
            this._MultitermPage.Size = new System.Drawing.Size(294, 140);
            this._MultitermPage.TabIndex = 1;
            this._MultitermPage.Text = "多条";
            this._MultitermPage.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(77, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // _UserPage
            // 
            this._UserPage.Controls.Add(this.button1);
            this._UserPage.Location = new System.Drawing.Point(4, 26);
            this._UserPage.Name = "_UserPage";
            this._UserPage.Padding = new System.Windows.Forms.Padding(3);
            this._UserPage.Size = new System.Drawing.Size(294, 140);
            this._UserPage.TabIndex = 2;
            this._UserPage.Text = "协议";
            this._UserPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // QuestionsEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "QuestionsEditorPanel";
            this.Size = new System.Drawing.Size(364, 221);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._QuestionTabControl.ResumeLayout(false);
            this._SinglePage.ResumeLayout(false);
            this._SinglePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopTimeNumericUpDown)).EndInit();
            this._MultitermPage.ResumeLayout(false);
            this._MultitermPage.PerformLayout();
            this._UserPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _IsHexDisplayCheckBox;
        private System.Windows.Forms.TabControl _QuestionTabControl;
        private System.Windows.Forms.TabPage _SinglePage;
        private System.Windows.Forms.TextBox _SingleQuestionTextbox;
        private System.Windows.Forms.TabPage _MultitermPage;
        private System.Windows.Forms.TabPage _UserPage;
        private System.Windows.Forms.Button _SingleAskButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _LoopTimeNumericUpDown;
        private System.Windows.Forms.CheckBox _IsSingleLoopCheckBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
    }
}
