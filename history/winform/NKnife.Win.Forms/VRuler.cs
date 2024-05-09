﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NKnife.Win.Forms.Common;

namespace NKnife.Win.Forms
{
    /// <summary>
    ///     纵向标尺
    /// </summary>
    public partial class VRuler : Control
    {
        #region 字段成员定义

        private const float INCH_UINT = 96F; //1英寸=96像素
        private const float CM_UINT = 15.49F; // 1CM = 15.49像素
        private const float MM_UINT = 3.78F; // 1MM = 3.78像素

        private float _CurZoom = 1; //当前缩放比例
        private RulerStyle _RulerStyle = RulerStyle.Pixel; //标尺刻度风格

        #endregion

        #region 属性成员定义

        /// <summary>
        ///     标尺刻度风格
        /// </summary>
        public RulerStyle RulerStyle
        {
            get { return _RulerStyle; }
            set
            {
                _RulerStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     零开始的位置
        /// </summary>
        internal float StartPos { get; set; }

        /// <summary>
        ///     当前坐标
        /// </summary>
        internal int CurPos { get; set; }

        /// <summary>
        ///     当前缩放比例
        /// </summary>
        private float CurZoom
        {
            get { return _CurZoom; }
            set
            {
                _CurZoom = value;
                Invalidate();
            }
        }

        #endregion

        #region 构造函数与初始化

        public VRuler()
        {
            //SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            StartPos = 0F;
            CurPos = 0;
            InitializeComponent();
        }

        #endregion

        #region 消息和事件响应

        /// <summary>
        ///     绘制控件
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawRuler();
            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        /// <summary>
        ///     绘画标尺
        /// </summary>
        protected void DrawRuler()
        {
            Graphics g = CreateGraphics();
            switch (RulerStyle)
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
                default:
                    break;
            }
            g.DrawLine(Pens.Black, Width - 1, 0, Width - 1, Height);
            g.Dispose();
        }

        /// <summary>
        ///     绘画像素标尺
        /// </summary>
        private void DrawRulerPixel()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int) (5/CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurZoom;

            int num = 0;
            for (float y = StartPos; y < Height; y += distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (scaleStep*num >= 1290)
                {
                    Console.WriteLine("scaleStep * num:" + (scaleStep*num));
                    Console.WriteLine("y:" + y);
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }

            num = 0;
            for (float y = StartPos; y >= 0; y -= distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
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
            if (CurZoom <= 5)
            {
                scaleStep = (int) (5/CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurZoom*INCH_UINT;

            int num = 0;
            for (float y = StartPos; y < Height; y += distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (scaleStep*num >= 1290)
                {
                    Console.WriteLine("scaleStep * num:" + (scaleStep*num));
                    Console.WriteLine("y:" + y);
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }

            num = 0;
            for (float y = StartPos; y >= 0; y -= distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }
            g.Dispose();
        }

        /// <summary>
        ///     绘画百分比标尺
        /// </summary>
        private void DrawRulerPercent()
        {
            Graphics g = CreateGraphics();
            int num = 0;
            float step = (float) Parent.Height/100;
            for (
                float y = StartPos;
                y < Height;
                y += step
                )
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = num.ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }

            num = 0;
            step = Parent.Height/100;
            for (
                float y = StartPos;
                y > 0;
                y -= step
                )
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = num.ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }
            g.Dispose();
        }

        /// <summary>
        ///     毫米标尺
        /// </summary>
        private void DrawRulerMM()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int) (5/CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep*CurZoom*MM_UINT;

            int num = 0;
            for (float y = StartPos; y < Height; y += distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (scaleStep*num >= 1290)
                {
                    Console.WriteLine("scaleStep * num:" + (scaleStep*num));
                    Console.WriteLine("y:" + y);
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }

            num = 0;
            for (float y = StartPos; y >= 0; y -= distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }
            g.Dispose();
        }

        /// <summary>
        ///     厘米标尺
        /// </summary>
        private void DrawRulerCM()
        {
            Graphics g = CreateGraphics();
            int scaleStep; //第个最小刻度代表的像素
            float distanceStep; //刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int) (5/CurZoom);
            }
            else
            {
                scaleStep = 10;
            }
            distanceStep = scaleStep*CurZoom*CM_UINT;

            int num = 0;
            for (float y = StartPos; y < Height; y += distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (scaleStep*num >= 1290)
                {
                    Console.WriteLine("scaleStep * num:" + (scaleStep*num));
                    Console.WriteLine("y:" + y);
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }

            num = 0;
            for (float y = StartPos; y >= 0; y -= distanceStep)
            {
                int offset = (Width*3)/4;
                if (num%5 == 0)
                {
                    offset = (Width*3)/5;
                }

                if (num%10 == 0)
                {
                    string str = (scaleStep*num).ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            y + i*8);
                        i++;
                    }
                }
                ++num;
                g.DrawLine(Pens.Black, offset, (int) y, Width, (int) y);
            }
            g.Dispose();
        }

        /// <summary>
        ///     绘画标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerLine(Graphics g)
        {
            switch (RulerStyle)
            {
                case RulerStyle.Pixel:
                    var penHide = new Pen(BackColor, 1);
                    g.DrawLine(penHide, 0, (int) StartPos + CurPos, Width - 1, (int) StartPos + CurPos);
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
                    penHide = new Pen(BackColor, 1);
                    g.DrawLine(penHide, 0, (int) StartPos + CurPos, Width - 1, (int) StartPos + CurPos);
                    penHide.Dispose();

                    DrawRulerPercentLine(g);
                    break;
                default:
                    break;
            }
            g.DrawLine(Pens.Black, Width - 1, 0, Width - 1, Height);
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
            //G.Invalidate(this, 0, 0, (int) StartPos + CurPos, Width - 1, 1);
        }

        /// <summary>
        ///     绘画像素标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerPixelLine(Graphics g)
        {
            if (CurZoom == 1)
            {
                float offset = Height;
                int pos = (int) StartPos + CurPos;
                if (CurPos%50 == 0)
                {
                    offset = 0;
                    g.DrawLine(Pens.Black, offset, pos, Width, pos);
                }
                else if (CurPos%25 == 0)
                {
                    offset = (Width*3)/5;
                    g.DrawLine(Pens.Black, offset, pos, Width, pos);
                }
                else if (CurPos%5 == 0)
                {
                    offset = (Width*3)/4;
                    g.DrawLine(Pens.Black, offset, pos, Width, pos);
                }

                if (CurPos%50 < 25)
                {
                    int intPos = (CurPos/50)*50;
                    string str = intPos.ToString();
                    offset = 0;
                    int i = 0;
                    foreach (char ch in str)
                    {
                        g.DrawString(ch.ToString(),
                            new Font("Tahoma", 7),
                            new SolidBrush(Color.Black),
                            0,
                            intPos + (int) StartPos + i*8);
                        i++;
                    }
                }
            }
            else
            {
                //G.Invalidate(this, 0, 0, (int) StartPos + CurPos, Width - 1, 1);
            }
        }

        /// <summary>
        ///     响应DrawFrame的大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawFrame_SizeChanged(object sender, EventArgs e)
        {
            BringToFront();
//            if (TDPanel.DrawFrame.Height >=
//                TDPanel.DrawPanel.Height + 2 * TDPanel.DrawFrame.MinSpace +
//                (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
//            {
//                this.Height = TDPanel.DrawFrame.Height + 2 * DesignPanel.RulerSize;
//                this.StartPos = TDPanel.DrawPanel.Location.Y;
//                this.Location = new Point(0, 0);
//            }
//            else
//            {
//                this.Height = TDPanel.DrawPanel.Height + 2 * TDPanel.DrawFrame.MinSpace +
//                              (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize);
//                this.Location = new Point(0,
//                    TDPanel.DrawPanel.Location.Y -
//                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize));
//                this.StartPos = TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize;
//            }
            Invalidate();
        }

        /// <summary>
        ///     响应拖动DrawFrame的滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawFrame_ScrollValueChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("DrawFrame_ScrollValueChanged");
            //this.BringToFront();
//            if (TDPanel.DrawFrame.Height <
//                TDPanel.DrawPanel.Height + 2 * TDPanel.DrawFrame.MinSpace +
//                (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
//            //当工作区比较小:即出现了纵滚动条时
//            {
//                this.Location = new Point(0,
//                    TDPanel.DrawPanel.Location.Y -
//                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize));
//            }
        }

