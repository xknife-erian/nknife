namespace NKnife.Socket.StarterKit
{
    partial class DoorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoorForm));
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._NewServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._NewClientMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.BottomToolStripPanel
            // 
            this._ToolStripContainer.BottomToolStripPanel.Controls.Add(this._StatusStrip);
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(780, 422);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(780, 469);
            this._ToolStripContainer.TabIndex = 0;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._MenuStrip);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(780, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(780, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewServerMenuItem,
            this._NewClientMenuItem,
            this.toolStripSeparator2,
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // _NewServerMenuItem
            // 
            this._NewServerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_NewServerMenuItem.Image")));
            this._NewServerMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewServerMenuItem.Name = "_NewServerMenuItem";
            this._NewServerMenuItem.Size = new System.Drawing.Size(161, 22);
            this._NewServerMenuItem.Text = "新建服务端(&S)...";
            // 
            // _NewClientMenuItem
            // 
            this._NewClientMenuItem.Name = "_NewClientMenuItem";
            this._NewClientMenuItem.Size = new System.Drawing.Size(161, 22);
            this._NewClientMenuItem.Text = "新建客户端(&C)...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项OToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)...";
            // 
            // DoorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 469);
            this.Controls.Add(this._ToolStripContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "DoorForm";
            this.Text = "Socket测试工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _NewServerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _NewClientMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
    }
}