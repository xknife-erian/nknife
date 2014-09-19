namespace Jeelu.Win
{
    partial class ExpandCssControl
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
            this.lblPagination = new System.Windows.Forms.Label();
            this.lblForestall = new System.Windows.Forms.Label();
            this.lblSith = new System.Windows.Forms.Label();
            this.lblSeeing = new System.Windows.Forms.Label();
            this.lblCursor = new System.Windows.Forms.Label();
            this.lblColander = new System.Windows.Forms.Label();
            this.conForestall = new System.Windows.Forms.ComboBox();
            this.conSith = new System.Windows.Forms.ComboBox();
            this.conCursor = new System.Windows.Forms.ComboBox();
            this.conColander = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblPagination
            // 
            this.lblPagination.AutoSize = true;
            this.lblPagination.Location = new System.Drawing.Point(106, 16);
            this.lblPagination.Name = "lblPagination";
            this.lblPagination.Size = new System.Drawing.Size(29, 12);
            this.lblPagination.TabIndex = 0;
            this.lblPagination.Text = "分页";
            // 
            // lblForestall
            // 
            this.lblForestall.AutoSize = true;
            this.lblForestall.Location = new System.Drawing.Point(47, 37);
            this.lblForestall.Name = "lblForestall";
            this.lblForestall.Size = new System.Drawing.Size(53, 12);
            this.lblForestall.TabIndex = 1;
            this.lblForestall.Text = "之前(&B):";
            // 
            // lblSith
            // 
            this.lblSith.AutoSize = true;
            this.lblSith.Location = new System.Drawing.Point(47, 64);
            this.lblSith.Name = "lblSith";
            this.lblSith.Size = new System.Drawing.Size(53, 12);
            this.lblSith.TabIndex = 2;
            this.lblSith.Text = "之后(&T):";
            // 
            // lblSeeing
            // 
            this.lblSeeing.AutoSize = true;
            this.lblSeeing.Location = new System.Drawing.Point(106, 94);
            this.lblSeeing.Name = "lblSeeing";
            this.lblSeeing.Size = new System.Drawing.Size(53, 12);
            this.lblSeeing.TabIndex = 3;
            this.lblSeeing.Text = "视觉效果";
            // 
            // lblCursor
            // 
            this.lblCursor.AutoSize = true;
            this.lblCursor.Location = new System.Drawing.Point(47, 115);
            this.lblCursor.Name = "lblCursor";
            this.lblCursor.Size = new System.Drawing.Size(53, 12);
            this.lblCursor.TabIndex = 4;
            this.lblCursor.Text = "光标(&C):";
            // 
            // lblColander
            // 
            this.lblColander.AutoSize = true;
            this.lblColander.Location = new System.Drawing.Point(47, 142);
            this.lblColander.Name = "lblColander";
            this.lblColander.Size = new System.Drawing.Size(53, 12);
            this.lblColander.TabIndex = 5;
            this.lblColander.Text = "滤镜(&F):";
            // 
            // conForestall
            // 
            this.conForestall.FormattingEnabled = true;
            this.conForestall.Location = new System.Drawing.Point(103, 34);
            this.conForestall.Name = "conForestall";
            this.conForestall.Size = new System.Drawing.Size(121, 20);
            this.conForestall.TabIndex = 6;
            // 
            // conSith
            // 
            this.conSith.FormattingEnabled = true;
            this.conSith.Location = new System.Drawing.Point(103, 60);
            this.conSith.Name = "conSith";
            this.conSith.Size = new System.Drawing.Size(121, 20);
            this.conSith.TabIndex = 7;
            // 
            // conCursor
            // 
            this.conCursor.FormattingEnabled = true;
            this.conCursor.Location = new System.Drawing.Point(103, 112);
            this.conCursor.Name = "conCursor";
            this.conCursor.Size = new System.Drawing.Size(143, 20);
            this.conCursor.TabIndex = 8;
            // 
            // conColander
            // 
            this.conColander.FormattingEnabled = true;
            this.conColander.Location = new System.Drawing.Point(103, 138);
            this.conColander.Name = "conColander";
            this.conColander.Size = new System.Drawing.Size(255, 20);
            this.conColander.TabIndex = 9;
            // 
            // ExpandCssControl
            // 
            this.Controls.Add(this.conColander);
            this.Controls.Add(this.conCursor);
            this.Controls.Add(this.conSith);
            this.Controls.Add(this.conForestall);
            this.Controls.Add(this.lblColander);
            this.Controls.Add(this.lblCursor);
            this.Controls.Add(this.lblSeeing);
            this.Controls.Add(this.lblSith);
            this.Controls.Add(this.lblForestall);
            this.Controls.Add(this.lblPagination);
            this.Name = "ExpandCssControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPagination;
        private System.Windows.Forms.Label lblForestall;
        private System.Windows.Forms.Label lblSith;
        private System.Windows.Forms.Label lblSeeing;
        private System.Windows.Forms.Label lblCursor;
        private System.Windows.Forms.Label lblColander;
        private System.Windows.Forms.ComboBox conForestall;
        private System.Windows.Forms.ComboBox conSith;
        private System.Windows.Forms.ComboBox conCursor;
        private System.Windows.Forms.ComboBox conColander;
    }
}
