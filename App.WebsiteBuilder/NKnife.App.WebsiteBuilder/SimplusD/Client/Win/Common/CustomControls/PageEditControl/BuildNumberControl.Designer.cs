namespace Jeelu.SimplusD.Client.Win
{
    partial class BuildNumberControl
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBuildNumber = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnBuildNumber
            // 
            this.btnBuildNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnBuildNumber.Location = new System.Drawing.Point(186, 3);
            this.btnBuildNumber.Name = "btnBuildNumber";
            this.btnBuildNumber.Size = new System.Drawing.Size(63, 19);
            this.btnBuildNumber.TabIndex = 1;
            this.btnBuildNumber.Text = "button1";
            this.btnBuildNumber.UseVisualStyleBackColor = true;
            this.btnBuildNumber.Click += new System.EventHandler(this.button1_Click);
            // 
            // BuildNumberControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBuildNumber);
            this.Controls.Add(this.textBox1);
            this.Name = "BuildNumberControl";
            this.Size = new System.Drawing.Size(254, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBuildNumber;
    }
}
