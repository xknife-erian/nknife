namespace NKnife.StarterKit.Forms
{
    partial class ChineseCharForm
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
            this._ChineseCharTextbox = new System.Windows.Forms.TextBox();
            this._PinyinTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ChineseCharTextbox
            // 
            this._ChineseCharTextbox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this._ChineseCharTextbox.Location = new System.Drawing.Point(90, 42);
            this._ChineseCharTextbox.Multiline = true;
            this._ChineseCharTextbox.Name = "_ChineseCharTextbox";
            this._ChineseCharTextbox.Size = new System.Drawing.Size(266, 136);
            this._ChineseCharTextbox.TabIndex = 1;
            this._ChineseCharTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _PinyinTextbox
            // 
            this._PinyinTextbox.Font = new System.Drawing.Font("Calibri", 18F);
            this._PinyinTextbox.Location = new System.Drawing.Point(90, 210);
            this._PinyinTextbox.Name = "_PinyinTextbox";
            this._PinyinTextbox.Size = new System.Drawing.Size(266, 37);
            this._PinyinTextbox.TabIndex = 0;
            this._PinyinTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._PinyinTextbox.TextChanged += new System.EventHandler(this.PinyinInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入拼音";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "拼音对应的汉字";
            // 
            // _ClearButton
            // 
            this._ClearButton.Location = new System.Drawing.Point(185, 268);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(75, 23);
            this._ClearButton.TabIndex = 2;
            this._ClearButton.Text = "Clear";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this._ClearButton_Click);
            // 
            // ChineseCharForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 315);
            this.Controls.Add(this._ClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._PinyinTextbox);
            this.Controls.Add(this._ChineseCharTextbox);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChineseCharForm";
            this.Text = "ChineseCharForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _ChineseCharTextbox;
        private System.Windows.Forms.TextBox _PinyinTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _ClearButton;
    }
}