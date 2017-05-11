namespace NKnife.Kits.WinFormUIKit.Forms
{
    partial class ChineseCharUseFrequencyDockView
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
            this.button1 = new System.Windows.Forms.Button();
            this._CahrTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._StringTextbox = new System.Windows.Forms.TextBox();
            this._Fre1TextBox = new System.Windows.Forms.TextBox();
            this._Fre2TextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._TimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.button1.Location = new System.Drawing.Point(31, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _CahrTextBox
            // 
            this._CahrTextBox.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._CahrTextBox.Location = new System.Drawing.Point(65, 36);
            this._CahrTextBox.Name = "_CahrTextBox";
            this._CahrTextBox.Size = new System.Drawing.Size(116, 21);
            this._CahrTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "汉字";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label2.Location = new System.Drawing.Point(28, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "频率";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._Fre1TextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._CahrTextBox);
            this.groupBox1.Location = new System.Drawing.Point(38, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 227);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单字查询";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._Fre2TextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this._StringTextbox);
            this.groupBox2.Location = new System.Drawing.Point(287, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 227);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "多字查询";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label3.Location = new System.Drawing.Point(36, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "汉字";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.button2.Location = new System.Drawing.Point(39, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 30);
            this.button2.TabIndex = 0;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label5.Location = new System.Drawing.Point(36, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "频率";
            // 
            // _StringTextbox
            // 
            this._StringTextbox.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._StringTextbox.Location = new System.Drawing.Point(69, 33);
            this._StringTextbox.Multiline = true;
            this._StringTextbox.Name = "_StringTextbox";
            this._StringTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._StringTextbox.Size = new System.Drawing.Size(315, 54);
            this._StringTextbox.TabIndex = 1;
            // 
            // _Fre1TextBox
            // 
            this._Fre1TextBox.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._Fre1TextBox.Location = new System.Drawing.Point(65, 93);
            this._Fre1TextBox.Multiline = true;
            this._Fre1TextBox.Name = "_Fre1TextBox";
            this._Fre1TextBox.ReadOnly = true;
            this._Fre1TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._Fre1TextBox.Size = new System.Drawing.Size(116, 72);
            this._Fre1TextBox.TabIndex = 4;
            // 
            // _Fre2TextBox
            // 
            this._Fre2TextBox.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._Fre2TextBox.Location = new System.Drawing.Point(69, 93);
            this._Fre2TextBox.Multiline = true;
            this._Fre2TextBox.Name = "_Fre2TextBox";
            this._Fre2TextBox.ReadOnly = true;
            this._Fre2TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._Fre2TextBox.Size = new System.Drawing.Size(315, 82);
            this._Fre2TextBox.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._TimeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 276);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(726, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "耗时:";
            // 
            // _TimeLabel
            // 
            this._TimeLabel.Name = "_TimeLabel";
            this._TimeLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ChineseCharUseFrequencyDockView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 298);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChineseCharUseFrequencyDockView";
            this.ShowIcon = false;
            this.Text = "汉字使用频率";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox _CahrTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _Fre1TextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _Fre2TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _StringTextbox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _TimeLabel;
    }
}