using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class PartRectForm
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
            this.isRowRadioBtn = new System.Windows.Forms.RadioButton();
            this.isColumnRadioBtn = new System.Windows.Forms.RadioButton();
            this.partNumText = new Jeelu.Win.ValidateTextBox();
            this.labelNum = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // isRowRadioBtn
            // 
            this.isRowRadioBtn.AutoSize = true;
            this.isRowRadioBtn.Checked = true;
            this.isRowRadioBtn.Location = new System.Drawing.Point(37, 23);
            this.isRowRadioBtn.Name = "isRowRadioBtn";
            this.isRowRadioBtn.Size = new System.Drawing.Size(73, 17);
            this.isRowRadioBtn.TabIndex = 2;
            this.isRowRadioBtn.TabStop = true;
            this.isRowRadioBtn.Text = "横向分割";
            this.isRowRadioBtn.UseVisualStyleBackColor = true;
            // 
            // isColumnRadioBtn
            // 
            this.isColumnRadioBtn.AutoSize = true;
            this.isColumnRadioBtn.Location = new System.Drawing.Point(119, 23);
            this.isColumnRadioBtn.Name = "isColumnRadioBtn";
            this.isColumnRadioBtn.Size = new System.Drawing.Size(73, 17);
            this.isColumnRadioBtn.TabIndex = 3;
            this.isColumnRadioBtn.Text = "纵向分割";
            this.isColumnRadioBtn.UseVisualStyleBackColor = true;
            // 
            // partNumText
            // 
            this.partNumText.Location = new System.Drawing.Point(119, 57);
            this.partNumText.Name = "partNumText";
            this.partNumText.RegexText = "^[0-9]*$";
            this.partNumText.RegexTextRuntime = "^[0-9]*$";
            this.partNumText.Size = new System.Drawing.Size(71, 21);
            this.partNumText.TabIndex = 4;
            this.partNumText.TabStop = false;
            // 
            // labelNum
            // 
            this.labelNum.AutoSize = true;
            this.labelNum.Location = new System.Drawing.Point(37, 61);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(71, 13);
            this.labelNum.TabIndex = 3;
            this.labelNum.Text = "分成矩形数:";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(37, 100);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(71, 25);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(119, 100);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(71, 25);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // PartRectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 138);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.partNumText);
            this.Controls.Add(this.isColumnRadioBtn);
            this.Controls.Add(this.isRowRadioBtn);
            this.Name = "PartRectForm";
            this.Text = "分割矩形";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton isRowRadioBtn;
        private System.Windows.Forms.RadioButton isColumnRadioBtn;
        private ValidateTextBox partNumText;
        private System.Windows.Forms.Label labelNum;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}

