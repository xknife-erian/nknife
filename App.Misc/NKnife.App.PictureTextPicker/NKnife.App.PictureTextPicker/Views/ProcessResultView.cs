using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class ProcessResultView : DockContent
    {
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ListView _DocumentResultListView;
        private System.Windows.Forms.ColumnHeader IndexColumnHeader;
        private System.Windows.Forms.ColumnHeader CoordinateColumnHeader;
        private System.Windows.Forms.ColumnHeader TextColumnHeader;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    
        public ProcessResultView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessResultView));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._DocumentResultListView = new System.Windows.Forms.ListView();
            this.IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CoordinateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TextColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._DocumentResultListView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(410, 336);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(410, 361);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(33, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // _DocumentResultListView
            // 
            this._DocumentResultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IndexColumnHeader,
            this.CoordinateColumnHeader,
            this.TextColumnHeader});
            this._DocumentResultListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DocumentResultListView.FullRowSelect = true;
            this._DocumentResultListView.GridLines = true;
            this._DocumentResultListView.Location = new System.Drawing.Point(0, 0);
            this._DocumentResultListView.Name = "_DocumentResultListView";
            this._DocumentResultListView.Size = new System.Drawing.Size(410, 336);
            this._DocumentResultListView.TabIndex = 0;
            this._DocumentResultListView.UseCompatibleStateImageBehavior = false;
            this._DocumentResultListView.View = System.Windows.Forms.View.Details;
            // 
            // IndexColumnHeader
            // 
            this.IndexColumnHeader.Text = "编号";
            // 
            // CoordinateColumnHeader
            // 
            this.CoordinateColumnHeader.Text = "坐标";
            this.CoordinateColumnHeader.Width = 200;
            // 
            // TextColumnHeader
            // 
            this.TextColumnHeader.Text = "文字";
            this.TextColumnHeader.Width = 100;
            // 
            // ProcessResultView
            // 
            this.ClientSize = new System.Drawing.Size(410, 361);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProcessResultView";
            this.Text = "数据列表";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
