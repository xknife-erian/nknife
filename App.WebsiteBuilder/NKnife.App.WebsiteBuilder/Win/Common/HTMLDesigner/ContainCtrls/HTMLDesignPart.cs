using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using mshtml;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class HTMLDesignPart : WebBrowser
    {
        #region 接口声明：IHTMLDocument
        public IHTMLDocument idoc
        {
            get { return (IHTMLDocument)this.Document.DomDocument; }
        }
        public IHTMLDocument2 idoc2
        {
            get { return (IHTMLDocument2)this.Document.DomDocument; }
        }
        public IHTMLDocument3 idoc3
        {
            get { return (IHTMLDocument3)this.Document.DomDocument; }
        }
        public IHTMLDocument4 idoc4
        {
            get { return (IHTMLDocument4)this.Document.DomDocument; }
        }
        public IHTMLDocument5 idoc5
        {
            get { return (IHTMLDocument5)this.Document.DomDocument; }
        }
        #endregion


        public HTMLDesignPart()
        {
            InitializeComponent();
            this.IsWebBrowserContextMenuEnabled= false;
            this.ImeMode = ImeMode.On;
        }
    }
}
