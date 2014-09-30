using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public sealed class ImagesPanel : FlowLayoutPanel
    {
        private string[] _CurrImages;

        public ImagesPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            FlowDirection = FlowDirection.LeftToRight;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            Images = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                var box = new ImageBox();
                box.Size = new Size(160, 200);
                box.Margin = new Padding(10, 10, 10, 10);
                Controls.Add(box);
            }
        }

        public List<string> Images { get; set; }

        public void FillImages(params string[] images)
        {
            _CurrImages = images;
            Images.AddRange(images);
            var readImageThread = new Thread(ReadImage);
            readImageThread.IsBackground = true;
            readImageThread.Start();
        }

        private void ReadImage()
        {
            if (_CurrImages == null || _CurrImages.Length <= 0)
                return;
            foreach (string currImage in _CurrImages)
            {
                try
                {
                    Image image = Image.FromFile(currImage);
                    var imagebox = new ImageBox();
                    imagebox.Size = new Size(160, 200);
                    imagebox.Margin = new Padding(10, 10, 10, 10);
                    imagebox.SetImage(image);
                    this.ThreadSafeInvoke(() => Controls.Add(imagebox));
                }
                catch (Exception)
                {
                    Debug.Fail(string.Format("文件无法读取:{0}", currImage));
                }
            }
        }

        private sealed class ImageBox : Control
        {
            private readonly Label _Label = new Label();
            private readonly Control _PictureBox = new Control();
            private bool _IsSelected;

            public ImageBox()
            {
                SuspendLayout();

                _Label.Dock = DockStyle.Bottom;
                _Label.TextAlign = ContentAlignment.MiddleCenter;

                _PictureBox.Location = new Point(0, 0);
                _PictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                Controls.Add(_PictureBox);
                Controls.Add(_Label);
                ResumeLayout(false);
                PerformLayout();
                SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

                _PictureBox.BackColor = Color.Brown;
                _Label.BackColor = Color.Goldenrod;
                _Label.Text = "abcdefg.png";
                _PictureBox.MouseHover += _PictureBox_MouseHover;
                _PictureBox.MouseLeave += _PictureBox_MouseLeave;
                _PictureBox.MouseClick += _PictureBox_MouseClick;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (_IsSelected == false)
                    return;
                Graphics g = e.Graphics;
                g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(0, 0, Width - 1, Height - 1));
            }

            private void _PictureBox_MouseLeave(object sender, EventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Regular);
            }

            private void _PictureBox_MouseHover(object sender, EventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);
            }

            private void _PictureBox_MouseClick(object sender, MouseEventArgs e)
            {
                _Label.Font = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);
                _IsSelected = true;
                Refresh();
            }

            protected override void OnAutoSizeChanged(EventArgs e)
            {
                base.OnAutoSizeChanged(e);
                SetOwnControls();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                SetOwnControls();
            }

            private void SetOwnControls()
            {
                _PictureBox.Height = Height - _Label.Height;
            }

            public void SetImage(Image currImage)
            {
                _PictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                _PictureBox.BackgroundImage = currImage;
            }
        }
    }
}