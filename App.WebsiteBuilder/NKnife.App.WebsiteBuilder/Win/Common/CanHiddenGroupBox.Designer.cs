namespace Jeelu.Win
{
    partial class CanHiddenGroupBox
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
            this.components = new System.ComponentModel.Container();
            this.buttonHidden = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.groupBoxContent = new System.Windows.Forms.GroupBox();
            this.imageListButtonHidden = new System.Windows.Forms.ImageList(this.components);
            this.singleLineLine = new Jeelu.Win.SingleLine();
            this.SuspendLayout();
            // 
            // buttonHidden
            // 
            this.buttonHidden.Location = new System.Drawing.Point(0, 0);
            this.buttonHidden.Name = "buttonHidden";
            this.buttonHidden.Size = new System.Drawing.Size(15, 15);
            this.buttonHidden.TabIndex = 0;
            this.buttonHidden.UseVisualStyleBackColor = true;
            this.buttonHidden.Click += new System.EventHandler(this.buttonHidden_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Font = new System.Drawing.Font("宋体", 9F);
            this.labelText.Location = new System.Drawing.Point(21, 2);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(83, 12);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "可隐藏groubox";
            // 
            // groupBoxContent
            // 
            this.groupBoxContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxContent.Location = new System.Drawing.Point(1, 3);
            this.groupBoxContent.Name = "groupBoxContent";
            this.groupBoxContent.Size = new System.Drawing.Size(217, 212);
            this.groupBoxContent.TabIndex = 3;
            this.groupBoxContent.TabStop = false;
            this.groupBoxContent.Text = "  可隐藏groubox";
            // 
            // imageListButtonHidden
            // 
            this.imageListButtonHidden.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListButtonHidden.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListButtonHidden.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // singleLineLine
            // 
            this.singleLineLine.LineStyle = Jeelu.Win.LineStyle.Standard;
            this.singleLineLine.Location = new System.Drawing.Point(104, 8);
            this.singleLineLine.Name = "singleLineLine";
            this.singleLineLine.Size = new System.Drawing.Size(113, 2);
            this.singleLineLine.TabIndex = 2;
            this.singleLineLine.TabStop = false;
            // 
            // CanHiddenGroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonHidden);
            this.Controls.Add(this.groupBoxContent);
            this.Controls.Add(this.singleLineLine);
            this.Controls.Add(this.labelText);
            this.Name = "CanHiddenGroupBox";
            this.Size = new System.Drawing.Size(220, 228);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonHidden;
        private System.Windows.Forms.Label labelText;
        private SingleLine singleLineLine;
        private System.Windows.Forms.GroupBox groupBoxContent;
        private System.Windows.Forms.ImageList imageListButtonHidden;
    }
}
