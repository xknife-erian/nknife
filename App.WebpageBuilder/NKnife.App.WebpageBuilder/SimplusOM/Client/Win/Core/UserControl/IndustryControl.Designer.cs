namespace Jeelu.SimplusOM.Client
{
    partial class IndustryControl
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
            this.industry1ComboBox = new System.Windows.Forms.ComboBox();
            this.industry2ComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // industry1ComboBox
            // 
            this.industry1ComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.industry1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.industry1ComboBox.FormattingEnabled = true;
            this.industry1ComboBox.Location = new System.Drawing.Point(0, 0);
            this.industry1ComboBox.Name = "industry1ComboBox";
            this.industry1ComboBox.Size = new System.Drawing.Size(195, 20);
            this.industry1ComboBox.TabIndex = 11;
            this.industry1ComboBox.SelectedIndexChanged += new System.EventHandler(this.IndustryComBox_SelectedIndexChanged);
            // 
            // industry2ComboBox
            // 
            this.industry2ComboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.industry2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.industry2ComboBox.FormattingEnabled = true;
            this.industry2ComboBox.Location = new System.Drawing.Point(0, 22);
            this.industry2ComboBox.Name = "industry2ComboBox";
            this.industry2ComboBox.Size = new System.Drawing.Size(195, 20);
            this.industry2ComboBox.TabIndex = 10;
            this.industry2ComboBox.SelectedIndexChanged += new System.EventHandler(this.IndustryComBox_SelectedIndexChanged);
            // 
            // IndustryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.industry2ComboBox);
            this.Controls.Add(this.industry1ComboBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IndustryControl";
            this.Size = new System.Drawing.Size(195, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox industry1ComboBox;
        private System.Windows.Forms.ComboBox industry2ComboBox;


    }
}
