using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class MdiSnipDesignerForm : BaseEditViewForm, IGetPropertiesForPanelable,IMarkPosition
    {
        #region 字段和属性

        private DesignerPanel _designerPanel;

        private CssProperty _width = null;
        private int _height = 0;
        string _tmpltID;
        string _snipId;
        private Image _backImageInTmplt;

        MdiTmpltDesignForm _ownerTmpltForm;

        private int _brownsIsExpand = 200;

        public SnipXmlElement SnipElement { get; private set; }

        //add by fenggy 2008-06-19 保存页面片新的SnipName
        private string _newSnipName = "";

        /// <summary>
        /// 获取或设置页面片设计器的预览器是否展开
        /// </summary>
        public int BrownsIsExPand
        {
            get { return _brownsIsExpand; }
            set { _brownsIsExpand = value; }
        }

        /// <summary>
        /// 获取或设置此页面片的模板背景图片
        /// </summary>
        public Image BackImageInTmplt
        {
            get { return _backImageInTmplt; }
            set { _backImageInTmplt = value; }
        }

        /// <summary>
        /// 获取包含此页面片的模板ID
        /// </summary>
        public string TmpltID
        {
            get { return _tmpltID; }
        }

        /// <summary>
        /// 获取或设置页面片类型
        /// </summary>
        public PageSnipType SnipType
        {
            get { return SnipElement.SnipType; }
        }

        /// <summary>
        /// 获取或设置页面片宽度
        /// </summary>
        public CssProperty SnipWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 获取或设置页面片高度
        /// </summary>
        public int SnipHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 获取包含的页面片设计器对象
        /// </summary>
        public SnipPageDesigner SnipPageDesigner
        {
            get { return _designerPanel.Designer; }
        }

        /// <summary>
        /// 获取导航栏
        /// </summary>
        public NavigationStrip BottomNavigation { get; private set; }

        /// <summary>
        /// 获取设计器的预览框
        /// </summary>
        //public WebBrowser WebBrowser
        //{
        //    get { return previewBrowser; }
        //}

        #endregion

        #region 构造函数与Form的初始化

        /// <summary>
        /// 页面片设计器
        /// </summary>        
        public MdiSnipDesignerForm(string tmpltID, string snipID)
        {
            ///赋初值
            ///
            _snipId = snipID;
            _tmpltID = tmpltID;

            InitializeComponent();

            ///从所属的模板窗体里获取当前页面片的XmlElement。
            _ownerTmpltForm = Service.Workbench.GetWorkDocumentById(tmpltID, WorkDocumentType.TmpltDesigner) as MdiTmpltDesignForm;
            if (_ownerTmpltForm == null)
            {
                Debug.Fail("此页面片对应的模板窗口不存在。");
                return;
            }

            ///通过所属的模板窗口和传入的SnipID，获得SnipXmlElement
            SnipElement = _ownerTmpltForm.TmpltDoc.GetSnipElementById(_snipId);

            ///通过SnipXmlElement创建DesignerPanel(DesignerPanel构造函数将创建SnipPageDesigner)
            this._designerPanel = new DesignerPanel(_ownerTmpltForm.TmpltDoc.HasAutoKeyWordsSequenceType);

            this._designerPanel.BackColor = SoftwareOption.SnipDesigner.FormBackColor;// System.Drawing.Color.DarkGray;
            this._designerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._designerPanel.Location = new System.Drawing.Point(0, 0);
            this._designerPanel.Name = "workPanel";
            this._designerPanel.Size = new System.Drawing.Size(747, 528);
            this._designerPanel.TabIndex = 0;
            this._designerPanel.Text = "designerPanel1";

            this.SnipPageDesigner.MouseDoubleClick += new MouseEventHandler(SnipPageDesigner_MouseDoubleClick);
            this.SnipPageDesigner.SelectedPartsChanged += new EventHandler(SnipPageDesigner_SelectedPartsChanged);

            this.Controls.Add(this._designerPanel);

            ///导航栏的初始化
            this.BottomNavigation = new NavigationStrip();
            this.BottomNavigation.Dock = DockStyle.Bottom;
            this.BottomNavigation.NavigateTarget = this.SnipPageDesigner;
            this.Controls.Add(this.BottomNavigation);

            this.ShowIcon = true;
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("tree.img.SnipPage2").GetHicon());

            _timer.Interval = 100;
            _timer.Tick += new EventHandler(_timer_Tick);

            SnipPageDesigner.Load(SnipElement);
            Text = SnipPageDesigner.Text;
            SnipPageDesigner.DesignerReseted += new EventHandler(SnipPageDesigner_DesignerReseted);
            SnipPageDesigner.PartsLayouted += new EventHandler(SnipPageDesigner_PartsLayouted);
            this.SizeChanged += new EventHandler(MdiSnipDesignerForm_SizeChanged);

            // add by fenggy 2006-06-13 增加处理PART定位的函数
            SdsiteXmlDocument.OrientationPart += new EventHandler<EventArgs<string[]>>(SdsiteXmlDocument_OrientationPart);

            // add by fenggy 2006-06-17 更改FORM的TEXT
            SdsiteXmlDocument.SnipDesignerFormTextChange += new EventHandler<EventArgs<string[]>>(SdsiteXmlDocument_SnipDesignerFormTextChange);

        }

       
        
        /// <summary>
        /// add by fenggy 2006-06-13 处理PART的定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SdsiteXmlDocument_OrientationPart(object sender, EventArgs<string[]> e)
        {//
            //因为所有打开的页面片设计器都回相应该事件,所以只处理目标页面片！其他的过滤掉
            if (this._snipId.Equals(e.Item[0].ToString())) //e.Item[0]页面片ID
            {
                SnipPagePart part = SnipPageDesigner.GetPartByID(e.Item[1].ToString());//e.Item[1]欲定位PART 的ID
                if (part != null)
                {
                    this.SnipPageDesigner.SelectPartAndCheck(part);
                }
            }
        }
        /// <summary>
        /// add by fenggy 2008-06-17 修改FORM的TEXT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SdsiteXmlDocument_SnipDesignerFormTextChange(object sender, EventArgs<string[]> e)
        {
            //因为所有打开的页面片设计器都回相应该事件,所以只处理目标页面片！其他的过滤掉
            if (this._snipId.Equals(e.Item[0].ToString())) //e.Item[0]页面片ID
            {
                //新添加的页面片,如果不保存,并且在重命名,新的内容保存不上,所以这里先存着
                _newSnipName = e.Item[1].ToString();
                Text = StringParserService.Parse("${res:snipDesign.text.formText}") + _newSnipName;

            }
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            //this.splitPanel = new System.Windows.Forms.SplitContainer();
            //this.hideBtn = new HideButton();
            //this.previewBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();

            //this.previewBrowser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            //this.previewBrowser.Location = new System.Drawing.Point(1, 10);

            //this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //this.splitPanel.Panel2.Controls.Add(this.previewBrowser);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 528);
            //this.Controls.Add(this.splitPanel);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SnipDesignerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "页面片设计器";
            this.Load += new System.EventHandler(this.SnipDesignerForm_Load);
            this.ResumeLayout(false);

        }

        //private System.Windows.Forms.WebBrowser previewBrowser;
       // private System.Windows.Forms.SplitContainer splitPanel;
       // private HideButton hideBtn;

        #endregion

        #region 重写函数

        public override bool IsModified
        {
            get { return SnipPageDesigner.IsModified; }
        }

        public override bool Save()
        {
            ///得到Document里的相应页面片Element
            SnipXmlElement snipEleNew = _ownerTmpltForm.TmpltDoc.GetSnipElementById(SnipElement.Id);

            ///两个Element不是同一个对象，则更新
            if (!object.ReferenceEquals(SnipElement, snipEleNew))
            {
                SnipElement = snipEleNew;
            }

            //add by fenggy 2008-06-19
            //Snip的NAME有能修改,如果该页面片增加时没有保存且修改了NAME,以后在保存,这样的话,
            //NAME 是保存不上的,所以在这里在将修改过的名字重新赋值给页面片
            if (!string.IsNullOrEmpty(_newSnipName))
            {
                this.SnipElement.SnipName = _newSnipName;
            }

            ///得到页面片保存的Element
            SnipPageDesigner.SaveToElement(this.SnipElement);
            SnipPageDesigner.IsModified = false;

            OnSaved(EventArgs.Empty);

            return true;
        }

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.SnipDesigner; }
        }

        public override string Id
        {
            get
            {
                return _snipId;
            }
        }

        public override bool CanUndo()
        {
            return SnipPageDesigner.CanUndo();
        }

        public override bool CanRedo()
        {
            return SnipPageDesigner.CanRedo();
        }

        public override bool CanCut()
        {
            return true;
        }

        public override bool CanCopy()
        {
            return true;
        }

        public override bool CanPaste()
        {
            return true;
        }

        public override bool CanDelete()
        {
            return true;
        }

        public override bool CanSelectAll()
        {
            return true;
        }

        public override void Undo()
        {
            SnipPageDesigner.Undo();
        }

        public override void Redo()
        {
            SnipPageDesigner.Redo();
        }

        public override void Cut()
        {

        }

        public override void Copy()
        {

        }

        public override void Paste()
        {

        }

        public override void Delete()
        {
            //cmdManager.BeginBatchCommand();

            //_cmdManager.EndBatchCommand();
        }

        public override void SelectAll()
        {

        }

        #endregion

        #region 事件相关
        /// <summary>
        /// 保存后的事件
        /// </summary>
        public event EventHandler Saved;
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }

        void SnipPageDesigner_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Control)
            {
                SnipPagePart part = SnipPageDesigner.GetPartAt(SnipPageDesigner.PointToClient(Control.MousePosition), true);
                if (part != null)
                {
                    SnipPageDesigner.EditPart(part, false);
                    // from：郑浩 计划改为 双击后 “编辑” 2008.03.03早
                    ////Point point = SnipPageDesigner.PointToClient(Control.MousePosition);
                    ////if (part.HitTest(point))
                    ////{
                    //CssSetupForm form = new CssSetupForm(part.Css, CurType.Part);
                    //if (form.ShowDialog() == DialogResult.OK)
                    //{
                    //    part.Css = form.CSSText;
                    //}
                    ////}
                }
            }
        }

        //void hideBtn_Click(object sender, EventArgs e)
        //{
        //    if (hideBtn.IsHide)
        //    {
        //        BrownsIsExPand = this.splitPanel.SplitterDistance;
        //        this.splitPanel.SplitterDistance = Height - 5;
        //    }
        //    else
        //    {
        //        //this.splitPanel.SplitterDistance = BrownsIsExPand;
        //    }
        //}

        void MdiSnipDesignerForm_SizeChanged(object sender, EventArgs e)
        {
            //this.hideBtn.Location = new Point(Width / 2 - 30, 1);
        }

        /// <summary>
        /// 放一个Timer，为了延迟LayoutParted事件导致的WebBrowser重排
        /// </summary>
        Timer _timer = new Timer();
        void _timer_Tick(object sender, EventArgs e)
        {
            //_timer.Stop();
            //WorkbenchForm.MainForm.MainPreviewPad.DocumentText =
            //    SnipPageDesigner.ToHtmlPreview();
        }

        void SnipPageDesigner_PartsLayouted(object sender, EventArgs e)
        {
            //WorkbenchForm.MainForm.MainPreviewPad.DocumentText =
            //    SnipPageDesigner.ToHtmlPreview();
            //_timer.Stop();
            //_timer.Start();
        }

        void SnipPageDesigner_DesignerReseted(object sender, EventArgs e)
        {
            WorkbenchForm.MainForm.MainPreviewPad.DocumentText =
                SnipPageDesigner.ToHtmlPreview();
        }

        private void SnipDesignerForm_Load(object sender, EventArgs e)
        {
            SnipPageDesigner.LayoutParts();
            WorkbenchForm.MainForm.MainPreviewPad.DocumentText =
                SnipPageDesigner.ToHtmlPreview();
        }
        #endregion

        #region IGetPropertiesForPanelable 成员

        public object[] GetPropertiesForPanel()
        {
            if (!Service.Sdsite.IsOpened)
            {
                return new object[0];
            }
            return this.SnipPageDesigner.SelectedParts.ToArray();
        }

        public event EventHandler PropertiesChanged;

        protected void OnPropertiesChanged(EventArgs e)
        {
            if (PropertiesChanged != null)
            {
                PropertiesChanged(this, e);
            }
        }
        void SnipPageDesigner_SelectedPartsChanged(object sender, EventArgs e)
        {
            OnPropertiesChanged(EventArgs.Empty);
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
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}