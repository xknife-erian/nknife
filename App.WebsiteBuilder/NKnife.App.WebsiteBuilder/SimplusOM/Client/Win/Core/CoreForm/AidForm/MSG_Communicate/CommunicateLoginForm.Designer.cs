namespace Jeelu.SimplusOM.Client
{
    partial class CommunicateLoginForm
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
            this.communicateLogDGV = new System.Windows.Forms.DataGridView();
            this.comTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchBtn = new System.Windows.Forms.Button();
            this.beginDTP = new System.Windows.Forms.DateTimePicker();
            this.endDTP = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.communicateLogDGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.tableLayoutPanel1);
            this.MainPanel.Size = new System.Drawing.Size(578, 346);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.communicateLogDGV, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 346);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // communicateLogDGV
            // 
            this.communicateLogDGV.AllowUserToAddRows = false;
            this.communicateLogDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.communicateLogDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.comTime,
            this.comContent});
            this.communicateLogDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicateLogDGV.Location = new System.Drawing.Point(3, 63);
            this.communicateLogDGV.Name = "communicateLogDGV";
            this.communicateLogDGV.ReadOnly = true;
            this.communicateLogDGV.RowTemplate.Height = 23;
            this.communicateLogDGV.Size = new System.Drawing.Size(572, 280);
            this.communicateLogDGV.TabIndex = 12;
            this.communicateLogDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.communicateLogDGV_CellDoubleClick);
            // 
            // comTime
            // 
            this.comTime.HeaderText = "时间";
            this.comTime.Name = "comTime";
            this.comTime.ReadOnly = true;
            // 
            // comContent
            // 
            this.comContent.HeaderText = "内容";
            this.comContent.Name = "comContent";
            this.comContent.ReadOnly = true;
            this.comContent.Width = 400;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchBtn);
            this.panel1.Controls.Add(this.beginDTP);
            this.panel1.Controls.Add(this.endDTP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 54);
            this.panel1.TabIndex = 0;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(355, 9);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 14;
            this.searchBtn.Text = "查询";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // beginDTP
            // 
            this.beginDTP.Location = new System.Drawing.Point(19, 12);
            this.beginDTP.Name = "beginDTP";
            this.beginDTP.ShowCheckBox = true;
            this.beginDTP.Size = new System.Drawing.Size(125, 21);
            this.beginDTP.TabIndex = 13;
            // 
            // endDTP
            // 
            this.endDTP.Location = new System.Drawing.Point(194, 12);
            this.endDTP.Name = "endDTP";
            this.endDTP.ShowCheckBox = true;
            this.endDTP.Size = new System.Drawing.Size(110, 21);
            this.endDTP.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "到";
            // 
            // CommunicateLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 381);
            this.Name = "CommunicateLoginForm";
            this.TabText = "沟通记录";
            this.Text = "沟通记录";
            this.MainPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.communicateLogDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView communicateLogDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn comTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn comContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.DateTimePicker beginDTP;
        private System.Windows.Forms.DateTimePicker endDTP;
        private System.Windows.Forms.Label label3;


    }
}