using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Draws.Controls.Frames.Base;
using NKnife.Draws.Controls.Frames.Event;
using NKnife.Ioc;

namespace NKnife.Draws.Controls.Frames
{
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

        private PictureFrame _Parent;

        #endregion

        #region 构造函数

        public DrawingBoard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,
                true);
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
                        Cursor = Cursors.Help;
                        break;
                }
            }
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
                Parent.SizeChanged += delegate { SetOwnSize(_Multiple); };
                _Parent = (PictureFrame) Parent;
            }
        }

        private void ImageDesignPanel_BackgroundImageChanged(object sender, EventArgs e)
        {
            SetOwnSize(_Multiple);
        }

        public void SetOwnSize(double multiple)
        {
            Size srcSize = Size;
            double srcMultiple = _Multiple;
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
            OnZoomed(new BoardZoomEventArgs(srcSize, PointToClient(MousePosition), srcMultiple, multiple));
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
        public void EnlargeDesignPanel()
        {
            SetOwnSize(_Multiple += 0.2);
        }

        /// <summary>
        ///     缩小
        /// </summary>
        public void ShrinkDesignPanel()
        {
            SetOwnSize(_Multiple -= 0.2);
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
                        if (rl.Selected.Contains(rect)) //选中的矩形
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

        #region 键盘响应

        public void RemoveSelectedRectangle()
        {
            var rl = DI.Get<RectangleList>();
            if (rl.Selected.Count > 0)
            {
                DialogResult ds = MessageBox.Show(this, "是否删除被选择的矩形设计区？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ds == DialogResult.Yes)
                {
                    foreach (RectangleF rect in rl.Selected)
                    {
                        rl.Remove(rect);
                    }
                    Invalidate();
                }
            }
        }

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
                            if (!rl.Selected.Contains(rect))
                            {
                                rl.Selected.Add(rect);
                                Invalidate();
                                _Parent.OnSelected(new RectangleSelectedEventArgs(rect, e));
                                break;
                            }
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
                    if (!rl.Selected.Contains(rect) && rect != rl.Hover)
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
                        _Parent.OnRectangleDoubleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, true, rect));
                        break;
                    }
                }
                _Parent.OnRectangleDoubleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, false, Rectangle.Empty));
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
                            ShrinkDesignPanel();
                        else
                            EnlargeDesignPanel();
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
                            _Parent.OnRectangleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, true, rect));
                            break;
                        }
                    }
                    _Parent.OnRectangleClick(new RectangleClickEventArgs(e, ImagePanelDesignMode, false, Rectangle.Empty));
                }
            }
        }

        #endregion
    }
}