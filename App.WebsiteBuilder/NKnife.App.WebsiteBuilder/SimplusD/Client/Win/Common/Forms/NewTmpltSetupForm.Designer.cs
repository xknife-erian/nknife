using System.Drawing;
using Jeelu.Win;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    partial class NewTmpltSetupForm
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
            this.labTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxWidth = new Jeelu.Win.ValidateTextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.textBoxHeight = new Jeelu.Win.ValidateTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelImage = new System.Windows.Forms.Panel();
            this.panelGraghics = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnSaveAndOpen = new System.Windows.Forms.Button();
            this.openNemImage = new Jeelu.Win.FileSelecterControl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxChooseImage = new System.Windows.Forms.CheckBox();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.panelImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(10, 376);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(85, 13);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "Template Name:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(108, 373);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(596, 21);
            this.textBoxTitle.TabIndex = 1;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(77, 432);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(39, 13);
            this.labelWidth.TabIndex = 2;
            this.labelWidth.Text = "Width:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Enabled = false;
            this.textBoxWidth.Location = new System.Drawing.Point(108, 429);
            this.textBoxWidth.MaxLength = 4;
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.RegexText = "^[0-9]*$";
            this.textBoxWidth.RegexTextRuntime = "^[0-9]*$";
            this.textBoxWidth.Size = new System.Drawing.Size(60, 21);
            this.textBoxWidth.TabIndex = 3;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(204, 432);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(42, 13);
            this.labelHeight.TabIndex = 4;
            this.labelHeight.Text = "Height:";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Enabled = false;
            this.textBoxHeight.Location = new System.Drawing.Point(237, 429);
            this.textBoxHeight.MaxLength = 4;
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.RegexText = "^[0-9]*$";
            this.textBoxHeight.RegexTextRuntime = "^[0-9]*$";
            this.textBoxHeight.Size = new System.Drawing.Size(60, 21);
            this.textBoxHeight.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "px";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "px";
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.panelGraghics);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelImage.Location = new System.Drawing.Point(10, 10);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(694, 355);
            this.panelImage.TabIndex = 9;
            // 
            // panelGraghics
            // 
            this.panelGraghics.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGraghics.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panelGraghics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraghics.Location = new System.Drawing.Point(0, 0);
            this.panelGraghics.Name = "panelGraghics";
            this.panelGraghics.Size = new System.Drawing.Size(694, 355);
            this.panelGraghics.TabIndex = 1;
            this.panelGraghics.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGraghics_Paint);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "图像文件| *.bmp;*.jpg;*gif;*jpeg;*png;*.emf;*.wmf;*.ico;*.tiff;*.exif";
            // 
            // btnSaveAndOpen
            // 
            this.btnSaveAndOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndOpen.Location = new System.Drawing.Point(521, 464);
            this.btnSaveAndOpen.Name = "btnSaveAndOpen";
            this.btnSaveAndOpen.Size = new System.Drawing.Size(90, 30);
            this.btnSaveAndOpen.TabIndex = 12;
            this.btnSaveAndOpen.Text = "SaveAndOpen";
            this.btnSaveAndOpen.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // openNemImage
            // 
            this.openNemImage.DialogTitle = "Open Files";
            this.openNemImage.FileName = "";
            this.openNemImage.FileNames = null;
            this.openNemImage.FileSelectFilter = Jeelu.Win.FileSelectFilter.Image;
            this.openNemImage.FilesHistory = null;
            this.openNemImage.InitialDirectory = null;
            this.openNemImage.Location = new System.Drawing.Point(108, 400);
            this.openNemImage.MultiSelect = false;
            this.openNemImage.Name = "openNemImage";
            this.openNemImage.ReadOnly = false;
            this.openNemImage.SelectFolder = false;
            this.openNemImage.Size = new System.Drawing.Size(596, 23);
            this.openNemImage.Style = Jeelu.Win.FileSelectControlStyle.TextBoxAndImageButton;
            this.openNemImage.TabIndex = 13;
            this.openNemImage.Word = "浏览...";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(614, 464);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxChooseImage
            // 
            this.checkBoxChooseImage.AutoSize = true;
            this.checkBoxChooseImage.Checked = true;
            this.checkBoxChooseImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChooseImage.Location = new System.Drawing.Point(10, 404);
            this.checkBoxChooseImage.Name = "checkBoxChooseImage";
            this.checkBoxChooseImage.Size = new System.Drawing.Size(112, 17);
            this.checkBoxChooseImage.TabIndex = 17;
            this.checkBoxChooseImage.Text = "BackgroundImage";
            this.checkBoxChooseImage.UseVisualStyleBackColor = true;
            this.checkBoxChooseImage.CheckedChanged += new System.EventHandler(this.checkBoxChooseImage_CheckedChanged);
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(11, 457);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(693, 1);
            this.singleLine1.TabIndex = 18;
            this.singleLine1.TabStop = false;
            // 
            // NewTmpltSetupForm
            // 
            this.AcceptButton = this.btnSaveAndOpen;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(714, 507);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.openNemImage);
            this.Controls.Add(this.btnSaveAndOpen);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxChooseImage);
            this.HelpButton = false;
            this.Name = "NewTmpltSetupForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "新建模板";
            this.Load += new System.EventHandler(this.NewTmpltSetupForm_Load);
            this.panelImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTitle;
        private ValidateTextBox textBoxWidth;
        private ValidateTextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Panel panelGraghics;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnSaveAndOpen;
        private FileSelecterControl openNemImage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxChooseImage;
        private SingleLine singleLine1;
    }
}