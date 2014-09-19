using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Jeelu.Win
{
    public class ImgButton : Button
    {
        private MyImgButtonStyle _style;
        [Category("My")]
        public MyImgButtonStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        private Color _buttonColor = Color.FromArgb(0, 120, 0);
        [Category("My")]
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set { _buttonColor = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            float offsetX = (float)this.Width / 10 + (float)this.Width / 10;
            float offsetY = (float)this.Height / 10 + (float)this.Height / 10;
            Graphics g = pevent.Graphics;
            switch (Style)
            {
                case MyImgButtonStyle.Add:
                    {
                        /// 画加号的横线
                        PointF locationH = new PointF(
                            offsetX,
                            this.Height / 2 - offsetY / 2);
                        float widthH = (float)this.Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_buttonColor);
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        /// 画加号的竖线
                        PointF locationV = new PointF(
                            (float)this.Width / 2 - offsetX / 2,
                            offsetY);
                        float widthV = offsetX;
                        float heightV = (float)this.Height - offsetY - offsetY;
                        RectangleF rV = new RectangleF(locationV, new SizeF(widthV, heightV));
                        g.FillRectangle(brush, rV);
                        break;
                    }
                case MyImgButtonStyle.ReMove:
                    {
                        /// 画减号的横线
                        PointF locationH = new PointF(
                            offsetX,
                            this.Height / 2 - offsetY / 2);
                        float widthH = (float)this.Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_buttonColor);
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case MyImgButtonStyle.ArrowUp:
                    {
                        /// 画箭头
                        PointF PointF1 = new PointF(
                            (float)this.Width / 2,
                            offsetY);
                        PointF PointF2 = new PointF(
                            offsetX,
                            (float)this.Height/2);
                        PointF PointF3 = new PointF(
                            (float)this.Width - offsetX,
                            (float)this.Height / 2);
                        PointF[] PointFArr = new PointF[] { PointF1, PointF2, PointF3 };
                        Brush brush = new SolidBrush(_buttonColor);
                        g.FillPolygon(brush, PointFArr);

                        /// 画箭头的竖线
                        PointF locationH = new PointF(
                            (float)this.Width / 2 - offsetX / 2,
                            (float)this.Height / 2);
                        float widthH = offsetX;
                        float heightH = (float)this.Height/2 - offsetY;
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case MyImgButtonStyle.ArrowDown:
                    {
                        /// 画箭头
                        PointF PointF1 = new PointF(
                            (float)this.Width / 2,
                            this.Height-offsetY);
                        PointF PointF2 = new PointF(
                            offsetX,
                            (float)this.Height / 2);
                        PointF PointF3 = new PointF(
                            (float)this.Width - offsetX,
                            (float)this.Height / 2);
                        PointF[] PointFArr = new PointF[] { PointF1, PointF2, PointF3 };
                        Brush brush = new SolidBrush(_buttonColor);
                        g.FillPolygon(brush, PointFArr);

                        /// 画箭头的竖线
                        PointF locationH = new PointF(
                            (float)this.Width / 2 - offsetX / 2,
                            offsetY);
                        float widthH = offsetX;
                        float heightH = (float)this.Height / 2 - offsetY;
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case MyImgButtonStyle.ArrowLeft:
                    {
                        /// 画箭头
                        PointF PointF1 = new PointF(
                            offsetX,
                            (float)this.Height / 2);
                        PointF PointF2 = new PointF(
                            (float)this.Width / 2,
                            offsetY);
                        PointF PointF3 = new PointF(
                            (float)this.Width / 2,
                            (float)this.Height - offsetY);
                        PointF[] PointFArr = new PointF[] { PointF1, PointF2, PointF3 };
                        Brush brush = new SolidBrush(_buttonColor);
                        g.FillPolygon(brush, PointFArr);

                        /// 画箭头的横线
                        PointF locationH = new PointF(
                            (float)this.Width / 2,
                            this.Height / 2 - offsetY / 2);
                        float widthH = this.Width / 2 - offsetX;
                        float heightH = offsetY+1;
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case MyImgButtonStyle.ArrowRight:
                    {
                        /// 画箭头
                        PointF PointF1 = new PointF(
                            this.Width - offsetX,
                            (float)this.Height / 2);
                        PointF PointF2 = new PointF(
                            (float)this.Width / 2,
                            offsetY);
                        PointF PointF3 = new PointF(
                            (float)this.Width / 2,
                            (float)this.Height - offsetY);
                        PointF[] PointFArr = new PointF[] { PointF1, PointF2, PointF3 };
                        Brush brush = new SolidBrush(_buttonColor);
                        g.FillPolygon(brush, PointFArr);

                        /// 画箭头的横线
                        PointF locationH = new PointF(
                            offsetX,
                            this.Height / 2 - offsetY / 2);
                        float widthH = this.Width / 2 - offsetX+2;
                        float heightH = offsetY + 1;
                        RectangleF rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                default:
                    break;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.Text = string.Empty;
        }

        public enum MyImgButtonStyle
        {
            Add,
            ReMove,
            ArrowUp,
            ArrowDown,
            ArrowLeft,
            ArrowRight,
        }
    }
}
