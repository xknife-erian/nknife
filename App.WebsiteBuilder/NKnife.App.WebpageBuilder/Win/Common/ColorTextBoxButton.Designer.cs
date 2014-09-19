namespace Jeelu.Win
{
    partial class ColorTextBoxButton
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
            this.textBoxColor = new System.Windows.Forms.TextBox();
            this.btnColor = new Jeelu.Win.ColorGeneralButton();
            this.SuspendLayout();
            // 
            // textBoxColor
            // 
            this.textBoxColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxColor.Location = new System.Drawing.Point(23, 0);
            this.textBoxColor.Name = "textBoxColor";
            this.textBoxColor.Size = new System.Drawing.Size(65, 21);
            this.textBoxColor.TabIndex = 0;
            // 
            // btnColor
            // 
            this.btnColor.ColorArgbString = "";
            this.btnColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnColor.Location = new System.Drawing.Point(0, 0);
            this.btnColor.MyColor = System.Drawing.Color.Black;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 22);
            this.btnColor.TabIndex = 10;
            this.btnColor.Text = "Black";
            this.btnColor.MyColorChanged += new System.EventHandler(this.btnColor_MyColorChanged);
            // 
            // ColorTextBoxButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxColor);
            this.Controls.Add(this.btnColor);
            this.Name = "ColorTextBoxButton";
            this.Size = new System.Drawing.Size(90, 22);
            this.Leave += new System.EventHandler(this.ColorTextBoxButton_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxColor;
        private ColorGeneralButton btnColor;
    }
}
