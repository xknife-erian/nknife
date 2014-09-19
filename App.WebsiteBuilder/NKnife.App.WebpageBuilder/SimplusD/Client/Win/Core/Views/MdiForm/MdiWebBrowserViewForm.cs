using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;
using mshtml;

namespace Jeelu.SimplusD.Client.Win
{
    public class MdiWebBrowserViewForm : BaseViewForm
    {
        protected WebBrowser _webBrowser = new WebBrowser();
        protected string _address;

        protected IHTMLDocument2 IDoc2
        {
            get
            {
                if (_webBrowser == null || _webBrowser.Document == null)
                {
                    return null;
                }
                return (IHTMLDocument2)_webBrowser.Document.DomDocument;
            }
        }
        public MdiWebBrowserViewForm()
        {
            this.ShowIcon = true;
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("tree.img.site").GetHicon());

            _webBrowser.Dock = DockStyle.Fill;
            _webBrowser.ProgressChanged += delegate
            {
                if (_webBrowser.Document != null && !string.IsNullOrEmpty(_webBrowser.Document.Title))
                {
                    this.Text = _webBrowser.Document.Title;
                }
                else
                {
                    this.Text = "[Jeeluä¯ÀÀÆ÷]";
                }
            };
            this.Text = "[Jeeluä¯ÀÀÆ÷]";
            this.Controls.Add(_webBrowser);
        }

        public void Navigation(string address)
        {
            _address = address;

            _webBrowser.Navigate(_address);
        }

        public override string Id
        {
            get { return _address; }
        }

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.WebBrowser; }
        }

        public override void Undo()
        {
            IDoc2.execCommand("Undo", true, true);
        }

        public override void Redo()
        {
            IDoc2.execCommand("Redo", true, true);
        }

        public override void Cut()
        {
            IDoc2.execCommand("Cut", true, true);
        }

        public override void Copy()
        {
            IDoc2.execCommand("Copy", true, true);
        }

        public override void Paste()
        {
            IDoc2.execCommand("Paste", true, true);
        }

        public override void Delete()
        {
            IDoc2.execCommand("Delete", true, true);
        }

        public override void SelectAll()
        {
            IDoc2.execCommand("SelectAll", true, true);
        }

        public override bool CanUndo()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Undo");
        }

        public override bool CanRedo()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Redo");
        }

        public override bool CanCut()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Cut");
        }

        public override bool CanCopy()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Copy");
        }

        public override bool CanPaste()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Paste");
        }

        public override bool CanDelete()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Delete");
        }

        public override bool CanSelectAll()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("SelectAll");
        }

    }
}
