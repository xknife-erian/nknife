namespace Jeelu.SimplusD.Client.Win
{
    partial class InsertDatetimeForm
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
            this.Daylabel = new System.Windows.Forms.Label();
            this.Datelabel = new System.Windows.Forms.Label();
            this.Timelabel = new System.Windows.Forms.Label();
            this.DateListBox = new System.Windows.Forms.ListBox();
            this.dayComboBox = new System.Windows.Forms.ComboBox();
            this.TimeComboBox = new System.Windows.Forms.ComboBox();
            this.updateCheckBox = new System.Windows.Forms.CheckBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.HelpBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Daylabel
            // 
            this.Daylabel.AutoSize = true;
            this.Daylabel.Location = new System.Drawing.Point(12, 21);
            this.Daylabel.Name = "Daylabel";
            this.Daylabel.Size = new System.Drawing.Size(23, 12);
            this.Daylabel.TabIndex = 0;
            this.Daylabel.Text = "Day";
            // 
            // Datelabel
            // 
            this.Datelabel.AutoSize = true;
            this.Datelabel.Location = new System.Drawing.Point(12, 42);
            this.Datelabel.Name = "Datelabel";
            this.Datelabel.Size = new System.Drawing.Size(29, 12);
            this.Datelabel.TabIndex = 1;
            this.Datelabel.Text = "date";
            // 
            // Timelabel
            // 
            this.Timelabel.AutoSize = true;
            this.Timelabel.Location = new System.Drawing.Point(12, 137);
            this.Timelabel.Name = "Timelabel";
            this.Timelabel.Size = new System.Drawing.Size(29, 12);
            this.Timelabel.TabIndex = 2;
            this.Timelabel.Text = "time";
            // 
            // DateListBox
            // 
            this.DateListBox.FormattingEnabled = true;
            this.DateListBox.ItemHeight = 12;
            this.DateListBox.Location = new System.Drawing.Point(71, 42);
            this.DateListBox.Name = "DateListBox";
            this.DateListBox.Size = new System.Drawing.Size(120, 88);
            this.DateListBox.TabIndex = 3;
            // 
            // dayComboBox
            // 
            this.dayComboBox.FormattingEnabled = true;
            this.dayComboBox.Location = new System.Drawing.Point(71, 18);
            this.dayComboBox.Name = "dayComboBox";
            this.dayComboBox.Size = new System.Drawing.Size(121, 20);
            this.dayComboBox.TabIndex = 4;
            // 
            // TimeComboBox
            // 
            this.TimeComboBox.FormattingEnabled = true;
            this.TimeComboBox.Location = new System.Drawing.Point(71, 134);
            this.TimeComboBox.Name = "TimeComboBox";
            this.TimeComboBox.Size = new System.Drawing.Size(121, 20);
            this.TimeComboBox.TabIndex = 5;
            // 
            // updateCheckBox
            // 
            this.updateCheckBox.AutoSize = true;
            this.updateCheckBox.Location = new System.Drawing.Point(71, 160);
            this.updateCheckBox.Name = "updateCheckBox";
            this.updateCheckBox.Size = new System.Drawing.Size(60, 16);
            this.updateCheckBox.TabIndex = 6;
            this.updateCheckBox.Text = "UpDate";
            this.updateCheckBox.UseVisualStyleBackColor = true;
            this.updateCheckBox.Visible = false;
            // 
            // OKBtn
            // 
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(233, 16);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 7;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(233, 45);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 8;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // HelpBtn
            // 
            this.HelpBtn.Location = new System.Drawing.Point(233, 74);
            this.HelpBtn.Name = "HelpBtn";
            this.HelpBtn.Size = new System.Drawing.Size(75, 23);
            this.HelpBtn.TabIndex = 9;
            this.HelpBtn.Text = "Help";
            this.HelpBtn.UseVisualStyleBackColor = true;
            this.HelpBtn.Visible = false;
            // 
            // InsertDatetimeForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(329, 211);
            this.Controls.Add(this.HelpBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.updateCheckBox);
            this.Controls.Add(this.TimeComboBox);
            this.Controls.Add(this.dayComboBox);
            this.Controls.Add(this.DateListBox);
            this.Controls.Add(this.Timelabel);
            this.Controls.Add(this.Datelabel);
            this.Controls.Add(this.Daylabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertDatetimeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertDatetimeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Daylabel;
        private System.Windows.Forms.Label Datelabel;
        private System.Windows.Forms.Label Timelabel;
        private System.Windows.Forms.ListBox DateListBox;
        private System.Windows.Forms.ComboBox dayComboBox;
        private System.Windows.Forms.ComboBox TimeComboBox;
        private System.Windows.Forms.CheckBox updateCheckBox;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button HelpBtn;
    }
}