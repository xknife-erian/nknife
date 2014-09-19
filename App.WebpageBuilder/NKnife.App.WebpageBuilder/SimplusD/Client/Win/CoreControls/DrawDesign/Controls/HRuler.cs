using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 横向标尺
    /// </summary>
    public partial class HRuler : Control
    {
        #region 字段成员定义

        const float InchUint = 96F; //1英寸=96像素
        const float CMUint = 15.49F; // 1CM = 15.49像素
        const float MMUint = 3.78F; // 1MM = 3.78像素
        
        private EnumRulerScaleStyle _rulerStyle = EnumRulerScaleStyle.Pixel;
        private float _curZoom = 1;//当前缩放比例

        #endregion

        #region 属性成员定义

        /// <summary>
        /// 模板设计器对象
        /// </summary>
        internal DesignPanel TDPanel { get; set; }

        /// <summary>
        /// 标尺的刻度风格
        /// </summary>
        public EnumRulerScaleStyle RulerScaleStyle
        {
            get { return _rulerStyle; }
            set
            {
                _rulerStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 纵向标尺
        /// </summary>
        public VRuler VRuler
        {
            get { return TDPanel.VRuler; }
        }

        /// <summary>
        /// 零开始的位置,标尺顶端
        /// </summary>
        internal float StartPos { get; set; }

        /// <summary>
        /// 鼠标位置，用于绘画标尺指示线
        /// </summary>
        internal int CurPos { get; set; }

        /// <summary>
        /// 当前缩放比例
        /// </summary>
        public float CurZoom
        {
            get { return _curZoom; }
            set
            {
                _curZoom = value;
                Invalidate();
            }
        }
        #endregion

        #region 构造函数

        public HRuler(DesignPanel tD)
        {
            TDPanel = tD;
            StartPos = 0F;
            CurPos = 0;
            InitializeComponent();
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            // TODO: 在此处添加自定义绘制代码
            DrawRuler();
            g.Dispose();
            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 绘画标尺
        /// </summary>
        private void DrawRuler()
        {
            Graphics g = CreateGraphics();

            switch (RulerScaleStyle)
            {
                case EnumRulerScaleStyle.Pixel:
                    DrawRulerPixel();
                    break;
                case EnumRulerScaleStyle.Inch:
                    DrawRulerInch();
                    break;
                case EnumRulerScaleStyle.CM:
                    DrawRulerCM();
                    break;
                case EnumRulerScaleStyle.MM:
                    DrawRulerMM();
                    break;
                case EnumRulerScaleStyle.Percent:
                    DrawRulerPercent();
                    break;
                default:
                    break;
            }
            g.DrawLine(Pens.Black, 0, Height - 1, Width, Height - 1);
            g.Dispose();
        }

        /// <summary>
        /// 绘画标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerLine(Graphics g)
        {
            switch (RulerScaleStyle)
            {
                case EnumRulerScaleStyle.Pixel:
                    Pen penHide = new Pen(this.BackColor, 1);
                    g.DrawLine(penHide, StartPos + CurPos, 0, StartPos + CurPos, Height - 1);
                    penHide.Dispose();

                    DrawRulerPixelLine(g);
                    break;
                case EnumRulerScaleStyle.Inch:
                    DrawRulerInchLine(g);
                    break;
                case EnumRulerScaleStyle.CM:
                    DrawRulerCMLine(g);
                    break;
                case EnumRulerScaleStyle.MM:
                    DrawRulerMMLine(g);
                    break;
                case EnumRulerScaleStyle.Percent:
                    DrawRulerPercentLine(g);
                    break;
                default:
                    break;
            }
            g.DrawLine(Pens.Black, 0, Height - 1, Width, Height - 1);
        }

        /// <summary>
        /// 绘画像素标尺指示线(用于替代Invaliate,减少刷新,减少标尺闪动)
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerPixelLine(Graphics g)
        {
            if (CurZoom == 1)
            {
                float offset = Height;
                int pos = (int)StartPos + CurPos;
                if (CurPos % 50 == 0)
                {
                    offset = 0;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }
                else if (CurPos % 25 == 0)
                {
                    offset = (Height * 3) / 5;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }
                else if (CurPos % 5 == 0)
                {
                    offset = (Height * 3) / 4;
                    g.DrawLine(Pens.Black, pos, offset, pos, Height);
                }

                if (CurPos % 50 < 20)
                {
                    int intpos = (CurPos / 50) * 50;
                    offset = 0;
                    g.DrawString(intpos.ToString(),
                        new Font("Tahoma", 7),
                        new SolidBrush(Color.Black),
                        intpos + +(int)StartPos,
                        0);
                }
            }
            else
            {
                CommonFuns.Invalidate(this, 0, (int)StartPos + CurPos, 0, 1, Height - 1);
            }
        }

        /// <summary>
        /// 绘画标尺指示线:毫米的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerMMLine(Graphics g)
        {
        }

        /// <summary>
        /// 绘画标尺指示线:厘米的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerCMLine(Graphics g)
        {
        }

        /// <summary>
        /// 绘画标尺指示线:英寸的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerInchLine(Graphics g)
        {
        }

        /// <summary>
        /// 绘画标尺指示线:百分比的标尺先用刷新来实现,不过用的是局部刷新
        /// </summary>
        /// <param name="g"></param>
        private void DrawRulerPercentLine(Graphics g)
        {
            CommonFuns.Invalidate(this, 0, (int)StartPos + CurPos, 0, 1, Height - 1);
        }

        /// <summary>
        /// 绘画像素标尺
        /// </summary>
        private void DrawRulerPixel()
        {
            Graphics g = CreateGraphics();
            int scaleStep;//第个最小刻度代表的像素
            float distanceStep;//刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int)(5 / CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep * CurZoom;

            int num = 0;
            for (
                float x = this.StartPos;
                x < this.Width;
                x += distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
                float x = this.StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
        /// 绘画英寸标尺
        /// </summary>
        private void DrawRulerInch()
        {
            Graphics g = CreateGraphics();
            int scaleStep;//第个最小刻度代表的像素
            float distanceStep;//刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int)(5 / CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep * CurZoom * InchUint;

            int num = 0;
            for (
                float x = this.StartPos;
                x < this.Width;
                x += distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
                float x = this.StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
        /// 绘画毫米标尺
        /// </summary>
        private void DrawRulerMM()
        {
            Graphics g = CreateGraphics();
            int scaleStep;//第个最小刻度代表的像素
            float distanceStep;//刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int)(5 / CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep * CurZoom * MMUint;

            int num = 0;
            for (
                float x = this.StartPos;
                x < this.Width;
                x += distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
                float x = this.StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
        /// 绘画厘米标尺
        /// </summary>
        private void DrawRulerCM()
        {
            Graphics g = CreateGraphics();
            int scaleStep;//第个最小刻度代表的像素
            float distanceStep;//刻度间的距离
            if (CurZoom <= 5)
            {
                scaleStep = (int)(5 / CurZoom);
            }
            else
            {
                scaleStep = 1;
            }
            distanceStep = scaleStep * CurZoom * CMUint;

            int num = 0;
            for (
                float x = this.StartPos;
                x < this.Width;
                x += distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
                float x = this.StartPos;
                x >= 0;
                x -= distanceStep
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
                {
                    offset = 0;
                    g.DrawString((scaleStep * num).ToString(),
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
        /// 绘画百分比标尺
        /// </summary>
        private void DrawRulerPercent()
        {
            Graphics g = CreateGraphics();
            float step = (float)TDPanel.DrawPanel.Width / 100;
            int num = 0;
            for (
                float x = this.StartPos;
                x < this.Width;
                x += step
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
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
                float x = this.StartPos;
                x > 0;
                x -= step
                )
            {
                float offset = (Height * 3) / 4;
                if (num % 5 == 0)
                {
                    offset = (Height * 3) / 5;
                }

                if (num % 10 == 0)
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

        #endregion

        #region 事件响应

        /// <summary>
        /// 响应DrawFrame的大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawFrame_SizeChanged(object sender, EventArgs e)
        {
            this.BringToFront();
            if (TDPanel.DrawFrame.Width >=
                TDPanel.DrawPanel.Width + 2 * TDPanel.DrawFrame.MinSpace +
                (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                this.Width = TDPanel.DrawFrame.Width + 2 * DesignPanel.RulerSize;
                this.StartPos = TDPanel.DrawPanel.Location.X;
                this.Location = new Point(0, 0);
            }
            else
            {
                this.Width = TDPanel.DrawPanel.Width + 2 * TDPanel.DrawFrame.MinSpace +
                              (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize);
                this.Location = new Point(TDPanel.DrawPanel.Location.X -
                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize), 0);
                this.StartPos = TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize;
            }
            Invalidate();
        }

        /// <summary>
        /// 响应拖动DrawFrame的滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawFrame_ScrollValueChanged(object sender, EventArgs e)
        {
            // this.BringToFront();
            if (TDPanel.DrawFrame.Width <
                TDPanel.DrawPanel.Width + 2 * TDPanel.DrawFrame.MinSpace +
                (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
            //当工作区比较小:即出现了纵滚动条时
            {
                this.Location = new Point(TDPanel.DrawPanel.Location.X -
                                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize), 0);
            }
        }

        /// <summary>
        /// 因鼠标的移动或点击产生的重画标尺的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawPanel_MouseRulerEvent(object sender, MouseEventArgs e)
        {

            if (e.X != CurPos)
            {
                Graphics g = CreateGraphics();

                DrawRulerLine(g);
                //CommonFuns.Invalidate(this, 0, (int)StartPos + _curPos, 0, 1, Height - 1);
                CurPos = e.X;
                //CommonFuns.Invalidate(this, 1, (int)StartPos + _curPos, 0, 1, Height - 2);
                Pen penDash = new Pen(Color.Black, 1);
                penDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawLine(penDash, StartPos + CurPos, 0, StartPos + CurPos, Height - 1);

                g.Dispose();
            }
        }

        /// <summary>
        /// 缩小或放大事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DrawPanel_ChangeZoomEvent(object sender, EventArgs e)
        {
            //    this.BringToFront();
            if (TDPanel.DrawFrame.Width >=
               TDPanel.DrawPanel.Width + 2 * TDPanel.DrawFrame.MinSpace +
               (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                this.Width = TDPanel.DrawFrame.Width + 2 * DesignPanel.RulerSize;
                this.StartPos = TDPanel.DrawPanel.Location.X;
                this.Location = new Point(0, 0);
            }
            else
            {
                this.Width = TDPanel.DrawPanel.Width + 2 * TDPanel.DrawFrame.MinSpace +
                              (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize);
                this.Location = new Point(TDPanel.DrawPanel.Location.X -
                    (TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize), 0);
                this.StartPos = TDPanel.DrawFrame.MinSpace + DesignPanel.RulerSize;
            }
            CurZoom = TDPanel.DrawPanel.CurZoom;
        }

        #endregion
    }
}
