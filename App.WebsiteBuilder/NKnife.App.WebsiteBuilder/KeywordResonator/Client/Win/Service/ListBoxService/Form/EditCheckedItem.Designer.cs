namespace Jeelu.KeywordResonator.Client
{
    partial class EditCheckedItem
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
            this.txt_dec = new System.Windows.Forms.TextBox();
            this.txt_src = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lb_dec = new System.Windows.Forms.Label();
            this.lb_src = new System.Windows.Forms.Label();
            this.lb_tip = new System.Windows.Forms.Label();
            this.lb_word = new System.Windows.Forms.Label();
            this.txt_word = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txt_word)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_dec
            // 
            this.txt_dec.Location = new System.Drawing.Point(71, 36);
            this.txt_dec.Multiline = true;
            this.txt_dec.Name = "txt_dec";
            this.txt_dec.Size = new System.Drawing.Size(229, 59);
            this.txt_dec.TabIndex = 0;
            // 
            // txt_src
            // 
            this.txt_src.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_src.Location = new System.Drawing.Point(71, 101);
            this.txt_src.Multiline = true;
            this.txt_src.Name = "txt_src";
            this.txt_src.ReadOnly = true;
            this.txt_src.Size = new System.Drawing.Size(229, 59);
            this.txt_src.TabIndex = 1;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(71, 167);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "确认";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(199, 167);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // lb_dec
            // 
            this.lb_dec.AutoSize = true;
            this.lb_dec.Location = new System.Drawing.Point(12, 59);
            this.lb_dec.Name = "lb_dec";
            this.lb_dec.Size = new System.Drawing.Size(53, 12);
            this.lb_dec.TabIndex = 5;
            this.lb_dec.Text = "新关键字";
            // 
            // lb_src
            // 
            this.lb_src.AutoSize = true;
            this.lb_src.Location = new System.Drawing.Point(12, 117);
            this.lb_src.Name = "lb_src";
            this.lb_src.Size = new System.Drawing.Size(53, 12);
            this.lb_src.TabIndex = 6;
            this.lb_src.Text = "源关键字";
            // 
            // lb_tip
            // 
            this.lb_tip.AutoSize = true;
            this.lb_tip.Location = new System.Drawing.Point(69, 12);
            this.lb_tip.Name = "lb_tip";
            this.lb_tip.Size = new System.Drawing.Size(41, 12);
            this.lb_tip.TabIndex = 4;
            this.lb_tip.Text = "lb_tip";
            // 
            // lb_word
            // 
            this.lb_word.AutoSize = true;
            this.lb_word.Location = new System.Drawing.Point(36, 104);
            this.lb_word.Name = "lb_word";
            this.lb_word.Size = new System.Drawing.Size(29, 12);
            this.lb_word.TabIndex = 7;
            this.lb_word.Text = "词频";
            this.lb_word.Visible = false;
            // 
            // txt_word
            // 
            this.txt_word.Location = new System.Drawing.Point(71, 101);
            this.txt_word.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txt_word.Name = "txt_word";
            this.txt_word.Size = new System.Drawing.Size(229, 21);
            this.txt_word.TabIndex = 8;
            this.txt_word.Visible = false;
            // 
            // EditCheckedItem
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(343, 202);
            this.Controls.Add(this.txt_word);
            this.Controls.Add(this.lb_word);
            this.Controls.Add(this.lb_src);
            this.Controls.Add(this.lb_tip);
            this.Controls.Add(this.lb_dec);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.txt_src);
            this.Controls.Add(this.txt_dec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditCheckedItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditCheckedItem";
            ((System.ComponentModel.ISupportInitialize)(this.txt_word)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_dec;
        private System.Windows.Forms.TextBox txt_src;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lb_dec;
        private System.Windows.Forms.Label lb_src;
        private System.Windows.Forms.Label lb_tip;
        private System.Windows.Forms.Label lb_word;
        private System.Windows.Forms.NumericUpDown txt_word;
    }
}