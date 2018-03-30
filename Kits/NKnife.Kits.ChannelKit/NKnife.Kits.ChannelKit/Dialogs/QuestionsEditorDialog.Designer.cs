namespace NKnife.Kits.ChannelKit.Dialogs
{
    partial class QuestionsEditorDialog
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
            this._AcceptButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
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
            this.groupBox1.SuspendLayout();
            this._QuestionTabControl.SuspendLayout();
            this._SinglePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopTimeNumericUpDown)).BeginInit();
            this._MultitermPage.SuspendLayout();
            this._UserPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._AcceptButton.Location = new System.Drawing.Point(197, 229);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(102, 32);
            this._AcceptButton.TabIndex = 4;
            this._AcceptButton.Text = "确定(&A)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(308, 229);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(102, 32);
            this._CancelButton.TabIndex = 3;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._IsHexDisplayCheckBox);
            this.groupBox1.Controls.Add(this._QuestionTabControl);
            this.groupBox1.Location = new System.Drawing.Point(22, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 215);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送";
            // 
            // _IsHexDisplayCheckBox
            // 
            this._IsHexDisplayCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._IsHexDisplayCheckBox.AutoSize = true;
            this._IsHexDisplayCheckBox.Location = new System.Drawing.Point(10, 191);
            this._IsHexDisplayCheckBox.Name = "_IsHexDisplayCheckBox";
            this._IsHexDisplayCheckBox.Size = new System.Drawing.Size(97, 17);
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
            this._QuestionTabControl.Size = new System.Drawing.Size(375, 170);
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
            this._SinglePage.Size = new System.Drawing.Size(367, 140);
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
            this.label1.Size = new System.Drawing.Size(24, 13);
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
            this._IsSingleLoopCheckBox.Size = new System.Drawing.Size(55, 17);
            this._IsSingleLoopCheckBox.TabIndex = 2;
            this._IsSingleLoopCheckBox.Text = "定时:";
            this._IsSingleLoopCheckBox.UseVisualStyleBackColor = true;
            // 
            // _SingleAskButton
            // 
            this._SingleAskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._SingleAskButton.Enabled = false;
            this._SingleAskButton.Location = new System.Drawing.Point(287, 111);
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
            this._SingleQuestionTextbox.Size = new System.Drawing.Size(354, 103);
            this._SingleQuestionTextbox.TabIndex = 0;
            // 
            // _MultitermPage
            // 
            this._MultitermPage.Controls.Add(this.checkBox1);
            this._MultitermPage.Location = new System.Drawing.Point(4, 26);
            this._MultitermPage.Name = "_MultitermPage";
            this._MultitermPage.Padding = new System.Windows.Forms.Padding(3);
            this._MultitermPage.Size = new System.Drawing.Size(367, 140);
            this._MultitermPage.TabIndex = 1;
            this._MultitermPage.Text = "多条";
            this._MultitermPage.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 17);
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
            this._UserPage.Size = new System.Drawing.Size(367, 140);
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
            // QuestionsEditorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(433, 277);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuestionsEditorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "协议编辑";
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
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _IsHexDisplayCheckBox;
        private System.Windows.Forms.TabControl _QuestionTabControl;
        private System.Windows.Forms.TabPage _SinglePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _LoopTimeNumericUpDown;
        private System.Windows.Forms.CheckBox _IsSingleLoopCheckBox;
        private System.Windows.Forms.Button _SingleAskButton;
        private System.Windows.Forms.TextBox _SingleQuestionTextbox;
        private System.Windows.Forms.TabPage _MultitermPage;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage _UserPage;
        private System.Windows.Forms.Button button1;
    }
}