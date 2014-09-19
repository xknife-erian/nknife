namespace Jeelu.SimplusOM.Client
{
    partial class ReturnSetForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.validateTextBox4 = new Jeelu.Win.ValidateTextBox();
            this.validateTextBox3 = new Jeelu.Win.ValidateTextBox();
            this.validateTextBox2 = new Jeelu.Win.ValidateTextBox();
            this.validateTextBox1 = new Jeelu.Win.ValidateTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.validateTextBox6 = new Jeelu.Win.ValidateTextBox();
            this.validateTextBox5 = new Jeelu.Win.ValidateTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "完成任务<50%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "50%<=完成任务<70%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "70%<=完成任务<100%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "完成任务>=100%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "返点率：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(315, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.validateTextBox4);
            this.groupBox1.Controls.Add(this.validateTextBox3);
            this.groupBox1.Controls.Add(this.validateTextBox2);
            this.groupBox1.Controls.Add(this.validateTextBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "月返点";
            // 
            // validateTextBox4
            // 
            this.validateTextBox4.Location = new System.Drawing.Point(236, 76);
            this.validateTextBox4.MaxLength = 2;
            this.validateTextBox4.Name = "validateTextBox4";
            this.validateTextBox4.RegexText = "^[0-9]*$";
            this.validateTextBox4.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox4.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox4.TabIndex = 3;
            // 
            // validateTextBox3
            // 
            this.validateTextBox3.Location = new System.Drawing.Point(236, 55);
            this.validateTextBox3.MaxLength = 2;
            this.validateTextBox3.Name = "validateTextBox3";
            this.validateTextBox3.RegexText = "^[0-9]*$";
            this.validateTextBox3.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox3.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox3.TabIndex = 2;
            // 
            // validateTextBox2
            // 
            this.validateTextBox2.Location = new System.Drawing.Point(236, 34);
            this.validateTextBox2.MaxLength = 2;
            this.validateTextBox2.Name = "validateTextBox2";
            this.validateTextBox2.RegexText = "^[0-9]*$";
            this.validateTextBox2.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox2.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox2.TabIndex = 1;
            // 
            // validateTextBox1
            // 
            this.validateTextBox1.Location = new System.Drawing.Point(236, 13);
            this.validateTextBox1.MaxLength = 2;
            this.validateTextBox1.Name = "validateTextBox1";
            this.validateTextBox1.RegexText = "^[0-9]*$";
            this.validateTextBox1.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox1.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox1.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(315, 82);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "返点率：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(315, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(166, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "返点率：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(315, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "%";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(166, 82);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 0;
            this.label18.Text = "返点率：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.validateTextBox6);
            this.groupBox2.Controls.Add(this.validateTextBox5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Location = new System.Drawing.Point(16, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "季度返点";
            // 
            // validateTextBox6
            // 
            this.validateTextBox6.Location = new System.Drawing.Point(236, 34);
            this.validateTextBox6.MaxLength = 2;
            this.validateTextBox6.Name = "validateTextBox6";
            this.validateTextBox6.RegexText = "^[0-9]*$";
            this.validateTextBox6.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox6.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox6.TabIndex = 1;
            // 
            // validateTextBox5
            // 
            this.validateTextBox5.Location = new System.Drawing.Point(236, 13);
            this.validateTextBox5.MaxLength = 2;
            this.validateTextBox5.Name = "validateTextBox5";
            this.validateTextBox5.RegexText = "^[0-9]*$";
            this.validateTextBox5.RegexTextRuntime = "^[0-9]*$";
            this.validateTextBox5.Size = new System.Drawing.Size(73, 21);
            this.validateTextBox5.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "完成季度任务";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "每超额50%递增";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(166, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "返点率：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(166, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "返点率：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(315, 40);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(11, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "%";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(315, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(11, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "%";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(193, 216);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(274, 216);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // ReturnSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 260);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReturnSetForm";
            this.Text = "返点设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Jeelu.Win.ValidateTextBox validateTextBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private Jeelu.Win.ValidateTextBox validateTextBox4;
        private Jeelu.Win.ValidateTextBox validateTextBox3;
        private Jeelu.Win.ValidateTextBox validateTextBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private Jeelu.Win.ValidateTextBox validateTextBox6;
        private Jeelu.Win.ValidateTextBox validateTextBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}