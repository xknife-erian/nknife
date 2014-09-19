namespace Jeelu.SimplusSoftwareUpdate
{
    partial class SetupFinish
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
            this.chkRunSimplusD = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkRunSimplusD
            // 
            this.chkRunSimplusD.AutoSize = true;
            this.chkRunSimplusD.Checked = true;
            this.chkRunSimplusD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunSimplusD.Location = new System.Drawing.Point(323, 119);
            this.chkRunSimplusD.Name = "chkRunSimplusD";
            this.chkRunSimplusD.Size = new System.Drawing.Size(108, 16);
            this.chkRunSimplusD.TabIndex = 0;
            this.chkRunSimplusD.Text = "运行SimplusD！";
            this.chkRunSimplusD.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(333, 173);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(40, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "更新SimplusD！成功完成！";
            // 
            // SetupFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 243);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkRunSimplusD);
            this.MaximizeBox = false;
            this.Name = "SetupFinish";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安装完成";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetupFinish_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRunSimplusD;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}