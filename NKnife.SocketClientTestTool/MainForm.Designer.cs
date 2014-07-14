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
            this._PortNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._ConnButton = new System.Windows.Forms.Button();
            this.@__ContentTextbox = new System.Windows.Forms.TextBox();
            this._SendButton = new System.Windows.Forms.Button();
            this._CLoseButton = new System.Windows.Forms.Button();
            this._ReceviedTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._IpAddressControl = new NKnife.GUI.WinForm.IPAddressControl.IpAddressControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._LogTabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumberBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.@__ContentTextbox.Location = new System.Drawing.Point(0, 0);
            this.@__ContentTextbox.Multiline = true;
            this.@__ContentTextbox.Name = "__ContentTextbox";
            this.@__ContentTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__ContentTextbox.Size = new System.Drawing.Size(650, 250);
            this.@__ContentTextbox.TabIndex = 4;
            // 
            // _SendButton
            // 
            this._SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._SendButton.Location = new System.Drawing.Point(615, 14);
            this._SendButton.Name = "_SendButton";
            this._SendButton.Size = new System.Drawing.Size(81, 31);
            this._SendButton.TabIndex = 5;
            this._SendButton.Text = "发送";
            this._SendButton.UseVisualStyleBackColor = true;
            this._SendButton.Click += new System.EventHandler(this._SendButton_Click);
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
            this._ReceviedTextBox.Location = new System.Drawing.Point(0, 0);
            this._ReceviedTextBox.Multiline = true;
            this._ReceviedTextBox.Name = "_ReceviedTextBox";
            this._ReceviedTextBox.ReadOnly = true;
            this._ReceviedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ReceviedTextBox.Size = new System.Drawing.Size(650, 265);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._ReceviedTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(650, 525);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 8;
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
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this._LogTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 46);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 539);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(656, 531);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Socket信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _LogTabPage
            // 
            this._LogTabPage.Location = new System.Drawing.Point(4, 4);
            this._LogTabPage.Name = "_LogTabPage";
            this._LogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._LogTabPage.Size = new System.Drawing.Size(656, 531);
            this._LogTabPage.TabIndex = 1;
            this._LogTabPage.Text = "程序日志";
            this._LogTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 597);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._IpAddressControl);
            this.Controls.Add(this._CLoseButton);
            this.Controls.Add(this._SendButton);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown _PortNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ConnButton;
        private System.Windows.Forms.TextBox __ContentTextbox;
        private System.Windows.Forms.Button _SendButton;
        private System.Windows.Forms.Button _CLoseButton;
        private System.Windows.Forms.TextBox _ReceviedTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private IpAddressControl _IpAddressControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage _LogTabPage;
    }
}

