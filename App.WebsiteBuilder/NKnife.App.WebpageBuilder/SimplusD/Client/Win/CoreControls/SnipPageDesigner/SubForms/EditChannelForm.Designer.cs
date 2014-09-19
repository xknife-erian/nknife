using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class EditChannelForm
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
            this.enterBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.ChannelNamestreeView = new System.Windows.Forms.TreeView();
            this.labelChannel = new System.Windows.Forms.Label();
            this.groupBoxSelectDisplayType = new System.Windows.Forms.GroupBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.buttonBrowsePic = new System.Windows.Forms.Button();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.radioButtonDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonTitle = new System.Windows.Forms.RadioButton();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.radioButtonPic = new System.Windows.Forms.RadioButton();
            this.groupBoxSelectDisplayType.SuspendLayout();
            this.SuspendLayout();
            // 
            // enterBtn
            // 
            this.enterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enterBtn.BackColor = System.Drawing.SystemColors.Control;
            this.enterBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.enterBtn.Location = new System.Drawing.Point(245, 223);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(45, 25);
            this.enterBtn.TabIndex = 2;
            this.enterBtn.Text = "确 定";
            this.enterBtn.UseVisualStyleBackColor = false;
            this.enterBtn.Click += new System.EventHandler(this.enterBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitBtn.Location = new System.Drawing.Point(296, 223);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(45, 25);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.Text = "返 回";
            this.exitBtn.UseVisualStyleBackColor = true;
            // 
            // ChannelNamestreeView
            // 
            this.ChannelNamestreeView.HideSelection = false;
            this.ChannelNamestreeView.Location = new System.Drawing.Point(15, 25);
            this.ChannelNamestreeView.Name = "ChannelNamestreeView";
            this.ChannelNamestreeView.Size = new System.Drawing.Size(164, 223);
            this.ChannelNamestreeView.TabIndex = 6;
            this.ChannelNamestreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ChannelNamestreeView_AfterSelect);
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(12, 9);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(115, 13);
            this.labelChannel.TabIndex = 0;
            this.labelChannel.Text = "选择所关联的频道：";
            // 
            // groupBoxSelectDisplayType
            // 
            this.groupBoxSelectDisplayType.Controls.Add(this.textBoxText);
            this.groupBoxSelectDisplayType.Controls.Add(this.buttonBrowsePic);
            this.groupBoxSelectDisplayType.Controls.Add(this.textBoxTitle);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonDefault);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonTitle);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonText);
            this.groupBoxSelectDisplayType.Controls.Add(this.radioButtonPic);
            this.groupBoxSelectDisplayType.Location = new System.Drawing.Point(185, 9);
            this.groupBoxSelectDisplayType.Name = "groupBoxSelectDisplayType";
            this.groupBoxSelectDisplayType.Size = new System.Drawing.Size(194, 134);
            this.groupBoxSelectDisplayType.TabIndex = 7;
            this.groupBoxSelectDisplayType.TabStop = false;
            this.groupBoxSelectDisplayType.Text = "SelectDisplayType";
            // 
            // textBoxText
            // 
            this.textBoxText.Enabled = false;
            this.textBoxText.Location = new System.Drawing.Point(84, 101);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(100, 21);
            this.textBoxText.TabIndex = 6;
            this.textBoxText.Leave += new System.EventHandler(this.textBoxText_Leave);
            // 
            // buttonBrowsePic
            // 
            this.buttonBrowsePic.Enabled = false;
            this.buttonBrowsePic.Location = new System.Drawing.Point(111, 72);
            this.buttonBrowsePic.Name = "buttonBrowsePic";
            this.buttonBrowsePic.Size = new System.Drawing.Size(73, 23);
            this.buttonBrowsePic.TabIndex = 5;
            this.buttonBrowsePic.Text = "browse";
            this.buttonBrowsePic.UseVisualStyleBackColor = true;
            this.buttonBrowsePic.Click += new System.EventHandler(this.buttonBrowsePic_Click);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Enabled = false;
            this.textBoxTitle.Location = new System.Drawing.Point(84, 45);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(100, 21);
            this.textBoxTitle.TabIndex = 4;
            // 
            // radioButtonDefault
            // 
            this.radioButtonDefault.AutoSize = true;
            this.radioButtonDefault.Checked = true;
            this.radioButtonDefault.Location = new System.Drawing.Point(6, 20);
            this.radioButtonDefault.Name = "radioButtonDefault";
            this.radioButtonDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButtonDefault.TabIndex = 3;
            this.radioButtonDefault.TabStop = true;
            this.radioButtonDefault.Text = "default";
            this.radioButtonDefault.UseVisualStyleBackColor = true;
            this.radioButtonDefault.CheckedChanged += new System.EventHandler(this.radioButtonDefault_CheckedChanged);
            // 
            // radioButtonTitle
            // 
            this.radioButtonTitle.AutoSize = true;
            this.radioButtonTitle.Location = new System.Drawing.Point(6, 46);
            this.radioButtonTitle.Name = "radioButtonTitle";
            this.radioButtonTitle.Size = new System.Drawing.Size(43, 17);
            this.radioButtonTitle.TabIndex = 2;
            this.radioButtonTitle.Text = "title";
            this.radioButtonTitle.UseVisualStyleBackColor = true;
            this.radioButtonTitle.CheckedChanged += new System.EventHandler(this.radioButtonTitle_CheckedChanged);
            // 
            // radioButtonText
            // 
            this.radioButtonText.AutoSize = true;
            this.radioButtonText.Location = new System.Drawing.Point(6, 102);
            this.radioButtonText.Name = "radioButtonText";
            this.radioButtonText.Size = new System.Drawing.Size(45, 17);
            this.radioButtonText.TabIndex = 1;
            this.radioButtonText.Text = "text";
            this.radioButtonText.UseVisualStyleBackColor = true;
            this.radioButtonText.CheckedChanged += new System.EventHandler(this.radioButtonText_CheckedChanged);
            // 
            // radioButtonPic
            // 
            this.radioButtonPic.AutoSize = true;
            this.radioButtonPic.Location = new System.Drawing.Point(6, 75);
            this.radioButtonPic.Name = "radioButtonPic";
            this.radioButtonPic.Size = new System.Drawing.Size(58, 17);
            this.radioButtonPic.TabIndex = 0;
            this.radioButtonPic.Text = "pictrue";
            this.radioButtonPic.UseVisualStyleBackColor = true;
            this.radioButtonPic.CheckedChanged += new System.EventHandler(this.radioButtonPic_CheckedChanged);
            // 
            // EditChannelForm
            // 
            this.AcceptButton = this.enterBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitBtn;
            this.ClientSize = new System.Drawing.Size(384, 260);
            this.Controls.Add(this.groupBoxSelectDisplayType);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.ChannelNamestreeView);
            this.Controls.Add(this.enterBtn);
            this.Name = "EditChannelForm";
            this.Text = "SnipAddSingleChannelForm";
            this.Load += new System.EventHandler(this.SnipAddSingleChannelForm_Load);
            this.groupBoxSelectDisplayType.ResumeLayout(false);
            this.groupBoxSelectDisplayType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.TreeView ChannelNamestreeView;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.GroupBox groupBoxSelectDisplayType;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button buttonBrowsePic;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.RadioButton radioButtonDefault;
        private System.Windows.Forms.RadioButton radioButtonTitle;
        private System.Windows.Forms.RadioButton radioButtonText;
        private System.Windows.Forms.RadioButton radioButtonPic;
    }
}