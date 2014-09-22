namespace Jeelu.Win
{
    partial class CssSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CssSettingForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.sbtnDesign = new System.Windows.Forms.ToolStripButton();
            this.sbtnCode = new System.Windows.Forms.ToolStripButton();
            this.listBoxLeftType = new System.Windows.Forms.ListBox();
            this.splitContainerUI = new System.Windows.Forms.SplitContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.txtCssText = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            this.splitContainerUI.Panel1.SuspendLayout();
            this.splitContainerUI.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbtnDesign,
            this.sbtnCode});
            this.toolStrip.Location = new System.Drawing.Point(5, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(516, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // sbtnDesign
            // 
            this.sbtnDesign.Checked = true;
            this.sbtnDesign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sbtnDesign.Image = ((System.Drawing.Image)(resources.GetObject("sbtnDesign.Image")));
            this.sbtnDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnDesign.Name = "sbtnDesign";
            this.sbtnDesign.Size = new System.Drawing.Size(51, 22);
            this.sbtnDesign.Text = "设计";
            this.sbtnDesign.Click += new System.EventHandler(this.sbtnDesign_Click);
            // 
            // sbtnCode
            // 
            this.sbtnCode.Image = ((System.Drawing.Image)(resources.GetObject("sbtnCode.Image")));
            this.sbtnCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnCode.Name = "sbtnCode";
            this.sbtnCode.Size = new System.Drawing.Size(51, 22);
            this.sbtnCode.Text = "代码";
            this.sbtnCode.Click += new System.EventHandler(this.sbtnCode_Click);
            // 
            // listBoxLeftType
            // 
            this.listBoxLeftType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLeftType.FormattingEnabled = true;
            this.listBoxLeftType.Items.AddRange(new object[] {
            "类型",
            "背景",
            "区块",
            "方框",
            "列表",
            "定位",
            "扩展"});
            this.listBoxLeftType.Location = new System.Drawing.Point(0, 0);
            this.listBoxLeftType.Name = "listBoxLeftType";
            this.listBoxLeftType.Size = new System.Drawing.Size(81, 251);
            this.listBoxLeftType.TabIndex = 1;
            this.listBoxLeftType.SelectedIndexChanged += new System.EventHandler(this.listBoxLeftType_SelectedIndexChanged);
            // 
            // splitContainerUI
            // 
            this.splitContainerUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerUI.IsSplitterFixed = true;
            this.splitContainerUI.Location = new System.Drawing.Point(0, 0);
            this.splitContainerUI.Name = "splitContainerUI";
            // 
            // splitContainerUI.Panel1
            // 
            this.splitContainerUI.Panel1.Controls.Add(this.listBoxLeftType);
            this.splitContainerUI.Size = new System.Drawing.Size(516, 255);
            this.splitContainerUI.SplitterDistance = 81;
            this.splitContainerUI.TabIndex = 2;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(5, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerUI);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.txtCssText);
            this.splitContainerMain.Panel2Collapsed = true;
            this.splitContainerMain.Size = new System.Drawing.Size(516, 255);
            this.splitContainerMain.SplitterDistance = 210;
            this.splitContainerMain.TabIndex = 3;
            // 
            // txtCssText
            // 
            this.txtCssText.AcceptsReturn = true;
            this.txtCssText.AcceptsTab = true;
            this.txtCssText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCssText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCssText.Location = new System.Drawing.Point(0, 0);
            this.txtCssText.Multiline = true;
            this.txtCssText.Name = "txtCssText";
            this.txtCssText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCssText.Size = new System.Drawing.Size(96, 100);
            this.txtCssText.TabIndex = 0;
            this.txtCssText.WordWrap = false;
            this.txtCssText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCssText_KeyDown);
            this.txtCssText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCssText_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(365, 286);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(446, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CssSettingForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(526, 324);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.Name = "CssSettingForm";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.ShowInTaskbar = false;
            this.Text = "CSS 设置";
            this.DoubleClick += new System.EventHandler(this.CssSettingForm_DoubleClick);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainerUI.Panel1.ResumeLayout(false);
            this.splitContainerUI.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ListBox listBoxLeftType;
        private System.Windows.Forms.SplitContainer splitContainerUI;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripButton sbtnDesign;
        private System.Windows.Forms.ToolStripButton sbtnCode;
        private System.Windows.Forms.TextBox txtCssText;
    }
}