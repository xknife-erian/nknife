using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Events;
using NKnife.Win.Forms.Common;
using NKnife.Win.Forms.EventParams;
using NKnife.Win.Forms.Frames;
using NKnife.Win.Forms.Frames.Base;

namespace NKnife.Win.Forms.Frames
{
    public sealed partial class PictureFrame : Control, IPictureFrame
    {
        #region 成员变量

        private readonly DrawingBoard _DrawingBoard;
        private readonly HScrollBar _HScrollBar = new HScrollBar();
        private readonly VScrollBar _VScrollBar = new VScrollBar();

        /// <summary>
        ///     画板与画框之间留出的空白区域。
        /// </summary>
        private int _Blankness = 20;

        /// <summary>
        ///     画板与画框之间留出的空白区域。
        /// </summary>
        public int Blankness
        {
            get { return _Blankness; }
            set { _Blankness = value; }
        }

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

        #endregion

        #region 缩放，滚动条, 鼠标滚轮

        private void _DrawingBoard_PanelZoomed(object sender, BoardZoomEventArgs e)
        {
            var mpoint = PointToClient(MousePosition);
            if (_DrawingBoard.Width > Width)
            {
                _HScrollBar.Enabled = true;
                _HScrollBar.Minimum = -_Blankness;
                _HScrollBar.Maximum = _DrawingBoard.Width - Width + _Blankness;

                var acu = e.MouseClickedLocation.X / e.OldZoom;
                var now = acu * Zoom;
                _HScrollBar.Value = -(int)(mpoint.X - now);
            }
            else
            {
                _HScrollBar.Enabled = false;
            }
            if (_DrawingBoard.Height > Height)
            {
                _VScrollBar.Enabled = true;
                _VScrollBar.Minimum = -_Blankness;
                _VScrollBar.Maximum = _DrawingBoard.Height - Height + _Blankness;

                var acu = e.MouseClickedLocation.Y / e.OldZoom;
                var now = acu * Zoom;
                _VScrollBar.Value = -(int)(mpoint.Y - now);
            }
            else
            {
                _VScrollBar.Enabled = false;
            }
            Refresh();
        }

        private void _DrawingBoard_PanelDragging(object sender, BoardDraggingEventArgs e)
        {
            if (_DrawingBoard.Width <= Width && _DrawingBoard.Height <= Height)
                return;
            //根据拖动的起点和当前点之间的距离，触发滚动条的Value发生变化，再间接触发画板的移动
            Point current = PointToClient(_DrawingBoard.PointToScreen(e.CurrentPoint));
            Point start = PointToClient(_DrawingBoard.PointToScreen(e.StartPoint));

            int x = (current.X - start.X) / 3;
            int y = (current.Y - start.Y) / 3;

            int offsetX = _HScrollBar.Value - x;
            if (offsetX > _HScrollBar.Minimum && offsetX < _HScrollBar.Maximum)
                _HScrollBar.Value = offsetX;
            int offsetY = _VScrollBar.Value - y;
            if (offsetY > _VScrollBar.Minimum && offsetY < _VScrollBar.Maximum)
                _VScrollBar.Value = _VScrollBar.Value - y;
            //TODO:左与上的边距有了，为什么下与右的边距没有呢？
        }

        private void _VScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _DrawingBoard.Top = -_VScrollBar.Value;
        }

        private void _HScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _DrawingBoard.Left = -_HScrollBar.Value;
        }

        #endregion

        #region IDesignBenchCore Method

        public RectangleList Rectangles { get; private set; }

        /// <summary>
        /// 更新矩形列表
        /// </summary>
        /// <param name="rectangleList"></param>
        public void UpdateRectangleList(RectangleList rectangleList)
        {
            Rectangles = rectangleList;
        }

        public void SetDrawingBoardDesignMode(DrawingBoardDesignMode mode)
        {
            _DrawingBoard.ImagePanelDesignMode = mode;
        }

        public void SetSelectedImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image", "指定的设计图片image不应为Null");
            }
            if(_DrawingBoard.Image != null)
                Rectangles.Clear();
            _DrawingBoard.Image = image;

            _DrawingBoard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _DrawingBoard.SizeChanged += delegate { SetImageDesignPanelLocationOnLoaded(); };

            SetImageDesignPanelLocationOnLoaded();
            _DrawingBoard.Visible = true;
        }

        /// <summary>
        /// 设置当前被选择的矩形的下一步操作模式
        /// </summary>
        /// <param name="selectedMode"></param>
        public void SetSelectedMode(RectangleList.SelectedMode selectedMode)
        {
            Rectangles.Current.SelectedMode = selectedMode;
        }

        /// <summary>
        /// 将当前已克隆的进行粘贴
        /// </summary>
        /// <param name="screenPoint"></param>
        public void PasteSelected(Point screenPoint)
        {
            if (_DrawingBoard.ImagePanelDesignMode != DrawingBoardDesignMode.Selecting)
                return;
            _DrawingBoard.PasteSelectedRectangle(screenPoint);
        }

        public void DeleteSelected()
        {
            if (_DrawingBoard.ImagePanelDesignMode == DrawingBoardDesignMode.Selecting)
            {
                _DrawingBoard.RemoveSelectedRectangle();
            }
        }

        public void SelectAll()
        {
            _DrawingBoard.SelectAllRectangles();
        }

        /// <summary>
        /// 根据指定的操作模式对矩形进行操作
        /// </summary>
        /// <param name="ro">指定的操作模式</param>
        public void RectangleOperating(RectangleOperation ro)
        {
            _DrawingBoard.RectangleOperating(ro);
        }

        public void PressShiftKey(bool isKeyDown)
        {
            if (_DrawingBoard.ImagePanelDesignMode == DrawingBoardDesignMode.Zooming)
                _DrawingBoard.SetZoomMode(isKeyDown);
        }

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

        /// <summary>首次载入图片时设置图板的位置
        /// </summary>
        private void SetImageDesignPanelLocationOnLoaded()
        {
            int x = (Width - _Blankness - _DrawingBoard.Width)/2;
            int y = (Height - _Blankness - _DrawingBoard.Height)/2;
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