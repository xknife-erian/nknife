namespace Jeelu.SimplusPagePreviewer
{
    partial class PerviewInfoForm
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
            this.labRequest = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labProName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "正在访问";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "占用端口号";
            // 
            // labRequest
            // 
            this.labRequest.AutoSize = true;
            this.labRequest.Location = new System.Drawing.Point(136, 41);
            this.labRequest.Name = "labRequest";
            this.labRequest.Size = new System.Drawing.Size(35, 13);
            this.labRequest.TabIndex = 2;
            this.labRequest.Text = "label3";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(136, 75);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(35, 13);
            this.labPort.TabIndex = 3;
            this.labPort.Text = "label4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(291, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "停止";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "预览项目";
            // 
            // labProName
            // 
            this.labProName.AutoSize = true;
            this.labProName.Location = new System.Drawing.Point(136, 10);
            this.labProName.Name = "labProName";
            this.labProName.Size = new System.Drawing.Size(35, 13);
            this.labProName.TabIndex = 6;
            this.labProName.Text = "label4";
            // 
            // PerviewInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 147);
            this.Controls.Add(this.labProName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.labRequest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "PerviewInfo";
            this.ShowIcon = false;
            this.Text = "SimplusD Preview Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labRequest;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labProName;
    }
}