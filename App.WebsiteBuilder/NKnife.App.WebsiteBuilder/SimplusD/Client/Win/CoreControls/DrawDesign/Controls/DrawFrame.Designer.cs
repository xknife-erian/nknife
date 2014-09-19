namespace Jeelu.SimplusD.Client.Win
{
    partial class DrawFrame
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
            
            this._dfVScrollBar = new System.Windows.Forms.VScrollBar();
            this._dfHScrollBar = new System.Windows.Forms.HScrollBar();
            //this._hatchControl = new System.Windows.Forms.Control();
            this.statusScrollSplitContainer = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.statusScrollSplitContainer.Panel1.SuspendLayout();
            this.statusScrollSplitContainer.Panel2.SuspendLayout();
            this.statusScrollSplitContainer.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // _dfVScrollBar
            // 
            this._dfVScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this._dfVScrollBar.Location = new System.Drawing.Point(398, 0);
            this._dfVScrollBar.Name = "_dfVScrollBar";
            this._dfVScrollBar.Size = new System.Drawing.Size(15, 198);
            this._dfVScrollBar.TabIndex = 3;
            // 
            // _dfHScrollBar
            // 
            this._dfHScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dfHScrollBar.Location = new System.Drawing.Point(0, 0);
            this._dfHScrollBar.Name = "_dfHScrollBar";
            this._dfHScrollBar.Size = new System.Drawing.Size(183, 15);
            this._dfHScrollBar.TabIndex = 2;
            // 
            // statusScrollSplitContainer
            // 
            this.statusScrollSplitContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusScrollSplitContainer.Location = new System.Drawing.Point(0, 183);
            this.statusScrollSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.statusScrollSplitContainer.Name = "statusScrollSplitContainer";
            // 
            // statusScrollSplitContainer.Panel1
            // 
            this.statusScrollSplitContainer.Panel1.Controls.Add(this.flowLayoutPanel);
            // 
            // statusScrollSplitContainer.Panel2
            // 
            this.statusScrollSplitContainer.Panel2.Controls.Add(this._dfHScrollBar);
            this.statusScrollSplitContainer.Size = new System.Drawing.Size(398, 15);
            this.statusScrollSplitContainer.SplitterDistance = 213;
            this.statusScrollSplitContainer.SplitterWidth = 2;
            this.statusScrollSplitContainer.TabIndex = 1;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(213, 15);
            this.flowLayoutPanel.TabIndex = 1;
            //
            // _hatchControl
            //
            //this._hatchControl.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            // 
            // DrawFrame
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.statusScrollSplitContainer);
            this.Controls.Add(this._dfVScrollBar);
            //this.Controls.Add(this._hatchControl);
            this.Name = "DrawFrame";
            this.Size = new System.Drawing.Size(0, 0);
            this.statusScrollSplitContainer.Panel1.ResumeLayout(false);
            this.statusScrollSplitContainer.Panel2.ResumeLayout(false);
            this.statusScrollSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.VScrollBar _dfVScrollBar;
        private System.Windows.Forms.HScrollBar _dfHScrollBar;
        private System.Windows.Forms.SplitContainer statusScrollSplitContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}
