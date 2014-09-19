using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class SnipChannelNamesTreeForm
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
            this.ChannelNamestreeView = new System.Windows.Forms.TreeView();
            this.groupBoxLink = new System.Windows.Forms.GroupBox();
            this.comboBoxSizeUnit = new System.Windows.Forms.ComboBox();
            this.textBoxWidth = new Jeelu.Win.ValidateTextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.groupBoxSeparator = new System.Windows.Forms.GroupBox();
            this.UsedSeparator = new System.Windows.Forms.CheckBox();
            this.textBoxDefaultSeparator = new System.Windows.Forms.TextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.enterBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelChannel = new System.Windows.Forms.Label();
            this.groupBoxLink.SuspendLayout();
            this.groupBoxSeparator.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelNamestreeView
            // 
            this.ChannelNamestreeView.BackColor = System.Drawing.SystemColors.Info;
            this.ChannelNamestreeView.CheckBoxes = true;
            this.ChannelNamestreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChannelNamestreeView.Location = new System.Drawing.Point(0, 0);
            this.ChannelNamestreeView.Name = "ChannelNamestreeView";
            this.ChannelNamestreeView.Size = new System.Drawing.Size(475, 385);
            this.ChannelNamestreeView.TabIndex = 0;
            // 
            // groupBoxLink
            // 
            this.groupBoxLink.Controls.Add(this.comboBoxSizeUnit);
            this.groupBoxLink.Controls.Add(this.textBoxWidth);
            this.groupBoxLink.Controls.Add(this.labelWidth);
            this.groupBoxLink.Location = new System.Drawing.Point(3, 74);
            this.groupBoxLink.Name = "groupBoxLink";
            this.groupBoxLink.Size = new System.Drawing.Size(195, 76);
            this.groupBoxLink.TabIndex = 1;
            this.groupBoxLink.TabStop = false;
            this.groupBoxLink.Text = "频道链接选项";
            // 
            // comboBoxSizeUnit
            // 
            this.comboBoxSizeUnit.BackColor = System.Drawing.SystemColors.Info;
            this.comboBoxSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSizeUnit.FormattingEnabled = true;
            this.comboBoxSizeUnit.Location = new System.Drawing.Point(96, 37);
            this.comboBoxSizeUnit.Name = "comboBoxSizeUnit";
            this.comboBoxSizeUnit.Size = new System.Drawing.Size(84, 21);
            this.comboBoxSizeUnit.TabIndex = 9;
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxWidth.Location = new System.Drawing.Point(18, 37);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.RegexText = "^[0-9]*$";
            this.textBoxWidth.RegexTextRuntime = "^[0-9]*$";
            this.textBoxWidth.Size = new System.Drawing.Size(72, 21);
            this.textBoxWidth.TabIndex = 8;
            this.textBoxWidth.Text = "80";
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(15, 21);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(43, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "宽度：";
            // 
            // groupBoxSeparator
            // 
            this.groupBoxSeparator.Controls.Add(this.UsedSeparator);
            this.groupBoxSeparator.Controls.Add(this.textBoxDefaultSeparator);
            this.groupBoxSeparator.Location = new System.Drawing.Point(3, 12);
            this.groupBoxSeparator.Name = "groupBoxSeparator";
            this.groupBoxSeparator.Size = new System.Drawing.Size(195, 56);
            this.groupBoxSeparator.TabIndex = 0;
            this.groupBoxSeparator.TabStop = false;
            this.groupBoxSeparator.Text = "间隔符";
            // 
            // UsedSeparator
            // 
            this.UsedSeparator.AutoSize = true;
            this.UsedSeparator.Checked = true;
            this.UsedSeparator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UsedSeparator.Location = new System.Drawing.Point(6, 22);
            this.UsedSeparator.Name = "UsedSeparator";
            this.UsedSeparator.Size = new System.Drawing.Size(15, 14);
            this.UsedSeparator.TabIndex = 0;
            this.UsedSeparator.UseVisualStyleBackColor = true;
            this.UsedSeparator.CheckedChanged += new System.EventHandler(this.UsedSeparator_CheckedChanged);
            // 
            // textBoxDefaultSeparator
            // 
            this.textBoxDefaultSeparator.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxDefaultSeparator.Enabled = false;
            this.textBoxDefaultSeparator.Location = new System.Drawing.Point(115, 19);
            this.textBoxDefaultSeparator.Name = "textBoxDefaultSeparator";
            this.textBoxDefaultSeparator.Size = new System.Drawing.Size(67, 21);
            this.textBoxDefaultSeparator.TabIndex = 0;
            this.textBoxDefaultSeparator.Text = "|";
            this.textBoxDefaultSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exitBtn
            // 
            this.exitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitBtn.Location = new System.Drawing.Point(118, 225);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(57, 25);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "退 出";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // enterBtn
            // 
            this.enterBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.enterBtn.Location = new System.Drawing.Point(19, 225);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(57, 25);
            this.enterBtn.TabIndex = 0;
            this.enterBtn.Text = "确 定";
            this.enterBtn.UseVisualStyleBackColor = true;
            this.enterBtn.Click += new System.EventHandler(this.enterBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBoxSeparator);
            this.panel1.Controls.Add(this.enterBtn);
            this.panel1.Controls.Add(this.exitBtn);
            this.panel1.Controls.Add(this.groupBoxLink);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 413);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(205, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(475, 413);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ChannelNamestreeView);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(475, 385);
            this.panel4.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelChannel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(475, 28);
            this.panel3.TabIndex = 1;
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(6, 9);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(103, 13);
            this.labelChannel.TabIndex = 0;
            this.labelChannel.Text = "选择所需的频道：";
            // 
            // SnipChannelNamesTreeForm
            // 
            this.AcceptButton = this.enterBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitBtn;
            this.ClientSize = new System.Drawing.Size(680, 413);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(437, 305);
            this.Name = "SnipChannelNamesTreeForm";
            this.Text = "_频道选择";
            this.Load += new System.EventHandler(this.SnipChannelNamesTreeForm_Load);
            this.groupBoxLink.ResumeLayout(false);
            this.groupBoxLink.PerformLayout();
            this.groupBoxSeparator.ResumeLayout(false);
            this.groupBoxSeparator.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ChannelNamestreeView;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button enterBtn;
        private System.Windows.Forms.GroupBox groupBoxSeparator;
        private System.Windows.Forms.CheckBox UsedSeparator;
        private System.Windows.Forms.GroupBox groupBoxLink;
        private System.Windows.Forms.TextBox textBoxDefaultSeparator;
        private System.Windows.Forms.Label labelWidth;
        private ValidateTextBox textBoxWidth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.ComboBox comboBoxSizeUnit;
    }
}