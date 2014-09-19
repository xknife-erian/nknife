namespace Jeelu.SimplusD.Client.Win
{
    partial class ProductImage
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
            this.btnAddImage = new System.Windows.Forms.Button();
            this.conPicShow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.conPicShow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(57, 171);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(83, 23);
            this.btnAddImage.TabIndex = 2;
            this.btnAddImage.Text = "button1";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // conPicShow
            // 
            this.conPicShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conPicShow.Location = new System.Drawing.Point(3, 0);
            this.conPicShow.Name = "conPicShow";
            this.conPicShow.Size = new System.Drawing.Size(202, 165);
            this.conPicShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.conPicShow.TabIndex = 5;
            this.conPicShow.TabStop = false;
            this.conPicShow.Paint += new System.Windows.Forms.PaintEventHandler(this.conPicShow_Paint);
            // 
            // ProductImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.conPicShow);
            this.Controls.Add(this.btnAddImage);
            this.Name = "ProductImage";
            this.Size = new System.Drawing.Size(207, 198);
            ((System.ComponentModel.ISupportInitialize)(this.conPicShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.PictureBox conPicShow;
    }
}
