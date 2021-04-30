using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Win.Forms.Frames;
using NKnife.Win.Forms.Common;
using NKnife.Win.Forms.EventParams;
using NKnife.Win.Forms.Frames.Base;
using NKnife.Win.Forms.Properties;

namespace NKnife.Win.Forms.Frames
{
    /// <summary>
    /// 镶嵌在画框中的设计面板，该面板一般用来载入一幅图片，而后以该图片为背景进行一些设计工作。
    /// lukan@xknife.net 2014-8-26
    /// </summary>
    internal sealed class DrawingBoard : Control
    {
        #region 类成员变量，当设计时鼠标拖动时的一些需计算的坐标点

        /// <summary>
        ///     定义移动鼠标时的坐标点
        /// </summary>
        private Point _CurrentDesign;

        /// <summary>
        ///     定义松开鼠标时的坐标点
        /// </summary>
        private Point _EndDesign;

        /// <summary>
        ///     定义鼠标按下时的坐标点
        /// </summary>
        private Point _StartDesign;

        /// <summary>
        ///     定义当拖动状态时，鼠标按下时的坐标点
        /// </summary>
        private Point _StartDragging;

        #endregion

        #region 类成员变量，原始图，当前图

        private Bitmap _CurrentImage;
        private Bitmap _SourceImage;

        #endregion

        #region 类成员变量：工作状态，父控件

        private DrawingBoardDesignMode _ImagePanelDesignMode = DrawingBoardDesignMode.Selecting;

        /// <summary>
        ///     当前是否在设计期间。主要是指是否正在用鼠标进行工作。
        /// </summary>
        private bool _IsDesign;

        /// <summary>
        /// 父控件。一般来讲仅使用画框做为本类型的承载。
        /// </summary>
        private PictureFrame _Parent;

        #endregion

        #region 构造函数

        public DrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            BackgroundImageChanged += ImageDesignPanel_BackgroundImageChanged;
            ParentChanged += ImageDesignPanel_ParentChanged;
            BackgroundImageLayout = ImageLayout.Zoom;
            ImagePanelDesignMode = DrawingBoardDesignMode.Selecting;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     图板当前的工作模式
        /// </summary>
        public DrawingBoardDesignMode ImagePanelDesignMode
        {
            get { return _ImagePanelDesignMode; }
            set
            {
                _ImagePanelDesignMode = value;
                switch (value)
                {
                    case DrawingBoardDesignMode.Selecting:
                        Cursor = Cursors.Arrow;
                        break;
                    case DrawingBoardDesignMode.Designing:
                        Cursor = Cursors.Cross;
                        break;
                    case DrawingBoardDesignMode.Dragging:
                        Cursor = Cursors.Hand;
                        break;
                    case DrawingBoardDesignMode.Zooming:
                        SetCursor(true, new Point(0,0));
                        break;
                }
            }
        }

        public void SetZoomMode(bool isKeyDown)
        {
            SetCursor(!isKeyDown, new Point(0, 0));
        }

        //系统没有通常缩放的放大镜图标，该函数从资源载入一个PNG图片做为图标
        private void SetCursor(bool isPlus, Point hotPoint)
        {
            Bitmap src = isPlus ? OwnResources.cursor_zoom_plus : OwnResources.cursor_zoom_minus;

            var cursorBitmap = new Bitmap(src.Width * 2 - hotPoint.X, src.Height * 2 - hotPoint.Y);
            Graphics g = Graphics.FromImage(cursorBitmap);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(src, src.Width - hotPoint.X, src.Height - hotPoint.Y, src.Width, src.Height);

            Cursor = new Cursor(cursorBitmap.GetHicon());

            g.Dispose();
            cursorBitmap.Dispose();
        }

        public Image Image
        {
            get { return _SourceImage; }
            set
            {
                Bitmap old = _SourceImage;
                _SourceImage = new Bitmap(value);
                _CurrentImage = new Bitmap(value);
                BackgroundImage = value;
                _Parent.OnImageLoaded(new ImageLoadEventArgs(old, value));
            }
        }

        #endregion

        #region 当Size发生变化时

        /// <summary>
        ///     图像展示的大小，初始默认是父容器的90%，以保证第一次尽可能大的展示整个图片
        /// </summary>
        private double _Multiple = 0.9;

