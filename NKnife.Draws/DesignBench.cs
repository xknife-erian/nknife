using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Draws.Common;
using NKnife.Draws.Common.Event;

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

        #region IDesignBenchCore

        public RectangleList RectangleList { get; internal set; }

        public void SetImagePanelDesignMode(ImagePanelDesignMode mode)
        {
            _ImageDesignPanel.ImagePanelDesignMode = mode;
        }

        /// <summary>
        /// 设置打算设计的图片
        /// </summary>
        /// <param name="image">指定的设计图片</param>
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

        private void SetImageDesignPanelLocation()
        {
            int x = (Width - 20 - _ImageDesignPanel.Width)/2;
            int y = (Height - 20 - _ImageDesignPanel.Height)/2;
            _ImageDesignPanel.Location = new Point(x, y);
        }

        #endregion

        #region Event

        public event EventHandler<RectangleClickEventArgs> RectangleDoubleClick;
        public event EventHandler<ImageLoadEventArgs> ImageLoaded;
        public event EventHandler<RectangleSelectingEventArgs> Selecting;
        public event EventHandler<RectangleSelectedEventArgs> Selected;
        public event EventHandler<DragParamsEventArgs> DesignDragging;
        public event EventHandler<DragParamsEventArgs> DesignDragged;

        internal virtual void OnRectangleDoubleClick(RectangleClickEventArgs e)
        {
            EventHandler<RectangleClickEventArgs> handler = RectangleDoubleClick;
            if (handler != null) 
                handler(this, e);
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
