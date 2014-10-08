namespace NKnife.App.PictureTextPicker
{
    partial class AppOptionSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FixThumbSizeFalseRadioBox = new System.Windows.Forms.RadioButton();
            this.FixThumbSizeTureRadioBox = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.PictureTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ThumbHeightTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ThumbWidthTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FixThumbSizeFalseRadioBox);
            this.groupBox1.Controls.Add(this.FixThumbSizeTureRadioBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.PictureTypeComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ThumbHeightTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ThumbWidthTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 112);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "缩略图设置";
            // 
            // FixThumbSizeFalseRadioBox
            // 
            this.FixThumbSizeFalseRadioBox.AutoSize = true;
            this.FixThumbSizeFalseRadioBox.Checked = true;
            this.FixThumbSizeFalseRadioBox.Location = new System.Drawing.Point(132, 54);
            this.FixThumbSizeFalseRadioBox.Name = "FixThumbSizeFalseRadioBox";
            this.FixThumbSizeFalseRadioBox.Size = new System.Drawing.Size(35, 16);
            this.FixThumbSizeFalseRadioBox.TabIndex = 11;
            this.FixThumbSizeFalseRadioBox.TabStop = true;
            this.FixThumbSizeFalseRadioBox.Text = "否";
            this.FixThumbSizeFalseRadioBox.UseVisualStyleBackColor = true;
            // 
            // FixThumbSizeTureRadioBox
            // 
            this.FixThumbSizeTureRadioBox.AutoSize = true;
            this.FixThumbSizeTureRadioBox.Location = new System.Drawing.Point(97, 54);
            this.FixThumbSizeTureRadioBox.Name = "FixThumbSizeTureRadioBox";
            this.FixThumbSizeTureRadioBox.Size = new System.Drawing.Size(35, 16);
            this.FixThumbSizeTureRadioBox.TabIndex = 10;
            this.FixThumbSizeTureRadioBox.Text = "是";
            this.FixThumbSizeTureRadioBox.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "是否固定尺寸：";
            // 
            // PictureTypeComboBox
            // 
            this.PictureTypeComboBox.FormattingEnabled = true;
            this.PictureTypeComboBox.Items.AddRange(new object[] {
            "*.jpg",
            "*.png",
            "*.gif",
            "*.tif",
            "*.bmp"});
            this.PictureTypeComboBox.Location = new System.Drawing.Point(49, 81);
            this.PictureTypeComboBox.Name = "PictureTypeComboBox";
            this.PictureTypeComboBox.Size = new System.Drawing.Size(71, 20);
            this.PictureTypeComboBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "格式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "高：";
            // 
            // ThumbHeightTextBox
            // 
            this.ThumbHeightTextBox.Location = new System.Drawing.Point(142, 23);
            this.ThumbHeightTextBox.Name = "ThumbHeightTextBox";
            this.ThumbHeightTextBox.Size = new System.Drawing.Size(62, 21);
            this.ThumbHeightTextBox.TabIndex = 6;
            this.ThumbHeightTextBox.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "长：";
            // 
            // ThumbWidthTextBox
            // 
            this.ThumbWidthTextBox.Location = new System.Drawing.Point(39, 23);
            this.ThumbWidthTextBox.Name = "ThumbWidthTextBox";
            this.ThumbWidthTextBox.Size = new System.Drawing.Size(62, 21);
            this.ThumbWidthTextBox.TabIndex = 4;
            this.ThumbWidthTextBox.Text = "180";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(127, 140);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 29);
            this.OkButton.TabIndex = 14;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(221, 140);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 29);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // AppOptionSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 181);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppOptionSetting";
            this.Text = "选项";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton FixThumbSizeFalseRadioBox;
        private System.Windows.Forms.RadioButton FixThumbSizeTureRadioBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox PictureTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ThumbHeightTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ThumbWidthTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
    }
}