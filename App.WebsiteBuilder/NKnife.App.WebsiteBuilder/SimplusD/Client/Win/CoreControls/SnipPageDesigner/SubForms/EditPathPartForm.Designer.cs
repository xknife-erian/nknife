namespace Jeelu.SimplusD.Client.Win
{
    partial class EditPathPartForm
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
            this.groupBoxSelectDisplayType = new System.Windows.Forms.GroupBox();
            this.radioButtonDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonTitle = new System.Windows.Forms.RadioButton();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.radioButtonPic = new System.Windows.Forms.RadioButton();
            this.groupBoxSeparatorCode = new System.Windows.Forms.GroupBox();
            this.textBoxSeparatorCode = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxSelectDisplayType.SuspendLayout();
            this.groupBoxSeparatorCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSelectDisplayType
            // 
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonDefault);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonTitle);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonText);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonPic);
            this.groupBoxSelectDisplayType.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSelectDisplayType.Name = "groupBoxSelectDisplayType";
            this.groupBoxSelectDisplayType.Size = new System.Drawing.Size(194, 79);
            this.groupBoxSelectDisplayType.TabIndex = 1;
            this.groupBoxSelectDisplayType.TabStop = false;
            this.groupBoxSelectDisplayType.Text = "SelectDisplayType";
            // 
            // radioButtonDefault
            // 
            this.radioButtonDefault.AutoSize = true;
            this.radioButtonDefault.Checked = true;
            this.radioButtonDefault.Location = new System.Drawing.Point(6, 20);
            this.radioButtonDefault.Name = "radioButtonDefault";
            this.radioButtonDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButtonDefault.TabIndex = 3;
            this.radioButtonDefault.TabStop = true;
            this.radioButtonDefault.Text = "default";
            this.radioButtonDefault.UseVisualStyleBackColor = true;
            this.radioButtonDefault.CheckedChanged += new System.EventHandler(this.radioButtonDefault_CheckedChanged);
            // 
            // radioButtonTitle
            // 
            this.radioButtonTitle.AutoSize = true;
            this.radioButtonTitle.Location = new System.Drawing.Point(109, 20);
            this.radioButtonTitle.Name = "radioButtonTitle";
            this.radioButtonTitle.Size = new System.Drawing.Size(43, 17);
            this.radioButtonTitle.TabIndex = 2;
            this.radioButtonTitle.Text = "title";
            this.radioButtonTitle.UseVisualStyleBackColor = true;
            this.radioButtonTitle.CheckedChanged += new System.EventHandler(this.radioButtonTitle_CheckedChanged);
            // 
            // radioButtonText
            // 
            this.radioButtonText.AutoSize = true;
            this.radioButtonText.Location = new System.Drawing.Point(107, 52);
            this.radioButtonText.Name = "radioButtonText";
            this.radioButtonText.Size = new System.Drawing.Size(45, 17);
            this.radioButtonText.TabIndex = 1;
            this.radioButtonText.Text = "text";
            this.radioButtonText.UseVisualStyleBackColor = true;
            this.radioButtonText.CheckedChanged += new System.EventHandler(this.radioButtonText_CheckedChanged);
            // 
            // radioButtonPic
            // 
            this.radioButtonPic.AutoSize = true;
            this.radioButtonPic.Location = new System.Drawing.Point(6, 52);
            this.radioButtonPic.Name = "radioButtonPic";
            this.radioButtonPic.Size = new System.Drawing.Size(58, 17);
            this.radioButtonPic.TabIndex = 0;
            this.radioButtonPic.Text = "pictrue";
            this.radioButtonPic.UseVisualStyleBackColor = true;
            this.radioButtonPic.CheckedChanged += new System.EventHandler(this.radioButtonPic_CheckedChanged);
            // 
            // groupBoxSeparatorCode
            // 
            this.groupBoxSeparatorCode.Controls.Add(this.textBoxSeparatorCode);
            this.groupBoxSeparatorCode.Location = new System.Drawing.Point(12, 97);
            this.groupBoxSeparatorCode.Name = "groupBoxSeparatorCode";
            this.groupBoxSeparatorCode.Size = new System.Drawing.Size(194, 53);
            this.groupBoxSeparatorCode.TabIndex = 2;
            this.groupBoxSeparatorCode.TabStop = false;
            this.groupBoxSeparatorCode.Text = "间隔符号";
            // 
            // textBoxSeparatorCode
            // 
            this.textBoxSeparatorCode.Location = new System.Drawing.Point(6, 20);
            this.textBoxSeparatorCode.Name = "textBoxSeparatorCode";
            this.textBoxSeparatorCode.Size = new System.Drawing.Size(178, 21);
            this.textBoxSeparatorCode.TabIndex = 0;
            this.textBoxSeparatorCode.Text = ">>";
            this.textBoxSeparatorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSeparatorCode.Leave += new System.EventHandler(this.textBoxSeparatorCode_Leave);
            // 
            // buttonEnter
            // 
            this.buttonEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonEnter.Location = new System.Drawing.Point(105, 156);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(46, 23);
            this.buttonEnter.TabIndex = 3;
            this.buttonEnter.Text = "确 定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(157, 156);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(46, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // EditPathPartForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 187);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.groupBoxSeparatorCode);
            this.Controls.Add(this.groupBoxSelectDisplayType);
            this.Name = "EditPathPartForm";
            this.Text = "编辑面包屑型块";
            this.groupBoxSelectDisplayType.ResumeLayout(false);
            this.groupBoxSelectDisplayType.PerformLayout();
            this.groupBoxSeparatorCode.ResumeLayout(false);
            this.groupBoxSeparatorCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSelectDisplayType;
        private System.Windows.Forms.RadioButton radioButtonDefault;
        private System.Windows.Forms.RadioButton radioButtonTitle;
        private System.Windows.Forms.RadioButton radioButtonText;
        private System.Windows.Forms.RadioButton radioButtonPic;
        private System.Windows.Forms.GroupBox groupBoxSeparatorCode;
        private System.Windows.Forms.TextBox textBoxSeparatorCode;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
    }
}