using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Draws.Common;
using NKnife.Ioc;

namespace NKnife.Draws
{
    public sealed class ImageDesignPanel : Control
    {
        #region 类成员变量，当设计时鼠标拖动时的一些需计算的坐标点

        /// <summary>
        ///     定义移动鼠标时的坐标点
        /// </summary>
        private Point _Current;

        /// <summary>
        ///     定义松开鼠标时的坐标点
        /// </summary>
        private Point _End;

        /// <summary>
        ///     定义鼠标按下时的坐标点
        /// </summary>
        private Point _Start;

        #endregion

        #region 类成员变量，原始图，当前图

        private Bitmap _CurrentImage;
        private Bitmap _SourceImage;

        #endregion

        #region 类成员变量，工作状态

        private ImagePanelDesignMode _ImagePanelDesignMode;

        /// <summary>
        ///     当前是否在设计期间。主要是指是否正在用鼠标进行工作。
        /// </summary>
        private bool _IsDesign;

        #endregion

        #region 构造函数

        public ImageDesignPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            BackgroundImageChanged += ImageDesignPanel_BackgroundImageChanged;
            ParentChanged += ImageDesignPanel_ParentChanged;
            BackgroundImageLayout = ImageLayout.Zoom;
            Cursor = Cursors.Cross;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     当前图像的缩放率
        /// </summary>
        public double Zoom { get; set; }

        /// <summary>
        ///     图板当前的工作模式
        /// </summary>
        public ImagePanelDesignMode ImagePanelDesignMode
        {
            get { return _ImagePanelDesignMode; }
            set
            {
                _ImagePanelDesignMode = value;
                switch (value)
                {
                    case ImagePanelDesignMode.Selecting:
                        Cursor = Cursors.Hand;
                        break;
                    default:
                        Cursor = Cursors.Cross;
                        break;
                }
            }
        }

        public Image Image
        {
            get { return _SourceImage; }
            set
            {
                _SourceImage = new Bitmap(value);
                _CurrentImage = new Bitmap(value);
                BackgroundImage = value;
            }
        }

        #endregion

        #region Size发生变化时

        private Size _ParentSize;

        public Size ParentSize
        {
            get { return _ParentSize; }
            set
            {
                _ParentSize = value;
                if (BackgroundImage != null)
                {
                    SetOwnSize();
                }
            }
        }

