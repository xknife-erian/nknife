using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    internal class ClassPartContainer
    {
        #region 内部变量

        Image _floatLeft = ResourceService.GetResourceImage("fontlist.leftArrow");
        Image _floatRight = ResourceService.GetResourceImage("fontlist.rightArrow");
        Image _moveImg = ResourceService.GetResourceImage("selectedSnipPart.CanMove");
        //Image _floatNone = ResourceService.GetResourceImage("middle_align");

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取或设置显示间距
        /// </summary>
        public int DisplaySpace { get; set; }

        /// <summary>
        /// 是否使用渐变
        /// </summary>
        public bool UsingLinearGradient { get; set; }

        /// <summary>
        /// 获取扇形角半径
        /// </summary>
        public int PieRadii { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器中容器模块的颜色
        /// </summary>
        public Color BoxFillColor { get; set; }

        public Color BoxForeColor { get; set; }
       
        /// <summary>
        /// 获取或设置页面片设计器中文本的颜色
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器中被选择模块框的颜色
        /// </summary>
        public Color SelectedPenColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器中容器模块框的颜色
        /// </summary>
        public Color BoxPenColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器中模块框的颜色
        /// </summary>
        public Color ItemPenColor { get; set; }

        /// <summary>
        /// 获取对应的页面片设计器
        /// </summary>
        public SnipPageDesigner Designer
        {
            get { return PartContainer.Designer; }
        }

        /// <summary>
        /// 获取或设置页面片设计器静态模块的颜色
        /// </summary>
        public Color StaticPartColor { get; set; }

        public Color StaticPartForeColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器导航（频道/栏目）模块的颜色
        /// </summary>
        public Color NavagationPartColor { get; set; }

        public Color NavagationPartForeColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器定制特性模块的颜色
        /// </summary>
        public Color AttributePartColor { get; set; }

        public Color AttributePartForeColor { get; set; }

        /// <summary>
        /// 获取或设置页面片设计器Path模块的颜色
        /// </summary>
        public Color PathPartColor { get; set; }

        public Color PathPartForeColor { get; set; }

        /// <summary>
        /// PartContainer
        /// </summary>
        public IPartContainer PartContainer { get; private set; }

        #endregion

        #region 构造函数

        public ClassPartContainer(IPartContainer parentContainer)
        {
            
            ResourcesReader.SetObjectResourcesHelper(this);
            
            PartContainer = parentContainer;
            DisplaySpace = SoftwareOption.SnipDesigner.DisplaySpace;
            BoxFillColor = SoftwareOption.SnipDesigner.BoxFillColor;
            TextColor = SoftwareOption.SnipDesigner.TextColor;
            SelectedPenColor = SoftwareOption.SnipDesigner.SelectedPenColor;
            ItemPenColor = SoftwareOption.SnipDesigner.ItemPenColor;
            BoxPenColor = SoftwareOption.SnipDesigner.BoxPenColor;
            StaticPartColor = SoftwareOption.SnipDesigner.StaticPartColor;
            NavagationPartColor = SoftwareOption.SnipDesigner.NavagationPartColor;
            AttributePartColor = SoftwareOption.SnipDesigner.AttributePartColor;
            PathPartColor = SoftwareOption.SnipDesigner.PathPartColor;

            PathPartForeColor = SoftwareOption.SnipDesigner.PathPartForeColor;
            AttributePartForeColor = SoftwareOption.SnipDesigner.AttributePartForeColor;
            NavagationPartForeColor = SoftwareOption.SnipDesigner.NavagationPartForeColor;
            StaticPartForeColor = SoftwareOption.SnipDesigner.StaticPartForeColor;
            BoxForeColor = SoftwareOption.SnipDesigner.BoxForeColor;
            PieRadii = SoftwareOption.SnipDesigner.PieRadii; //需要添加到选项里
            UsingLinearGradient = SoftwareOption.SnipDesigner.UsingLinearGradient;

        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 绘制包含的子 part 
        /// </summary>
        /// <param name="g"></param>
        public void PaintChildParts(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿
            Pen selectedPen = new Pen(SelectedPenColor);
            List<SnipPagePart> selectedList = new List<SnipPagePart>();
            Brush textBrush = new SolidBrush(BoxForeColor);
            Color currentColor = StaticPartColor;
            Pen currentPen = new Pen(currentColor);
            foreach (SnipPagePart part in PartContainer.ChildParts)
            {
                if (part.Selected)
                {
                    selectedList.Add(part);
                }

                ///将在此矩形上画SnipPagePart
                //Point rectLocation = new Point(part.LocationForDesigner.X + SnipPageDesigner.PartSeparator,
                //    part.LocationForDesigner.Y + SnipPageDesigner.PartSeparator);
                Rectangle rectForDraw = new Rectangle(part.LocationForDesigner, new Size(part.Bounds.Width-1, part.Bounds.Height ));
                Point floatImagePoint = new Point(rectForDraw.X + 2, rectForDraw.Y + 2);

                #region 根据类型生成颜色并画出来

                if (!part.IsBox)
                {                   
                    switch (part.PartType)
                    {                        
                        case SnipPartType.Static:
                            currentColor = StaticPartColor;
                            textBrush = new SolidBrush(StaticPartForeColor);
                            break;
                        case SnipPartType.Navigation:
                            currentColor = NavagationPartColor;
                            textBrush = new SolidBrush(NavagationPartForeColor);
                            break;
                        case SnipPartType.Attribute:
                            currentColor = AttributePartColor;
                            textBrush = new SolidBrush(AttributePartForeColor);
                            break;
                        case SnipPartType.Path:
                            currentColor = PathPartColor;
                            textBrush = new SolidBrush(PathPartForeColor);
                            break;
                        default:
                            break;                        
                    }
                    currentPen = new Pen(ItemPenColor);
                }
                else
                {
                    currentColor = BoxFillColor;
                    currentPen = new Pen(BoxPenColor);
                }

                FillPart(g, currentColor, rectForDraw,part.UsedMinWidth);
                DrawPart(g, currentPen, rectForDraw,part.UsedMinWidth);

                #endregion

                ///画文字标志
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Near;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                string _displayText = part.PartType == SnipPartType.Attribute ? part.Text : part.Title;
                g.DrawString(_displayText, Designer.Font, textBrush, rectForDraw.X + 2, rectForDraw.Y + rectForDraw.Height - Designer.Font.Height - 2, sf);
               
                ///画浮动标识
                switch (part.Float_Css)
                {
                    case "left":
                        g.DrawImage(_floatLeft, floatImagePoint);
                        break;
                    case "right":
                        g.DrawImage(_floatRight, floatImagePoint);
                        break;
                    default:
                        break;
                }

                ///递归画其下的ChildParts
                if (part.HasChild)
                {
                    part.PaintChildParts(g);
                }
            }

            List<Point> _boxIconLocations = new List<Point>();
            ///画选中项的边框
            foreach (SnipPagePart part in selectedList)
            {
                Rectangle rectForDraw = new Rectangle(part.LocationForDesigner, new Size(part.Bounds.Width - 1, part.Bounds.Height - 1));

                DrawPart(g, selectedPen, rectForDraw,part.UsedMinWidth);

                if (part.IsBox)
                {
                    g.DrawImageUnscaled(_moveImg, new Point(rectForDraw.X + part.BoxIconXOffset, rectForDraw.Y + part.BoxIconYOffset));
                }
            }
        }

        /// <summary>
        /// 布局其下的ChildParts
        /// </summary>
        public void LayoutParts()
        {
            if (Designer._isUpdateData)
            {
                return;
            }

            int currentLine = 0;
            int innerLines = 0;
            Dictionary<int, LineData> dicLinesData = new Dictionary<int, LineData>();

            for (int i = 0; i < PartContainer.ChildParts.Count; i++)
            {
                SnipPagePart part = PartContainer.ChildParts[i];

                ///若此Part有子Part，则先对其内部进行布局
                if (part.HasChild)
                {
                    part.LayoutParts();
                }

                int leftOffset = PartContainer.Padding.Left;   //此变量是将要出现的控件的水平起始位置
                int rightOffset = PartContainer.Padding.Right;   //此变量是将要出现的控件的水平结束位置

                ///取出当前行位置对应的LineData
                LineData lineData = null;
                if (!dicLinesData.TryGetValue(currentLine, out lineData))
                {
                    lineData = new LineData(currentLine, i);
                    dicLinesData.Add(currentLine, lineData);
                }
                else
                {
                    ///遍历来检测当前插入行的左右边界
                    foreach (SnipPagePart r in lineData.LineParts)
                    {
                        if (r.Float_Css == "left")
                        {
                            leftOffset = r.Bounds.X + r.Bounds.Width;
                        }
                        else if (r.Float_Css == "right")
                        {
                            rightOffset = PartContainer.FactWidth - r.Bounds.X;
                        }
                    }
                }

                ///剩余的宽度
                int spare = PartContainer.FactWidth - leftOffset - rightOffset;

                ///若剩余宽度太小，则另起一行
                if (spare != (PartContainer.FactWidth - PartContainer.Padding.Left - PartContainer.Padding.Right) && spare < 5)
                {
                    currentLine++;
                    i--;
                    continue;
                }

                int height = SnipPageDesigner.PartHeight * part.FactLines;// +(SnipPageDesigner.PartSeparator * 2);
                int width = CssLongnessToPixel(part.Width_Css, part, lineData);
                part.WidthPixel = width;
                ///若剩余宽度不够，则另起一行
                if (part.Float_Css != "none" && spare != (PartContainer.FactWidth - PartContainer.Padding.Left - PartContainer.Padding.Right) && spare < part.WidthPixel)
                {
                    currentLine++;
                    i--;
                    continue;
                }
                int x = (part.Float_Css != "right") ? leftOffset : PartContainer.FactWidth - (rightOffset + width);
                int y = currentLine * SnipPageDesigner.PartHeight + PartContainer.Padding.Top;

                part.Bounds = new Rectangle(x, y, width, height);
                int line = AddToDicLayout(dicLinesData, part, currentLine);

                innerLines = Math.Max(innerLines, currentLine + part.FactLines);
                if (part.Float_Css == "none")
                {
                    ///由于不是浮动行，则下一个Part要重新起一行：currentLine递增
                    currentLine += line;
                }
            }

            PartContainer.ResetInnerLines(innerLines);

            if (PartContainer is SnipPageDesigner)
            {
                var designer = ((SnipPageDesigner)PartContainer);
                designer.Height = PartContainer.InnerLines * SnipPageDesigner.PartHeight + ((SnipPageDesigner)PartContainer).Padding.Vertical;

                int cx = 0, cy = 0;
                cx = (designer.Parent.Width - designer.Width) / 2;
                cy = (designer.Parent.Height - designer.Height) / 2;
                designer.Location = new Point(cx, cy);
            }
            else if (PartContainer is SnipPagePart)
            {
                ((SnipPagePart)PartContainer).Invalidate();
            }

            PartContainer.Designer.OnPartsLayouted(EventArgs.Empty);
        }

        public SnipPagePart GetPartAt(Point point, bool isRecursiveChilds)
        {
            //modify: zhenghao at 2008-06-23 10:00
            //先找所有被选择的
            foreach (SnipPagePart part in PartContainer.ChildParts)
            {
                if (!part.Selected)
                {
                    continue;
                }
                if (part.HitTest(point))
                {
                    if (isRecursiveChilds && part.HasChild)
                    {
                        SnipPagePart temp = part.GetPartAt(point, true);
                        if (temp != null)
                        {
                            return temp;
                        }
                    }
                    return part;
                }
            }
            foreach (SnipPagePart part2 in PartContainer.ChildParts)
            {
                if (part2.Selected)
                {
                    continue;
                }
                if (part2.HitTest(point))
                {
                    if (isRecursiveChilds && part2.HasChild)
                    {
                        SnipPagePart temp = part2.GetPartAt(point, true);
                        if (temp != null)
                        {
                            return temp;
                        }
                    }
                    return part2;
                }
            }
            return null;
        }

        /// <summary>
        /// add by fenggy 2008-06-13 根据ID得到SnipPagePart
        /// </summary>
        /// <param name="PartID"></param>
        /// <returns></returns>
        public SnipPagePart GetPartByID(string PartID)
        {
            return GetPartFromPartCollection(PartID, PartContainer.ChildParts);
        }

        /// <summary>
        /// add by fenggy 2008-06-16 根据ID从PartCollection中寻找对应的PART
        /// </summary>
        /// <param name="PartID"></param>
        /// <param name="ChildParts"></param>
        /// <returns></returns>
        private SnipPagePart GetPartFromPartCollection(string PartID, SnipPagePartCollection ChildParts)
        {
            foreach (SnipPagePart part in ChildParts)
            {
                SnipPagePart retPart = null;                
                if (part.PartID.Equals(PartID))
                {
                    retPart = part;
                }
                if (retPart != null)
                {
                    return retPart;
                }
                if (part.ChildParts.Count > 0)
                {
                    retPart = GetPartFromPartCollection(PartID, part.ChildParts);
                    if (retPart != null)
                    {
                        return retPart;
                    }
                }
            }
            return null;
        }

        public SnipPagePart[] GetPartsAt(Point point)
        {
            List<SnipPagePart> parts = new List<SnipPagePart>();

            SnipPagePart part = PartContainer.GetPartAt(point, false);
            bool isHit = true;
            while (part != null && isHit)
            {
                parts.Add(part);
                isHit = false;
                if (part.HasChild)
                {
                    foreach (SnipPagePart p in part.ChildParts)
                    {
                        if (p.HitTest(point))
                        {
                            part = p;
                            isHit = true;
                            break;
                        }
                    }
                }
            }
            return parts.ToArray();
        }

        public SnipPagePart[] GetPartsAt(Rectangle rect)
        {
            return GetPartsAtEx(PartContainer, rect);
        }

        /// <summary>
        /// 当前part的实际像素宽度
        /// </summary>
        /// <param name="cssLongness"></param>
        /// <returns></returns>
        public int CssLongnessToPixel(string cssLongness)
        {
            return CssLongnessToPixel(cssLongness, null, null);
        }

        /// <summary>
        /// 当前part的实际像素宽度
        /// </summary>
        /// <param name="cssLongness"></param>
        /// <returns></returns>
        public int CssLongnessToPixel(string cssLongness, SnipPagePart part, LineData lineData)
        {
            if (string.IsNullOrEmpty(cssLongness))
            {
                return 0;
            }

            ///处理px
            if (cssLongness.EndsWith("px", StringComparison.InvariantCultureIgnoreCase))
            {
                return int.Parse(cssLongness.Substring(0, cssLongness.Length - 2));
            }
            ///处理%
            else if (cssLongness.EndsWith("%", StringComparison.InvariantCultureIgnoreCase))
            {
                int width = 0;
                if (part != null)
                {
                    width = part.ParentContainer.WidthPixel;
                    if (part.Float_Css == "none")
                    {
                        foreach (SnipPagePart p in lineData.LineParts)
                        {
                            width -= p.WidthPixel;
                        }
                    }
                }
                else
                {
                    width = PartContainer.ParentContainer.WidthPixel;
                }
                return width * int.Parse(cssLongness.Substring(0, cssLongness.Length - 1)) / 100;
            }
            ///处理其他，默认是纯数字，当像素处理
            else
            {
                return int.Parse(cssLongness);
            }
        }

        #endregion

        #region 内部方法
       
        /// <summary>
        /// 填充圆角矩形（渐变）
        /// </summary>
        private void FillPart(Graphics g, Color color1, Rectangle rect, bool usedMinWidth)
        {
            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int h = rect.Height;

            int arcW = (int)(PieRadii * 2);
            Brush brush;

            /////// edit by zhenghao at 2008-06-30 14:55 
            if (usedMinWidth)
            {
                brush = new SolidBrush(Color.Yellow);
                Rectangle rectTemp = new Rectangle(x, y, PieRadii, h);
                g.FillRectangle(brush, rectTemp);
                return;
            }
            ///////
            
            if (UsingLinearGradient)
                brush = new LinearGradientBrush(new Point(x, y), new Point(x + w, y + h), color1, Color.White);
            else
                brush = new SolidBrush(color1);            

            Rectangle r1 = new Rectangle(x, y, arcW, arcW);
            Rectangle r2 = new Rectangle(x + w - arcW, y, arcW, arcW);
            Rectangle r3 = new Rectangle(x, y + h - arcW, arcW, arcW);
            Rectangle r4 = new Rectangle(x + w - arcW, y + h - arcW, arcW, arcW);

            Rectangle r5 = new Rectangle(x + PieRadii, y, w - arcW, PieRadii);
            Rectangle r6 = new Rectangle(x, y + PieRadii - 1, w, h - arcW + 1);
            Rectangle r7 = new Rectangle(x + PieRadii, y + h - PieRadii - 1, w - arcW, PieRadii);

            g.FillPie(brush, r1, 180, 90);
            g.FillPie(brush, r2, 270, 90);
            g.FillPie(brush, r3, 90, 90);
            g.FillPie(brush, r4, 0, 90);

            g.FillRectangles(brush, new Rectangle[] { r5, r6, r7 });
        }


        /// <summary>
        /// 画圆角矩形的边框
        /// </summary>
        private void DrawPart(Graphics g, Pen pen , Rectangle rect ,bool usedMinWidth)
        {
            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int h = rect.Height;

            int arcW = PieRadii * 2;

            /////// edit by zhenghao at 2008-06-30 14:55 
            if (usedMinWidth)
            {
                Rectangle rectTemp = new Rectangle(x, y, PieRadii, h);
                g.DrawRectangle(pen, rectTemp);
                return;
            }
            ///////

            RectangleF r1 = new RectangleF(x, y, arcW, arcW);
            RectangleF r2 = new RectangleF(x + w - arcW - pen.Width, y, arcW, arcW);
            RectangleF r3 = new RectangleF(x, y + h - arcW - pen.Width, arcW, arcW);
            RectangleF r4 = new RectangleF(x + w - arcW - pen.Width, y + h - arcW - pen.Width, arcW, arcW);

            g.DrawArc(pen, r1, 180, 90);
            g.DrawArc(pen, r2, 270, 90);
            g.DrawArc(pen, r3, 90, 90);
            g.DrawArc(pen, r4, 0, 90);

            g.DrawLine(pen, new Point(x + PieRadii, y), new Point((int)(x + w - PieRadii-pen.Width),y));

            g.DrawLine(pen, new Point(x + PieRadii, (int)(y + h - pen.Width)), 
                new Point((int)(x + w - PieRadii - pen.Width), (int)(y + h - pen.Width)));

            g.DrawLine(pen, new Point(x , y + PieRadii), new Point(x ,(int)(y + h - PieRadii-pen.Width)));

            g.DrawLine(pen, new Point((int)(x + w - pen.Width), y + PieRadii),
                new Point((int)(x + w - pen.Width), (int)(y + h - PieRadii - pen.Width)));

        }

        private SnipPagePart[] GetPartsAtEx(IPartContainer container, Rectangle rect)
        {
            List<SnipPagePart> list = new List<SnipPagePart>();
            //bool hasHited = false;
            foreach (SnipPagePart part in container.ChildParts)
            {
                if (part.HitTest(rect))
                {
                    //hasHited = true;
                    list.Add(part);

                    ///递归检测其下的子Part
                    if (part.HasChild)
                    {
                        SnipPagePart[] arrParts = GetPartsAtEx(part, rect);
                        list.AddRange(arrParts);
                    }
                }
                //else if (hasHited)   //若是跳出，则可能出现不连续的选择不成功
                //{
                //    break;
                //}
            }

            return list.ToArray();
        }

        private int AddToDicLayout(Dictionary<int, LineData> dicLinesData, SnipPagePart part, int startLine)
        {
            int i = 0;
            int multi = part.FactLines;
            for (; i < multi; i++)
            {
                LineData lineData;
                if (!dicLinesData.TryGetValue(startLine + i, out lineData))
                {
                    lineData = new LineData(i, part.Index);
                    dicLinesData.Add(startLine + i, lineData);
                }
                lineData.LineParts.Add(part);
            }
            return i;
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
