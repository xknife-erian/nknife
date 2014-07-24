using System.Drawing;
using System.Windows.Forms;
using NKnife.Draws.Common;

namespace NKnife.Draws
{
    public partial class DesignBench : UserControl
    {
        private readonly ImageDesignPanel _ImageDesignPanel = new ImageDesignPanel();

        public ImageDesignPanel ImageDesignPanel
        {
            get { return _ImageDesignPanel; }
        }

        public DesignBench()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            _ImageDesignPanel.DesignDragging += delegate(object sender, DragParamsEventArgs e)
            {
                _StartLabel.Text = e.Start.ToString();
                _EndLabel.Text = e.End.ToString();
            };
        }

        public void SetSelectedImage(Image image)
        {
            if (image == null)
            {
                return;
            }
            _ImageDesignPanel.ParentSize = Size;
            _ImageDesignPanel.Image = image;

            _ImageDesignPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _ImageDesignPanel.SizeChanged += delegate { SetImageDesignPanelLocation(); };

            SetImageDesignPanelLocation();
            Controls.Add(_ImageDesignPanel);
        }

        private void SetImageDesignPanelLocation()
        {
            int x = (Width - 20 - _ImageDesignPanel.Width)/2;
            int y = (Height - 20 - _ImageDesignPanel.Height)/2;
            _ImageDesignPanel.Location = new Point(x, y);
        }

        public void SetImagePanelDesignMode(ImagePanelDesignMode mode)
        {
            _ImageDesignPanel.ImagePanelDesignMode = mode;
        }
    }
}
