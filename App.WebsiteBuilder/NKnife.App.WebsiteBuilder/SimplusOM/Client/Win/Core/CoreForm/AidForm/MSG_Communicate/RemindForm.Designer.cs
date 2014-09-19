namespace Jeelu.SimplusOM.Client
{
    partial class RemindForm
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
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.addAgentBtn = new System.Windows.Forms.Button();
            this.delAgentBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.addUserBtn = new System.Windows.Forms.Button();
            this.delUserBtn = new System.Windows.Forms.Button();
            this.allAgentListBox = new System.Windows.Forms.ListBox();
            this.addAgentListBox = new System.Windows.Forms.ListBox();
            this.allUserListBox = new System.Windows.Forms.ListBox();
            this.addUserListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // contentTextBox
            // 
            this.contentTextBox.Location = new System.Drawing.Point(26, 203);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Size = new System.Drawing.Size(399, 108);
            this.contentTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "添加内容：";
            // 
            // OKBtn
            // 
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(238, 327);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(332, 327);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "代理商：";
            this.label2.Visible = false;
            // 
            // addAgentBtn
            // 
            this.addAgentBtn.Location = new System.Drawing.Point(222, 21);
            this.addAgentBtn.Name = "addAgentBtn";
            this.addAgentBtn.Size = new System.Drawing.Size(48, 23);
            this.addAgentBtn.TabIndex = 4;
            this.addAgentBtn.Text = ">>>";
            this.addAgentBtn.UseVisualStyleBackColor = true;
            this.addAgentBtn.Click += new System.EventHandler(this.addAgentBtn_Click);
            // 
            // delAgentBtn
            // 
            this.delAgentBtn.Location = new System.Drawing.Point(222, 50);
            this.delAgentBtn.Name = "delAgentBtn";
            this.delAgentBtn.Size = new System.Drawing.Size(48, 23);
            this.delAgentBtn.TabIndex = 5;
            this.delAgentBtn.Text = "<<<";
            this.delAgentBtn.UseVisualStyleBackColor = true;
            this.delAgentBtn.Click += new System.EventHandler(this.delAgentBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "客户：";
            this.label3.Visible = false;
            // 
            // addUserBtn
            // 
            this.addUserBtn.Location = new System.Drawing.Point(222, 105);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(48, 23);
            this.addUserBtn.TabIndex = 4;
            this.addUserBtn.Text = ">>>";
            this.addUserBtn.UseVisualStyleBackColor = true;
            this.addUserBtn.Click += new System.EventHandler(this.addUserBtn_Click);
            // 
            // delUserBtn
            // 
            this.delUserBtn.Location = new System.Drawing.Point(222, 134);
            this.delUserBtn.Name = "delUserBtn";
            this.delUserBtn.Size = new System.Drawing.Size(48, 23);
            this.delUserBtn.TabIndex = 5;
            this.delUserBtn.Text = "<<<";
            this.delUserBtn.UseVisualStyleBackColor = true;
            this.delUserBtn.Click += new System.EventHandler(this.delUserBtn_Click);
            // 
            // allAgentListBox
            // 
            this.allAgentListBox.FormattingEnabled = true;
            this.allAgentListBox.ItemHeight = 12;
            this.allAgentListBox.Location = new System.Drawing.Point(78, 11);
            this.allAgentListBox.Name = "allAgentListBox";
            this.allAgentListBox.Size = new System.Drawing.Size(127, 76);
            this.allAgentListBox.TabIndex = 6;
            // 
            // addAgentListBox
            // 
            this.addAgentListBox.FormattingEnabled = true;
            this.addAgentListBox.ItemHeight = 12;
            this.addAgentListBox.Location = new System.Drawing.Point(293, 11);
            this.addAgentListBox.Name = "addAgentListBox";
            this.addAgentListBox.Size = new System.Drawing.Size(127, 76);
            this.addAgentListBox.TabIndex = 6;
            // 
            // allUserListBox
            // 
            this.allUserListBox.FormattingEnabled = true;
            this.allUserListBox.ItemHeight = 12;
            this.allUserListBox.Location = new System.Drawing.Point(78, 93);
            this.allUserListBox.Name = "allUserListBox";
            this.allUserListBox.Size = new System.Drawing.Size(127, 76);
            this.allUserListBox.TabIndex = 6;
            // 
            // addUserListBox
            // 
            this.addUserListBox.FormattingEnabled = true;
            this.addUserListBox.ItemHeight = 12;
            this.addUserListBox.Location = new System.Drawing.Point(293, 93);
            this.addUserListBox.Name = "addUserListBox";
            this.addUserListBox.Size = new System.Drawing.Size(127, 76);
            this.addUserListBox.TabIndex = 6;
            // 
            // RemindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 367);
            this.Controls.Add(this.addAgentListBox);
            this.Controls.Add(this.addUserListBox);
            this.Controls.Add(this.allUserListBox);
            this.Controls.Add(this.allAgentListBox);
            this.Controls.Add(this.delUserBtn);
            this.Controls.Add(this.delAgentBtn);
            this.Controls.Add(this.addUserBtn);
            this.Controls.Add(this.addAgentBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contentTextBox);
            this.Name = "RemindForm";
            this.Text = "添加提醒";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox contentTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addAgentBtn;
        private System.Windows.Forms.Button delAgentBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.Button delUserBtn;
        private System.Windows.Forms.ListBox allAgentListBox;
        private System.Windows.Forms.ListBox addAgentListBox;
        private System.Windows.Forms.ListBox allUserListBox;
        private System.Windows.Forms.ListBox addUserListBox;
    }
}