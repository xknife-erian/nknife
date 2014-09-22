using System.Drawing;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class CssSetupForm
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("常用选项");
            Jeelu.CssSection cssSection2 = new Jeelu.CssSection();
            this.treeCSSType = new System.Windows.Forms.TreeView();
            this.checkBoxDisplayCSSText = new System.Windows.Forms.CheckBox();
            this.textBoxCSSText = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelCSSType = new System.Windows.Forms.Label();
            this.labelCurrentContent = new System.Windows.Forms.Label();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.tabPageBackGroud = new System.Windows.Forms.TabPage();
            this.singleLine3 = new Jeelu.Win.SingleLine();
            this.checkBoxIsAutoSize = new System.Windows.Forms.CheckBox();
            this.groupBoxLinkOption = new System.Windows.Forms.GroupBox();
            this.checkBoxImgBorder = new System.Windows.Forms.CheckBox();
            this.labelColorLast = new System.Windows.Forms.Label();
            this.textBoxLastColor = new System.Windows.Forms.TextBox();
            this.colorBtnLast = new Jeelu.Win.ColorGeneralButton();
            this.checkBoxUnderLine = new System.Windows.Forms.CheckBox();
            this.labelColorBefore = new System.Windows.Forms.Label();
            this.textBoxBeforeColor = new System.Windows.Forms.TextBox();
            this.colorBtnBefore = new Jeelu.Win.ColorGeneralButton();
            this.btnBrowsePic = new System.Windows.Forms.Button();
            this.checkBoxUsingCurImg = new System.Windows.Forms.CheckBox();
            this.checkBoxUsingBackImg = new System.Windows.Forms.CheckBox();
            this.checkBoxUsingBackColor = new System.Windows.Forms.CheckBox();
            this.comboBoxBackgroudLayout = new System.Windows.Forms.ComboBox();
            this.labelBackLayout = new System.Windows.Forms.Label();
            this.textBoxBackColor = new System.Windows.Forms.TextBox();
            this.btnSelectBackColor = new Jeelu.Win.ColorGeneralButton();
            this.singleLine2 = new Jeelu.Win.SingleLine();
            this.tabPageLayout = new System.Windows.Forms.TabPage();
            this.groupBoxGeneralLayout = new System.Windows.Forms.GroupBox();
            this.cssGeneralLayoutControl = new Jeelu.SimplusD.Client.Win.CSSGeneralLayoutControl();
            this.groupBoxGeneralContent = new System.Windows.Forms.GroupBox();
            this.cssGeneralContectControl = new Jeelu.SimplusD.Client.Win.CSSGeneralContectControl();
            this.tabControlGeneral = new System.Windows.Forms.TabControl();
            this.tabPageBorder = new System.Windows.Forms.TabPage();
            this.myBoxModelPanel = new Jeelu.SimplusD.Client.Win.BoxModelPanel();
            this.borderPicBox = new System.Windows.Forms.PictureBox();
            this.tabPageFont = new System.Windows.Forms.TabPage();
            this.myFont = new Jeelu.SimplusD.Client.Win.CssMyFont();
            this.tabPageProperty = new System.Windows.Forms.TabPage();
            this.textBoxCurType = new System.Windows.Forms.TextBox();
            this.labelCurType = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.tabPageBackGroud.SuspendLayout();
            this.groupBoxLinkOption.SuspendLayout();
            this.tabPageLayout.SuspendLayout();
            this.groupBoxGeneralLayout.SuspendLayout();
            this.groupBoxGeneralContent.SuspendLayout();
            this.tabControlGeneral.SuspendLayout();
            this.tabPageBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.borderPicBox)).BeginInit();
            this.tabPageFont.SuspendLayout();
            this.tabPageProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeCSSType
            // 
            this.treeCSSType.BackColor = System.Drawing.SystemColors.Info;
            this.treeCSSType.Location = new System.Drawing.Point(10, 26);
            this.treeCSSType.Name = "treeCSSType";
            treeNode2.Name = "_generalNode";
            treeNode2.Text = "常用选项";
            this.treeCSSType.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeCSSType.Size = new System.Drawing.Size(170, 264);
            this.treeCSSType.TabIndex = 0;
            this.treeCSSType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCSSType_AfterSelect);
            // 
            // checkBoxDisplayCSSText
            // 
            this.checkBoxDisplayCSSText.AutoSize = true;
            this.checkBoxDisplayCSSText.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxDisplayCSSText.Location = new System.Drawing.Point(10, 296);
            this.checkBoxDisplayCSSText.Name = "checkBoxDisplayCSSText";
            this.checkBoxDisplayCSSText.Size = new System.Drawing.Size(105, 17);
            this.checkBoxDisplayCSSText.TabIndex = 1;
            this.checkBoxDisplayCSSText.Text = "显示CSS字符串";
            this.checkBoxDisplayCSSText.UseVisualStyleBackColor = false;
            this.checkBoxDisplayCSSText.CheckedChanged += new System.EventHandler(this.checkBoxDisplayCSSText_CheckedChanged);
            // 
            // textBoxCSSText
            // 
            this.textBoxCSSText.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxCSSText.Location = new System.Drawing.Point(10, 320);
            this.textBoxCSSText.Multiline = true;
            this.textBoxCSSText.Name = "textBoxCSSText";
            this.textBoxCSSText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCSSText.Size = new System.Drawing.Size(632, 22);
            this.textBoxCSSText.TabIndex = 3;
            this.textBoxCSSText.Visible = false;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(510, 313);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(58, 25);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(574, 313);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(58, 25);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "取 消";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // labelCSSType
            // 
            this.labelCSSType.AutoSize = true;
            this.labelCSSType.Location = new System.Drawing.Point(8, 10);
            this.labelCSSType.Name = "labelCSSType";
            this.labelCSSType.Size = new System.Drawing.Size(31, 13);
            this.labelCSSType.TabIndex = 7;
            this.labelCSSType.Text = "选项";
            // 
            // labelCurrentContent
            // 
            this.labelCurrentContent.AutoSize = true;
            this.labelCurrentContent.Location = new System.Drawing.Point(184, 10);
            this.labelCurrentContent.Name = "labelCurrentContent";
            this.labelCurrentContent.Size = new System.Drawing.Size(55, 13);
            this.labelCurrentContent.TabIndex = 8;
            this.labelCurrentContent.Text = "当前类型";
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(186, 26);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(456, 1);
            this.singleLine1.TabIndex = 9;
            this.singleLine1.TabStop = false;
            // 
            // tabPageBackGroud
            // 
            this.tabPageBackGroud.Controls.Add(this.singleLine3);
            this.tabPageBackGroud.Controls.Add(this.checkBoxIsAutoSize);
            this.tabPageBackGroud.Controls.Add(this.groupBoxLinkOption);
            this.tabPageBackGroud.Controls.Add(this.btnBrowsePic);
            this.tabPageBackGroud.Controls.Add(this.checkBoxUsingCurImg);
            this.tabPageBackGroud.Controls.Add(this.checkBoxUsingBackImg);
            this.tabPageBackGroud.Controls.Add(this.checkBoxUsingBackColor);
            this.tabPageBackGroud.Controls.Add(this.comboBoxBackgroudLayout);
            this.tabPageBackGroud.Controls.Add(this.labelBackLayout);
            this.tabPageBackGroud.Controls.Add(this.textBoxBackColor);
            this.tabPageBackGroud.Controls.Add(this.btnSelectBackColor);
            this.tabPageBackGroud.Controls.Add(this.singleLine2);
            this.tabPageBackGroud.Location = new System.Drawing.Point(4, 25);
            this.tabPageBackGroud.Name = "tabPageBackGroud";
            this.tabPageBackGroud.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackGroud.Size = new System.Drawing.Size(447, 235);
            this.tabPageBackGroud.TabIndex = 1;
            this.tabPageBackGroud.Text = "背景";
            this.tabPageBackGroud.UseVisualStyleBackColor = true;
            // 
            // singleLine3
            // 
            this.singleLine3.Location = new System.Drawing.Point(3, 29);
            this.singleLine3.Name = "singleLine3";
            this.singleLine3.Size = new System.Drawing.Size(435, 1);
            this.singleLine3.TabIndex = 60;
            this.singleLine3.TabStop = false;
            // 
            // checkBoxIsAutoSize
            // 
            this.checkBoxIsAutoSize.AutoSize = true;
            this.checkBoxIsAutoSize.Location = new System.Drawing.Point(9, 6);
            this.checkBoxIsAutoSize.Name = "checkBoxIsAutoSize";
            this.checkBoxIsAutoSize.Size = new System.Drawing.Size(146, 17);
            this.checkBoxIsAutoSize.TabIndex = 59;
            this.checkBoxIsAutoSize.Text = "页面宽窄随浏览器变化";
            this.checkBoxIsAutoSize.UseVisualStyleBackColor = true;
            this.checkBoxIsAutoSize.CheckedChanged += new System.EventHandler(this.checkBoxIsAutoSize_CheckedChanged);
            // 
            // groupBoxLinkOption
            // 
            this.groupBoxLinkOption.Controls.Add(this.checkBoxImgBorder);
            this.groupBoxLinkOption.Controls.Add(this.labelColorLast);
            this.groupBoxLinkOption.Controls.Add(this.textBoxLastColor);
            this.groupBoxLinkOption.Controls.Add(this.colorBtnLast);
            this.groupBoxLinkOption.Controls.Add(this.checkBoxUnderLine);
            this.groupBoxLinkOption.Controls.Add(this.labelColorBefore);
            this.groupBoxLinkOption.Controls.Add(this.textBoxBeforeColor);
            this.groupBoxLinkOption.Controls.Add(this.colorBtnBefore);
            this.groupBoxLinkOption.Location = new System.Drawing.Point(3, 123);
            this.groupBoxLinkOption.Name = "groupBoxLinkOption";
            this.groupBoxLinkOption.Size = new System.Drawing.Size(436, 106);
            this.groupBoxLinkOption.TabIndex = 58;
            this.groupBoxLinkOption.TabStop = false;
            this.groupBoxLinkOption.Text = "链接选项";
            // 
            // checkBoxImgBorder
            // 
            this.checkBoxImgBorder.AutoSize = true;
            this.checkBoxImgBorder.Location = new System.Drawing.Point(133, 46);
            this.checkBoxImgBorder.Name = "checkBoxImgBorder";
            this.checkBoxImgBorder.Size = new System.Drawing.Size(122, 17);
            this.checkBoxImgBorder.TabIndex = 61;
            this.checkBoxImgBorder.Text = "显示图片链接边框";
            this.checkBoxImgBorder.UseVisualStyleBackColor = true;
            this.checkBoxImgBorder.CheckedChanged += new System.EventHandler(this.checkBoxImgBorder_CheckedChanged);
            // 
            // labelColorLast
            // 
            this.labelColorLast.AutoSize = true;
            this.labelColorLast.Enabled = false;
            this.labelColorLast.Location = new System.Drawing.Point(159, 23);
            this.labelColorLast.Name = "labelColorLast";
            this.labelColorLast.Size = new System.Drawing.Size(55, 13);
            this.labelColorLast.TabIndex = 58;
            this.labelColorLast.Text = "点击后：";
            // 
            // textBoxLastColor
            // 
            this.textBoxLastColor.Location = new System.Drawing.Point(214, 17);
            this.textBoxLastColor.Multiline = true;
            this.textBoxLastColor.Name = "textBoxLastColor";
            this.textBoxLastColor.Size = new System.Drawing.Size(62, 22);
            this.textBoxLastColor.TabIndex = 57;
            this.textBoxLastColor.Text = "#0000FF";
            this.textBoxLastColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colorBtnLast
            // 
            this.colorBtnLast.ColorArgbString = "";
            this.colorBtnLast.Location = new System.Drawing.Point(282, 17);
            this.colorBtnLast.MyColor = System.Drawing.Color.Blue;
            this.colorBtnLast.Name = "colorBtnLast";
            this.colorBtnLast.Size = new System.Drawing.Size(23, 23);
            this.colorBtnLast.TabIndex = 56;
            this.colorBtnLast.Text = "Blue";
            this.colorBtnLast.UseVisualStyleBackColor = false;
            // 
            // checkBoxUnderLine
            // 
            this.checkBoxUnderLine.AutoSize = true;
            this.checkBoxUnderLine.Checked = true;
            this.checkBoxUnderLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUnderLine.Location = new System.Drawing.Point(18, 46);
            this.checkBoxUnderLine.Name = "checkBoxUnderLine";
            this.checkBoxUnderLine.Size = new System.Drawing.Size(86, 17);
            this.checkBoxUnderLine.TabIndex = 55;
            this.checkBoxUnderLine.Text = "显示下划线";
            this.checkBoxUnderLine.UseVisualStyleBackColor = true;
            this.checkBoxUnderLine.CheckedChanged += new System.EventHandler(this.checkBoxUnderLine_CheckedChanged);
            // 
            // labelColorBefore
            // 
            this.labelColorBefore.AutoSize = true;
            this.labelColorBefore.Enabled = false;
            this.labelColorBefore.Location = new System.Drawing.Point(8, 23);
            this.labelColorBefore.Name = "labelColorBefore";
            this.labelColorBefore.Size = new System.Drawing.Size(55, 13);
            this.labelColorBefore.TabIndex = 54;
            this.labelColorBefore.Text = "点击前：";
            // 
            // textBoxBeforeColor
            // 
            this.textBoxBeforeColor.Location = new System.Drawing.Point(65, 17);
            this.textBoxBeforeColor.Multiline = true;
            this.textBoxBeforeColor.Name = "textBoxBeforeColor";
            this.textBoxBeforeColor.Size = new System.Drawing.Size(62, 22);
            this.textBoxBeforeColor.TabIndex = 27;
            this.textBoxBeforeColor.Text = "#0000FF";
            this.textBoxBeforeColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colorBtnBefore
            // 
            this.colorBtnBefore.ColorArgbString = "";
            this.colorBtnBefore.Location = new System.Drawing.Point(133, 17);
            this.colorBtnBefore.MyColor = System.Drawing.Color.Blue;
            this.colorBtnBefore.Name = "colorBtnBefore";
            this.colorBtnBefore.Size = new System.Drawing.Size(23, 23);
            this.colorBtnBefore.TabIndex = 26;
            this.colorBtnBefore.Text = "Blue";
            this.colorBtnBefore.UseVisualStyleBackColor = false;
            // 
            // btnBrowsePic
            // 
            this.btnBrowsePic.Enabled = false;
            this.btnBrowsePic.Location = new System.Drawing.Point(116, 71);
            this.btnBrowsePic.Name = "btnBrowsePic";
            this.btnBrowsePic.Size = new System.Drawing.Size(60, 23);
            this.btnBrowsePic.TabIndex = 56;
            this.btnBrowsePic.Text = "浏 览";
            this.btnBrowsePic.UseVisualStyleBackColor = true;
            this.btnBrowsePic.Click += new System.EventHandler(this.btnBrowsePic_Click);
            // 
            // checkBoxUsingCurImg
            // 
            this.checkBoxUsingCurImg.AutoSize = true;
            this.checkBoxUsingCurImg.Enabled = false;
            this.checkBoxUsingCurImg.Location = new System.Drawing.Point(10, 98);
            this.checkBoxUsingCurImg.Name = "checkBoxUsingCurImg";
            this.checkBoxUsingCurImg.Size = new System.Drawing.Size(98, 17);
            this.checkBoxUsingCurImg.TabIndex = 55;
            this.checkBoxUsingCurImg.Text = "使用当前背景";
            this.checkBoxUsingCurImg.UseVisualStyleBackColor = true;
            this.checkBoxUsingCurImg.CheckedChanged += new System.EventHandler(this.checkBoxUsingCurImg_CheckedChanged);
            // 
            // checkBoxUsingBackImg
            // 
            this.checkBoxUsingBackImg.AutoSize = true;
            this.checkBoxUsingBackImg.Location = new System.Drawing.Point(10, 75);
            this.checkBoxUsingBackImg.Name = "checkBoxUsingBackImg";
            this.checkBoxUsingBackImg.Size = new System.Drawing.Size(110, 17);
            this.checkBoxUsingBackImg.TabIndex = 25;
            this.checkBoxUsingBackImg.Text = "使用背景图片：";
            this.checkBoxUsingBackImg.UseVisualStyleBackColor = true;
            this.checkBoxUsingBackImg.CheckedChanged += new System.EventHandler(this.checkBoxUsingBackImg_CheckedChanged);
            // 
            // checkBoxUsingBackColor
            // 
            this.checkBoxUsingBackColor.AutoSize = true;
            this.checkBoxUsingBackColor.Location = new System.Drawing.Point(10, 41);
            this.checkBoxUsingBackColor.Name = "checkBoxUsingBackColor";
            this.checkBoxUsingBackColor.Size = new System.Drawing.Size(98, 17);
            this.checkBoxUsingBackColor.TabIndex = 24;
            this.checkBoxUsingBackColor.Text = "使用背景色：";
            this.checkBoxUsingBackColor.UseVisualStyleBackColor = true;
            this.checkBoxUsingBackColor.CheckedChanged += new System.EventHandler(this.checkBoxUsingBackColor_CheckedChanged);
            // 
            // comboBoxBackgroudLayout
            // 
            this.comboBoxBackgroudLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackgroudLayout.Enabled = false;
            this.comboBoxBackgroudLayout.FormattingEnabled = true;
            this.comboBoxBackgroudLayout.Items.AddRange(new object[] {
            "居中",
            "平铺",
            "垂直平铺",
            "水平平铺"});
            this.comboBoxBackgroudLayout.Location = new System.Drawing.Point(270, 73);
            this.comboBoxBackgroudLayout.Name = "comboBoxBackgroudLayout";
            this.comboBoxBackgroudLayout.Size = new System.Drawing.Size(83, 21);
            this.comboBoxBackgroudLayout.TabIndex = 21;
            this.comboBoxBackgroudLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackgroudLayout_SelectedIndexChanged);
            // 
            // labelBackLayout
            // 
            this.labelBackLayout.AutoSize = true;
            this.labelBackLayout.Enabled = false;
            this.labelBackLayout.Location = new System.Drawing.Point(182, 76);
            this.labelBackLayout.Name = "labelBackLayout";
            this.labelBackLayout.Size = new System.Drawing.Size(91, 13);
            this.labelBackLayout.TabIndex = 20;
            this.labelBackLayout.Text = "背景布局方式：";
            // 
            // textBoxBackColor
            // 
            this.textBoxBackColor.Enabled = false;
            this.textBoxBackColor.Location = new System.Drawing.Point(126, 36);
            this.textBoxBackColor.Multiline = true;
            this.textBoxBackColor.Name = "textBoxBackColor";
            this.textBoxBackColor.Size = new System.Drawing.Size(91, 22);
            this.textBoxBackColor.TabIndex = 17;
            this.textBoxBackColor.Text = "#FFFFFF";
            this.textBoxBackColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxBackColor.TextChanged += new System.EventHandler(this.textBoxBackColor_TextChanged);
            // 
            // btnSelectBackColor
            // 
            this.btnSelectBackColor.ColorArgbString = "";
            this.btnSelectBackColor.Enabled = false;
            this.btnSelectBackColor.Location = new System.Drawing.Point(223, 35);
            this.btnSelectBackColor.MyColor = System.Drawing.Color.White;
            this.btnSelectBackColor.Name = "btnSelectBackColor";
            this.btnSelectBackColor.Size = new System.Drawing.Size(23, 23);
            this.btnSelectBackColor.TabIndex = 3;
            this.btnSelectBackColor.Text = "White";
            this.btnSelectBackColor.UseVisualStyleBackColor = false;
            // 
            // singleLine2
            // 
            this.singleLine2.Location = new System.Drawing.Point(5, 63);
            this.singleLine2.Name = "singleLine2";
            this.singleLine2.Size = new System.Drawing.Size(435, 1);
            this.singleLine2.TabIndex = 2;
            this.singleLine2.TabStop = false;
            // 
            // tabPageLayout
            // 
            this.tabPageLayout.Controls.Add(this.groupBoxGeneralLayout);
            this.tabPageLayout.Controls.Add(this.groupBoxGeneralContent);
            this.tabPageLayout.Location = new System.Drawing.Point(4, 25);
            this.tabPageLayout.Name = "tabPageLayout";
            this.tabPageLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLayout.Size = new System.Drawing.Size(447, 235);
            this.tabPageLayout.TabIndex = 0;
            this.tabPageLayout.Text = "布局";
            this.tabPageLayout.UseVisualStyleBackColor = true;
            // 
            // groupBoxGeneralLayout
            // 
            this.groupBoxGeneralLayout.Controls.Add(this.cssGeneralLayoutControl);
            this.groupBoxGeneralLayout.Location = new System.Drawing.Point(4, 6);
            this.groupBoxGeneralLayout.Name = "groupBoxGeneralLayout";
            this.groupBoxGeneralLayout.Size = new System.Drawing.Size(215, 188);
            this.groupBoxGeneralLayout.TabIndex = 13;
            this.groupBoxGeneralLayout.TabStop = false;
            this.groupBoxGeneralLayout.Text = "布局选项";
            // 
            // cssGeneralLayoutControl
            // 
            this.cssGeneralLayoutControl.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.cssGeneralLayoutControl.Location = new System.Drawing.Point(6, 20);
            this.cssGeneralLayoutControl.Name = "cssGeneralLayoutControl";
            this.cssGeneralLayoutControl.Size = new System.Drawing.Size(203, 90);
            this.cssGeneralLayoutControl.TabIndex = 10;
            // 
            // groupBoxGeneralContent
            // 
            this.groupBoxGeneralContent.Controls.Add(this.cssGeneralContectControl);
            this.groupBoxGeneralContent.Location = new System.Drawing.Point(225, 6);
            this.groupBoxGeneralContent.Name = "groupBoxGeneralContent";
            this.groupBoxGeneralContent.Size = new System.Drawing.Size(216, 188);
            this.groupBoxGeneralContent.TabIndex = 14;
            this.groupBoxGeneralContent.TabStop = false;
            this.groupBoxGeneralContent.Text = "内容选项";
            // 
            // cssGeneralContectControl
            // 
            this.cssGeneralContectControl.Location = new System.Drawing.Point(6, 20);
            this.cssGeneralContectControl.Name = "cssGeneralContectControl";
            this.cssGeneralContectControl.Size = new System.Drawing.Size(204, 148);
            this.cssGeneralContectControl.TabIndex = 11;
            // 
            // tabControlGeneral
            // 
            this.tabControlGeneral.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlGeneral.Controls.Add(this.tabPageLayout);
            this.tabControlGeneral.Controls.Add(this.tabPageBorder);
            this.tabControlGeneral.Controls.Add(this.tabPageFont);
            this.tabControlGeneral.Controls.Add(this.tabPageBackGroud);
            this.tabControlGeneral.Controls.Add(this.tabPageProperty);
            this.tabControlGeneral.Location = new System.Drawing.Point(187, 33);
            this.tabControlGeneral.Name = "tabControlGeneral";
            this.tabControlGeneral.SelectedIndex = 0;
            this.tabControlGeneral.Size = new System.Drawing.Size(455, 264);
            this.tabControlGeneral.TabIndex = 16;
            // 
            // tabPageBorder
            // 
            this.tabPageBorder.Controls.Add(this.myBoxModelPanel);
            this.tabPageBorder.Controls.Add(this.borderPicBox);
            this.tabPageBorder.Location = new System.Drawing.Point(4, 25);
            this.tabPageBorder.Name = "tabPageBorder";
            this.tabPageBorder.Size = new System.Drawing.Size(447, 235);
            this.tabPageBorder.TabIndex = 0;
            this.tabPageBorder.Text = "边框";
            this.tabPageBorder.UseVisualStyleBackColor = true;
            // 
            // myBoxModelPanel
            // 
            this.myBoxModelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myBoxModelPanel.Location = new System.Drawing.Point(70, 0);
            this.myBoxModelPanel.Name = "myBoxModelPanel";
            this.myBoxModelPanel.Size = new System.Drawing.Size(377, 235);
            this.myBoxModelPanel.TabIndex = 2;
            // 
            // borderPicBox
            // 
            this.borderPicBox.BackColor = System.Drawing.SystemColors.Info;
            this.borderPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.borderPicBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderPicBox.Image = global::Jeelu.SimplusD.Client.Win.Resources.Resource.框_布局;
            this.borderPicBox.Location = new System.Drawing.Point(0, 0);
            this.borderPicBox.Name = "borderPicBox";
            this.borderPicBox.Size = new System.Drawing.Size(70, 235);
            this.borderPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.borderPicBox.TabIndex = 1;
            this.borderPicBox.TabStop = false;
            // 
            // tabPageFont
            // 
            this.tabPageFont.Controls.Add(this.myFont);
            this.tabPageFont.Location = new System.Drawing.Point(4, 25);
            this.tabPageFont.Name = "tabPageFont";
            this.tabPageFont.Size = new System.Drawing.Size(447, 235);
            this.tabPageFont.TabIndex = 1;
            this.tabPageFont.Text = "字体";
            this.tabPageFont.UseVisualStyleBackColor = true;
            // 
            // myFont
            // 
            this.myFont.CssSection = cssSection2;
            this.myFont.CurentFont = null;
            this.myFont.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myFont.IsBold = false;
            this.myFont.IsItalic = false;
            this.myFont.Location = new System.Drawing.Point(5, 7);
            this.myFont.Name = "myFont";
            this.myFont.Size = new System.Drawing.Size(298, 67);
            this.myFont.TabIndex = 0;
            // 
            // tabPageProperty
            // 
            this.tabPageProperty.Controls.Add(this.textBoxCurType);
            this.tabPageProperty.Controls.Add(this.labelCurType);
            this.tabPageProperty.Controls.Add(this.labelWidth);
            this.tabPageProperty.Controls.Add(this.textBoxTitle);
            this.tabPageProperty.Controls.Add(this.labelHeight);
            this.tabPageProperty.Controls.Add(this.labelTitle);
            this.tabPageProperty.Controls.Add(this.textBoxWidth);
            this.tabPageProperty.Controls.Add(this.textBoxHeight);
            this.tabPageProperty.Location = new System.Drawing.Point(4, 25);
            this.tabPageProperty.Name = "tabPageProperty";
            this.tabPageProperty.Size = new System.Drawing.Size(447, 235);
            this.tabPageProperty.TabIndex = 2;
            this.tabPageProperty.Text = "属性";
            this.tabPageProperty.UseVisualStyleBackColor = true;
            // 
            // textBoxCurType
            // 
            this.textBoxCurType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurType.Enabled = false;
            this.textBoxCurType.Location = new System.Drawing.Point(284, 8);
            this.textBoxCurType.Name = "textBoxCurType";
            this.textBoxCurType.Size = new System.Drawing.Size(105, 21);
            this.textBoxCurType.TabIndex = 15;
            this.textBoxCurType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCurType
            // 
            this.labelCurType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelCurType.AutoSize = true;
            this.labelCurType.Location = new System.Drawing.Point(199, 10);
            this.labelCurType.Name = "labelCurType";
            this.labelCurType.Size = new System.Drawing.Size(79, 13);
            this.labelCurType.TabIndex = 14;
            this.labelCurType.Text = "当前块类型：";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(15, 46);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(67, 13);
            this.labelWidth.TabIndex = 7;
            this.labelWidth.Text = "当前宽度：";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTitle.Location = new System.Drawing.Point(88, 8);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(105, 21);
            this.textBoxTitle.TabIndex = 13;
            this.textBoxTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(211, 46);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(67, 13);
            this.labelHeight.TabIndex = 8;
            this.labelHeight.Text = "当前高度：";
            // 
            // labelTitle
            // 
            this.labelTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(3, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(79, 13);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "当前块标题：";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxWidth.Enabled = false;
            this.textBoxWidth.Location = new System.Drawing.Point(88, 44);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(105, 21);
            this.textBoxWidth.TabIndex = 9;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxHeight.Enabled = false;
            this.textBoxHeight.Location = new System.Drawing.Point(284, 44);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(105, 21);
            this.textBoxHeight.TabIndex = 10;
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CssSetupForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(647, 347);
            this.Controls.Add(this.tabControlGeneral);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this.labelCurrentContent);
            this.Controls.Add(this.labelCSSType);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxCSSText);
            this.Controls.Add(this.checkBoxDisplayCSSText);
            this.Controls.Add(this.treeCSSType);
            this.Name = "CssSetupForm";
            this.Text = "CssSetupForm";
            this.Load += new System.EventHandler(this.CssSetupForm_Load);
            this.tabPageBackGroud.ResumeLayout(false);
            this.tabPageBackGroud.PerformLayout();
            this.groupBoxLinkOption.ResumeLayout(false);
            this.groupBoxLinkOption.PerformLayout();
            this.tabPageLayout.ResumeLayout(false);
            this.groupBoxGeneralLayout.ResumeLayout(false);
            this.groupBoxGeneralContent.ResumeLayout(false);
            this.tabControlGeneral.ResumeLayout(false);
            this.tabPageBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.borderPicBox)).EndInit();
            this.tabPageFont.ResumeLayout(false);
            this.tabPageProperty.ResumeLayout(false);
            this.tabPageProperty.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeCSSType;
        private System.Windows.Forms.CheckBox checkBoxDisplayCSSText;
        private System.Windows.Forms.TextBox textBoxCSSText;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelCSSType;
        private System.Windows.Forms.Label labelCurrentContent;
        private SingleLine singleLine1;
        private System.Windows.Forms.TabPage tabPageBackGroud;
        private System.Windows.Forms.ComboBox comboBoxBackgroudLayout;
        private System.Windows.Forms.Label labelBackLayout;
        private System.Windows.Forms.TextBox textBoxBackColor;
        private ColorGeneralButton btnSelectBackColor;
        private SingleLine singleLine2;
        private System.Windows.Forms.TabPage tabPageLayout;
        private System.Windows.Forms.GroupBox groupBoxGeneralLayout;
        private CSSGeneralLayoutControl cssGeneralLayoutControl;
        private System.Windows.Forms.GroupBox groupBoxGeneralContent;
        private CSSGeneralContectControl cssGeneralContectControl;
        private System.Windows.Forms.TabControl tabControlGeneral;
        private System.Windows.Forms.CheckBox checkBoxUsingBackImg;
        private System.Windows.Forms.CheckBox checkBoxUsingBackColor;
        private System.Windows.Forms.TabPage tabPageProperty;
        private System.Windows.Forms.TextBox textBoxCurType;
        private System.Windows.Forms.Label labelCurType;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.CheckBox checkBoxUsingCurImg;
        private System.Windows.Forms.Button btnBrowsePic;
        private System.Windows.Forms.GroupBox groupBoxLinkOption;
        private System.Windows.Forms.Label labelColorBefore;
        private System.Windows.Forms.TextBox textBoxBeforeColor;
        private ColorGeneralButton colorBtnBefore;
        private System.Windows.Forms.CheckBox checkBoxUnderLine;
        private System.Windows.Forms.Label labelColorLast;
        private System.Windows.Forms.TextBox textBoxLastColor;
        private ColorGeneralButton colorBtnLast;
        private System.Windows.Forms.CheckBox checkBoxImgBorder;
        private System.Windows.Forms.TabPage tabPageBorder;
        private BoxModelPanel myBoxModelPanel;
        private System.Windows.Forms.PictureBox borderPicBox;
        private System.Windows.Forms.TabPage tabPageFont;
        private CssMyFont myFont;
        private SingleLine singleLine3;
        private System.Windows.Forms.CheckBox checkBoxIsAutoSize;
    }
}