using System.Windows.Forms;

namespace NKnife.ChannelKnife.View
{
    partial class Workbench
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DevicesManagerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._WindowsItem = new System.Windows.Forms.ToolStripMenuItem();
            this._SingleDeviceTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this._OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._ServerInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._SpringLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._VersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileItem,
            this._WindowsItem,
            this._ToolItem,
            this._HelpItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this._MenuStrip.Size = new System.Drawing.Size(1176, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // _FileItem
            // 
            this._FileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._DevicesManagerMenuItem,
            this.toolStripSeparator2,
            this._ExitToolStripMenuItem});
            this._FileItem.Name = "_FileItem";
            this._FileItem.Size = new System.Drawing.Size(58, 21);
            this._FileItem.Text = "文件(&F)";
            // 
            // _DevicesManagerMenuItem
            // 
            this._DevicesManagerMenuItem.Name = "_DevicesManagerMenuItem";
            this._DevicesManagerMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this._DevicesManagerMenuItem.Size = new System.Drawing.Size(165, 22);
            this._DevicesManagerMenuItem.Text = "设备管理(&M)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // _ExitToolStripMenuItem
            // 
            this._ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            this._ExitToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this._ExitToolStripMenuItem.Text = "退出(&X)";
            // 
            // _WindowsItem
            // 
            this._WindowsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SingleDeviceTestMenuItem});
            this._WindowsItem.Name = "_WindowsItem";
            this._WindowsItem.Size = new System.Drawing.Size(60, 21);
            this._WindowsItem.Text = "视图(&V)";
            // 
            // _SingleDeviceTestMenuItem
            // 
            this._SingleDeviceTestMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_SingleDeviceTestMenuItem.Image")));
            this._SingleDeviceTestMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SingleDeviceTestMenuItem.Name = "_SingleDeviceTestMenuItem";
            this._SingleDeviceTestMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this._SingleDeviceTestMenuItem.Size = new System.Drawing.Size(160, 22);
            this._SingleDeviceTestMenuItem.Text = "单体设备(&S)";
            // 
            // _ToolItem
            // 
            this._ToolItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OptionToolStripMenuItem});
            this._ToolItem.Name = "_ToolItem";
            this._ToolItem.Size = new System.Drawing.Size(59, 21);
            this._ToolItem.Text = "工具(&T)";
            // 
            // _OptionToolStripMenuItem
            // 
            this._OptionToolStripMenuItem.Name = "_OptionToolStripMenuItem";
            this._OptionToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this._OptionToolStripMenuItem.Text = "选项(&O)";
            // 
            // _HelpItem
            // 
            this._HelpItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AboutToolStripMenuItem});
            this._HelpItem.Name = "_HelpItem";
            this._HelpItem.Size = new System.Drawing.Size(61, 21);
            this._HelpItem.Text = "帮助(&H)";
            // 
            // _AboutToolStripMenuItem
            // 
            this._AboutToolStripMenuItem.Name = "_AboutToolStripMenuItem";
            this._AboutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this._AboutToolStripMenuItem.Text = "关于(&A)...";
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ServerInfoLabel,
            this._SpringLabel,
            this._VersionLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 875);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this._StatusStrip.Size = new System.Drawing.Size(1176, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // _ServerInfoLabel
            // 
            this._ServerInfoLabel.Name = "_ServerInfoLabel";
            this._ServerInfoLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _SpringLabel
            // 
            this._SpringLabel.Name = "_SpringLabel";
            this._SpringLabel.Size = new System.Drawing.Size(1159, 17);
            this._SpringLabel.Spring = true;
            // 
            // _VersionLabel
            // 
            this._VersionLabel.Name = "_VersionLabel";
            this._VersionLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // Workbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 897);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._MenuStrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Location = new System.Drawing.Point(100, 50);
            this.MainMenuStrip = this._MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Workbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialPort And Socket Debugger";
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _FileItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ToolItem;
        private System.Windows.Forms.ToolStripMenuItem _OptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _HelpItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _SingleDeviceTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DevicesManagerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _WindowsItem;
        private ToolStripStatusLabel _SpringLabel;
        private ToolStripStatusLabel _VersionLabel;
        private ToolStripStatusLabel _ServerInfoLabel;
    }
}

