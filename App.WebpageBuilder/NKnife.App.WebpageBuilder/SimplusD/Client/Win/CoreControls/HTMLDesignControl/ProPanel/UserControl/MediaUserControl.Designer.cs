using System.Drawing;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class MediaUserControl
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
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.pathtextBox = new System.Windows.Forms.TextBox();
            this.pathLabel = new System.Windows.Forms.Label();
            this.useclasscomboBox = new System.Windows.Forms.ComboBox();
            this.useclassLabel = new System.Windows.Forms.Label();
            this.loopcheckBox = new System.Windows.Forms.CheckBox();
            this.autoplaycheckBox = new System.Windows.Forms.CheckBox();
            this.qualitycomboBox = new System.Windows.Forms.ComboBox();
            this.scalecomboBox = new System.Windows.Forms.ComboBox();
            this.aligncomboBox = new System.Windows.Forms.ComboBox();
            this.qualitylabel = new System.Windows.Forms.Label();
            this.scalelabel = new System.Windows.Forms.Label();
            this.alignlabel = new System.Windows.Forms.Label();
            this.vspacelabel = new System.Windows.Forms.Label();
            this.hspacelabel = new System.Windows.Forms.Label();
            this.vspacetextBox = new Jeelu.Win.ValidateTextBox();
            this.hspacetextBox = new Jeelu.Win.ValidateTextBox();
            this.bglabel = new System.Windows.Forms.Label();
            this.bgtextBox = new System.Windows.Forms.TextBox();
            this.flashHeigUintComboBox = new System.Windows.Forms.ComboBox();
            this.flashWidUintComboBox = new System.Windows.Forms.ComboBox();
            this.widthTextBox = new Jeelu.Win.ValidateTextBox();
            this.heightTextBox = new Jeelu.Win.ValidateTextBox();
            this.bgColorButton = new Jeelu.Win.ColorGeneralButton();
            this.SuspendLayout();
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(37, 30);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(41, 12);
            this.heightLabel.TabIndex = 10;
            this.heightLabel.Text = "height";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(37, 8);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 12);
            this.widthLabel.TabIndex = 9;
            this.widthLabel.Text = "width";
            // 
            // pathtextBox
            // 
            this.pathtextBox.Location = new System.Drawing.Point(254, 5);
            this.pathtextBox.Name = "pathtextBox";
            this.pathtextBox.Size = new System.Drawing.Size(292, 21);
            this.pathtextBox.TabIndex = 14;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(219, 8);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(29, 12);
            this.pathLabel.TabIndex = 13;
            this.pathLabel.Text = "path";
            // 
            // useclasscomboBox
            // 
            this.useclasscomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.useclasscomboBox.FormattingEnabled = true;
            this.useclasscomboBox.Location = new System.Drawing.Point(633, 5);
            this.useclasscomboBox.Name = "useclasscomboBox";
            this.useclasscomboBox.Size = new System.Drawing.Size(121, 20);
            this.useclasscomboBox.TabIndex = 16;
            this.useclasscomboBox.Visible = false;
            // 
            // useclassLabel
            // 
            this.useclassLabel.AutoSize = true;
            this.useclassLabel.Location = new System.Drawing.Point(574, 8);
            this.useclassLabel.Name = "useclassLabel";
            this.useclassLabel.Size = new System.Drawing.Size(53, 12);
            this.useclassLabel.TabIndex = 15;
            this.useclassLabel.Text = "useclass";
            this.useclassLabel.Visible = false;
            // 
            // loopcheckBox
            // 
            this.loopcheckBox.AutoSize = true;
            this.loopcheckBox.Location = new System.Drawing.Point(49, 58);
            this.loopcheckBox.Name = "loopcheckBox";
            this.loopcheckBox.Size = new System.Drawing.Size(48, 16);
            this.loopcheckBox.TabIndex = 17;
            this.loopcheckBox.Text = "loop";
            this.loopcheckBox.UseVisualStyleBackColor = true;
            // 
            // autoplaycheckBox
            // 
            this.autoplaycheckBox.AutoSize = true;
            this.autoplaycheckBox.Location = new System.Drawing.Point(49, 75);
            this.autoplaycheckBox.Name = "autoplaycheckBox";
            this.autoplaycheckBox.Size = new System.Drawing.Size(72, 16);
            this.autoplaycheckBox.TabIndex = 18;
            this.autoplaycheckBox.Text = "autoplay";
            this.autoplaycheckBox.UseVisualStyleBackColor = true;
            // 
            // qualitycomboBox
            // 
            this.qualitycomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qualitycomboBox.FormattingEnabled = true;
            this.qualitycomboBox.Location = new System.Drawing.Point(410, 55);
            this.qualitycomboBox.Name = "qualitycomboBox";
            this.qualitycomboBox.Size = new System.Drawing.Size(121, 20);
            this.qualitycomboBox.TabIndex = 20;
            // 
            // scalecomboBox
            // 
            this.scalecomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scalecomboBox.FormattingEnabled = true;
            this.scalecomboBox.Location = new System.Drawing.Point(410, 76);
            this.scalecomboBox.Name = "scalecomboBox";
            this.scalecomboBox.Size = new System.Drawing.Size(121, 20);
            this.scalecomboBox.TabIndex = 22;
            // 
            // aligncomboBox
            // 
            this.aligncomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aligncomboBox.FormattingEnabled = true;
            this.aligncomboBox.Location = new System.Drawing.Point(629, 54);
            this.aligncomboBox.Name = "aligncomboBox";
            this.aligncomboBox.Size = new System.Drawing.Size(125, 20);
            this.aligncomboBox.TabIndex = 24;
            // 
            // qualitylabel
            // 
            this.qualitylabel.AutoSize = true;
            this.qualitylabel.Location = new System.Drawing.Point(351, 57);
            this.qualitylabel.Name = "qualitylabel";
            this.qualitylabel.Size = new System.Drawing.Size(47, 12);
            this.qualitylabel.TabIndex = 25;
            this.qualitylabel.Text = "quality";
            // 
            // scalelabel
            // 
            this.scalelabel.AutoSize = true;
            this.scalelabel.Location = new System.Drawing.Point(351, 79);
            this.scalelabel.Name = "scalelabel";
            this.scalelabel.Size = new System.Drawing.Size(35, 12);
            this.scalelabel.TabIndex = 26;
            this.scalelabel.Text = "scale";
            // 
            // alignlabel
            // 
            this.alignlabel.AutoSize = true;
            this.alignlabel.Location = new System.Drawing.Point(586, 56);
            this.alignlabel.Name = "alignlabel";
            this.alignlabel.Size = new System.Drawing.Size(35, 12);
            this.alignlabel.TabIndex = 27;
            this.alignlabel.Text = "align";
            // 
            // vspacelabel
            // 
            this.vspacelabel.AutoSize = true;
            this.vspacelabel.Location = new System.Drawing.Point(188, 56);
            this.vspacelabel.Name = "vspacelabel";
            this.vspacelabel.Size = new System.Drawing.Size(41, 12);
            this.vspacelabel.TabIndex = 28;
            this.vspacelabel.Text = "vspace";
            // 
            // hspacelabel
            // 
            this.hspacelabel.AutoSize = true;
            this.hspacelabel.Location = new System.Drawing.Point(188, 78);
            this.hspacelabel.Name = "hspacelabel";
            this.hspacelabel.Size = new System.Drawing.Size(41, 12);
            this.hspacelabel.TabIndex = 29;
            this.hspacelabel.Text = "hspace";
            // 
            // vspacetextBox
            // 
            this.vspacetextBox.Location = new System.Drawing.Point(239, 54);
            this.vspacetextBox.Name = "vspacetextBox";
            this.vspacetextBox.RegexText = "^\\d*$";
            this.vspacetextBox.RegexTextRuntime = "^\\d*$";
            this.vspacetextBox.Size = new System.Drawing.Size(60, 21);
            this.vspacetextBox.TabIndex = 31;
            // 
            // hspacetextBox
            // 
            this.hspacetextBox.Location = new System.Drawing.Point(239, 76);
            this.hspacetextBox.Name = "hspacetextBox";
            this.hspacetextBox.RegexText = "^\\d*$";
            this.hspacetextBox.RegexTextRuntime = "^\\d*$";
            this.hspacetextBox.Size = new System.Drawing.Size(60, 21);
            this.hspacetextBox.TabIndex = 30;
            // 
            // bglabel
            // 
            this.bglabel.AutoSize = true;
            this.bglabel.Location = new System.Drawing.Point(586, 78);
            this.bglabel.Name = "bglabel";
            this.bglabel.Size = new System.Drawing.Size(17, 12);
            this.bglabel.TabIndex = 32;
            this.bglabel.Text = "bg";
            this.bglabel.Visible = false;
            // 
            // bgtextBox
            // 
            this.bgtextBox.Location = new System.Drawing.Point(666, 75);
            this.bgtextBox.Name = "bgtextBox";
            this.bgtextBox.Size = new System.Drawing.Size(88, 21);
            this.bgtextBox.TabIndex = 33;
            this.bgtextBox.Visible = false;
            // 
            // flashHeigUintComboBox
            // 
            this.flashHeigUintComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flashHeigUintComboBox.Location = new System.Drawing.Point(144, 26);
            this.flashHeigUintComboBox.Name = "flashHeigUintComboBox";
            this.flashHeigUintComboBox.Size = new System.Drawing.Size(54, 20);
            this.flashHeigUintComboBox.TabIndex = 35;
            // 
            // flashWidUintComboBox
            // 
            this.flashWidUintComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flashWidUintComboBox.Location = new System.Drawing.Point(144, 4);
            this.flashWidUintComboBox.Name = "flashWidUintComboBox";
            this.flashWidUintComboBox.Size = new System.Drawing.Size(54, 20);
            this.flashWidUintComboBox.TabIndex = 34;
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(78, 4);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.RegexText = "^\\d*$";
            this.widthTextBox.RegexTextRuntime = "^\\d*$";
            this.widthTextBox.Size = new System.Drawing.Size(60, 21);
            this.widthTextBox.TabIndex = 36;
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(78, 26);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.RegexText = "^\\d*$";
            this.heightTextBox.RegexTextRuntime = "^\\d*$";
            this.heightTextBox.Size = new System.Drawing.Size(60, 21);
            this.heightTextBox.TabIndex = 37;
            // 
            // bgColorButton
            // 
            this.bgColorButton.ColorArgbString = "";
            this.bgColorButton.Location = new System.Drawing.Point(628, 74);
            this.bgColorButton.MyColor = System.Drawing.Color.Empty;
            this.bgColorButton.Name = "bgColorButton";
            this.bgColorButton.Size = new System.Drawing.Size(21, 21);
            this.bgColorButton.TabIndex = 0;
            this.bgColorButton.Visible = false;
            // 
            // MediaUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bgColorButton);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.flashHeigUintComboBox);
            this.Controls.Add(this.flashWidUintComboBox);
            this.Controls.Add(this.bgtextBox);
            this.Controls.Add(this.bglabel);
            this.Controls.Add(this.vspacetextBox);
            this.Controls.Add(this.hspacetextBox);
            this.Controls.Add(this.hspacelabel);
            this.Controls.Add(this.vspacelabel);
            this.Controls.Add(this.alignlabel);
            this.Controls.Add(this.scalelabel);
            this.Controls.Add(this.qualitylabel);
            this.Controls.Add(this.aligncomboBox);
            this.Controls.Add(this.scalecomboBox);
            this.Controls.Add(this.qualitycomboBox);
            this.Controls.Add(this.autoplaycheckBox);
            this.Controls.Add(this.loopcheckBox);
            this.Controls.Add(this.useclasscomboBox);
            this.Controls.Add(this.useclassLabel);
            this.Controls.Add(this.pathtextBox);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Name = "MediaUserControl";
            this.Size = new System.Drawing.Size(827, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MediaUserControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorGeneralButton bgColorButton;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.TextBox pathtextBox;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.ComboBox useclasscomboBox;
        private System.Windows.Forms.Label useclassLabel;
        private System.Windows.Forms.CheckBox loopcheckBox;
        private System.Windows.Forms.CheckBox autoplaycheckBox;
        private System.Windows.Forms.ComboBox qualitycomboBox;
        private System.Windows.Forms.ComboBox scalecomboBox;
        private System.Windows.Forms.ComboBox aligncomboBox;
        private System.Windows.Forms.Label qualitylabel;
        private System.Windows.Forms.Label scalelabel;
        private System.Windows.Forms.Label alignlabel;
        private System.Windows.Forms.Label vspacelabel;
        private System.Windows.Forms.Label hspacelabel;
        private ValidateTextBox vspacetextBox;
        private ValidateTextBox hspacetextBox;
        private System.Windows.Forms.Label bglabel;
        private System.Windows.Forms.TextBox bgtextBox;
        private System.Windows.Forms.ComboBox flashHeigUintComboBox;
        private System.Windows.Forms.ComboBox flashWidUintComboBox;
        private ValidateTextBox widthTextBox;
        private ValidateTextBox heightTextBox;
    }
}
