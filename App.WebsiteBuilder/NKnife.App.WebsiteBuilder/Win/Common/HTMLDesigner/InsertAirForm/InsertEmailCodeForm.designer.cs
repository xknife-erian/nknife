using System.Windows.Forms;
namespace Jeelu.Win
{
    partial class InsertEmailCodeForm
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
            this.emailLinkGroupBox = new System.Windows.Forms.GroupBox();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.lblEmailSubject = new System.Windows.Forms.Label();
            this.emailComboBox = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.emailLinkGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // emailLinkGroupBox
            // 
            this.emailLinkGroupBox.Controls.Add(this.txtEmailSubject);
            this.emailLinkGroupBox.Controls.Add(this.lblEmailSubject);
            this.emailLinkGroupBox.Controls.Add(this.emailComboBox);
            this.emailLinkGroupBox.Controls.Add(this.lblEmail);
            this.emailLinkGroupBox.Location = new System.Drawing.Point(13, 12);
            this.emailLinkGroupBox.Name = "emailLinkGroupBox";
            this.emailLinkGroupBox.Size = new System.Drawing.Size(415, 103);
            this.emailLinkGroupBox.TabIndex = 0;
            this.emailLinkGroupBox.TabStop = false;
            this.emailLinkGroupBox.Text = "emailLinkGroupBox";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Location = new System.Drawing.Point(107, 48);
            this.txtEmailSubject.Multiline = true;
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmailSubject.Size = new System.Drawing.Size(288, 40);
            this.txtEmailSubject.TabIndex = 3;
            // 
            // lblEmailSubject
            // 
            this.lblEmailSubject.Location = new System.Drawing.Point(18, 48);
            this.lblEmailSubject.Name = "lblEmailSubject";
            this.lblEmailSubject.Size = new System.Drawing.Size(59, 13);
            this.lblEmailSubject.TabIndex = 2;
            this.lblEmailSubject.Text = "emailSubjectLabel";
            // 
            // emailComboBox
            // 
            this.emailComboBox.Location = new System.Drawing.Point(107, 22);
            this.emailComboBox.Name = "emailComboBox";
            this.emailComboBox.Size = new System.Drawing.Size(289, 21);
            this.emailComboBox.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(18, 25);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(83, 13);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "emailLabel";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(341, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(249, 121);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // InsertEmailCodeForm
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(443, 166);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.emailLinkGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertEmailCodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertEmailCodeForm";
            this.TopMost = true;
            this.emailLinkGroupBox.ResumeLayout(false);
            this.emailLinkGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox emailLinkGroupBox;
        private Label lblEmailSubject;
        private ComboBox emailComboBox;
        private Label lblEmail;
        private TextBox txtEmailSubject;
        private Button btnCancel;
        private Button btnOK;
    }
}