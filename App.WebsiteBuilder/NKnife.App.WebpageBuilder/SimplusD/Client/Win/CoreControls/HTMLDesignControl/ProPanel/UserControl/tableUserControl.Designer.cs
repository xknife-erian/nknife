using System.Drawing;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class tableUserControl
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
            this.bgColorButton = new Jeelu.Win.ColorGeneralButton();
            this.borderColorButton = new Jeelu.Win.ColorGeneralButton();
            this.widthunitcomboBox = new System.Windows.Forms.ComboBox();
            this.classcomboBox = new System.Windows.Forms.ComboBox();
            this.aligncomboBox = new System.Windows.Forms.ComboBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.colLabel = new System.Windows.Forms.Label();
            this.rowLabel = new System.Windows.Forms.Label();
            this.rownumtextBox = new Jeelu.Win.ValidateTextBox();
            this.colnumtextBox = new Jeelu.Win.ValidateTextBox();
            this.widthtextBox = new Jeelu.Win.ValidateTextBox();
            this.spaceLabel = new System.Windows.Forms.Label();
            this.fillLabel = new System.Windows.Forms.Label();
            this.spacetextBox = new Jeelu.Win.ValidateTextBox();
            this.filltextBox = new Jeelu.Win.ValidateTextBox();
            this.boderLabel = new System.Windows.Forms.Label();
            this.alignLabel = new System.Windows.Forms.Label();
            this.useclassLabel = new System.Windows.Forms.Label();
            this.bordertextBox = new Jeelu.Win.ValidateTextBox();
            this.bgcolorLabel = new System.Windows.Forms.Label();
            this.bgpicLabel = new System.Windows.Forms.Label();
            this.bordercolorLabel = new System.Windows.Forms.Label();
            this.bgpicsrctextBox = new System.Windows.Forms.TextBox();
            this.bordercolortextBox = new System.Windows.Forms.TextBox();
            this.bgcolortextBox = new System.Windows.Forms.TextBox();
            this.browserbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bgColorButton
            // 
            this.bgColorButton.ColorArgbString = "";
            this.bgColorButton.Location = new System.Drawing.Point(155, 52);
            this.bgColorButton.MyColor = System.Drawing.Color.Empty;
            this.bgColorButton.Name = "bgColorButton";
            this.bgColorButton.Size = new System.Drawing.Size(21, 21);
            this.bgColorButton.TabIndex = 0;
            // 
            // borderColorButton
            // 
            this.borderColorButton.ColorArgbString = "";
            this.borderColorButton.Location = new System.Drawing.Point(155, 75);
            this.borderColorButton.MyColor = System.Drawing.Color.Empty;
            this.borderColorButton.Name = "borderColorButton";
            this.borderColorButton.Size = new System.Drawing.Size(21, 21);
            this.borderColorButton.TabIndex = 1;
            // 
            // widthunitcomboBox
            // 
            this.widthunitcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthunitcomboBox.FormattingEnabled = true;
            this.widthunitcomboBox.Location = new System.Drawing.Point(158, 26);
            this.widthunitcomboBox.Name = "widthunitcomboBox";
            this.widthunitcomboBox.Size = new System.Drawing.Size(54, 20);
            this.widthunitcomboBox.TabIndex = 19;
            // 
            // classcomboBox
            // 
            this.classcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classcomboBox.FormattingEnabled = true;
            this.classcomboBox.Location = new System.Drawing.Point(532, 4);
            this.classcomboBox.Name = "classcomboBox";
            this.classcomboBox.Size = new System.Drawing.Size(106, 20);
            this.classcomboBox.TabIndex = 17;
            this.classcomboBox.Visible = false;
            // 
            // aligncomboBox
            // 
            this.aligncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aligncomboBox.FormattingEnabled = true;
            this.aligncomboBox.Location = new System.Drawing.Point(383, 4);
            this.aligncomboBox.Name = "aligncomboBox";
            this.aligncomboBox.Size = new System.Drawing.Size(91, 20);
            this.aligncomboBox.TabIndex = 15;
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(84, 31);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(11, 12);
            this.widthLabel.TabIndex = 13;
            this.widthLabel.Text = "W";
            // 
            // colLabel
            // 
            this.colLabel.AutoSize = true;
            this.colLabel.Location = new System.Drawing.Point(28, 31);
            this.colLabel.Name = "colLabel";
            this.colLabel.Size = new System.Drawing.Size(11, 12);
            this.colLabel.TabIndex = 12;
            this.colLabel.Text = "C";
            // 
            // rowLabel
            // 
            this.rowLabel.AutoSize = true;
            this.rowLabel.Location = new System.Drawing.Point(28, 8);
            this.rowLabel.Name = "rowLabel";
            this.rowLabel.Size = new System.Drawing.Size(11, 12);
            this.rowLabel.TabIndex = 11;
            this.rowLabel.Text = "R";
            // 
            // rownumtextBox
            // 
            this.rownumtextBox.Location = new System.Drawing.Point(51, 3);
            this.rownumtextBox.Name = "rownumtextBox";
            this.rownumtextBox.RegexText = "^\\d*$";
            this.rownumtextBox.RegexTextRuntime = "^\\d*$";
            this.rownumtextBox.Size = new System.Drawing.Size(23, 21);
            this.rownumtextBox.TabIndex = 20;
            // 
            // colnumtextBox
            // 
            this.colnumtextBox.Location = new System.Drawing.Point(51, 26);
            this.colnumtextBox.Name = "colnumtextBox";
            this.colnumtextBox.RegexText = "^\\d*$";
            this.colnumtextBox.RegexTextRuntime = "^\\d*$";
            this.colnumtextBox.Size = new System.Drawing.Size(23, 21);
            this.colnumtextBox.TabIndex = 21;
            // 
            // widthtextBox
            // 
            this.widthtextBox.Location = new System.Drawing.Point(107, 26);
            this.widthtextBox.Name = "widthtextBox";
            this.widthtextBox.RegexText = "^\\d*$";
            this.widthtextBox.RegexTextRuntime = "^\\d*$";
            this.widthtextBox.Size = new System.Drawing.Size(44, 21);
            this.widthtextBox.TabIndex = 23;
            // 
            // spaceLabel
            // 
            this.spaceLabel.AutoSize = true;
            this.spaceLabel.Location = new System.Drawing.Point(243, 31);
            this.spaceLabel.Name = "spaceLabel";
            this.spaceLabel.Size = new System.Drawing.Size(35, 12);
            this.spaceLabel.TabIndex = 25;
            this.spaceLabel.Text = "space";
            // 
            // fillLabel
            // 
            this.fillLabel.AutoSize = true;
            this.fillLabel.Location = new System.Drawing.Point(243, 8);
            this.fillLabel.Name = "fillLabel";
            this.fillLabel.Size = new System.Drawing.Size(29, 12);
            this.fillLabel.TabIndex = 24;
            this.fillLabel.Text = "Fill";
            // 
            // spacetextBox
            // 
            this.spacetextBox.Location = new System.Drawing.Point(278, 26);
            this.spacetextBox.Name = "spacetextBox";
            this.spacetextBox.RegexText = "^\\d*$";
            this.spacetextBox.RegexTextRuntime = "^\\d*$";
            this.spacetextBox.Size = new System.Drawing.Size(43, 21);
            this.spacetextBox.TabIndex = 27;
            // 
            // filltextBox
            // 
            this.filltextBox.Location = new System.Drawing.Point(278, 3);
            this.filltextBox.Name = "filltextBox";
            this.filltextBox.RegexText = "^\\d*$";
            this.filltextBox.RegexTextRuntime = "^\\d*$";
            this.filltextBox.Size = new System.Drawing.Size(43, 21);
            this.filltextBox.TabIndex = 26;
            // 
            // boderLabel
            // 
            this.boderLabel.AutoSize = true;
            this.boderLabel.Location = new System.Drawing.Point(348, 32);
            this.boderLabel.Name = "boderLabel";
            this.boderLabel.Size = new System.Drawing.Size(41, 12);
            this.boderLabel.TabIndex = 29;
            this.boderLabel.Text = "border";
            // 
            // alignLabel
            // 
            this.alignLabel.AutoSize = true;
            this.alignLabel.Location = new System.Drawing.Point(348, 8);
            this.alignLabel.Name = "alignLabel";
            this.alignLabel.Size = new System.Drawing.Size(35, 12);
            this.alignLabel.TabIndex = 28;
            this.alignLabel.Text = "align";
            // 
            // useclassLabel
            // 
            this.useclassLabel.AutoSize = true;
            this.useclassLabel.Location = new System.Drawing.Point(509, 11);
            this.useclassLabel.Name = "useclassLabel";
            this.useclassLabel.Size = new System.Drawing.Size(0, 12);
            this.useclassLabel.TabIndex = 30;
            // 
            // bordertextBox
            // 
            this.bordertextBox.Location = new System.Drawing.Point(383, 26);
            this.bordertextBox.Name = "bordertextBox";
            this.bordertextBox.RegexText = "^\\d*$";
            this.bordertextBox.RegexTextRuntime = "^\\d*$";
            this.bordertextBox.Size = new System.Drawing.Size(50, 21);
            this.bordertextBox.TabIndex = 32;
            // 
            // bgcolorLabel
            // 
            this.bgcolorLabel.AutoSize = true;
            this.bgcolorLabel.Location = new System.Drawing.Point(68, 56);
            this.bgcolorLabel.Name = "bgcolorLabel";
            this.bgcolorLabel.Size = new System.Drawing.Size(23, 12);
            this.bgcolorLabel.TabIndex = 33;
            this.bgcolorLabel.Text = "bgc";
            // 
            // bgpicLabel
            // 
            this.bgpicLabel.AutoSize = true;
            this.bgpicLabel.Location = new System.Drawing.Point(259, 57);
            this.bgpicLabel.Name = "bgpicLabel";
            this.bgpicLabel.Size = new System.Drawing.Size(35, 12);
            this.bgpicLabel.TabIndex = 34;
            this.bgpicLabel.Text = "bgpic";
            // 
            // bordercolorLabel
            // 
            this.bordercolorLabel.AutoSize = true;
            this.bordercolorLabel.Location = new System.Drawing.Point(69, 76);
            this.bordercolorLabel.Name = "bordercolorLabel";
            this.bordercolorLabel.Size = new System.Drawing.Size(29, 12);
            this.bordercolorLabel.TabIndex = 35;
            this.bordercolorLabel.Text = "bodc";
            // 
            // bgpicsrctextBox
            // 
            this.bgpicsrctextBox.Location = new System.Drawing.Point(314, 52);
            this.bgpicsrctextBox.Name = "bgpicsrctextBox";
            this.bgpicsrctextBox.Size = new System.Drawing.Size(200, 21);
            this.bgpicsrctextBox.TabIndex = 38;
            // 
            // bordercolortextBox
            // 
            this.bordercolortextBox.Location = new System.Drawing.Point(184, 74);
            this.bordercolortextBox.Name = "bordercolortextBox";
            this.bordercolortextBox.Size = new System.Drawing.Size(69, 21);
            this.bordercolortextBox.TabIndex = 37;
            // 
            // bgcolortextBox
            // 
            this.bgcolortextBox.Location = new System.Drawing.Point(184, 52);
            this.bgcolortextBox.Name = "bgcolortextBox";
            this.bgcolortextBox.Size = new System.Drawing.Size(69, 21);
            this.bgcolortextBox.TabIndex = 36;
            // 
            // browserbutton
            // 
            this.browserbutton.Location = new System.Drawing.Point(540, 52);
            this.browserbutton.Name = "browserbutton";
            this.browserbutton.Size = new System.Drawing.Size(53, 23);
            this.browserbutton.TabIndex = 39;
            this.browserbutton.UseVisualStyleBackColor = true;
            // 
            // tableUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bgColorButton);
            this.Controls.Add(this.borderColorButton);
            this.Controls.Add(this.browserbutton);
            this.Controls.Add(this.bgpicsrctextBox);
            this.Controls.Add(this.bordercolortextBox);
            this.Controls.Add(this.bgcolortextBox);
            this.Controls.Add(this.bordercolorLabel);
            this.Controls.Add(this.bgpicLabel);
            this.Controls.Add(this.bgcolorLabel);
            this.Controls.Add(this.bordertextBox);
            this.Controls.Add(this.useclassLabel);
            this.Controls.Add(this.boderLabel);
            this.Controls.Add(this.alignLabel);
            this.Controls.Add(this.spacetextBox);
            this.Controls.Add(this.filltextBox);
            this.Controls.Add(this.spaceLabel);
            this.Controls.Add(this.fillLabel);
            this.Controls.Add(this.widthtextBox);
            this.Controls.Add(this.colnumtextBox);
            this.Controls.Add(this.rownumtextBox);
            this.Controls.Add(this.widthunitcomboBox);
            this.Controls.Add(this.classcomboBox);
            this.Controls.Add(this.aligncomboBox);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.colLabel);
            this.Controls.Add(this.rowLabel);
            this.Name = "tableUserControl";
            this.Size = new System.Drawing.Size(827, 114);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.tableUserControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox widthunitcomboBox;
        private System.Windows.Forms.ComboBox classcomboBox;
        private System.Windows.Forms.ComboBox aligncomboBox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label colLabel;
        private System.Windows.Forms.Label rowLabel;
        private ValidateTextBox rownumtextBox;
        private ValidateTextBox colnumtextBox;
        private ValidateTextBox widthtextBox;
        private System.Windows.Forms.Label spaceLabel;
        private System.Windows.Forms.Label fillLabel;
        private ValidateTextBox spacetextBox;
        private ValidateTextBox filltextBox;
        private System.Windows.Forms.Label boderLabel;
        private System.Windows.Forms.Label alignLabel;
        private System.Windows.Forms.Label useclassLabel;
        private ValidateTextBox bordertextBox;
        private System.Windows.Forms.Label bgcolorLabel;
        private System.Windows.Forms.Label bgpicLabel;
        private System.Windows.Forms.Label bordercolorLabel;
        private System.Windows.Forms.TextBox bgpicsrctextBox;
        private System.Windows.Forms.TextBox bordercolortextBox;
        private System.Windows.Forms.TextBox bgcolortextBox;
        private System.Windows.Forms.Button browserbutton;
        public ColorGeneralButton bgColorButton;
        public ColorGeneralButton borderColorButton;
    }
}
