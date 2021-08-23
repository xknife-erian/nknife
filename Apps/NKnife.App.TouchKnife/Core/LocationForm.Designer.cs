namespace NKnife.App.TouchInputKnife.Core
{
    partial class LocationForm
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
            this._XNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._YNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._CancelButton = new System.Windows.Forms.Button();
            this._OkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._XNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._YNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X:";
            // 
            // _XNumericUpDown
            // 
            this._XNumericUpDown.Location = new System.Drawing.Point(56, 29);
            this._XNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._XNumericUpDown.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this._XNumericUpDown.Name = "_XNumericUpDown";
            this._XNumericUpDown.Size = new System.Drawing.Size(140, 21);
            this._XNumericUpDown.TabIndex = 0;
            // 
            // _YNumericUpDown
            // 
            this._YNumericUpDown.Location = new System.Drawing.Point(56, 58);
            this._YNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._YNumericUpDown.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this._YNumericUpDown.Name = "_YNumericUpDown";
            this._YNumericUpDown.Size = new System.Drawing.Size(140, 21);
            this._YNumericUpDown.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // _CancelButton
            // 
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(120, 105);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(76, 25);
            this._CancelButton.TabIndex = 3;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _OkButton
            // 
            this._OkButton.Location = new System.Drawing.Point(36, 105);
            this._OkButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._OkButton.Name = "_OkButton";
            this._OkButton.Size = new System.Drawing.Size(76, 25);
            this._OkButton.TabIndex = 2;
            this._OkButton.Text = "OK";
            this._OkButton.UseVisualStyleBackColor = true;
            this._OkButton.Click += new System.EventHandler(this._OkButton_Click);
            // 
            // LocationForm
            // 
            this.AcceptButton = this._OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(234, 164);
            this.ControlBox = false;
            this.Controls.Add(this._OkButton);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._YNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._XNumericUpDown);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LocationForm";
            this.Text = "输入窗口位置";
            ((System.ComponentModel.ISupportInitialize)(this._XNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._YNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _XNumericUpDown;
        private System.Windows.Forms.NumericUpDown _YNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _OkButton;
    }
}