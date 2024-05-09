using NKnife.Win.Forms;

namespace NKnife.Win.Forms.Kit.Forms
{
    partial class ImagesPanelControlTestDockView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesPanelControlTestDockView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._ImagesPanel = new NKnife.Win.Forms.ImagesPanel();
            this._PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._FindButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._ImagesPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._PropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1030, 516);
            this.splitContainer1.SplitterDistance = 665;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 0;
            // 
            // _ImagesPanel
            // 
            this._ImagesPanel.AutoScroll = true;
            this._ImagesPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this._ImagesPanel.BoxMargin = 10;
            this._ImagesPanel.BoxSize = NKnife.Win.Forms.ImagesPanel.ImageBoxSize.Medium;
            this._ImagesPanel.BuildLabelText = null;
            this._ImagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ImagesPanel.ImageBoxColor = System.Drawing.Color.Brown;
            this._ImagesPanel.ImageBoxLabelColor = System.Drawing.Color.Beige;
            this._ImagesPanel.ImageBoxLabelFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ImagesPanel.Location = new System.Drawing.Point(0, 0);
            this._ImagesPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ImagesPanel.Name = "_ImagesPanel";
            this._ImagesPanel.SelectedImageFile = null;
            this._ImagesPanel.Size = new System.Drawing.Size(665, 516);
            this._ImagesPanel.TabIndex = 0;
            // 
            // _PropertyGrid
            // 
            this._PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._PropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._PropertyGrid.Name = "_PropertyGrid";
            this._PropertyGrid.Size = new System.Drawing.Size(353, 516);
            this._PropertyGrid.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FindButton,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1030, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _FindButton
            // 
            this._FindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._FindButton.Image = ((System.Drawing.Image)(resources.GetObject("_FindButton.Image")));
            this._FindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._FindButton.Name = "_FindButton";
            this._FindButton.Size = new System.Drawing.Size(120, 22);
            this._FindButton.Text = "选择含有图片的目录";
            this._FindButton.Click += new System.EventHandler(this._FindButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ImagesPanelControlTestDockView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1030, 541);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImagesPanelControlTestDockView";
            this.ShowIcon = false;
            this.Text = "ImagesPanel控件测试";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ImagesPanel _ImagesPanel;
        private System.Windows.Forms.PropertyGrid _PropertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _FindButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}