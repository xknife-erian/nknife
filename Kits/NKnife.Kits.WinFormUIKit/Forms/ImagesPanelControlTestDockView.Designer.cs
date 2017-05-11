using NKnife.Draws.WinForm;

namespace NKnife.Kits.WinFormUIKit.Forms
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
            this._ImagesPanel = new ImagesPanel();
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
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._ImagesPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._PropertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(653, 436);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            // 
            // _ImagesPanel
            // 
            this._ImagesPanel.AutoScroll = true;
            this._ImagesPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this._ImagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ImagesPanel.Location = new System.Drawing.Point(0, 0);
            this._ImagesPanel.Name = "_ImagesPanel";
            this._ImagesPanel.Size = new System.Drawing.Size(422, 436);
            this._ImagesPanel.TabIndex = 0;
            // 
            // _PropertyGrid
            // 
            this._PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._PropertyGrid.Name = "_PropertyGrid";
            this._PropertyGrid.Size = new System.Drawing.Size(221, 436);
            this._PropertyGrid.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FindButton,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(653, 25);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 461);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
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