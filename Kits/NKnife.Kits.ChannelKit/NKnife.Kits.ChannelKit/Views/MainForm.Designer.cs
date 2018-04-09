namespace NKnife.Kits.ChannelKit.Views
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.simpleSerialPanel1 = new SingleSerialPanel();
            this.simpleSerialPanel2 = new SingleSerialPanel();
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
            this.splitContainer1.Location = new System.Drawing.Point(4, 1);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleSerialPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.simpleSerialPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 753);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.SplitterWidth = 14;
            this.splitContainer1.TabIndex = 0;
            // 
            // simpleSerialPanel1
            // 
            this.simpleSerialPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleSerialPanel1.Location = new System.Drawing.Point(0, 0);
            this.simpleSerialPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simpleSerialPanel1.Name = "simpleSerialPanel1";
            this.simpleSerialPanel1.Size = new System.Drawing.Size(500, 753);
            this.simpleSerialPanel1.TabIndex = 0;
            // 
            // simpleSerialPanel2
            // 
            this.simpleSerialPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleSerialPanel2.Location = new System.Drawing.Point(0, 0);
            this.simpleSerialPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simpleSerialPanel2.Name = "simpleSerialPanel2";
            this.simpleSerialPanel2.Size = new System.Drawing.Size(486, 753);
            this.simpleSerialPanel2.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 757);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(4, 1, 4, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NKnife串口调试器";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SingleSerialPanel simpleSerialPanel1;
        private SingleSerialPanel simpleSerialPanel2;
    }
}

