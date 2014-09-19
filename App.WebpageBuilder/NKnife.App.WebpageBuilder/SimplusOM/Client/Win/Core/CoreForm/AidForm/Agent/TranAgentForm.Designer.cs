namespace Jeelu.SimplusOM.Client
{
    partial class TranAgentForm
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
            this.firstChargeTextBox = new Jeelu.Win.ValidateTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.cashFlowGroupBox = new System.Windows.Forms.GroupBox();
            this.quarterReturn2TextBox = new Jeelu.Win.ValidateTextBox();
            this.quarterReturn1TextBox = new Jeelu.Win.ValidateTextBox();
            this.return4TextBox = new Jeelu.Win.ValidateTextBox();
            this.return3TextBox = new Jeelu.Win.ValidateTextBox();
            this.return2TextBox = new Jeelu.Win.ValidateTextBox();
            this.return1TextBox = new Jeelu.Win.ValidateTextBox();
            this.MonthTaskTextBox = new Jeelu.Win.ValidateTextBox();
            this.AddTimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.quarterTaskTextBox = new Jeelu.Win.ValidateTextBox();
            this.agentIDTextBox = new System.Windows.Forms.TextBox();
            this.agentNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.cashFlowGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstChargeTextBox
            // 
            this.firstChargeTextBox.Location = new System.Drawing.Point(327, 57);
            this.firstChargeTextBox.MaxLength = 10;
            this.firstChargeTextBox.Name = "firstChargeTextBox";
            this.firstChargeTextBox.RegexText = "^[0-9]*$";
            this.firstChargeTextBox.RegexTextRuntime = "^[0-9]*$";
            this.firstChargeTextBox.Size = new System.Drawing.Size(79, 21);
            this.firstChargeTextBox.TabIndex = 1;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(29, 61);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 12);
            this.label24.TabIndex = 16;
            this.label24.Text = "首次充值时间";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(244, 61);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 12);
            this.label23.TabIndex = 15;
            this.label23.Text = "首次充值金额";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(29, 82);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 14;
            this.label22.Text = "月任务";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 7;
            this.label15.Text = "完成月任务<50%";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(403, 315);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(484, 315);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // cashFlowGroupBox
            // 
            this.cashFlowGroupBox.Controls.Add(this.agentIDTextBox);
            this.cashFlowGroupBox.Controls.Add(this.agentNameTextBox);
            this.cashFlowGroupBox.Controls.Add(this.label2);
            this.cashFlowGroupBox.Controls.Add(this.label41);
            this.cashFlowGroupBox.Controls.Add(this.quarterReturn2TextBox);
            this.cashFlowGroupBox.Controls.Add(this.quarterReturn1TextBox);
            this.cashFlowGroupBox.Controls.Add(this.return4TextBox);
            this.cashFlowGroupBox.Controls.Add(this.return3TextBox);
            this.cashFlowGroupBox.Controls.Add(this.return2TextBox);
            this.cashFlowGroupBox.Controls.Add(this.return1TextBox);
            this.cashFlowGroupBox.Controls.Add(this.MonthTaskTextBox);
            this.cashFlowGroupBox.Controls.Add(this.AddTimeDateTimePicker);
            this.cashFlowGroupBox.Controls.Add(this.label24);
            this.cashFlowGroupBox.Controls.Add(this.label4);
            this.cashFlowGroupBox.Controls.Add(this.label6);
            this.cashFlowGroupBox.Controls.Add(this.label26);
            this.cashFlowGroupBox.Controls.Add(this.label33);
            this.cashFlowGroupBox.Controls.Add(this.label32);
            this.cashFlowGroupBox.Controls.Add(this.label31);
            this.cashFlowGroupBox.Controls.Add(this.label30);
            this.cashFlowGroupBox.Controls.Add(this.label29);
            this.cashFlowGroupBox.Controls.Add(this.label28);
            this.cashFlowGroupBox.Controls.Add(this.label27);
            this.cashFlowGroupBox.Controls.Add(this.label25);
            this.cashFlowGroupBox.Controls.Add(this.label21);
            this.cashFlowGroupBox.Controls.Add(this.label13);
            this.cashFlowGroupBox.Controls.Add(this.label5);
            this.cashFlowGroupBox.Controls.Add(this.label15);
            this.cashFlowGroupBox.Controls.Add(this.firstChargeTextBox);
            this.cashFlowGroupBox.Controls.Add(this.label14);
            this.cashFlowGroupBox.Controls.Add(this.label10);
            this.cashFlowGroupBox.Controls.Add(this.label12);
            this.cashFlowGroupBox.Controls.Add(this.label9);
            this.cashFlowGroupBox.Controls.Add(this.label8);
            this.cashFlowGroupBox.Controls.Add(this.label7);
            this.cashFlowGroupBox.Controls.Add(this.label11);
            this.cashFlowGroupBox.Controls.Add(this.label22);
            this.cashFlowGroupBox.Controls.Add(this.label23);
            this.cashFlowGroupBox.Controls.Add(this.quarterTaskTextBox);
            this.cashFlowGroupBox.Location = new System.Drawing.Point(12, 12);
            this.cashFlowGroupBox.Name = "cashFlowGroupBox";
            this.cashFlowGroupBox.Size = new System.Drawing.Size(568, 275);
            this.cashFlowGroupBox.TabIndex = 1;
            this.cashFlowGroupBox.TabStop = false;
            this.cashFlowGroupBox.Text = "金额流转设置";
            // 
            // quarterReturn2TextBox
            // 
            this.quarterReturn2TextBox.Location = new System.Drawing.Point(275, 235);
            this.quarterReturn2TextBox.MaxLength = 2;
            this.quarterReturn2TextBox.Name = "quarterReturn2TextBox";
            this.quarterReturn2TextBox.RegexText = "^[0-9]*$";
            this.quarterReturn2TextBox.RegexTextRuntime = "^[0-9]*$";
            this.quarterReturn2TextBox.Size = new System.Drawing.Size(126, 21);
            this.quarterReturn2TextBox.TabIndex = 8;
            // 
            // quarterReturn1TextBox
            // 
            this.quarterReturn1TextBox.Location = new System.Drawing.Point(275, 214);
            this.quarterReturn1TextBox.MaxLength = 2;
            this.quarterReturn1TextBox.Name = "quarterReturn1TextBox";
            this.quarterReturn1TextBox.RegexText = "^[0-9]*$";
            this.quarterReturn1TextBox.RegexTextRuntime = "^[0-9]*$";
            this.quarterReturn1TextBox.Size = new System.Drawing.Size(126, 21);
            this.quarterReturn1TextBox.TabIndex = 7;
            // 
            // return4TextBox
            // 
            this.return4TextBox.Location = new System.Drawing.Point(275, 169);
            this.return4TextBox.MaxLength = 2;
            this.return4TextBox.Name = "return4TextBox";
            this.return4TextBox.RegexText = "^[0-9]*$";
            this.return4TextBox.RegexTextRuntime = "^[0-9]*$";
            this.return4TextBox.Size = new System.Drawing.Size(126, 21);
            this.return4TextBox.TabIndex = 6;
            // 
            // return3TextBox
            // 
            this.return3TextBox.Location = new System.Drawing.Point(275, 148);
            this.return3TextBox.MaxLength = 2;
            this.return3TextBox.Name = "return3TextBox";
            this.return3TextBox.RegexText = "^[0-9]*$";
            this.return3TextBox.RegexTextRuntime = "^[0-9]*$";
            this.return3TextBox.Size = new System.Drawing.Size(126, 21);
            this.return3TextBox.TabIndex = 5;
            // 
            // return2TextBox
            // 
            this.return2TextBox.Location = new System.Drawing.Point(275, 127);
            this.return2TextBox.MaxLength = 2;
            this.return2TextBox.Name = "return2TextBox";
            this.return2TextBox.RegexText = "^[0-9]*$";
            this.return2TextBox.RegexTextRuntime = "^[0-9]*$";
            this.return2TextBox.Size = new System.Drawing.Size(126, 21);
            this.return2TextBox.TabIndex = 4;
            // 
            // return1TextBox
            // 
            this.return1TextBox.Location = new System.Drawing.Point(275, 106);
            this.return1TextBox.MaxLength = 2;
            this.return1TextBox.Name = "return1TextBox";
            this.return1TextBox.RegexText = "^[0-9]*$";
            this.return1TextBox.RegexTextRuntime = "^[0-9]*$";
            this.return1TextBox.Size = new System.Drawing.Size(126, 21);
            this.return1TextBox.TabIndex = 3;
            // 
            // MonthTaskTextBox
            // 
            this.MonthTaskTextBox.Location = new System.Drawing.Point(121, 78);
            this.MonthTaskTextBox.MaxLength = 10;
            this.MonthTaskTextBox.Name = "MonthTaskTextBox";
            this.MonthTaskTextBox.RegexText = "^[0-9]*$";
            this.MonthTaskTextBox.RegexTextRuntime = "^[0-9]*$";
            this.MonthTaskTextBox.Size = new System.Drawing.Size(108, 21);
            this.MonthTaskTextBox.TabIndex = 2;
            this.MonthTaskTextBox.TextChanged += new System.EventHandler(this.MonthTaskTextBox_TextChanged);
            // 
            // AddTimeDateTimePicker
            // 
            this.AddTimeDateTimePicker.Location = new System.Drawing.Point(121, 57);
            this.AddTimeDateTimePicker.Name = "AddTimeDateTimePicker";
            this.AddTimeDateTimePicker.Size = new System.Drawing.Size(108, 21);
            this.AddTimeDateTimePicker.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "完成月任务>=100%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "70%<=完成月任务<100%";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(244, 194);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 12);
            this.label26.TabIndex = 7;
            this.label26.Text = "元";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(415, 238);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 7;
            this.label33.Text = "%";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(415, 219);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(11, 12);
            this.label32.TabIndex = 7;
            this.label32.Text = "%";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(415, 174);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(11, 12);
            this.label31.TabIndex = 7;
            this.label31.Text = "%";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(415, 153);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(11, 12);
            this.label30.TabIndex = 7;
            this.label30.Text = "%";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(415, 133);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(11, 12);
            this.label29.TabIndex = 7;
            this.label29.Text = "%";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(415, 115);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(11, 12);
            this.label28.TabIndex = 7;
            this.label28.Text = "%";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(415, 63);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 12);
            this.label27.TabIndex = 7;
            this.label27.Text = "元";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(244, 82);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 7;
            this.label25.Text = "元";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(29, 236);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "每超额50%递增";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 215);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "完成季度任务";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "50%<=完成月任务<70%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(214, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 14;
            this.label14.Text = "返点率：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "返点率：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(214, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "返点率：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "返点率：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "返点率：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "返点率：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(29, 194);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "季度任务";
            // 
            // quarterTaskTextBox
            // 
            this.quarterTaskTextBox.Location = new System.Drawing.Point(121, 191);
            this.quarterTaskTextBox.MaxLength = 9;
            this.quarterTaskTextBox.Name = "quarterTaskTextBox";
            this.quarterTaskTextBox.ReadOnly = true;
            this.quarterTaskTextBox.RegexText = "^[0-9]*$";
            this.quarterTaskTextBox.RegexTextRuntime = "^[0-9]*$";
            this.quarterTaskTextBox.Size = new System.Drawing.Size(105, 21);
            this.quarterTaskTextBox.TabIndex = 7;
            // 
            // agentIDTextBox
            // 
            this.agentIDTextBox.Enabled = false;
            this.agentIDTextBox.Location = new System.Drawing.Point(121, 23);
            this.agentIDTextBox.Name = "agentIDTextBox";
            this.agentIDTextBox.ReadOnly = true;
            this.agentIDTextBox.Size = new System.Drawing.Size(105, 21);
            this.agentIDTextBox.TabIndex = 34;
            // 
            // agentNameTextBox
            // 
            this.agentNameTextBox.Location = new System.Drawing.Point(327, 23);
            this.agentNameTextBox.Name = "agentNameTextBox";
            this.agentNameTextBox.ReadOnly = true;
            this.agentNameTextBox.Size = new System.Drawing.Size(126, 21);
            this.agentNameTextBox.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 37;
            this.label2.Text = "代理ID";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(244, 27);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(53, 12);
            this.label41.TabIndex = 36;
            this.label41.Text = "代理名称";
            // 
            // TranAgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 355);
            this.Controls.Add(this.cashFlowGroupBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TranAgentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增代理商";
            this.cashFlowGroupBox.ResumeLayout(false);
            this.cashFlowGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Jeelu.Win.ValidateTextBox firstChargeTextBox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.GroupBox cashFlowGroupBox;
        private System.Windows.Forms.DateTimePicker AddTimeDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private Jeelu.Win.ValidateTextBox quarterTaskTextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private Jeelu.Win.ValidateTextBox quarterReturn2TextBox;
        private Jeelu.Win.ValidateTextBox quarterReturn1TextBox;
        private Jeelu.Win.ValidateTextBox return4TextBox;
        private Jeelu.Win.ValidateTextBox return3TextBox;
        private Jeelu.Win.ValidateTextBox return2TextBox;
        private Jeelu.Win.ValidateTextBox return1TextBox;
        private Jeelu.Win.ValidateTextBox MonthTaskTextBox;
        private System.Windows.Forms.TextBox agentIDTextBox;
        private System.Windows.Forms.TextBox agentNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label41;
    }
}