namespace Jeelu.SimplusOM.Client
{
    partial class AreaControl
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
            this.area3ComboBox = new System.Windows.Forms.ComboBox();
            this.area1ComboBox = new System.Windows.Forms.ComboBox();
            this.area2ComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // area3ComboBox
            // 
            this.area3ComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.area3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.area3ComboBox.FormattingEnabled = true;
            this.area3ComboBox.Location = new System.Drawing.Point(200, 0);
            this.area3ComboBox.Name = "area3ComboBox";
            this.area3ComboBox.Size = new System.Drawing.Size(100, 20);
            this.area3ComboBox.TabIndex = 12;
            this.area3ComboBox.SelectedIndexChanged += new System.EventHandler(this.AreaComBox_SelectedIndexChanged);
            // 
            // area1ComboBox
            // 
            this.area1ComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.area1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.area1ComboBox.FormattingEnabled = true;
            this.area1ComboBox.Location = new System.Drawing.Point(0, 0);
            this.area1ComboBox.Name = "area1ComboBox";
            this.area1ComboBox.Size = new System.Drawing.Size(100, 20);
            this.area1ComboBox.TabIndex = 11;
            this.area1ComboBox.SelectedIndexChanged += new System.EventHandler(this.AreaComBox_SelectedIndexChanged);
            // 
            // area2ComboBox
            // 
            this.area2ComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.area2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.area2ComboBox.FormattingEnabled = true;
            this.area2ComboBox.Location = new System.Drawing.Point(100, 0);
            this.area2ComboBox.Name = "area2ComboBox";
            this.area2ComboBox.Size = new System.Drawing.Size(100, 20);
            this.area2ComboBox.TabIndex = 10;
            this.area2ComboBox.SelectedIndexChanged += new System.EventHandler(this.AreaComBox_SelectedIndexChanged);
            // 
            // AreaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.area3ComboBox);
            this.Controls.Add(this.area2ComboBox);
            this.Controls.Add(this.area1ComboBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AreaControl";
            this.Size = new System.Drawing.Size(301, 21);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox area3ComboBox;
        public System.Windows.Forms.ComboBox area1ComboBox;
        public System.Windows.Forms.ComboBox area2ComboBox;

    }
}
