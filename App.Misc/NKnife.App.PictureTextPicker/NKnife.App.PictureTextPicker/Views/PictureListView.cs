using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureListView :DockContent
    {
        private WebBrowser ThumbNailListWebBrowser;
        private IPictureList _PictureList = DI.Get<IPictureList>();

        public PictureListView()
        {
            InitializeComponent();
            _PictureList.PictureListChanged += PictureListChanged;
        }

        private void PictureListChanged(object sender, EventArgs eventArgs)
        {
           
        }

        protected void InitializeComponent()
        {
            this.ThumbNailListWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // ThumbNailListWebBrowser
            // 
            this.ThumbNailListWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThumbNailListWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.ThumbNailListWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ThumbNailListWebBrowser.Name = "ThumbNailListWebBrowser";
            this.ThumbNailListWebBrowser.Size = new System.Drawing.Size(378, 366);
            this.ThumbNailListWebBrowser.TabIndex = 0;
            // 
            // PictureListView
            // 
            this.ClientSize = new System.Drawing.Size(378, 366);
            this.Controls.Add(this.ThumbNailListWebBrowser);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureListView";
            this.Text = "图片列表";
            this.ResumeLayout(false);

        }
    }
}
