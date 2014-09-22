namespace Jeelu.SimplusD.Client.Win
{
    partial class FontListEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontListEditor));
            this.fontListBox = new System.Windows.Forms.ListBox();
            this.checkedFontListbox = new System.Windows.Forms.ListBox();
            this.addRowbtn = new System.Windows.Forms.Button();
            this.deleteRowBtn = new System.Windows.Forms.Button();
            this.upMoveBtn = new System.Windows.Forms.Button();
            this.downMoveBtn = new System.Windows.Forms.Button();
            this.leftMoveBtn = new System.Windows.Forms.Button();
            this.rightMoveBtn = new System.Windows.Forms.Button();
            this.txtCheckedFont = new System.Windows.Forms.TextBox();
            this.allFontListBox = new System.Windows.Forms.ListBox();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFontList = new System.Windows.Forms.Label();
            this.lblCheckedFont = new System.Windows.Forms.Label();
            this.lblEnabledFont = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // fontListBox
            // 
            this.fontListBox.FormattingEnabled = true;
            this.fontListBox.Location = new System.Drawing.Point(12, 44);
            this.fontListBox.Name = "fontListBox";
            this.fontListBox.Size = new System.Drawing.Size(332, 108);
            this.fontListBox.TabIndex = 0;
            this.fontListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fontListBox_MouseClick);
            this.fontListBox.SelectedIndexChanged += new System.EventHandler(this.fontListBox_SelectedIndexChanged);
            this.fontListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fontListBox_KeyDown);
            // 
            // checkedFontListbox
            // 
            this.checkedFontListbox.FormattingEnabled = true;
            this.checkedFontListbox.Location = new System.Drawing.Point(12, 187);
            this.checkedFontListbox.Name = "checkedFontListbox";
            this.checkedFontListbox.Size = new System.Drawing.Size(139, 134);
            this.checkedFontListbox.TabIndex = 1;
            this.checkedFontListbox.SelectedIndexChanged += new System.EventHandler(this.checkedFontListbox_SelectedIndexChanged);
            // 
            // addRowbtn
            // 
            this.addRowbtn.Location = new System.Drawing.Point(12, 13);
            this.addRowbtn.Name = "addRowbtn";
            this.addRowbtn.Size = new System.Drawing.Size(25, 25);
            this.addRowbtn.TabIndex = 2;
            this.addRowbtn.UseVisualStyleBackColor = true;
            this.addRowbtn.Click += new System.EventHandler(this.addRowbtn_Click);
            // 
            // deleteRowBtn
            // 
            this.deleteRowBtn.Location = new System.Drawing.Point(43, 13);
            this.deleteRowBtn.Name = "deleteRowBtn";
            this.deleteRowBtn.Size = new System.Drawing.Size(25, 25);
            this.deleteRowBtn.TabIndex = 2;
            this.deleteRowBtn.UseVisualStyleBackColor = true;
            this.deleteRowBtn.Click += new System.EventHandler(this.deleteRowBtn_Click);
            // 
            // upMoveBtn
            // 
            this.upMoveBtn.Location = new System.Drawing.Point(288, 13);
            this.upMoveBtn.Name = "upMoveBtn";
            this.upMoveBtn.Size = new System.Drawing.Size(25, 25);
            this.upMoveBtn.TabIndex = 2;
            this.upMoveBtn.UseVisualStyleBackColor = true;
            this.upMoveBtn.Click += new System.EventHandler(this.upMoveBtn_Click);
            // 
            // downMoveBtn
            // 
            this.downMoveBtn.Location = new System.Drawing.Point(319, 13);
            this.downMoveBtn.Name = "downMoveBtn";
            this.downMoveBtn.Size = new System.Drawing.Size(25, 25);
            this.downMoveBtn.TabIndex = 2;
            this.downMoveBtn.UseVisualStyleBackColor = true;
            this.downMoveBtn.Click += new System.EventHandler(this.downMoveBtn_Click);
            // 
            // leftMoveBtn
            // 
            this.leftMoveBtn.Location = new System.Drawing.Point(161, 206);
            this.leftMoveBtn.Name = "leftMoveBtn";
            this.leftMoveBtn.Size = new System.Drawing.Size(35, 25);
            this.leftMoveBtn.TabIndex = 3;
            this.leftMoveBtn.UseVisualStyleBackColor = true;
            this.leftMoveBtn.Click += new System.EventHandler(this.leftMoveBtn_Click);
            // 
            // rightMoveBtn
            // 
            this.rightMoveBtn.Location = new System.Drawing.Point(161, 237);
            this.rightMoveBtn.Name = "rightMoveBtn";
            this.rightMoveBtn.Size = new System.Drawing.Size(35, 25);
            this.rightMoveBtn.TabIndex = 3;
            this.rightMoveBtn.UseVisualStyleBackColor = true;
            this.rightMoveBtn.Click += new System.EventHandler(this.rightMoveBtn_Click);
            // 
            // txtCheckedFont
            // 
            this.txtCheckedFont.Location = new System.Drawing.Point(205, 300);
            this.txtCheckedFont.Name = "txtCheckedFont";
            this.txtCheckedFont.Size = new System.Drawing.Size(139, 21);
            this.txtCheckedFont.TabIndex = 4;
            this.txtCheckedFont.TextChanged += new System.EventHandler(this.txtCheckedFont_TextChanged);
            // 
            // allFontListBox
            // 
            this.allFontListBox.FormattingEnabled = true;
            this.allFontListBox.Location = new System.Drawing.Point(205, 187);
            this.allFontListBox.Name = "allFontListBox";
            this.allFontListBox.Size = new System.Drawing.Size(139, 108);
            this.allFontListBox.TabIndex = 1;
            this.allFontListBox.SelectedIndexChanged += new System.EventHandler(this.allFontListBox_SelectedIndexChanged);
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(360, 14);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.singleLine1.Size = new System.Drawing.Size(1, 307);
            this.singleLine1.TabIndex = 5;
            this.singleLine1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(372, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFontList
            // 
            this.lblFontList.AutoSize = true;
            this.lblFontList.Location = new System.Drawing.Point(147, 19);
            this.lblFontList.Name = "lblFontList";
            this.lblFontList.Size = new System.Drawing.Size(49, 13);
            this.lblFontList.TabIndex = 8;
            this.lblFontList.Text = "FontList:";
            // 
            // lblCheckedFont
            // 
            this.lblCheckedFont.AutoSize = true;
            this.lblCheckedFont.Location = new System.Drawing.Point(9, 171);
            this.lblCheckedFont.Name = "lblCheckedFont";
            this.lblCheckedFont.Size = new System.Drawing.Size(82, 13);
            this.lblCheckedFont.TabIndex = 8;
            this.lblCheckedFont.Text = "CheckedFont：";
            // 
            // lblEnabledFont
            // 
            this.lblEnabledFont.AutoSize = true;
            this.lblEnabledFont.Location = new System.Drawing.Point(202, 171);
            this.lblEnabledFont.Name = "lblEnabledFont";
            this.lblEnabledFont.Size = new System.Drawing.Size(79, 13);
            this.lblEnabledFont.TabIndex = 8;
            this.lblEnabledFont.Text = "EnabledFont：";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "downArrow.ico");
            this.imageList.Images.SetKeyName(1, "leftArrow.ico");
            this.imageList.Images.SetKeyName(2, "delete.ico");
            this.imageList.Images.SetKeyName(3, "new.ico");
            this.imageList.Images.SetKeyName(4, "rightArrow.ico");
            this.imageList.Images.SetKeyName(5, "upArrow.ico");
            // 
            // FontListEditor
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(466, 337);
            this.Controls.Add(this.lblEnabledFont);
            this.Controls.Add(this.lblCheckedFont);
            this.Controls.Add(this.lblFontList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this.txtCheckedFont);
            this.Controls.Add(this.rightMoveBtn);
            this.Controls.Add(this.leftMoveBtn);
            this.Controls.Add(this.downMoveBtn);
            this.Controls.Add(this.upMoveBtn);
            this.Controls.Add(this.deleteRowBtn);
            this.Controls.Add(this.addRowbtn);
            this.Controls.Add(this.allFontListBox);
            this.Controls.Add(this.checkedFontListbox);
            this.Controls.Add(this.fontListBox);
            this.Name = "FontListEditor";
            this.Text = "FontListEditorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox fontListBox;
        private System.Windows.Forms.ListBox checkedFontListbox;
        private System.Windows.Forms.Button addRowbtn;
        private System.Windows.Forms.Button deleteRowBtn;
        private System.Windows.Forms.Button upMoveBtn;
        private System.Windows.Forms.Button downMoveBtn;
        private System.Windows.Forms.Button leftMoveBtn;
        private System.Windows.Forms.Button rightMoveBtn;
        private System.Windows.Forms.TextBox txtCheckedFont;
        private System.Windows.Forms.ListBox allFontListBox;
        private Jeelu.Win.SingleLine singleLine1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFontList;
        private System.Windows.Forms.Label lblCheckedFont;
        private System.Windows.Forms.Label lblEnabledFont;
        private System.Windows.Forms.ImageList imageList;
    }
}