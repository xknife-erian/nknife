using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class SelectFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFileForm));
            this.listView = new ListViewEx();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.FilterComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectFileToolStrip = new System.Windows.Forms.ToolStrip();
            this.backToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.preToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.upToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.newFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ImpResourcesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ImgpictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.selectFileToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.CanLoad = false;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.HideSelection = false;
            this.listView.LabelEdit = true;
            this.listView.Location = new System.Drawing.Point(3, 29);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(510, 253);
            this.listView.TabIndex = 1;
            this.listView.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.listView.ThumbNailSize = 95;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(411, 7);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "打开";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(411, 34);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件类型:";
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(81, 9);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(304, 21);
            this.PathTextBox.TabIndex = 1;
            // 
            // FilterComboBox
            // 
            this.FilterComboBox.FormattingEnabled = true;
            this.FilterComboBox.Location = new System.Drawing.Point(81, 37);
            this.FilterComboBox.Name = "FilterComboBox";
            this.FilterComboBox.Size = new System.Drawing.Size(304, 20);
            this.FilterComboBox.TabIndex = 3;
            this.FilterComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.selectFileToolStrip, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.40171F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.59829F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 360);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.OKBtn);
            this.panel1.Controls.Add(this.FilterComboBox);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Controls.Add(this.PathTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 69);
            this.panel1.TabIndex = 2;
            // 
            // selectFileToolStrip
            // 
            this.selectFileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripButton,
            this.preToolStripButton,
            this.upToolStripButton,
            this.newFolderToolStripButton,
            this.toolStripSeparator1,
            this.ImpResourcesToolStripButton});
            this.selectFileToolStrip.Location = new System.Drawing.Point(0, 0);
            this.selectFileToolStrip.Name = "selectFileToolStrip";
            this.selectFileToolStrip.Size = new System.Drawing.Size(516, 25);
            this.selectFileToolStrip.TabIndex = 0;
            this.selectFileToolStrip.Text = "toolStrip1";
            // 
            // backToolStripButton
            // 
            this.backToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backToolStripButton.Enabled = false;
            this.backToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("backToolStripButton.Image")));
            this.backToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backToolStripButton.Name = "backToolStripButton";
            this.backToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.backToolStripButton.Text = "后退";
            this.backToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // preToolStripButton
            // 
            this.preToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.preToolStripButton.Enabled = false;
            this.preToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("preToolStripButton.Image")));
            this.preToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.preToolStripButton.Name = "preToolStripButton";
            this.preToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.preToolStripButton.Text = "前进";
            this.preToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // upToolStripButton
            // 
            this.upToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.upToolStripButton.Enabled = false;
            this.upToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("upToolStripButton.Image")));
            this.upToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upToolStripButton.Name = "upToolStripButton";
            this.upToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.upToolStripButton.Text = "上翻";
            this.upToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // newFolderToolStripButton
            // 
            this.newFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newFolderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newFolderToolStripButton.Image")));
            this.newFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFolderToolStripButton.Name = "newFolderToolStripButton";
            this.newFolderToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newFolderToolStripButton.Text = "新建文件夹";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ImpResourcesToolStripButton
            // 
            this.ImpResourcesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ImpResourcesToolStripButton.Image")));
            this.ImpResourcesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImpResourcesToolStripButton.Name = "ImpResourcesToolStripButton";
            this.ImpResourcesToolStripButton.Size = new System.Drawing.Size(97, 22);
            this.ImpResourcesToolStripButton.Text = "导入资源文件";
            // 
            // ImgpictureBox
            // 
            this.ImgpictureBox.Location = new System.Drawing.Point(522, 110);
            this.ImgpictureBox.Name = "ImgpictureBox";
            this.ImgpictureBox.Size = new System.Drawing.Size(112, 97);
            this.ImgpictureBox.TabIndex = 25;
            this.ImgpictureBox.TabStop = false;
            // 
            // SelectFileForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(640, 364);
            this.Controls.Add(this.ImgpictureBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectFileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择文件";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.selectFileToolStrip.ResumeLayout(false);
            this.selectFileToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgpictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.ComboBox FilterComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip selectFileToolStrip;
        private System.Windows.Forms.ToolStripButton backToolStripButton;
        private System.Windows.Forms.ToolStripButton preToolStripButton;
        private System.Windows.Forms.ToolStripButton upToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ImpResourcesToolStripButton;
        private System.Windows.Forms.ToolStripButton newFolderToolStripButton;
        private System.Windows.Forms.PictureBox ImgpictureBox;
        private ListViewEx listView;
    }
}