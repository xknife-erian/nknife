namespace Jeelu.SimplusOM.Client
{
    partial class ChargeAgentHistForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chargeCountLabel = new System.Windows.Forms.Label();
            this.chargeAmountLabel = new System.Windows.Forms.Label();
            this.chargeAgentHistoryDGV = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chargeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeSend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeReceive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chargeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargeChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chargeAgentHistoryDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "充值次数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "充值金额：";
            // 
            // chargeCountLabel
            // 
            this.chargeCountLabel.AutoSize = true;
            this.chargeCountLabel.Location = new System.Drawing.Point(140, 25);
            this.chargeCountLabel.Name = "chargeCountLabel";
            this.chargeCountLabel.Size = new System.Drawing.Size(17, 12);
            this.chargeCountLabel.TabIndex = 0;
            this.chargeCountLabel.Text = "10";
            // 
            // chargeAmountLabel
            // 
            this.chargeAmountLabel.AutoSize = true;
            this.chargeAmountLabel.Location = new System.Drawing.Point(140, 54);
            this.chargeAmountLabel.Name = "chargeAmountLabel";
            this.chargeAmountLabel.Size = new System.Drawing.Size(29, 12);
            this.chargeAmountLabel.TabIndex = 1;
            this.chargeAmountLabel.Text = "1000";
            // 
            // chargeAgentHistoryDGV
            // 
            this.chargeAgentHistoryDGV.AllowUserToAddRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.chargeAgentHistoryDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.chargeAgentHistoryDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chargeAgentHistoryDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chargeId,
            this.chargeTime,
            this.chargeSend,
            this.chargeReceive,
            this.chargeChecked,
            this.chargeAmount,
            this.chargeManager,
            this.chargeChecker});
            this.chargeAgentHistoryDGV.Location = new System.Drawing.Point(30, 83);
            this.chargeAgentHistoryDGV.Name = "chargeAgentHistoryDGV";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.chargeAgentHistoryDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.chargeAgentHistoryDGV.RowHeadersWidth = 15;
            this.chargeAgentHistoryDGV.RowTemplate.Height = 23;
            this.chargeAgentHistoryDGV.Size = new System.Drawing.Size(567, 253);
            this.chargeAgentHistoryDGV.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "次";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "元";
            // 
            // chargeId
            // 
            this.chargeId.HeaderText = "Column1";
            this.chargeId.Name = "chargeId";
            this.chargeId.Visible = false;
            // 
            // chargeTime
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.chargeTime.DefaultCellStyle = dataGridViewCellStyle8;
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
            // chargeReceive
            // 
            this.chargeReceive.HeaderText = "被充值方";
            this.chargeReceive.Name = "chargeReceive";
            this.chargeReceive.ReadOnly = true;
            // 
            // chargeChecked
            // 
            this.chargeChecked.HeaderText = "审核";
            this.chargeChecked.Name = "chargeChecked";
            this.chargeChecked.ReadOnly = true;
            this.chargeChecked.Width = 40;
            // 
            // chargeAmount
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "#.##";
            this.chargeAmount.DefaultCellStyle = dataGridViewCellStyle9;
            this.chargeAmount.HeaderText = "金额";
            this.chargeAmount.Name = "chargeAmount";
            this.chargeAmount.ReadOnly = true;
            this.chargeAmount.Width = 60;
            // 
            // chargeManager
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.chargeManager.DefaultCellStyle = dataGridViewCellStyle10;
            this.chargeManager.HeaderText = "充值人";
            this.chargeManager.Name = "chargeManager";
            this.chargeManager.ReadOnly = true;
            this.chargeManager.Width = 70;
            // 
            // chargeChecker
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.chargeChecker.DefaultCellStyle = dataGridViewCellStyle11;
            this.chargeChecker.HeaderText = "审核人";
            this.chargeChecker.Name = "chargeChecker";
            this.chargeChecker.ReadOnly = true;
            this.chargeChecker.Width = 70;
            // 
            // CheckBtn
            // 
            this.CheckBtn.Location = new System.Drawing.Point(463, 38);
            this.CheckBtn.Name = "CheckBtn";
            this.CheckBtn.Size = new System.Drawing.Size(75, 23);
            this.CheckBtn.TabIndex = 3;
            this.CheckBtn.Text = "审核";
            this.CheckBtn.UseVisualStyleBackColor = true;
            this.CheckBtn.Visible = false;
            this.CheckBtn.Click += new System.EventHandler(this.CheckBtn_Click);
            // 
            // ChargeAgentHistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 348);
            this.Controls.Add(this.CheckBtn);
            this.Controls.Add(this.chargeAgentHistoryDGV);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chargeAmountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chargeCountLabel);
            this.Controls.Add(this.label1);
            this.Name = "ChargeAgentHistForm";
            this.Text = "充值记录";
            ((System.ComponentModel.ISupportInitialize)(this.chargeAgentHistoryDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label chargeCountLabel;
        private System.Windows.Forms.Label chargeAmountLabel;
        private System.Windows.Forms.DataGridView chargeAgentHistoryDGV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeReceive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chargeChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargeChecker;
        private System.Windows.Forms.Button CheckBtn;
    }
}