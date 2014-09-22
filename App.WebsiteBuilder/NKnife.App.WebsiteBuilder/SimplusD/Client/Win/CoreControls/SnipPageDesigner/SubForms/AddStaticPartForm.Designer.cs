using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class AddStaticPartForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBox = new System.Windows.Forms.Control();
            this.designerBox = new System.Windows.Forms.Control();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(365, 331);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 28);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(450, 331);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 28);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "取消";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnBox
            // 
            this.btnBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBox.Location = new System.Drawing.Point(2, 325);
            this.btnBox.Name = "btnBox";
            this.btnBox.Size = new System.Drawing.Size(540, 42);
            this.btnBox.TabIndex = 0;
            // 
            // designerBox
            // 
            this.designerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designerBox.Location = new System.Drawing.Point(2, 0);
            this.designerBox.Name = "designerBox";
            this.designerBox.Size = new System.Drawing.Size(540, 325);
            this.designerBox.TabIndex = 1;
            // 
            // AddStaticPartForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(544, 367);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.designerBox);
            this.Controls.Add(this.btnBox);
            this.Name = "AddStaticPartForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Control btnBox;
        private System.Windows.Forms.Control designerBox;
    }
}