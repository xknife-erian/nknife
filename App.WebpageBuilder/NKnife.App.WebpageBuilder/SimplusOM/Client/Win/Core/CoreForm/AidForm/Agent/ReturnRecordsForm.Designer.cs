namespace Jeelu.SimplusOM.Client
{
    partial class ReturnRecordsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.returnRecordDGV = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.returnAmountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.returnCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.returnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnSend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnReceive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.returnRecordDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // returnRecordDGV
            // 
            this.returnRecordDGV.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.returnRecordDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.returnRecordDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.returnRecordDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.returnId,
            this.returnDate,
            this.returnSend,
            this.returnReceive,
            this.returnAmount});
            this.returnRecordDGV.Location = new System.Drawing.Point(15, 80);
            this.returnRecordDGV.Name = "returnRecordDGV";
            this.returnRecordDGV.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.returnRecordDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.returnRecordDGV.RowHeadersWidth = 15;
            this.returnRecordDGV.RowTemplate.Height = 23;
            this.returnRecordDGV.Size = new System.Drawing.Size(394, 253);
            this.returnRecordDGV.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "元";
            // 
            // returnAmountLabel
            // 
            this.returnAmountLabel.AutoSize = true;
            this.returnAmountLabel.Location = new System.Drawing.Point(125, 51);
            this.returnAmountLabel.Name = "returnAmountLabel";
            this.returnAmountLabel.Size = new System.Drawing.Size(29, 12);
            this.returnAmountLabel.TabIndex = 8;
            this.returnAmountLabel.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "返点金额：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "次";
            // 
            // returnCountLabel
            // 
            this.returnCountLabel.AutoSize = true;
            this.returnCountLabel.Location = new System.Drawing.Point(125, 22);
            this.returnCountLabel.Name = "returnCountLabel";
            this.returnCountLabel.Size = new System.Drawing.Size(17, 12);
            this.returnCountLabel.TabIndex = 4;
            this.returnCountLabel.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "返点次数：";
            // 
            // returnId
            // 
            this.returnId.HeaderText = "Column1";
            this.returnId.Name = "returnId";
            this.returnId.ReadOnly = true;
            this.returnId.Visible = false;
            // 
            // returnDate
            // 
            this.returnDate.HeaderText = "返点时间";
            this.returnDate.Name = "returnDate";
            this.returnDate.ReadOnly = true;
            // 
            // returnSend
            // 
            this.returnSend.HeaderText = "返点方";
            this.returnSend.Name = "returnSend";
            this.returnSend.ReadOnly = true;
            // 
            // returnReceive
            // 
            this.returnReceive.HeaderText = "被返点方";
            this.returnReceive.Name = "returnReceive";
            this.returnReceive.ReadOnly = true;
            // 
            // returnAmount
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#.##";
            this.returnAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.returnAmount.HeaderText = "金额";
            this.returnAmount.Name = "returnAmount";
            this.returnAmount.ReadOnly = true;
            this.returnAmount.Width = 60;
            // 
            // ReturnRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 355);
            this.Controls.Add(this.returnRecordDGV);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.returnAmountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.returnCountLabel);
            this.Controls.Add(this.label1);
            this.Name = "ReturnRecordsForm";
            this.Text = "返点记录";
            ((System.ComponentModel.ISupportInitialize)(this.returnRecordDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView returnRecordDGV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label returnAmountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label returnCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnReceive;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnAmount;
    }
}