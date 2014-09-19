namespace Jeelu.SimplusD.Client.Win
{
    partial class CSSGeneralContectControl
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
            this._labelTextAlign = new System.Windows.Forms.Label();
            this._comboBoxTextAlign = new System.Windows.Forms.ComboBox();
            this._textBoxHeight = new Jeelu.Win.ValidateTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._comboBoxOverFlow = new System.Windows.Forms.ComboBox();
            this._comboBoxClear = new System.Windows.Forms.ComboBox();
            this._comboBoxVerticalAlign = new System.Windows.Forms.ComboBox();
            this._label = new System.Windows.Forms.Label();
            this.comboBoxSizeUnit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _labelTextAlign
            // 
            this._labelTextAlign.AutoSize = true;
            this._labelTextAlign.Location = new System.Drawing.Point(3, 8);
            this._labelTextAlign.Name = "_labelTextAlign";
            this._labelTextAlign.Size = new System.Drawing.Size(65, 12);
            this._labelTextAlign.TabIndex = 0;
            this._labelTextAlign.Text = "水平布局：";
            // 
            // _comboBoxTextAlign
            // 
            this._comboBoxTextAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTextAlign.FormattingEnabled = true;
            this._comboBoxTextAlign.Items.AddRange(new object[] {
            "居中",
            "左对齐",
            "右对齐",
            "默认"});
            this._comboBoxTextAlign.Location = new System.Drawing.Point(74, 5);
            this._comboBoxTextAlign.Name = "_comboBoxTextAlign";
            this._comboBoxTextAlign.Size = new System.Drawing.Size(121, 20);
            this._comboBoxTextAlign.TabIndex = 10;
            this._comboBoxTextAlign.SelectedIndexChanged += new System.EventHandler(this._comboBoxTextAlign_SelectedIndexChanged);
            // 
            // _textBoxHeight
            // 
            this._textBoxHeight.Location = new System.Drawing.Point(74, 83);
            this._textBoxHeight.Name = "_textBoxHeight";
            this._textBoxHeight.RegexText = "^[0-9]*$";
            this._textBoxHeight.RegexTextRuntime = "^[0-9]*$";
            this._textBoxHeight.Size = new System.Drawing.Size(53, 21);
            this._textBoxHeight.TabIndex = 11;
            this._textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._textBoxHeight.TextChanged += new System.EventHandler(this.textBoxHeight_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "行高：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "溢出方式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "CLEAR：";
            // 
            // _comboBoxOverFlow
            // 
            this._comboBoxOverFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxOverFlow.FormattingEnabled = true;
            this._comboBoxOverFlow.Items.AddRange(new object[] {
            "自动",
            "可见",
            "隐藏",
            "滚动",
            "默认"});
            this._comboBoxOverFlow.Location = new System.Drawing.Point(74, 57);
            this._comboBoxOverFlow.Name = "_comboBoxOverFlow";
            this._comboBoxOverFlow.Size = new System.Drawing.Size(121, 20);
            this._comboBoxOverFlow.TabIndex = 15;
            this._comboBoxOverFlow.SelectedIndexChanged += new System.EventHandler(this._comboBoxOverFlow_SelectedIndexChanged);
            // 
            // _comboBoxClear
            // 
            this._comboBoxClear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxClear.FormattingEnabled = true;
            this._comboBoxClear.Items.AddRange(new object[] {
            "both",
            "left",
            "right",
            "none"});
            this._comboBoxClear.Location = new System.Drawing.Point(74, 110);
            this._comboBoxClear.Name = "_comboBoxClear";
            this._comboBoxClear.Size = new System.Drawing.Size(121, 20);
            this._comboBoxClear.TabIndex = 16;
            this._comboBoxClear.SelectedIndexChanged += new System.EventHandler(this.comboBoxClear_SelectedIndexChanged);
            // 
            // _comboBoxVerticalAlign
            // 
            this._comboBoxVerticalAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxVerticalAlign.FormattingEnabled = true;
            this._comboBoxVerticalAlign.Items.AddRange(new object[] {
            "middle",
            "top",
            "bottom",
            "text-bottom",
            "text-top",
            "sub",
            "super",
            "baseline",
            "default"});
            this._comboBoxVerticalAlign.Location = new System.Drawing.Point(74, 31);
            this._comboBoxVerticalAlign.Name = "_comboBoxVerticalAlign";
            this._comboBoxVerticalAlign.Size = new System.Drawing.Size(121, 20);
            this._comboBoxVerticalAlign.TabIndex = 18;
            this._comboBoxVerticalAlign.SelectedIndexChanged += new System.EventHandler(this._comboBoxVerticalAlign_SelectedIndexChanged);
            // 
            // _label
            // 
            this._label.AutoSize = true;
            this._label.Location = new System.Drawing.Point(3, 34);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(65, 12);
            this._label.TabIndex = 17;
            this._label.Text = "垂直布局：";
            // 
            // comboBoxSizeUnit
            // 
            this.comboBoxSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSizeUnit.FormattingEnabled = true;
            this.comboBoxSizeUnit.Location = new System.Drawing.Point(133, 83);
            this.comboBoxSizeUnit.Name = "comboBoxSizeUnit";
            this.comboBoxSizeUnit.Size = new System.Drawing.Size(62, 20);
            this.comboBoxSizeUnit.TabIndex = 19;
            // 
            // CSSGeneralContectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxSizeUnit);
            this.Controls.Add(this._comboBoxVerticalAlign);
            this.Controls.Add(this._label);
            this.Controls.Add(this._comboBoxClear);
            this.Controls.Add(this._comboBoxOverFlow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxHeight);
            this.Controls.Add(this._comboBoxTextAlign);
            this.Controls.Add(this._labelTextAlign);
            this.Name = "CSSGeneralContectControl";
            this.Size = new System.Drawing.Size(204, 137);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelTextAlign;
        private System.Windows.Forms.ComboBox _comboBoxTextAlign;
        private Jeelu.Win.ValidateTextBox _textBoxHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _comboBoxOverFlow;
        private System.Windows.Forms.ComboBox _comboBoxClear;
        private System.Windows.Forms.ComboBox _comboBoxVerticalAlign;
        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.ComboBox comboBoxSizeUnit;

    }
}
