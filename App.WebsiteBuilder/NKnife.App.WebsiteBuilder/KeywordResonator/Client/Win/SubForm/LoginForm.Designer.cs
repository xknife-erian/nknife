namespace Jeelu.KeywordResonator.Client
{
    partial class LoginForm
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
            this._OkButton = new System.Windows.Forms.Button();
            this._InformationLabel = new System.Windows.Forms.Label();
            this._PwdTextBox = new System.Windows.Forms.TextBox();
            this._ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(238, 128);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(68, 23);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _OkButton
            // 
            this._OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._OkButton.Location = new System.Drawing.Point(164, 128);
            this._OkButton.Name = "_OkButton";
            this._OkButton.Size = new System.Drawing.Size(68, 23);
            this._OkButton.TabIndex = 1;
            this._OkButton.Text = "OK";
            this._OkButton.UseVisualStyleBackColor = true;
            this._OkButton.Click += new System.EventHandler(this._OkButton_Click);
            // 
            // _InformationLabel
            // 
            this._InformationLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._InformationLabel.Location = new System.Drawing.Point(20, 10);
            this._InformationLabel.Name = "_InformationLabel";
            this._InformationLabel.Size = new System.Drawing.Size(286, 32);
            this._InformationLabel.TabIndex = 4;
            this._InformationLabel.Text = "Suggestive Information";
            this._InformationLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // _PwdTextBox
            // 
            this._PwdTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._PwdTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._PwdTextBox.Location = new System.Drawing.Point(20, 42);
            this._PwdTextBox.Name = "_PwdTextBox";
            this._PwdTextBox.PasswordChar = '*';
            this._PwdTextBox.Size = new System.Drawing.Size(286, 26);
            this._PwdTextBox.TabIndex = 0;
            this._PwdTextBox.TextChanged += new System.EventHandler(this._PwdTextBox_TextChanged);
            // 
            // _ErrorLabel
            // 
            this._ErrorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._ErrorLabel.Location = new System.Drawing.Point(20, 68);
            this._ErrorLabel.Name = "_ErrorLabel";
            this._ErrorLabel.Size = new System.Drawing.Size(286, 36);
            this._ErrorLabel.TabIndex = 5;
            this._ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoginForm
            // 
            this.AcceptButton = this._OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(326, 174);
            this.ControlBox = false;
            this.Controls.Add(this._ErrorLabel);
            this.Controls.Add(this._PwdTextBox);
            this.Controls.Add(this._InformationLabel);
            this.Controls.Add(this._OkButton);
            this.Controls.Add(this._CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _OkButton;
        private System.Windows.Forms.Label _InformationLabel;
        private System.Windows.Forms.TextBox _PwdTextBox;
        private System.Windows.Forms.Label _ErrorLabel;
    }
}