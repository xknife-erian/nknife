namespace NKnife.Kits.NLog.NLog4Kit
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._SimpleTestButton = new System.Windows.Forms.ToolStripButton();
            this._Input100LogButton = new System.Windows.Forms.ToolStripButton();
            this._SetDebugModeButton = new System.Windows.Forms.ToolStripButton();
            this._LoggerFormButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SimpleTestButton,
            this._Input100LogButton,
            this._SetDebugModeButton,
            this._LoggerFormButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(866, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _SimpleTestButton
            // 
            this._SimpleTestButton.Image = ((System.Drawing.Image)(resources.GetObject("_SimpleTestButton.Image")));
            this._SimpleTestButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SimpleTestButton.Name = "_SimpleTestButton";
            this._SimpleTestButton.Size = new System.Drawing.Size(76, 22);
            this._SimpleTestButton.Text = "简单测试";
            this._SimpleTestButton.Click += new System.EventHandler(this._SimpleTestButton_Click);
            // 
            // _Input100LogButton
            // 
            this._Input100LogButton.Image = ((System.Drawing.Image)(resources.GetObject("_Input100LogButton.Image")));
            this._Input100LogButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._Input100LogButton.Name = "_Input100LogButton";
            this._Input100LogButton.Size = new System.Drawing.Size(157, 22);
            this._Input100LogButton.Text = "输出100条日志进行测试";
            this._Input100LogButton.Click += new System.EventHandler(this.Input100LogButton_Click);
            // 
            // _SetDebugModeButton
            // 
            this._SetDebugModeButton.Image = ((System.Drawing.Image)(resources.GetObject("_SetDebugModeButton.Image")));
            this._SetDebugModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SetDebugModeButton.Name = "_SetDebugModeButton";
            this._SetDebugModeButton.Size = new System.Drawing.Size(100, 22);
            this._SetDebugModeButton.Text = "设置调试模式";
            this._SetDebugModeButton.Click += new System.EventHandler(this._SetDebugModeButton_Click);
            // 
            // _LoggerFormButton
            // 
            this._LoggerFormButton.Image = ((System.Drawing.Image)(resources.GetObject("_LoggerFormButton.Image")));
            this._LoggerFormButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._LoggerFormButton.Name = "_LoggerFormButton";
            this._LoggerFormButton.Size = new System.Drawing.Size(76, 22);
            this._LoggerFormButton.Text = "日志窗体";
            this._LoggerFormButton.Click += new System.EventHandler(this._LoggerFormButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 504);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _Input100LogButton;
        private System.Windows.Forms.ToolStripButton _SetDebugModeButton;
        private System.Windows.Forms.ToolStripButton _SimpleTestButton;
        private System.Windows.Forms.ToolStripButton _LoggerFormButton;
    }
}

