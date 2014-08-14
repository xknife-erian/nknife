using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Draws.Controls;
using NKnife.Draws.Designs.Base;
using NKnife.Draws.Designs.Event;
using NKnife.Events;

namespace NKnife.Draws.Designs
{
    public sealed partial class DesignBench : Control, IDesignBenchCore
    {
        #region 成员变量

        private readonly ImageDesignPanel _ImageDesignPanel;
        private HScrollBar _HScrollBar = new HScrollBar();
        private VScrollBar _VScrollBar = new VScrollBar();

        internal ImageDesignPanel ImageDesignPanel
        {
            get { return _ImageDesignPanel; }
        }

        #endregion

        #region 构造函数

        public DesignBench()
        {
            Rectangles = new RectangleList();
            InitializeComponent();

            _HScrollBar.Dock = DockStyle.Bottom;
            _HScrollBar.Enabled = false;
            _HScrollBar.Scroll += _HScrollBar_Scroll;
            _VScrollBar.Dock = DockStyle.Right;
            _VScrollBar.Enabled = false;
            _VScrollBar.Scroll += _VScrollBar_Scroll;
            _ImageDesignPanel = new ImageDesignPanel {Visible = false};
            _ImageDesignPanel.SizeChanged += _ImageDesignPanel_SizeChanged;
            _ImageDesignPanel.PanelDragging += _ImageDesignPanel_PanelDragging;

            Controls.Add(_VScrollBar);
            Controls.Add(_HScrollBar);
            Controls.Add(_ImageDesignPanel);
        }

        private void _ImageDesignPanel_PanelDragging(object sender, PanelDraggingEventArgs e)
        {
            var screenPoint = _ImageDesignPanel.PointToScreen(e.MousePoint);
            var ownPoint = PointToClient(screenPoint);
            // TODO: 2014年8月15日，拖拽图片，下方的SizeChanged好象不对，放大后应该用一个事件来触发，以控制滚动条的位置
        }

        private void _VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _ImageDesignPanel.Top = -e.NewValue;
        }

        private void _HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _ImageDesignPanel.Left = -e.NewValue;
        }

        private void _ImageDesignPanel_SizeChanged(object sender, EventArgs e)
        {
            if (_ImageDesignPanel.Width > Width)
            {
                _HScrollBar.Enabled = true;
                _HScrollBar.Maximum = _ImageDesignPanel.Width - Width;
            }
            if (_ImageDesignPanel.Height > Height)
            {
                _VScrollBar.Enabled = true;
                _VScrollBar.Maximum = _ImageDesignPanel.Height - Height;
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
                        _ImageDesignPanel.RemoveSelectedRectangle();
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

        internal void OnDesignDragging(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragging;
            if (handler != null)
                handler(this, e);
        }

        internal void OnDesignDragged(DragParamsEventArgs e)
        {
            EventHandler<DragParamsEventArgs> handler = DesignDragged;
            if (handler != null)
                handler(this, e);
        }

        #endregion

    }
}