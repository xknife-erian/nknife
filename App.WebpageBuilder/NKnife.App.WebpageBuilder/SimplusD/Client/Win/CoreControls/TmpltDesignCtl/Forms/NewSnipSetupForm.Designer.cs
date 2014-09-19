namespace Jeelu.SimplusD.Client.Win
{
    partial class NewSnipSetupForm
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
            this.radioButtonContent = new System.Windows.Forms.RadioButton();
            this.radioButtonGenerel = new System.Windows.Forms.RadioButton();
            this.groupBoxDoWhat = new System.Windows.Forms.GroupBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelSnipName = new System.Windows.Forms.Label();
            this.textBoxSnipName = new System.Windows.Forms.TextBox();
            this.groupBoxDoWhat.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonContent
            // 
            this.radioButtonContent.AutoSize = true;
            this.radioButtonContent.Location = new System.Drawing.Point(179, 20);
            this.radioButtonContent.Name = "radioButtonContent";
            this.radioButtonContent.Size = new System.Drawing.Size(109, 17);
            this.radioButtonContent.TabIndex = 1;
            this.radioButtonContent.Text = "新建正文页面片";
            this.radioButtonContent.UseVisualStyleBackColor = true;
            this.radioButtonContent.CheckedChanged += new System.EventHandler(this.radioButtonContent_CheckedChanged);
            // 
            // radioButtonGenerel
            // 
            this.radioButtonGenerel.AutoSize = true;
            this.radioButtonGenerel.Checked = true;
            this.radioButtonGenerel.Location = new System.Drawing.Point(52, 20);
            this.radioButtonGenerel.Name = "radioButtonGenerel";
            this.radioButtonGenerel.Size = new System.Drawing.Size(109, 17);
            this.radioButtonGenerel.TabIndex = 2;
            this.radioButtonGenerel.TabStop = true;
            this.radioButtonGenerel.Text = "新建普通页面片";
            this.radioButtonGenerel.UseVisualStyleBackColor = true;
            this.radioButtonGenerel.CheckedChanged += new System.EventHandler(this.radioButtonGenerel_CheckedChanged);
            // 
            // groupBoxDoWhat
            // 
            this.groupBoxDoWhat.Controls.Add(this.radioButtonGenerel);
            this.groupBoxDoWhat.Controls.Add(this.radioButtonContent);
            this.groupBoxDoWhat.Location = new System.Drawing.Point(15, 33);
            this.groupBoxDoWhat.Name = "groupBoxDoWhat";
            this.groupBoxDoWhat.Size = new System.Drawing.Size(340, 43);
            this.groupBoxDoWhat.TabIndex = 3;
            this.groupBoxDoWhat.TabStop = false;
            this.groupBoxDoWhat.Text = "新建/修改";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEnter.BackColor = System.Drawing.SystemColors.Control;
            this.buttonEnter.Location = new System.Drawing.Point(249, 82);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(50, 23);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = "确 定";
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(305, 82);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(50, 23);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "取 消";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelSnipName
            // 
            this.labelSnipName.AutoSize = true;
            this.labelSnipName.Location = new System.Drawing.Point(12, 9);
            this.labelSnipName.Name = "labelSnipName";
            this.labelSnipName.Size = new System.Drawing.Size(79, 13);
            this.labelSnipName.TabIndex = 7;
            this.labelSnipName.Text = "页面片名称：";
            // 
            // textBoxSnipName
            // 
            this.textBoxSnipName.Location = new System.Drawing.Point(97, 6);
            this.textBoxSnipName.Name = "textBoxSnipName";
            this.textBoxSnipName.Size = new System.Drawing.Size(258, 21);
            this.textBoxSnipName.TabIndex = 8;
            this.textBoxSnipName.Text = "新页面片";
            this.textBoxSnipName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NewSnipSetupForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(368, 112);
            this.Controls.Add(this.textBoxSnipName);
            this.Controls.Add(this.labelSnipName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.groupBoxDoWhat);
            this.Name = "NewSnipSetupForm";
            this.Text = "设置页面片选项";
            this.groupBoxDoWhat.ResumeLayout(false);
            this.groupBoxDoWhat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonContent;
        private System.Windows.Forms.RadioButton radioButtonGenerel;
        private System.Windows.Forms.GroupBox groupBoxDoWhat;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelSnipName;
        private System.Windows.Forms.TextBox textBoxSnipName;

    }
}