namespace Jeelu.SimplusD.Client.Win
{
    partial class CssMyFont
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
            this.labelFont = new System.Windows.Forms.Label();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.btnBold = new System.Windows.Forms.Button();
            this.btnI = new System.Windows.Forms.Button();
            this.labelSize = new System.Windows.Forms.Label();
            this.comboBoxSize = new System.Windows.Forms.ComboBox();
            this.comboBoxSizeUint = new System.Windows.Forms.ComboBox();
            this.btnColor = new Jeelu.Win.ColorGeneralButton();
            this.textBoxColor = new Jeelu.Win.ValidateTextBox();
            this.SuspendLayout();
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFont.Location = new System.Drawing.Point(6, 12);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(29, 12);
            this.labelFont.TabIndex = 0;
            this.labelFont.Text = "字体";
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(38, 9);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(133, 20);
            this.comboBoxFont.TabIndex = 1;
            // 
            // btnBold
            // 
            this.btnBold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBold.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBold.Location = new System.Drawing.Point(175, 7);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(27, 23);
            this.btnBold.TabIndex = 2;
            this.btnBold.Text = "B";
            this.btnBold.UseVisualStyleBackColor = true;
            // 
            // btnI
            // 
            this.btnI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnI.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnI.Location = new System.Drawing.Point(207, 7);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(27, 23);
            this.btnI.TabIndex = 3;
            this.btnI.Text = "I";
            this.btnI.UseVisualStyleBackColor = true;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSize.Location = new System.Drawing.Point(6, 41);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(29, 12);
            this.labelSize.TabIndex = 4;
            this.labelSize.Text = "大小";
            // 
            // comboBoxSize
            // 
            this.comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSize.FormattingEnabled = true;
            this.comboBoxSize.Location = new System.Drawing.Point(38, 38);
            this.comboBoxSize.Name = "comboBoxSize";
            this.comboBoxSize.Size = new System.Drawing.Size(49, 20);
            this.comboBoxSize.TabIndex = 5;
            // 
            // comboBoxSizeUint
            // 
            this.comboBoxSizeUint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSizeUint.Enabled = false;
            this.comboBoxSizeUint.FormattingEnabled = true;
            this.comboBoxSizeUint.Location = new System.Drawing.Point(91, 38);
            this.comboBoxSizeUint.Name = "comboBoxSizeUint";
            this.comboBoxSizeUint.Size = new System.Drawing.Size(79, 20);
            this.comboBoxSizeUint.TabIndex = 6;
            // 
            // btnColor
            // 
            this.btnColor.ColorArgbString = "#000000";
            this.btnColor.Location = new System.Drawing.Point(174, 36);
            this.btnColor.MyColor = System.Drawing.Color.Black;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 23);
            this.btnColor.TabIndex = 9;
            // 
            // textBoxColor
            // 
            this.textBoxColor.Location = new System.Drawing.Point(200, 38);
            this.textBoxColor.Name = "textBoxColor";
            this.textBoxColor.RegexText = "";
            this.textBoxColor.RegexTextRuntime = "";
            this.textBoxColor.Size = new System.Drawing.Size(46, 21);
            this.textBoxColor.TabIndex = 8;
            // 
            // CssMyFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxColor);
            this.Controls.Add(this.comboBoxSizeUint);
            this.Controls.Add(this.comboBoxSize);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.btnI);
            this.Controls.Add(this.btnBold);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.btnColor);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "CssMyFont";
            this.Size = new System.Drawing.Size(259, 71);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.Button btnBold;
        private System.Windows.Forms.Button btnI;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.ComboBox comboBoxSize;
        private System.Windows.Forms.ComboBox comboBoxSizeUint;
        private Jeelu.Win.ColorGeneralButton btnColor;
        private Jeelu.Win.ValidateTextBox textBoxColor;
    }
}
