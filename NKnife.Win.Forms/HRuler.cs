﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Win.Forms.Common;

namespace NKnife.Win.Forms
{
    /// <summary>
    ///     横向标尺
    /// </summary>
    public partial class HRuler : Control
    {
        private const float INCH_UINT = 96F; //1英寸=96像素
        private const float CM_UINT = 15.49F; // 1CM = 15.49像素
        private const float MM_UINT = 3.78F; // 1MM = 3.78像素

        private float _CurrentZoom = 1; //当前缩放比例
        private RulerStyle _RulerStyle = RulerStyle.Pixel;

        public HRuler()
        {
            StartPos = 0F;
            CurrentPosition = 0;
            InitializeComponent();
        }

        /// <summary>
        ///     模板设计器对象
        /// </summary>
        /// <summary>
        ///     标尺的刻度风格
        /// </summary>
        public RulerStyle RulerScaleStyle
        {
            get { return _RulerStyle; }
            set
            {
                _RulerStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     零开始的位置,标尺顶端
        /// </summary>
        internal float StartPos { get; set; }

        /// <summary>
        ///     鼠标位置，用于绘画标尺指示线
        /// </summary>
        internal int CurrentPosition { get; set; }

        /// <summary>
        ///     当前缩放比例
        /// </summary>
        public float CurrentZoom
        {
            get { return _CurrentZoom; }
            set
            {
                _CurrentZoom = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     绘制控件
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            DrawRuler();
            g.Dispose();
            base.OnPaint(pe);
        }

        /// <summary>
        ///     绘画标尺
        /// </summary>
        private void DrawRuler()
        {
            Graphics g = CreateGraphics();
            switch (RulerScaleStyle)
            {
                case RulerStyle.Pixel:
                    DrawRulerPixel();
                    break;
                case RulerStyle.Inch:
                    DrawRulerInch();
                    break;
                case RulerStyle.CM:
                    DrawRulerCM();
                    break;
                case RulerStyle.MM:
                    DrawRulerMM();
                    break;
                case RulerStyle.Percent:
                    DrawRulerPercent();
                    break;
            }
            g.DrawLine(Pens.Black, 0, Height - 1, Width, Height - 1);
            g.Dispose();
        }

        /// <summary>
        ///     绘画标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerLine(Graphics g)
        {
            switch (RulerScaleStyle)
            {
                case RulerStyle.Pixel:
                    var penHide = new Pen(BackColor, 1);
                    g.DrawLine(penHide, StartPos + CurrentPosition, 0, StartPos + CurrentPosition, Height - 1);
                    penHide.Dispose();

                    DrawRulerPixelLine(g);
                    break;
                case RulerStyle.Inch:
                    DrawRulerInchLine(g);
                    break;
                case RulerStyle.CM:
                    DrawRulerCMLine(g);
                    break;
                case RulerStyle.MM:
                    DrawRulerMMLine(g);
                    break;
                case RulerStyle.Percent:
                    DrawRulerPercentLine(g);
                    break;
            }
            g.DrawLine(Pens.Black, 0, Height - 1, Width, Height - 1);
        }

        /// <summary>
        ///     绘画像素标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerPixelLine(Graphics g)
        {
            if (CurrentZoom == 1)
            {
                float offset = Height;
                int pos = (int) StartPos + CurrentPosition;
                if (CurrentPosition%50 == 0)
                {
                    offset = 0;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }
                else if (CurrentPosition%25 == 0)
                {
                    offset = (Height*3)/5;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }
                else if (CurrentPosition%5 == 0)
                {
                    offset = (Height*3)/4;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }

                if (CurrentPosition%50 < 20)
                {
                    int intpos = (CurrentPosition/50)*50;
                    g.DrawString(intpos.ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        intpos + +(int) StartPos,
                        0);
                }
            }
            else
            {
                //G.Invalidate(this, 0, (int) StartPos + CurrentPosition, 0, 1, Height - 1);
            }
        }

        /// <summary>
        ///     绘画标尺指示线:毫米的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerMMLine(Graphics g)
        {
        }

        /// <summary>
        ///     绘画标尺指示线:厘米的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerCMLine(Graphics g)
        {
        }

        /// <summary>
        ///     绘画标尺指示线:英寸的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerInchLine(Graphics g)
        {
        }

        /// <summary>
        ///     绘画标尺指示线:百分比的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerPercentLine(Graphics g)
        {
            //G.Invalidate(this, 0, (int) StartPos + CurrentPosition, 0, 1, Height - 1);
        }

        /// <summary>
        ///     绘画像素标尺
        /// </summary>
        private void DrawRulerPixel()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurrentZoom <= 5)
            {
                scaleStep = (int) (5/CurrentZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurrentZoom;

            int num = 0;
            for (
                float x = StartPos;
                x < Width;
                x += distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            num = 0;
            for (
                float x = StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            g.Dispose();
        }

        /// <summary>
        ///     绘画英寸标尺
        /// </summary>
        private void DrawRulerInch()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurrentZoom <= 5)
            {
                scaleStep = (int) (5/CurrentZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurrentZoom*INCH_UINT;

            int num = 0;
            for (
                float x = StartPos;
                x < Width;
                x += distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            num = 0;
            for (
                float x = StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            g.Dispose();
        }

        /// <summary>
        ///     绘画毫米标尺
        /// </summary>
        private void DrawRulerMM()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurrentZoom <= 5)
            {
                scaleStep = (int) (5/CurrentZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurrentZoom*MM_UINT;

            int num = 0;
            for (
                float x = StartPos;
                x < Width;
                x += distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            num = 0;
            for (
                float x = StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            g.Dispose();
        }

        /// <summary>
        ///     绘画厘米标尺
        /// </summary>
        private void DrawRulerCM()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurrentZoom <= 5)
            {
                scaleStep = (int) (5/CurrentZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurrentZoom*CM_UINT;

            int num = 0;
            for (
                float x = StartPos;
                x < Width;
                x += distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            num = 0;
            for (
                float x = StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep*num).ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }

            g.Dispose();
        }

        /// <summary>
        ///     绘画百分比标尺
        /// </summary>
        private void DrawRulerPercent()
        {
            Graphics g = CreateGraphics();
            float step = (float) Parent.Width/100;
            int num = 0;
            for (
                float x = StartPos;
                x < Width;
                x += step
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString(num.ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }
            num = 0;
            for (
                float x = StartPos;
                x > 0;
                x -= step
                )
            {
                float offset = (Height*3)/4;
                if (num%5 == 0)
                {
                    offset = (Height*3)/5;
                }

                if (num%10 == 0)
                {
                    offset = 0;
                    g.DrawString(num.ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        x,
                        0);
                }
                ++num;
                g.DrawLine(Pens.Black, x, offset, x, Height);
            }
            g.Dispose();
        }

        /// <summary>
        ///     响应DrawFrame的大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawFrame_SizeChanged(object sender, EventArgs e)
        {
 //           BringToFront();
//            if (Parent.Width >=
//                Parent.Width + 2 + //* Parent.DrawFrame.MinSpace +
//                (Parent.Width + DesignPanel.RulerSize))
//            {
//                Width = Parent.Width + 2*DesignPanel.RulerSize;
//                StartPos = Parent.Location.X;
//                Location = new Point(0, 0);
//            }
//            else
//            {
//                Width = Parent.Width + //2 * Parent.MinSpace +
//                        (Parent.Width + DesignPanel.RulerSize);
//                Location = new Point(Parent.Location.X -
//                                     (DesignPanel.RulerSize), 0);
//                StartPos = DesignPanel.RulerSize;
//            }
//            Invalidate();
        }

        /// <summary>
        ///     响应拖动DrawFrame的滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawFrame_ScrollValueChanged(object sender, EventArgs e)
        {
            // this.BringToFront();
//            if (Parent.Width <
//                Parent.Width + //2 * Parent.MinSpace +
//                (Parent.Width + DesignPanel.RulerSize))
//                //当工作区比较小:即出现了纵滚动条时
//            {
//                Location = new Point(Parent.Location.X -
//                                     (DesignPanel.RulerSize), 0);
//            }
        }

        /// <summary>
        ///     因鼠标的移动或点击产生的重画标尺的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawPanel_MouseRulerEvent(object sender, MouseEventArgs e)
        {
            if (e.X != CurrentPosition)
            {
                Graphics g = CreateGraphics();

                DrawRulerLine(g);
                //CommonFuns.Invalidate(this, 0, (int)StartPos + _curPos, 0, 1, Height - 1);
                CurrentPosition = e.X;
                //CommonFuns.Invalidate(this, 1, (int)StartPos + _curPos, 0, 1, Height - 2);
                var penDash = new Pen(Color.Black, 1);
                penDash.DashStyle = DashStyle.Dot;
                g.DrawLine(penDash, StartPos + CurrentPosition, 0, StartPos + CurrentPosition, Height - 1);

                g.Dispose();
            }
        }

        /// <summary>
        ///     缩小或放大事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawPanel_ChangeZoomEvent(object sender, EventArgs e)
        {
            //    this.BringToFront();
//            if (Parent.Width >=
//                Parent.Width + //2 * Parent.MinSpace +
//                (Parent.Width + DesignPanel.RulerSize))
//            {
//                Width = Parent.Width + 2*DesignPanel.RulerSize;
//                StartPos = Parent.Location.X;
//                Location = new Point(0, 0);
//            }
//            else
//            {
//                Width = Parent.Width + //2 * Parent.MinSpace +
//                        (Parent.Width + DesignPanel.RulerSize);
//                Location = new Point(Parent.Location.X -
//                                     (DesignPanel.RulerSize), 0);
//                StartPos = DesignPanel.RulerSize;
//            }
            //CurZoom = Parent.CurZoom;
        }
    }
}