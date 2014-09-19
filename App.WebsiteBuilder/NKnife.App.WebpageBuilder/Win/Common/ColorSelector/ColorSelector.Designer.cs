namespace Jeelu.Win
{
    partial class ColorSelector
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHtmlValue = new System.Windows.Forms.TextBox();
            this.bottomColorPad1 = new Jeelu.Win.BottomColorPad();
            this.colorSlideBar1 = new Jeelu.Win.ColorSlideBar();
            this.colorPad1 = new Jeelu.Win.ColorPad();
            this.txtR = new System.Windows.Forms.NumericUpDown();
            this.txtG = new System.Windows.Forms.NumericUpDown();
            this.txtB = new System.Windows.Forms.NumericUpDown();
            this.txtA = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtA)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "红:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "绿:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "蓝:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Alpha:";
            // 
            // txtHtmlValue
            // 
            this.txtHtmlValue.Location = new System.Drawing.Point(122, 78);
            this.txtHtmlValue.Name = "txtHtmlValue";
            this.txtHtmlValue.Size = new System.Drawing.Size(65, 21);
            this.txtHtmlValue.TabIndex = 3;
            this.txtHtmlValue.Text = "#000000";
            // 
            // bottomColorPad1
            // 
            this.bottomColorPad1.Location = new System.Drawing.Point(12, 125);
            this.bottomColorPad1.Name = "bottomColorPad1";
            this.bottomColorPad1.OwnerColorSelector = null;
            this.bottomColorPad1.Size = new System.Drawing.Size(205, 33);
            this.bottomColorPad1.TabIndex = 5;
            this.bottomColorPad1.Text = "bottomColorPad1";
            // 
            // colorSlideBar1
            // 
            this.colorSlideBar1.Location = new System.Drawing.Point(192, 12);
            this.colorSlideBar1.Name = "colorSlideBar1";
            this.colorSlideBar1.OwnerColorSelector = null;
            this.colorSlideBar1.Size = new System.Drawing.Size(25, 60);
            this.colorSlideBar1.TabIndex = 4;
            this.colorSlideBar1.Text = "colorSlideBar1";
            // 
            // colorPad1
            // 
            this.colorPad1.Location = new System.Drawing.Point(122, 12);
            this.colorPad1.Name = "colorPad1";
            this.colorPad1.OwnerColorSelector = null;
            this.colorPad1.Size = new System.Drawing.Size(65, 65);
            this.colorPad1.TabIndex = 2;
            this.colorPad1.Text = "colorPad1";
            // 
            // txtR
            // 
            this.txtR.Location = new System.Drawing.Point(49, 11);
            this.txtR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(53, 21);
            this.txtR.TabIndex = 6;
            // 
            // txtG
            // 
            this.txtG.Location = new System.Drawing.Point(49, 33);
            this.txtG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.txtG.Name = "txtG";
            this.txtG.Size = new System.Drawing.Size(53, 21);
            this.txtG.TabIndex = 6;
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(49, 55);
            this.txtB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(53, 21);
            this.txtB.TabIndex = 6;
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(49, 77);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(53, 21);
            this.txtA.TabIndex = 6;
            // 
            // ColorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtG);
            this.Controls.Add(this.txtR);
            this.Controls.Add(this.bottomColorPad1);
            this.Controls.Add(this.colorSlideBar1);
            this.Controls.Add(this.txtHtmlValue);
            this.Controls.Add(this.colorPad1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ColorSelector";
            this.Size = new System.Drawing.Size(228, 168);
            ((System.ComponentModel.ISupportInitialize)(this.txtR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ColorPad colorPad1;
        private System.Windows.Forms.TextBox txtHtmlValue;
        private ColorSlideBar colorSlideBar1;
        private BottomColorPad bottomColorPad1;
        private System.Windows.Forms.NumericUpDown txtR;
        private System.Windows.Forms.NumericUpDown txtG;
        private System.Windows.Forms.NumericUpDown txtB;
        private System.Windows.Forms.NumericUpDown txtA;
    }
}
