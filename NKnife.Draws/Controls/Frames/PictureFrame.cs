using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Draws.Controls.Frames.Base;
using NKnife.Draws.Controls.Frames.Event;
using NKnife.Events;

namespace NKnife.Draws.Controls.Frames
{
    public sealed partial class PictureFrame : Control, IPictureFrame
    {
        #region 成员变量

        private readonly DrawingBoard _DrawingBoard;
        private readonly HScrollBar _HScrollBar = new HScrollBar();
        private readonly VScrollBar _VScrollBar = new VScrollBar();

        #endregion

        #region 构造函数

        public PictureFrame()
        {
            Rectangles = new RectangleList();
            InitializeComponent();

            _HScrollBar.Dock = DockStyle.Bottom;
            _HScrollBar.Enabled = false;
            _HScrollBar.ValueChanged += _HScrollBar_ValueChanged;
            _VScrollBar.Dock = DockStyle.Right;
            _VScrollBar.Enabled = false;
            _VScrollBar.ValueChanged += _VScrollBar_ValueChanged;
            _DrawingBoard = new DrawingBoard {Visible = false};
            _DrawingBoard.BoardDragging += _DrawingBoard_PanelDragging;
            _DrawingBoard.Zoomed += _DrawingBoard_PanelZoomed;

            Controls.Add(_VScrollBar);
            Controls.Add(_HScrollBar);
            Controls.Add(_DrawingBoard);
        }

        void _DrawingBoard_PanelZoomed(object sender, BoardZoomEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _DrawingBoard_PanelDragging(object sender, BoardDraggingEventArgs e)
        {
            if (_DrawingBoard.Width <= Width && _DrawingBoard.Height <= Height)
                return;
            //根据拖动的起点和当前点之间的距离，触发滚动条的Value发生变化，再间接触发画板的移动
            var current = PointToClient(_DrawingBoard.PointToScreen(e.CurrentPoint));
            var start = PointToClient(_DrawingBoard.PointToScreen(e.StartPoint));

            var x = (current.X - start.X)/3;
            var y = (current.Y - start.Y)/3;

            var offsetX = _HScrollBar.Value - x;
            if (offsetX > _HScrollBar.Minimum && offsetX < _HScrollBar.Maximum)
                _HScrollBar.Value = offsetX;
            var offsetY = _VScrollBar.Value - y;
            if (offsetY > _VScrollBar.Minimum && offsetY < _VScrollBar.Maximum)
                _VScrollBar.Value = _VScrollBar.Value - y;
        }

        private void _VScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _DrawingBoard.Top = -_VScrollBar.Value;
        }

        private void _HScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _DrawingBoard.Left = -_HScrollBar.Value;
        }

        private void _DrawingBoard_SizeChanged(object sender, EventArgs e)
        {
            // TODO: SizeChanged好象不对，放大后应该用一个事件来触发，以控制滚动条的位置
            if (_DrawingBoard.Width > Width)
            {
                _HScrollBar.Enabled = true;
                _HScrollBar.Maximum = _DrawingBoard.Width - Width;
            }
            if (_DrawingBoard.Height > Height)
            {
                _VScrollBar.Enabled = true;
                _VScrollBar.Maximum = _DrawingBoard.Height - Height;
            }
        }

        #endregion

        #region IDesignBenchCore Method

        public RectangleList Rectangles { get; internal set; }

        #region 缩放率

        private double _Zoom;

        /// <summary>
        ///     当前设计图像的缩放率
        /// </summary>
        public double Zoom
        {
            get { return _Zoom; }
            internal set
            {
                double old = _Zoom;
                _Zoom = value;
                OnZoomChanged(new ChangedEventArgs<double>(old, value));
            }
        }

        #endregion

        public void SetDrawingBoardDesignMode(DrawingBoardDesignMode mode)
        {
            _DrawingBoard.ImagePanelDesignMode = mode;
        }

