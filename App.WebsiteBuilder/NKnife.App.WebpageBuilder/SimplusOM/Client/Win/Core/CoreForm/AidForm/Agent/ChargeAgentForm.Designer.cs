namespace Jeelu.SimplusOM.Client
{
    partial class ChargeAgentForm
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
            this.chargeHistoryListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.banlanceLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.laterBanlanceLabel = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chargeAmountTextBox = new Jeelu.Win.ValidateTextBox();
            this.desTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chargeHistoryListBox
            // 
            this.chargeHistoryListBox.FormattingEnabled = true;
            this.chargeHistoryListBox.ItemHeight = 12;
            this.chargeHistoryListBox.Location = new System.Drawing.Point(26, 50);
            this.chargeHistoryListBox.Name = "chargeHistoryListBox";
            this.chargeHistoryListBox.Size = new System.Drawing.Size(120, 196);
            this.chargeHistoryListBox.TabIndex = 0;
            this.chargeHistoryListBox.DoubleClick += new System.EventHandler(this.chargeHistoryListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "历史充值记录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "帐户余额：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "充值金额：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "剩余金额：";
            // 
            // banlanceLabel
            // 
            this.banlanceLabel.AutoSize = true;
            this.banlanceLabel.Location = new System.Drawing.Point(281, 50);
            this.banlanceLabel.Name = "banlanceLabel";
            this.banlanceLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.banlanceLabel.Size = new System.Drawing.Size(29, 12);
            this.banlanceLabel.TabIndex = 1;
            this.banlanceLabel.Text = "1000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "元";
            // 
            // laterBanlanceLabel
            // 
            this.laterBanlanceLabel.AutoSize = true;
            this.laterBanlanceLabel.Location = new System.Drawing.Point(287, 102);
            this.laterBanlanceLabel.Name = "laterBanlanceLabel";
            this.laterBanlanceLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.laterBanlanceLabel.Size = new System.Drawing.Size(23, 12);
            this.laterBanlanceLabel.TabIndex = 1;
            this.laterBanlanceLabel.Text = "200";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(181, 264);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(270, 264);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "元";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(328, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "元";
            // 
            // chargeAmountTextBox
            // 
            this.chargeAmountTextBox.Location = new System.Drawing.Point(242, 73);
            this.chargeAmountTextBox.Name = "chargeAmountTextBox";
            this.chargeAmountTextBox.RegexText = "^\\d*$";
            this.chargeAmountTextBox.RegexTextRuntime = "^\\d*$";
            this.chargeAmountTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chargeAmountTextBox.Size = new System.Drawing.Size(68, 21);
            this.chargeAmountTextBox.TabIndex = 3;
            this.chargeAmountTextBox.TextChanged += new System.EventHandler(this.chargeAmountTextBox_TextChanged);
            // 
            // desTextBox
            // 
            this.desTextBox.Location = new System.Drawing.Point(167, 155);
            this.desTextBox.Multiline = true;
            this.desTextBox.Name = "desTextBox";
            this.desTextBox.Size = new System.Drawing.Size(178, 91);
            this.desTextBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "充值描叙：";
            // 
            // ChargeAgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 299);
            this.Controls.Add(this.desTextBox);
            this.Controls.Add(this.chargeAmountTextBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.laterBanlanceLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.banlanceLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chargeHistoryListBox);
            this.Name = "ChargeAgentForm";
            this.Text = "代理商充值";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox chargeHistoryListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label banlanceLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label laterBanlanceLabel;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Jeelu.Win.ValidateTextBox chargeAmountTextBox;
        private System.Windows.Forms.TextBox desTextBox;
        private System.Windows.Forms.Label label5;
    }
}