namespace NKnife.Win.Forms
{
    partial class TextArrayDialog
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
            this._CancelButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._TabControl = new System.Windows.Forms.TabControl();
            this._TabPage = new System.Windows.Forms.TabPage();
            this._DownButton = new System.Windows.Forms.Button();
            this._UpButton = new System.Windows.Forms.Button();
            this._DeleteButton = new System.Windows.Forms.Button();
            this._AddButton = new System.Windows.Forms.Button();
            this._AgreeButton = new System.Windows.Forms.Button();
            this._CurrentItemValueTextBox = new System.Windows.Forms.TextBox();
            this._ListView = new System.Windows.Forms.ListView();
            this._IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._DataColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._TabControl.SuspendLayout();
            this._TabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(170, 346);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(52, 28);
            this._CancelButton.TabIndex = 0;
            this._CancelButton.Text = "取消";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(111, 346);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(52, 28);
            this._AcceptButton.TabIndex = 1;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // _TabControl
            // 
            this._TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TabControl.Controls.Add(this._TabPage);
            this._TabControl.Location = new System.Drawing.Point(4, 4);
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(297, 337);
            this._TabControl.TabIndex = 4;
            // 
            // _TabPage
            // 
            this._TabPage.Controls.Add(this._DownButton);
            this._TabPage.Controls.Add(this._UpButton);
            this._TabPage.Controls.Add(this._DeleteButton);
            this._TabPage.Controls.Add(this._AddButton);
            this._TabPage.Controls.Add(this._AgreeButton);
            this._TabPage.Controls.Add(this._CurrentItemValueTextBox);
            this._TabPage.Controls.Add(this._ListView);
            this._TabPage.Location = new System.Drawing.Point(4, 26);
            this._TabPage.Name = "_TabPage";
            this._TabPage.Padding = new System.Windows.Forms.Padding(3);
            this._TabPage.Size = new System.Drawing.Size(289, 307);
            this._TabPage.TabIndex = 0;
            this._TabPage.Text = "数据";
            this._TabPage.UseVisualStyleBackColor = true;
            // 
            // _DownButton
            // 
            this._DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DownButton.Enabled = false;
            this._DownButton.Location = new System.Drawing.Point(220, 79);
            this._DownButton.Name = "_DownButton";
            this._DownButton.Size = new System.Drawing.Size(66, 23);
            this._DownButton.TabIndex = 7;
            this._DownButton.Text = "下移";
            this._DownButton.UseVisualStyleBackColor = true;
            this._DownButton.Click += new System.EventHandler(this._DownButton_Click);
            // 
            // _UpButton
            // 
            this._UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._UpButton.Enabled = false;
            this._UpButton.Location = new System.Drawing.Point(220, 55);
            this._UpButton.Name = "_UpButton";
            this._UpButton.Size = new System.Drawing.Size(66, 23);
            this._UpButton.TabIndex = 6;
            this._UpButton.Text = "上移";
            this._UpButton.UseVisualStyleBackColor = true;
            this._UpButton.Click += new System.EventHandler(this._UpButton_Click);
            // 
            // _DeleteButton
            // 
            this._DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DeleteButton.Enabled = false;
            this._DeleteButton.Location = new System.Drawing.Point(220, 26);
            this._DeleteButton.Name = "_DeleteButton";
            this._DeleteButton.Size = new System.Drawing.Size(66, 23);
            this._DeleteButton.TabIndex = 4;
            this._DeleteButton.Text = "删除";
            this._DeleteButton.UseVisualStyleBackColor = true;
            this._DeleteButton.Click += new System.EventHandler(this._DeleteButton_Click);
            // 
            // _AddButton
            // 
            this._AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._AddButton.Location = new System.Drawing.Point(220, 2);
            this._AddButton.Name = "_AddButton";
            this._AddButton.Size = new System.Drawing.Size(66, 23);
            this._AddButton.TabIndex = 3;
            this._AddButton.Text = "添加";
            this._AddButton.UseVisualStyleBackColor = true;
            this._AddButton.Click += new System.EventHandler(this._AddButton_Click);
            // 
            // _AgreeButton
            // 
            this._AgreeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AgreeButton.Enabled = false;
            this._AgreeButton.Location = new System.Drawing.Point(217, 275);
            this._AgreeButton.Name = "_AgreeButton";
            this._AgreeButton.Size = new System.Drawing.Size(66, 23);
            this._AgreeButton.TabIndex = 1;
            this._AgreeButton.Text = "应用";
            this._AgreeButton.UseVisualStyleBackColor = true;
            this._AgreeButton.Click += new System.EventHandler(this._AgreeButton_Click);
            // 
            // _CurrentItemValueTextBox
            // 
            this._CurrentItemValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CurrentItemValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._CurrentItemValueTextBox.Location = new System.Drawing.Point(3, 275);
            this._CurrentItemValueTextBox.Name = "_CurrentItemValueTextBox";
            this._CurrentItemValueTextBox.Size = new System.Drawing.Size(211, 23);
            this._CurrentItemValueTextBox.TabIndex = 0;
            // 
            // _ListView
            // 
            this._ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._IndexColumnHeader,
            this._DataColumnHeader});
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HideSelection = false;
            this._ListView.Location = new System.Drawing.Point(3, 3);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(211, 266);
            this._ListView.TabIndex = 2;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // _IndexColumnHeader
            // 
            this._IndexColumnHeader.Text = "";
            this._IndexColumnHeader.Width = 30;
            // 
            // _DataColumnHeader
            // 
            this._DataColumnHeader.Text = "Data";
            this._DataColumnHeader.Width = 150;
            // 
            // TextArrayDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(302, 383);
            this.Controls.Add(this._TabControl);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(120, 149);
            this.Name = "TextArrayDialog";
            this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TextArrayDialog";
            this._TabControl.ResumeLayout(false);
            this._TabPage.ResumeLayout(false);
            this._TabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.TabControl _TabControl;
        private System.Windows.Forms.TabPage _TabPage;
        private System.Windows.Forms.TextBox _CurrentItemValueTextBox;
        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.Button _AgreeButton;
        private System.Windows.Forms.ColumnHeader _IndexColumnHeader;
        private System.Windows.Forms.ColumnHeader _DataColumnHeader;
        private System.Windows.Forms.Button _UpButton;
        private System.Windows.Forms.Button _DeleteButton;
        private System.Windows.Forms.Button _AddButton;
        private System.Windows.Forms.Button _DownButton;
    }
}