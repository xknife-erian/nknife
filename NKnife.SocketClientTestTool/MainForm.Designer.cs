using NKnife.GUI.WinForm.IPAddressControl;

namespace NKnife.SocketClientTestTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._PortNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._ConnButton = new System.Windows.Forms.Button();
            this.@__ContentTextbox = new System.Windows.Forms.TextBox();
            this._CLoseButton = new System.Windows.Forms.Button();
            this._ReceviedTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this._ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this._SendButton = new System.Windows.Forms.ToolStripButton();
            this._StatusStrip2 = new System.Windows.Forms.StatusStrip();
            this._ToolStrip2 = new System.Windows.Forms.ToolStrip();
            this._IpAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this._TabControl = new System.Windows.Forms.TabControl();
            this._SocketInfoTabPage = new System.Windows.Forms.TabPage();
            this._LogTabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._ToolStrip1.SuspendLayout();
            this._TabControl.SuspendLayout();
            this._SocketInfoTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PortNumberBox
            // 
            this._PortNumberBox.Location = new System.Drawing.Point(219, 19);
            this._PortNumberBox.Maximum = new decimal(new int[] {
            25565,
            0,
            0,
            0});
            this._PortNumberBox.Name = "_PortNumberBox";
            this._PortNumberBox.Size = new System.Drawing.Size(64, 21);
            this._PortNumberBox.TabIndex = 1;
            this._PortNumberBox.Value = new decimal(new int[] {
            22020,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器地址";
            // 
            // _ConnButton
            // 
            this._ConnButton.Location = new System.Drawing.Point(295, 15);
            this._ConnButton.Name = "_ConnButton";
            this._ConnButton.Size = new System.Drawing.Size(75, 28);
            this._ConnButton.TabIndex = 3;
            this._ConnButton.Text = "连接";
            this._ConnButton.UseVisualStyleBackColor = true;
            this._ConnButton.Click += new System.EventHandler(this._ConnButton_Click);
            // 
            // __ContentTextbox
            // 
            this.@__ContentTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__ContentTextbox.Location = new System.Drawing.Point(0, 25);
            this.@__ContentTextbox.Multiline = true;
            this.@__ContentTextbox.Name = "__ContentTextbox";
            this.@__ContentTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__ContentTextbox.Size = new System.Drawing.Size(737, 234);
            this.@__ContentTextbox.TabIndex = 4;
            // 
            // _CLoseButton
            // 
            this._CLoseButton.Location = new System.Drawing.Point(376, 15);
            this._CLoseButton.Name = "_CLoseButton";
            this._CLoseButton.Size = new System.Drawing.Size(75, 28);
            this._CLoseButton.TabIndex = 6;
            this._CLoseButton.Text = "关闭";
            this._CLoseButton.UseVisualStyleBackColor = true;
            this._CLoseButton.Click += new System.EventHandler(this._CLoseButton_Click);
            // 
            // _ReceviedTextBox
            // 
            this._ReceviedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ReceviedTextBox.Location = new System.Drawing.Point(0, 25);
            this._ReceviedTextBox.Multiline = true;
            this._ReceviedTextBox.Name = "_ReceviedTextBox";
            this._ReceviedTextBox.ReadOnly = true;
            this._ReceviedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ReceviedTextBox.Size = new System.Drawing.Size(737, 257);
            this._ReceviedTextBox.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.@__ContentTextbox);
            this.splitContainer1.Panel1.Controls.Add(this._StatusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this._ToolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._ReceviedTextBox);
            this.splitContainer1.Panel2.Controls.Add(this._StatusStrip2);
            this.splitContainer1.Panel2.Controls.Add(this._ToolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(737, 595);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 8;
            // 
            // _StatusStrip1
            // 
            this._StatusStrip1.Location = new System.Drawing.Point(0, 259);
            this._StatusStrip1.Name = "_StatusStrip1";
            this._StatusStrip1.Size = new System.Drawing.Size(737, 22);
            this._StatusStrip1.TabIndex = 10;
            this._StatusStrip1.Text = "statusStrip1";
            // 
            // _ToolStrip1
            // 
            this._ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SendButton});
            this._ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip1.Name = "_ToolStrip1";
            this._ToolStrip1.Size = new System.Drawing.Size(737, 25);
            this._ToolStrip1.TabIndex = 9;
            this._ToolStrip1.Text = "toolStrip1";
            // 
            // _SendButton
            // 
            this._SendButton.Image = ((System.Drawing.Image)(resources.GetObject("_SendButton.Image")));
            this._SendButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SendButton.Name = "_SendButton";
            this._SendButton.Size = new System.Drawing.Size(80, 22);
            this._SendButton.Text = " 协议发送";
            this._SendButton.Click += new System.EventHandler(this._SendButton_Click);
            // 
            // _StatusStrip2
            // 
            this._StatusStrip2.Location = new System.Drawing.Point(0, 282);
            this._StatusStrip2.Name = "_StatusStrip2";
            this._StatusStrip2.Size = new System.Drawing.Size(737, 22);
            this._StatusStrip2.TabIndex = 9;
            this._StatusStrip2.Text = "statusStrip2";
            // 
            // _ToolStrip2
            // 
            this._ToolStrip2.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip2.Name = "_ToolStrip2";
            this._ToolStrip2.Size = new System.Drawing.Size(737, 25);
            this._ToolStrip2.TabIndex = 8;
            this._ToolStrip2.Text = "toolStrip2";
            // 
            // _IpAddressControl
            // 
            this._IpAddressControl.AllowInternalTab = false;
            this._IpAddressControl.AutoHeight = true;
            this._IpAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._IpAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._IpAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._IpAddressControl.Location = new System.Drawing.Point(85, 19);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(102, 21);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(128, 21);
            this._IpAddressControl.TabIndex = 9;
            this._IpAddressControl.Text = "127.0.0.1";
            // 
            // _TabControl
            // 
            this._TabControl.Alignment = System.Windows.Forms.TabAlignment.Right;
            this._TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TabControl.Controls.Add(this._SocketInfoTabPage);
            this._TabControl.Controls.Add(this._LogTabPage);
            this._TabControl.Location = new System.Drawing.Point(12, 46);
            this._TabControl.Multiline = true;
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(771, 609);
            this._TabControl.TabIndex = 10;
            // 
            // _SocketInfoTabPage
            // 
            this._SocketInfoTabPage.Controls.Add(this.splitContainer1);
            this._SocketInfoTabPage.Location = new System.Drawing.Point(4, 4);
            this._SocketInfoTabPage.Name = "_SocketInfoTabPage";
            this._SocketInfoTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._SocketInfoTabPage.Size = new System.Drawing.Size(743, 601);
            this._SocketInfoTabPage.TabIndex = 0;
            this._SocketInfoTabPage.Text = "Socket信息";
            this._SocketInfoTabPage.UseVisualStyleBackColor = true;
            // 
            // _LogTabPage
            // 
            this._LogTabPage.Location = new System.Drawing.Point(4, 4);
            this._LogTabPage.Name = "_LogTabPage";
            this._LogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._LogTabPage.Size = new System.Drawing.Size(743, 601);
            this._LogTabPage.TabIndex = 1;
            this._LogTabPage.Text = "程序日志";
            this._LogTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 667);
            this.Controls.Add(this._TabControl);
            this.Controls.Add(this._IpAddressControl);
            this.Controls.Add(this._CLoseButton);
            this.Controls.Add(this._ConnButton);
            this.Controls.Add(this._PortNumberBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Socket测试工具";
            ((System.ComponentModel.ISupportInitialize)(this._PortNumberBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this._ToolStrip1.ResumeLayout(false);
            this._ToolStrip1.PerformLayout();
            this._TabControl.ResumeLayout(false);
            this._SocketInfoTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown _PortNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ConnButton;
        private System.Windows.Forms.TextBox __ContentTextbox;
        private System.Windows.Forms.Button _CLoseButton;
        private System.Windows.Forms.TextBox _ReceviedTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private IpAddressControl _IpAddressControl;
        private System.Windows.Forms.TabControl _TabControl;
        private System.Windows.Forms.TabPage _SocketInfoTabPage;
        private System.Windows.Forms.TabPage _LogTabPage;
        private System.Windows.Forms.ToolStrip _ToolStrip1;
        private System.Windows.Forms.ToolStripButton _SendButton;
        private System.Windows.Forms.StatusStrip _StatusStrip1;
        private System.Windows.Forms.StatusStrip _StatusStrip2;
        private System.Windows.Forms.ToolStrip _ToolStrip2;
    }
}

