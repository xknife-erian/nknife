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
            this._IpAddressControl = new Gean.Gui.WinForm.IPAddressControl.IPAddressControl();
            this._PortNumberBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._ConnButton = new System.Windows.Forms.Button();
            this.@__ContentTextbox = new System.Windows.Forms.TextBox();
            this._SendButton = new System.Windows.Forms.Button();
            this._CLoseButton = new System.Windows.Forms.Button();
            this._ReceviedTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._PortNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _IpAddressControl
            // 
            this._IpAddressControl.AllowInternalTab = false;
            this._IpAddressControl.AutoHeight = true;
            this._IpAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this._IpAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._IpAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._IpAddressControl.Location = new System.Drawing.Point(81, 22);
            this._IpAddressControl.MinimumSize = new System.Drawing.Size(102, 21);
            this._IpAddressControl.Name = "_IpAddressControl";
            this._IpAddressControl.ReadOnly = false;
            this._IpAddressControl.Size = new System.Drawing.Size(134, 21);
            this._IpAddressControl.TabIndex = 0;
            this._IpAddressControl.Text = "127.0.0.1";
            // 
            // _PortNumberBox
            // 
            this._PortNumberBox.Location = new System.Drawing.Point(221, 22);
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
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器地址：";
            // 
            // _ConnButton
            // 
            this._ConnButton.Location = new System.Drawing.Point(291, 19);
            this._ConnButton.Name = "_ConnButton";
            this._ConnButton.Size = new System.Drawing.Size(75, 28);
            this._ConnButton.TabIndex = 3;
            this._ConnButton.Text = "连接";
            this._ConnButton.UseVisualStyleBackColor = true;
            this._ConnButton.Click += new System.EventHandler(this._ConnButton_Click);
            // 
            // __ContentTextbox
            // 
            this.@__ContentTextbox.Location = new System.Drawing.Point(81, 51);
            this.@__ContentTextbox.Multiline = true;
            this.@__ContentTextbox.Name = "__ContentTextbox";
            this.@__ContentTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__ContentTextbox.Size = new System.Drawing.Size(581, 226);
            this.@__ContentTextbox.TabIndex = 4;
            // 
            // _SendButton
            // 
            this._SendButton.Location = new System.Drawing.Point(81, 399);
            this._SendButton.Name = "_SendButton";
            this._SendButton.Size = new System.Drawing.Size(81, 31);
            this._SendButton.TabIndex = 5;
            this._SendButton.Text = "发送";
            this._SendButton.UseVisualStyleBackColor = true;
            this._SendButton.Click += new System.EventHandler(this._SendButton_Click);
            // 
            // _CLoseButton
            // 
            this._CLoseButton.Location = new System.Drawing.Point(372, 19);
            this._CLoseButton.Name = "_CLoseButton";
            this._CLoseButton.Size = new System.Drawing.Size(75, 28);
            this._CLoseButton.TabIndex = 6;
            this._CLoseButton.Text = "关闭";
            this._CLoseButton.UseVisualStyleBackColor = true;
            this._CLoseButton.Click += new System.EventHandler(this._CLoseButton_Click);
            // 
            // _ReceviedTextBox
            // 
            this._ReceviedTextBox.Location = new System.Drawing.Point(81, 283);
            this._ReceviedTextBox.Multiline = true;
            this._ReceviedTextBox.Name = "_ReceviedTextBox";
            this._ReceviedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._ReceviedTextBox.Size = new System.Drawing.Size(581, 110);
            this._ReceviedTextBox.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 442);
            this.Controls.Add(this._ReceviedTextBox);
            this.Controls.Add(this._CLoseButton);
            this.Controls.Add(this._SendButton);
            this.Controls.Add(this.@__ContentTextbox);
            this.Controls.Add(this._ConnButton);
            this.Controls.Add(this._PortNumberBox);
            this.Controls.Add(this._IpAddressControl);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Socket测试工具";
            ((System.ComponentModel.ISupportInitialize)(this._PortNumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gean.Gui.WinForm.IPAddressControl.IPAddressControl _IpAddressControl;
        private System.Windows.Forms.NumericUpDown _PortNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ConnButton;
        private System.Windows.Forms.TextBox __ContentTextbox;
        private System.Windows.Forms.Button _SendButton;
        private System.Windows.Forms.Button _CLoseButton;
        private System.Windows.Forms.TextBox _ReceviedTextBox;
    }
}

