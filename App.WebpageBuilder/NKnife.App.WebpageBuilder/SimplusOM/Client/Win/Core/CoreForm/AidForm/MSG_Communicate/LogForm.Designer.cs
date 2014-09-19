namespace Jeelu.SimplusOM.Client
{
    partial class LoginLogForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginLogDGV = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logoutTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.loginLogDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "累计登录次数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "15次";
            // 
            // loginLogDGV
            // 
            this.loginLogDGV.AllowUserToAddRows = false;
            this.loginLogDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loginLogDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.loginDate,
            this.loginTime,
            this.logoutTime});
            this.loginLogDGV.Location = new System.Drawing.Point(15, 49);
            this.loginLogDGV.Name = "loginLogDGV";
            this.loginLogDGV.RowHeadersWidth = 15;
            this.loginLogDGV.RowTemplate.Height = 23;
            this.loginLogDGV.Size = new System.Drawing.Size(319, 205);
            this.loginLogDGV.TabIndex = 2;
            // 
            // ID
            // 
            this.ID.HeaderText = "Column1";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // loginDate
            // 
            this.loginDate.HeaderText = "登录日期";
            this.loginDate.Name = "loginDate";
            // 
            // loginTime
            // 
            this.loginTime.HeaderText = "登录时间";
            this.loginTime.Name = "loginTime";
            // 
            // logoutTime
            // 
            this.logoutTime.HeaderText = "登出时间";
            this.logoutTime.Name = "logoutTime";
            // 
            // LoginLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 266);
            this.Controls.Add(this.loginLogDGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginLogForm";
            this.Text = "登录记录";
            ((System.ComponentModel.ISupportInitialize)(this.loginLogDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView loginLogDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn logoutTime;
    }
}