namespace Jeelu.Win
{
    partial class BlockCssControl
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.css_WordSpace = new Jeelu.Win.CssFieldUnit();
            this.css_CharSpace = new Jeelu.Win.CssFieldUnit();
            this.css_VertiSimilar = new Jeelu.Win.CssFieldUnit();
            this.css_TextIndet = new Jeelu.Win.CssFieldUnit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_Space = new System.Windows.Forms.ComboBox();
            this.cb_Show = new System.Windows.Forms.ComboBox();
            this.cb_TextSimilar = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // css_WordSpace
            // 
            this.css_WordSpace.FieldUnitType = Jeelu.Win.CssFieldUnitType.WordSpacing;
            this.css_WordSpace.Location = new System.Drawing.Point(110, 7);
            this.css_WordSpace.Name = "css_WordSpace";
            this.css_WordSpace.Size = new System.Drawing.Size(194, 23);
            this.css_WordSpace.TabIndex = 0;
            this.css_WordSpace.Value = "";
            // 
            // css_CharSpace
            // 
            this.css_CharSpace.FieldUnitType = Jeelu.Win.CssFieldUnitType.LetterSpacing;
            this.css_CharSpace.Location = new System.Drawing.Point(110, 31);
            this.css_CharSpace.Name = "css_CharSpace";
            this.css_CharSpace.Size = new System.Drawing.Size(194, 23);
            this.css_CharSpace.TabIndex = 0;
            this.css_CharSpace.Value = "";
            // 
            // css_VertiSimilar
            // 
            this.css_VertiSimilar.FieldUnitType = Jeelu.Win.CssFieldUnitType.VerticalAlign;
            this.css_VertiSimilar.Location = new System.Drawing.Point(110, 56);
            this.css_VertiSimilar.Name = "css_VertiSimilar";
            this.css_VertiSimilar.Size = new System.Drawing.Size(194, 23);
            this.css_VertiSimilar.TabIndex = 0;
            this.css_VertiSimilar.Value = "";
            // 
            // css_TextIndet
            // 
            this.css_TextIndet.FieldUnitType = Jeelu.Win.CssFieldUnitType.TextIndent;
            this.css_TextIndet.Location = new System.Drawing.Point(110, 106);
            this.css_TextIndet.Name = "css_TextIndet";
            this.css_TextIndet.Size = new System.Drawing.Size(194, 23);
            this.css_TextIndet.TabIndex = 0;
            this.css_TextIndet.Value = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "单词间距(&S):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "字母间距(&L):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "垂直对齐(&V):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "文本对齐(&T):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "文字缩进(&I):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "空格(&W):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "显示(&D):";
            // 
            // cb_Space
            // 
            this.cb_Space.FormattingEnabled = true;
            this.cb_Space.Location = new System.Drawing.Point(113, 134);
            this.cb_Space.Name = "cb_Space";
            this.cb_Space.Size = new System.Drawing.Size(107, 20);
            this.cb_Space.TabIndex = 2;
            // 
            // cb_Show
            // 
            this.cb_Show.FormattingEnabled = true;
            this.cb_Show.Location = new System.Drawing.Point(113, 161);
            this.cb_Show.Name = "cb_Show";
            this.cb_Show.Size = new System.Drawing.Size(156, 20);
            this.cb_Show.TabIndex = 2;
            // 
            // cb_TextSimilar
            // 
            this.cb_TextSimilar.FormattingEnabled = true;
            this.cb_TextSimilar.Location = new System.Drawing.Point(113, 82);
            this.cb_TextSimilar.Name = "cb_TextSimilar";
            this.cb_TextSimilar.Size = new System.Drawing.Size(107, 20);
            this.cb_TextSimilar.TabIndex = 3;
            // 
            // BlockCssControl
            // 
            this.Controls.Add(this.cb_TextSimilar);
            this.Controls.Add(this.cb_Show);
            this.Controls.Add(this.cb_Space);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.css_TextIndet);
            this.Controls.Add(this.css_VertiSimilar);
            this.Controls.Add(this.css_CharSpace);
            this.Controls.Add(this.css_WordSpace);
            this.Name = "BlockCssControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CssFieldUnit css_CharSpace;
        private CssFieldUnit css_VertiSimilar;
        private CssFieldUnit css_TextIndet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_Space;
        private System.Windows.Forms.ComboBox cb_Show;
        private System.Windows.Forms.ComboBox cb_TextSimilar;
        private CssFieldUnit css_WordSpace;
    }
}
