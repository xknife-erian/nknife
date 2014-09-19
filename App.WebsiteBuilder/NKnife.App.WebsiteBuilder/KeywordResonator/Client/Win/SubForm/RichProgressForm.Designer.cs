using System.Windows.Forms;
namespace Jeelu.KeywordResonator.Client
{
    partial class RichProgressForm
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
            this._DescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this._DescriptionLabel = new System.Windows.Forms.Label();
            this._MasterLabel = new System.Windows.Forms.Label();
            this.MasterProgressBar = new System.Windows.Forms.ProgressBar();
            this._FollowLabel = new System.Windows.Forms.Label();
            this.FollowProgressBar = new System.Windows.Forms.ProgressBar();
            this._HideButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._DescriptionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _DescriptionGroupBox
            // 
            this._DescriptionGroupBox.Controls.Add(this._DescriptionLabel);
            this._DescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._DescriptionGroupBox.Location = new System.Drawing.Point(15, 5);
            this._DescriptionGroupBox.Name = "_DescriptionGroupBox";
            this._DescriptionGroupBox.Size = new System.Drawing.Size(382, 63);
            this._DescriptionGroupBox.TabIndex = 0;
            this._DescriptionGroupBox.TabStop = false;
            // 
            // _DescriptionLabel
            // 
            this._DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DescriptionLabel.Location = new System.Drawing.Point(3, 17);
            this._DescriptionLabel.Name = "_DescriptionLabel";
            this._DescriptionLabel.Size = new System.Drawing.Size(376, 43);
            this._DescriptionLabel.TabIndex = 0;
            this._DescriptionLabel.Text = "DescriptionLabel";
            // 
            // _MasterLabel
            // 
            this._MasterLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._MasterLabel.Location = new System.Drawing.Point(15, 68);
            this._MasterLabel.Name = "_MasterLabel";
            this._MasterLabel.Size = new System.Drawing.Size(382, 20);
            this._MasterLabel.TabIndex = 1;
            this._MasterLabel.Text = "MasterLabel";
            this._MasterLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MasterProgressBar
            // 
            this.MasterProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.MasterProgressBar.Location = new System.Drawing.Point(15, 88);
            this.MasterProgressBar.Name = "MasterProgressBar";
            this.MasterProgressBar.Size = new System.Drawing.Size(382, 14);
            this.MasterProgressBar.TabIndex = 2;
            // 
            // _FollowLabel
            // 
            this._FollowLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._FollowLabel.Location = new System.Drawing.Point(15, 102);
            this._FollowLabel.Name = "_FollowLabel";
            this._FollowLabel.Size = new System.Drawing.Size(382, 20);
            this._FollowLabel.TabIndex = 3;
            this._FollowLabel.Text = "FollowLabel";
            this._FollowLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // FollowProgressBar
            // 
            this.FollowProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.FollowProgressBar.Location = new System.Drawing.Point(15, 122);
            this.FollowProgressBar.Name = "FollowProgressBar";
            this.FollowProgressBar.Size = new System.Drawing.Size(382, 14);
            this.FollowProgressBar.TabIndex = 4;
            // 
            // _HideButton
            // 
            this._HideButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._HideButton.Location = new System.Drawing.Point(239, 144);
            this._HideButton.Name = "_HideButton";
            this._HideButton.Size = new System.Drawing.Size(75, 23);
            this._HideButton.TabIndex = 5;
            this._HideButton.Text = "Hide";
            this._HideButton.UseVisualStyleBackColor = true;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.Location = new System.Drawing.Point(322, 144);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 23);
            this._CancelButton.TabIndex = 6;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // RichProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 185);
            this.ControlBox = false;
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._HideButton);
            this.Controls.Add(this.FollowProgressBar);
            this.Controls.Add(this._FollowLabel);
            this.Controls.Add(this.MasterProgressBar);
            this.Controls.Add(this._MasterLabel);
            this.Controls.Add(this._DescriptionGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RichProgressForm";
            this.Padding = new System.Windows.Forms.Padding(15, 5, 15, 15);
            this.ShowInTaskbar = false;
            this.Text = "RichProgressForm";
            this._DescriptionGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _DescriptionGroupBox;
        private System.Windows.Forms.Label _MasterLabel;
        private System.Windows.Forms.Label _FollowLabel;
        private System.Windows.Forms.Button _HideButton;
        private System.Windows.Forms.Label _DescriptionLabel;
        private System.Windows.Forms.Button _CancelButton;
        private ProgressBar MasterProgressBar;
        private ProgressBar FollowProgressBar;

    }
}