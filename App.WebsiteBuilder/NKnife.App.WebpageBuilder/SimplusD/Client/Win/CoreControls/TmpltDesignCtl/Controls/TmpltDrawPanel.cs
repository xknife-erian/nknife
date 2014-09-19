using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;

using Jeelu.SimplusD;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltDrawPanel : DrawPanel
    {
        #region 属性成员定义

        /// <summary>
        /// 获取模板文档
        /// </summary>
        public TmpltXmlDocument TmpltDoc { get; set; }

        Image _lockedImg;
        Image _selectedImg;
        Image _hasSnip;
        Image _hasContentSnip;

        #endregion

        #region 构造函数

        public TmpltDrawPanel(DesignPanel tDPanel, int width, int height, Image backImage)
            : base(tDPanel, width, height, backImage)
        {
            ResourcesReader.SetObjectResourcesHelper(this);

            InitializeComponent();

            //双缓存
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            _lockedImg = GetImage("lockedImage");
            _selectedImg = GetImage("selectedImage");
            _hasSnip = GetImage("hasSnipImage");
            _hasContentSnip = GetImage("hasContentSnipImage");

        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 检查是否含有正文型页面片
        /// </summary>
        /// <returns></returns>
        private bool GetHasContentSnip()
        {
            if (ListRect.SnipRectList.Count > 0)
            {
                foreach (Rect rect in ListRect.SnipRectList)
                {
                    if (rect.SnipType == PageSnipType.Content)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 打开或添加页面片属性
        /// </summary>
        /// <param name="selectedRect"></param>
        private void ChangeSnipPerperty(SnipRect selectedRect)
        {
            if (selectedRect.HasSnip)
            {
                ///打开页面片
                Form snipForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.SnipDesigner, selectedRect.SnipID, (TDPanel as TmpltDesignerPanel).TmpltID);
            }
            else
            {
                TmpltXmlDocument doc = TmpltDoc;
                TmpltDesignerPanel tDpanel = TDPanel as TmpltDesignerPanel;
                //tDpanel.SaveTmplt(doc);
                tDpanel.SaveRects();
                NewSnipSetupForm form = new NewSnipSetupForm(doc, selectedRect, GetHasContentSnip());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SnipXmlElement snipElement = doc.GetSnipElementById(selectedRect.SnipID);
                    snipElement.SnipName = form.SnipName;
                    snipElement.SnipType = form.SnipType;
                    snipElement.Width = selectedRect.Width.ToString() + "px";
                    snipElement.Height = selectedRect.Height.ToString() + "px";
                    snipElement.Title = form.SnipName;
                    snipElement.HasSnip = true;
                    
                    tDpanel.SaveRects();
                    selectedRect.HasSnip = true;
                    TDPanel.Modified = true;
                    selectedRect.SnipType = form.IsContent ? PageSnipType.Content : PageSnipType.General;
                    if (form.IsContent)
                    {
                        if (!doc.HasContentSnip)
                        {
                            doc.HasContentSnip = true;
                        }
                    }
                    TDPanel.HasContentSnip = form.IsContent;
                    ///打开页面片
                    Form snipForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.SnipDesigner, selectedRect.SnipID, (TDPanel as TmpltDesignerPanel).TmpltID);
                }
            }
        }

        /// <summary>
        /// 获取当前矩形的底面图片
        /// </summary>
        /// <param name="snipRect"></param>
        /// <returns></returns>
        private string GetCurBackImg(Rect rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);

            Image _drawPanelBack = BackImage;
            XmlElement ele = TmpltDoc.DocumentElement;
            if (Utility.Convert.StringToBool(ele.GetAttribute("hasBackImg")))
            {
                AnyXmlElement imgEle = (AnyXmlElement)ele.SelectSingleNode("backImage");
                _drawPanelBack = Utility.Convert.Base64ToImage(imgEle.CDataValue);
            }

            g.DrawImage(_drawPanelBack, 0, 0, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), GraphicsUnit.Pixel);
            
            return  @"url(${srs_" + Service.Sdsite.CurrentDocument.Resources.ImageSaveAsResources(bmp, rect.SnipID) + "})";

        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        /// 是否可以粘贴(有相应格式的数据)
        /// </summary>
        /// <returns></returns>
        public static bool isPastable()
        {
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(SnipData).FullName;

            return dataObj.GetDataPresent(format);
        }

        #endregion

        #region 系统事件重写

        /// <summary>
        /// 打开右键菜单的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void RectMenu_Opening(object sender, CancelEventArgs e)
        {
            //生成div层次结构

            ///复制页面片数据
            if (ListRect.GetSelectedRects().Count == 1 && ListRect.GetSelectedRects()[0].HasSnip)
            {
                this.ContextMenuStrip.Items["copySnipData"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["copySnipData"].Enabled = false;
            }

            ///撤销
            if (CommandList.IsExistUndo())
            {
                this.ContextMenuStrip.Items["undo"].Enabled = true;
                this.ContextMenuStrip.Items["undo"].Text = "撤销"; //+ this.CommandList.CurCommand.Value.GetCommandInfo();
            }
            else
            {
                this.ContextMenuStrip.Items["undo"].Enabled = false;
                this.ContextMenuStrip.Items["undo"].Text = "不能撤销";
            }

            ///重做
            if (CommandList.IsExistRedo())
            {
                this.ContextMenuStrip.Items["redo"].Enabled = true;

                LinkedListNode<BaseCommand> reDoCommandNode = CommandList.CurCommand;
                if (CommandList.CurCommand == null)
                {
                    reDoCommandNode = CommandList.Commands.First;
                }
                else
                {
                    reDoCommandNode = CommandList.CurCommand.Next;
                }
                this.ContextMenuStrip.Items["redo"].Text = "重做" + reDoCommandNode.Value.GetCommandInfo();
            }
            else
            {
                this.ContextMenuStrip.Items["redo"].Enabled = false;
                this.ContextMenuStrip.Items["redo"].Text = "不能重做";
            }

            base.RectMenu_Opening(sender, e);
        }

        /// <summary>
        /// 双击事件之响应代码
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            this.Select();///获取焦点    
            if (DrawType == EnumDrawType.MouseSelect ||
                DrawType == EnumDrawType.MouseSelectRect)
            {
                List<Rect> selectedRects = ListRect.GetSelectedRects();
                List<PartitionLine> selectedLines = ListLine.GetSelectedLines();
                List<Rect> curSelectedRects = new List<Rect>();

                FirstRealPoint = BacktrackMouse(e);
                ///遍历整个rectlist
                foreach (Rect rect in this.ListRect.SnipRectList)
                {
                    if ((!rect.IsDeleted) && rect.IsSelectable(FirstRealPoint))
                    {
                        if ((!rect.IsSelected) || selectedRects.Count > 1)
                        //未被选择,或者如果已经被选择,且有其他矩形被选择,则先激发其他更改选择矩形的命令
                        {
                            curSelectedRects.Add(rect);
                            SelectCommand selectRectCommand = new SelectCommand(TDPanel,
                                curSelectedRects,
                                selectedRects,
                                new List<PartitionLine>(),
                                selectedLines);

                            selectRectCommand.Execute();
                            CommandList.AddCommand(selectRectCommand);
                        }
                        ///弹出属性对话框以查看和修改
                        ChangeSnipPerperty((SnipRect)rect);
                        break;
                    }
                }
            }

            base.OnMouseDoubleClick(e);
        }

        #endregion

        #region 消息和事件处理

        /// <summary>
        /// 拷贝页面片数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CopySnipData_Click(object sender, EventArgs e)
        {
            if (ListRect.GetSelectedRects().Count == 1)
            {
                SnipRect rect = (SnipRect)ListRect.GetSelectedRects()[0];
                if (rect.HasSnip)
                {
                    rect.SnipData = new SnipData(TmpltDoc.GetSnipElementById(rect.SnipID));
                    rect.CopyToClipboard();
                }
                else
                {
                    MessageService.Show(StringParserService.Parse("${res:tmpltDesign.tmpltDrawPanel.message.hasNoneSnip}"));
                }
            }
        }

        /// <summary>
        /// 粘贴菜单执行函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pasteSnipData_Click(object sender, EventArgs e)
        {
            foreach (Rect rect in ListRect.GetSelectedRects())
            {
                if (rect.IsLocked || rect.HasSnip)
                {
                    MessageService.Show("${res:tmpltDesign.tmpltDrawPanel.message.hasLockedRect}");
                    return;
                }
            }
            PasteSnipDataCommand pasteCommand = new PasteSnipDataCommand(this.TDPanel,
                ListRect.GetSelectedRects(),
                SnipData.GetFromClipboard(), TmpltDoc);
            pasteCommand.Execute();
            this.TDPanel.CommandList.AddCommand(pasteCommand);

        }

        /// <summary>
        /// 属性菜单执行函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SnipPerperty_Click(object sender, EventArgs e)
        {
            SnipRect selectedRect = (SnipRect)ListRect.GetSelectedRects()[0];
            ChangeSnipPerperty(selectedRect);
        }        
       
        /// <summary>
        /// 右键菜单的“放大”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ZoomIn_Click(object sender, EventArgs e)
        { 
            ZoomIn(); 
        }

        /// <summary>
        /// 右键菜单的“缩小”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        void ZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        /// <summary>
        /// 右键菜单的“实际大小”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InitZoom_Click(object sender, EventArgs e)
        {
            InitZoom();
        }

        /// <summary>
        /// 右键菜单的“锁定矩形”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LockRect_Click(object sender, EventArgs e)
        {
            LockRects();
        }

        /// <summary>
        /// 解除锁定矩形
        /// </summary>
        void DisLockRect_Click(object sender, EventArgs e)
        {
            DisLockRects();
        }

        /// <summary>
        /// 响应隐藏标尺
        /// </summary>
        void HideRuler_Click(object sender, EventArgs e)
        {
            TDPanel.IsRulerHide = !TDPanel.IsRulerHide;
            ((ToolStripMenuItem)RectMenu.Items["hideRuler"]).Checked = TDPanel.IsRulerHide;

        }

        /// <summary>
        /// 响应删除页面片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeleteSnip_Click(object sender, EventArgs e)
        {
            DeleteSnip();
            TDPanel.Modified = true;
        }

        /// <summary>
        /// 删除直线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Delete_Click(object sender, EventArgs e)
        {
            DeleteLines();
        }

        /// <summary>
        /// 响应撤销命令
        /// </summary>
        void Undo_Click(object sender, EventArgs e)
        {
            if (CommandList.IsExistUndo())
            {
                this.CommandList.UnDo();
            }
        }

        /// <summary>
        /// 响应重做命令
        /// </summary>
        void Redo_Click(object sender, EventArgs e)
        {
            if (CommandList.IsExistRedo())
            {
                this.CommandList.ReDo();
            }
        }

        /// <summary>
        /// 响应合并矩形命令
        /// </summary>
        void MergeRect_Click(object sender, EventArgs e)
        {
            ///判断是否可以合并
            List<Rect> dismergableRects = this.ListRect.GetDisMergableRects();
            if (!ListRect.IsRectMergable())///可以合并
            {
               // MessageBox.Show("存在锁定或所选矩形无法完成合并！");
                MessageService.Show("${res:tmpltDesignTip.recMerge}", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                foreach (Rect rect in dismergableRects)
                {
                    //DrawRectFlash(rect);
                }
            }
            else
            {
                MergeRectCommand mergeRectCommand = new MergeRectCommand(TDPanel);
                mergeRectCommand.Execute();
            }
        }

        /// <summary>
        /// 响应分割矩形
        /// </summary>
        void PartRect_Click(object sender, EventArgs e)
        {
            Rect selectedRect = ListRect.GetSelectedRects()[0];
            ///触发事件的执行
            PartRectEventArgs partRectArgs = new PartRectEventArgs(selectedRect);
            TDPanel.OnPartRect(partRectArgs);

            if (!partRectArgs.Cancel)
            {
                ///生成新的被分割的矩形
                List<Rect> newRects = ListRect.PartRect(selectedRect, partRectArgs.IsRow, partRectArgs.PartNum);
                List<PartitionLine> newLines = ListLine.PartRect(selectedRect, partRectArgs.IsRow, partRectArgs.PartNum);

                PartRectCommand partrectCommand = new PartRectCommand(TDPanel, partRectArgs.IsRow, newRects, selectedRect, newLines);
                partrectCommand.Execute();
                TDPanel.CommandList.AddCommand(partrectCommand);
            }
        }

        /// <summary>
        /// 响应当前模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TmpltProperty_Click(object sender, EventArgs e)
        {
            CssSetupForm form = new CssSetupForm(TmpltDoc);
            form.Owner = this.FindForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                TmpltDoc.DocumentElement.SetAttribute("webCss", form.CSSText);
                this.DrawFrame.TDPanel.Modified = true;
            }
        }

        /// <summary>
        /// 响应使用当前背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CutCurrentBackImg_Click(object sender, EventArgs e)
        {
            if (BackImage == null)
            {
                return;
            }
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            Bitmap bmp = new Bitmap(selectedRect.Width, selectedRect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(BackImage, new Rectangle(0, 0, bmp.Width, bmp.Height),
                selectedRect.X, selectedRect.Y, selectedRect.Width, selectedRect.Height, GraphicsUnit.Pixel);
            g.Flush();
            g.Dispose();
            SaveResourceImageForm form = new SaveResourceImageForm(bmp);
            form.Owner = this.FindForm();
            form.Text = StringParserService.Parse("${res:tmpltDesign.DrawPanel.contextMenu.cutCurrentBackImg}");
            //form.OrderText = StringParserService.Parse("${res:tmpltDesign.DrawPanel.contextMenu.cutCurrentBackImgFormOrderText}");
            if (form.ShowDialog() == DialogResult.OK)
            {                
                //Service.Sdsite.CurrentDocument.Resources.ImageSaveAsResources(bmp, form.Value);
            }
        }

        /// <summary>
        /// 编辑整个模板的CSS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TmpltCssProperty_Click(object sender, EventArgs e)
        { }

        /// <summary>
        /// 响应当前矩形Css属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void CurRectProperty_Click(object sender, EventArgs e)
        {
            ((TmpltDesignerPanel)TDPanel).SaveTmplt(TmpltDoc);
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            if (selectedRect is SnipRect)
            {
                SnipRect snipRect = (SnipRect)selectedRect;
                SnipXmlElement ele = TmpltDoc.GetSnipElementById(snipRect.SnipID);               

                //writed by zhenghao 2008.05.20
                CssSettingForm form = new CssSettingForm(snipRect.ToCss);
                form.Owner = this.FindForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    snipRect.ToCss = form.CssText;
                    ele.Css = snipRect.ToCss;
                    this.DrawFrame.TDPanel.Modified = true;
                }
            }
        }

        /// <summary>
        /// 响应设置当前模板背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackImgSetup_Click(object sender, EventArgs e)
        {
            OpenFileDialog newPic = new OpenFileDialog();
            newPic.Filter = "jpeg图片文件 | *.jpg;*.jpeg | png图片文件 | *.png";
            newPic.Multiselect = false;
            if (newPic.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(newPic.FileName))
                {
                    TmpltDoc.HasBackImage = false;
                    this.DrawFrame.TDPanel.Modified = true;
                }
                else
                {
                    Image newImg = Image.FromFile(newPic.FileName);
                    TmpltDoc.HasBackImage = true;
                    TmpltDoc.BackImage = newImg;
                    this.DrawFrame.TDPanel.Modified = true;
                }
            }
        }

        #endregion

        #region 基类函数重写

        /// <summary>
        /// 控件矩形元素重画
        /// </summary>
        protected override void DrawPanelListRect(Graphics g)
        {
            int j = 0;
            for (int i = 0; i < ListRect.SnipRectList.Count; i++)
            {
                if (ListRect.SnipRectList[i].IsDeleted)
                {
                    continue;
                }
                j++;
                DrawRect(ListRect.SnipRectList[i], g);
            }
        }

        /// <summary>
        /// 矩形元素重画
        /// </summary>
        /// <param name="rect"></param>
        public override void DrawRect(Rect rect)
        {
            Graphics g = this.CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            DrawRect(rect, g);
        }

        /// <summary>
        /// 矩形元素重画
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="g"></param>
        public override void DrawRect(Rect rect, Graphics g)
        {
            Pen penBlack = new Pen(Color.Black, PenSize);
            Pen penSelected = new Pen(this.SelectedRectColor, PenSize * BoldPenTimes);

            if (rect.IsDeleted)
            {
                return;
            }
            Rectangle rectangle = new Rectangle(
                rect.X,
                rect.Y,
                rect.Width,
                rect.Height);
            Rectangle rectangle2 = new Rectangle(
                rect.X,
                rect.Y,
                rect.Width + 5,
                rect.Height + 5);
            if (rect.IsSelected)
            {
                g.FillRectangle(RectSelectedBrush, rectangle);
                g.DrawRectangle(Pens.Black, rectangle);
                g.DrawImage(_selectedImg, new PointF(rectangle.X + penBlack.Width, rectangle.Y + penBlack.Width));//画选择标志
            }
            else
            {
                g.DrawRectangle(penBlack, rectangle);
            }
            if (rect.IsLocked)
            {
                g.FillRectangle(LockedBrush, rectangle);
                g.DrawImage(_lockedImg, new PointF(rectangle.X + 20 + penBlack.Width,rectangle.Y + penBlack.Width));//画选择标志
            }
            if (rect is SnipRect)
            {
                SnipRect sRect = (SnipRect)rect;
                if (!sRect.HasSnip)
                {
                    
                }
                else if (sRect.SnipType != PageSnipType.Content)
                {
                    g.FillRectangle(HasSnipBrush, rectangle);
                    g.DrawImage(_hasSnip, new PointF(rectangle.X + penBlack.Width, rectangle.Y + 20 + penBlack.Width));//画选择标志
            
                }
                else if (sRect.SnipType == PageSnipType.Content)
                {
                    g.FillRectangle(HasContentSnipBrush, rectangle);
                    g.DrawImage(_hasContentSnip, new PointF(rectangle.X  + penBlack.Width, rectangle.Y + 20 + penBlack.Width));//画选择标志
            
                }
            }
            //释放资源
            penBlack.Dispose();
            penSelected.Dispose();
        }

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        protected override void InitContextMenu()
        {
            ToolStripMenuItem rectItem;

            base.InitContextMenu();

            ///添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());
            
            //添加复制页面片数据菜单
            rectItem = new ToolStripMenuItem(GetText("copySnipData"));
            rectItem.Name = "copySnipData";
            rectItem.ShortcutKeys = Keys.Control | Keys.C;
            rectItem.Click += new EventHandler(CopySnipData_Click);
            this.RectMenu.Items.Add(rectItem);

            //添加粘贴页面片数据菜单
            rectItem = new ToolStripMenuItem(GetText("pasteSnipData"));
            rectItem.Name = "pasteSnipData";
            rectItem.ShortcutKeys = Keys.Control | Keys.V;
            rectItem.Click += new EventHandler(pasteSnipData_Click);
            this.RectMenu.Items.Add(rectItem);

            ///添加页面片属性菜单
            rectItem = new ToolStripMenuItem(GetText("snipPerperty"));
            rectItem.Name = "snipPerperty";
            rectItem.Click += new EventHandler(SnipPerperty_Click);
            this.RectMenu.Items.Add(rectItem);

            //删除所选页面片
            rectItem = new ToolStripMenuItem(GetText("deleteSnip"));
            rectItem.Name = "deleteSnip";
            rectItem.Click += new EventHandler(DeleteSnip_Click);
            this.RectMenu.Items.Add(rectItem);
        }
       
        /// <summary>
        /// 初始化右键菜单的内容
        /// </summary>
        protected override void InitContextMenuItems()
        {
            ToolStripMenuItem rectItem;

            //当前矩形Css属性
            rectItem = new ToolStripMenuItem(GetText("curRectProperty"));
            rectItem.Name = "curRectProperty";
            rectItem.ShortcutKeys = Keys.Alt | Keys.Enter;
            rectItem.Click += new EventHandler(CurRectProperty_Click);
            this.RectMenu.Items.Add(rectItem);

            //整个模板Css
            //zhenghao at 2008-06-24 15:56
            rectItem = new ToolStripMenuItem(GetText("tmpltCssItem"));
            rectItem.Name = "tmpltCssItem";
            rectItem.Click += new EventHandler(TmpltCssProperty_Click);
            this.RectMenu.Items.Add(rectItem);

            //***添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());

            //截取当前背景
            rectItem = new ToolStripMenuItem(GetText("cutCurrentBackImg"));
            rectItem.Name = "cutCurrentBackImg";
            rectItem.Click += new EventHandler(CutCurrentBackImg_Click);
            this.RectMenu.Items.Add(rectItem);

            //***添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());

            //放大
            rectItem = new ToolStripMenuItem(GetText("zoomIn"));
            rectItem.Name = "zoomIn";
            rectItem.ShortcutKeys = Keys.Control | Keys.Add;
            rectItem.Click += new EventHandler(ZoomIn_Click);
            this.RectMenu.Items.Add(rectItem);

            //缩小
            rectItem = new ToolStripMenuItem(GetText("zoomOut"));
            rectItem.Name = "zoomOut";
            rectItem.ShortcutKeys = Keys.Control | Keys.Subtract;
            rectItem.Click += new EventHandler(ZoomOut_Click);
            this.RectMenu.Items.Add(rectItem);            

            //实际大小
            rectItem = new ToolStripMenuItem(GetText("initZoom"));
            rectItem.Name = "initZoom";
            //rectItem.ShortcutKeys = Keys.Control | Keys.Subtract;
            rectItem.Click += new EventHandler(InitZoom_Click);
            this.RectMenu.Items.Add(rectItem);

            //添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());

            //添加撤销菜单
            rectItem = new ToolStripMenuItem(GetText("undo"));
            rectItem.Name = "undo";
            rectItem.ShortcutKeys = Keys.Control | Keys.Z;
            rectItem.Click += new EventHandler(Undo_Click);
            this.RectMenu.Items.Add(rectItem);

            //重做菜单
            rectItem = new ToolStripMenuItem(GetText("redo"));
            rectItem.Name = "redo";
            rectItem.ShortcutKeys = Keys.Control | Keys.Y;
            rectItem.Click += new EventHandler(Redo_Click);
            this.RectMenu.Items.Add(rectItem);

            //添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());


            //添加删除菜单
            rectItem = new ToolStripMenuItem(GetText("deleteLine"));
            rectItem.Name = "deleteLine";
            rectItem.ShortcutKeys = Keys.Delete;
            rectItem.Click += new EventHandler(Delete_Click);
            this.RectMenu.Items.Add(rectItem);

            //添加分割线
            this.RectMenu.Items.Add(new ToolStripSeparator());

            //合并矩形
            rectItem = new ToolStripMenuItem(GetText("mergeRect"));
            rectItem.Name = "mergeRect";
            rectItem.Click += new EventHandler(MergeRect_Click);
            this.RectMenu.Items.Add(rectItem);

            //分割矩形
            rectItem = new ToolStripMenuItem(GetText("partRect"));
            rectItem.Name = "partRect";
            rectItem.Click += new EventHandler(PartRect_Click);
            this.RectMenu.Items.Add(rectItem);

            //锁定矩形
            rectItem = new ToolStripMenuItem(GetText("lockRect"));
            rectItem.Name = "lockRect";
            rectItem.Click += new EventHandler(LockRect_Click);
            this.RectMenu.Items.Add(rectItem);

            //解开锁定矩形
            rectItem = new ToolStripMenuItem(GetText("disLockRect"));
            rectItem.Name = "disLockRect";
            rectItem.Click += new EventHandler(DisLockRect_Click);
            rectItem.Enabled = false;
            this.RectMenu.Items.Add(rectItem);

            //标尺
            rectItem = new ToolStripMenuItem(GetText("hideRuler"));
            rectItem.Name = "hideRuler";
            rectItem.ShortcutKeys = Keys.Control | Keys.R;
            rectItem.Click += new EventHandler(HideRuler_Click);
            rectItem.Checked = false;
            this.RectMenu.Items.Add(rectItem);
        }

        #endregion
    }
}
