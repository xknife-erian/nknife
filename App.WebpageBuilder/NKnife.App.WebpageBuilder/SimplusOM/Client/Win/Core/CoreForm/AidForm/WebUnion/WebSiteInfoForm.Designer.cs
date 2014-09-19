namespace Jeelu.SimplusOM.Client
{
    partial class WebSiteInfoForm
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
            this.statDGV = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.sumIncomeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ipclickCountLabel = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipClick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.income = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.statDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // statDGV
            // 
            this.statDGV.AllowUserToAddRows = false;
            this.statDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.siteURL,
            this.siteName,
            this.ipClick,
            this.income});
            this.statDGV.Location = new System.Drawing.Point(23, 69);
            this.statDGV.Name = "statDGV";
            this.statDGV.ReadOnly = true;
            this.statDGV.RowTemplate.Height = 23;
            this.statDGV.Size = new System.Drawing.Size(493, 256);
            this.statDGV.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "总收入：";
            // 
            // sumIncomeLabel
            // 
            this.sumIncomeLabel.AutoSize = true;
            this.sumIncomeLabel.Location = new System.Drawing.Point(182, 42);
            this.sumIncomeLabel.Name = "sumIncomeLabel";
            this.sumIncomeLabel.Size = new System.Drawing.Size(11, 12);
            this.sumIncomeLabel.TabIndex = 10;
            this.sumIncomeLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "广告点击总量：";
            // 
            // ipclickCountLabel
            // 
            this.ipclickCountLabel.AutoSize = true;
            this.ipclickCountLabel.Location = new System.Drawing.Point(182, 18);
            this.ipclickCountLabel.Name = "ipclickCountLabel";
            this.ipclickCountLabel.Size = new System.Drawing.Size(11, 12);
            this.ipclickCountLabel.TabIndex = 10;
            this.ipclickCountLabel.Text = "0";
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // siteURL
            // 
            this.siteURL.HeaderText = "网址";
            this.siteURL.Name = "siteURL";
            this.siteURL.ReadOnly = true;
            this.siteURL.Width = 150;
            // 
            // siteName
            // 
            this.siteName.HeaderText = "名称";
            this.siteName.Name = "siteName";
            this.siteName.ReadOnly = true;
            this.siteName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ipClick
            // 
            this.ipClick.HeaderText = "点击";
            this.ipClick.Name = "ipClick";
            this.ipClick.ReadOnly = true;
            this.ipClick.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // income
            // 
            dataGridViewCellStyle1.Format = "#.##";
            this.income.DefaultCellStyle = dataGridViewCellStyle1;
            this.income.HeaderText = "收入";
            this.income.Name = "income";
            this.income.ReadOnly = true;
            // 
            // WebSiteInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 336);
            this.Controls.Add(this.statDGV);
            this.Controls.Add(this.ipclickCountLabel);
            this.Controls.Add(this.sumIncomeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "WebSiteInfoForm";
            this.Text = "网站资料";
            ((System.ComponentModel.ISupportInitialize)(this.statDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView statDGV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label sumIncomeLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ipclickCountLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipClick;
        private System.Windows.Forms.DataGridViewTextBoxColumn income;
    }
}