namespace Jeelu.SimplusD.Client.Win
{
    partial class EditListBoxForm
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
            this.panelAttOption = new System.Windows.Forms.Panel();
            this.labOption = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelAttOption
            // 
            this.panelAttOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAttOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAttOption.Enabled = false;
            this.panelAttOption.Location = new System.Drawing.Point(7, 25);
            this.panelAttOption.Name = "panelAttOption";
            this.panelAttOption.Size = new System.Drawing.Size(411, 203);
            this.panelAttOption.TabIndex = 18;
            // 
            // labOption
            // 
            this.labOption.AutoSize = true;
            this.labOption.Location = new System.Drawing.Point(5, 9);
            this.labOption.Name = "labOption";
            this.labOption.Size = new System.Drawing.Size(115, 13);
            this.labOption.TabIndex = 17;
            this.labOption.Text = "选择要显示的内容：";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(306, 234);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(53, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(365, 234);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(53, 23);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // EditListBoxForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 261);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelAttOption);
            this.Controls.Add(this.labOption);
            this.Name = "EditListBoxForm";
            this.Text = "EditListBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelAttOption;
        private System.Windows.Forms.Label labOption;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}