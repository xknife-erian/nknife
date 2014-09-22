namespace Jeelu.Win
{
    partial class BoxCssControl
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
            this.borderPicBox = new System.Windows.Forms.PictureBox();
            this.tabBox = new System.Windows.Forms.TabControl();
            this.tabPagePadding = new System.Windows.Forms.TabPage();
            this.cssPaddingLeft = new Jeelu.Win.CssFieldUnit();
            this.cssPaddingDown = new Jeelu.Win.CssFieldUnit();
            this.cssPaddingRight = new Jeelu.Win.CssFieldUnit();
            this.cssPaddingUp = new Jeelu.Win.CssFieldUnit();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxPadding = new System.Windows.Forms.CheckBox();
            this.tabPageMargin = new System.Windows.Forms.TabPage();
            this.cssMarginLeft = new Jeelu.Win.CssFieldUnit();
            this.cssMarginDown = new Jeelu.Win.CssFieldUnit();
            this.cssMarginRight = new Jeelu.Win.CssFieldUnit();
            this.cssMarginUp = new Jeelu.Win.CssFieldUnit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxMargin = new System.Windows.Forms.CheckBox();
            this.tabPageBorder = new System.Windows.Forms.TabPage();
            this.groupBoxColor = new System.Windows.Forms.GroupBox();
            this.cssBorderColorLeft = new Jeelu.Win.ColorTextBoxButton();
            this.cssBorderColorDown = new Jeelu.Win.ColorTextBoxButton();
            this.cssBorderColorRight = new Jeelu.Win.ColorTextBoxButton();
            this.cssBorderColorUp = new Jeelu.Win.ColorTextBoxButton();
            this.checkBoxBorderColor = new System.Windows.Forms.CheckBox();
            this.groupBoxWidth = new System.Windows.Forms.GroupBox();
            this.cssBorderWidthLeft = new Jeelu.Win.CssFieldUnit();
            this.cssBorderWidthDown = new Jeelu.Win.CssFieldUnit();
            this.cssBorderWidthRight = new Jeelu.Win.CssFieldUnit();
            this.cssBorderWidthUp = new Jeelu.Win.CssFieldUnit();
            this.checkBoxBorderWidth = new System.Windows.Forms.CheckBox();
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.cmbStyleLeft = new System.Windows.Forms.ComboBox();
            this.cmbStyleDown = new System.Windows.Forms.ComboBox();
            this.cmbStyleRight = new System.Windows.Forms.ComboBox();
            this.cmbStyleUp = new System.Windows.Forms.ComboBox();
            this.checkBoxBoderStyle = new System.Windows.Forms.CheckBox();
            this.labBorderLeft = new System.Windows.Forms.Label();
            this.labBorderDown = new System.Windows.Forms.Label();
            this.labBorderRight = new System.Windows.Forms.Label();
            this.labBorderUp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.borderPicBox)).BeginInit();
            this.tabBox.SuspendLayout();
            this.tabPagePadding.SuspendLayout();
            this.tabPageMargin.SuspendLayout();
            this.tabPageBorder.SuspendLayout();
            this.groupBoxColor.SuspendLayout();
            this.groupBoxWidth.SuspendLayout();
            this.groupStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderPicBox
            // 
            this.borderPicBox.BackColor = System.Drawing.SystemColors.Info;
            this.borderPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.borderPicBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderPicBox.Image = global::Jeelu.Win.Resource.q;
            this.borderPicBox.Location = new System.Drawing.Point(0, 0);
            this.borderPicBox.Name = "borderPicBox";
            this.borderPicBox.Size = new System.Drawing.Size(72, 250);
            this.borderPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.borderPicBox.TabIndex = 2;
            this.borderPicBox.TabStop = false;
            // 
            // tabBox
            // 
            this.tabBox.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabBox.Controls.Add(this.tabPagePadding);
            this.tabBox.Controls.Add(this.tabPageMargin);
            this.tabBox.Controls.Add(this.tabPageBorder);
            this.tabBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBox.Location = new System.Drawing.Point(72, 0);
            this.tabBox.Name = "tabBox";
            this.tabBox.SelectedIndex = 0;
            this.tabBox.Size = new System.Drawing.Size(358, 250);
            this.tabBox.TabIndex = 3;
            // 
            // tabPagePadding
            // 
            this.tabPagePadding.Controls.Add(this.cssPaddingLeft);
            this.tabPagePadding.Controls.Add(this.cssPaddingDown);
            this.tabPagePadding.Controls.Add(this.cssPaddingRight);
            this.tabPagePadding.Controls.Add(this.cssPaddingUp);
            this.tabPagePadding.Controls.Add(this.label5);
            this.tabPagePadding.Controls.Add(this.label6);
            this.tabPagePadding.Controls.Add(this.label7);
            this.tabPagePadding.Controls.Add(this.label8);
            this.tabPagePadding.Controls.Add(this.checkBoxPadding);
            this.tabPagePadding.Location = new System.Drawing.Point(4, 24);
            this.tabPagePadding.Name = "tabPagePadding";
            this.tabPagePadding.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePadding.Size = new System.Drawing.Size(350, 222);
            this.tabPagePadding.TabIndex = 0;
            this.tabPagePadding.Text = "填 充";
            this.tabPagePadding.UseVisualStyleBackColor = true;
            // 
            // cssPaddingLeft
            // 
            this.cssPaddingLeft.Enabled = false;
            this.cssPaddingLeft.FieldUnitType = Jeelu.Win.CssFieldUnitType.Padding;
            this.cssPaddingLeft.Location = new System.Drawing.Point(39, 115);
            this.cssPaddingLeft.Name = "cssPaddingLeft";
            this.cssPaddingLeft.Size = new System.Drawing.Size(141, 23);
            this.cssPaddingLeft.TabIndex = 27;
            this.cssPaddingLeft.Value = "";
            // 
            // cssPaddingDown
            // 
            this.cssPaddingDown.Enabled = false;
            this.cssPaddingDown.FieldUnitType = Jeelu.Win.CssFieldUnitType.Padding;
            this.cssPaddingDown.Location = new System.Drawing.Point(39, 86);
            this.cssPaddingDown.Name = "cssPaddingDown";
            this.cssPaddingDown.Size = new System.Drawing.Size(141, 23);
            this.cssPaddingDown.TabIndex = 26;
            this.cssPaddingDown.Value = "";
            // 
            // cssPaddingRight
            // 
            this.cssPaddingRight.Enabled = false;
            this.cssPaddingRight.FieldUnitType = Jeelu.Win.CssFieldUnitType.Padding;
            this.cssPaddingRight.Location = new System.Drawing.Point(39, 58);
            this.cssPaddingRight.Name = "cssPaddingRight";
            this.cssPaddingRight.Size = new System.Drawing.Size(141, 23);
            this.cssPaddingRight.TabIndex = 25;
            this.cssPaddingRight.Value = "";
            // 
            // cssPaddingUp
            // 
            this.cssPaddingUp.FieldUnitType = Jeelu.Win.CssFieldUnitType.Padding;
            this.cssPaddingUp.Location = new System.Drawing.Point(39, 30);
            this.cssPaddingUp.Name = "cssPaddingUp";
            this.cssPaddingUp.Size = new System.Drawing.Size(141, 23);
            this.cssPaddingUp.TabIndex = 24;
            this.cssPaddingUp.Value = "";
            this.cssPaddingUp.ValueChanged += new System.EventHandler(this.cssPaddingUp_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "左：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "下：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "右：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "上：";
            // 
            // checkBoxPadding
            // 
            this.checkBoxPadding.AutoSize = true;
            this.checkBoxPadding.Checked = true;
            this.checkBoxPadding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPadding.Location = new System.Drawing.Point(15, 7);
            this.checkBoxPadding.Name = "checkBoxPadding";
            this.checkBoxPadding.Size = new System.Drawing.Size(72, 16);
            this.checkBoxPadding.TabIndex = 0;
            this.checkBoxPadding.Text = "全部相同";
            this.checkBoxPadding.UseVisualStyleBackColor = true;
            this.checkBoxPadding.CheckedChanged += new System.EventHandler(this.checkBoxPadding_CheckedChanged);
            // 
            // tabPageMargin
            // 
            this.tabPageMargin.Controls.Add(this.cssMarginLeft);
            this.tabPageMargin.Controls.Add(this.cssMarginDown);
            this.tabPageMargin.Controls.Add(this.cssMarginRight);
            this.tabPageMargin.Controls.Add(this.cssMarginUp);
            this.tabPageMargin.Controls.Add(this.label1);
            this.tabPageMargin.Controls.Add(this.label2);
            this.tabPageMargin.Controls.Add(this.label3);
            this.tabPageMargin.Controls.Add(this.label4);
            this.tabPageMargin.Controls.Add(this.checkBoxMargin);
            this.tabPageMargin.Location = new System.Drawing.Point(4, 24);
            this.tabPageMargin.Name = "tabPageMargin";
            this.tabPageMargin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMargin.Size = new System.Drawing.Size(350, 222);
            this.tabPageMargin.TabIndex = 1;
            this.tabPageMargin.Text = "边 界";
            this.tabPageMargin.UseVisualStyleBackColor = true;
            // 
            // cssMarginLeft
            // 
            this.cssMarginLeft.Enabled = false;
            this.cssMarginLeft.FieldUnitType = Jeelu.Win.CssFieldUnitType.MarginWidthHeightPosition;
            this.cssMarginLeft.Location = new System.Drawing.Point(38, 115);
            this.cssMarginLeft.Name = "cssMarginLeft";
            this.cssMarginLeft.Size = new System.Drawing.Size(141, 23);
            this.cssMarginLeft.TabIndex = 19;
            this.cssMarginLeft.Value = "";
            // 
            // cssMarginDown
            // 
            this.cssMarginDown.Enabled = false;
            this.cssMarginDown.FieldUnitType = Jeelu.Win.CssFieldUnitType.MarginWidthHeightPosition;
            this.cssMarginDown.Location = new System.Drawing.Point(38, 86);
            this.cssMarginDown.Name = "cssMarginDown";
            this.cssMarginDown.Size = new System.Drawing.Size(141, 23);
            this.cssMarginDown.TabIndex = 18;
            this.cssMarginDown.Value = "";
            // 
            // cssMarginRight
            // 
            this.cssMarginRight.Enabled = false;
            this.cssMarginRight.FieldUnitType = Jeelu.Win.CssFieldUnitType.MarginWidthHeightPosition;
            this.cssMarginRight.Location = new System.Drawing.Point(38, 58);
            this.cssMarginRight.Name = "cssMarginRight";
            this.cssMarginRight.Size = new System.Drawing.Size(141, 23);
            this.cssMarginRight.TabIndex = 17;
            this.cssMarginRight.Value = "";
            // 
            // cssMarginUp
            // 
            this.cssMarginUp.FieldUnitType = Jeelu.Win.CssFieldUnitType.MarginWidthHeightPosition;
            this.cssMarginUp.Location = new System.Drawing.Point(38, 30);
            this.cssMarginUp.Name = "cssMarginUp";
            this.cssMarginUp.Size = new System.Drawing.Size(141, 23);
            this.cssMarginUp.TabIndex = 16;
            this.cssMarginUp.Value = "";
            this.cssMarginUp.ValueChanged += new System.EventHandler(this.cssMarginUp_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "左：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "下：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "右：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "上：";
            // 
            // checkBoxMargin
            // 
            this.checkBoxMargin.AutoSize = true;
            this.checkBoxMargin.Checked = true;
            this.checkBoxMargin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMargin.Location = new System.Drawing.Point(15, 7);
            this.checkBoxMargin.Name = "checkBoxMargin";
            this.checkBoxMargin.Size = new System.Drawing.Size(72, 16);
            this.checkBoxMargin.TabIndex = 1;
            this.checkBoxMargin.Text = "全部相同";
            this.checkBoxMargin.UseVisualStyleBackColor = true;
            this.checkBoxMargin.CheckedChanged += new System.EventHandler(this.checkBoxMargin_CheckedChanged);
            // 
            // tabPageBorder
            // 
            this.tabPageBorder.Controls.Add(this.groupBoxColor);
            this.tabPageBorder.Controls.Add(this.groupBoxWidth);
            this.tabPageBorder.Controls.Add(this.groupStyle);
            this.tabPageBorder.Controls.Add(this.labBorderLeft);
            this.tabPageBorder.Controls.Add(this.labBorderDown);
            this.tabPageBorder.Controls.Add(this.labBorderRight);
            this.tabPageBorder.Controls.Add(this.labBorderUp);
            this.tabPageBorder.Location = new System.Drawing.Point(4, 24);
            this.tabPageBorder.Name = "tabPageBorder";
            this.tabPageBorder.Size = new System.Drawing.Size(350, 222);
            this.tabPageBorder.TabIndex = 2;
            this.tabPageBorder.Text = "边 框";
            this.tabPageBorder.UseVisualStyleBackColor = true;
            // 
            // groupBoxColor
            // 
            this.groupBoxColor.Controls.Add(this.cssBorderColorLeft);
            this.groupBoxColor.Controls.Add(this.cssBorderColorDown);
            this.groupBoxColor.Controls.Add(this.cssBorderColorRight);
            this.groupBoxColor.Controls.Add(this.cssBorderColorUp);
            this.groupBoxColor.Controls.Add(this.checkBoxBorderColor);
            this.groupBoxColor.Location = new System.Drawing.Point(258, 3);
            this.groupBoxColor.Name = "groupBoxColor";
            this.groupBoxColor.Size = new System.Drawing.Size(88, 145);
            this.groupBoxColor.TabIndex = 8;
            this.groupBoxColor.TabStop = false;
            this.groupBoxColor.Text = "颜色";
            // 
            // cssBorderColorLeft
            // 
            this.cssBorderColorLeft.CheckColor = true;
            this.cssBorderColorLeft.Color = System.Drawing.Color.Empty;
            this.cssBorderColorLeft.ColorText = "";
            this.cssBorderColorLeft.Enabled = false;
            this.cssBorderColorLeft.Location = new System.Drawing.Point(6, 113);
            this.cssBorderColorLeft.Name = "cssBorderColorLeft";
            this.cssBorderColorLeft.Size = new System.Drawing.Size(80, 22);
            this.cssBorderColorLeft.TabIndex = 6;
            // 
            // cssBorderColorDown
            // 
            this.cssBorderColorDown.CheckColor = true;
            this.cssBorderColorDown.Color = System.Drawing.Color.Empty;
            this.cssBorderColorDown.ColorText = "";
            this.cssBorderColorDown.Enabled = false;
            this.cssBorderColorDown.Location = new System.Drawing.Point(6, 89);
            this.cssBorderColorDown.Name = "cssBorderColorDown";
            this.cssBorderColorDown.Size = new System.Drawing.Size(80, 22);
            this.cssBorderColorDown.TabIndex = 5;
            // 
            // cssBorderColorRight
            // 
            this.cssBorderColorRight.CheckColor = true;
            this.cssBorderColorRight.Color = System.Drawing.Color.Empty;
            this.cssBorderColorRight.ColorText = "";
            this.cssBorderColorRight.Enabled = false;
            this.cssBorderColorRight.Location = new System.Drawing.Point(6, 63);
            this.cssBorderColorRight.Name = "cssBorderColorRight";
            this.cssBorderColorRight.Size = new System.Drawing.Size(80, 22);
            this.cssBorderColorRight.TabIndex = 4;
            // 
            // cssBorderColorUp
            // 
            this.cssBorderColorUp.CheckColor = true;
            this.cssBorderColorUp.Color = System.Drawing.Color.Empty;
            this.cssBorderColorUp.ColorText = "";
            this.cssBorderColorUp.Location = new System.Drawing.Point(6, 39);
            this.cssBorderColorUp.Name = "cssBorderColorUp";
            this.cssBorderColorUp.Size = new System.Drawing.Size(80, 22);
            this.cssBorderColorUp.TabIndex = 3;
            this.cssBorderColorUp.ColorTextChanged += new System.EventHandler(this.cssBorderColorUp_ColorTextChanged);
            // 
            // checkBoxBorderColor
            // 
            this.checkBoxBorderColor.AutoSize = true;
            this.checkBoxBorderColor.Checked = true;
            this.checkBoxBorderColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBorderColor.Location = new System.Drawing.Point(6, 18);
            this.checkBoxBorderColor.Name = "checkBoxBorderColor";
            this.checkBoxBorderColor.Size = new System.Drawing.Size(72, 16);
            this.checkBoxBorderColor.TabIndex = 2;
            this.checkBoxBorderColor.Text = "全部相同";
            this.checkBoxBorderColor.UseVisualStyleBackColor = true;
            this.checkBoxBorderColor.CheckedChanged += new System.EventHandler(this.checkBoxBorderColor_CheckedChanged);
            // 
            // groupBoxWidth
            // 
            this.groupBoxWidth.Controls.Add(this.cssBorderWidthLeft);
            this.groupBoxWidth.Controls.Add(this.cssBorderWidthDown);
            this.groupBoxWidth.Controls.Add(this.cssBorderWidthRight);
            this.groupBoxWidth.Controls.Add(this.cssBorderWidthUp);
            this.groupBoxWidth.Controls.Add(this.checkBoxBorderWidth);
            this.groupBoxWidth.Location = new System.Drawing.Point(123, 3);
            this.groupBoxWidth.Name = "groupBoxWidth";
            this.groupBoxWidth.Size = new System.Drawing.Size(135, 145);
            this.groupBoxWidth.TabIndex = 7;
            this.groupBoxWidth.TabStop = false;
            this.groupBoxWidth.Text = "宽度";
            // 
            // cssBorderWidthLeft
            // 
            this.cssBorderWidthLeft.Enabled = false;
            this.cssBorderWidthLeft.FieldUnitType = Jeelu.Win.CssFieldUnitType.BorderWidth;
            this.cssBorderWidthLeft.Location = new System.Drawing.Point(3, 116);
            this.cssBorderWidthLeft.Name = "cssBorderWidthLeft";
            this.cssBorderWidthLeft.Size = new System.Drawing.Size(129, 23);
            this.cssBorderWidthLeft.TabIndex = 6;
            this.cssBorderWidthLeft.Value = "";
            // 
            // cssBorderWidthDown
            // 
            this.cssBorderWidthDown.Enabled = false;
            this.cssBorderWidthDown.FieldUnitType = Jeelu.Win.CssFieldUnitType.BorderWidth;
            this.cssBorderWidthDown.Location = new System.Drawing.Point(4, 90);
            this.cssBorderWidthDown.Name = "cssBorderWidthDown";
            this.cssBorderWidthDown.Size = new System.Drawing.Size(129, 23);
            this.cssBorderWidthDown.TabIndex = 5;
            this.cssBorderWidthDown.Value = "";
            // 
            // cssBorderWidthRight
            // 
            this.cssBorderWidthRight.Enabled = false;
            this.cssBorderWidthRight.FieldUnitType = Jeelu.Win.CssFieldUnitType.BorderWidth;
            this.cssBorderWidthRight.Location = new System.Drawing.Point(4, 64);
            this.cssBorderWidthRight.Name = "cssBorderWidthRight";
            this.cssBorderWidthRight.Size = new System.Drawing.Size(129, 23);
            this.cssBorderWidthRight.TabIndex = 4;
            this.cssBorderWidthRight.Value = "";
            // 
            // cssBorderWidthUp
            // 
            this.cssBorderWidthUp.FieldUnitType = Jeelu.Win.CssFieldUnitType.BorderWidth;
            this.cssBorderWidthUp.Location = new System.Drawing.Point(4, 38);
            this.cssBorderWidthUp.Name = "cssBorderWidthUp";
            this.cssBorderWidthUp.Size = new System.Drawing.Size(129, 23);
            this.cssBorderWidthUp.TabIndex = 3;
            this.cssBorderWidthUp.Value = "";
            this.cssBorderWidthUp.ValueChanged += new System.EventHandler(this.cssBorderWidthUp_ValueChanged);
            // 
            // checkBoxBorderWidth
            // 
            this.checkBoxBorderWidth.AutoSize = true;
            this.checkBoxBorderWidth.Checked = true;
            this.checkBoxBorderWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBorderWidth.Location = new System.Drawing.Point(6, 18);
            this.checkBoxBorderWidth.Name = "checkBoxBorderWidth";
            this.checkBoxBorderWidth.Size = new System.Drawing.Size(72, 16);
            this.checkBoxBorderWidth.TabIndex = 2;
            this.checkBoxBorderWidth.Text = "全部相同";
            this.checkBoxBorderWidth.UseVisualStyleBackColor = true;
            this.checkBoxBorderWidth.CheckedChanged += new System.EventHandler(this.checkBoxBorderWidth_CheckedChanged);
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.cmbStyleLeft);
            this.groupStyle.Controls.Add(this.cmbStyleDown);
            this.groupStyle.Controls.Add(this.cmbStyleRight);
            this.groupStyle.Controls.Add(this.cmbStyleUp);
            this.groupStyle.Controls.Add(this.checkBoxBoderStyle);
            this.groupStyle.Location = new System.Drawing.Point(38, 3);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(84, 145);
            this.groupStyle.TabIndex = 6;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "样式";
            // 
            // cmbStyleLeft
            // 
            this.cmbStyleLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleLeft.Enabled = false;
            this.cmbStyleLeft.FormattingEnabled = true;
            this.cmbStyleLeft.Location = new System.Drawing.Point(6, 117);
            this.cmbStyleLeft.Name = "cmbStyleLeft";
            this.cmbStyleLeft.Size = new System.Drawing.Size(70, 20);
            this.cmbStyleLeft.TabIndex = 13;
            // 
            // cmbStyleDown
            // 
            this.cmbStyleDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleDown.Enabled = false;
            this.cmbStyleDown.FormattingEnabled = true;
            this.cmbStyleDown.Location = new System.Drawing.Point(6, 91);
            this.cmbStyleDown.Name = "cmbStyleDown";
            this.cmbStyleDown.Size = new System.Drawing.Size(70, 20);
            this.cmbStyleDown.TabIndex = 12;
            // 
            // cmbStyleRight
            // 
            this.cmbStyleRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleRight.Enabled = false;
            this.cmbStyleRight.FormattingEnabled = true;
            this.cmbStyleRight.Location = new System.Drawing.Point(6, 65);
            this.cmbStyleRight.Name = "cmbStyleRight";
            this.cmbStyleRight.Size = new System.Drawing.Size(70, 20);
            this.cmbStyleRight.TabIndex = 11;
            // 
            // cmbStyleUp
            // 
            this.cmbStyleUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleUp.FormattingEnabled = true;
            this.cmbStyleUp.Location = new System.Drawing.Point(6, 39);
            this.cmbStyleUp.Name = "cmbStyleUp";
            this.cmbStyleUp.Size = new System.Drawing.Size(70, 20);
            this.cmbStyleUp.TabIndex = 10;
            this.cmbStyleUp.TextChanged += new System.EventHandler(this.cmbStyleUp_TextChanged);
            // 
            // checkBoxBoderStyle
            // 
            this.checkBoxBoderStyle.AutoSize = true;
            this.checkBoxBoderStyle.Checked = true;
            this.checkBoxBoderStyle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBoderStyle.Location = new System.Drawing.Point(6, 18);
            this.checkBoxBoderStyle.Name = "checkBoxBoderStyle";
            this.checkBoxBoderStyle.Size = new System.Drawing.Size(72, 16);
            this.checkBoxBoderStyle.TabIndex = 1;
            this.checkBoxBoderStyle.Text = "全部相同";
            this.checkBoxBoderStyle.UseVisualStyleBackColor = true;
            this.checkBoxBoderStyle.CheckedChanged += new System.EventHandler(this.checkBoxBoderStyle_CheckedChanged);
            // 
            // labBorderLeft
            // 
            this.labBorderLeft.AutoSize = true;
            this.labBorderLeft.Location = new System.Drawing.Point(5, 124);
            this.labBorderLeft.Name = "labBorderLeft";
            this.labBorderLeft.Size = new System.Drawing.Size(29, 12);
            this.labBorderLeft.TabIndex = 5;
            this.labBorderLeft.Text = "左：";
            // 
            // labBorderDown
            // 
            this.labBorderDown.AutoSize = true;
            this.labBorderDown.Location = new System.Drawing.Point(4, 98);
            this.labBorderDown.Name = "labBorderDown";
            this.labBorderDown.Size = new System.Drawing.Size(29, 12);
            this.labBorderDown.TabIndex = 4;
            this.labBorderDown.Text = "下：";
            // 
            // labBorderRight
            // 
            this.labBorderRight.AutoSize = true;
            this.labBorderRight.Location = new System.Drawing.Point(5, 72);
            this.labBorderRight.Name = "labBorderRight";
            this.labBorderRight.Size = new System.Drawing.Size(29, 12);
            this.labBorderRight.TabIndex = 3;
            this.labBorderRight.Text = "右：";
            // 
            // labBorderUp
            // 
            this.labBorderUp.AutoSize = true;
            this.labBorderUp.Location = new System.Drawing.Point(5, 46);
            this.labBorderUp.Name = "labBorderUp";
            this.labBorderUp.Size = new System.Drawing.Size(29, 12);
            this.labBorderUp.TabIndex = 2;
            this.labBorderUp.Text = "上：";
            // 
            // BoxCssControl
            // 
            this.Controls.Add(this.tabBox);
            this.Controls.Add(this.borderPicBox);
            this.Name = "BoxCssControl";
            ((System.ComponentModel.ISupportInitialize)(this.borderPicBox)).EndInit();
            this.tabBox.ResumeLayout(false);
            this.tabPagePadding.ResumeLayout(false);
            this.tabPagePadding.PerformLayout();
            this.tabPageMargin.ResumeLayout(false);
            this.tabPageMargin.PerformLayout();
            this.tabPageBorder.ResumeLayout(false);
            this.tabPageBorder.PerformLayout();
            this.groupBoxColor.ResumeLayout(false);
            this.groupBoxColor.PerformLayout();
            this.groupBoxWidth.ResumeLayout(false);
            this.groupBoxWidth.PerformLayout();
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox borderPicBox;
        private System.Windows.Forms.TabControl tabBox;
        private System.Windows.Forms.TabPage tabPagePadding;
        private System.Windows.Forms.TabPage tabPageMargin;
        private System.Windows.Forms.TabPage tabPageBorder;
        private System.Windows.Forms.CheckBox checkBoxPadding;
        private System.Windows.Forms.CheckBox checkBoxMargin;
        private System.Windows.Forms.CheckBox checkBoxBoderStyle;
        private System.Windows.Forms.Label labBorderLeft;
        private System.Windows.Forms.Label labBorderDown;
        private System.Windows.Forms.Label labBorderRight;
        private System.Windows.Forms.Label labBorderUp;
        private CssFieldUnit cssMarginLeft;
        private CssFieldUnit cssMarginDown;
        private CssFieldUnit cssMarginRight;
        private CssFieldUnit cssMarginUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CssFieldUnit cssPaddingLeft;
        private CssFieldUnit cssPaddingDown;
        private CssFieldUnit cssPaddingRight;
        private CssFieldUnit cssPaddingUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBoxWidth;
        private System.Windows.Forms.GroupBox groupStyle;
        private System.Windows.Forms.ComboBox cmbStyleLeft;
        private System.Windows.Forms.ComboBox cmbStyleDown;
        private System.Windows.Forms.ComboBox cmbStyleRight;
        private System.Windows.Forms.ComboBox cmbStyleUp;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private ColorTextBoxButton cssBorderColorRight;
        private ColorTextBoxButton cssBorderColorUp;
        private System.Windows.Forms.CheckBox checkBoxBorderColor;
        private System.Windows.Forms.CheckBox checkBoxBorderWidth;
        private ColorTextBoxButton cssBorderColorLeft;
        private ColorTextBoxButton cssBorderColorDown;
        private CssFieldUnit cssBorderWidthLeft;
        private CssFieldUnit cssBorderWidthDown;
        private CssFieldUnit cssBorderWidthRight;
        private CssFieldUnit cssBorderWidthUp;
    }
}
