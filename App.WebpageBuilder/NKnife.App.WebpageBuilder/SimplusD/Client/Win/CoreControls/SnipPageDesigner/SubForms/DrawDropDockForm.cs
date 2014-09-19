using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class DrawDropDockForm : Form
    {
        public readonly static DrawDropDockForm Singler = new DrawDropDockForm();
        public static HalfOpacityForm _halfOpacityForm;
        static bool _isShow;
        static public bool IsShow
        {
            get { return _isShow; }
        }

        static private int _imageWidth;
        static public int ImageWidth
        {
            get { return _imageWidth; }
        }

        static bool _showInto;

        static Image _imgLeft;
        static Image _imgRight;
        static Image _imgTop;
        static Image _imgBottom;
        static Image _imgInto;

        static Rectangle _leftRect;
        static Rectangle _topRect;
        static Rectangle _rightRect;
        static Rectangle _bottomRect;
        static Rectangle _intoRect;

        static DrawDropDockForm()
        {
            _imgLeft = ResourceService.GetResourceImage("page.img.DockDenotebitmap");
            _imageWidth = _imgLeft.Width;
            _imgTop = new Bitmap(_imgLeft);
            _imgTop.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _imgRight = new Bitmap(_imgTop);
            _imgRight.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _imgBottom = new Bitmap(_imgRight);
            _imgBottom.RotateFlip(RotateFlipType.Rotate90FlipNone);
            _imgInto = ResourceService.GetResourceImage("page.img.DockDenoteIntobitmap");

            _halfOpacityForm = new HalfOpacityForm();
        }

        private DrawDropDockForm()
        {
            ///设置一些初始值
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.AllowTransparency = true;
            this.Opacity = 0.7;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            ///画四个指示符号
            g.DrawImage(_imgLeft, _leftRect);
            g.DrawImage(_imgTop, _topRect);
            g.DrawImage(_imgRight, _rightRect);
            g.DrawImage(_imgBottom, _bottomRect);
            if (_showInto)
            {
                g.DrawImage(_imgInto, _intoRect);
            }

            base.OnPaint(e);
        }

        static Rectangle _prevRect;
        static public void ShowForm(int x, int y, int width, int height,bool showInto)
        {
            _showInto = showInto;
            Rectangle thisRect = new Rectangle(x, y, width, height);
            if (_prevRect == thisRect)
            {
                return;
            }
            HideForm();

            _isShow = true;
            _prevRect = thisRect;
            Utility.DllImport.SetWindowShow(Singler, null, x, y, width, height);

            ///计算指示图片显示的位置
            int showCount = _showInto ? 3 : 2;
            int hWidth = Math.Min(_imgTop.Width, width);   //此为水平图片的宽度
            int hHeight = Math.Min(_imgTop.Height, height / showCount);   //此为水平图片的高度
            int vWidth = Math.Min(_imgLeft.Width, width / showCount);   //此为垂直图片的宽度
            int vHeight = Math.Min(_imgLeft.Height, height);   //此为垂直图片的高度
            //if (vWidth + hWidth + vWidth > Singler.Width && hHeight + vHeight + hHeight > Singler.Height)
            //{
            //    vWidth = hWidth = Singler.Width / 3;
            //    hHeight = vHeight = Singler.Height / 3;
            //}

            _leftRect = new Rectangle(0, (Singler.Height - vHeight) / 2, vWidth, vHeight);
            _topRect = new Rectangle((Singler.Width - hWidth) / 2, 0, hWidth, hHeight);
            _rightRect = new Rectangle(Singler.Width - vWidth, (Singler.Height - vHeight) / 2, vWidth, vHeight);
            _bottomRect = new Rectangle((Singler.Width - hWidth) / 2, Singler.Height - hHeight, hWidth, hHeight);
            if (_showInto)
            {
                _intoRect = new Rectangle((Singler.Width - hWidth) / 2, (Singler.Height - vHeight) / 2,ImageWidth,ImageWidth);
            }
        }
        protected override void SetVisibleCore(bool value)
        {
            if (value == false)
            {
                _isShow = false;
            }
            base.SetVisibleCore(value);
        }
        static public void HideForm()
        {
            _isShow = false;
            _prevRect = Rectangle.Empty;
            HideHalfOpacity();
            Singler.Hide();
        }
        /// <summary>
        /// 获取DrawDrop的结果(上，下，左，右[，里面])
        /// </summary>
        static public DrawDropResult GetDrawDropResult()
        {
            if (!IsShow)
            {
                return DrawDropResult.None;
            }

            Point mouseLocationClient = Singler.PointToClient(Control.MousePosition);
            if (_leftRect.Contains(mouseLocationClient))
            {
                return DrawDropResult.Left;
            }
            else if (_topRect.Contains(mouseLocationClient))
            {
                return DrawDropResult.Top;
            }
            else if (_rightRect.Contains(mouseLocationClient))
            {
                return DrawDropResult.Right;
            }
            else if (_bottomRect.Contains(mouseLocationClient))
            {
                return DrawDropResult.Bottom;
            }
            else if (_intoRect.Contains(mouseLocationClient))
            {
                return DrawDropResult.Into;
            }
            return DrawDropResult.None;
        }
        static public void HideHalfOpacity()
        {
            _halfOpacityForm.Bounds = new Rectangle(0, 0, 0, 0);
            _halfOpacityForm.Hide();
        }

        static Rectangle _prevHalfOpacityRect;
        static DrawDropResult _prevHalfOpacityResult;
        static public void ShowHalfOpacity(DrawDropResult result)
        {
            ///如果为None，则隐藏
            if (result == DrawDropResult.None)
            {
                HideHalfOpacity();
                return;
            }
            
            Rectangle tempRect;
            switch (result)
            {
                case DrawDropResult.Left:
                    tempRect = _leftRect;
                    break;
                case DrawDropResult.Top:
                    tempRect = _topRect;
                    break;
                case DrawDropResult.Right:
                    tempRect = _rightRect;
                    break;
                case DrawDropResult.Bottom:
                    tempRect = _bottomRect;
                    break;
                case DrawDropResult.Into:
                    tempRect = _intoRect;
                    break;
                default:
                    throw new Exception();
            }
            if (_halfOpacityForm.Visible && result == _prevHalfOpacityResult && _prevHalfOpacityRect == tempRect)
            {
                return;
            }
            else
            {
                _prevHalfOpacityResult= result;
                _prevHalfOpacityRect = tempRect;
            }

            Point tempPoint = Singler.PointToScreen(tempRect.Location);
            Utility.DllImport.SetWindowShow(_halfOpacityForm, null, tempPoint.X, tempPoint.Y, tempRect.Width, tempRect.Height);
        }
    }

    public enum DrawDropResult
    {
        None            = 0,
        Left            = 1,
        Top             = 2,
        Right           = 3,
        Bottom          = 4,
        Into            = 5,
    }
}
