namespace Jeelu
{
    partial class CloseAllWindowSaveForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnYes = new System.Windows.Forms.Button();
            this.btnAllYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnAllNo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblShowMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(13, 60);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnAllYes
            // 
            this.btnAllYes.Location = new System.Drawing.Point(92, 60);
            this.btnAllYes.Name = "btnAllYes";
            this.btnAllYes.Size = new System.Drawing.Size(75, 23);
            this.btnAllYes.TabIndex = 1;
            this.btnAllYes.Text = "all yes";
            this.btnAllYes.UseVisualStyleBackColor = true;
            this.btnAllYes.Click += new System.EventHandler(this.btnAllYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(171, 60);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "no";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnAllNo
            // 
            this.btnAllNo.Location = new System.Drawing.Point(250, 60);
            this.btnAllNo.Name = "btnAllNo";
            this.btnAllNo.Size = new System.Drawing.Size(75, 23);
            this.btnAllNo.TabIndex = 3;
            this.btnAllNo.Text = "all no";
            this.btnAllNo.UseVisualStyleBackColor = true;
            this.btnAllNo.Click += new System.EventHandler(this.btnAllNo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(329, 60);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblShowMsg
            // 
            this.lblShowMsg.Location = new System.Drawing.Point(13, 17);
            this.lblShowMsg.Name = "lblShowMsg";
            this.lblShowMsg.Size = new System.Drawing.Size(391, 28);
            this.lblShowMsg.TabIndex = 5;
            this.lblShowMsg.Text = "lblShowMsg";
            // 
            // CloseAllWindowSaveOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(417, 95);
            this.Controls.Add(this.lblShowMsg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAllNo);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnAllYes);
            this.Controls.Add(this.btnYes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloseAllWindowSaveOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnAllYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnAllNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblShowMsg;
    }
}