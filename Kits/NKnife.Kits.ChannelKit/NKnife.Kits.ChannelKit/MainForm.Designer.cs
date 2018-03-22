namespace NKnife.Kits.ChannelKit
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.simpleSerialPanel1 = new NKnife.Kits.ChannelKit.SimpleSerialPanel();
            this.simpleSerialPanel2 = new NKnife.Kits.ChannelKit.SimpleSerialPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleSerialPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.simpleSerialPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(778, 557);
            this.splitContainer1.SplitterDistance = 389;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 0;
            // 
            // simpleSerialPanel1
            // 
            this.simpleSerialPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleSerialPanel1.Location = new System.Drawing.Point(0, 0);
            this.simpleSerialPanel1.Name = "simpleSerialPanel1";
            this.simpleSerialPanel1.Size = new System.Drawing.Size(389, 557);
            this.simpleSerialPanel1.TabIndex = 0;
            // 
            // simpleSerialPanel2
            // 
            this.simpleSerialPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleSerialPanel2.Location = new System.Drawing.Point(0, 0);
            this.simpleSerialPanel2.Name = "simpleSerialPanel2";
            this.simpleSerialPanel2.Size = new System.Drawing.Size(377, 557);
            this.simpleSerialPanel2.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SimpleSerialPanel simpleSerialPanel1;
        private SimpleSerialPanel simpleSerialPanel2;
    }
}

