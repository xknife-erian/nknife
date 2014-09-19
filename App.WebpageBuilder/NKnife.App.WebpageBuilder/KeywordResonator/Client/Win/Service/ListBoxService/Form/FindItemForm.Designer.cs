namespace Jeelu.KeywordResonator.Client
{
    partial class FindItemForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Source = new System.Windows.Forms.TextBox();
            this.chk_MatchWord = new System.Windows.Forms.CheckBox();
            this.chk_CaseSensitive = new System.Windows.Forms.CheckBox();
            this.btn_FindNext = new System.Windows.Forms.Button();
            this.btn_FindPrev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "查找(&F):";
            // 
            // txt_Source
            // 
            this.txt_Source.Location = new System.Drawing.Point(61, 10);
            this.txt_Source.Name = "txt_Source";
            this.txt_Source.Size = new System.Drawing.Size(284, 21);
            this.txt_Source.TabIndex = 0;
            // 
            // chk_MatchWord
            // 
            this.chk_MatchWord.AutoSize = true;
            this.chk_MatchWord.Location = new System.Drawing.Point(61, 45);
            this.chk_MatchWord.Name = "chk_MatchWord";
            this.chk_MatchWord.Size = new System.Drawing.Size(90, 16);
            this.chk_MatchWord.TabIndex = 3;
            this.chk_MatchWord.Text = "全字匹配(&W)";
            this.chk_MatchWord.UseVisualStyleBackColor = true;
            // 
            // chk_CaseSensitive
            // 
            this.chk_CaseSensitive.AutoSize = true;
            this.chk_CaseSensitive.Location = new System.Drawing.Point(176, 45);
            this.chk_CaseSensitive.Name = "chk_CaseSensitive";
            this.chk_CaseSensitive.Size = new System.Drawing.Size(102, 16);
            this.chk_CaseSensitive.TabIndex = 2;
            this.chk_CaseSensitive.Text = "区分大小写(&C)";
            this.chk_CaseSensitive.UseVisualStyleBackColor = true;
            // 
            // btn_FindNext
            // 
            this.btn_FindNext.Location = new System.Drawing.Point(254, 73);
            this.btn_FindNext.Name = "btn_FindNext";
            this.btn_FindNext.Size = new System.Drawing.Size(75, 23);
            this.btn_FindNext.TabIndex = 1;
            this.btn_FindNext.Text = "下一个(&N)";
            this.btn_FindNext.UseVisualStyleBackColor = true;
            this.btn_FindNext.Click += new System.EventHandler(this.btn_FindNext_Click);
            // 
            // btn_FindPrev
            // 
            this.btn_FindPrev.Location = new System.Drawing.Point(159, 73);
            this.btn_FindPrev.Name = "btn_FindPrev";
            this.btn_FindPrev.Size = new System.Drawing.Size(75, 23);
            this.btn_FindPrev.TabIndex = 1;
            this.btn_FindPrev.Text = "上一个(&P)";
            this.btn_FindPrev.UseVisualStyleBackColor = true;
            this.btn_FindPrev.Click += new System.EventHandler(this.btn_FindPrev_Click);
            // 
            // FindItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 108);
            this.Controls.Add(this.btn_FindPrev);
            this.Controls.Add(this.btn_FindNext);
            this.Controls.Add(this.chk_CaseSensitive);
            this.Controls.Add(this.chk_MatchWord);
            this.Controls.Add(this.txt_Source);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindItemForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "查找";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindItemForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Source;
        private System.Windows.Forms.CheckBox chk_MatchWord;
        private System.Windows.Forms.CheckBox chk_CaseSensitive;
        private System.Windows.Forms.Button btn_FindNext;
        private System.Windows.Forms.Button btn_FindPrev;
    }
}