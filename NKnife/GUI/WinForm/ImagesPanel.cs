using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NKnife.Events;

namespace NKnife.GUI.WinForm
{
    public sealed class ImagesPanel : FlowLayoutPanel
    {
        public enum ImageBoxSize
        {
            XLarge,
            Large,
            Medium,
            Small,
            XSmall
        }

        private readonly List<string> _Images = new List<string>();
        private string[] _CurrImages = new string[0];

        public ImagesPanel()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            FlowDirection = FlowDirection.LeftToRight;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;

            ImageBoxLabelFont = DefaultFont;
            ImageBoxColor = Color.Brown;
            ImageBoxLabelColor = Color.Beige;
            BoxSize = ImageBoxSize.Medium;
            BoxMargin = 10;
        }

        public Font ImageBoxLabelFont { get; set; }

        public Color ImageBoxLabelColor { get; set; }

        public Color ImageBoxColor { get; set; }

        public ImageBoxSize BoxSize { get; set; }

        public int BoxMargin { get; set; }

        public Func<string, string> BuildLabelText { get; set; } 

        [Browsable(false)]
        public string SelectedImageFile { get; set; }

        public event EventHandler<EventArgs<string>> SelectedImage;

        private void OnSelectedImage(EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = SelectedImage;
            if (handler != null)
                handler(this, e);
        }

        public void FillImages(params string[] images)
        {
            _CurrImages = images;
            _Images.AddRange(images);
            var readImageThread = new Thread(ReadImage);
            readImageThread.IsBackground = true;
            readImageThread.Start();
        }

        private void ReadImage()
        {
            if (_CurrImages == null || _CurrImages.Length <= 0)
                return;
            foreach (string currImageFile in _CurrImages)
            {
                try
                {
                    byte[] b = File.ReadAllBytes(currImageFile);
                    var mem = new MemoryStream(b);
                    mem.Position = 0;
                    Image image = Image.FromStream(mem);

                    var imagebox = new ImageBox(ImageBoxColor, ImageBoxLabelColor, ImageBoxLabelFont);
                    var w = 80;
                    var h = 80/3*4;
                    int p = w/2 - w/20;
                    switch (BoxSize)
                    {
                        case ImageBoxSize.XLarge:
                            imagebox.Size = new Size(w*5, h*5);
                            p = w*5/2 - w*5/20;
                            break;
                        case ImageBoxSize.Large:
                            imagebox.Size = new Size(w*4, h*4);
                            p = w*5/2 - w*5/20;
                            break;
                        case ImageBoxSize.Medium:
                            imagebox.Size = new Size(w*3, h*3);
                            p = w*5/2 - w*5/20;
                            break;
                        case ImageBoxSize.Small:
                            imagebox.Size = new Size(w*2, h*2);
                            p = w*5/2 - w*5/20;
                            break;
                        case ImageBoxSize.XSmall:
                            imagebox.Size = new Size(w*1, h*1);
                            p = w*5/2 - w*5/20;
                            break;
                    }
                    Padding = new Padding(p,0,p,0);
                    imagebox.Margin = new Padding(BoxMargin, BoxMargin, BoxMargin, BoxMargin);
                    var label = currImageFile;
                    if (BuildLabelText != null)
                    {
                        label = BuildLabelText.Invoke(currImageFile);
                    }
                    imagebox.SetImage(image, label);

                    this.ThreadSafeInvoke(() => Controls.Add(imagebox));
                }
                catch (Exception)
                {
                    Debug.Fail(string.Format("文件无法读取:{0}", currImageFile));
                }
            }
        }

        private sealed class ImageBox : Control
        {
            private readonly Label _Label = new Label();
            private readonly Control _PictureBox = new Control();
            private bool _IsSelected;

            public ImageBox(Color imageBoxColor, Color imageBoxLabelColor, Font imageBoxLabelFont)
            {
                SuspendLayout();

                _Label.Dock = DockStyle.Bottom;
                _Label.BackColor = imageBoxLabelColor;
                _Label.Font = imageBoxLabelFont;
                _Label.TextAlign = ContentAlignment.MiddleCenter;

                _PictureBox.Location = new Point(0, 0);
                _PictureBox.BackColor = imageBoxColor;
                _PictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

                Controls.Add(_PictureBox);
                Controls.Add(_Label);
                ResumeLayout(false);
                PerformLayout();
                SetStyle(
                    ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint, true);

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

            public void SetImage(Image currImage, string label)
            {
                _PictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                _PictureBox.BackgroundImage = currImage;
                _Label.Text = label;
            }
        }
    }
}