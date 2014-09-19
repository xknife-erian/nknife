namespace Jeelu.SimplusOM.Client
{
    partial class ChargeRecordsForm
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
            this.ChargeAmountLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ChargeNumLab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChargeRecordsDGV = new System.Windows.Forms.DataGridView();
            this.idcol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timecol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountcol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managercol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkcol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.checkmancol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChargeRecordsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Size = new System.Drawing.Size(602, 304);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ChargeAmountLab);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ChargeNumLab);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 70);
            this.panel1.TabIndex = 0;
            // 
            // ChargeAmountLab
            // 
            this.ChargeAmountLab.AutoSize = true;
            this.ChargeAmountLab.Location = new System.Drawing.Point(169, 39);
            this.ChargeAmountLab.Name = "ChargeAmountLab";
            this.ChargeAmountLab.Size = new System.Drawing.Size(41, 12);
            this.ChargeAmountLab.TabIndex = 0;
            this.ChargeAmountLab.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "累计充值金额：";
            // 
            // ChargeNumLab
            // 
            this.ChargeNumLab.AutoSize = true;
            this.ChargeNumLab.Location = new System.Drawing.Point(169, 13);
            this.ChargeNumLab.Name = "ChargeNumLab";
            this.ChargeNumLab.Size = new System.Drawing.Size(41, 12);
            this.ChargeNumLab.TabIndex = 0;
            this.ChargeNumLab.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "充值次数：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ChargeRecordsDGV);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(602, 234);
            this.panel2.TabIndex = 0;
            // 
            // ChargeRecordsDGV
            // 
            this.ChargeRecordsDGV.AllowUserToAddRows = false;
            this.ChargeRecordsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChargeRecordsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idcol,
            this.timecol,
            this.amountcol,
            this.managercol,
            this.checkcol,
            this.checkmancol});
            this.ChargeRecordsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargeRecordsDGV.Location = new System.Drawing.Point(0, 0);
            this.ChargeRecordsDGV.Name = "ChargeRecordsDGV";
            this.ChargeRecordsDGV.RowTemplate.Height = 23;
            this.ChargeRecordsDGV.Size = new System.Drawing.Size(602, 234);
            this.ChargeRecordsDGV.TabIndex = 0;
            // 
            // idcol
            // 
            this.idcol.HeaderText = "Column1";
            this.idcol.Name = "idcol";
            this.idcol.Visible = false;
            // 
            // timecol
            // 
            this.timecol.HeaderText = "时间";
            this.timecol.Name = "timecol";
            // 
            // amountcol
            // 
            this.amountcol.HeaderText = "金额";
            this.amountcol.Name = "amountcol";
            // 
            // managercol
            // 
            this.managercol.HeaderText = "负责人";
            this.managercol.Name = "managercol";
            // 
            // checkcol
            // 
            this.checkcol.HeaderText = "审核";
            this.checkcol.Name = "checkcol";
            this.checkcol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkcol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // checkmancol
            // 
            this.checkmancol.HeaderText = "审核人";
            this.checkmancol.Name = "checkmancol";
            // 
            // ChargeRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(602, 339);
            this.Name = "ChargeRecordsForm";
            this.Text = "充值记录";
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChargeRecordsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ChargeAmountLab;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ChargeNumLab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ChargeRecordsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcol;
        private System.Windows.Forms.DataGridViewTextBoxColumn timecol;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountcol;
        private System.Windows.Forms.DataGridViewTextBoxColumn managercol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkcol;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkmancol;
    }
}