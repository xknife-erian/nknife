namespace Jeelu.Win
{
    partial class CssFieldUnit
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
            this.cbbUnit = new System.Windows.Forms.ComboBox();
            this.cbbField = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbUnit
            // 
            this.cbbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbUnit.Enabled = false;
            this.cbbUnit.FormattingEnabled = true;
            this.cbbUnit.Items.AddRange(new object[] {
            "像素(px)",
            "点数(pt)",
            "英寸(in)",
            "厘米(cm)",
            "毫米(mm)",
            "12pt字(pc)",
            "字体高(em)",
            "字母x的高(ex)",
            "%"});
            this.cbbUnit.Location = new System.Drawing.Point(70, 1);
            this.cbbUnit.Size = new System.Drawing.Size(80, 20);
            this.cbbUnit.TabIndex = 0;
            // 
            // cbbField
            // 
            this.cbbField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbField.FormattingEnabled = true;
            this.cbbField.Location = new System.Drawing.Point(3, 1);
            this.cbbField.Size = new System.Drawing.Size(65, 20);
            this.cbbField.TabIndex = 0;
            // 
            // CssFieldUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbField);
            this.Controls.Add(this.cbbUnit);
            this.Name = "CssFieldUnit";
            this.Size = new System.Drawing.Size(153, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbUnit;
        private System.Windows.Forms.ComboBox cbbField;
    }
}
