namespace NKnife.App.MathsExercise
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._三个数的加减口算Button = new System.Windows.Forms.Button();
            this._NumericBox = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._两个数的加减口算Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _三个数的加减口算Button
            // 
            this._三个数的加减口算Button.Location = new System.Drawing.Point(24, 71);
            this._三个数的加减口算Button.Margin = new System.Windows.Forms.Padding(4);
            this._三个数的加减口算Button.Name = "_三个数的加减口算Button";
            this._三个数的加减口算Button.Size = new System.Drawing.Size(162, 28);
            this._三个数的加减口算Button.TabIndex = 0;
            this._三个数的加减口算Button.Text = "三个数的加减口算";
            this._三个数的加减口算Button.UseVisualStyleBackColor = true;
            this._三个数的加减口算Button.Click += new System.EventHandler(this._三个数的加减口算Button_Click);
            // 
            // _NumericBox
            // 
            this._NumericBox.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._NumericBox.Location = new System.Drawing.Point(10, 24);
            this._NumericBox.Maximum = new decimal(new int[] {
            512000,
            0,
            0,
            0});
            this._NumericBox.Name = "_NumericBox";
            this._NumericBox.Size = new System.Drawing.Size(428, 25);
            this._NumericBox.TabIndex = 1;
            this._NumericBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._NumericBox.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this._AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(472, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ExitToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // _ExitToolStripMenuItem
            // 
            this._ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            this._ExitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this._ExitToolStripMenuItem.Text = "退出(&X)";
            this._ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // _AboutToolStripMenuItem
            // 
            this._AboutToolStripMenuItem.Name = "_AboutToolStripMenuItem";
            this._AboutToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this._AboutToolStripMenuItem.Text = "关于(&A)";
            this._AboutToolStripMenuItem.Click += new System.EventHandler(this._AboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(472, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _两个数的加减口算Button
            // 
            this._两个数的加减口算Button.Location = new System.Drawing.Point(24, 33);
            this._两个数的加减口算Button.Name = "_两个数的加减口算Button";
            this._两个数的加减口算Button.Size = new System.Drawing.Size(162, 28);
            this._两个数的加减口算Button.TabIndex = 4;
            this._两个数的加减口算Button.Text = "两个数的加减口算";
            this._两个数的加减口算Button.UseVisualStyleBackColor = true;
            this._两个数的加减口算Button.Click += new System.EventHandler(this._两个数的加减口算Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._NumericBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 62);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出题数量：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._两个数的加减口算Button);
            this.groupBox2.Controls.Add(this._三个数的加减口算Button);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 121);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "加减法(100以内)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 325);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小学生数学口算练习生成器";
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _三个数的加减口算Button;
        private System.Windows.Forms.NumericUpDown _NumericBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutToolStripMenuItem;
        private System.Windows.Forms.Button _两个数的加减口算Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

