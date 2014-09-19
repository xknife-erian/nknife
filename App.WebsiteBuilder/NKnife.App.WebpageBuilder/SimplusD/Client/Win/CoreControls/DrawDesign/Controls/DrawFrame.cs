using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class DrawFrame : UserControl
    {
        #region 字段成员定义

        private DesignPanel _tDPanel;//运行时初始化
        private DrawPanel _drawPanel;//里面含有的画板.
       
        private int _minSpace = 25;//最小的空白距离
        private Point _lastLocation = new Point();// DrawPanel的上一次的位置
        //private DrawToolsBox _drawToolsBox;//运行时初始化

        Pen myBlack = new Pen(Color.FromArgb(64, 64, 64), 2.5F);
        Point point1_1 = new Point();
        Point point1_2 = new Point();
        Point point2_1 = new Point();
        Point point2_2 = new Point();
        #endregion

        #region 属性成员定义

        //public DrawToolsBox DrawToolsBox
        //{
        //    get { return _drawToolsBox; }
        //}
        /// <summary>
        /// 里面含有的画板.
        /// </summary>
        public DrawPanel DrawPanel
        {
            get { return _drawPanel; }
            set { _drawPanel = value; }
        }

        /// <summary>
        /// 设计器
        /// </summary>
        public DesignPanel TDPanel
        {
            get { return _tDPanel; }
            set { _tDPanel = value; }
        }

        /// <summary>
        /// 最小的空白距离
        /// </summary>
        public int MinSpace
        {
            get { return _minSpace; }
            set { _minSpace = value; }
        }

        /// <summary>
        /// DrawPanel的上一次的位置
        /// </summary>
        public Point LastLocation
        {
            get { return _lastLocation; }
            set { _lastLocation = value; }
        }

        /// <summary>
        /// 当前放大缩小倍数
        /// </summary>
        public float CurZoom
        {
            get { return DrawPanel.CurZoom; }
            set
            {
                DrawPanel.CurZoom = value;
            }
        }

        /// <summary>
        /// DrawFrame的纵向滚动条
        /// </summary>
        public VScrollBar DfVScrollBar
        {
            get { return _dfVScrollBar; }
            set { _dfVScrollBar = value; }
        }

        /// <summary>
        /// DrawFrame的横向滚动条
        /// </summary>
        public HScrollBar DfHScrollBar
        {
            get { return _dfHScrollBar; }
            set { _dfHScrollBar = value; }
        }

        #endregion

        #region 构造函数

        public DrawFrame(DesignPanel tD, int width, int height,Image backImage)
        {
           
            TDPanel = tD;
            
            this.SetStyle(ControlStyles.Selectable, true);

            InitializeComponent();

            this.BackColor = Color.FromArgb(192, 192, 192);
            this.SetStyle(ControlStyles.Selectable, true);
            this.AutoScroll = false;
            this.Cursor = Cursors.Arrow;

            DrawPanel = CreateDrawPanel(tD, width, height,backImage);
            DrawPanel.TabIndex = 0;
            DrawPanel.Select();
            DrawPanel.BackColor = Color.White;
            
            this.Controls.Add(DrawPanel);
            this.SetLocation();

            //事件初始化
            
            this.SizeChanged += new EventHandler(DrawFrame_SizeChanged);
            this.DfVScrollBar.ValueChanged += new EventHandler(DfVScrollBar_ValueChanged);
            this.DfHScrollBar.ValueChanged += new EventHandler(DfHScrollBar_ValueChanged);
            this.DfVScrollBar.SmallChange = 10;
            this.DfHScrollBar.SmallChange = 10;
        }
        
        /// <summary>
        /// 设置左上角坐标
        /// </summary>
        private void SetLocation()
        {
            LastLocation = DrawPanel.Location;
            int x;
            int y;
            if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                x = (TDPanel.DrawFrame.Width - DrawPanel.Width) / 2;
            }
            else
            {
                x = MinSpace + DesignPanel.RulerSize;
            }
            if (this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                y = (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2;
            }
            else
            {
                y = MinSpace + DesignPanel.RulerSize;
            }
            DrawPanel.Location = new Point(x, y);
        }

        /// <summary>
        /// 鼠标滚动响应
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (DfVScrollBar.Enabled)//如果纵标尺可用时
            {
                if (e.Delta > 0 && DfVScrollBar.Value > DfVScrollBar.SmallChange)
                    DfVScrollBar.Value -= DfVScrollBar.SmallChange;
                else if (e.Delta < 0 && DfVScrollBar.Value < DfVScrollBar.Maximum + 1 - DfVScrollBar.LargeChange - DfVScrollBar.SmallChange)
                {
                    DfVScrollBar.Value += DfVScrollBar.SmallChange;
                }
            }
            else if (DfHScrollBar.Enabled)
            {
                if (e.Delta > 0 && DfHScrollBar.Value > DfHScrollBar.SmallChange)
                    DfHScrollBar.Value -= DfHScrollBar.SmallChange;
                else if (e.Delta < 0 && DfHScrollBar.Value < DfHScrollBar.Maximum + 1 - DfHScrollBar.LargeChange - DfHScrollBar.SmallChange)
                {
                    DfHScrollBar.Value += DfHScrollBar.SmallChange;
                }
            }
        }

        /// <summary>
        /// 纵向滚动条值改变时的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DfHScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (DfHScrollBar.Enabled)
            {
                HideHatchLines();
                LastLocation = DrawPanel.Location;
                DrawPanel.Location = new Point(MinSpace + DesignPanel.RulerSize - DfHScrollBar.Value, DrawPanel.Location.Y);
                DrawHatchLines();
                //DrawPanel.ResetCurRect();
            }
        }

        /// <summary>
        /// 纵向滚动条值改变时的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DfVScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (DfVScrollBar.Enabled)
            {
                HideHatchLines();
                LastLocation = DrawPanel.Location;
                DrawPanel.Location = new Point(DrawPanel.Location.X, MinSpace + DesignPanel.RulerSize - DfVScrollBar.Value);
                DrawHatchLines();
                //DrawPanel.ResetCurRect();
            }
        }

        #endregion

        #region 公共函数成员接口

        #endregion

        #region 事件消息处理

        //List<KeyValuePair<Point, Point>> _hatchLines = new List<KeyValuePair<Point, Point>>();
     
        //Pen myBlack = new Pen(Color.FromArgb(64, 64, 64), 2.5F);
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码
            //HideHatchLines();
            //DrawHatchLines();
            //if (_hatchLines.Count == 2)
            //{
            //    foreach (KeyValuePair<Point,Point> line in _hatchLines)
            //    {
            //        pe.Graphics.DrawLine(myBlack, line.Key, line.Value);
            //    }
            //}

            //HideHatchLines();
            // 调用基类 OnPaint
            pe.Graphics.DrawLine(myBlack, point1_1, point1_2);
            pe.Graphics.DrawLine(myBlack, point2_1, point2_2);

            base.OnPaint(pe);
        }

        /// <summary>
        /// 绘画drawPanel的阴影效果
        /// </summary>
        public void DrawHatchLines()
        {
            //Graphics g = this.CreateGraphics();
            //Color myColor = Color.FromArgb(64, 64, 64);
            //Pen myBlack = new Pen(myColor, 2.5F);
            //////_hatchLines.Clear();
            ////////绘画右和下方的阴影效果
            //////KeyValuePair<Point, Point> _line1 = new KeyValuePair<Point, Point>(
            //////    new Point(DrawPanel.Location.X + DrawPanel.Width + 1, DrawPanel.Location.Y + 2), 
            //////   new Point(DrawPanel.Location.X + DrawPanel.Width + 1,DrawPanel.Location.Y + DrawPanel.Height + 2));

            //////_hatchLines.Add(_line1);

            //////KeyValuePair<Point, Point> _line2 = new KeyValuePair<Point, Point>(
            //////    new Point(DrawPanel.Location.X + 2,DrawPanel.Location.Y + DrawPanel.Height + 1),
            //////    new Point(DrawPanel.Location.X + DrawPanel.Width + 2, DrawPanel.Location.Y + DrawPanel.Height + 1));

            //////_hatchLines.Add(_line2);
            //g.DrawLine(myBlack,
            //   DrawPanel.Location.X + DrawPanel.Width + 1,
            //   DrawPanel.Location.Y + 2,
            //   DrawPanel.Location.X + DrawPanel.Width + 1,
            //   DrawPanel.Location.Y + DrawPanel.Height + 2);

            //g.DrawLine(myBlack,
            //   DrawPanel.Location.X + 2,
            //   DrawPanel.Location.Y + DrawPanel.Height + 1,
            //   DrawPanel.Location.X + DrawPanel.Width + 2,
            //   DrawPanel.Location.Y + DrawPanel.Height + 1);
            //myBlack.Dispose();

            //g.Dispose();

            point1_1 = new Point(DrawPanel.Location.X + DrawPanel.Width + 1,
               DrawPanel.Location.Y + 2);
            point1_2 = new Point(   DrawPanel.Location.X + DrawPanel.Width + 1,
               DrawPanel.Location.Y + DrawPanel.Height + 2);

            
            point2_1 = new Point(   DrawPanel.Location.X + 2,
               DrawPanel.Location.Y + DrawPanel.Height + 1);
            point2_2 = new Point(  DrawPanel.Location.X + DrawPanel.Width + 2,
               DrawPanel.Location.Y + DrawPanel.Height + 1);
            this.Invalidate();
            this.Update();
        }
        //int hatchX, hatchY, hatchW, hatchH;

        /// <summary>
        /// 隐藏绘画drawPanel的阴影效果
        /// </summary>
        public void HideHatchLines()
        {
            Graphics g = this.CreateGraphics();

            Pen hidePen = new Pen(this.BackColor, 2.5F);
            //绘画右和下方的阴影效果
            g.DrawLine(hidePen,
               LastLocation.X + DrawPanel.Width + 1,
               LastLocation.Y + 2,
               LastLocation.X + DrawPanel.Width + 1,
               LastLocation.Y + DrawPanel.Height + 2);

            g.DrawLine(hidePen,
               LastLocation.X + 2,
               LastLocation.Y + DrawPanel.Height + 1,
               LastLocation.X + DrawPanel.Width + 2,
               LastLocation.Y + DrawPanel.Height + 1);
            hidePen.Dispose();

            g.Dispose();
        }

        private void DrawFrame_SizeChanged(object sender, EventArgs e)
        {
            //HideHatchLines();
            SizeChange_DrawPanelLocation();
            //DrawHatchLines();
        }

        /// <summary>
        /// SizeChange时改变DrawPanel的loacation
        /// </summary>
        public void SizeChange_DrawPanelLocation()
        {
            LastLocation = DrawPanel.Location;
            if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//框架控件比较大时
            {
                DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
                    (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
                //设置scroll
                DfVScrollBar.Enabled = false;
             //   DfVScrollBar.Value = 0;
                
                DfHScrollBar.Enabled = false;
           //     DfHScrollBar.Value = 0;
                
            }
            else if (this.Width < DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//宽度比较小时
            {
                //if (DrawPanel.Location.X >= MinSpace + DesignPanel.RulerSize)//当前的位置x坐标大于一定要留言的最小距离.
                //否则不用修改其location
                {
                    DrawPanel.Location = new Point(MinSpace + DesignPanel.RulerSize,
                        (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
                    
                }
                //设置scroll
                DfVScrollBar.Enabled = false;
                

                DfHScrollBar.Enabled = true;
                DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                DfHScrollBar.LargeChange = this.Width - 2;

            }
            else if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height < DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                //if (this.LastLocation.Y >= MinSpace + DesignPanel.RulerSize)
                {
                    DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
                        MinSpace + DesignPanel.RulerSize);

                }
                //设置scroll
                DfHScrollBar.Enabled = false;
                


                DfVScrollBar.Enabled = true;
                DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                DfVScrollBar.LargeChange = this.Height - 2;
            }
            else
            {
                int newLocationX = LastLocation.X >= MinSpace + DesignPanel.RulerSize ?
                    MinSpace + DesignPanel.RulerSize : LastLocation.X;
                int newLocationY = LastLocation.Y > MinSpace + DesignPanel.RulerSize ?
                    MinSpace + DesignPanel.RulerSize : LastLocation.Y;
                DrawPanel.Location = new Point(newLocationX, newLocationY);
                DfHScrollBar.Value = LastLocation.X > MinSpace + DesignPanel.RulerSize ? 0 : DfHScrollBar.Value;
                DfVScrollBar.Value = LastLocation.Y > MinSpace + DesignPanel.RulerSize ? 0 : DfVScrollBar.Value;

                //设置scroll
                DfHScrollBar.Enabled = true;
                DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                if (this.Width > 2)
                {
                    DfHScrollBar.LargeChange = this.Width - 2;
                }


                DfVScrollBar.Enabled = true;
                DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                if (this.Height > 2)
                {
                    DfVScrollBar.LargeChange = this.Height - 2;
                }
            }
        //    LastLocation = new Point(DrawPanel.Location.X, DrawPanel.Location.Y);
        }

        /// <summary>
        /// Zoom时改变DrawPanel的loacation
        /// </summary>
        /// <param name="centerPt">缩放后以centerPt为中心点</param>
        public void Zoom_DrawPanelLocation(Point centerPt)
        {
            LastLocation = DrawPanel.Location;
            Point newCenterPt = new Point((int)(centerPt.X * CurZoom), (int)(centerPt.Y * CurZoom));
            int x = this.Size.Width / 2 - newCenterPt.X;
            int y = this.Size.Height / 2 - newCenterPt.Y;
            //    DrawPanel.Location= new Point(this.Size.Width/2 - newCenterPt.X, this.Size.Height/2 - newCenterPt.Y);

            //设置滚动条
            if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//框架控件比较大时
            {
                //设置scroll
                DfVScrollBar.Enabled = false;
                DfVScrollBar.Value = 0;
                
                DfHScrollBar.Enabled = false;
                DfHScrollBar.Value = 0;

                DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
                    (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);

            }
            else if (this.Width < DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//宽度比较小时
            {
                //设置Location
                if (DrawPanel.Location.X == MinSpace + DesignPanel.RulerSize)//当前的位置x坐标大于一定要留的最小距离.
                //否则修改其location
                {
                    DrawPanel.Location = new Point(MinSpace + DesignPanel.RulerSize,
                        (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
                }
                else
                {
                    DrawPanel.Location = new Point(x,
                        (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
                }

                //设置scroll
                DfVScrollBar.Enabled = false;
                DfVScrollBar.Value = 0;

                DfHScrollBar.Enabled = true;
                DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                DfHScrollBar.LargeChange = this.Width - 2;
                DfHScrollBar.Value = MinSpace + DesignPanel.RulerSize - DrawPanel.Location.X;

            }
            else if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
                this.Height < DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                //设置Location
                if (this.LastLocation.Y == MinSpace + DesignPanel.RulerSize)
                {
                    DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
                        MinSpace + DesignPanel.RulerSize);

                }
                else
                {
                    DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2, y);
                }
                //设置scroll
                DfHScrollBar.Enabled = false;
                DfHScrollBar.Value = 0;
                // 

                DfVScrollBar.Enabled = true;
                DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                DfVScrollBar.LargeChange = this.Height - 2;
                if (MinSpace + DesignPanel.RulerSize - DrawPanel.Location.Y < 0)
                {
                    DfVScrollBar.Value = 0;
                }
                else
                    DfVScrollBar.Value = MinSpace + DesignPanel.RulerSize - DrawPanel.Location.Y;
            }
            else
            {
                //设置Location
                int newX = x;
                int newY = y;
                if (DrawPanel.Location.X == MinSpace + DesignPanel.RulerSize)//当前的位置x坐标大于一定要留言的最小距离.
                //否则不用修改其location
                {
                    newX = MinSpace + DesignPanel.RulerSize;
                }
                if (this.LastLocation.Y == MinSpace + DesignPanel.RulerSize)
                {
                    newY = MinSpace + DesignPanel.RulerSize;

                }
                DrawPanel.Location = new Point(newX, newY);


                //设置scroll
                DfHScrollBar.Enabled = true;
                DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                if (this.Width > 2)
                {
                    DfHScrollBar.LargeChange = this.Width - 2;
                }


                DfVScrollBar.Enabled = true;
                DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
                if (this.Height > 2)
                {
                    DfVScrollBar.LargeChange = this.Height - 2;
                }
                if ( MinSpace + DesignPanel.RulerSize - DrawPanel.Location.X < 0)
                {
                    DfHScrollBar.Value = 0;
                }
                else if (MinSpace + DesignPanel.RulerSize - DrawPanel.Location.X > DfHScrollBar.Maximum)
                {
                    DfHScrollBar.Value = DfHScrollBar.Maximum;
                }
                else
                    DfHScrollBar.Value = MinSpace + DesignPanel.RulerSize - DrawPanel.Location.X;
                if (MinSpace + DesignPanel.RulerSize - DrawPanel.Location.Y < 0)
                {
                    DfVScrollBar.Value = 0;
                }
                else if (MinSpace + DesignPanel.RulerSize - DrawPanel.Location.Y > DfVScrollBar.Maximum)
                {
                    DfVScrollBar.Value = DfVScrollBar.Maximum;
                }
                else
                    DfVScrollBar.Value = MinSpace + DesignPanel.RulerSize - DrawPanel.Location.Y;
            }

            DrawHatchLines();

            //if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
            //    this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//框架控件比较大时
            //{
            //    DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
            //        (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
            //    //设置scroll
            //    DfVScrollBar.Enabled = false;
            //    DfHScrollBar.Enabled = false;
            //}
            //else if (this.Width < DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
            //    this.Height >= DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))//宽度比较小时
            //{
            //    if (this.LastLocation.X >= MinSpace)
            //    {
            //        DrawPanel.Location = new Point(MinSpace,
            //            (TDPanel.DrawFrame.Height - DrawPanel.Height) / 2);
            //        
            //    }
            //    //设置scroll
            //    DfVScrollBar.Enabled = false;

            //    DfHScrollBar.Enabled = true;
            //    DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
            //    DfHScrollBar.LargeChange = this.Width - 2;

            //}
            //else if (this.Width >= DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) &&
            //    this.Height < DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize))
            //{
            //    if (this.LastLocation.Y >= MinSpace)
            //    {
            //        DrawPanel.Location = new Point((TDPanel.DrawFrame.Width - DrawPanel.Width) / 2,
            //            MinSpace);
            //        

            //    }
            //    //设置scroll
            //    DfHScrollBar.Enabled = false;


            //    DfVScrollBar.Enabled = true;
            //    DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
            //    DfVScrollBar.LargeChange = this.Height - 2;
            //}
            //else
            //{
            //    int newLocationX = LastLocation.X > MinSpace ? MinSpace : LastLocation.X;
            //    int newLocationY = LastLocation.Y > MinSpace ? MinSpace : LastLocation.Y;
            //    DrawPanel.Location = new Point(newLocationX, newLocationY);
            //    DfHScrollBar.Value = LastLocation.X > MinSpace ? 0 : DfHScrollBar.Value;
            //    DfVScrollBar.Value = LastLocation.Y > MinSpace ? 0 : DfVScrollBar.Value;

            //    //设置scroll
            //    DfHScrollBar.Enabled = true;
            //    DfHScrollBar.Maximum = DrawPanel.Width + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
            //    if (this.Width > 2)
            //    {
            //        DfHScrollBar.LargeChange = this.Width - 2;
            //    }


            //    DfVScrollBar.Enabled = true;
            //    DfVScrollBar.Maximum = DrawPanel.Height + 2 * MinSpace + (DfVScrollBar.Width + DesignPanel.RulerSize) - 1;
            //    if (this.Height > 2)
            //    {
            //        DfVScrollBar.LargeChange = this.Height - 2;
            //    }
            //}
            //LastLocation = new Point(DrawPanel.Location.X, DrawPanel.Location.Y);
        }

        /// <summary>
        /// 计算上一次中心点,以drawPanel的左上角为相对起点,随zoom变化而变化.
        /// </summary>
        /// <returns></returns>
        public Point GetCenterPoint()
        {
            //if (DrawPanel.RightMousePoint.X > -1 )
            //{
            //    return DrawPanel.RightMousePoint;   
            //}
            //else
            {

                int x = Size.Width / 2 - DrawPanel.Location.X;
                int y = Size.Height / 2 - DrawPanel.Location.Y;
                return new Point((int)(x / DrawPanel.LastZoom), (int)(y / DrawPanel.LastZoom));
            }

            //if ()
            //{

            //}
            //if (this.Width > DrawPanel.InitWidth * DrawPanel.LastZoom + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ) &&
            //    this.Height > DrawPanel.Height + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ))//框架控件比较大时
            //{
            //    centerPt = new Point(DrawPanel.InitWidth * DrawPanel.LastZoom / 2, DrawPanel.Height / 2);
            //}
            //else if (this.Width <= DrawPanel.InitWidth * DrawPanel.LastZoom + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ) &&
            //         this.Height > DrawPanel.Height + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ))//宽度比较小时
            //{
            //    centerPt = new Point(
            //        this.Width / 2 - LastLocation.X,
            //        DrawPanel.Height / 2);
            //}
            //else if (this.Width > DrawPanel.InitWidth * DrawPanel.LastZoom + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ) &&
            //    this.Height <= DrawPanel.Height + 2 * MinSpace + ( DfVScrollBar.Width + DesignPanel.RulerSize ))
            //{
            //    centerPt = new Point(
            //        DrawPanel.InitWidth * DrawPanel.LastZoom / 2 ,
            //        this.Height / 2 - LastLocation.Y
            //        );
            //}
            //else
            //{
            //    centerPt = new Point(
            //        this.Width / 2 - LastLocation.X,
            //        this.Height / 2 - LastLocation.Y);

            //}
        }
        
        #endregion

        #region 工厂模式的实现函数

        /// <summary>
        /// 动态创建DrawPanel
        /// </summary>
        /// <param name="tD"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        protected virtual DrawPanel CreateDrawPanel(DesignPanel tD, int width, int height,Image backImage)
        {
            return new DrawPanel(tD, width, height,backImage);
        }
        #endregion
    }
}
