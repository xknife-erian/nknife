namespace NKnife.Win.UpdaterFromGitHub.Example
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._updateButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this._getUpdateInfoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _updateButton
            // 
            this._updateButton.Location = new System.Drawing.Point(78, 190);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(170, 78);
            this._updateButton.TabIndex = 0;
            this._updateButton.Text = "更新本程序";
            this._updateButton.UseVisualStyleBackColor = true;
            this._updateButton.Click += new System.EventHandler(this._updateButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Location = new System.Drawing.Point(78, 366);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(170, 41);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "关闭";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _getUpdateInfoButton
            // 
            this._getUpdateInfoButton.Location = new System.Drawing.Point(78, 122);
            this._getUpdateInfoButton.Name = "_getUpdateInfoButton";
            this._getUpdateInfoButton.Size = new System.Drawing.Size(170, 62);
            this._getUpdateInfoButton.TabIndex = 3;
            this._getUpdateInfoButton.Text = "获取更新信息";
            this._getUpdateInfoButton.UseVisualStyleBackColor = true;
            this._getUpdateInfoButton.Click += new System.EventHandler(this._getUpdateInfoButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 429);
            this.Controls.Add(this._getUpdateInfoButton);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._updateButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExampleForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Button _getUpdateInfoButton;
    }
}

