namespace Jeelu.SimplusOM.Client
{
    partial class FrozedAgentForm
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
            this.frozedhistoryListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.frozedReasonTextBox = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // frozedhistoryListBox
            // 
            this.frozedhistoryListBox.FormattingEnabled = true;
            this.frozedhistoryListBox.ItemHeight = 12;
            this.frozedhistoryListBox.Location = new System.Drawing.Point(12, 37);
            this.frozedhistoryListBox.Name = "frozedhistoryListBox";
            this.frozedhistoryListBox.Size = new System.Drawing.Size(125, 196);
            this.frozedhistoryListBox.TabIndex = 0;
            this.frozedhistoryListBox.DoubleClick += new System.EventHandler(this.frozedhistoryListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查询历史记录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "请在下面的输入框中输入原因：";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(198, 254);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // frozedReasonTextBox
            // 
            this.frozedReasonTextBox.Location = new System.Drawing.Point(143, 37);
            this.frozedReasonTextBox.Multiline = true;
            this.frozedReasonTextBox.Name = "frozedReasonTextBox";
            this.frozedReasonTextBox.Size = new System.Drawing.Size(230, 196);
            this.frozedReasonTextBox.TabIndex = 3;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(279, 254);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // FrozedAgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 289);
            this.Controls.Add(this.frozedReasonTextBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frozedhistoryListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrozedAgentForm";
            this.Text = "冻结代理商";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox frozedhistoryListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.TextBox frozedReasonTextBox;
        private System.Windows.Forms.Button CancelBtn;
    }
}