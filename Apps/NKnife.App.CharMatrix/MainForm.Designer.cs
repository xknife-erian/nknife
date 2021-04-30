namespace NKnife.App.CharMatrix
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
            this.tvList = new System.Windows.Forms.TreeView();
            this.tbWords = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numSpacing = new System.Windows.Forms.NumericUpDown();
            this.fontDlg = new System.Windows.Forms.FontDialog();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblSp = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpacing)).BeginInit();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // tvList
            // 
            this.tvList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvList.BackColor = System.Drawing.SystemColors.Info;
            this.tvList.HideSelection = false;
            this.tvList.Location = new System.Drawing.Point(611, 1);
            this.tvList.Name = "tvList";
            this.tvList.Size = new System.Drawing.Size(180, 565);
            this.tvList.TabIndex = 0;
            this.tvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvList_AfterSelect);
            // 
            // tbWords
            // 
            this.tbWords.Location = new System.Drawing.Point(561, 12);
            this.tbWords.Name = "tbWords";
            this.tbWords.Size = new System.Drawing.Size(150, 21);
            this.tbWords.TabIndex = 1;
            this.tbWords.Text = "程序员小刀";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(717, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // numX
            // 
            this.numX.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numX.Location = new System.Drawing.Point(29, 12);
            this.numX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(50, 21);
            this.numX.TabIndex = 3;
            this.numX.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numY
            // 
            this.numY.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numY.Location = new System.Drawing.Point(103, 13);
            this.numY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(50, 21);
            this.numY.TabIndex = 3;
            this.numY.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numSpacing
            // 
            this.numSpacing.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSpacing.Location = new System.Drawing.Point(196, 13);
            this.numSpacing.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSpacing.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numSpacing.Name = "numSpacing";
            this.numSpacing.Size = new System.Drawing.Size(50, 21);
            this.numSpacing.TabIndex = 3;
            this.numSpacing.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(9, 19);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(25, 13);
            this.lblX.TabIndex = 4;
            this.lblX.Text = "X：";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(83, 19);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(25, 13);
            this.lblY.TabIndex = 4;
            this.lblY.Text = "Y：";
            // 
            // lblSp
            // 
            this.lblSp.AutoSize = true;
            this.lblSp.Location = new System.Drawing.Point(158, 19);
            this.lblSp.Name = "lblSp";
            this.lblSp.Size = new System.Drawing.Size(43, 13);
            this.lblSp.TabIndex = 4;
            this.lblSp.Text = "间隔：";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(524, 19);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(43, 13);
            this.lblText.TabIndex = 4;
            this.lblText.Text = "文本：";
            // 
            // pnl
            // 
            this.pnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl.BackColor = System.Drawing.Color.Gainsboro;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.numWidth);
            this.pnl.Controls.Add(this.tbFont);
            this.pnl.Controls.Add(this.numY);
            this.pnl.Controls.Add(this.lblWidth);
            this.pnl.Controls.Add(this.lblY);
            this.pnl.Controls.Add(this.numX);
            this.pnl.Controls.Add(this.numSpacing);
            this.pnl.Controls.Add(this.lblFont);
            this.pnl.Controls.Add(this.lblSp);
            this.pnl.Controls.Add(this.lblX);
            this.pnl.Controls.Add(this.btnFont);
            this.pnl.Controls.Add(this.btnOK);
            this.pnl.Controls.Add(this.tbWords);
            this.pnl.Controls.Add(this.lblText);
            this.pnl.Location = new System.Drawing.Point(-8, 568);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(811, 56);
            this.pnl.TabIndex = 5;
            // 
            // numWidth
            // 
            this.numWidth.DecimalPlaces = 1;
            this.numWidth.Location = new System.Drawing.Point(468, 13);
            this.numWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(50, 21);
            this.numWidth.TabIndex = 3;
            this.numWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbFont
            // 
            this.tbFont.BackColor = System.Drawing.SystemColors.Window;
            this.tbFont.Location = new System.Drawing.Point(288, 13);
            this.tbFont.Name = "tbFont";
            this.tbFont.ReadOnly = true;
            this.tbFont.Size = new System.Drawing.Size(100, 21);
            this.tbFont.TabIndex = 1;
            this.tbFont.DoubleClick += new System.EventHandler(this.tbFont_DoubleClick);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(430, 19);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(43, 13);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "线宽：";
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(250, 19);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(43, 13);
            this.lblFont.TabIndex = 4;
            this.lblFont.Text = "字体：";
            // 
            // btnFont
            // 
            this.btnFont.Location = new System.Drawing.Point(392, 12);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(32, 25);
            this.btnFont.TabIndex = 2;
            this.btnFont.Text = "...";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 616);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.tvList);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrueTypeText";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpacing)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvList;
        private System.Windows.Forms.TextBox tbWords;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numSpacing;
        private System.Windows.Forms.FontDialog fontDlg;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblSp;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblWidth;
    }
}

