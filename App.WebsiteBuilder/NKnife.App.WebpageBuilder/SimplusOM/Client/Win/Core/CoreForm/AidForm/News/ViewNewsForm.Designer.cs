namespace Jeelu.SimplusOM.Client
{
    partial class ViewNewsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBtn = new System.Windows.Forms.Button();
            this.endDTP = new System.Windows.Forms.DateTimePicker();
            this.beginDTP = new System.Windows.Forms.DateTimePicker();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newsDGV = new System.Windows.Forms.DataGridView();
            this.newsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.newsDGV, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(602, 260);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchBtn);
            this.panel1.Controls.Add(this.endDTP);
            this.panel1.Controls.Add(this.beginDTP);
            this.panel1.Controls.Add(this.titleTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 59);
            this.panel1.TabIndex = 0;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(512, 25);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 3;
            this.searchBtn.Text = "查询";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // endDTP
            // 
            this.endDTP.Location = new System.Drawing.Point(376, 26);
            this.endDTP.Name = "endDTP";
            this.endDTP.ShowCheckBox = true;
            this.endDTP.Size = new System.Drawing.Size(123, 21);
            this.endDTP.TabIndex = 2;
            // 
            // beginDTP
            // 
            this.beginDTP.Location = new System.Drawing.Point(221, 26);
            this.beginDTP.Name = "beginDTP";
            this.beginDTP.ShowCheckBox = true;
            this.beginDTP.Size = new System.Drawing.Size(126, 21);
            this.beginDTP.TabIndex = 2;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(74, 26);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(66, 21);
            this.titleTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "发布时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题：";
            // 
            // newsDGV
            // 
            this.newsDGV.AllowUserToAddRows = false;
            this.newsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.newsId,
            this.newsTitle,
            this.newsCol,
            this.newsTime,
            this.newsContent});
            this.newsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsDGV.Location = new System.Drawing.Point(3, 68);
            this.newsDGV.Name = "newsDGV";
            this.newsDGV.ReadOnly = true;
            this.newsDGV.RowTemplate.Height = 23;
            this.newsDGV.Size = new System.Drawing.Size(596, 189);
            this.newsDGV.TabIndex = 1;
            // 
            // newsId
            // 
            this.newsId.HeaderText = "id";
            this.newsId.Name = "newsId";
            this.newsId.ReadOnly = true;
            this.newsId.Visible = false;
            // 
            // newsTitle
            // 
            this.newsTitle.HeaderText = "标题";
            this.newsTitle.Name = "newsTitle";
            this.newsTitle.ReadOnly = true;
            // 
            // newsCol
            // 
            this.newsCol.HeaderText = "栏目";
            this.newsCol.Name = "newsCol";
            this.newsCol.ReadOnly = true;
            // 
            // newsTime
            // 
            this.newsTime.HeaderText = "发布时间";
            this.newsTime.Name = "newsTime";
            this.newsTime.ReadOnly = true;
            // 
            // newsContent
            // 
            this.newsContent.HeaderText = "内容";
            this.newsContent.Name = "newsContent";
            this.newsContent.ReadOnly = true;
            this.newsContent.Width = 300;
            // 
            // ViewNewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(602, 295);
            this.Name = "ViewNewsForm";
            this.TabText = "新闻管理";
            this.Text = "新闻管理";
            this.MainPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker endDTP;
        private System.Windows.Forms.DateTimePicker beginDTP;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView newsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsId;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn newsContent;
    }
}