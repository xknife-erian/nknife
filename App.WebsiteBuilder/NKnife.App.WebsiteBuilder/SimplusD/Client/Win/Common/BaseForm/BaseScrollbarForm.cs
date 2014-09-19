using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class BaseScrollbarForm : BaseForm
    {
        public BaseScrollbarForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private ProgressBar _progressBar;
        public ProgressBar ProgressBar
        {
            get { return _progressBar; }
            set { _progressBar = value; }
        }

        private void InitializeComponent()
        {
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(11, 27);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(397, 23);
            this._progressBar.TabIndex = 0;
            // 
            // BaseScrollbarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ShowInTaskbar = false;
            this.ClientSize = new System.Drawing.Size(420, 74);
            this.Controls.Add(this._progressBar);
            this.Name = "BaseScrollbarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseScrollbarForm";
            this.ResumeLayout(false);
        }

    }
}