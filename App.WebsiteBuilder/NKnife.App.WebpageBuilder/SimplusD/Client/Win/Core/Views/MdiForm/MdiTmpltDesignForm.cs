using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MdiTmpltDesignForm : BaseEditViewForm,IGetPropertiesForPanelable,IMarkPosition
    {
        #region 内部变量

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        IContainer components = null;

        //TODO:欧阳，下一步这里应该是动态获取
        Image _drawPanelBack;// = Image.FromFile(Path.Combine(PathService.SoftwarePath, "Sina.com.png"));

        //为在构造函数里关闭Form所提供的
        Timer _timerCloseForm;
        bool _willClose = false;

        #region 工具栏

        ToolStrip toolStrip = new ToolStrip(); // 工具拦

        Dictionary<ToolStripItem, int> _itemsDrawType = new Dictionary<ToolStripItem, int>();

        Dictionary<int, ToolStripItem> _drawTypeitems = new Dictionary<int, ToolStripItem>();

        List<ToolStripItem> _toolStripItems = new List<ToolStripItem>();//工具拦中各个项的集合

        private EnumDrawType _drawType = EnumDrawType.MouseSelect;  //绘制状态

        private XmlTextReader _xmlTextReader;

        #endregion

        #endregion

        #region  属性

        public List<Form> SnipDesignerForms { get; private set; }

        /// <summary>
        /// 获取或设置模板的底面图片
        /// </summary>
        public Image BackImage
        {
            get { return _drawPanelBack; }
            set { _drawPanelBack = value; }
        }

        public TmpltDesignerPanel TmpltDesign { get; private set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string TmpltID { get; private set; }

        /// <summary>
        ///  获取模板文件对象
        /// </summary>
        public TmpltXmlDocument TmpltDoc { get; private set; }

        #endregion

        #region 构造函数

        public MdiTmpltDesignForm(string tmpltID )
        {
            ResourcesReader.SetControlPropertyHelper(this);
            TmpltID = tmpltID;
            SnipDesignerForms = new List<Form>();
            InitializeComponent();
            this.ShowIcon = true;
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("tree.img.templet").GetHicon());
            TmpltSimpleExXmlElement tmpltEle = Service.Sdsite.CurrentDocument.GetTmpltElementById(tmpltID);
            if (tmpltEle == null)
            {
                MessageService.Show("文件不存在，打开失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BeginClose();
                return;
            }
            this.Text = tmpltEle.Title;
            Service.Sdsite.CurrentDocument.ElementTitleChanged += new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);
            Service.Workbench.WorkDocumentNewOpened += new EventHandler<EventArgs<FormData>>(WorkbenchService_WorkDocumentNewOpened);

            Debug.Assert(!string.IsNullOrEmpty(TmpltID));
            TmpltSimpleExXmlElement ele = Service.Sdsite.CurrentDocument.GetTmpltElementById(TmpltID);
            if (ele == null || !File.Exists(ele.AbsoluteFilePath))
            {
                MessageService.Show(StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.message.mdiFormLoad}"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                BeginClose();
                return;
            }
            TmpltDoc = ele.GetIndexXmlDocument();

            InitTmpltInfo();
            TmpltDesign.Dock = DockStyle.Fill;
            this.Controls.Add(TmpltDesign);
            TmpltDesign.BringToFront();
            
        }

        void BeginClose()
        {
            _willClose = true;
            _timerCloseForm = new Timer();
            _timerCloseForm.Interval = 10;
            _timerCloseForm.Tick += delegate
            {
                _timerCloseForm.Stop();
                _timerCloseForm.Dispose();
                this.Close();
            };
            _timerCloseForm.Start();
        }

        void CurrentDocument_ElementTitleChanged(object sender, ChangeTitleEventArgs e)
        {
            if (e.Item.Id == this.TmpltID)
            {
                this.Text = e.NewTitle;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ///释放对全局事件的监听
            Service.Workbench.WorkDocumentNewOpened -= new EventHandler<EventArgs<FormData>>(WorkbenchService_WorkDocumentNewOpened);

            Service.Sdsite.CurrentDocument.ElementTitleChanged -= new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);

            base.OnFormClosed(e);
        }

        void WorkbenchService_WorkDocumentNewOpened(object sender, EventArgs<FormData> e)
        {
            if (e.Item.WorkDocumentType == WorkDocumentType.SnipDesigner
                && e.Item.OwnerId == this.Id)
            {
                MdiSnipDesignerForm snipForm = e.Item.Form as MdiSnipDesignerForm;

                ///对新打开的页面片设计器窗体监听一些事件
                if (!SnipDesignerForms.Contains(snipForm))
                {
                    snipForm.FormClosed += new FormClosedEventHandler(snipForm_FormClosed);
                    snipForm.Saved += new EventHandler(snipForm_Saved);

                    SnipDesignerForms.Add(snipForm);
                }
            }
        }

        #endregion
       
        #region 事件

        void snipForm_Saved(object sender, EventArgs e)
        {
            TmpltDesign.Modified = true;
        }

        void snipForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = sender as Form;

            SnipDesignerForms.Remove(form);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (SnipDesignerForms.Count > 0)
            {
                try
                {
                    Service.Workbench.CloseAllWindowData.BeginCloseAllWindow();

                    while (SnipDesignerForms.Count > 0)
                    {
                        Form form = SnipDesignerForms[0];
                        form.Close();
                        if (!form.IsDisposed)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    base.OnFormClosing(e);
                }
                finally
                {
                    Service.Workbench.CloseAllWindowData.EndCloseAllWindow();
                }
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        /// <summary>
        /// 左键点击工具栏工作区域时发生
        /// </summary>
        void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripButton)
            {
                this.UnCheckedButtons();
                ToolStripButton btn = (ToolStripButton)e.ClickedItem;
                
                if (btn.Name == "cssSetup")
                {
                    ((TmpltDrawPanel)TmpltDesign.DrawPanel).CurRectProperty_Click(null,EventArgs.Empty);
                }
                else if (btn.Name == "backImgSetup")
                {
                    ((TmpltDrawPanel)TmpltDesign.DrawPanel).BackImgSetup_Click(null, EventArgs.Empty);
                }
                else
                {
                    btn.CheckState = CheckState.Checked;
                    _drawType = (EnumDrawType)_itemsDrawType[btn];
                    TmpltDesign.DrawPanel.DrawType = _drawType;
                }
            }
        }

        #endregion

        #region 重写函数

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

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.TmpltDesigner; }
        }

        public override bool IsModified
        {
            get
            {
                if (_willClose)
                {
                    return false;
                }
                return TmpltDesign.Modified;
            }
        }

        public override string Id
        {
            get
            {
                return TmpltID;
            }
        }

        public override bool Save()
        {
            TmpltDesign.SaveTmplt(TmpltDoc);
            TmpltDesign.Modified = false;
            return true;
        }
        public override bool CannelSave()
        {
            if (string.IsNullOrEmpty(TmpltDesign.strRectsData)) return false;

            //得到该模板对应的 TmpltSimpleExXmlElement
            TmpltSimpleExXmlElement ele = Service.Sdsite.CurrentDocument.GetTmpltElementById(TmpltID);
            if (ele != null)
            {
                TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(TmpltID);


                tmpltDoc.DocumentElement.RemoveChild(tmpltDoc.GetRectsElement());

                XmlElement newRects = tmpltDoc.CreateElement("rects");
                newRects.InnerXml = TmpltDesign.strRectsData;
                tmpltDoc.DocumentElement.AppendChild(newRects);
            }
            return true;
        }

        public override void Undo()
        {
        }

        public override void Redo()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SelectAll()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CanUndo()
        {
            return false;
        }

        public override bool CanRedo()
        {
            return false;
        }

        public override bool CanCut()
        {
            return false;
        }

        public override bool CanCopy()
        {
            return false;
        }

        public override bool CanPaste()
        {
            return false;
        }

        public override bool CanDelete()
        {
            return false;
        }

        public override bool CanSelectAll()
        {
            return false;
        }
        #endregion

        #region 内部函数

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeComponent()
        {
            #region 初始化工具栏
            this.toolStrip.Location = new System.Drawing.Point(2, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(26, 102);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "";

           // _xmlTextReader = new XmlTextReader(PathService.CL_DrawToolsBox);
            //
            SetButtons();
            //

            this.toolStrip.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip_ItemClicked);
            this.toolStrip.Items.AddRange(_toolStripItems.ToArray());
            toolStrip.Visible = SoftwareOption.TmpltDesigner.ShowToolBar;
            this.Controls.Add(toolStrip);
            toolStrip.BringToFront();
            #endregion
            this.components = new Container();
        }

        /// <summary>
        /// 初始化模板的信息
        /// </summary>
        private void InitTmpltInfo()
        {
            XmlElement ele = TmpltDoc.DocumentElement;
            if (Utility.Convert.StringToBool(ele.GetAttribute("hasBackImg")))
            {
                int w = Convert.ToInt32( TmpltDoc.Width);
                int h = Convert.ToInt32(TmpltDoc.Height);
                Bitmap _backBmp = new Bitmap(w, h);
                AnyXmlElement imgEle = (AnyXmlElement)ele.SelectSingleNode("backImage");
                _drawPanelBack = Utility.Convert.Base64ToImage(imgEle.CDataValue);
                Graphics g = Graphics.FromImage(_backBmp);
                int _w = _drawPanelBack.Width <= w ? _drawPanelBack.Width : w;
                int _h = _drawPanelBack.Height <= h ? _drawPanelBack.Height : h;
                g.DrawImageUnscaledAndClipped(_drawPanelBack, new Rectangle(0, 0, _w, _h));
                Image _backImg = _backBmp;
                Graphics gImg = Graphics.FromImage(_backImg);
                gImg.FillRectangle(new SolidBrush(Color.FromArgb(70, Color.White)), new Rectangle(0, 0, w, h));
                TmpltDesign = new TmpltDesignerPanel(w,h , _backImg, TmpltDoc);
            }
            else
            {
                TmpltDesign = new TmpltDesignerPanel(
                    Utility.Convert.PxStringToInt(ele.GetAttribute("width")),
                    Utility.Convert.PxStringToInt(ele.GetAttribute("height")),
                    null, TmpltDoc);
            }
            TmpltDesign.DrawPanel.ChangDrawTypeEvent += new Jeelu.SimplusD.Client.Win.DrawPanel.ChangDrawTypeEventHandler(DrawPanel_ChangDrawTypeEvent);
            TmpltDesign.DrawPanel.SelectingRectEvent += new EventHandler(DrawPanel_SelectingRectEvent);
        }
                                
        void DrawPanel_ChangDrawTypeEvent(object sender, EventArgs e)
        {
            if (TmpltDesign.DrawPanel.DrawType != this._drawType)
            {
                this._drawType = TmpltDesign.DrawPanel.DrawType;
                UnCheckedButtons();
                switch ((int)_drawType)
                {
                    case 0:
                        break;
                    case 1:
                        ((ToolStripButton)_drawTypeitems[1]).Checked = _drawTypeitems[1] != null ? true : false;
                        break;
                    case 2:
                        ((ToolStripButton)_drawTypeitems[2]).Checked = _drawTypeitems[2] != null ? true : false;
                        break;
                    case 3:
                        ((ToolStripButton)_drawTypeitems[3]).Checked = _drawTypeitems[3] != null ? true : false;
                        break;
                    case 4:
                        ((ToolStripButton)_drawTypeitems[4]).Checked = _drawTypeitems[4] != null ? true : false;
                        break;
                    case 5:
                        ((ToolStripButton)_drawTypeitems[5]).Checked = _drawTypeitems[5] != null ? true : false;
                        break;
                    case 6:
                        ((ToolStripButton)_drawTypeitems[6]).Checked = _drawTypeitems[6] != null ? true : false;
                        break;
                    case 10:
                        ((ToolStripButton)_drawTypeitems[10]).Checked = _drawTypeitems[10] != null ? true : false;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置工具栏的工具（按钮）种类和数量
        /// </summary>
        private void SetButtons()
        {
            #region choose
            ToolStripButton btn = new ToolStripButton(GetText("selectText"));
            btn.Name = GetText("selectText");
            btn.ToolTipText = GetText("toolTipSelectText");
            Icon icon = GetIcon("selectIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            int drawType = Convert.ToInt32(GetText("selectType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = true;
            _toolStripItems.Add(btn);
            #endregion

            #region chooseRect
            btn = new ToolStripButton(GetText("selectRectText"));
            btn.Name = GetText("selectRectText");
            btn.ToolTipText = GetText("toolTipSelectRectText");
            icon = GetIcon("selectRectIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("selectRectType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            #region chooseLine
            btn = new ToolStripButton(GetText("selectLineText"));
            btn.Name = GetText("selectRectText");
            btn.ToolTipText = GetText("toolTipSelectLineText");
            icon = GetIcon("selectLineIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("selectLineType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            ToolStripSeparator separator = new ToolStripSeparator();
            _toolStripItems.Add(separator);

            #region pen
            btn = new ToolStripButton(GetText("drawPenText"));
            btn.Name = GetText("drawPenText");
            btn.ToolTipText = GetText("toolTipDrawPenText");
            icon = GetIcon("drawPenIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("drawPenType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            #region deleteLine
            btn = new ToolStripButton(GetText("deleteLineText"));
            btn.Name = GetText("deleteLineText");
            btn.ToolTipText = GetText("toolTipDeleteText");
            icon = GetIcon("deleteIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("deleteLineType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            separator = new ToolStripSeparator();
            _toolStripItems.Add(separator);

            #region move
            btn = new ToolStripButton(GetText("moveText"));
            btn.Name = GetText("moveText");
            btn.ToolTipText = GetText("toolTipMoveText");
            icon = GetIcon("moveIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("moveType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            #region zoom
            btn = new ToolStripButton(GetText("zoomText"));
            btn.Name = GetText("zoomText");
            btn.ToolTipText = GetText("toolTipZoomText");
            icon = GetIcon("zoomIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("zoomType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            separator = new ToolStripSeparator();
            _toolStripItems.Add(separator);

            #region cssSetup
            btn = new ToolStripButton(GetText("cssSetupText"));
            btn.Name = GetText("cssSetupText");
            btn.ToolTipText = GetText("toolTipCssSetupText");
            icon = GetIcon("cssSetupIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("cssSetupType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

            #region BackImageSetup
            btn = new ToolStripButton(GetText("backImageSetupText"));
            btn.Name = GetText("backImageSetupText");
            btn.ToolTipText = GetText("toolTipBackImageSetupText");
            icon = GetIcon("backImageSetupIcon");
            btn.Image = icon.ToBitmap();
            btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            drawType = Convert.ToInt32(GetText("backImageSetupType"));
            _itemsDrawType[btn] = drawType;
            _drawTypeitems[drawType] = btn;
            btn.ImageAlign = ContentAlignment.BottomCenter;
            btn.Checked = false;
            _toolStripItems.Add(btn);
            #endregion

           

            //while (_xmlTextReader.Read())
            //{
            //    if (_xmlTextReader.NodeType == XmlNodeType.EndElement)
            //    {
            //        continue;
            //    }
            //    if (_xmlTextReader.Name == "button")
            //    {
            //        string text = _xmlTextReader.GetAttribute("text");
            //        string name = _xmlTextReader.GetAttribute("name");
            //        Icon icon = Icon.FromHandle(ResourceService.GetResourceImage(_xmlTextReader.GetAttribute("imageurl")).GetHicon());
            //            //new Icon(Path.Combine(Application.StartupPath, _xmlTextReader.GetAttribute("imageurl")));
            //        Image image = icon.ToBitmap();
            //        string tool_tip_text = _xmlTextReader.GetAttribute("tool_tip_text");
            //        int drawtype = Convert.ToInt32(_xmlTextReader.GetAttribute("drawtype"));
            //        ToolStripButton btn = new ToolStripButton(text);
            //        btn.Name = name;
            //        btn.ToolTipText = tool_tip_text;
            //        btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            //        _itemsDrawType[btn] = drawtype;
            //        _drawTypeitems[drawtype] = btn;
            //        btn.Image = image;
            //        btn.ImageAlign = ContentAlignment.BottomCenter;
            //        btn.Checked = name == "choose";
            //        _toolStripItems.Add(btn);
            //    }
            //    if (_xmlTextReader.Name == "separator")
            //    {
            //        ToolStripSeparator separator = new ToolStripSeparator();
            //        _toolStripItems.Add(separator);
            //    }
            //}
        }

        /// <summary>
        /// 把所有的工具栏按钮的状态设置为Unchecked
        /// </summary>
        private void UnCheckedButtons()
        {
            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripButton)
                {
                    ToolStripButton btn = (ToolStripButton)item;
                    btn.CheckState = CheckState.Unchecked;
                }
            }
        }

        #endregion

        #region IGetPropertiesForPanelable 成员

        public object[] GetPropertiesForPanel()
        {
            if (_willClose)
            {
                return new object[0];
            }
            if (!Service.Sdsite.IsOpened)
            {
                return new object[0];
            }
            List<Rect> list = TmpltDesign.DrawPanel.ListRect.GetSelectedRects();    
            return list.ToArray();
        }

        public event EventHandler PropertiesChanged;

        protected void OnPropertiesChanged(EventArgs e)
        {
            if (PropertiesChanged != null)
            {
                PropertiesChanged(this, e);
            }
        }

        void DrawPanel_SelectingRectEvent(object sender, EventArgs e)
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

        #region ResourcesReader

        public string GetText(string key)
        {
            return ResourcesReader.GetText(key, this);
        }

        public Icon GetIcon(string key)
        {
            return ResourcesReader.GetIcon(key, this);
        }

        public Image GetImage(string key)
        {
            return ResourcesReader.GetImage(key, this);
        }

        public Cursor GetCursor(string key)
        {
            return ResourcesReader.GetCursor(key, this);
        }

        public byte[] GetBytes(string key)
        {
            return ResourcesReader.GetBytes(key, this);
        }
        #endregion

    }
}
