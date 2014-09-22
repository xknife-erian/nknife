using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class lineUserControl
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
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.alignLabel = new System.Windows.Forms.Label();
            this.useclassLabel = new System.Windows.Forms.Label();
            this.widthUnitcomboBox = new System.Windows.Forms.ComboBox();
            this.AligncomboBox = new System.Windows.Forms.ComboBox();
            this.useclasscomboBox = new System.Windows.Forms.ComboBox();
            this.heighttextBox = new Jeelu.Win.ValidateTextBox();
            this.widthtextBox = new Jeelu.Win.ValidateTextBox();
            this.shadcheckBox = new System.Windows.Forms.CheckBox();
            this.heightUnitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(29, 7);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 12);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(29, 30);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 12);
            this.heightLabel.TabIndex = 1;
            this.heightLabel.Text = "Height";
            // 
            // alignLabel
            // 
            this.alignLabel.AutoSize = true;
            this.alignLabel.Location = new System.Drawing.Point(239, 7);
            this.alignLabel.Name = "alignLabel";
            this.alignLabel.Size = new System.Drawing.Size(35, 12);
            this.alignLabel.TabIndex = 2;
            this.alignLabel.Text = "Align";
            // 
            // useclassLabel
            // 
            this.useclassLabel.AutoSize = true;
            this.useclassLabel.Location = new System.Drawing.Point(414, 7);
            this.useclassLabel.Name = "useclassLabel";
            this.useclassLabel.Size = new System.Drawing.Size(35, 12);
            this.useclassLabel.TabIndex = 3;
            this.useclassLabel.Text = "Class";
            this.useclassLabel.Visible = false;
            // 
            // widthUnitcomboBox
            // 
            this.widthUnitcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthUnitcomboBox.FormattingEnabled = true;
            this.widthUnitcomboBox.Location = new System.Drawing.Point(128, 4);
            this.widthUnitcomboBox.Name = "widthUnitcomboBox";
            this.widthUnitcomboBox.Size = new System.Drawing.Size(70, 20);
            this.widthUnitcomboBox.TabIndex = 4;
            // 
            // AligncomboBox
            // 
            this.AligncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AligncomboBox.FormattingEnabled = true;
            this.AligncomboBox.Location = new System.Drawing.Point(280, 4);
            this.AligncomboBox.Name = "AligncomboBox";
            this.AligncomboBox.Size = new System.Drawing.Size(121, 20);
            this.AligncomboBox.TabIndex = 5;
            // 
            // useclasscomboBox
            // 
            this.useclasscomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.useclasscomboBox.FormattingEnabled = true;
            this.useclasscomboBox.Location = new System.Drawing.Point(455, 4);
            this.useclasscomboBox.Name = "useclasscomboBox";
            this.useclasscomboBox.Size = new System.Drawing.Size(121, 20);
            this.useclasscomboBox.TabIndex = 6;
            this.useclasscomboBox.Visible = false;
            // 
            // heighttextBox
            // 
            this.heighttextBox.Location = new System.Drawing.Point(62, 27);
            this.heighttextBox.Name = "heighttextBox";
            this.heighttextBox.RegexText = "^\\d*$";
            this.heighttextBox.RegexTextRuntime = "^\\d*$";
            this.heighttextBox.Size = new System.Drawing.Size(60, 21);
            this.heighttextBox.TabIndex = 7;
            // 
            // widthtextBox
            // 
            this.widthtextBox.Location = new System.Drawing.Point(62, 4);
            this.widthtextBox.Name = "widthtextBox";
            this.widthtextBox.RegexText = "^\\d*$";
            this.widthtextBox.RegexTextRuntime = "^\\d*$";
            this.widthtextBox.Size = new System.Drawing.Size(60, 21);
            this.widthtextBox.TabIndex = 8;
            // 
            // shadcheckBox
            // 
            this.shadcheckBox.AutoSize = true;
            this.shadcheckBox.Location = new System.Drawing.Point(280, 29);
            this.shadcheckBox.Name = "shadcheckBox";
            this.shadcheckBox.Size = new System.Drawing.Size(54, 16);
            this.shadcheckBox.TabIndex = 9;
            this.shadcheckBox.Text = "Shade";
            this.shadcheckBox.UseVisualStyleBackColor = true;
            // 
            // heightUnitLabel
            // 
            this.heightUnitLabel.AutoSize = true;
            this.heightUnitLabel.Location = new System.Drawing.Point(128, 32);
            this.heightUnitLabel.Name = "heightUnitLabel";
            this.heightUnitLabel.Size = new System.Drawing.Size(0, 12);
            this.heightUnitLabel.TabIndex = 10;
            // 
            // lineUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.heightUnitLabel);
            this.Controls.Add(this.shadcheckBox);
            this.Controls.Add(this.widthtextBox);
            this.Controls.Add(this.heighttextBox);
            this.Controls.Add(this.useclasscomboBox);
            this.Controls.Add(this.AligncomboBox);
            this.Controls.Add(this.widthUnitcomboBox);
            this.Controls.Add(this.useclassLabel);
            this.Controls.Add(this.alignLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Name = "lineUserControl";
            this.Size = new System.Drawing.Size(827, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.lineUserControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label alignLabel;
        private System.Windows.Forms.Label useclassLabel;
        private System.Windows.Forms.ComboBox widthUnitcomboBox;
        private System.Windows.Forms.ComboBox AligncomboBox;
        private System.Windows.Forms.ComboBox useclasscomboBox;
        private ValidateTextBox heighttextBox;
        private ValidateTextBox widthtextBox;
        private System.Windows.Forms.CheckBox shadcheckBox;
        private System.Windows.Forms.Label heightUnitLabel;
    }
}
