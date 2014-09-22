namespace Jeelu.SimplusD.Client.Win
{
    partial class PartSetupForm
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
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.buttonBrowsePic = new System.Windows.Forms.Button();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.radioButtonDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonTitle = new System.Windows.Forms.RadioButton();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.radioButtonPic = new System.Windows.Forms.RadioButton();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.cssFieldUnitWidth = new Jeelu.Win.CssFieldUnit();
            this.groupBoxWidth = new System.Windows.Forms.GroupBox();
            this.groupBoxSelectDisplayType.SuspendLayout();
            this.groupBoxWidth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSelectDisplayType
            // 
            this.groupBoxSelectDisplayType.Controls.Add(this.textBoxText);
            this.groupBoxSelectDisplayType.Controls.Add(this.buttonBrowsePic);
            this.groupBoxSelectDisplayType.Controls.Add(this.textBoxTitle);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonDefault);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonTitle);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonText);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonPic);
            this.groupBoxSelectDisplayType.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSelectDisplayType.Name = "groupBoxSelectDisplayType";
            this.groupBoxSelectDisplayType.Size = new System.Drawing.Size(194, 134);
            this.groupBoxSelectDisplayType.TabIndex = 0;
            this.groupBoxSelectDisplayType.TabStop = false;
            this.groupBoxSelectDisplayType.Text = "SelectDisplayType";
            // 
            // textBoxText
            // 
            this.textBoxText.Enabled = false;
            this.textBoxText.Location = new System.Drawing.Point(84, 101);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(100, 21);
            this.textBoxText.TabIndex = 6;
            this.textBoxText.Leave += new System.EventHandler(this.textBoxText_Leave);
            // 
            // buttonBrowsePic
            // 
            this.buttonBrowsePic.Enabled = false;
            this.buttonBrowsePic.Location = new System.Drawing.Point(111, 72);
            this.buttonBrowsePic.Name = "buttonBrowsePic";
            this.buttonBrowsePic.Size = new System.Drawing.Size(73, 23);
            this.buttonBrowsePic.TabIndex = 5;
            this.buttonBrowsePic.Text = "browse";
            this.buttonBrowsePic.UseVisualStyleBackColor = true;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Enabled = false;
            this.textBoxTitle.Location = new System.Drawing.Point(84, 45);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(100, 21);
            this.textBoxTitle.TabIndex = 4;
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
            this.radioButtonTitle.Location = new System.Drawing.Point(6, 46);
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
            this.radioButtonText.Location = new System.Drawing.Point(6, 102);
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
            this.radioButtonPic.Location = new System.Drawing.Point(6, 75);
            this.radioButtonPic.Name = "radioButtonPic";
            this.radioButtonPic.Size = new System.Drawing.Size(58, 17);
            this.radioButtonPic.TabIndex = 0;
            this.radioButtonPic.Text = "pictrue";
            this.radioButtonPic.UseVisualStyleBackColor = true;
            this.radioButtonPic.CheckedChanged += new System.EventHandler(this.radioButtonPic_CheckedChanged);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonEnter.Location = new System.Drawing.Point(90, 211);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(55, 23);
            this.buttonEnter.TabIndex = 6;
            this.buttonEnter.Text = "enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(151, 211);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(55, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // cssFieldUnitWidth
            // 
            this.cssFieldUnitWidth.FieldUnitType = Jeelu.Win.CssFieldUnitType.Part;
            this.cssFieldUnitWidth.Location = new System.Drawing.Point(12, 20);
            this.cssFieldUnitWidth.Name = "cssFieldUnitWidth";
            this.cssFieldUnitWidth.Size = new System.Drawing.Size(172, 25);
            this.cssFieldUnitWidth.TabIndex = 14;
            this.cssFieldUnitWidth.Value = "";
            this.cssFieldUnitWidth.ValueChanged += new System.EventHandler(this.cssFieldUnitWidth_ValueChanged);
            // 
            // groupBoxWidth
            // 
            this.groupBoxWidth.Controls.Add(this.cssFieldUnitWidth);
            this.groupBoxWidth.Location = new System.Drawing.Point(12, 152);
            this.groupBoxWidth.Name = "groupBoxWidth";
            this.groupBoxWidth.Size = new System.Drawing.Size(194, 53);
            this.groupBoxWidth.TabIndex = 15;
            this.groupBoxWidth.TabStop = false;
            this.groupBoxWidth.Text = "Width";
            // 
            // PartSetupForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 246);
            this.Controls.Add(this.groupBoxWidth);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.groupBoxSelectDisplayType);
            this.Name = "PartSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PartSetupForm";
            this.groupBoxSelectDisplayType.ResumeLayout(false);
            this.groupBoxSelectDisplayType.PerformLayout();
            this.groupBoxWidth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSelectDisplayType;
        private System.Windows.Forms.RadioButton radioButtonDefault;
        private System.Windows.Forms.RadioButton radioButtonTitle;
        private System.Windows.Forms.RadioButton radioButtonText;
        private System.Windows.Forms.RadioButton radioButtonPic;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button buttonBrowsePic;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
        private Jeelu.Win.CssFieldUnit cssFieldUnitWidth;
        private System.Windows.Forms.GroupBox groupBoxWidth;
    }
}