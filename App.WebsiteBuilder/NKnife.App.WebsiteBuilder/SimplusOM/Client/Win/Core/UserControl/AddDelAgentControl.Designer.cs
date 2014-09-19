namespace Jeelu.SimplusOM.Client
{
    partial class AddDelAgentControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.AddAreaListBox = new System.Windows.Forms.ListBox();
            this.AllAreaListBox = new System.Windows.Forms.ListBox();
            this.DelAreaBtn = new System.Windows.Forms.Button();
            this.AddAreaBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddAreaListBox
            // 
            this.AddAreaListBox.FormattingEnabled = true;
            this.AddAreaListBox.ItemHeight = 12;
            this.AddAreaListBox.Location = new System.Drawing.Point(249, 1);
            this.AddAreaListBox.Name = "AddAreaListBox";
            this.AddAreaListBox.Size = new System.Drawing.Size(155, 64);
            this.AddAreaListBox.TabIndex = 11;
            // 
            // AllAreaListBox
            // 
            this.AllAreaListBox.FormattingEnabled = true;
            this.AllAreaListBox.ItemHeight = 12;
            this.AllAreaListBox.Location = new System.Drawing.Point(3, 3);
            this.AllAreaListBox.Name = "AllAreaListBox";
            this.AllAreaListBox.Size = new System.Drawing.Size(155, 64);
            this.AllAreaListBox.TabIndex = 8;
            // 
            // DelAreaBtn
            // 
            this.DelAreaBtn.Location = new System.Drawing.Point(172, 36);
            this.DelAreaBtn.Name = "DelAreaBtn";
            this.DelAreaBtn.Size = new System.Drawing.Size(54, 23);
            this.DelAreaBtn.TabIndex = 10;
            this.DelAreaBtn.Text = "<<";
            this.DelAreaBtn.UseVisualStyleBackColor = true;
            this.DelAreaBtn.Click += new System.EventHandler(this.DelAreaBtn_Click);
            // 
            // AddAreaBtn
            // 
            this.AddAreaBtn.Location = new System.Drawing.Point(172, 7);
            this.AddAreaBtn.Name = "AddAreaBtn";
            this.AddAreaBtn.Size = new System.Drawing.Size(54, 23);
            this.AddAreaBtn.TabIndex = 9;
            this.AddAreaBtn.Text = ">>";
            this.AddAreaBtn.UseVisualStyleBackColor = true;
            this.AddAreaBtn.Click += new System.EventHandler(this.AddAreaBtn_Click);
            // 
            // AddDelAreaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddAreaListBox);
            this.Controls.Add(this.AllAreaListBox);
            this.Controls.Add(this.DelAreaBtn);
            this.Controls.Add(this.AddAreaBtn);
            this.Name = "AddDelAreaControl";
            this.Size = new System.Drawing.Size(406, 68);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AddAreaListBox;
        private System.Windows.Forms.ListBox AllAreaListBox;
        private System.Windows.Forms.Button DelAreaBtn;
        private System.Windows.Forms.Button AddAreaBtn;
    }
}
