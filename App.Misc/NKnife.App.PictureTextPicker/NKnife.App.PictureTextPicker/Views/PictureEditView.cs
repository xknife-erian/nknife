using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureEditView : DockContent
    {
        private System.Windows.Forms.WebBrowser PictureEditWebBrowser;
    
        private void InitializeComponent()
        {
            this.PictureEditWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // PictureEditWebBrowser
            // 
            this.PictureEditWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureEditWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.PictureEditWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.PictureEditWebBrowser.Name = "PictureEditWebBrowser";
            this.PictureEditWebBrowser.Size = new System.Drawing.Size(550, 473);
            this.PictureEditWebBrowser.TabIndex = 0;
            // 
            // PictureEditView
            // 
            this.ClientSize = new System.Drawing.Size(550, 473);
            this.Controls.Add(this.PictureEditWebBrowser);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureEditView";
            this.ResumeLayout(false);

        }
    }
}
