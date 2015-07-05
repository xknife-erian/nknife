namespace NKnife.Kits.SocketKnife.StressTest
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RemoveTalkButton = new System.Windows.Forms.Button();
            this.AddTalkButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TalkCountLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SessionCountLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientSendIntervalTextBox = new System.Windows.Forms.TextBox();
            this.ClientCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartTestButton = new System.Windows.Forms.Button();
            this.LogContainerPanel = new System.Windows.Forms.Panel();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.RemoveTalkButton);
            this.MainPanel.Controls.Add(this.AddTalkButton);
            this.MainPanel.Controls.Add(this.groupBox2);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.ClientSendIntervalTextBox);
            this.MainPanel.Controls.Add(this.ClientCountTextBox);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.StartTestButton);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(684, 297);
            this.MainPanel.TabIndex = 4;
            // 
            // RemoveTalkButton
            // 
            this.RemoveTalkButton.Enabled = false;
            this.RemoveTalkButton.Location = new System.Drawing.Point(233, 229);
            this.RemoveTalkButton.Name = "RemoveTalkButton";
            this.RemoveTalkButton.Size = new System.Drawing.Size(83, 39);
            this.RemoveTalkButton.TabIndex = 10;
            this.RemoveTalkButton.Text = "减少对讲";
            this.RemoveTalkButton.UseVisualStyleBackColor = true;
            this.RemoveTalkButton.Click += new System.EventHandler(this.RemoveTalkButtonClick);
            // 
            // AddTalkButton
            // 
            this.AddTalkButton.Enabled = false;
            this.AddTalkButton.Location = new System.Drawing.Point(133, 229);
            this.AddTalkButton.Name = "AddTalkButton";
            this.AddTalkButton.Size = new System.Drawing.Size(83, 39);
            this.AddTalkButton.TabIndex = 9;
            this.AddTalkButton.Text = "增加对讲";
            this.AddTalkButton.UseVisualStyleBackColor = true;
            this.AddTalkButton.Click += new System.EventHandler(this.AddTalkButtonClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TalkCountLabel);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.SessionCountLabel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(349, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 169);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server状态";
            // 
            // TalkCountLabel
            // 
            this.TalkCountLabel.BackColor = System.Drawing.Color.White;
            this.TalkCountLabel.Location = new System.Drawing.Point(107, 53);
            this.TalkCountLabel.Name = "TalkCountLabel";
            this.TalkCountLabel.Size = new System.Drawing.Size(100, 18);
            this.TalkCountLabel.TabIndex = 12;
            this.TalkCountLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "Talk数量：";
            // 
            // SessionCountLabel
            // 
            this.SessionCountLabel.BackColor = System.Drawing.Color.White;
            this.SessionCountLabel.Location = new System.Drawing.Point(107, 26);
            this.SessionCountLabel.Name = "SessionCountLabel";
            this.SessionCountLabel.Size = new System.Drawing.Size(100, 18);
            this.SessionCountLabel.TabIndex = 10;
            this.SessionCountLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Session数量：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "（毫秒，不小于50）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "（1-1000）";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 70);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "说明";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(20, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(542, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "通过SocketKnife建立一个SocketServer，指定数量的SocketClient，连接该Server，每个Client均定时（指定时间间隔）发送测试" +
    "数据给Server，Server收到后返回";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "发送间隔：";
            // 
            // ClientSendIntervalTextBox
            // 
            this.ClientSendIntervalTextBox.Location = new System.Drawing.Point(116, 164);
            this.ClientSendIntervalTextBox.Name = "ClientSendIntervalTextBox";
            this.ClientSendIntervalTextBox.Size = new System.Drawing.Size(100, 21);
            this.ClientSendIntervalTextBox.TabIndex = 3;
            this.ClientSendIntervalTextBox.Text = "1000";
            // 
            // ClientCountTextBox
            // 
            this.ClientCountTextBox.Location = new System.Drawing.Point(116, 122);
            this.ClientCountTextBox.Name = "ClientCountTextBox";
            this.ClientCountTextBox.Size = new System.Drawing.Size(100, 21);
            this.ClientCountTextBox.TabIndex = 2;
            this.ClientCountTextBox.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Client数量：";
            // 
            // StartTestButton
            // 
            this.StartTestButton.Location = new System.Drawing.Point(33, 229);
            this.StartTestButton.Name = "StartTestButton";
            this.StartTestButton.Size = new System.Drawing.Size(83, 39);
            this.StartTestButton.TabIndex = 0;
            this.StartTestButton.Text = "开始测试";
            this.StartTestButton.UseVisualStyleBackColor = true;
            this.StartTestButton.Click += new System.EventHandler(this.StartTestButtonClick);
            // 
            // LogContainerPanel
            // 
            this.LogContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogContainerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LogContainerPanel.Location = new System.Drawing.Point(0, 322);
            this.LogContainerPanel.Name = "LogContainerPanel";
            this.LogContainerPanel.Size = new System.Drawing.Size(684, 282);
            this.LogContainerPanel.TabIndex = 2;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(684, 25);
            this.MainMenuStrip.TabIndex = 3;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 604);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LogContainerPanel);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "MainForm";
            this.Text = "SocketKnife压力测试";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button StartTestButton;
        private System.Windows.Forms.Panel LogContainerPanel;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ClientSendIntervalTextBox;
        private System.Windows.Forms.TextBox ClientCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label SessionCountLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button RemoveTalkButton;
        private System.Windows.Forms.Button AddTalkButton;
        private System.Windows.Forms.Label TalkCountLabel;
        private System.Windows.Forms.Label label8;
    }
}

