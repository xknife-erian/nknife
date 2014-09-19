using System.Windows.Forms;
namespace Jeelu.Win
{
    partial class InsertLinkCodeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bookmarkComboBox = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.openEmailBtn = new System.Windows.Forms.Button();
            this.openDocumentBtn = new System.Windows.Forms.Button();
            this.openHTMLfileBtn = new System.Windows.Forms.Button();
            this.picLinkPathTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkAccesskeyComboBox = new System.Windows.Forms.ComboBox();
            this.LinkTargetComboBox = new System.Windows.Forms.ComboBox();
            this.newformBtn = new System.Windows.Forms.Button();
            this.newOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.linkTipTextBox = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bookmarkComboBox);
            this.groupBox1.Controls.Add(this.Label2);
            this.groupBox1.Controls.Add(this.openEmailBtn);
            this.groupBox1.Controls.Add(this.openDocumentBtn);
            this.groupBox1.Controls.Add(this.openHTMLfileBtn);
            this.groupBox1.Controls.Add(this.picLinkPathTextBox);
            this.groupBox1.Controls.Add(this.Label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 82);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 超级链接";
            // 
            // bookmarkComboBox
            // 
            this.bookmarkComboBox.Location = new System.Drawing.Point(76, 49);
            this.bookmarkComboBox.Name = "bookmarkComboBox";
            this.bookmarkComboBox.Size = new System.Drawing.Size(135, 21);
            this.bookmarkComboBox.TabIndex = 1;
            this.bookmarkComboBox.TextChanged += new System.EventHandler(this.bookmarkComboBox_TextChanged);
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(11, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "书签:";
            // 
            // openEmailBtn
            // 
            this.openEmailBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openEmailBtn.Location = new System.Drawing.Point(418, 20);
            this.openEmailBtn.Name = "openEmailBtn";
            this.openEmailBtn.Size = new System.Drawing.Size(28, 23);
            this.openEmailBtn.TabIndex = 4;
            this.openEmailBtn.Click += new System.EventHandler(this.openEmailBtn_Click);
            // 
            // openDocumentBtn
            // 
            this.openDocumentBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openDocumentBtn.Location = new System.Drawing.Point(390, 20);
            this.openDocumentBtn.Name = "openDocumentBtn";
            this.openDocumentBtn.Size = new System.Drawing.Size(28, 23);
            this.openDocumentBtn.TabIndex = 3;
            this.openDocumentBtn.Click += new System.EventHandler(this.openDocumentBtn_Click);
            // 
            // openHTMLfileBtn
            // 
            this.openHTMLfileBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openHTMLfileBtn.Location = new System.Drawing.Point(362, 20);
            this.openHTMLfileBtn.Name = "openHTMLfileBtn";
            this.openHTMLfileBtn.Size = new System.Drawing.Size(28, 23);
            this.openHTMLfileBtn.TabIndex = 2;
            this.openHTMLfileBtn.Click += new System.EventHandler(this.picPathBtn_Click);
            // 
            // picLinkPathTextBox
            // 
            this.picLinkPathTextBox.Location = new System.Drawing.Point(76, 22);
            this.picLinkPathTextBox.Name = "picLinkPathTextBox";
            this.picLinkPathTextBox.Size = new System.Drawing.Size(280, 21);
            this.picLinkPathTextBox.TabIndex = 0;
            this.picLinkPathTextBox.TextChanged += new System.EventHandler(this.picLinkPathTextBox_TextChanged);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(11, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(59, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "链接地址:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkAccesskeyComboBox);
            this.groupBox2.Controls.Add(this.LinkTargetComboBox);
            this.groupBox2.Controls.Add(this.newformBtn);
            this.groupBox2.Controls.Add(this.newOpenCheckBox);
            this.groupBox2.Controls.Add(this.Label5);
            this.groupBox2.Controls.Add(this.linkTipTextBox);
            this.groupBox2.Controls.Add(this.Label4);
            this.groupBox2.Controls.Add(this.Label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 129);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "链接选项:";
            // 
            // linkAccesskeyComboBox
            // 
            this.linkAccesskeyComboBox.Location = new System.Drawing.Point(76, 40);
            this.linkAccesskeyComboBox.Name = "linkAccesskeyComboBox";
            this.linkAccesskeyComboBox.Size = new System.Drawing.Size(157, 21);
            this.linkAccesskeyComboBox.TabIndex = 1;
            this.linkAccesskeyComboBox.TextChanged += new System.EventHandler(this.linkAccesskeyComboBox_TextChanged);
            // 
            // LinkTargetComboBox
            // 
            this.LinkTargetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LinkTargetComboBox.Items.AddRange(new object[] {
            "_blank",
            "_parent",
            "_self",
            "_top"});
            this.LinkTargetComboBox.Location = new System.Drawing.Point(76, 15);
            this.LinkTargetComboBox.Name = "LinkTargetComboBox";
            this.LinkTargetComboBox.Size = new System.Drawing.Size(157, 21);
            this.LinkTargetComboBox.TabIndex = 0;
            this.LinkTargetComboBox.TextChanged += new System.EventHandler(this.LinkTargetComboBox_TextChanged);
            // 
            // newformBtn
            // 
            this.newformBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newformBtn.Location = new System.Drawing.Point(299, 38);
            this.newformBtn.Name = "newformBtn";
            this.newformBtn.Size = new System.Drawing.Size(86, 23);
            this.newformBtn.TabIndex = 23;
            this.newformBtn.Text = "新窗口选项";
            this.newformBtn.Visible = false;
            this.newformBtn.Click += new System.EventHandler(this.newformBtn_Click);
            // 
            // newOpenCheckBox
            // 
            this.newOpenCheckBox.Location = new System.Drawing.Point(299, 18);
            this.newOpenCheckBox.Name = "newOpenCheckBox";
            this.newOpenCheckBox.Size = new System.Drawing.Size(147, 18);
            this.newOpenCheckBox.TabIndex = 22;
            this.newOpenCheckBox.Text = "在新窗口开启链接";
            this.newOpenCheckBox.Visible = false;
            this.newOpenCheckBox.CheckedChanged += new System.EventHandler(this.newOpenCheckBox_CheckedChanged);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(11, 43);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(47, 13);
            this.Label5.TabIndex = 20;
            this.Label5.Text = "存取键:";
            // 
            // linkTipTextBox
            // 
            this.linkTipTextBox.Location = new System.Drawing.Point(76, 65);
            this.linkTipTextBox.Multiline = true;
            this.linkTipTextBox.Name = "linkTipTextBox";
            this.linkTipTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.linkTipTextBox.Size = new System.Drawing.Size(370, 51);
            this.linkTipTextBox.TabIndex = 2;
            this.linkTipTextBox.TextChanged += new System.EventHandler(this.linkTipTextBox_TextChanged);
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(11, 68);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(59, 13);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "链接提示:";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(11, 18);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 16;
            this.Label3.Text = "目标:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelBtn.Location = new System.Drawing.Point(405, 238);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 24);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "取消";
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okBtn.Location = new System.Drawing.Point(323, 238);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 24);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "确定";
            // 
            // Button6
            // 
            this.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button6.Location = new System.Drawing.Point(228, 238);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(80, 24);
            this.Button6.TabIndex = 1;
            this.Button6.Text = "清除";
            // 
            // frmInsertLinkCode
            // 
            this.AcceptButton = this.okBtn;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(504, 282);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsertLinkCode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超级链接";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmInsertLinkCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button openEmailBtn;
        private Button openDocumentBtn;
        private Button openHTMLfileBtn;
        private TextBox picLinkPathTextBox;
        private Label Label1;
        private Label Label2;
        private Label Label5;
        private TextBox linkTipTextBox;
        private Label Label4;
        private Label Label3;
        private CheckBox newOpenCheckBox;
        private Button newformBtn;
        private Button cancelBtn;
        private Button okBtn;
        private Button Button6;
        private ComboBox bookmarkComboBox;
        private ComboBox LinkTargetComboBox;
        private ComboBox linkAccesskeyComboBox;
    }
}