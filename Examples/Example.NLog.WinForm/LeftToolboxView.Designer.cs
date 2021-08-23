namespace NKnife.NLog.WinForm.Example
{
    partial class LeftToolboxView
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
            this._Func10Button = new System.Windows.Forms.Button();
            this._Func1Button = new System.Windows.Forms.Button();
            this._Func2Button = new System.Windows.Forms.Button();
            this._Func12Button = new System.Windows.Forms.Button();
            this._GroupCountBox = new System.Windows.Forms.NumericUpDown();
            this._Stop1Button = new System.Windows.Forms.Button();
            this._Stop2Button = new System.Windows.Forms.Button();
            this._Func11Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._GroupCountBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _Func10Button
            // 
            this._Func10Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Func10Button.Location = new System.Drawing.Point(14, 17);
            this._Func10Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Func10Button.Name = "_Func10Button";
            this._Func10Button.Size = new System.Drawing.Size(244, 33);
            this._Func10Button.TabIndex = 0;
            this._Func10Button.Text = "一次生成1条日志";
            this._Func10Button.UseVisualStyleBackColor = true;
            this._Func10Button.Click += new System.EventHandler(this._Func10Button_Click);
            // 
            // _Func1Button
            // 
            this._Func1Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Func1Button.Location = new System.Drawing.Point(14, 140);
            this._Func1Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Func1Button.Name = "_Func1Button";
            this._Func1Button.Size = new System.Drawing.Size(173, 33);
            this._Func1Button.TabIndex = 3;
            this._Func1Button.Text = "独立线程: 2条/1毫秒";
            this._Func1Button.UseVisualStyleBackColor = true;
            this._Func1Button.Click += new System.EventHandler(this._Func1Button_Click);
            // 
            // _Func2Button
            // 
            this._Func2Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Func2Button.Location = new System.Drawing.Point(14, 181);
            this._Func2Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Func2Button.Name = "_Func2Button";
            this._Func2Button.Size = new System.Drawing.Size(173, 33);
            this._Func2Button.TabIndex = 4;
            this._Func2Button.Text = "Timer: 10组/1毫秒";
            this._Func2Button.UseVisualStyleBackColor = true;
            this._Func2Button.Click += new System.EventHandler(this._Func2Button_Click);
            // 
            // _Func12Button
            // 
            this._Func12Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Func12Button.Location = new System.Drawing.Point(14, 99);
            this._Func12Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Func12Button.Name = "_Func12Button";
            this._Func12Button.Size = new System.Drawing.Size(173, 33);
            this._Func12Button.TabIndex = 2;
            this._Func12Button.Text = "并行: 指定数量组";
            this._Func12Button.UseVisualStyleBackColor = true;
            this._Func12Button.Click += new System.EventHandler(this._Func12Button_Click);
            // 
            // _GroupCountBox
            // 
            this._GroupCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._GroupCountBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._GroupCountBox.Location = new System.Drawing.Point(191, 101);
            this._GroupCountBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._GroupCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._GroupCountBox.Name = "_GroupCountBox";
            this._GroupCountBox.Size = new System.Drawing.Size(67, 29);
            this._GroupCountBox.TabIndex = 11;
            this._GroupCountBox.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _Stop1Button
            // 
            this._Stop1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Stop1Button.Location = new System.Drawing.Point(191, 140);
            this._Stop1Button.Name = "_Stop1Button";
            this._Stop1Button.Size = new System.Drawing.Size(67, 33);
            this._Stop1Button.TabIndex = 7;
            this._Stop1Button.Text = "停止";
            this._Stop1Button.UseVisualStyleBackColor = true;
            this._Stop1Button.Click += new System.EventHandler(this._Stop1Button_Click);
            // 
            // _Stop2Button
            // 
            this._Stop2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._Stop2Button.Location = new System.Drawing.Point(191, 181);
            this._Stop2Button.Name = "_Stop2Button";
            this._Stop2Button.Size = new System.Drawing.Size(67, 33);
            this._Stop2Button.TabIndex = 8;
            this._Stop2Button.Text = "停止";
            this._Stop2Button.UseVisualStyleBackColor = true;
            this._Stop2Button.Click += new System.EventHandler(this._Stop2Button_Click);
            // 
            // _Func11Button
            // 
            this._Func11Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Func11Button.Location = new System.Drawing.Point(14, 58);
            this._Func11Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._Func11Button.Name = "_Func11Button";
            this._Func11Button.Size = new System.Drawing.Size(244, 33);
            this._Func11Button.TabIndex = 1;
            this._Func11Button.Text = "一次生成1组日志";
            this._Func11Button.UseVisualStyleBackColor = true;
            this._Func11Button.Click += new System.EventHandler(this._Func11Button_Click);
            // 
            // LeftToolboxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 465);
            this.Controls.Add(this._Func11Button);
            this.Controls.Add(this._Stop2Button);
            this.Controls.Add(this._Stop1Button);
            this.Controls.Add(this._GroupCountBox);
            this.Controls.Add(this._Func12Button);
            this.Controls.Add(this._Func2Button);
            this.Controls.Add(this._Func1Button);
            this.Controls.Add(this._Func10Button);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LeftToolboxView";
            this.Text = "测试功能";
            ((System.ComponentModel.ISupportInitialize)(this._GroupCountBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _Func10Button;
        private System.Windows.Forms.Button _Func1Button;
        private System.Windows.Forms.Button _Func2Button;
        private System.Windows.Forms.Button _Func12Button;
        private System.Windows.Forms.NumericUpDown _GroupCountBox;
        private System.Windows.Forms.Button _Stop1Button;
        private System.Windows.Forms.Button _Stop2Button;
        private System.Windows.Forms.Button _Func11Button;
    }
}