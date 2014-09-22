namespace Jeelu.SimplusD.Client.Win
{
    partial class ProductProperty
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
            this.lblPropertyGroupChoose = new System.Windows.Forms.Label();
            this.conProductPropPanel = new System.Windows.Forms.Panel();
            this.btnDelProp = new System.Windows.Forms.Button();
            this.btnModifyProp = new System.Windows.Forms.Button();
            this.conProductProp = new System.Windows.Forms.ComboBox();
            this.btnAddProp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPropertyGroupChoose
            // 
            this.lblPropertyGroupChoose.AutoSize = true;
            this.lblPropertyGroupChoose.Location = new System.Drawing.Point(-3, 7);
            this.lblPropertyGroupChoose.Name = "lblPropertyGroupChoose";
            this.lblPropertyGroupChoose.Size = new System.Drawing.Size(77, 12);
            this.lblPropertyGroupChoose.TabIndex = 23;
            this.lblPropertyGroupChoose.Text = "GroupChoose:";
            // 
            // conProductPropPanel
            // 
            this.conProductPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.conProductPropPanel.AutoScroll = true;
            this.conProductPropPanel.Location = new System.Drawing.Point(0, 31);
            this.conProductPropPanel.Name = "conProductPropPanel";
            this.conProductPropPanel.Size = new System.Drawing.Size(592, 100);
            this.conProductPropPanel.TabIndex = 28;
            // 
            // btnDelProp
            // 
            this.btnDelProp.Location = new System.Drawing.Point(302, 2);
            this.btnDelProp.Name = "btnDelProp";
            this.btnDelProp.Size = new System.Drawing.Size(45, 23);
            this.btnDelProp.TabIndex = 27;
            this.btnDelProp.Text = "Del";
            this.btnDelProp.UseVisualStyleBackColor = true;
            this.btnDelProp.Click += new System.EventHandler(this.btnDelProp_Click);
            // 
            // btnModifyProp
            // 
            this.btnModifyProp.Location = new System.Drawing.Point(255, 2);
            this.btnModifyProp.Name = "btnModifyProp";
            this.btnModifyProp.Size = new System.Drawing.Size(45, 23);
            this.btnModifyProp.TabIndex = 26;
            this.btnModifyProp.Text = "Modi";
            this.btnModifyProp.UseVisualStyleBackColor = true;
            this.btnModifyProp.Click += new System.EventHandler(this.btnModifyProp_Click);
            // 
            // conProductProp
            // 
            this.conProductProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conProductProp.FormattingEnabled = true;
            this.conProductProp.Location = new System.Drawing.Point(82, 4);
            this.conProductProp.Name = "conProductProp";
            this.conProductProp.Size = new System.Drawing.Size(120, 20);
            this.conProductProp.TabIndex = 24;
            // 
            // btnAddProp
            // 
            this.btnAddProp.Location = new System.Drawing.Point(208, 2);
            this.btnAddProp.Name = "btnAddProp";
            this.btnAddProp.Size = new System.Drawing.Size(45, 23);
            this.btnAddProp.TabIndex = 25;
            this.btnAddProp.Text = "Add";
            this.btnAddProp.UseVisualStyleBackColor = true;
            this.btnAddProp.Click += new System.EventHandler(this.btnAddProp_Click);
            // 
            // ProductProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPropertyGroupChoose);
            this.Controls.Add(this.conProductPropPanel);
            this.Controls.Add(this.btnDelProp);
            this.Controls.Add(this.btnModifyProp);
            this.Controls.Add(this.conProductProp);
            this.Controls.Add(this.btnAddProp);
            this.Name = "ProductProperty";
            this.Size = new System.Drawing.Size(697, 131);
            this.Load += new System.EventHandler(this.ProductProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPropertyGroupChoose;
        private System.Windows.Forms.Panel conProductPropPanel;
        private System.Windows.Forms.Button btnDelProp;
        private System.Windows.Forms.Button btnModifyProp;
        private System.Windows.Forms.ComboBox conProductProp;
        private System.Windows.Forms.Button btnAddProp;
    }
}
