using NKnife.Tools.Robot.CubeOctopus.Base;

namespace NKnife.Tools.Robot.CubeOctopus
{
    partial class CubeExplorerForm
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
            this._LeftCW90Button = new System.Windows.Forms.Button();
            this._RightCCW90Button = new System.Windows.Forms.Button();
            this._RightCW90Button = new System.Windows.Forms.Button();
            this._LeftCCW90Button = new System.Windows.Forms.Button();
            this._UpCW90Button = new System.Windows.Forms.Button();
            this._UpCCW90Button = new System.Windows.Forms.Button();
            this._DownCW90Button = new System.Windows.Forms.Button();
            this._DwonCCW90Button = new System.Windows.Forms.Button();
            this._BackCW90Button = new System.Windows.Forms.Button();
            this._BackCCW90Button = new System.Windows.Forms.Button();
            this._FrontCW90Button = new System.Windows.Forms.Button();
            this._FrontCCW90Button = new System.Windows.Forms.Button();
            this._RandomButton = new System.Windows.Forms.Button();
            this._InitButton = new System.Windows.Forms.Button();
            this._RandomStepNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._InTextbox = new System.Windows.Forms.TextBox();
            this._OutTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._RandomStepTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._CubePaper = new CubePaperControl();
            this._SolutionButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._RandomStepNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // _LeftCW90Button
            // 
            this._LeftCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._LeftCW90Button.Location = new System.Drawing.Point(804, 12);
            this._LeftCW90Button.Name = "_LeftCW90Button";
            this._LeftCW90Button.Size = new System.Drawing.Size(151, 31);
            this._LeftCW90Button.TabIndex = 1;
            this._LeftCW90Button.Text = "[ L ]左顺";
            this._LeftCW90Button.UseVisualStyleBackColor = true;
            this._LeftCW90Button.Click += new System.EventHandler(this.LeftCW90ButtonClick);
            // 
            // _RightCCW90Button
            // 
            this._RightCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RightCCW90Button.Location = new System.Drawing.Point(804, 123);
            this._RightCCW90Button.Name = "_RightCCW90Button";
            this._RightCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._RightCCW90Button.TabIndex = 2;
            this._RightCCW90Button.Text = "[ R\' ]右逆";
            this._RightCCW90Button.UseVisualStyleBackColor = true;
            this._RightCCW90Button.Click += new System.EventHandler(this.RightCCW90ButtonClick);
            // 
            // _RightCW90Button
            // 
            this._RightCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RightCW90Button.Location = new System.Drawing.Point(804, 86);
            this._RightCW90Button.Name = "_RightCW90Button";
            this._RightCW90Button.Size = new System.Drawing.Size(151, 31);
            this._RightCW90Button.TabIndex = 3;
            this._RightCW90Button.Text = "[ R ]右顺";
            this._RightCW90Button.UseVisualStyleBackColor = true;
            this._RightCW90Button.Click += new System.EventHandler(this.RightCW90ButtonClick);
            // 
            // _LeftCCW90Button
            // 
            this._LeftCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._LeftCCW90Button.Location = new System.Drawing.Point(804, 49);
            this._LeftCCW90Button.Name = "_LeftCCW90Button";
            this._LeftCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._LeftCCW90Button.TabIndex = 4;
            this._LeftCCW90Button.Text = "[ L\' ]左逆";
            this._LeftCCW90Button.UseVisualStyleBackColor = true;
            this._LeftCCW90Button.Click += new System.EventHandler(this.LeftCCW90ButtonClick);
            // 
            // _UpCW90Button
            // 
            this._UpCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._UpCW90Button.Location = new System.Drawing.Point(804, 169);
            this._UpCW90Button.Name = "_UpCW90Button";
            this._UpCW90Button.Size = new System.Drawing.Size(151, 31);
            this._UpCW90Button.TabIndex = 6;
            this._UpCW90Button.Text = "[ U ]上顺";
            this._UpCW90Button.UseVisualStyleBackColor = true;
            this._UpCW90Button.Click += new System.EventHandler(this.UpCW90ButtonClick);
            // 
            // _UpCCW90Button
            // 
            this._UpCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._UpCCW90Button.Location = new System.Drawing.Point(804, 206);
            this._UpCCW90Button.Name = "_UpCCW90Button";
            this._UpCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._UpCCW90Button.TabIndex = 5;
            this._UpCCW90Button.Text = "[ U\' ]上逆";
            this._UpCCW90Button.UseVisualStyleBackColor = true;
            this._UpCCW90Button.Click += new System.EventHandler(this.UpCCW90ButtonClick);
            // 
            // _DownCW90Button
            // 
            this._DownCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DownCW90Button.Location = new System.Drawing.Point(804, 243);
            this._DownCW90Button.Name = "_DownCW90Button";
            this._DownCW90Button.Size = new System.Drawing.Size(151, 31);
            this._DownCW90Button.TabIndex = 8;
            this._DownCW90Button.Text = "[ D ]下顺";
            this._DownCW90Button.UseVisualStyleBackColor = true;
            this._DownCW90Button.Click += new System.EventHandler(this.DownCW90ButtonClick);
            // 
            // _DwonCCW90Button
            // 
            this._DwonCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._DwonCCW90Button.Location = new System.Drawing.Point(804, 280);
            this._DwonCCW90Button.Name = "_DwonCCW90Button";
            this._DwonCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._DwonCCW90Button.TabIndex = 7;
            this._DwonCCW90Button.Text = "[ D\' ]下逆";
            this._DwonCCW90Button.UseVisualStyleBackColor = true;
            this._DwonCCW90Button.Click += new System.EventHandler(this.DwonCCW90ButtonClick);
            // 
            // _BackCW90Button
            // 
            this._BackCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BackCW90Button.Location = new System.Drawing.Point(804, 402);
            this._BackCW90Button.Name = "_BackCW90Button";
            this._BackCW90Button.Size = new System.Drawing.Size(151, 31);
            this._BackCW90Button.TabIndex = 12;
            this._BackCW90Button.Text = "[ B ]后顺";
            this._BackCW90Button.UseVisualStyleBackColor = true;
            this._BackCW90Button.Click += new System.EventHandler(this.BackCW90ButtonClick);
            // 
            // _BackCCW90Button
            // 
            this._BackCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BackCCW90Button.Location = new System.Drawing.Point(804, 439);
            this._BackCCW90Button.Name = "_BackCCW90Button";
            this._BackCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._BackCCW90Button.TabIndex = 11;
            this._BackCCW90Button.Text = "[ B\' ]后逆";
            this._BackCCW90Button.UseVisualStyleBackColor = true;
            this._BackCCW90Button.Click += new System.EventHandler(this.BackCCW90ButtonClick);
            // 
            // _FrontCW90Button
            // 
            this._FrontCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._FrontCW90Button.Location = new System.Drawing.Point(804, 328);
            this._FrontCW90Button.Name = "_FrontCW90Button";
            this._FrontCW90Button.Size = new System.Drawing.Size(151, 31);
            this._FrontCW90Button.TabIndex = 10;
            this._FrontCW90Button.Text = "[ F ]前顺";
            this._FrontCW90Button.UseVisualStyleBackColor = true;
            this._FrontCW90Button.Click += new System.EventHandler(this.FrontCW90ButtonClick);
            // 
            // _FrontCCW90Button
            // 
            this._FrontCCW90Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._FrontCCW90Button.Location = new System.Drawing.Point(804, 365);
            this._FrontCCW90Button.Name = "_FrontCCW90Button";
            this._FrontCCW90Button.Size = new System.Drawing.Size(151, 31);
            this._FrontCCW90Button.TabIndex = 9;
            this._FrontCCW90Button.Text = "[ F\' ]前逆";
            this._FrontCCW90Button.UseVisualStyleBackColor = true;
            this._FrontCCW90Button.Click += new System.EventHandler(this.FrontCCW90ButtonClick);
            // 
            // _RandomButton
            // 
            this._RandomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RandomButton.Location = new System.Drawing.Point(804, 532);
            this._RandomButton.Name = "_RandomButton";
            this._RandomButton.Size = new System.Drawing.Size(69, 40);
            this._RandomButton.TabIndex = 13;
            this._RandomButton.Text = "随机打乱";
            this._RandomButton.UseVisualStyleBackColor = true;
            this._RandomButton.Click += new System.EventHandler(this.RandomButtonClick);
            // 
            // _InitButton
            // 
            this._InitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._InitButton.Location = new System.Drawing.Point(886, 532);
            this._InitButton.Name = "_InitButton";
            this._InitButton.Size = new System.Drawing.Size(69, 40);
            this._InitButton.TabIndex = 14;
            this._InitButton.Text = "恢复初始";
            this._InitButton.UseVisualStyleBackColor = true;
            this._InitButton.Click += new System.EventHandler(this.InitButtonClick);
            // 
            // _RandomStepNumber
            // 
            this._RandomStepNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._RandomStepNumber.Location = new System.Drawing.Point(804, 509);
            this._RandomStepNumber.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._RandomStepNumber.Name = "_RandomStepNumber";
            this._RandomStepNumber.Size = new System.Drawing.Size(69, 21);
            this._RandomStepNumber.TabIndex = 15;
            this._RandomStepNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._RandomStepNumber.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 585);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Mike Reid魔方表示法：";
            // 
            // _InTextbox
            // 
            this._InTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._InTextbox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._InTextbox.Location = new System.Drawing.Point(139, 578);
            this._InTextbox.Name = "_InTextbox";
            this._InTextbox.ReadOnly = true;
            this._InTextbox.Size = new System.Drawing.Size(659, 26);
            this._InTextbox.TabIndex = 17;
            // 
            // _OutTextbox
            // 
            this._OutTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._OutTextbox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._OutTextbox.Location = new System.Drawing.Point(139, 609);
            this._OutTextbox.Name = "_OutTextbox";
            this._OutTextbox.ReadOnly = true;
            this._OutTextbox.Size = new System.Drawing.Size(659, 26);
            this._OutTextbox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 616);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "解法输出：";
            // 
            // _RandomStepTextbox
            // 
            this._RandomStepTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._RandomStepTextbox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._RandomStepTextbox.Location = new System.Drawing.Point(139, 546);
            this._RandomStepTextbox.Name = "_RandomStepTextbox";
            this._RandomStepTextbox.ReadOnly = true;
            this._RandomStepTextbox.Size = new System.Drawing.Size(659, 26);
            this._RandomStepTextbox.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 553);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "随机打乱步骤：";
            // 
            // _CubePaper
            // 
            this._CubePaper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CubePaper.BackColor = System.Drawing.Color.SlateGray;
            this._CubePaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._CubePaper.Location = new System.Drawing.Point(12, 12);
            this._CubePaper.Name = "_CubePaper";
            this._CubePaper.Size = new System.Drawing.Size(786, 528);
            this._CubePaper.TabIndex = 0;
            // 
            // _SolutionButton
            // 
            this._SolutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._SolutionButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SolutionButton.Location = new System.Drawing.Point(804, 578);
            this._SolutionButton.Name = "_SolutionButton";
            this._SolutionButton.Size = new System.Drawing.Size(69, 57);
            this._SolutionButton.TabIndex = 22;
            this._SolutionButton.Text = "解魔";
            this._SolutionButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(886, 578);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 57);
            this.button1.TabIndex = 23;
            this.button1.Text = "验证";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CubeExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 653);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._SolutionButton);
            this.Controls.Add(this._RandomStepTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._OutTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._InTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._RandomStepNumber);
            this.Controls.Add(this._InitButton);
            this.Controls.Add(this._RandomButton);
            this.Controls.Add(this._BackCW90Button);
            this.Controls.Add(this._BackCCW90Button);
            this.Controls.Add(this._FrontCW90Button);
            this.Controls.Add(this._FrontCCW90Button);
            this.Controls.Add(this._DownCW90Button);
            this.Controls.Add(this._DwonCCW90Button);
            this.Controls.Add(this._UpCW90Button);
            this.Controls.Add(this._UpCCW90Button);
            this.Controls.Add(this._LeftCCW90Button);
            this.Controls.Add(this._RightCW90Button);
            this.Controls.Add(this._RightCCW90Button);
            this.Controls.Add(this._LeftCW90Button);
            this.Controls.Add(this._CubePaper);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CubeExplorerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CubeExplorerForm";
            ((System.ComponentModel.ISupportInitialize)(this._RandomStepNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CubePaperControl _CubePaper;
        private System.Windows.Forms.Button _LeftCW90Button;
        private System.Windows.Forms.Button _RightCCW90Button;
        private System.Windows.Forms.Button _RightCW90Button;
        private System.Windows.Forms.Button _LeftCCW90Button;
        private System.Windows.Forms.Button _UpCW90Button;
        private System.Windows.Forms.Button _UpCCW90Button;
        private System.Windows.Forms.Button _DownCW90Button;
        private System.Windows.Forms.Button _DwonCCW90Button;
        private System.Windows.Forms.Button _BackCW90Button;
        private System.Windows.Forms.Button _BackCCW90Button;
        private System.Windows.Forms.Button _FrontCW90Button;
        private System.Windows.Forms.Button _FrontCCW90Button;
        private System.Windows.Forms.Button _RandomButton;
        private System.Windows.Forms.Button _InitButton;
        private System.Windows.Forms.NumericUpDown _RandomStepNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _InTextbox;
        private System.Windows.Forms.TextBox _OutTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _RandomStepTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _SolutionButton;
        private System.Windows.Forms.Button button1;
    }
}