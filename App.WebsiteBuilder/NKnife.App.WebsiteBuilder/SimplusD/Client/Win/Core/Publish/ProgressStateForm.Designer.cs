namespace Jeelu.SimplusD.Client.Win
{
    partial class ProgressStateForm
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
            this.components = new System.ComponentModel.Container();
            this.progressRate = new System.Windows.Forms.ProgressBar();
            this.lblShowMsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressRate
            // 
            this.progressRate.Location = new System.Drawing.Point(12, 32);
            this.progressRate.Name = "progressRate";
            this.progressRate.Size = new System.Drawing.Size(604, 23);
            this.progressRate.TabIndex = 0;
            // 
            // lblShowMsg
            // 
            this.lblShowMsg.AutoSize = true;
            this.lblShowMsg.Location = new System.Drawing.Point(15, 12);
            this.lblShowMsg.Name = "lblShowMsg";
            this.lblShowMsg.Size = new System.Drawing.Size(0, 12);
            this.lblShowMsg.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(541, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProgressStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 95);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblShowMsg);
            this.Controls.Add(this.progressRate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressStateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传进度";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressRate;
        private System.Windows.Forms.Label lblShowMsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCancel;

    }
}