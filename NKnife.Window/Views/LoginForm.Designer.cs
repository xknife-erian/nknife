namespace NKnife.Window.Views
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
            this.label1 = new System.Windows.Forms.Label();
            this._LoginButton = new System.Windows.Forms.Button();
            this._LoginNameCmbox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._LoginPasswordTextbox = new System.Windows.Forms.TextBox();
            this._CancelButton = new System.Windows.Forms.Button();
            this._RememberCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._LoginNameTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._LoginFormTopPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoginFormTopPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "员工: ";
            // 
            // _LoginButton
            // 
            this._LoginButton.Location = new System.Drawing.Point(82, 29);
            this._LoginButton.Name = "_LoginButton";
            this._LoginButton.Size = new System.Drawing.Size(83, 32);
            this._LoginButton.TabIndex = 0;
            this._LoginButton.Text = "登录(&L)";
            this._LoginButton.UseVisualStyleBackColor = true;
            this._LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
            // 
            // _LoginNameCmbox
            // 
            this._LoginNameCmbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._LoginNameCmbox.FormattingEnabled = true;
            this._LoginNameCmbox.Location = new System.Drawing.Point(83, 17);
            this._LoginNameCmbox.Name = "_LoginNameCmbox";
            this._LoginNameCmbox.Size = new System.Drawing.Size(193, 21);
            this._LoginNameCmbox.TabIndex = 1;
            this._LoginNameCmbox.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码: ";
            // 
            // _LoginPasswordTextbox
            // 
            this._LoginPasswordTextbox.Location = new System.Drawing.Point(83, 44);
            this._LoginPasswordTextbox.Name = "_LoginPasswordTextbox";
            this._LoginPasswordTextbox.PasswordChar = '*';
            this._LoginPasswordTextbox.Size = new System.Drawing.Size(193, 21);
            this._LoginPasswordTextbox.TabIndex = 0;
            // 
            // _CancelButton
            // 
            this._CancelButton.Location = new System.Drawing.Point(171, 29);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(83, 32);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&X)";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // _RememberCheckBox
            // 
            this._RememberCheckBox.AutoSize = true;
            this._RememberCheckBox.Location = new System.Drawing.Point(83, 6);
            this._RememberCheckBox.Name = "_RememberCheckBox";
            this._RememberCheckBox.Size = new System.Drawing.Size(98, 17);
            this._RememberCheckBox.TabIndex = 3;
            this._RememberCheckBox.Text = "记忆登录信息";
            this._RememberCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this._LoginNameTextBox);
            this.panel1.Controls.Add(this._LoginPasswordTextbox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._LoginNameCmbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 82);
            this.panel1.TabIndex = 1;
            // 
            // _LoginNameTextBox
            // 
            this._LoginNameTextBox.Location = new System.Drawing.Point(83, 17);
            this._LoginNameTextBox.Name = "_LoginNameTextBox";
            this._LoginNameTextBox.Size = new System.Drawing.Size(193, 21);
            this._LoginNameTextBox.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._LoginButton);
            this.panel2.Controls.Add(this._CancelButton);
            this.panel2.Controls.Add(this._RememberCheckBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 110);
            this.panel2.TabIndex = 0;
            // 
            // _LoginFormTopPictureBox
            // 
            this._LoginFormTopPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._LoginFormTopPictureBox.Location = new System.Drawing.Point(0, 0);
            this._LoginFormTopPictureBox.Name = "_LoginFormTopPictureBox";
            this._LoginFormTopPictureBox.Size = new System.Drawing.Size(325, 80);
            this._LoginFormTopPictureBox.TabIndex = 8;
            this._LoginFormTopPictureBox.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this._LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 275);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._LoginFormTopPictureBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoginFormTopPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _LoginButton;
        private System.Windows.Forms.ComboBox _LoginNameCmbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _LoginPasswordTextbox;
        private System.Windows.Forms.PictureBox _LoginFormTopPictureBox;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.CheckBox _RememberCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox _LoginNameTextBox;
    }
}