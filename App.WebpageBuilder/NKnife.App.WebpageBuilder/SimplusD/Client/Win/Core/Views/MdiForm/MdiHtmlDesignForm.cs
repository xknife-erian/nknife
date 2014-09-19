using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using mshtml;
using System.Xml;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class MdiHtmlDesignForm : BaseEditViewForm ,IMarkPosition
    {

        string _pageId = "";

        public string PageId
        {
            get { return _pageId; }
            set { _pageId = value; }
        }
        IHTMLDocument2 IDoc2 = null;
        WebBrowser _designWebB = null;
        PageXmlDocument _pageDoc = null;//页面文件

        private HTMLDesignerEx _htmldesign;
        public HTMLDesignerEx HtmlDesign
        {
            get { return _htmldesign; }
        }

        public MdiHtmlDesignForm(string pageId)
        {
            this._pageId = pageId;
            this.ShowIcon = true;
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("tree.img.article2").GetHicon());
        }

        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(!string.IsNullOrEmpty(_pageId));
            PageSimpleExXmlElement ele = Service.Sdsite.CurrentDocument.GetPageElementById(_pageId);
            if (ele == null || !File.Exists(ele.AbsoluteFilePath))
            {
                MessageService.Show("文件不存在，打开失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Timer timer = new Timer();
                timer.Interval = 10;
                timer.Tick += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    this.Close();
                };
                timer.Start();
                return;
            }

            this._pageDoc = ele.GetIndexXmlDocument();
            // this._pageEle = (PageElement)this._pageDoc.GetElementById(pageId); /wangmiao

            this.Text = ele.Title;//w
            Service.Sdsite.CurrentDocument.ElementTitleChanged += new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);

            //控件设置
            _htmldesign = new HTMLDesignerEx(PageId);
            //_htmldesign.PageId = _pageId;
            //this.Controls.Add(_htmldesign.GetMainToolStrip());
            //this.Controls.Add(_htmldesign.GetHtmlPanel());
            this.Controls.Add(_htmldesign);
            string fileName = Service.Sdsite.CurrentDocument.GetPageElementById(_pageId).Title;//w
            this._htmldesign.PageTitle = fileName;//w

           //this._htmldesign.SetHtmlPanel().BringToFront();
            this._designWebB = this._htmldesign.DesignWebBrowser;

            //IDoc2 = _htmldesign.Idoc2;
            base.OnLoad(e);
        }

        void CurrentDocument_ElementTitleChanged(object sender, ChangeTitleEventArgs e)
        {
            if (e.Item.Id == this._pageId)
            {
                this.Text = e.NewTitle;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_designWebB != null)
            {
                Service.Sdsite.DesignDataDocument.HTMLDesignerDesignPanelHeight = _htmldesign.splitCon.SplitterDistance;
            }

            Service.Sdsite.CurrentDocument.ElementTitleChanged -= new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);
            _htmldesign.PageProPanel.SubmitPageContentInfo();//保存页面属性设置
            base.OnFormClosed(e);
        }

        #region 重写的基类的实现

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.HtmlDesigner; }
        }

        public override bool IsModified
        {
            get
            {
                if (_htmldesign == null)
                {
                    return false;
                }
                return _htmldesign.IsModified;
            }

        }

        public override string Id
        {
            get { return _pageId; }
        }

        public override bool Save()
        {
            _pageDoc.PageText = HtmlDesign.PageText;
            //_pageEle.Title = HtmlDesign.ContentTitle;wang
            _pageDoc.Save();
            //_htmldesign.PageText = _pageDoc.PageText;//wm
            return true;
        }
        /// <summary>
        /// 添加页面
        /// </summary>
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
            if (_designWebB.Focused)
                IDoc2.execCommand("Cut", true, true);
            else
            {
              //  _htmldesign.CodeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
            }
        }

        public override void Copy()
        {
            if (_designWebB.Focused)
                IDoc2.execCommand("Copy", true, true);
            else
            {
                //_htmldesign.CodeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
            }
        }

        public override void Paste()
        {
            if (_designWebB.Focused)
                IDoc2.execCommand("Paste", true, true);
          //  else
               // _htmldesign.CodeTextEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
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
            //return true;
        }

        public override bool CanRedo()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("Redo");
            //return true;
        }

        public override bool CanCut()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return true;// (bool)IDoc2.queryCommandEnabled("Cut");
        }

        public override bool CanCopy()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return true;// (bool)IDoc2.queryCommandEnabled("Copy");
        }

        public override bool CanPaste()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return true;// (bool)IDoc2.queryCommandEnabled("Paste");
        }

        public override bool CanDelete()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return true;// (bool)IDoc2.queryCommandEnabled("Delete");
        }

        public override bool CanSelectAll()
        {
            if (IDoc2 == null)
            {
                return false;
            }
            return (bool)IDoc2.queryCommandEnabled("SelectAll");
        }

        public override Control PropertyPanel
        {
            get
            {
                if (_htmldesign == null)
                {
                    return null;
                }
                return _htmldesign.PageProPanel;//.PropertyPanel;
            }
        }

        #endregion

        #region IMarkPosition 成员

        public void MarkPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public ISearch Search
        {
            get { throw new NotImplementedException(); }
        }

        public List<Position> SelectedPositions
        {
            get { throw new NotImplementedException(); }
        }

        public Position CurrentPosition
        {
            get { return null; }
        }

        //public Position GetCurrentPosition()
        //{
            
        //}
        #endregion
    }
}
