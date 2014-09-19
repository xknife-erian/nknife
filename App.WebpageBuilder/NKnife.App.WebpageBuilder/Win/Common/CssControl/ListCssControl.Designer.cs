namespace Jeelu.Win
{
    partial class ListCssControl
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
            this.lblStyle = new System.Windows.Forms.Label();
            this.lblProjSignImage = new System.Windows.Forms.Label();
            this.lblPostion = new System.Windows.Forms.Label();
            this.cbxStyle = new System.Windows.Forms.ComboBox();
            this.cbxProjSignImage = new System.Windows.Forms.ComboBox();
            this.cbxPostion = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(68, 25);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(53, 12);
            this.lblStyle.TabIndex = 0;
            this.lblStyle.Text = "类型(&T):";
            // 
            // lblProjSignImage
            // 
            this.lblProjSignImage.AutoSize = true;
            this.lblProjSignImage.Location = new System.Drawing.Point(20, 53);
            this.lblProjSignImage.Name = "lblProjSignImage";
            this.lblProjSignImage.Size = new System.Drawing.Size(101, 12);
            this.lblProjSignImage.TabIndex = 1;
            this.lblProjSignImage.Text = "项目符号图像(&B):";
            // 
            // lblPostion
            // 
            this.lblPostion.AutoSize = true;
            this.lblPostion.Location = new System.Drawing.Point(68, 80);
            this.lblPostion.Name = "lblPostion";
            this.lblPostion.Size = new System.Drawing.Size(53, 12);
            this.lblPostion.TabIndex = 2;
            this.lblPostion.Text = "位置(&P):";
            // 
            // cbxStyle
            // 
            this.cbxStyle.FormattingEnabled = true;
            this.cbxStyle.Location = new System.Drawing.Point(124, 22);
            this.cbxStyle.Name = "cbxStyle";
            this.cbxStyle.Size = new System.Drawing.Size(125, 20);
            this.cbxStyle.TabIndex = 3;
            // 
            // cbxProjSignImage
            // 
            this.cbxProjSignImage.FormattingEnabled = true;
            this.cbxProjSignImage.Location = new System.Drawing.Point(124, 50);
            this.cbxProjSignImage.Name = "cbxProjSignImage";
            this.cbxProjSignImage.Size = new System.Drawing.Size(207, 20);
            this.cbxProjSignImage.TabIndex = 4;
            // 
            // cbxPostion
            // 
            this.cbxPostion.FormattingEnabled = true;
            this.cbxPostion.Location = new System.Drawing.Point(124, 77);
            this.cbxPostion.Name = "cbxPostion";
            this.cbxPostion.Size = new System.Drawing.Size(91, 20);
            this.cbxPostion.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(337, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ListCssControl
            // 
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cbxPostion);
            this.Controls.Add(this.cbxProjSignImage);
            this.Controls.Add(this.cbxStyle);
            this.Controls.Add(this.lblPostion);
            this.Controls.Add(this.lblProjSignImage);
            this.Controls.Add(this.lblStyle);
            this.Name = "ListCssControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblProjSignImage;
        private System.Windows.Forms.Label lblPostion;
        private System.Windows.Forms.ComboBox cbxStyle;
        private System.Windows.Forms.ComboBox cbxProjSignImage;
        private System.Windows.Forms.ComboBox cbxPostion;
        private System.Windows.Forms.Button btnBrowse;
    }
}
