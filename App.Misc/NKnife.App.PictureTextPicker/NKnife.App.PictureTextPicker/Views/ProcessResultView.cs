using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class ProcessResultView : DockContent
    {
        private readonly IPictureList _PictureList = DI.Get<IPictureList>();

        #region 控件
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer1;
        private System.Windows.Forms.ListView _DocumentResultListView;
        private System.Windows.Forms.ColumnHeader _IndexColumnHeader;
        private System.Windows.Forms.ColumnHeader _CoordinateColumnHeader;
        private System.Windows.Forms.ColumnHeader _TextColumnHeader;
        private System.Windows.Forms.ToolStrip _ToolStrip1;
        private System.Windows.Forms.ToolStripButton _ToolStripButton1;
        #endregion

        #region 构造函数
        public ProcessResultView()
        {
            InitializeComponent();
            _PictureList.PictureSelected += (s, e) =>
            {

            };
        }

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessResultView));
            this._ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this._ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._DocumentResultListView = new System.Windows.Forms.ListView();
            this._IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._CoordinateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._TextColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ToolStripContainer1.ContentPanel.SuspendLayout();
            this._ToolStripContainer1.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer1.SuspendLayout();
            this._ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this._ToolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this._ToolStripContainer1.ContentPanel.Controls.Add(this._DocumentResultListView);
            this._ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(410, 336);
            this._ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer1.LeftToolStripPanelVisible = false;
            this._ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer1.Name = "_ToolStripContainer1";
            this._ToolStripContainer1.RightToolStripPanelVisible = false;
            this._ToolStripContainer1.Size = new System.Drawing.Size(410, 361);
            this._ToolStripContainer1.TabIndex = 0;
            this._ToolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this._ToolStripContainer1.TopToolStripPanel.Controls.Add(this._ToolStrip1);
            // 
            // toolStrip1
            // 
            this._ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this._ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ToolStripButton1});
            this._ToolStrip1.Location = new System.Drawing.Point(3, 0);
            this._ToolStrip1.Name = "_ToolStrip1";
            this._ToolStrip1.Size = new System.Drawing.Size(33, 25);
            this._ToolStrip1.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this._ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this._ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ToolStripButton1.Name = "toolStripButton1";
            this._ToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this._ToolStripButton1.Text = "toolStripButton1";
            // 
            // _DocumentResultListView
            // 
            this._DocumentResultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._IndexColumnHeader,
            this._CoordinateColumnHeader,
            this._TextColumnHeader});
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
            this._IndexColumnHeader.Text = "编号";
            // 
            // CoordinateColumnHeader
            // 
            this._CoordinateColumnHeader.Text = "坐标";
            this._CoordinateColumnHeader.Width = 200;
            // 
            // TextColumnHeader
            // 
            this._TextColumnHeader.Text = "文字";
            this._TextColumnHeader.Width = 100;
            // 
            // ProcessResultView
            // 
            this.ClientSize = new System.Drawing.Size(410, 361);
            this.Controls.Add(this._ToolStripContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProcessResultView";
            this.Text = "数据列表";
            this._ToolStripContainer1.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer1.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer1.ResumeLayout(false);
            this._ToolStripContainer1.PerformLayout();
            this._ToolStrip1.ResumeLayout(false);
            this._ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
