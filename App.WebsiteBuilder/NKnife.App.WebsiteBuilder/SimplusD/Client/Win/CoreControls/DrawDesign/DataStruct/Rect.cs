using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 绘画矩形
    /// </summary>
    public class Rect : INavigateNode
    {
        #region 字段成员定义

        private int _x;
        private int _y;
        private int _width;
        private int _height;

        private CssSection _cssSection = new CssSection();

        private PageSnipType _snipType;

        #endregion

        #region 属性定义

        /// <summary>
        /// 获取或设置包含的数据
        /// </summary>
        public SnipData SnipData { get; set; }

        /// <summary>
        /// 获取或设置页面片类型
        /// </summary>
        [PropertyPad(0, 0, "snipType", MainControlType = MainControlType.TextBox, IsReadOnly = true)]
        public PageSnipType SnipType
        {
            get
            {
                if (HasSnip)
                    return _snipType;
                else
                    return PageSnipType.None;
            }
            set { _snipType = value; }
        }

        /// <summary>
        /// 获取或设置页面片ID
        /// </summary>
        //[PropertyPad(0, 1, "snipID", MainControlType = MainControlType.TextBox)]
        public string SnipID { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 左上角横坐标
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                _cssSection.Properties["left"] = value.ToString() + "px";
                _x = value;
            }
        }

        /// <summary>
        /// 左上角纵坐标
        /// </summary>
        public int Y
        {
            get { return _y; }
            set
            {
                _cssSection.Properties["top"] = value.ToString() + "px";
                _y = value;
            }
        }

        /// <summary>
        /// 矩形宽度
        /// </summary>
        [PropertyPad(1, 0, "rectWidth", MainControlType = MainControlType.TextBox,IsReadOnly=true)]
        public int Width
        {
            get { return _width; }
            set
            {
                _cssSection.Properties["width"] = value.ToString() + "px";
                _width = value;
            }
        }

        /// <summary>
        /// 矩形高度
        /// </summary>
        [PropertyPad(1, 1, "rectHeight", MainControlType = MainControlType.TextBox,IsReadOnly=true)]
        public int Height
        {
            get { return _height; }
            set
            {
                _cssSection.Properties["height"] = value.ToString() + "px";
                _height = value;
            }
        }

        /// <summary>
        /// 是否处于删除状态
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否处于选择状态
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 是否已锁定
        /// </summary>
        //[PropertyPad(0, 1, "%IsLocked", MainControlType = MainControlType.SimpleCheckBox)]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取矩形（rect）中是否含有页面片
        /// </summary>
        //[PropertyPad(0, 1, "%HasSnip", MainControlType = MainControlType.SimpleCheckBox)]
        public bool HasSnip { get; set; }

        /// <summary>
        /// 获取或设置矩形的Css字符串
        /// </summary>
        [PropertyPad(1, 2, "rectCss", MainControlType = MainControlType.TextBox)]
        public string ToCss
        {
            get { return _cssSection.ToString(); }
            set
            {
                _cssSection = CssSection.Parse(value);
            }
        }

        #endregion

        #region 构造函数

        public Rect(int x, int y, int width, int height)
        {
            Title = "";
            IsDeleted = false;
            IsSelected = false;
            IsLocked = false;
            HasSnip = false;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _cssSection.Properties["float"] = "none";
            _cssSection.Properties["clear"] = "left";
            _cssSection.Properties["overflow"] = "auto";
        }

        public Rect(Rectangle rect)
            : this(rect.X, rect.Y, rect.Width, rect.Height)
        { }

        #endregion

        #region 公共成员函数接口

        /// <summary>
        /// 判断是否可以被选择
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsSelectable(Point point)
        {
            return point.X > X &&
                point.X < X + Width &&
                point.Y > Y &&
                point.Y < Y + Height;
        }

        /// <summary>
        /// 判断是否可以在精度范围内被选择
        /// </summary>
        /// <param name="point"></param>
        /// <param name="precision">精度</param>
        /// <returns></returns>
        public bool IsSelectable(Point point, int precision)
        {
            return point.X > X + precision &&
                 point.X < X + Width - precision &&
                 point.Y > Y + precision &&
                 point.Y < Y + Height - precision;
        }

        /// <summary>
        /// 根据直线,得到矩形之边界.
        /// 默认line过了this之某边界
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public PartitionLine GetBorderline(PartitionLine line)
        {
            if (line.IsRow)
            {
                if (line.Position == this.Y)
                {
                    return new PartitionLine(X, X + Width, Y, true);
                }
                else if (line.Position == Y + Height)
                {
                    return new PartitionLine(X, X + Width, Y + Height, true);
                }
                else return null;
            }
            else
            {
                if (line.Position == this.X)
                {
                    return new PartitionLine(Y, Y + Height, X, false);
                }
                else if (line.Position == X + Width)
                {
                    return new PartitionLine(Y, Y + Height, X + Width, false);
                }
                else return null;
            }
        }

        /// <summary>
        /// 得到矩形边界
        /// </summary>
        /// <returns></returns>
        public List<PartitionLine> GetBorderlines()
        {
            List<PartitionLine> borderLines = new List<PartitionLine>();
            borderLines.Add(new PartitionLine(X, X + Width, Y, true));
            borderLines.Add(new PartitionLine(X, X + Width, Y + Height, true));
            borderLines.Add(new PartitionLine(Y, Y + Height, X, false));
            borderLines.Add(new PartitionLine(Y, Y + Height, X + Width, false));
            return borderLines;
        }

        /// <summary>
        /// 合并两个矩形,将neighbourRect合并到this中.
        /// </summary>
        /// <param name="neighbourRect">待合并的相邻矩形</param>
        /// <returns></returns>
        public void MergeTwoRect(Rect neighbourRect)
        {
            neighbourRect.IsDeleted = true;
            if (this.X == neighbourRect.X)
            {
                ///neighbourRect位于this的上方
                if (this.Y == neighbourRect.Y + neighbourRect.Height)
                {
                    this.Y = neighbourRect.Y;
                    this.Height += neighbourRect.Height;
                }
                ///下方
                else
                {
                    this.Height += neighbourRect.Height;
                }
            }
            else
            {
                ///左方
                if (this.X == neighbourRect.X + neighbourRect.Width)
                {
                    this.X = neighbourRect.X;
                    this.Width += neighbourRect.Width;
                }
                ///右方
                else
                {
                    this.Width += neighbourRect.Width;
                }
            }
        }

        /// <summary>
        /// 将this分割成两个矩形
        /// </summary>
        /// <param name="childRect">内含在this中,待分离之矩形</param>
        public void PartTwoRect(Rect childRect)
        {
            ///恢复被删除之矩形
            childRect.IsDeleted = false;
            ///修改被保留但合并了其他矩形的矩形
            if (this.Width == childRect.Width)
            {
                ///被删除矩形有其上方
                if (this.Y == childRect.Y)
                {
                    this.Y += childRect.Height;
                    this.Height -= childRect.Height;
                }
                else///下文
                {
                    this.Height -= childRect.Height;
                }
            }
            else
            {
                ///左方
                if (this.X == childRect.X)
                {
                    this.X += childRect.Width;
                    this.Width -= childRect.Width;
                }
                ///右方
                else
                {
                    this.Width -= childRect.Width;
                }
            }
        }

        public void MergeMutiRects(MergeRectCommand command)
        {
            foreach (Rect rect in command.RemovedRects)
            {
                rect.IsDeleted = true;
            }
            this.X = command.BoundaryRect.X;
            this.Y = command.BoundaryRect.Y;
            this.Width = command.BoundaryRect.Width;
            this.Height = command.BoundaryRect.Height;
        }

        public void PartMutiRects(MergeRectCommand command)
        {
            foreach (Rect rect in command.RemovedRects)
            {
                rect.IsDeleted = false;
            }

            this.X = command.LeftRect.X;
            this.Y = command.LeftRect.Y;
            this.Width = command.LeftRect.Width;
            this.Height = command.LeftRect.Height;
        }

        public void SelectRect()
        {
            this.IsSelected = true;
        }

        public void UnSelectRect()
        {
            this.IsSelected = false;
        }


        public void LockRect()
        {
            this.IsLocked = true;
        }

        public void UnLockRect()
        {
            this.IsLocked = false;
        }

        #endregion

        #region 虚拟函数

        virtual public void CreateRectData()
        {
        }

        #endregion


        #region INavigateNode 成员


        public string Message
        {
            get { return SnipID; }
        }

        #endregion
    }
}