        private void ImageDesignPanel_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.SizeChanged += delegate
                {
                    ParentSize = Parent.Size;
                    SetOwnSize();
                };
            }
        }

        private void ImageDesignPanel_BackgroundImageChanged(object sender, EventArgs e)
        {
            SetOwnSize();
        }

        private void SetOwnSize()
        {
            if (BackgroundImage == null || ParentSize == Size.Empty)
                return;
            int w = BackgroundImage.Width;
            int h = BackgroundImage.Height;
            int pw = ParentSize.Width;
            int ph = ParentSize.Height;
            if (w > h)
            {
                Zoom = (pw*0.9)/w;
                Width = (int) (pw*0.9);
                Height = (int) (h*Zoom);
            }
            else
            {
                Zoom = (ph*0.9)/h;
                Height = (int) (ph*0.9);
                Width = (int) (w*Zoom);
            }
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            var p = new Pen(Color.Red, 1) {DashStyle = DashStyle.Dash};
            var b = new SolidBrush(Color.FromArgb(40, 255, 0, 0));

            var rl = DI.Get<RectangleList>();
            if (rl.Count > 0)
            {
                using (Graphics imgG = Graphics.FromImage(_CurrentImage))
                {
                    imgG.DrawImage(_SourceImage, new Point(0, 0));
                    foreach (RectangleF rect in rl)
                    {
                        if (rect != rl.Actived)
                        {
                            imgG.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
                            imgG.DrawRectangle(Pens.Red, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        else
                        {
                            RectangleF active = rl.Actived;
                            var y = new SolidBrush(Color.FromArgb(80, 255, 255, 0));
                            imgG.FillRectangle(y, active.X, active.Y, active.Width, active.Height);
                            imgG.DrawRectangle(Pens.Red, active.X, active.Y, active.Width, active.Height);
                        }
                    }
                    imgG.Dispose();
                    g.DrawImage(_CurrentImage, ClientRectangle, new Rectangle(0, 0, _CurrentImage.Width, _CurrentImage.Height), GraphicsUnit.Pixel);
                }
            }

            if (_IsDesign)
            {
                var rect = new Rectangle(_End, new Size(Math.Abs(_Current.X - _Start.X), Math.Abs(_Current.Y - _Start.Y)));
                g.DrawRectangle(p, rect);
            }

            g.DrawLines(Pens.Black, new[]
            {
                new Point(0, 0), new Point(0, Height - 1), new Point(Width - 1, Height - 1), new Point(Width - 1, 0), new Point(0, 0)
            });
        }

        #endregion

        #region 键盘响应

        public void KeyEvent(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var rl = DI.Get<RectangleList>();
                if (rl.Actived != RectangleF.Empty)
                {
                    DialogResult ds = MessageBox.Show(this, "是否删除被选择的矩形设计区？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (ds == DialogResult.Yes)
                    {
                        if (rl.Remove(rl.Actived))
                        {
                            Invalidate();
                        }
                    }
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
                case ImagePanelDesignMode.Designing:
                    _Start = new Point(e.X, e.Y);
                    _IsDesign = true;
                    break;
                case ImagePanelDesignMode.Selecting:
                    break;
            }
        }

        #endregion

        #region 移动鼠标发生的事件

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch (ImagePanelDesignMode)
            {
                case ImagePanelDesignMode.Designing:
                {
                    if (!_IsDesign)
                        return;
                    _Current = new Point(e.X, e.Y);
                    if ((_Current.X - _Start.X) > 0 && (_Current.Y - _Start.Y) > 0) //当鼠标从左上角向开始移动时
                    {
                        _End = new Point(_Start.X, _Start.Y);
                    }
                    if ((_Current.X - _Start.X) < 0 && (_Current.Y - _Start.Y) > 0) //当鼠标从右上角向左下方向开始移动
                    {
                        _End = new Point(_Current.X, _Start.Y);
                    }
                    if ((_Current.X - _Start.X) > 0 && (_Current.Y - _Start.Y) < 0) //当鼠标从左下角向上开始移动时
                    {
                        _End = new Point(_Start.X, _Current.Y);
                    }
                    if ((_Current.X - _Start.X) < 0 && (_Current.Y - _Start.Y) < 0) //当鼠标从右下角向左方向上开始移动时
                    {
                        _End = new Point(_Current.X, _Current.Y);
                    }
                    //使控件的整个图面无效,并导致重绘控件,激发OnPaint绘制Design的矩形
                    Invalidate();
                    OnDesignDragging(new DragParamsEventArgs(_Start, _Current));
                    break;
                }
                case ImagePanelDesignMode.Selecting:
                {
                    var rl = DI.Get<RectangleList>();
                    foreach (RectangleF rect in rl)
                    {
                        var epoint = new Point((int) (e.X/Zoom), (int) (e.Y/Zoom));
                        if (rect.Contains(epoint))
                        {
                            if (rect != rl.Actived)
                            {
                                rl.Actived = rect;
                                Invalidate();
                                OnSelected(new RectangleSelectedEventArgs(rl.Actived));
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        #endregion

        #region 松开鼠标发生的事件

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            switch (ImagePanelDesignMode)
            {
                case ImagePanelDesignMode.Designing:
                {
                    _IsDesign = false;

                    var end = new PointF((float) (_End.X/Zoom), (float) (_End.Y/Zoom));
                    var start = new PointF((float) (_Start.X/Zoom), (float) (_Start.Y/Zoom));
                    var current = new PointF((float) (_Current.X/Zoom), (float) (_Current.Y/Zoom));

                    var rect = new RectangleF(end, new SizeF(Math.Abs(current.X - start.X), Math.Abs(current.Y - start.Y)));
                    DI.Get<RectangleList>().Add(rect);

                    Invalidate();
                    OnDesignDragged(new DragParamsEventArgs(_Start, _Current));
                    break;
                }
                case ImagePanelDesignMode.Selecting:
                {
                    break;
                }
            }
        }

        #endregion

        #region Event

        public event EventHandler<RectangleSelectedEventArgs> Selected;

        public event EventHandler<DragParamsEventArgs> DesignDragging;

        public event EventHandler<DragParamsEventArgs> DesignDragged;

        private void OnDesignDragged(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragged;
            if (handler != null)
                handler(this, e);
        }

        private void OnDesignDragging(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragging;
            if (handler != null)
                handler(this, e);
        }

        private void OnSelected(RectangleSelectedEventArgs e)
        {
            EventHandler<RectangleSelectedEventArgs> handler = Selected;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}