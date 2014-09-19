namespace Jeelu.Win
{
    partial class BackgroundCssControl
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
            this.lblBgColor = new System.Windows.Forms.Label();
            this.lblBgImage = new System.Windows.Forms.Label();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.lblLevelPostion = new System.Windows.Forms.Label();
            this.lblVerticalPostion = new System.Windows.Forms.Label();
            this.cbxBkImage = new System.Windows.Forms.ComboBox();
            this.cbxRepeat = new System.Windows.Forms.ComboBox();
            this.cbxAttachment = new System.Windows.Forms.ComboBox();
            this.ColorBtnBackground = new Jeelu.Win.ColorTextBoxButton();
            this.cssFieldUnitLevel = new Jeelu.Win.CssFieldUnit();
            this.cssFieldUnitVertical = new Jeelu.Win.CssFieldUnit();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBgColor
            // 
            this.lblBgColor.AutoSize = true;
            this.lblBgColor.Location = new System.Drawing.Point(36, 21);
            this.lblBgColor.Name = "lblBgColor";
            this.lblBgColor.Size = new System.Drawing.Size(77, 12);
            this.lblBgColor.TabIndex = 0;
            this.lblBgColor.Text = "背景颜色(&C):";
            // 
            // lblBgImage
            // 
            this.lblBgImage.AutoSize = true;
            this.lblBgImage.Location = new System.Drawing.Point(36, 47);
            this.lblBgImage.Name = "lblBgImage";
            this.lblBgImage.Size = new System.Drawing.Size(77, 12);
            this.lblBgImage.TabIndex = 1;
            this.lblBgImage.Text = "背景图像(&I):";
            // 
            // lblRepeat
            // 
            this.lblRepeat.AutoSize = true;
            this.lblRepeat.Location = new System.Drawing.Point(60, 75);
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(53, 12);
            this.lblRepeat.TabIndex = 2;
            this.lblRepeat.Text = "重复(&R):";
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Location = new System.Drawing.Point(60, 102);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(53, 12);
            this.lblAttachment.TabIndex = 3;
            this.lblAttachment.Text = "附件(&T):";
            // 
            // lblLevelPostion
            // 
            this.lblLevelPostion.AutoSize = true;
            this.lblLevelPostion.Location = new System.Drawing.Point(36, 130);
            this.lblLevelPostion.Name = "lblLevelPostion";
            this.lblLevelPostion.Size = new System.Drawing.Size(77, 12);
            this.lblLevelPostion.TabIndex = 4;
            this.lblLevelPostion.Text = "水平位置(&Z):";
            // 
            // lblVerticalPostion
            // 
            this.lblVerticalPostion.AutoSize = true;
            this.lblVerticalPostion.Location = new System.Drawing.Point(36, 159);
            this.lblVerticalPostion.Name = "lblVerticalPostion";
            this.lblVerticalPostion.Size = new System.Drawing.Size(77, 12);
            this.lblVerticalPostion.TabIndex = 5;
            this.lblVerticalPostion.Text = "垂直位置(&V):";
            // 
            // cbxBkImage
            // 
            this.cbxBkImage.FormattingEnabled = true;
            this.cbxBkImage.Location = new System.Drawing.Point(119, 44);
            this.cbxBkImage.Name = "cbxBkImage";
            this.cbxBkImage.Size = new System.Drawing.Size(179, 20);
            this.cbxBkImage.TabIndex = 6;
            // 
            // cbxRepeat
            // 
            this.cbxRepeat.FormattingEnabled = true;
            this.cbxRepeat.Location = new System.Drawing.Point(119, 70);
            this.cbxRepeat.Name = "cbxRepeat";
            this.cbxRepeat.Size = new System.Drawing.Size(82, 20);
            this.cbxRepeat.TabIndex = 7;
            // 
            // cbxAttachment
            // 
            this.cbxAttachment.FormattingEnabled = true;
            this.cbxAttachment.Location = new System.Drawing.Point(119, 96);
            this.cbxAttachment.Name = "cbxAttachment";
            this.cbxAttachment.Size = new System.Drawing.Size(82, 20);
            this.cbxAttachment.TabIndex = 8;
            // 
            // ColorBtnBackground
            // 
            this.ColorBtnBackground.CheckColor = true;
            this.ColorBtnBackground.Color = System.Drawing.Color.Empty;
            this.ColorBtnBackground.ColorText = "";
            this.ColorBtnBackground.Location = new System.Drawing.Point(119, 17);
            this.ColorBtnBackground.Name = "ColorBtnBackground";
            this.ColorBtnBackground.Size = new System.Drawing.Size(90, 22);
            this.ColorBtnBackground.TabIndex = 10;
            this.ColorBtnBackground.ColorTextChanged += new System.EventHandler(this.ColorBtnBackground_ColorTextChanged);
            // 
            // cssFieldUnitLevel
            // 
            this.cssFieldUnitLevel.FieldUnitType = Jeelu.Win.CssFieldUnitType.LevelPosition;
            this.cssFieldUnitLevel.Location = new System.Drawing.Point(116, 123);
            this.cssFieldUnitLevel.Name = "cssFieldUnitLevel";
            this.cssFieldUnitLevel.Size = new System.Drawing.Size(153, 23);
            this.cssFieldUnitLevel.TabIndex = 11;
            this.cssFieldUnitLevel.Value = null;
            // 
            // cssFieldUnitVertical
            // 
            this.cssFieldUnitVertical.FieldUnitType = Jeelu.Win.CssFieldUnitType.VerticalPosition;
            this.cssFieldUnitVertical.Location = new System.Drawing.Point(116, 151);
            this.cssFieldUnitVertical.Name = "cssFieldUnitVertical";
            this.cssFieldUnitVertical.Size = new System.Drawing.Size(153, 23);
            this.cssFieldUnitVertical.TabIndex = 12;
            this.cssFieldUnitVertical.Value = null;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(304, 42);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // BackgroundCssControl
            // 
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cssFieldUnitVertical);
            this.Controls.Add(this.cssFieldUnitLevel);
            this.Controls.Add(this.ColorBtnBackground);
            this.Controls.Add(this.cbxAttachment);
            this.Controls.Add(this.cbxRepeat);
            this.Controls.Add(this.cbxBkImage);
            this.Controls.Add(this.lblVerticalPostion);
            this.Controls.Add(this.lblLevelPostion);
            this.Controls.Add(this.lblAttachment);
            this.Controls.Add(this.lblRepeat);
            this.Controls.Add(this.lblBgImage);
            this.Controls.Add(this.lblBgColor);
            this.Name = "BackgroundCssControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBgColor;
        private System.Windows.Forms.Label lblBgImage;
        private System.Windows.Forms.Label lblRepeat;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.Label lblLevelPostion;
        private System.Windows.Forms.Label lblVerticalPostion;
        private System.Windows.Forms.ComboBox cbxBkImage;
        private System.Windows.Forms.ComboBox cbxRepeat;
        private System.Windows.Forms.ComboBox cbxAttachment;
        private ColorTextBoxButton ColorBtnBackground;
        private CssFieldUnit cssFieldUnitLevel;
        private CssFieldUnit cssFieldUnitVertical;
        private System.Windows.Forms.Button btnBrowse;
    }
}
