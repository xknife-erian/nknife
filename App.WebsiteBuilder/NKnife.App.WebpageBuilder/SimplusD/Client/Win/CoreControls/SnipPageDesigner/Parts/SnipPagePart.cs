using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    [Serializable]
    public class SnipPagePart : IPartContainer,INavigateNode
    {
        #region 构造函数
        
        protected SnipPagePart(SnipPageDesigner designer)
        {
            ResourcesReader.SetObjectResourcesHelper(this);
            _designer = designer;
            Text = "";
            Padding = new System.Windows.Forms.Padding(0);
            Index = -1;
            HtmlTagName = "div";
            FactLines = 2;
            _cssSection = new CssSection();
            PartType = SnipPartType.Static;
            ChildParts = new SnipPagePartCollection(this);
            _classPartContainer = new ClassPartContainer(this);
            Random random = new Random();
            //BackColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            _cssSection.Properties["width"] = "100%";
            _cssSection.Properties["height"] = "20px";
            _cssSection.Properties["float"] = "left";
            BoxIconXOffset = 0;
            BoxIconYOffset = -8;
            BoxIconSize = new Size(16, 16);
        }

        #endregion

        #region 字段和属性

        /// <summary>
        /// 最小宽度
        /// </summary>
        int _minWidth = SoftwareOption.SnipDesigner.PieRadii;
        /// <summary>
        /// 是否使用最小值
        /// </summary>
        public bool UsedMinWidth { get; set; }
        /// <summary>
        /// 获取或设置链接URL节点
        /// </summary>
        public AnyXmlElement AElement { get; set; }

        /// <summary>
        /// 获取或设置自定义ID
        /// </summary>
        public string CustomID { get; set; }

        private string _pageText;
        /// <summary>
        /// 页面HTML字符串
        /// </summary>
        public string PageText
        {
            get { return _pageText; }
            set
            {
                if (_pageText != value)
                {
                    Designer.CmdManager.AddExecSetPropertyPartCommand<string>(this,
                        PageText, value, SetPageTextCore);
                }
            }
        }

        private void SetPageTextCore(string value)
        {
            _pageText = value;
        }
        private SnipPageDesigner _designer;

        private ClassPartContainer _classPartContainer;

        protected int _multiLine = 2;

        /// <summary>
        /// edit by zhenghao at 2008-06-23 9:45
        /// 容器型指示图标的横坐标位移
        /// </summary>
        public int BoxIconXOffset { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-23 9:45
        /// 容器型指示图标的纵坐标位移
        /// </summary>
        public int BoxIconYOffset { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-23 9:45
        /// 容器型指示图标的大小
        /// </summary>
        public Size BoxIconSize { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-17 09:50
        /// 获取或设置图片Src
        /// </summary>
        public string PictrueSrc { get; set; }

        CssSection _cssSection;
        /// <summary>
        /// 获取PartID
        /// </summary>
        public string PartID { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-13 15:00
        /// 获取或设置显示样式
        /// </summary>
        public DisplayType DisplayType { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-13 15:00
        /// 获取或设置自定义文本
        /// </summary>
        public string AppointedText { get; set; }       

        /// <summary>
        /// edit by zhenghao at 2008-06-13 15:00
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置CSS字符串
        /// </summary>
        [PropertyPad(1, 2, "partCss",IsReadOnly=true,
            MainControlType = MainControlType.TextBox)]
        public string Css
        {
            get { return _cssSection.ToString(); }
            set
            {
                if (Css != value)
                {
                    Designer.CmdManager.AddExecSetPropertyPartCommand<string>(this, Css, value,
                        new SetPropertyCore<string>(SetCssCore), PartAction.Relayout);
                }
            }
        }

        private void SetCssCore(string value)
        {
            _cssSection = CssSection.Parse(value);
        }

        /// <summary>
        /// 获取或设置Part的Text
        /// </summary>
        [PropertyPad(0, 1, "partText", MainControlType = MainControlType.TextBox,IsReadOnly = true)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 获取或设置这部分的类型
        /// </summary>
        [PropertyPad(0, 0, "partType", MainControlType=MainControlType.TextBox)]
        public virtual SnipPartType PartType { get; private set; }

        public int Index { get; set; }

        public virtual string HtmlTagName { get; set; }

        /// <summary>
        /// 获取根据层级结构的Index生成的一段文本
        /// </summary>
        public string IndexTitle
        {
            get
            {
                SnipPagePart tempPart = this;
                string partIndex = tempPart.Index.ToString();
                while ((tempPart = tempPart.ParentContainer as SnipPagePart) != null)
                {
                    partIndex = tempPart.Index + "-" + partIndex;
                }
                return partIndex;
            }
        }

        public int Level { get; internal set; }

        /// <summary>
        /// 获取当前Part是否Box型。Box型才会有子Part。
        /// </summary>
        public bool IsBox
        {
            get
            {
                switch (PartType)
                {
                    case SnipPartType.List:
                    case SnipPartType.ListBox:
                    case SnipPartType.Box:
                   // case SnipPartType.Path:
                        return true;
                }
                return false;
            }
        }

        public Padding Padding { get;private set; }

        /// <summary>
        /// 所属设计器
        /// </summary>
        public SnipPageDesigner Designer
        {
            get
            {
                if (_designer != null)
                {
                    return _designer;
                }
                else if (ParentContainer != null)
                {
                    return ParentContainer.Designer;
                }
                return null;
            }
        }

        public IPartContainer ParentContainer { get; internal set; }

        public bool HasChild
        {
            get { return ChildParts.Count > 0; }
        }

        /// <summary>
        /// 以其父Part为参照坐标
        /// </summary>
        public Rectangle Bounds { get; internal set; }

        /// <summary>
        /// SnipPagePart集合
        /// </summary>
        public SnipPagePartCollection ChildParts{ get; private set; }

        public Point LocationForDesigner
        {
            get { return PointToDesigner(Bounds.Location); }
        }

        /// <summary>
        /// 以Designer为参照坐标
        /// </summary>
        public Rectangle BoundsForDesigner
        {
            get { return new Rectangle(LocationForDesigner, Bounds.Size); }
        }

        public bool Selected
        {
            get { return Designer.SelectedParts.Contains(this); }
            set
            {
                if (value == true)
                {
                    if (Selected == false)
                    {
                        Designer.SelectedParts.Add(this);
                    }
                }
                else
                {
                    if (Selected == true)
                    {
                        Designer.SelectedParts.Remove(this);
                    }
                }
            }
        }

        public int FactWidth
        {
            get
            {
                return (Float_Css != "none") ? WidthPixel : this.Bounds.Width;
            }
        }

        public void Invalidate()
        {
            if (Designer != null)
            {
                Designer.Invalidate(this.BoundsForDesigner);
            }
        }

        [PropertyPad(0, 0, "partMultiLine", MainControlType = MainControlType.NumericUpDown,NumericUpDownDecimalPlaces=0,NumericUpDownMax=100,NumericUpDownMin=1,NumericUpDownStep=1,IsReadOnly=false)]
        public int MultiLine
        {
            get { return _multiLine; }
            set
            {
                if (_multiLine != value)
                {
                    Designer.CmdManager.AddExecSetPropertyPartCommand<int>(this, MultiLine, value,
                        new SetPropertyCore<int>(SetMultiLineCore), PartAction.Relayout);
                }
            }
        }

        internal void SetMultiLineCore(int value)
        {
            if (_multiLine != value)
            {
                _multiLine = value;
                OnMultiLineChanged(EventArgs.Empty);
            }
        }

        #endregion

        /// <summary>
        /// 检查传入的Part是否可以放入当前Part中
        /// </summary>
        public virtual bool CanInto(SnipPagePart targetPart)
        {
            ///静态的和导航的可以放置在任何位置
            if (targetPart.PartType == SnipPartType.Static
                ||targetPart.PartType == SnipPartType.Navigation)
            {
                return true;
            }

            return false;
        }

        public event EventHandler MultiLineChanged;
        protected virtual void OnMultiLineChanged(EventArgs e)
        {
            ResetFactLines();

            if (MultiLineChanged != null)
            {
                MultiLineChanged(this, e);
            }
        }

        #region CSS布局的一些属性

        /// <summary>
        /// 当前Part的浮动属性
        /// </summary>
        public string Float_Css
        {
            get { return _cssSection.Properties["float"]; }
            set
            {
                Designer.CmdManager.AddExecSetPropertyPartCommand<string>(this, Float_Css, value,
                    new SetPropertyCore<string>(SetFloat_CssCore), PartAction.Relayout);
            }
        }

        protected void SetFloat_CssCore(string value)
        {
            _cssSection.Properties["float"] = value;
        }  

        /// <summary>
        /// 当前Part的宽度
        /// </summary>
        public string Width_Css
        {
            get 
            {
                int _width = CssLongnessToPixel(_cssSection.Properties["width"]);

                if (_width < (_minWidth*2))
                {
                    UsedMinWidth = true;
                    return _minWidth.ToString();
                }
                UsedMinWidth = false;
                return _width.ToString();
            }
            set
            {
                this.Designer.CmdManager.AddExecSetPropertyPartCommand<string>(this, Width_Css, value,
                    new SetPropertyCore<string>(SetWidth_CssCore), PartAction.Relayout);
            }
        }
        internal void SetWidth_CssCore(string value)
        {
            _cssSection.Properties["width"] = value;
        }

        public int WidthPixel { get; set; }
      
        /// <summary>
        /// 内部的行数
        /// </summary>
        public int InnerLines { get; private set; }

        /// <summary>
        /// 重新计算InnerLines
        /// </summary>
        public void ResetInnerLines(int innerLines)
        {
            if (innerLines != InnerLines)
            {
                InnerLines = innerLines;
                ResetFactLines();
                OnInnerLinesChanged(EventArgs.Empty);
            }
        }

        public event EventHandler InnerLinesChanged;
        protected virtual void OnInnerLinesChanged(EventArgs e)
        {
            if (InnerLinesChanged != null)
            {
                InnerLinesChanged(this, e);
            }
        }

        /// <summary>
        /// 实际的行数(其值为MultiLine与InnerLines中较大的一个)
        /// </summary>
        public int FactLines { get; private set; }

        /// <summary>
        /// 重新计算FactLines
        /// </summary>
        public void ResetFactLines()
        {
            int temp = Math.Max(InnerLines, MultiLine);
            if (temp != FactLines)
            {
                FactLines = temp;

                int height = SnipPageDesigner.PartHeight * FactLines;
                this.Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, height);

                OnFactLinesChanged(EventArgs.Empty);
            }
        }

        public event EventHandler FactLinesChanged;
        protected virtual void OnFactLinesChanged(EventArgs e)
        {
            this.Designer.LayoutParts();

            if (FactLinesChanged != null)
            {
                FactLinesChanged(this, e);
            }
        }

        #endregion

        public int CssLongnessToPixel(string cssLongness)
        {
            if (string.IsNullOrEmpty(cssLongness))
            {
                return 0;
            }

            if (cssLongness.EndsWith("px", StringComparison.InvariantCultureIgnoreCase))
            {
                return int.Parse(cssLongness.Substring(0, cssLongness.Length - 2));
            }
            else if (cssLongness.EndsWith("%", StringComparison.InvariantCultureIgnoreCase))
            {
                if(ParentContainer != null)
                    return ParentContainer.WidthPixel * int.Parse(cssLongness.Substring(0, cssLongness.Length - 1)) / 100;
                else
                    return _designer.WidthPixel * int.Parse(cssLongness.Substring(0, cssLongness.Length - 1)) / 100;
            }
            else
            {
                return int.Parse(cssLongness);
            }
        }

        public void MoveTo(IPartContainer parentContainer, int index)
        {
            ///Index为-1，表示当前Part是新增Part，那么直接放到指定位置并打开编辑窗口
            if (this.Index == -1)
            {
                parentContainer.ChildParts.Insert(index, this);
                Designer.EditPart(this,true);
            }
            ///同一级，位置一样，则直接返回
            else if (ParentContainer == parentContainer && this.Index == index)
            {
                return;
            }
            ///不能把当前Part移到自己的子Part里去
            else if (IsSub(parentContainer))
            {
                return;
            }
            ///位置不一样，移动节点
            else
            {
                ///开始批命令操作
                try
                {
                    ParentContainer.Designer.CmdManager.BeginBatchCommand();

                    ///移动节点
                    if (ParentContainer == parentContainer && this.Index < index)
                    {
                        index--;
                    }
                    this.ParentContainer.ChildParts.Remove(this);
                    parentContainer.ChildParts.Insert(index, this);
                }
                finally
                {
                    ///结束批命令操作
                    ParentContainer.Designer.CmdManager.EndBatchCommand();
                }
            }
        }

        /// <summary>
        /// 判断subContainer是否是当前Part的孙Part
        /// </summary>
        bool IsSub(IPartContainer subContainer)
        {
            IPartContainer subContainerParent = subContainer;
            while (subContainerParent != null && !(subContainerParent is SnipPageDesigner))
            {
                if (subContainerParent == this)
                {
                    return true;
                }
                subContainerParent = subContainerParent.ParentContainer;
            }

            return false;
        }

        #region 自定义的一些事件

        public event EventHandler Click;

        public event EventHandler DoubleClick;

        internal protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        internal protected virtual void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(this, e);
            }
        }

        #endregion

        /// <summary>
        /// 检测一个点是否在这个Part内
        /// </summary>
        /// <param name="point">相对于SnipPageDesigner坐标的一个坐标点</param>
        public bool HitTest(Point point)
        {
            ///edit by zhenghao at 2008-06-23 9:45
            ///对已经选定的容器型part进行检测
            if (Selected && IsBox)
            {
                int x = BoundsForDesigner.X + BoxIconXOffset;
                int y = BoundsForDesigner.Y + BoxIconYOffset;
                Rectangle _boxIconRect = new Rectangle(new Point(x, y), BoxIconSize);
                return _boxIconRect.Contains(point) || BoundsForDesigner.Contains(point);

            }
            ///Delete:return BoundsForDesignerDisplay.Contains(point);
            return BoundsForDesigner.Contains(point);
        }

        /// <summary>
        /// 检测一个矩形是否与这个Part相交
        /// </summary>
        /// <param name="point">相对于SnipPageDesigner坐标的一个矩形</param>
        public bool HitTest(Rectangle rect)
        {
            ///Delete:return BoundsForDesignerDisplay.IntersectsWith(rect);
            return BoundsForDesigner.IntersectsWith(rect);
        }

        public Point PointToDesigner(Point point)
        {
            int x = point.X;
            int y = point.Y;
            IPartContainer parentBox = this.ParentContainer;
            while (parentBox != null && !(parentBox is SnipPageDesigner))
            {
                x += parentBox.Bounds.X;
                y += parentBox.Bounds.Y;

                parentBox = parentBox.ParentContainer;
            }

            return new Point(x, y);
        }

        #region IPartContainer的实现

        public void PaintChildParts(Graphics g)
        {
            _classPartContainer.PaintChildParts(g);
        }

        public void LayoutParts()
        {
            _classPartContainer.LayoutParts();
        }

        /// <summary>
        /// 通过一个点获取其中的SnipPagePart
        /// </summary>
        /// <param name="point">相对于SnipPageDesigner的一个坐标点</param>
        /// <param name="isRecursiveChilds">是否递归查找其下的子Part</param>
        public SnipPagePart GetPartAt(Point point, bool isRecursiveChilds)
        {
            return _classPartContainer.GetPartAt(point, isRecursiveChilds);
        }

        #endregion

        static public SnipPagePart Parse(SnipPartXmlElement ele, SnipPageDesigner designer)
        {
            SnipPartType type = ele.SnipPartType;
            SnipPagePart part = null;

            switch (type)
            {
                case SnipPartType.Static:
                    {
                        part = SnipPagePart.Create(designer, type);
                        ((StaticPart)part).PageText = ele.CDataValue;
                        break;
                    }
                case SnipPartType.Box:
                    {
                        part = SnipPagePart.Create(designer, type);
                        break;
                    }
                case SnipPartType.List:
                    {
                        ListPart listPart = (ListPart)SnipPagePart.Create(designer, SnipPartType.List);                        
                        part = listPart;
                        break;
                    }
                case SnipPartType.ListBox:
                    {
                        ListBoxPart listBoxPart = (ListBoxPart)SnipPagePart.Create(designer, SnipPartType.ListBox);
                        listBoxPart.StyleType = ele.StyleType;
                        part = listBoxPart;
                        break;
                    }
                case SnipPartType.Navigation:
                    {
                        NavigationPart nPart = (NavigationPart)SnipPagePart.Create(designer, SnipPartType.Navigation);
                        nPart.ChannelID = ele.GetAttribute("channelId");
                        nPart.Text = ele.CDataValue;
                        part = nPart;
                        break;
                    }
                case SnipPartType.Attribute:
                    {
                        AttributePart attributepart = (AttributePart)SnipPagePart.Create(designer, SnipPartType.Attribute);
                        attributepart.AttributeName = ele.AttributeName;
                        attributepart.Text = ele.CDataValue;
                        part = attributepart;
                        break;
                    }
                case SnipPartType.Path:
                    {
                        PathPart pathPart = (PathPart)SnipPagePart.Create(designer, SnipPartType.Path);
                        pathPart.LinkDisplayType = (DisplayType)Enum.Parse(typeof(DisplayType), ele.GetAttribute("linkDisplayType"));
                        pathPart.SeparatorCode = ele.GetAttribute("separatorCode");
                        part = pathPart;
                        break;
                    }
                default:
                    throw new Exception("开发期异常。未知的SnipPartType:" + type);
            }
            part.PartType = type;
            part.Css = ele.SnipPartCss;
            part.PartID = ele.SnipPartId;
            part.CustomID = ele.CustomId;
            part.AElement = ele.AElement;
            return part;
        }

        /// <summary>
        /// 简单工厂，创建不同SnipPartType的Part
        /// </summary>
        static public SnipPagePart Create(SnipPageDesigner designer, SnipPartType type)
        {
            SnipPagePart part;

            switch (type)
            {
                case SnipPartType.None:
                case SnipPartType.Static:
                    {
                        part = new StaticPart(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.staticPartText}");
                        break;
                    }
                case SnipPartType.Box:
                    {
                        part = new SnipPagePart(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.box}");
                        break;
                    }
                case SnipPartType.Navigation:
                    {
                        part = new NavigationPart(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.navigationPartText}");
                        break;
                    }
                case SnipPartType.List:
                    {
                        part = ListPart.Create(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.listPartText}");
                        break;
                    }
                case SnipPartType.ListBox:
                    {
                        part = ListBoxPart.Create(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.listBoxPartText}");
                        break;
                    }
                case SnipPartType.Attribute:
                    {
                        part = AttributePart.Create(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.attributePartText}");
                        break;
                    }
                case SnipPartType.Path:
                    {
                        part = new PathPart(designer);
                        part.Title = StringParserService.Parse("${res:snipDesign.text.pathPartText}");
                        break;
                    }
                default:
                    throw new Exception("开发期异常。未知的SnipPartType:" + type);
            }
            part.PartID = XmlUtilService.CreateIncreaseId();
            part.PartType = type;            
            return part;
        }

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

        #region INavigateNode 成员

        string INavigateNode.Title
        {
            get { return this.Text; }
        }

        string INavigateNode.Message
        {
            get { return Title; }
        }

        bool INavigateNode.IsSelected
        {
            get { return Selected; }
        }

        #endregion
    }
}
