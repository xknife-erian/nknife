using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Draws.Common;
using NKnife.Draws.Common.Event;
using NKnife.Events;

namespace NKnife.Draws
{
    public partial class DesignBench : UserControl, IDesignBenchCore
    {
        #region 成员变量

        private readonly ImageDesignPanel _ImageDesignPanel;

        internal ImageDesignPanel ImageDesignPanel
        {
            get { return _ImageDesignPanel; }
        }

        #endregion

        #region 构造函数

        public DesignBench()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            _ImageDesignPanel = new ImageDesignPanel {Visible = false};
            InitializeComponent();
            RectangleList = new RectangleList();
            Controls.Add(_ImageDesignPanel);
        }

        #endregion

        #region 放大与缩小,及移动设计面板




        #endregion

        #region IDesignBenchCore Method

        public RectangleList RectangleList { get; internal set; }

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

        public void SetImagePanelDesignMode(ImagePanelDesignMode mode)
        {
            _ImageDesignPanel.ImagePanelDesignMode = mode;
        }

        public void SetSelectedImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("指定的设计图片image不应为Null");
            }
            _ImageDesignPanel.Image = image;

            _ImageDesignPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _ImageDesignPanel.SizeChanged += delegate { SetImageDesignPanelLocation(); };

            SetImageDesignPanelLocation();
            _ImageDesignPanel.Visible = true;
        }

        public void RespondKeyEvent(Keys key)
        {
            switch (key)
            {
                case Keys.Delete:
                {
                    if (_ImageDesignPanel.ImagePanelDesignMode == ImagePanelDesignMode.Selecting)
                    {
                        _ImageDesignPanel.RemoveCurrentRectangle();
                    }
                    break;
                }
            }
        }

        private void SetImageDesignPanelLocation()
        {
            int x = (Width - 20 - _ImageDesignPanel.Width)/2;
            int y = (Height - 20 - _ImageDesignPanel.Height)/2;
            _ImageDesignPanel.Location = new Point(x, y);
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
        public event EventHandler<DragParamsEventArgs> DesignDragging;
        public event EventHandler<DragParamsEventArgs> DesignDragged;

        public event EventHandler<MouseEventArgs> BenchDoubleClick;

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            EventHandler<MouseEventArgs> handler = BenchDoubleClick;
            if (handler != null)
                handler(this, (MouseEventArgs) e);
        }

        protected virtual void OnZoomChanged(ChangedEventArgs<double> e)
        {
            EventHandler<ChangedEventArgs<double>> handler = ZoomChanged;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnRectangleCreated(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleCreated;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnRectangleRemoved(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleRemoved;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnRectangleUpdated(RectangleListChangedEventArgs e)
        {
            EventHandler<RectangleListChangedEventArgs> handler = RectangleUpdated;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnRectangleDoubleClick(RectangleClickEventArgs e)
        {
            EventHandler<RectangleClickEventArgs> handler = RectangleDoubleClick;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnRectangleClick(RectangleClickEventArgs e)
        {
            EventHandler<RectangleClickEventArgs> handler = RectangleClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal virtual void OnImageLoaded(ImageLoadEventArgs e)
        {
            EventHandler<ImageLoadEventArgs> handler = ImageLoaded;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnSelecting(RectangleSelectingEventArgs e)
        {
            EventHandler<RectangleSelectingEventArgs> handler = Selecting;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnSelected(RectangleSelectedEventArgs e)
        {
            EventHandler<RectangleSelectedEventArgs> handler = Selected;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnDesignDragging(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragging;
            if (handler != null)
                handler(this, e);
        }

        internal virtual void OnDesignDragged(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragged;
            if (handler != null)
                handler(this, e);
        }

        #endregion

    }
}