        private void ImageDesignPanel_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.SizeChanged += delegate { SetOwnSize(_Multiple, Point.Empty); };
                _Parent = (PictureFrame) Parent;
            }
        }

        private void ImageDesignPanel_BackgroundImageChanged(object sender, EventArgs e)
        {
            SetOwnSize(_Multiple, Point.Empty);
        }

        private void SetOwnSize(double multiple, Point mousePoint)
        {
            if (Parent == null) //
                return;
            double oldZoom = _Parent.Zoom;
            _Multiple = multiple;

            if (BackgroundImage == null)
                return;

            int w = BackgroundImage.Width;
            int h = BackgroundImage.Height;
            int pw = Parent.Size.Width;
            int ph = Parent.Size.Height;

            if (w > h)
            {
                double z = (pw*multiple)/w;
                Size = new Size((int) (pw*multiple), (int) (h*z));
                //当控件尺寸更新完毕后，再更新父容器，以触发事件
                _Parent.Zoom = z;
            }
            else
            {
                double z = (ph*multiple)/h;
                Size = new Size((int) (w*z), (int) (ph*multiple));
                _Parent.Zoom = z;
            }
            if (mousePoint != Point.Empty)
            {
                //当缩放事件触发时，虽然尺寸已变化，但画板在画框中的位置还没有发生变化
                OnZoomed(new BoardZoomEventArgs(mousePoint, oldZoom));
            }
        }

        /// <summary>
        ///     当设计图板的大小发生变化后
        /// </summary>
        public event EventHandler<BoardZoomEventArgs> Zoomed;

        private void OnZoomed(BoardZoomEventArgs e)
        {
            EventHandler<BoardZoomEventArgs> handler = Zoomed;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     放大
        /// </summary>
        public void EnlargeDesignPanel(Point mousePoint)
        {
            var m = _Multiple + 0.3;
            SetOwnSize(m, mousePoint);
        }

        /// <summary>
        ///     缩小
        /// </summary>
        public void ShrinkDesignPanel(Point mousePoint)
        {
            var m = _Multiple - 0.3;
            SetOwnSize(m, mousePoint);
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            var border = new Pen(Color.Red, 1) {DashStyle = DashStyle.Dash};
            RectangleList rl = _Parent.Rectangles;
            if (rl.Count > 0)
            {
                //采用在内存中绘制好新图的方式贴图的方式进行绘制
                using (Graphics imgG = Graphics.FromImage(_CurrentImage))
                {
                    imgG.DrawImage(_SourceImage, new Point(0, 0));
                    foreach (RectangleF rect in rl)
                    {
                        if (rl.Current.Contains(rect)) //选中的矩形
                        {
                            var b = new SolidBrush(Color.FromArgb(80, 255, 255, 0));
                            imgG.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        else if (rect == rl.Hover) //鼠标滑过的矩形
                        {
                            var b = new SolidBrush(Color.FromArgb(40, 80, 180, 0));
                            imgG.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        else //普通矩形
                        {
                            var b = new SolidBrush(Color.FromArgb(40, 255, 0, 0));
                            imgG.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        imgG.DrawRectangle(Pens.Red, rect.X, rect.Y, rect.Width, rect.Height);
                    }
                    imgG.Dispose();
                }
                //将画好的新图贴到当前控件的工作区
                g.DrawImage(_CurrentImage, ClientRectangle, new Rectangle(0, 0, _CurrentImage.Width, _CurrentImage.Height), GraphicsUnit.Pixel);
            }

            //画正在拖动的矩形
            if (_IsDesign)
            {
                var rect = new Rectangle(_EndDesign, new Size(Math.Abs(_CurrentDesign.X - _StartDesign.X), Math.Abs(_CurrentDesign.Y - _StartDesign.Y)));
                g.DrawRectangle(border, rect);
            }
            //为了美观，绘制一个矩形外边框
            g.DrawLines(Pens.Black, new[]
            {
                new Point(0, 0), new Point(0, Height - 1), new Point(Width - 1, Height - 1), new Point(Width - 1, 0), new Point(0, 0)
            });
        }

        #endregion

        #region 响应:删除矩形,选择全部,粘贴已选择的矩形,矩形操作

        public void RemoveSelectedRectangle()
        {
            RectangleList rl = _Parent.Rectangles;
            if (rl.Current.Count > 0)
            {
                DialogResult ds = MessageBox.Show(this, "是否删除被选择的矩形设计区？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ds == DialogResult.Yes)
                {
                    foreach (RectangleF rect in rl.Current)
                    {
                        rl.Remove(rect);
                    }
                    rl.Current.Clear();
                    Invalidate();
                }
            }
        }

        public void PasteSelectedRectangle(Point screenPoint)
        {
            var rects = _Parent.Rectangles;
            if (rects.Count <= 0 || rects.Current.Count <= 0 ||
                rects.Current.SelectedMode == RectangleList.SelectedMode.None)
            {
                Debug.Fail("未做有效判断,即开始粘贴.");
                return;
            }
            PointF first = rects.Current[0].Location;
            if (screenPoint != Point.Empty)
            {
                var c = PointToClient(screenPoint);
                first = new PointF((float) (c.X*_Parent.Zoom), (float) (c.Y*_Parent.Zoom));
            }

            float offsetX = 15;
            float offsetY = 15;
            if (first != rects.Current[0].Location)
            {
                var lct = rects.Current[0].Location;
                offsetX = first.X - lct.X;
                offsetY = first.Y - lct.Y;
            }
            var newList = new List<RectangleF>(rects.Current.Count);
            foreach (var rect in rects.Current)
            {
                var newRect = new RectangleF(rect.X + offsetX, rect.Y + offsetY, rect.Width, rect.Height);
                newList.Add(newRect);

                switch (rects.Current.SelectedMode)
                {
                    case RectangleList.SelectedMode.Cut:
                        rects.Remove(rect);
                        break;
                }
            }
            rects.AddRange(newList);

            rects.Current.Clear();
            rects.Current.AddRange(newList);
            rects.Current.SelectedMode = RectangleList.SelectedMode.None;
            Invalidate();
        }

        public void SelectAllRectangles()
        {
            _Parent.Rectangles.Current.Clear();
            foreach (var rectangle in _Parent.Rectangles)
            {
                _Parent.Rectangles.Current.Add(rectangle);
            }
            Invalidate();
        }

        /// <summary>根据指定的操作模式对矩形进行操作
        /// </summary>
        /// <param name="ro">指定的操作模式</param>
        public void RectangleOperating(RectangleOperation ro)
        {
            switch (ro)
            {
                #region case
                case RectangleOperation.AlignBottom:
                    AlignBottom();
                    break;
                case RectangleOperation.AlignCenter:
                    AlignCenter();
                    break;
                case RectangleOperation.AlignHeight:
                    AlignHeight();
                    break;
                case RectangleOperation.AlignLeft:
                    AlignLeft();
                    break;
                case RectangleOperation.AlignMiddle:
                    AlignMiddle();
                    break;
                case RectangleOperation.AlignRight:
                    AlignRight();
                    break;
                case RectangleOperation.AlignSame:
                    AlignSame();
                    break;
                case RectangleOperation.AlignTop:
                    AlignTop();
                    break;
                case RectangleOperation.AlignWidth:
                    AlignWidth();
                    break;
                case RectangleOperation.ArrowDown:
                    ArrowDown();
                    break;
                case RectangleOperation.ArrowIn:
                    ArrowIn();
                    break;
                case RectangleOperation.ArrowLeft:
                    ArrowLeft();
                    break;
                case RectangleOperation.ArrowOut:
                    ArrowOut();
                    break;
                case RectangleOperation.ArrowRight:
                    ArrowRight();
                    break;
                case RectangleOperation.ArrowUp:
                    ArrowUp();
                    break;
                #endregion
            }
        }

        #region 矩形操作

        private void AlignBottom()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseY = current[0].Y + current[0].Height;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, baseY - rect.Height, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignCenter()//(纵向居中)
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点(纵向居中)
            var baseX = current[0].X + current[0].Width/2;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(baseX - rect.Width/2, rect.Y, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignHeight()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseH = current[0].Height;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y, rect.Width, baseH);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignLeft()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseX = current[0].X;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(baseX, rect.Y, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignMiddle()//(横向居中)
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点(横向居中)
            var baseY = current[0].Y + current[0].Height/2;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, baseY - rect.Height/2, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();
        }
        private void AlignRight()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseX = current[0].X + current[0].Width;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(baseX - rect.Width, rect.Y, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignSame()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseH = current[0].Height;
            var baseW = current[0].Width;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y, baseW, baseH);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void AlignTop()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseY = current[0].Y;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, baseY, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();
        }
        private void AlignWidth()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count <= 1)
                return;
            //基准点
            var baseW = current[0].Width;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y, baseW, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        /*--------------------------------*/
        private void ArrowDown()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y + 1, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void ArrowIn()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                RectangleF newRect;
                if (rect.Width - 1 > 0 && rect.Height - 1 > 0)
                    newRect = new RectangleF(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                else
                    newRect = rect;
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void ArrowLeft()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X-1, rect.Y, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void ArrowOut()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y, rect.Width+1, rect.Height+1);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }
        private void ArrowRight()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X+1, rect.Y, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//

        }
        private void ArrowUp()
        {
            var current = _Parent.Rectangles.Current;
            if (current.Count < 1)
                return;
            var clone = (RectangleList.Selected)current.Clone();
            current.Clear();
            foreach (var rect in clone)
            {
                var newRect = new RectangleF(rect.X, rect.Y-1, rect.Width, rect.Height);
                var index = _Parent.Rectangles.IndexOf(rect);
                _Parent.Rectangles[index] = newRect;
                current.Add(newRect);
            }
            Invalidate();//
        }

        #endregion

        #endregion

        #region 鼠标按下时发生的事件

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (ImagePanelDesignMode)
            {
                case DrawingBoardDesignMode.Designing:
                {
                    _StartDesign = new Point(e.X, e.Y);
                    _IsDesign = true;
                    break;
                }
                case DrawingBoardDesignMode.Selecting:
                {
                    RectangleList rl = _Parent.Rectangles;
                    foreach (RectangleF rect in rl)
                    {
                        var epoint = new Point((int) (e.X/_Parent.Zoom), (int) (e.Y/_Parent.Zoom));
                        if (rect.Contains(epoint))
                        {
                            if (!rl.Current.Contains(rect))
                            {
                                rl.Current.Add(rect);
                                _Parent.OnSelected(new RectangleSelectedEventArgs(rect, e));
                            }
                            else
                            {
                                if (e.Button == MouseButtons.Left)
                                    rl.Current.Remove(rect); //如果是已选的，置为未选择状态
                            }
                            Invalidate();
                            break;
                        }
                    }
                    break;
                }
                case DrawingBoardDesignMode.Dragging:
                {
                    _StartDragging = e.Location;
                    break;
                }
            }
        }

        #endregion

        #region 移动鼠标发生的事件

        public event EventHandler<BoardDraggingEventArgs> BoardDragging;

        private void OnBoardDragging(BoardDraggingEventArgs e)
        {
            EventHandler<BoardDraggingEventArgs> handler = BoardDragging;
            if (handler != null) 
                handler(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch (ImagePanelDesignMode)
            {
                case DrawingBoardDesignMode.Designing:
                {
                    if (!_IsDesign || e.Button != MouseButtons.Left)
                        return;
                    MouseMoveDesigning(e.X, e.Y);
                    break;
                }
                case DrawingBoardDesignMode.Selecting:
                {
                    MouseMoveSelecting(e);
                    break;
                }
                case  DrawingBoardDesignMode.Dragging:
                {
                    if (e.Button != MouseButtons.Left)
                        return;
                    OnBoardDragging(new BoardDraggingEventArgs(_StartDragging, e.Location));
                    break;
                }
            }
        }

        private void MouseMoveSelecting(MouseEventArgs e)
        {
            RectangleList rl = _Parent.Rectangles;
            foreach (RectangleF rect in rl)
            {
                var epoint = new Point((int) (e.X/_Parent.Zoom), (int) (e.Y/_Parent.Zoom));
                if (rect.Contains(epoint))
                {
                    if (!rl.Current.Contains(rect) && rect != rl.Hover)
                    {
                        rl.Hover = rect;
                        Invalidate();
                        _Parent.OnSelecting(new RectangleSelectingEventArgs(rect));
                        break;
                    }
                }
            }
        }

        private void MouseMoveDesigning(int x, int y)
        {
            _CurrentDesign = new Point(x, y);
            if ((_CurrentDesign.X - _StartDesign.X) > 0 && (_CurrentDesign.Y - _StartDesign.Y) > 0) //当鼠标从左上角向开始移动时
            {
                _EndDesign = new Point(_StartDesign.X, _StartDesign.Y);
            }
            if ((_CurrentDesign.X - _StartDesign.X) < 0 && (_CurrentDesign.Y - _StartDesign.Y) > 0) //当鼠标从右上角向左下方向开始移动
            {
                _EndDesign = new Point(_CurrentDesign.X, _StartDesign.Y);
            }
            if ((_CurrentDesign.X - _StartDesign.X) > 0 && (_CurrentDesign.Y - _StartDesign.Y) < 0) //当鼠标从左下角向上开始移动时
            {
                _EndDesign = new Point(_StartDesign.X, _CurrentDesign.Y);
            }
            if ((_CurrentDesign.X - _StartDesign.X) < 0 && (_CurrentDesign.Y - _StartDesign.Y) < 0) //当鼠标从右下角向左方向上开始移动时
            {
                _EndDesign = new Point(_CurrentDesign.X, _CurrentDesign.Y);
            }
            //使控件的整个图面无效,并导致重绘控件,激发OnPaint绘制Design的整个矩形
            Invalidate();
            _Parent.OnDesignDragging(new BoardDesignDragParamsEventArgs(_StartDesign, _CurrentDesign));
        }

        #endregion

        #region 松开鼠标发生的事件

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            switch (ImagePanelDesignMode)
            {
                case DrawingBoardDesignMode.Designing:
                {
                    if (e.Button != MouseButtons.Left)
                        return;
                    _IsDesign = false;
                    if (_EndDesign == Point.Empty) //未拖动
                        return;

                    var end = new PointF((float) (_EndDesign.X/_Parent.Zoom), (float) (_EndDesign.Y/_Parent.Zoom));
                    var start = new PointF((float) (_StartDesign.X/_Parent.Zoom), (float) (_StartDesign.Y/_Parent.Zoom));
                    var current = new PointF((float) (_CurrentDesign.X/_Parent.Zoom), (float) (_CurrentDesign.Y/_Parent.Zoom));

                    var rect = new RectangleF(end, new SizeF(Math.Abs(current.X - start.X), Math.Abs(current.Y - start.Y)));
                    _Parent.Rectangles.Add(rect);

                    Invalidate();
                    _EndDesign = Point.Empty;
                    var mode = RectangleListChangedEventArgs.RectangleChangedMode.Created;
                    _Parent.OnDesignDragged(new BoardDesignDragParamsEventArgs(_StartDesign, _CurrentDesign));
                    _Parent.OnRectangleCreated(new RectangleListChangedEventArgs(mode, rect));
                    break;
                }
                case DrawingBoardDesignMode.Selecting:
                {
                    break;
                }
            }
        }

        #endregion

        #region 鼠标单击双击事件

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left)
            {
                RectangleList rl = _Parent.Rectangles;
                foreach (RectangleF rect in rl)
                {
                    var epoint = new Point((int) (e.X/_Parent.Zoom), (int) (e.Y/_Parent.Zoom));
                    if (rect.Contains(epoint))
                    {
                        _Parent.OnRectangleDoubleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, rect));
                        break;
                    }
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.None)
            {
                if (ImagePanelDesignMode == DrawingBoardDesignMode.Zooming)
                {
                    if (e.Button == MouseButtons.Left && e.Clicks == 1) //左键单击
                    {
                        if ((ModifierKeys & Keys.Shift) == Keys.Shift) //是否按着Shift键
                            ShrinkDesignPanel(e.Location);
                        else
                            EnlargeDesignPanel(e.Location);
                    }
                }
                else
                {
                    RectangleList rl = _Parent.Rectangles;
                    foreach (RectangleF rect in rl)
                    {
                        var epoint = new Point((int) (e.X/_Parent.Zoom), (int) (e.Y/_Parent.Zoom));
                        if (rect.Contains(epoint))
                        {
                            _Parent.OnRectangleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, rect));
                            return;
                        }
                    }
                    _Parent.OnRectangleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, Rectangle.Empty));
                }
            }
        }

        #endregion

        //TODO:为何不能够响应滚轮呢？Bug出在何处？
        protected override void OnMouseWheel(MouseEventArgs e)
        {

            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Console.WriteLine(e.Delta);
            }
            else
            {
                Console.WriteLine(e.Delta);
            }
        }

    }
}