        /// <summary>
        ///     因鼠标的移动或点击产生的重画标尺的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawPanel_MouseRulerEvent(object sender, MouseEventArgs e)
        {
            if (e.Y != CurPos)
            {
                Graphics g = CreateGraphics();
                //绘画指示线,先隐藏之前的线,绘画被擦除的原本在标尺上的线
                DrawRulerLine(g);
                CurPos = e.Y;
                //再绘画现在的线
                var penDash = new Pen(Color.Black, 1);
                penDash.DashStyle = DashStyle.Dot;
                g.DrawLine(penDash, 0, (int) StartPos + CurPos, Width - 1, (int) StartPos + CurPos);
                penDash.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        ///     缩小或放大事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DrawPanel_ChangeZoomEvent(object sender, EventArgs e)
        {
            //   this.BringToFront();
//            if (TDPanel.DrawFrame.Height >=
//                TDPanel.DrawPanel.Height + 2 * TDPanel.DrawFrame.MinSpace +
//                (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
//            //当工作区比较大时
//            {
//                this.Height = TDPanel.DrawFrame.Height + 2 * DesignPanel.RulerSize;
//                this.StartPos = TDPanel.DrawPanel.Location.Y;
//                this.Location = new Point(0, 0);
//            }
//            else
//            {
//                this.Height = TDPanel.DrawPanel.Height + 2 * TDPanel.DrawFrame.MinSpace +
//                              (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize);
//                this.Location = new Point(0,
//                    TDPanel.DrawPanel.Location.Y -
//                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize));
//                this.StartPos = TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize;
//            }
//            CurZoom = TDPanel.DrawPanel.CurZoom;
            Invalidate();
        }

        #endregion
    }
}