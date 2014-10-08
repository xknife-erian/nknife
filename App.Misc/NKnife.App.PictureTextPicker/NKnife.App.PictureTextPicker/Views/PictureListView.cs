using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureListView :DockContent
    {
        private WebBrowser _ThumbNailListWebBrowser;
        private IPictureList _PictureList = DI.Get<IPictureList>();
        private List<string> _jpgsmallNamelist = new List<string>();
        private IAppOption _AppOption = DI.Get<IAppOption>();

        public PictureListView()
        {
            InitializeComponent();
            _PictureList.PictureListChanged += PictureListChanged;
        }


        private void PictureListViewLoad(object sender, EventArgs e)
        {
            var picListPath = Path.Combine(Application.StartupPath, @"HTML\PicList.htm");
            _ThumbNailListWebBrowser.Navigate(picListPath);
            _ThumbNailListWebBrowser.ObjectForScripting = this.ParentForm;
        }

        private void PictureListChanged(object sender, EventArgs eventArgs)
        {
            GetLocalSmallName();
            string data = JsonConvert.SerializeObject(_jpgsmallNamelist);
            string thumbNailDirectory = _AppOption.GetOption("ThumbNailDirectory", "") + @"\";
            _ThumbNailListWebBrowser.Document.InvokeScript("ShowPicList", new string[] { data, thumbNailDirectory });  
        }

        /// <summary>
        /// 获得本地缩略图片完整路径集
        /// </summary>
        public void GetLocalSmallName()
        {
            _jpgsmallNamelist.Clear();
            string thumbNailDirectory = _AppOption.GetOption("ThumbNailDirectory", "");
            string[] imagePathList = Directory.GetFiles(thumbNailDirectory, "*.jpg");
            if (imagePathList.Length > 0)
            {
                foreach (string strName in imagePathList)
                {
                    if (File.Exists(strName))
                    {
                        string jpgName = Path.GetFileName(strName);
                        _jpgsmallNamelist.Add(jpgName);
                    }
                }
            }
        }

        protected void InitializeComponent()
        {
            this._ThumbNailListWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // ThumbNailListWebBrowser
            // 
            this._ThumbNailListWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ThumbNailListWebBrowser.Location = new System.Drawing.Point(0, 0);
            this._ThumbNailListWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._ThumbNailListWebBrowser.Name = "_ThumbNailListWebBrowser";
            this._ThumbNailListWebBrowser.Size = new System.Drawing.Size(378, 366);
            this._ThumbNailListWebBrowser.TabIndex = 0;
            // 
            // PictureListView
            // 
            this.ClientSize = new System.Drawing.Size(378, 366);
            this.Controls.Add(this._ThumbNailListWebBrowser);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureListView";
            this.Text = "图片列表";
            this.Load += new System.EventHandler(this.PictureListViewLoad);
            this.ResumeLayout(false);

        }

    }
}
