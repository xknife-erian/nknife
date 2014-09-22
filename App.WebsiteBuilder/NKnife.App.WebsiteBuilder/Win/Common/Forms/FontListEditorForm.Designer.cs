namespace Jeelu.Win
{
    partial class FontListEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontListEditorForm));
            this.lblEnabledFont = new System.Windows.Forms.Label();
            this.lblCheckedFont = new System.Windows.Forms.Label();
            this.lblFontList = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCheckedFont = new System.Windows.Forms.TextBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.rightMoveBtn = new System.Windows.Forms.Button();
            this.leftMoveBtn = new System.Windows.Forms.Button();
            this.downMoveBtn = new System.Windows.Forms.Button();
            this.upMoveBtn = new System.Windows.Forms.Button();
            this.deleteRowBtn = new System.Windows.Forms.Button();
            this.addRowbtn = new System.Windows.Forms.Button();
            this.allFontListBox = new System.Windows.Forms.ListBox();
            this.checkedFontListbox = new System.Windows.Forms.ListBox();
            this.fontListBox = new System.Windows.Forms.ListBox();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.SuspendLayout();
            // 
            // lblEnabledFont
            // 
            this.lblEnabledFont.AutoSize = true;
            this.lblEnabledFont.Location = new System.Drawing.Point(206, 171);
            this.lblEnabledFont.Name = "lblEnabledFont";
            this.lblEnabledFont.Size = new System.Drawing.Size(59, 13);
            this.lblEnabledFont.TabIndex = 22;
            this.lblEnabledFont.Text = "可用字体:";
            // 
            // lblCheckedFont
            // 
            this.lblCheckedFont.AutoSize = true;
            this.lblCheckedFont.Location = new System.Drawing.Point(14, 171);
            this.lblCheckedFont.Name = "lblCheckedFont";
            this.lblCheckedFont.Size = new System.Drawing.Size(59, 13);
            this.lblCheckedFont.TabIndex = 23;
            this.lblCheckedFont.Text = "选择字体:";
            // 
            // lblFontList
            // 
            this.lblFontList.AutoSize = true;
            this.lblFontList.Location = new System.Drawing.Point(151, 20);
            this.lblFontList.Name = "lblFontList";
            this.lblFontList.Size = new System.Drawing.Size(71, 13);
            this.lblFontList.TabIndex = 24;
            this.lblFontList.Text = "字体族列表:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 45);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(376, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCheckedFont
            // 
            this.txtCheckedFont.Location = new System.Drawing.Point(209, 311);
            this.txtCheckedFont.Name = "txtCheckedFont";
            this.txtCheckedFont.Size = new System.Drawing.Size(139, 21);
            this.txtCheckedFont.TabIndex = 18;
            this.txtCheckedFont.TextChanged += new System.EventHandler(this.txtCheckedFont_TextChanged);
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
            // rightMoveBtn
            // 
            this.rightMoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("rightMoveBtn.Image")));
            this.rightMoveBtn.Location = new System.Drawing.Point(165, 242);
            this.rightMoveBtn.Name = "rightMoveBtn";
            this.rightMoveBtn.Size = new System.Drawing.Size(35, 27);
            this.rightMoveBtn.TabIndex = 17;
            this.rightMoveBtn.UseVisualStyleBackColor = true;
            this.rightMoveBtn.Click += new System.EventHandler(this.rightMoveBtn_Click);
            // 
            // leftMoveBtn
            // 
            this.leftMoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("leftMoveBtn.Image")));
            this.leftMoveBtn.Location = new System.Drawing.Point(165, 209);
            this.leftMoveBtn.Name = "leftMoveBtn";
            this.leftMoveBtn.Size = new System.Drawing.Size(35, 27);
            this.leftMoveBtn.TabIndex = 16;
            this.leftMoveBtn.UseVisualStyleBackColor = true;
            this.leftMoveBtn.Click += new System.EventHandler(this.leftMoveBtn_Click);
            // 
            // downMoveBtn
            // 
            this.downMoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("downMoveBtn.Image")));
            this.downMoveBtn.Location = new System.Drawing.Point(323, 14);
            this.downMoveBtn.Name = "downMoveBtn";
            this.downMoveBtn.Size = new System.Drawing.Size(25, 27);
            this.downMoveBtn.TabIndex = 12;
            this.downMoveBtn.UseVisualStyleBackColor = true;
            this.downMoveBtn.Click += new System.EventHandler(this.downMoveBtn_Click);
            // 
            // upMoveBtn
            // 
            this.upMoveBtn.Image = ((System.Drawing.Image)(resources.GetObject("upMoveBtn.Image")));
            this.upMoveBtn.Location = new System.Drawing.Point(292, 14);
            this.upMoveBtn.Name = "upMoveBtn";
            this.upMoveBtn.Size = new System.Drawing.Size(25, 27);
            this.upMoveBtn.TabIndex = 13;
            this.upMoveBtn.UseVisualStyleBackColor = true;
            this.upMoveBtn.Click += new System.EventHandler(this.upMoveBtn_Click);
            // 
            // deleteRowBtn
            // 
            this.deleteRowBtn.Image = ((System.Drawing.Image)(resources.GetObject("deleteRowBtn.Image")));
            this.deleteRowBtn.Location = new System.Drawing.Point(47, 14);
            this.deleteRowBtn.Name = "deleteRowBtn";
            this.deleteRowBtn.Size = new System.Drawing.Size(25, 27);
            this.deleteRowBtn.TabIndex = 15;
            this.deleteRowBtn.UseVisualStyleBackColor = true;
            this.deleteRowBtn.Click += new System.EventHandler(this.deleteRowBtn_Click);
            // 
            // addRowbtn
            // 
            this.addRowbtn.Image = ((System.Drawing.Image)(resources.GetObject("addRowbtn.Image")));
            this.addRowbtn.Location = new System.Drawing.Point(16, 14);
            this.addRowbtn.Name = "addRowbtn";
            this.addRowbtn.Size = new System.Drawing.Size(25, 27);
            this.addRowbtn.TabIndex = 14;
            this.addRowbtn.UseVisualStyleBackColor = true;
            this.addRowbtn.Click += new System.EventHandler(this.addRowbtn_Click);
            // 
            // allFontListBox
            // 
            this.allFontListBox.FormattingEnabled = true;
            this.allFontListBox.Location = new System.Drawing.Point(209, 188);
            this.allFontListBox.Name = "allFontListBox";
            this.allFontListBox.Size = new System.Drawing.Size(139, 108);
            this.allFontListBox.TabIndex = 10;
            this.allFontListBox.SelectedIndexChanged += new System.EventHandler(this.allFontListBox_SelectedIndexChanged);
            this.allFontListBox.DoubleClick += new System.EventHandler(this.allFontListBox_DoubleClick);
            // 
            // checkedFontListbox
            // 
            this.checkedFontListbox.FormattingEnabled = true;
            this.checkedFontListbox.Location = new System.Drawing.Point(16, 187);
            this.checkedFontListbox.Name = "checkedFontListbox";
            this.checkedFontListbox.Size = new System.Drawing.Size(139, 147);
            this.checkedFontListbox.TabIndex = 11;
            this.checkedFontListbox.SelectedIndexChanged += new System.EventHandler(this.checkedFontListbox_SelectedIndexChanged);
            this.checkedFontListbox.DoubleClick += new System.EventHandler(this.checkedFontListbox_DoubleClick);
            // 
            // fontListBox
            // 
            this.fontListBox.FormattingEnabled = true;
            this.fontListBox.Location = new System.Drawing.Point(16, 47);
            this.fontListBox.Name = "fontListBox";
            this.fontListBox.Size = new System.Drawing.Size(332, 108);
            this.fontListBox.TabIndex = 9;
            this.fontListBox.SelectedIndexChanged += new System.EventHandler(this.fontListBox_SelectedIndexChanged);
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(364, 12);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.singleLine1.Size = new System.Drawing.Size(1, 332);
            this.singleLine1.TabIndex = 19;
            this.singleLine1.TabStop = false;
            // 
            // FontListEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(464, 363);
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
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontListEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FontListEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnabledFont;
        private System.Windows.Forms.Label lblCheckedFont;
        private System.Windows.Forms.Label lblFontList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private SingleLine singleLine1;
        private System.Windows.Forms.TextBox txtCheckedFont;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button rightMoveBtn;
        private System.Windows.Forms.Button downMoveBtn;
        private System.Windows.Forms.Button upMoveBtn;
        private System.Windows.Forms.Button deleteRowBtn;
        private System.Windows.Forms.Button addRowbtn;
        private System.Windows.Forms.ListBox allFontListBox;
        private System.Windows.Forms.ListBox checkedFontListbox;
        private System.Windows.Forms.ListBox fontListBox;
        private System.Windows.Forms.Button leftMoveBtn;
    }
}