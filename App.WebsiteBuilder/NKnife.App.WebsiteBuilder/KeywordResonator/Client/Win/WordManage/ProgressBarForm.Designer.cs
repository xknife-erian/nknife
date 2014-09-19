namespace Jeelu.KeywordResonator.Client
{
    partial class ProgressBarForm
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
            this.progressFecthWord = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lableTotalPage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LableGetedPage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lableElapseTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelFailedPage = new System.Windows.Forms.Label();
            this.lstBoxUrls = new System.Windows.Forms.ListBox();
            this.timer_ElapseTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressFecthWord
            // 
            this.progressFecthWord.Location = new System.Drawing.Point(23, 12);
            this.progressFecthWord.Name = "progressFecthWord";
            this.progressFecthWord.Size = new System.Drawing.Size(492, 21);
            this.progressFecthWord.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "共:";
            // 
            // lableTotalPage
            // 
            this.lableTotalPage.AutoSize = true;
            this.lableTotalPage.Location = new System.Drawing.Point(44, 48);
            this.lableTotalPage.Name = "lableTotalPage";
            this.lableTotalPage.Size = new System.Drawing.Size(0, 13);
            this.lableTotalPage.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "页";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "成功:";
            // 
            // LableGetedPage
            // 
            this.LableGetedPage.AutoSize = true;
            this.LableGetedPage.Location = new System.Drawing.Point(146, 48);
            this.LableGetedPage.Name = "LableGetedPage";
            this.LableGetedPage.Size = new System.Drawing.Size(25, 13);
            this.LableGetedPage.TabIndex = 5;
            this.LableGetedPage.Text = "0    ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "耗时:";
            // 
            // lableElapseTime
            // 
            this.lableElapseTime.AutoSize = true;
            this.lableElapseTime.Location = new System.Drawing.Point(339, 48);
            this.lableElapseTime.Name = "lableElapseTime";
            this.lableElapseTime.Size = new System.Drawing.Size(51, 13);
            this.lableElapseTime.TabIndex = 8;
            this.lableElapseTime.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "网页抓取信息:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "失败:";
            // 
            // labelFailedPage
            // 
            this.labelFailedPage.AutoSize = true;
            this.labelFailedPage.Location = new System.Drawing.Point(243, 48);
            this.labelFailedPage.Name = "labelFailedPage";
            this.labelFailedPage.Size = new System.Drawing.Size(13, 13);
            this.labelFailedPage.TabIndex = 13;
            this.labelFailedPage.Text = "0";
            // 
            // lstBoxUrls
            // 
            this.lstBoxUrls.FormattingEnabled = true;
            this.lstBoxUrls.HorizontalScrollbar = true;
            this.lstBoxUrls.Location = new System.Drawing.Point(23, 89);
            this.lstBoxUrls.Name = "lstBoxUrls";
            this.lstBoxUrls.ScrollAlwaysVisible = true;
            this.lstBoxUrls.Size = new System.Drawing.Size(492, 186);
            this.lstBoxUrls.TabIndex = 15;
            // 
            // timer_ElapseTime
            // 
            this.timer_ElapseTime.Enabled = true;
            this.timer_ElapseTime.Interval = 1000;
            this.timer_ElapseTime.Tick += new System.EventHandler(this.timer_ElapseTime_Tick);
            // 
            // ProgressBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 294);
            this.ControlBox = false;
            this.Controls.Add(this.lstBoxUrls);
            this.Controls.Add(this.labelFailedPage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lableElapseTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LableGetedPage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lableTotalPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressFecthWord);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProgressBarForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "网页抓取信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressFecthWord;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lableTotalPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LableGetedPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lableElapseTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelFailedPage;
        private System.Windows.Forms.ListBox lstBoxUrls;
        private System.Windows.Forms.Timer timer_ElapseTime;
    }
}