using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    internal partial class ColorPad : ColorBaseControl
    {
        const byte ColorMax = 255;
        const byte BottomColor = 128;

        private ColorSelector _ownerColorSelector;
        public ColorSelector OwnerColorSelector
        {
            get { return _ownerColorSelector; }
            set
            {
                _ownerColorSelector = value;
            }
        }

        private Point _colorLocation;

        private Point _selectedLocation;

        public ColorPad()
        {
        }

        protected override void OnCreateControl()
        {
            ///先在缓冲区画好
            Bitmap tempBitmap = new Bitmap(((int)ColorMax + 1) * 6, (ColorMax + 1) - BottomColor);
            int iWidth = tempBitmap.Width;
            int iHeight = tempBitmap.Height;

            Rectangle rect = new Rectangle(0, 0, iWidth, iHeight);
            System.Drawing.Imaging.BitmapData bmpData = tempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr iPtr = bmpData.Scan0;

            int iBytes = iWidth * iHeight * 3;
            byte[] PixelValues = new byte[iBytes];
            int iPoint=0;
            for (int y = 0; y < tempBitmap.Height; y++)
            {
                CreatePaletteData(ref iPoint, (tempBitmap.Height - y) / ((float)tempBitmap.Height), ref PixelValues);
                //DrawColor(tempBitmap, y,(tempBitmap.Height - y) / ((float)tempBitmap.Height));
            }

            System.Runtime.InteropServices.Marshal.Copy(PixelValues, 0, iPtr, iBytes);
            tempBitmap.UnlockBits(bmpData);


            ///讲图片缩放到控件大小
            _imgForBuffer = new Bitmap(this.Width, this.Height);
            Graphics gImg = Graphics.FromImage(_imgForBuffer);
            gImg.DrawImage(tempBitmap, 0, 0, _imgForBuffer.Width, _imgForBuffer.Height);
            gImg.Dispose();

            base.OnCreateControl();
        }
        /// <summary>
        /// FGY:重新写生成色板函数
        /// </summary>
        /// <param name="para_depth">深度</param>
        /// <param name="para_PixelValues">真实的数据</param>

        private void CreatePaletteData(ref int iPoint,float para_depth, ref Byte[] para_PixelValues)
        {
            int nr = 255;
            int ng = 0;
            int nb = 0;
            for (; ng <= ColorMax; ng++)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            ng = ColorMax;

            for (; nr >= 0; nr--)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            nr = 0;

            for (; nb <= ColorMax; nb++)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            nb = ColorMax;

            for (; ng >= 0; ng--)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            ng = 0;
            for (; nr <= ColorMax; nr++)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            nr = ColorMax;

            for (; nb >= 0; nb--)
            {
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nr, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(ng, para_depth, BottomColor));
                para_PixelValues[iPoint++] = Convert.ToByte(CommonUtility.AddDepth(nb, para_depth, BottomColor));
            }
            nb = 0;

        }
        /// <summary>
        /// 绘制一行
        /// </summary>
        void DrawColor(Bitmap bitmap, int y,float depth)
        {
               
            int nr = 255;
            int ng = 0;
            int nb = 0;

            int x = 0;
            for (; ng <= ColorMax; ng++)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            ng = ColorMax;

            for (; nr >= 0; nr--)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            nr = 0;

            for (; nb <= ColorMax; nb++)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            nb = ColorMax;

            for (; ng >= 0; ng--)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            ng = 0;

            for (; nr <= ColorMax; nr++)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            nr = ColorMax;

            for (; nb >= 0; nb--)
            {
                bitmap.SetPixel(x, y, Color.FromArgb(CommonUtility.AddDepth(nr, depth,BottomColor), CommonUtility.AddDepth(ng, depth,BottomColor), CommonUtility.AddDepth(nb, depth,BottomColor)));
                x++;
            }
            nb = 0;
        }

        Pen penFixedOne = Pens.White;
        Pen penFixedTwo = Pens.Black;
        const int SignLineWidth = 3;
        const int SignLineOffset = 2;

        void DrawFixedSign(Graphics g)
        {
            DrawCross(g, _selectedLocation, penFixedOne);
            DrawCross(g, new Point(_selectedLocation.X + 1, _selectedLocation.Y + 1), penFixedTwo);
        }
        /// <summary>
        /// 画十字
        /// </summary>
        void DrawCross(Graphics g, Point pt, Pen pen)
        {
            g.DrawLine(pen, pt.X - SignLineOffset, pt.Y,
                pt.X - SignLineOffset - SignLineWidth, pt.Y);
            g.DrawLine(pen, pt.X, pt.Y - SignLineOffset,
                pt.X, pt.Y - SignLineOffset - SignLineWidth);
            g.DrawLine(pen, pt.X + SignLineOffset, pt.Y,
                pt.X + SignLineOffset + SignLineWidth, pt.Y);
            g.DrawLine(pen, pt.X, pt.Y + SignLineOffset,
                pt.X, pt.Y + SignLineOffset + SignLineWidth);
        }
        /// <summary>
        /// 用反转屏幕色画十字
        /// </summary>
        /// <param name="pt"></param>
        void DrawCrossReversible(Point pt)
        {
            DrawCrossReversibleCore(pt,Color.White);
            pt.Offset(1, 1);
            DrawCrossReversibleCore(pt,Color.Green);
        }
        void DrawCrossReversibleCore(Point pt,Color backColor)
        {
            ControlPaint.DrawReversibleLine(new Point(pt.X - SignLineOffset, pt.Y),
                new Point(pt.X - SignLineOffset - SignLineWidth, pt.Y), backColor);
            ControlPaint.DrawReversibleLine(new Point(pt.X, pt.Y - SignLineOffset),
                new Point(pt.X, pt.Y - SignLineOffset - SignLineWidth), backColor);
            ControlPaint.DrawReversibleLine(new Point(pt.X + SignLineOffset, pt.Y),
                new Point(pt.X + SignLineOffset + SignLineWidth, pt.Y), backColor);
            ControlPaint.DrawReversibleLine(new Point(pt.X, pt.Y + SignLineOffset),
                new Point(pt.X, pt.Y + SignLineOffset + SignLineWidth), backColor);
        }

        bool _moving = false;
        Point _prevMouseLocation;
        void StartMove()
        {
            _moving = true;

            ///反转光标
            Point temp = Control.MousePosition;
            DrawCrossReversible(temp);
            _prevMouseLocation = temp;

            Cursor.Hide();
            Cursor.Clip = this.RectangleToScreen(this.ClientRectangle);
        }

        void MoveColor(Point clientPoint)
        {
            if (_moving)
            {
                ///若是没有发生Paint事件，则先清除上一次的反转
                if (!_movingTimePaint)
                {
                    DrawCrossReversible(_prevMouseLocation);
                }

                ///反转光标
                Point temp = Control.MousePosition;
                DrawCrossReversible(temp);
                _movingTimePaint = false;
                _prevMouseLocation = temp;

                ///不允许clientPoint超出范围
                if (clientPoint.X < 0)
                {
                    clientPoint.X = 0;
                }
                else if (clientPoint.X > this.Width - 1)
                {
                    clientPoint.X = this.Width - 1;
                }

                if (clientPoint.Y < 0)
                {
                    clientPoint.Y = 0;
                }
                else if (clientPoint.Y > this.Height - 1)
                {
                    clientPoint.Y = this.Height - 1;
                }

                ///改变OwnerColorSelctor的TempValue值
                _colorLocation = clientPoint;
                OwnerColorSelector.BaseValue = _imgForBuffer.GetPixel(_colorLocation.X, _colorLocation.Y);
            }
        }

        void EndMove()
        {
            if (!_moving)
            {
                return;
            }
            _moving = false;

            OwnerColorSelector.Value = OwnerColorSelector.TempValue;

            ///取消反转
            DrawCrossReversible(_prevMouseLocation);

            Cursor.Show();
            Cursor.Clip = Rectangle.Empty;

            ///改变_selectedLocation
            _selectedLocation = this.PointToClient(Control.MousePosition);

            ///重画一次界面
            Graphics g = CreateGraphics();
            RedrawAll(g);
            g.Dispose();
        }

        void RedrawAll(Graphics g)
        {
            g.DrawImage(_imgForBuffer, new Rectangle(0, 0, this.Width, this.Height));
            DrawFixedSign(g);
        }

        Bitmap _imgForBuffer;
        bool _movingTimePaint = false;
        protected override void OnPaint(PaintEventArgs pe)
        {
            ///移动时若是发生Paint事件需要记录
            if (_moving)
            {
                _movingTimePaint = true;
            }

            Graphics g = pe.Graphics;

            RedrawAll(g);

            base.OnPaint(pe);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartMove();
                MoveColor(e.Location);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveColor(e.Location);
            }

            base.OnMouseMove(e);
        }

        protected override void OnLostCapture(EventArgs e)
        {
            EndMove();

            base.OnLostCapture(e);
        }
    }
}