        public void SetSelectedImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("指定的设计图片image不应为Null");
            }
            _DrawingBoard.Image = image;

            _DrawingBoard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _DrawingBoard.SizeChanged += delegate { SetImageDesignPanelLocation(); };

            SetImageDesignPanelLocation();
            _DrawingBoard.Visible = true;
        }

        public void RespondKeyEvent(Keys key)
        {
            switch (key)
            {
                case Keys.Delete:
                {
                    if (_DrawingBoard.ImagePanelDesignMode == DrawingBoardDesignMode.Selecting)
                    {
                        _DrawingBoard.RemoveSelectedRectangle();
                    }
                    break;
                }
            }
        }

        private void SetImageDesignPanelLocation()
        {
            int x = (Width - 20 - _DrawingBoard.Width)/2;
            int y = (Height - 20 - _DrawingBoard.Height)/2;
            _DrawingBoard.Location = new Point(x, y);
        }

        #endregion

        #region IDesignBenchCore Event

        public event EventHandler<ChangedEventArgs<double>> ZoomChanged;

        public event EventHandler<RectangleListChangedEventArgs> RectangleRemoved;
        public event EventHandler<RectangleListChangedEventArgs> RectangleCreated;
        public event EventHandler<RectangleListChangedEventArgs> RectangleUpdated;
        public event EventHandler<RectangleClickEventArgs> RectangleDoubleClick;
        public event EventHandler<RectangleClickEventArgs> RectangleClick;
        public event EventHandler<ImageLoadEventArgs> ImageLoaded;
        public event EventHandler<RectangleSelectingEventArgs> Selecting;
        public event EventHandler<RectangleSelectedEventArgs> Selected;
        public event EventHandler<BoardDesignDragParamsEventArgs> DesignDragging;
        public event EventHandler<BoardDesignDragParamsEventArgs> DesignDragged;

        public event EventHandler<MouseEventArgs> BenchDoubleClick;

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            EventHandler<MouseEventArgs> handler = BenchDoubleClick;
            if (handler != null)
                handler(this, (MouseEventArgs) e);
        }

        private void OnZoomChanged(ChangedEventArgs<double> e)
        {
            EventHandler<ChangedEventArgs<double>> handler = ZoomChanged;
            if (handler != null)
                handler(this, e);
        }

        internal void OnRectangleCreated(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleCreated;
            if (handler != null)
                handler(this, e);
        }

        internal void OnRectangleRemoved(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleRemoved;
            if (handler != null)
                handler(this, e);
        }

        internal void OnRectangleUpdated(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleUpdated;
            if (handler != null)
                handler(this, e);
        }

        internal void OnRectangleDoubleClick(RectangleClickEventArgs e)
        {
            EventHandler<RectangleClickEventArgs> handler = RectangleDoubleClick;
            if (handler != null)
                handler(this, e);
        }

        internal void OnRectangleClick(RectangleClickEventArgs e)
        {
            EventHandler<RectangleClickEventArgs> handler = RectangleClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void OnImageLoaded(ImageLoadEventArgs e)
        {
            EventHandler<ImageLoadEventArgs> handler = ImageLoaded;
            if (handler != null)
                handler(this, e);
        }

        internal void OnSelecting(RectangleSelectingEventArgs e)
        {
            EventHandler<RectangleSelectingEventArgs> handler = Selecting;
            if (handler != null)
                handler(this, e);
        }

        internal void OnSelected(RectangleSelectedEventArgs e)
        {
            EventHandler<RectangleSelectedEventArgs> handler = Selected;
            if (handler != null)
                handler(this, e);
        }

        internal void OnDesignDragging(BoardDesignDragParamsEventArgs e)
        {
            EventHandler<BoardDesignDragParamsEventArgs> handler = DesignDragging;
            if (handler != null)
                handler(this, e);
        }

        internal void OnDesignDragged(BoardDesignDragParamsEventArgs e)
        {
            EventHandler<BoardDesignDragParamsEventArgs> handler = DesignDragged;
            if (handler != null)
                handler(this, e);
        }

        #endregion

    }
}