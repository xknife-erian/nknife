namespace Jeelu.SimplusOM.Client
{
    partial class ChargeUserHistForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chargeUserHistoryDGV = new System.Windows.Forms.DataGridView();
            this.chargeChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.chargeAmountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chargeCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeSend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeReceive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chargeUserHistoryDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // chargeUserHistoryDGV
            // 
            this.chargeUserHistoryDGV.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.chargeUserHistoryDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.chargeUserHistoryDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chargeUserHistoryDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chargeId,
            this.chargeTime,
            this.chargeSend,
            this.chargeManager,
            this.chargeReceive,
            this.chargeChecked,
            this.chargeAmount,
            this.chargeChecker});
            this.chargeUserHistoryDGV.Location = new System.Drawing.Point(16, 90);
            this.chargeUserHistoryDGV.Name = "chargeUserHistoryDGV";
            this.chargeUserHistoryDGV.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.chargeUserHistoryDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.chargeUserHistoryDGV.RowHeadersWidth = 15;
            this.chargeUserHistoryDGV.RowTemplate.Height = 23;
            this.chargeUserHistoryDGV.Size = new System.Drawing.Size(390, 253);
            this.chargeUserHistoryDGV.TabIndex = 9;
            // 
            // chargeChecked
            // 
            this.chargeChecked.HeaderText = "审核";
            this.chargeChecked.Name = "chargeChecked";
            this.chargeChecked.ReadOnly = true;
            this.chargeChecked.Width = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "元";
            // 
            // chargeAmountLabel
            // 
            this.chargeAmountLabel.AutoSize = true;
            this.chargeAmountLabel.Location = new System.Drawing.Point(126, 61);
            this.chargeAmountLabel.Name = "chargeAmountLabel";
            this.chargeAmountLabel.Size = new System.Drawing.Size(29, 12);
            this.chargeAmountLabel.TabIndex = 8;
            this.chargeAmountLabel.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "充值金额：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "次";
            // 
            // chargeCountLabel
            // 
            this.chargeCountLabel.AutoSize = true;
            this.chargeCountLabel.Location = new System.Drawing.Point(126, 32);
            this.chargeCountLabel.Name = "chargeCountLabel";
            this.chargeCountLabel.Size = new System.Drawing.Size(17, 12);
            this.chargeCountLabel.TabIndex = 4;
            this.chargeCountLabel.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "充值次数：";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "时间";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "充值方";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.HeaderText = "充值人";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "被充值方";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "#.##";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn6.HeaderText = "金额";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn7.HeaderText = "审核人";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // chargeId
            // 
            this.chargeId.HeaderText = "Column1";
            this.chargeId.Name = "chargeId";
            this.chargeId.ReadOnly = true;
            this.chargeId.Visible = false;
            // 
            // chargeTime
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.chargeTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.chargeTime.HeaderText = "时间";
            this.chargeTime.Name = "chargeTime";
            this.chargeTime.ReadOnly = true;
            // 
            // chargeSend
            // 
            this.chargeSend.HeaderText = "充值方";
            this.chargeSend.Name = "chargeSend";
            this.chargeSend.ReadOnly = true;
            // 
            // chargeManager
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.chargeManager.DefaultCellStyle = dataGridViewCellStyle3;
            this.chargeManager.HeaderText = "充值人";
            this.chargeManager.Name = "chargeManager";
            this.chargeManager.ReadOnly = true;
            this.chargeManager.Width = 70;
            // 
            // chargeReceive
            // 
            this.chargeReceive.HeaderText = "被充值方";
            this.chargeReceive.Name = "chargeReceive";
            this.chargeReceive.ReadOnly = true;
            this.chargeReceive.Visible = false;
            // 
            // chargeAmount
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#.##";
            this.chargeAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.chargeAmount.HeaderText = "金额";
            this.chargeAmount.Name = "chargeAmount";
            this.chargeAmount.ReadOnly = true;
            this.chargeAmount.Width = 60;
            // 
            // chargeChecker
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.chargeChecker.DefaultCellStyle = dataGridViewCellStyle5;
            this.chargeChecker.HeaderText = "审核人";
            this.chargeChecker.Name = "chargeChecker";
            this.chargeChecker.ReadOnly = true;
            this.chargeChecker.Visible = false;
            this.chargeChecker.Width = 70;
            // 
            // checkBtn
            // 
            this.checkBtn.Location = new System.Drawing.Point(304, 44);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(75, 23);
            this.checkBtn.TabIndex = 10;
            this.checkBtn.Text = "审核";
            this.checkBtn.UseVisualStyleBackColor = true;
            this.checkBtn.Click += new System.EventHandler(this.checkBtn_Click);
            // 
            // ChargeUserHistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 374);
            this.Controls.Add(this.checkBtn);
            this.Controls.Add(this.chargeUserHistoryDGV);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chargeAmountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chargeCountLabel);
            this.Controls.Add(this.label1);
            this.Name = "ChargeUserHistForm";
            this.Text = "用户充值记录";
            ((System.ComponentModel.ISupportInitialize)(this.chargeUserHistoryDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView chargeUserHistoryDGV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label chargeAmountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label chargeCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeReceive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chargeChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeChecker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Button checkBtn;
    }
}