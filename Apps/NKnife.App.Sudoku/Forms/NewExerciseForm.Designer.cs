using NKnife.GUI.WinForm;

namespace NKnife.App.Sudoku.Forms
{
    partial class NewExerciseForm
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
            this._ExerciseTextBox = new QuickTextBox();
            this._CancleButton = new System.Windows.Forms.Button();
            this._AcceptButton = new System.Windows.Forms.Button();
            this._ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ExerciseTextBox
            // 
            this._ExerciseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._ExerciseTextBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ExerciseTextBox.Location = new System.Drawing.Point(18, 18);
            this._ExerciseTextBox.MaxLength = 81;
            this._ExerciseTextBox.Multiline = true;
            this._ExerciseTextBox.Name = "_ExerciseTextBox";
            this._ExerciseTextBox.Size = new System.Drawing.Size(416, 166);
            this._ExerciseTextBox.TabIndex = 0;
            this._ExerciseTextBox.Text = ".9..3.....5......2....1....6..2..8..1..5.....3.7............31.7..4............9." +
                "";
            // 
            // _CancleButton
            // 
            this._CancleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancleButton.Location = new System.Drawing.Point(368, 194);
            this._CancleButton.Name = "_CancleButton";
            this._CancleButton.Size = new System.Drawing.Size(68, 26);
            this._CancleButton.TabIndex = 3;
            this._CancleButton.Text = "取消";
            this._CancleButton.UseVisualStyleBackColor = true;
            this._CancleButton.Click += new System.EventHandler(this._CancleButton_Click);
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(297, 194);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(68, 26);
            this._AcceptButton.TabIndex = 2;
            this._AcceptButton.Text = "确定";
            this._AcceptButton.UseVisualStyleBackColor = true;
            this._AcceptButton.Click += new System.EventHandler(this._AcceptButton_Click);
            // 
            // _ClearButton
            // 
            this._ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearButton.Location = new System.Drawing.Point(214, 194);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(68, 26);
            this._ClearButton.TabIndex = 1;
            this._ClearButton.Text = "清空(&C)";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this._ClearButton_Click);
            // 
            // NewExerciseForm
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancleButton;
            this.ClientSize = new System.Drawing.Size(452, 235);
            this.Controls.Add(this._ClearButton);
            this.Controls.Add(this._AcceptButton);
            this.Controls.Add(this._CancleButton);
            this.Controls.Add(this._ExerciseTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewExerciseForm";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "输入新题目";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private QuickTextBox _ExerciseTextBox;
        private System.Windows.Forms.Button _CancleButton;
        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _ClearButton;
    }
}