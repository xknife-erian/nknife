namespace Jeelu.SimplusD.Client.Win
{
    partial class CSSGeneralLayoutControl
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
            this.label4 = new System.Windows.Forms.Label();
            this._comboBoxlayoutFloat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._textBoxWidth = new Jeelu.Win.ValidateTextBox();
            this._textBoxHeight = new Jeelu.Win.ValidateTextBox();
            this.comboBoxWidthSizeUnit = new System.Windows.Forms.ComboBox();
            this.comboBoxHeightSizeUnit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "宽度：";
            // 
            // _comboBoxlayoutFloat
            // 
            this._comboBoxlayoutFloat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxlayoutFloat.FormattingEnabled = true;
            this._comboBoxlayoutFloat.Items.AddRange(new object[] {
            "无浮动",
            "左浮动",
            "右浮动"});
            this._comboBoxlayoutFloat.Location = new System.Drawing.Point(74, 6);
            this._comboBoxlayoutFloat.Name = "_comboBoxlayoutFloat";
            this._comboBoxlayoutFloat.Size = new System.Drawing.Size(121, 20);
            this._comboBoxlayoutFloat.TabIndex = 9;
            this._comboBoxlayoutFloat.SelectedIndexChanged += new System.EventHandler(this._comboBoxlayoutFloat_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "浮动方式：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "高度：";
            // 
            // _textBoxWidth
            // 
            this._textBoxWidth.BackColor = System.Drawing.Color.White;
            this._textBoxWidth.Location = new System.Drawing.Point(74, 34);
            this._textBoxWidth.Name = "_textBoxWidth";
            this._textBoxWidth.RegexText = "^[0-9]*$";
            this._textBoxWidth.RegexTextRuntime = "^[0-9]*$";
            this._textBoxWidth.Size = new System.Drawing.Size(44, 21);
            this._textBoxWidth.TabIndex = 16;
            this._textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._textBoxWidth.TextChanged += new System.EventHandler(this._textBoxWidth_TextChanged);
            // 
            // _textBoxHeight
            // 
            this._textBoxHeight.BackColor = System.Drawing.Color.White;
            this._textBoxHeight.Location = new System.Drawing.Point(74, 62);
            this._textBoxHeight.Name = "_textBoxHeight";
            this._textBoxHeight.RegexText = "^[0-9]*?$";
            this._textBoxHeight.RegexTextRuntime = "^[0-9]*?$";
            this._textBoxHeight.Size = new System.Drawing.Size(44, 21);
            this._textBoxHeight.TabIndex = 17;
            this._textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._textBoxHeight.TextChanged += new System.EventHandler(this._textBoxHeight_TextChanged);
            // 
            // comboBoxWidthSizeUnit
            // 
            this.comboBoxWidthSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWidthSizeUnit.FormattingEnabled = true;
            this.comboBoxWidthSizeUnit.Location = new System.Drawing.Point(124, 35);
            this.comboBoxWidthSizeUnit.Name = "comboBoxWidthSizeUnit";
            this.comboBoxWidthSizeUnit.Size = new System.Drawing.Size(71, 20);
            this.comboBoxWidthSizeUnit.TabIndex = 20;
            this.comboBoxWidthSizeUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxWidthSizeUnit_SelectedIndexChanged);
            // 
            // comboBoxHeightSizeUnit
            // 
            this.comboBoxHeightSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHeightSizeUnit.FormattingEnabled = true;
            this.comboBoxHeightSizeUnit.Location = new System.Drawing.Point(124, 61);
            this.comboBoxHeightSizeUnit.Name = "comboBoxHeightSizeUnit";
            this.comboBoxHeightSizeUnit.Size = new System.Drawing.Size(70, 20);
            this.comboBoxHeightSizeUnit.TabIndex = 21;
            this.comboBoxHeightSizeUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxHeightSizeUnit_SelectedIndexChanged);
            // 
            // CSSGeneralLayoutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxHeightSizeUnit);
            this.Controls.Add(this.comboBoxWidthSizeUnit);
            this.Controls.Add(this._textBoxHeight);
            this.Controls.Add(this._textBoxWidth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._comboBoxlayoutFloat);
            this.Controls.Add(this.label1);
            this.Name = "CSSGeneralLayoutControl";
            this.Size = new System.Drawing.Size(208, 90);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _comboBoxlayoutFloat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private Jeelu.Win.ValidateTextBox _textBoxWidth;
        private Jeelu.Win.ValidateTextBox _textBoxHeight;
        private System.Windows.Forms.ComboBox comboBoxWidthSizeUnit;
        private System.Windows.Forms.ComboBox comboBoxHeightSizeUnit;
    }
}
