namespace NKnife.Tools.Robot.CubeOctopus
{
    partial class DistinguishCubeSurfaceForm
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
            this._VedioPanel = new System.Windows.Forms.Panel();
            this._PlayButton = new System.Windows.Forms.Button();
            this._StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _VedioPanel
            // 
            this._VedioPanel.Location = new System.Drawing.Point(12, 12);
            this._VedioPanel.Name = "_VedioPanel";
            this._VedioPanel.Size = new System.Drawing.Size(500, 500);
            this._VedioPanel.TabIndex = 0;
            // 
            // _PlayButton
            // 
            this._PlayButton.Location = new System.Drawing.Point(300, 518);
            this._PlayButton.Name = "_PlayButton";
            this._PlayButton.Size = new System.Drawing.Size(103, 35);
            this._PlayButton.TabIndex = 1;
            this._PlayButton.Text = "开始";
            this._PlayButton.UseVisualStyleBackColor = true;
            // 
            // _StopButton
            // 
            this._StopButton.Location = new System.Drawing.Point(409, 518);
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(103, 35);
            this._StopButton.TabIndex = 2;
            this._StopButton.Text = "停止";
            this._StopButton.UseVisualStyleBackColor = true;
            // 
            // DistinguishCubeSurfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 637);
            this.Controls.Add(this._StopButton);
            this.Controls.Add(this._PlayButton);
            this.Controls.Add(this._VedioPanel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DistinguishCubeSurfaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DistinguishCubeSurfaceForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _VedioPanel;
        private System.Windows.Forms.Button _PlayButton;
        private System.Windows.Forms.Button _StopButton;
    }
}