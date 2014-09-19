using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MdiWelComePageForm : MdiWebBrowserViewForm
    {
        private DataForScriptInStartup _dataForScript;
        public DataForScriptInStartup DataForScript
        {
            get { return _dataForScript; }
        }


        public MdiWelComePageForm()
        {
            InitializeComponent();
            string fullPath = Path.Combine(PathService.CL_StartupPage_Folder, "index.html");
            this.Navigation(fullPath);

            _webBrowser.AllowWebBrowserDrop = false;
            _webBrowser.IsWebBrowserContextMenuEnabled = false;
            _dataForScript = new DataForScriptInStartup();
            _webBrowser.ObjectForScripting = DataForScript;

            Service.RecentFiles.FileChanged += new EventHandler(RecentFiles_FileChanged);
        }

        void RecentFiles_FileChanged(object sender, EventArgs e)
        {
            if (!_webBrowser.IsDisposed)
            {
                _webBrowser.Document.InvokeScript("loadProjList");
            }
        }
    }
}