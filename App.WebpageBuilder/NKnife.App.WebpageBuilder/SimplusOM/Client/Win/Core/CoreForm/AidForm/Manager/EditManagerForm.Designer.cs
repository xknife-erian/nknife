namespace Jeelu.SimplusOM.Client
{
    partial class EditManagerForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AddAreaListBox = new System.Windows.Forms.ListBox();
            this.AllAreaListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DelAreaBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AddAreaBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.PassWord2TextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.PassWord1TextBox = new System.Windows.Forms.TextBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.AddAreaListBox);
            this.groupBox1.Controls.Add(this.AllAreaListBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DelAreaBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AddAreaBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.IDTextBox);
            this.groupBox1.Controls.Add(this.PassWord2TextBox);
            this.groupBox1.Controls.Add(this.NameTextBox);
            this.groupBox1.Controls.Add(this.PassWord1TextBox);
            this.groupBox1.Location = new System.Drawing.Point(39, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 152);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(265, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "密码不一致！";
            this.label5.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // AddAreaListBox
            // 
            this.AddAreaListBox.FormattingEnabled = true;
            this.AddAreaListBox.ItemHeight = 12;
            this.AddAreaListBox.Location = new System.Drawing.Point(344, 76);
            this.AddAreaListBox.Name = "AddAreaListBox";
            this.AddAreaListBox.Size = new System.Drawing.Size(155, 64);
            this.AddAreaListBox.TabIndex = 7;
            // 
            // AllAreaListBox
            // 
            this.AllAreaListBox.FormattingEnabled = true;
            this.AllAreaListBox.ItemHeight = 12;
            this.AllAreaListBox.Location = new System.Drawing.Point(98, 78);
            this.AllAreaListBox.Name = "AllAreaListBox";
            this.AllAreaListBox.Size = new System.Drawing.Size(155, 64);
            this.AllAreaListBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // DelAreaBtn
            // 
            this.DelAreaBtn.Location = new System.Drawing.Point(267, 111);
            this.DelAreaBtn.Name = "DelAreaBtn";
            this.DelAreaBtn.Size = new System.Drawing.Size(54, 23);
            this.DelAreaBtn.TabIndex = 6;
            this.DelAreaBtn.Text = "<<";
            this.DelAreaBtn.UseVisualStyleBackColor = true;
            this.DelAreaBtn.Click += new System.EventHandler(this.DelAreaBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "密码：";
            // 
            // AddAreaBtn
            // 
            this.AddAreaBtn.Location = new System.Drawing.Point(267, 82);
            this.AddAreaBtn.Name = "AddAreaBtn";
            this.AddAreaBtn.Size = new System.Drawing.Size(54, 23);
            this.AddAreaBtn.TabIndex = 5;
            this.AddAreaBtn.Text = ">>";
            this.AddAreaBtn.UseVisualStyleBackColor = true;
            this.AddAreaBtn.Click += new System.EventHandler(this.AddAreaBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "密码确认：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "分配地区;";
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(98, 15);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(155, 21);
            this.IDTextBox.TabIndex = 0;
            // 
            // PassWord2TextBox
            // 
            this.PassWord2TextBox.Location = new System.Drawing.Point(98, 57);
            this.PassWord2TextBox.Name = "PassWord2TextBox";
            this.PassWord2TextBox.PasswordChar = '*';
            this.PassWord2TextBox.Size = new System.Drawing.Size(155, 21);
            this.PassWord2TextBox.TabIndex = 3;
            this.PassWord2TextBox.Validated += new System.EventHandler(this.PassWordTextBox_Validated);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(344, 15);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(155, 21);
            this.NameTextBox.TabIndex = 1;
            // 
            // PassWord1TextBox
            // 
            this.PassWord1TextBox.Location = new System.Drawing.Point(98, 36);
            this.PassWord1TextBox.Name = "PassWord1TextBox";
            this.PassWord1TextBox.PasswordChar = '*';
            this.PassWord1TextBox.Size = new System.Drawing.Size(155, 21);
            this.PassWord1TextBox.TabIndex = 2;
            this.PassWord1TextBox.Validated += new System.EventHandler(this.PassWordTextBox_Validated);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(424, 203);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(505, 203);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // EditManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 264);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditManagerForm";
            this.Text = "员工管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox AddAreaListBox;
        private System.Windows.Forms.ListBox AllAreaListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DelAreaBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddAreaBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.TextBox PassWord2TextBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox PassWord1TextBox;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label5;


    }
}