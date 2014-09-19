namespace Jeelu.SimplusOM.Client
{
    partial class UserToOtherForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toAgentRButton = new System.Windows.Forms.RadioButton();
            this.toManagerRButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toOtherComboBox = new System.Windows.Forms.ComboBox();
            this.转至对象 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toAgentRButton);
            this.panel1.Controls.Add(this.toManagerRButton);
            this.panel1.Location = new System.Drawing.Point(94, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 25);
            this.panel1.TabIndex = 0;
            // 
            // toAgentRButton
            // 
            this.toAgentRButton.AutoSize = true;
            this.toAgentRButton.Location = new System.Drawing.Point(120, 3);
            this.toAgentRButton.Name = "toAgentRButton";
            this.toAgentRButton.Size = new System.Drawing.Size(83, 16);
            this.toAgentRButton.TabIndex = 0;
            this.toAgentRButton.Text = "转向代理商";
            this.toAgentRButton.UseVisualStyleBackColor = true;
            this.toAgentRButton.CheckedChanged += new System.EventHandler(this.toAgentRButton_CheckedChanged);
            // 
            // toManagerRButton
            // 
            this.toManagerRButton.AutoSize = true;
            this.toManagerRButton.Checked = true;
            this.toManagerRButton.Location = new System.Drawing.Point(17, 3);
            this.toManagerRButton.Name = "toManagerRButton";
            this.toManagerRButton.Size = new System.Drawing.Size(71, 16);
            this.toManagerRButton.TabIndex = 0;
            this.toManagerRButton.TabStop = true;
            this.toManagerRButton.Text = "转向个人";
            this.toManagerRButton.UseVisualStyleBackColor = true;
            this.toManagerRButton.CheckedChanged += new System.EventHandler(this.toManagerRButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "转移类型";
            // 
            // toOtherComboBox
            // 
            this.toOtherComboBox.FormattingEnabled = true;
            this.toOtherComboBox.Location = new System.Drawing.Point(111, 77);
            this.toOtherComboBox.Name = "toOtherComboBox";
            this.toOtherComboBox.Size = new System.Drawing.Size(121, 20);
            this.toOtherComboBox.TabIndex = 2;
            // 
            // 转至对象
            // 
            this.转至对象.AutoSize = true;
            this.转至对象.Location = new System.Drawing.Point(23, 80);
            this.转至对象.Name = "转至对象";
            this.转至对象.Size = new System.Drawing.Size(53, 12);
            this.转至对象.TabIndex = 3;
            this.转至对象.Text = "转至对象";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(181, 130);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(262, 130);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // UserToOtherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 182);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.转至对象);
            this.Controls.Add(this.toOtherComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "UserToOtherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代理商转移";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton toAgentRButton;
        private System.Windows.Forms.RadioButton toManagerRButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox toOtherComboBox;
        private System.Windows.Forms.Label 转至对象;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}