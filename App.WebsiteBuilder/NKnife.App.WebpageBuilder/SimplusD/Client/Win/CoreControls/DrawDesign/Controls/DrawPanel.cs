using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 画板控件,用于在其上设计模板
    /// </summary>
    public partial class DrawPanel : BaseControl
    {
        #region 字段定义

        #region 鼠标光标
        Cursor _mouseSelectCursor = Cursors.Arrow;
        Cursor _drawLineCursor = Cursors.Arrow;
        Cursor _panelMoveCursor = Cursors.Arrow;
        Cursor _panelMovingCursor = Cursors.Arrow;
        Cursor _hSplit = Cursors.HSplit;
        Cursor _vSplit = Cursors.VSplit;
        Cursor _hand = Cursors.Hand;
        Cursor _bigZoom = Cursors.Arrow;
        Cursor _smallZomm = Cursors.Arrow;
        Cursor _mouseDeleteLine = Cursors.Arrow;
        Cursor _selectLine = Cursors.Arrow;
        Cursor _selectRect = Cursors.Arrow;
        #endregion

        private EnumDrawType _drawType = EnumDrawType.MouseSelect;//绘画类型状态,0为非绘画状态,1为画直线,2为选择直线,3为选择矩形
        private EnumDrawType _tempDrawType = EnumDrawType.MouseSelect;
        private bool _isRectSelecting = false;//是否开始圈选
        private bool _isMouseDeleteLine = false; //是否开始用鼠标删除线段


        //todo:ZhengHao
        private int _penSize                = SoftwareOption.TmpltDesigner.PenSize;
        private Color _penColor             = SoftwareOption.TmpltDesigner.PenColor;
        private Color _selectedLineColor    = SoftwareOption.TmpltDesigner.SelectedLinePenColor;
        private Color _lockedLinePenColor   = SoftwareOption.TmpltDesigner.LockedLinePenColor;
        private Color _rectSelectColor      = SoftwareOption.TmpltDesigner.RectSelectedBrush;
        private Color _delectedLineColor    = SoftwareOption.TmpltDesigner.DirectLineColor;
        private Color _selectedRectColor    = SoftwareOption.TmpltDesigner.RectSelectedBrush;
        
        PartitionLine _lastSelectingLine = null;
        Rect _lastSelectingRect = null;

        Rect _curSelectingRect = null;

        float _curZoom = 1F;

        private PartitionLine _MouesDeleteLine;

        

        #endregion

        #region 属性定义

        /// <summary>
        /// 获取或设置画板的文档
        /// </summary>
        //public XmlDocument TmpltDoc { get; set; }

        /// <summary>
        /// 获取或设置画板的背景图片
        /// </summary>
        public Image BackImage { get; set; }

        /// <summary>
        /// 得到当前矩形,可用于属性面板
        /// </summary>
        public List<Rect> CurRects
        {
            get
            {
                return ListRect.GetSelectedRects();
            }
        }

        /// <summary>
        /// 画板
        /// </summary>
        public DesignPanel TDPanel { get; private set; }

        /// <summary>
        /// DesignPanel的DrawFrame
        /// </summary>
        public DrawFrame DrawFrame
        {
            get { return TDPanel.DrawFrame; }
          //  set { TDPanel.DrawFrame = value; }
        }

        /// <summary>
        /// 直线数组数据
        /// </summary>
        public LineList ListLine { get; private set; }

        /// <summary>
        /// 矩形数组数据,构造函数中初始化
        /// </summary>
        public RectList ListRect { get; private set; }

        /// <summary>
        /// 绘画类型状态,0为非绘画状态,1为画直线,2为鼠标选择对象,3为矩形拖动以选择对象选择
        /// </summary>
        public EnumDrawType DrawType
        {
            get { return _drawType; }
            set
            {
                switch (value)
                {
                    case EnumDrawType.DrawLine:
                        _curSelectingRect = null;
                        Cursor = _drawLineCursor;
                        break;
                    case EnumDrawType.MouseSelect:
                        Cursor = _mouseSelectCursor;
                        break;
                    case EnumDrawType.PanelMove:
                        Cursor = _panelMoveCursor;
                        break;
                    case EnumDrawType.ZoomBigger:
                        Cursor = _bigZoom;
                        break;
                    case EnumDrawType.ZoomSmaller:
                        Cursor = _smallZomm;
                        break;
                    case EnumDrawType.ZoomChang:
                        Cursor = _bigZoom;
                        break;
                    case EnumDrawType.MouseSelectLine:
                        Cursor = _selectLine;
                        break;
                    case EnumDrawType.MouseSelectRect:
                        Cursor = _selectRect;
                        break;
                    case EnumDrawType.MouseDeleteLine:
                        Cursor = _mouseDeleteLine;
                        break;
                    //case EnumDrawType.ClipPanel:
                    //    Cursor = _clip;
                    //    break;
                    default:
                        Cursor = Cursors.Arrow;
                        break;
                }
                _drawType = value;
                OnChangDrawType(new EventArgs());
            }
        }
        
        /// <summary>
        /// 画布状态
        /// </summary>  
        protected int State { get; set; }

        /// <summary>
        /// 临时存放直线的第一个点
        /// </summary>
        protected Point FirstRealPoint { get; set; }

        /// <summary>
        /// 临时存放直线的第二个点
        /// </summary>
        protected Point SecondRealPoint { get; set; }

        /// <summary>
        /// 画直线的预测起点
        /// </summary>
        protected Point StartRealPoint { get; set; }

        /// <summary>
        /// 画直线的预测终点
        /// </summary>
        protected Point EndRealPoint { get; set; }

        /// <summary>
        /// 右击坐标记录
        /// </summary>
        protected Point RightMousePoint { get; set; }

        protected Point FirstInitPoint { get; set; }

        protected Point SecondInitPoint { get; set; }

        /// <summary>
        ///画线时第一根“停靠的线”
        /// </summary>
        protected PartitionLine FirstLine { get; set; }

        /// <summary>
        ///画线时起点所在第二根停靠线(非终点停靠线!)
        /// </summary>
        protected PartitionLine SecondLine { get; set; }

        /// <summary>
        /// 画笔粗度
        /// </summary>
        public int PenSize
        {
            get { return _penSize; }
            set { 
                _penSize = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 加粗画笔之加粗倍数
        /// </summary>
        public int BoldPenTimes { get; set; }

        /// <summary>
        /// 默认画笔颜色
        /// </summary>
        public Color PenColor
        {
            get { return _penColor; }
            set {
                _penColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 默认的指示线的颜色
        /// </summary>
        public Color DirectLineColor { get; set; }

        /// <summary>
        /// 选择状态的直线颜色
        /// </summary>
        public Color SelecteLinePenColor
        {
            get { return _selectedLineColor; }
            set 
            { 
                _selectedLineColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 待选择状态的直线颜色
        /// </summary>
        public Color SelectingLineColor { get; set; }

        /// <summary>
        /// 锁定状态的直线颜色
        /// </summary>
        public Color LockedLinePenColor
        {
            get { return _lockedLinePenColor; }
            set 
            { 
                _lockedLinePenColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 做框选的矩形框的颜色
        /// </summary>
        public Color RectSelectColor
        {
            get { return _rectSelectColor; }
            set 
            {     
                _rectSelectColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///自动吸附之距离
        /// </summary>
        public int AutoAdsorb { get; set; }

        /// <summary>
        ///需要吸附之直线
        /// </summary>
        protected List<PartitionLine> AdsorbLines { get; set; }

        /// <summary>
        /// 横向直线数组
        /// </summary>
        public SDList<PartitionLine> HPartionLines
        {
            get { return ListLine.HPartionLines; }
            set { ListLine.HPartionLines = value; }
        }

        /// <summary>
        /// 获取或设置边界线
        /// </summary>
        public SDList<PartitionLine> BorderLines
        {
            get { return ListLine.BorderLines; }
            set { ListLine.BorderLines = value; }
        }

        /// <summary>
        /// 纵向直线
        /// </summary>
        public SDList<PartitionLine> VPartionLines
        {
            get { return ListLine.VPartionLines; }
            set { ListLine.VPartionLines = value; }
        }

        /// <summary>
        /// 选择对象偏移误差范围
        /// </summary>
        public int SelectPrecision { get; set; }

        /// <summary>
        ///指示线的绘画的偏移距离
        /// </summary>
        public int OffsetDirectionLine { get; set; }

        /// <summary>
        ///当前的偏移量
        /// </summary>
        public int CurrentPrecision { get; set; }

        /// <summary>
        /// 命令历史记录
        /// </summary>
        public CommandList CommandList
        {
            get { return TDPanel.CommandList; }
            set { TDPanel.CommandList = value; }
        }

        /// <summary>
        /// 选择被移动之直线,此直线不存在于listLine,而只是表明其欲移动之几何位置
        /// </summary>
        protected PartitionLine SelectedLine { get; set; }

        /// <summary>
        /// 移动到的直线
        /// </summary>
        protected PartitionLine MoveToLine { get; set; }

        /// <summary>
        /// 左或上的界线
        /// </summary>
        protected PartitionLine FirstBorderLine { get; set; }

        /// <summary>
        /// 右或下的界线
        /// </summary>
        protected PartitionLine SecondBorderLine { get; set; }

        /// <summary>
        /// 矩形待选择(鼠标划过时的填充颜色)
        /// </summary>
        public Color SelectingRectColor { get; set; }

        /// <summary>
        /// 矩形被选择时的填充颜色
        /// </summary>
        public Color SelectedRectColor
        {
            get { return _selectedRectColor; }
            set {
                _selectedRectColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 当前放大缩小倍数
        /// </summary>
        public float CurZoom
        {
            get { return _curZoom; }
            set
            {
                if (value!=_curZoom)
                {
                    LastZoom = _curZoom;
                    _curZoom = value;
                    ChangeZoom(new EventArgs());                    
                }                
            }
        }

        /// <summary>
        /// 上一次缩放值
        /// </summary>
        public float LastZoom { get; set; }

        /// <summary>
        /// 初始化,即curZoom=1时的宽度
        /// </summary>
        public int InitWidth { get; private set; }

        /// <summary>
        /// 初始化,即curZoom=1时的宽度
        /// </summary>
        public int InitHeight { get; private set; }

        /// <summary>
        /// 上次的坐标原点
        /// </summary>
        protected Point LastLocation
        {
            get { return TDPanel.DrawFrame.LastLocation; }
            set { TDPanel.DrawFrame.LastLocation = value; }
        }

        /// <summary>
        /// 用于画线时的指示控件
        /// </summary>
        protected DirectionLineCtl FirstDirectLineCtl { get; set; }

        /// <summary>
        /// 用于画线时的指示控件
        /// </summary>
        protected DirectionLineCtl SecondDirectLineCtl { get; set; }

        /// <summary>
        /// 设置或获取被选择时的颜色刷
        /// </summary>
        public Brush RectSelectedBrush { get; set; }

        /// <summary>
        /// 设置或获取被锁定时的颜色刷
        /// </summary>
        public Brush LockedBrush { get; set; }

        /// <summary>
        /// 设置或获取含有页面时的颜色刷
        /// </summary>
        public Brush HasSnipBrush { get; set; }

        /// <summary>
        /// 设置或获取含有正文时的颜色刷
        /// </summary>
        public Brush HasContentSnipBrush { get; set; }

        /// <summary>
        /// 上下文菜单
        /// </summary>
        public ContextMenuStrip RectMenu { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DrawPanel(DesignPanel tD, int width, int height, Image backImage)
        {
            ResourcesReader.SetControlPropertyHelper(this);            

            InitializeComponent();

            #region 初始化鼠标光标

            //_mouseSelectCursor = (Cursor)ResourceService.GetResource("tmplt.cursor.select"); //LoadCursor(_curPath + "select.cur");
            //_drawLineCursor = (Cursor)ResourceService.GetResource("tmplt.cursor.pen"); //LoadCursor(_curPath + "pen.cur");
            //_panelMoveCursor = (Cursor)ResourceService.GetResource("tmplt.cursor.move"); //LoadCursor(_curPath + "move.cur");
            //_panelMovingCursor = (Cursor)ResourceService.GetResource("tmplt.cursor.moving"); //LoadCursor(_curPath + "moving.cur");
            //_bigZoom = (Cursor)ResourceService.GetResource("tmplt.cursor.zoombig"); //LoadCursor(_curPath + "zoombig.cur");
            //_smallZomm = (Cursor)ResourceService.GetResource("tmplt.cursor.zoomsmall"); //LoadCursor(_curPath + "zoomsmall.cur");
            //_mouseDeleteLine = (Cursor)ResourceService.GetResource("tmplt.cursor.delete"); //LoadCursor(_curPath + "delete.cur");

            string _curPath = Application.StartupPath+@"\Cursors\";
            _mouseSelectCursor = LoadCursor(_curPath + "select.cur");
            _drawLineCursor = LoadCursor(_curPath + "pen.cur");
            _panelMoveCursor = LoadCursor(_curPath + "move.cur");
            _panelMovingCursor = LoadCursor(_curPath + "moving.cur");
            _bigZoom = LoadCursor(_curPath + "zoombig.cur");
            _smallZomm = LoadCursor(_curPath + "zoomsmall.cur");
            _mouseDeleteLine = LoadCursor(_curPath + "delete.cur");

            #endregion

            #region 初始化控件基本属性

            TDPanel = tD;
            Width = width;
            Height = height;
            InitWidth = width;
            InitHeight = height;
            ListLine = new LineList();
            State = 0;//状态
            RightMousePoint = new Point(-1, -1);
            LastZoom = 1F;

            BoldPenTimes = SoftwareOption.TmpltDesigner.BoldPenTimes; //加粗画笔之加粗倍数
            AutoAdsorb = SoftwareOption.TmpltDesigner.AutoAdsorb; //自动吸附的距离
            SelectPrecision = SoftwareOption.TmpltDesigner.SelectPrecision;//选择对象偏移误差范围
            OffsetDirectionLine = SoftwareOption.TmpltDesigner.OffsetDirectionLine;//指示线的绘画的偏移距离  
            CurrentPrecision = SoftwareOption.TmpltDesigner.CurrentPrecision; //当前的偏移量
            SelectingRectColor = SoftwareOption.TmpltDesigner.SelectingRectColor; //鼠标划过矩形时的填充色

            SelectingLineColor = SoftwareOption.TmpltDesigner.SelectingLineColor;
            DirectLineColor = SoftwareOption.TmpltDesigner.DirectLineColor;            
            RectSelectedBrush =new SolidBrush( SoftwareOption.TmpltDesigner.RectSelectedBrush); //new SolidBrush(Color.FromArgb(70, Color.Blue));
            LockedBrush = new SolidBrush(SoftwareOption.TmpltDesigner.LockedBrush);// new SolidBrush(Color.FromArgb(70, Color.Red));
            HasSnipBrush = new SolidBrush(SoftwareOption.TmpltDesigner.HasSnipBrush);// new SolidBrush(Color.FromArgb(70, Color.Lime));
            HasContentSnipBrush =new SolidBrush( SoftwareOption.TmpltDesigner.HasContentSnipBrush);// new SolidBrush(Color.FromArgb(70, Color.Green));

            RectMenu = new ContextMenuStrip();

            DrawType = EnumDrawType.MouseSelect;

            #endregion

            #region 设置控件之显示属性

            ///设置控件之显示属性(双缓存)
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();

            #endregion

            #region 初始化动态生成之成员

            ///初始化动态生成之成员
            ListRect = new RectList(tD);

            #endregion

            #region 初始化其他数据

            ///设置页面片list矩形
            ListRect.SnipRectList.Add(TDPanel.CreateRect(0, 0, Width, Height));

            ///添加四条边界分割线:按上,下,左,右的顺序加入到边界线数组中
            PartitionLine line = new PartitionLine(0, Width, 0, true);
            ListLine.BorderLines.Add( line);
            ListLine.AddLine(line);

            line = new PartitionLine(0, Width, Height, true);
            ListLine.BorderLines.Add(line);
            ListLine.AddLine(line);

            line = new PartitionLine(0, Height, 0, false);
            ListLine.BorderLines.Add(line);
            ListLine.AddLine(line);

            line = new PartitionLine(0, Height, Width, false);
            ListLine.BorderLines.Add(line);
            ListLine.AddLine(line);

            BackImage = backImage;

            //加入两个小控件，用于指示画线的分割线。
            FirstDirectLineCtl = new DirectionLineCtl(true);
            SecondDirectLineCtl = new DirectionLineCtl(true);

            this.Controls.Add(FirstDirectLineCtl);
            this.Controls.Add(SecondDirectLineCtl);

            FirstDirectLineCtl.Hide();
            SecondDirectLineCtl.Hide();
            FirstDirectLineCtl.BringToFront();
            SecondDirectLineCtl.BringToFront();

            ///弹出添加菜单
            InitContextMenu();

            #endregion
        }

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        protected virtual void InitContextMenu()
        {
            InitContextMenuItems();
            this.RectMenu.Opening += new CancelEventHandler(RectMenu_Opening);
            this.ContextMenuStrip = RectMenu;
        }

        /// <summary>
        /// 虚函数，初始化右键菜单的内容
        /// </summary>
        protected virtual void InitContextMenuItems()
        {

        }

        #endregion

        #region 重写事件函数

        /// <summary>
        /// 当控件绘制时。。。
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // 重绘画板元素
            Graphics g = pe.Graphics;

            g.ScaleTransform(CurZoom, CurZoom);
            if (BackImage != null)
            {
                g.DrawImage(BackImage, 0, 0,new RectangleF(0,0, InitWidth, InitHeight),GraphicsUnit.Pixel);
            }
            else
                g.DrawRectangle(new Pen(_penColor, _penSize), 0, 0, InitWidth, InitHeight);
            DrawPanelItems(g);

            /// 调用基类 OnPaint
            base.OnPaint(pe);
        }

        /// <summary>
        /// 当点击鼠标时。。。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Select();//获取焦点            
            OnMouseRuler(e);

            #region 点击左键
            if (e.Button == MouseButtons.Left)
            {
                if (DrawType == EnumDrawType.ZoomChang)
                {
                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                        {
                            ZoomOut();
                        }
                        else
                            ZoomIn();
                    }
                    if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        ZoomOut();
                    }
                }
                if (DrawType == EnumDrawType.ZoomBigger)
                {
                    if ((ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        ZoomOut();
                    }
                    else
                        ZoomIn();
                }
                if (State == 0 && DrawType == EnumDrawType.DrawLine)//如果开始画线,取点并存入point1中
                {
                    MouseDown_DrawLine(e);
                    //State = 1;//改变绘画状态  
                }
                else if (DrawType == EnumDrawType.MouseSelect || DrawType == EnumDrawType.MouseSelectRect || DrawType == EnumDrawType.MouseSelectLine)//鼠标选择
                {
                    MouseLeftDown_MouseSelect(e);
                }
                else if (DrawType == EnumDrawType.PanelMove)
                {
                    MouseLeftDown_PanelMove(e);
                }
                else if (DrawType == EnumDrawType.MouseDeleteLine)
                {
                    MouseLeftDown_MouseDeleteLine(e);
                }
            }

            #endregion

            #region 点击右键

            else if (e.Button == MouseButtons.Right)
            {
                //保存右击点坐标
                FirstRealPoint = this.BacktrackMouse(e);
                MouseRightDown_Select(e);
            }

            #endregion

            base.OnMouseDown(e);
        }

        /// <summary>
        /// 当移动鼠标时。。。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            OnMouseRuler(e);
            if ((DrawType == EnumDrawType.DrawLine && State == 1 && e.Button == MouseButtons.Left) )
            {
                MouseMove_DrawLine(e);
            }
            else if (DrawType == EnumDrawType.MouseSelect)
            {
                if ((State == 1 || State == 2) && e.Button == MouseButtons.Left)
                {
                    MouseMove_MoveLine(e);
                    State = 2;
                }
                else if (State == 0)
                {
                    if (_isRectSelecting)
                        MouseMove_RectSelect(e);
                    else
                        MouseMove_Selecting(e);
                }
            }
            else if (DrawType == EnumDrawType.MouseSelectRect)
            {
                if (State == 0)
                {
                    if (_isRectSelecting)
                        MouseMove_RectSelect(e);
                    else
                        MouseMove_SelectingRect(e);
                }
            }
            else if (DrawType == EnumDrawType.MouseSelectLine)
            {
                if ((State == 1 || State == 2) && e.Button == MouseButtons.Left)
                {
                    MouseMove_MoveLine(e);
                    State = 2;
                }
                else if (State == 0)
                {
                    MouseMove_SelectingLine(e);
                }
            }
            else if (DrawType == EnumDrawType.PanelMove && State == 1)
            {
                MouseMove_PanelMove(e);
            }
            else if (DrawType == EnumDrawType.ZoomBigger)
            {
                MouseMove_ZoomChangBigger(e);
            }
            else if (DrawType == EnumDrawType.MouseDeleteLine)
            {
                if (_isMouseDeleteLine)
                {
                    MouseMove_MouseDeleteLine(e);
                }
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// 当释放鼠标时。。。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (DrawType == EnumDrawType.DrawLine && State == 1)
                {
                    MouseUp_DrawLine(e);
                    State = 0;
                }
                else if (DrawType == EnumDrawType.MouseSelect || DrawType == EnumDrawType.MouseSelectLine)
                {
                    if (State == 0 || State == 2)
                    {
                        if (_isRectSelecting)
                        {
                            MouseUp_RectSelect(e);
                        }
                        else
                            MouseUp_MoveLine();
                    }
                    State = 0;
                }
                else if (DrawType == EnumDrawType.MouseSelectRect)
                {
                    if (State == 0 || State == 2)
                    {
                        if (_isRectSelecting)
                        {
                            MouseUp_RectSelect(e);
                        }
                    }
                    State = 0;
                }
                else if (DrawType == EnumDrawType.PanelMove)
                {
                    Cursor = _panelMoveCursor;
                    State = 0;
                }
                else if (DrawType == EnumDrawType.MouseDeleteLine)
                {
                    if (State == 0 || State == 2)
                    {
                        MouseUp_MouseDeleteLine(e);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                RightMousePoint = BacktrackMouse(e);
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// 当鼠标离开时。。。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (DrawType == EnumDrawType.MouseSelect || DrawType == EnumDrawType.MouseSelectRect || DrawType == EnumDrawType.MouseSelectLine)
            {
                _lastSelectingRect = null;
                _curSelectingRect = null;
                _lastSelectingLine = null;
                Invalidate();
                Update();
            }
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 捕捉键盘被按下消息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //裁剪
            //if (e.KeyCode == Keys.C)
            //{
            //    KeyDown_ClipPanel();
            //}
            //锁定与解锁
            if (e.KeyCode == Keys.K)
            {
                KeyDown_Lock();
            }
            //置为画线
            if (e.KeyCode == Keys.B)
            {
                KeyDown_DrawLine();
            }
            //置为鼠标选择
            if (e.KeyCode == Keys.V)
            {
                KeyDown_MouseSelect();
            }
            if (e.KeyCode == Keys.L)
            {
                KeyDown_MouseSelectLine();
            }
            if (e.KeyCode == Keys.R)
            {
                KeyDown_MouseSelectRect();
            }
            //手型拖动
            if (e.KeyCode == Keys.D)
            {
                KeyDown_PanelMove();
            }
            //放大、缩小
            if (e.KeyCode == Keys.Z)
            {
                KeyDown_ZoomBigger();
            }
            if (e.KeyCode == Keys.Space)
            {
                KeyDown_ZoomChang();
            }
            if (e.KeyCode == Keys.Menu)
            {
                if (DrawType == EnumDrawType.ZoomBigger)
                {
                    Cursor = _smallZomm;
                }
            }
            if (e.KeyCode == Keys.E)
            {
                KeyDown_MouseDeleteLine();
            }
            base.OnKeyDown(e);
        }

        #endregion

        #region 消息处理

        /// <summary>
        /// 改变zoom
        /// </summary>
        protected void ChangeZoom(EventArgs e)
        {
            Point centerPt = DrawFrame.GetCenterPoint();
            //修改大小
            this.Width = (int)(InitWidth * CurZoom);
            this.Height = (int)(InitHeight * CurZoom);
            //修改location
            TDPanel.DrawFrame.Zoom_DrawPanelLocation(centerPt);
            Invalidate();
            OnChangeZoom(e);
        }

        /// <summary>
        /// 对鼠标的坐标做相应的"回滚"操作,以使得在不同的放大缩小时能得到正确的数据.
        /// 参考自 BobPowell.net <seealso cref="http://www.bobpowell.net/backtrack.htm"/>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected Point BacktrackMouse(MouseEventArgs e)
        {
            // Backtrack the mouse...
            Point[] pts = new Point[] { new Point(e.X, e.Y) };
            Matrix mx = new Matrix();
            mx.Scale(1 / CurZoom, 1 / CurZoom);
            mx.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// 对鼠标的坐标做相应的"回滚"操作,以使得在不同的放大缩小时能得到正确的数据.
        /// 参考自 BobPowell.net <seealso cref="http://www.bobpowell.net/backtrack.htm"/>
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected Point BacktrackMouse(int x,int y )
        {
            // Backtrack the mouse...
            Point[] pts = new Point[] { new Point(x, y) };
            Matrix mx = new Matrix();
            mx.Scale(1 / CurZoom, 1 / CurZoom);
            mx.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// 控件直线元素重画
        /// </summary>
        protected void DrawPanelLines()
        {
            ///控件直线元素重绘
            Graphics g = this.CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            Pen pen1 = new Pen(this.PenColor, this.PenSize);
            Pen pen2 = new Pen(this.SelecteLinePenColor, this.PenSize * BoldPenTimes);

            //绘画横向直线
            foreach (PartitionLine line in HPartionLines)
            {
                DrawLine(line);
            }
            //绘画纵向直线
            foreach (PartitionLine line in VPartionLines)
            {
                DrawLine(line);
            }
            //释放资源
            pen1.Dispose();
            pen2.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 控件直线元素重画
        /// </summary>
        protected void DrawPanelLines(Graphics g)
        {
            ///控件直线元素重绘

            //绘画横向直线
            foreach (PartitionLine line in BorderLines)
            {
                if (line.Position != 0)
                {
                    PartitionLine newLine = new PartitionLine(line.Start, line.End, line.Position-PenSize, line.IsRow);
                    DrawLine(newLine, g);
                }
                //else
                //    DrawLine(line, g);
            }
            foreach (PartitionLine line in HPartionLines)
            {
                if (!BorderLines.Contains(line))
                {
                    PartitionLine newLine = new PartitionLine(line.Start, line.End, line.Position - PenSize, line.IsRow);
                    DrawLine(newLine, g);
                }
            }
            //绘画纵向直线
            foreach (PartitionLine line in VPartionLines)
            {
                if (!BorderLines.Contains(line))
                {
                    PartitionLine newLine = new PartitionLine(line.Start, line.End, line.Position - PenSize, line.IsRow);
                    DrawLine(newLine, g);
                }
            }
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="line"></param>
        internal void DrawLine(PartitionLine line)
        {
            //控件直线元素重绘
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            Pen pen = new Pen(PenColor, PenSize);
            Pen pen2 = new Pen(SelecteLinePenColor, PenSize * BoldPenTimes);
            Pen pen3 = new Pen(LockedLinePenColor, PenSize * BoldPenTimes);
            pen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            if (line.ChildLines == null || line.IsSelected)
            {
                if (line.IsRow)
                {
                    if (line.IsSelected)
                    {
                        g.DrawLine(pen2, line.Start, line.Position, line.End, line.Position);
                    }
                    else
                    {
                        g.DrawLine(pen, line.Start, line.Position, line.End, line.Position);
                    }
                    if (line.IsLocked)
                    {
                        g.DrawLine(pen3, line.Start, line.Position, line.End, line.Position);
                    }
                }
                else
                {
                    if (line.IsSelected)
                    {
                        g.DrawLine(pen2, line.Position, line.Start, line.Position, line.End);
                    }
                    else
                    {
                        g.DrawLine(pen, line.Position, line.Start, line.Position, line.End);
                    }
                    if (line.IsLocked)
                    {
                        g.DrawLine(pen3, line.Position, line.Start, line.Position, line.End);
                    }
                }
            }
            else
            {
                //   int i = 0;
                foreach (PartitionLine childLine in line.ChildLines)
                {
                    if (childLine.IsRow)
                    {
                        if (childLine.IsSelected)
                        {
                            g.DrawLine(pen2, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }
                        else
                        {
                            g.DrawLine(pen, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }
                        if (childLine.IsLocked)
                        {
                            g.DrawLine(pen3, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }

                    }
                    else
                    {
                        if (childLine.IsSelected)
                        {
                            g.DrawLine(pen2, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }
                        else
                        {
                            g.DrawLine(pen, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }
                        if (childLine.IsLocked)
                        {
                            g.DrawLine(pen3, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }

                    }
                }
            }
            //释放资源
            pen.Dispose();
            pen2.Dispose();
            pen3.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="line"></param>
        protected void DrawLine(PartitionLine line, Graphics g)
        {
            //控件直线元素重绘
            Pen pen = new Pen(PenColor, PenSize);
            Pen pen2 = new Pen(SelecteLinePenColor, PenSize * BoldPenTimes);
            Pen pen3 = new Pen(LockedLinePenColor, PenSize * BoldPenTimes);
            pen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            if (line.ChildLines == null || line.IsSelected || (line.ChildLines != null && line.ChildLines.Count == 0))
            {
                if (line.IsRow)
                {
                    if (line.IsSelected)
                    {
                        g.DrawLine(pen2, line.Start, line.Position, line.End, line.Position);
                    }
                    else
                    {
                        g.DrawLine(pen, line.Start, line.Position, line.End, line.Position);
                    }
                    if (line.IsLocked)
                    {
                        g.DrawLine(pen3, line.Start, line.Position, line.End, line.Position);
                    }
                }
                else
                {
                    if (line.IsSelected)
                    {
                        g.DrawLine(pen2, line.Position, line.Start, line.Position, line.End);
                    }
                    else
                    {
                        g.DrawLine(pen, line.Position, line.Start, line.Position, line.End);
                    }
                    if (line.IsLocked)
                    {
                        g.DrawLine(pen3, line.Position, line.Start, line.Position, line.End);
                    }
                }
            }
            else
            {
                //   int i = 0;
                foreach (PartitionLine childLine in line.ChildLines)
                {
                    if (childLine.IsRow)
                    {
                        if (childLine.IsSelected)
                        {
                            g.DrawLine(pen2, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }
                        else
                        {
                            g.DrawLine(pen, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }
                        if (childLine.IsLocked)
                        {
                            g.DrawLine(pen3, childLine.Start, childLine.Position, childLine.End, childLine.Position);
                        }

                    }
                    else
                    {
                        if (childLine.IsSelected)
                        {
                            g.DrawLine(pen2, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }
                        else
                        {
                            g.DrawLine(pen, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }
                        if (childLine.IsLocked)
                        {
                            g.DrawLine(pen3, childLine.Position, childLine.Start, childLine.Position, childLine.End);
                        }

                    }
                }
            }
            //释放资源
            pen.Dispose();
            pen2.Dispose();
            pen3.Dispose();
        }        

        /// <summary>
        /// 控件矩形元素重画
        /// </summary>
        protected virtual void DrawPanelListRect(Graphics g)
        {

        }

        /// <summary>
        /// 矩形元素重画
        /// </summary>
        /// <param name="rect"></param>
        public virtual void DrawRect(Rect rect)
        {

        }

        /// <summary>
        /// 矩形元素重画
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="g"></param>
        public virtual void DrawRect(Rect rect, Graphics g)
        {

        }

        /// <summary>
        /// 重绘画板元素
        /// </summary>
        protected virtual void DrawPanelItems(Graphics g)
        {
            //控件元素重绘
            
            DrawPanelListRect(g);
            DrawSelectingRect(g);
            DrawPanelLines(g);
        }

        private void DrawSelectingRect(Graphics g)
        {
            if (_curSelectingRect == null)
            {
                return;
            }
            Color c = SelectingRectColor;
            Bitmap bmp = new Bitmap(_curSelectingRect.Width, _curSelectingRect.Height);

            if (BackImage != null)
            {

                Graphics gBmp = Graphics.FromImage(bmp);
                gBmp.DrawImage(BackImage, new Rectangle(1, 1, bmp.Width, bmp.Height), _curSelectingRect.X, _curSelectingRect.Y, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
                gBmp.FillRectangle(new SolidBrush(c), 0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, _curSelectingRect.X, _curSelectingRect.Y);

            }
            else
            {
                Graphics gNull = Graphics.FromImage(bmp);
                gNull.FillRectangle(new SolidBrush(c), 0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, _curSelectingRect.X, _curSelectingRect.Y);
            }

        }
                
        private void MouseLeftDown_MouseDeleteLine(MouseEventArgs e)
        {
            _isMouseDeleteLine = true;
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision  , !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            //初始化哪些对象应该被取消选择,哪些应该选择
            if (selectedLine != null)
            {
                FindLineByLine findLineByLine = new FindLineByLine(selectedLine);
                List<PartitionLine> lines = new List<PartitionLine>();
                List<PartitionLine> baseLines = selectedLine.IsRow ? VPartionLines : HPartionLines;
                foreach (PartitionLine line in baseLines)
                {
                    if (findLineByLine.PredicateJoined(line))
                        lines.Add(line);
                }
                if (lines.Count != 0)
                    return;
                _MouesDeleteLine = selectedLine;
            }            
        }

        private void MouseMove_MouseDeleteLine(MouseEventArgs e)
        {
            if (!_isMouseDeleteLine)
                return;
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            if (selectedRect == null)//如果选中的不是矩形,即为直线
            {
                if (selectedLine != null)
                {
                    FindLineByLine findLineByLine = new FindLineByLine(selectedLine);
                    List<PartitionLine> lines = new List<PartitionLine>();
                    List<PartitionLine> baseLines = selectedLine.IsRow ? VPartionLines : HPartionLines;
                    foreach (PartitionLine line in baseLines)
                    {
                        if (findLineByLine.PredicateJoined(line))
                            lines.Add(line);
                    }
                    if (lines.Count != 0)
                        return;
                }
                if (selectedLine != null && selectedLine != _lastSelectingLine && !selectedLine.IsSelected)
                {
                    //绘画鼠标划过的效果
                    if (selectedLine.IsRow)
                    {
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_delectedLineColor, PenSize), selectedLine.Start, selectedLine.Position, selectedLine.End, selectedLine.Position);
                    }
                    else
                    {
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_delectedLineColor, PenSize), selectedLine.Position, selectedLine.Start, selectedLine.Position, selectedLine.End);
                    }
                    _MouesDeleteLine = selectedLine;
                    _lastSelectingLine = selectedLine;
                    _lastSelectingRect = null;
                }
                else if (selectedLine != null && selectedLine.IsSelected)
                {
                    //刷新上次的划过效果
                    InvalidateSelecting();
                }
            }
            g.Dispose();
        }
        
        private void MouseUp_MouseDeleteLine(MouseEventArgs e)
        {
            if (!_isMouseDeleteLine)
                return;
            _isMouseDeleteLine = false;
            if (_MouesDeleteLine != null)
            {
                DeleteLine(_MouesDeleteLine);
                _MouesDeleteLine = null;
            }
        }
        
        /// <summary>
        /// 鼠标弹起时框选命令的响应
        /// </summary>
        private void MouseUp_RectSelect(MouseEventArgs e)
        {
            if (!_isRectSelecting)
                return;
            _isRectSelecting = false;
            SecondRealPoint = BacktrackMouse(e);
            List<Rect> preSelectedRects = ListRect.GetSelectedRects();
            FindRectByRect findByRect = new FindRectByRect(
                Utility.Draw.PointToRectangle(FirstRealPoint, SecondRealPoint));
            List<Rect> selectableRects = ListRect.SnipRectList.FindAll(findByRect.FindSelectableRectPredicate);

            List<Rect> selectedRects = new List<Rect>();
            List<Rect> disSelectedRects = new List<Rect>();

            if ((DrawPanel.ModifierKeys & Keys.Control) == Keys.Control)
            {
                foreach (Rect rect in selectableRects)
                {
                    if (rect.IsSelected)
                    {
                        disSelectedRects.Add(rect);
                    }
                    else
                    {
                        selectedRects.Add(rect);
                    }
                }
            }
            else
            {
                selectedRects.AddRange(selectableRects);
                foreach (Rect rect in preSelectedRects)//移除已经被选择了的矩形
                {
                    if (selectableRects.IndexOf(rect) < 0)
                    {
                        disSelectedRects.Add(rect);
                    }
                }
            }
            SelectCommand selectCmd = new SelectCommand(TDPanel,
                selectedRects,
                disSelectedRects,
                new List<PartitionLine>(),
                new List<PartitionLine>());
            selectCmd.Execute();
            CommandList.AddCommand(selectCmd);
            OnSelectingRect(EventArgs.Empty);
        }
        
        /// <summary>
        /// 鼠标画线之响应函数
        /// </summary>
        /// <param name="e"></param>
        private void MouseDown_DrawLine(MouseEventArgs e)
        {
            FirstRealPoint = this.BacktrackMouse(e);
            FirstInitPoint = new Point(e.X, e.Y);

            //找到与给定点距离最短的线
            // 先过虑，再排序
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            Rect curRect = ListRect.GetSelectedRect(FirstRealPoint, 0);
            
            if (curRect != null && (curRect.IsLocked || curRect.HasSnip ))
            {
                //MessageBox.Show("当前要分割的矩形已经被锁定(或含有页面片)!");
                MessageService.Show("${res:tmpltDesignTip.drawPanel}",MessageBoxButtons.OK ,MessageBoxIcon.Asterisk );
                State = 0;
                Invalidate();
                FirstDirectLineCtl.Hide();
                SecondDirectLineCtl.Hide();
                return;
            }
            if (curRect != null)
            {
                CommonFuns.Invalidate(this, PenSize, curRect, CurZoom);
                this.Update();
            }
            //DrawDirectionLine(StartRealPoint, FirstRealPoint);
            //DrawDirectionLine(EndRealPoint, SecondRealPoint);
            State = 1;
        }

        /// <summary>
        /// 绘画指示线
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        private void DrawDirectionLine(Point pt1, Point pt2)
        {
            Graphics g = this.CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            Pen penDirect = new Pen(Color.Red, PenSize / 2);
            //penDirect.DashStyle = DashStyle.Dot;
            penDirect.SetLineCap(LineCap.ArrowAnchor, LineCap.Flat, DashCap.Flat);
            if (pt1.X == pt2.X)//纵向线
            {
                if (pt1.X > OffsetDirectionLine)//画在左方
                {
                    g.DrawLine(penDirect,
                        pt1.X - OffsetDirectionLine,
                        pt1.Y,
                        pt2.X - OffsetDirectionLine,
                        pt2.Y);
                }
                else//画在右方
                {
                    g.DrawLine(penDirect,
                        pt1.X + OffsetDirectionLine,
                        pt1.Y,
                        pt2.X + OffsetDirectionLine,
                        pt2.Y);
                }
            }
            else//横向线
            {
                if (pt1.Y > OffsetDirectionLine)//画在上方
                {
                    g.DrawLine(penDirect,
                        pt1.X,
                        pt1.Y - OffsetDirectionLine,
                        pt2.X,
                        pt2.Y - OffsetDirectionLine
                        );
                }
                else//画在下方
                {
                    g.DrawLine(penDirect,
                        pt1.X,
                        pt1.Y + OffsetDirectionLine,
                        pt2.X,
                        pt2.Y + OffsetDirectionLine
                    );
                }
            }
            //释放资源
            penDirect.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 隐藏以前绘画之指示线,两个坐标均为存储的真实数据
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        private void HideDirectionLineInvalidate(Point pt1, Point pt2)
        {
            Point p1, p2;
            if (pt1.X == pt2.X)///纵向线
            {
                if (pt1.X > OffsetDirectionLine)//画在左方
                {
                    p1 = new Point(
                        pt1.X - OffsetDirectionLine,
                        pt1.Y);
                    p2 = new Point(
                        pt2.X - OffsetDirectionLine,
                        pt2.Y);

                }
                else//画在右方
                {
                    p1 = new Point(
                        pt1.X + OffsetDirectionLine,
                        pt1.Y);
                    p2 = new Point(
                        pt2.X + OffsetDirectionLine,
                        pt2.Y);
                }
            }
            else//横向线
            {
                if (pt1.Y > OffsetDirectionLine)//画在上方
                {
                    p1 = new Point(
                        pt1.X,
                        pt1.Y - OffsetDirectionLine);
                    p2 = new Point(
                        pt2.X,
                        pt2.Y - OffsetDirectionLine
                        );
                }
                else//画在下方
                {
                    p1 = new Point(
                        pt1.X,
                        pt1.Y + OffsetDirectionLine);
                    p2 = new Point(
                        pt2.X,
                        pt2.Y + OffsetDirectionLine
                        );
                }
            }
            CommonFuns.Invalidate(this, PenSize, p1, pt2, CurZoom);
        }

        /// <summary>
        /// 鼠标选取对象之响应函数
        /// </summary>
        /// <param name="e"></param>
        private void MouseLeftDown_RectSelect(MouseEventArgs e)
        {
            SecondRealPoint = FirstRealPoint;
            _isRectSelecting = true;
            
            List<PartitionLine> preSelectedLines = ListLine.GetSelectedLines();//得到所有已被选择的直线
            List<Rect> preSelectedRects = ListRect.GetSelectedRects();//得到所有已被选择之矩形
            List<Rect> curSelectedRects = new List<Rect>();//刚被选择的矩形
            List<Rect> disSelectedRects = new List<Rect>();
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            if (selectedRect != null)
            {
                if (selectedRect.IsSelected)//本身已经被选择
                {
                    if ((DrawPanel.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        disSelectedRects.Add(selectedRect);
                    }
                    else//已经被选择且没有按下ctrl,则此命令无效
                    {
                        //如果有被选择之直线，则继续响应移动直线之命令
                        MouseDown_MoveLine(e);
                        return;
                    }
                }
                else//本身还没有被选择
                {
                    InvalidateSelecting();
                    this.Update();
                    if ((DrawPanel.ModifierKeys & Keys.Control) == Keys.Control)//本身还没有被选择且按下了ctrl
                    {
                        curSelectedRects.Add(selectedRect);
                    }
                    else//本身还没有被选择且没有按下ctrl
                    {
                        curSelectedRects.Add(selectedRect);
                        disSelectedRects.AddRange(preSelectedRects);
                    }
                }
                
                SelectCommand selectCmd = new SelectCommand(TDPanel,
                   curSelectedRects,
                   disSelectedRects,
                   preSelectedLines,
                   new List<PartitionLine>());
               selectCmd.Execute();
               CommandList.AddCommand(selectCmd);
            }

           
        }

        /// <summary>
        /// 移动drawPanel的命令实现.
        /// </summary>
        /// <param name="e"></param>
        private void MouseLeftDown_PanelMove(MouseEventArgs e)
        {
            Cursor = _panelMovingCursor;
            if (TDPanel.DrawFrame.Width < Width + 2 * TDPanel.DrawFrame.MinSpace + (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize) ||
                TDPanel.DrawFrame.Height < Height + 2 * TDPanel.DrawFrame.MinSpace + (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))//不能移动
            {
                SecondRealPoint = new Point(e.X, e.Y);
                State = 1;
                Console.WriteLine("Down:e.X:" + e.X.ToString() + ",e.Y: " + e.Y.ToString());
            }

        }

        /// <summary>
        /// 鼠标左键选择对象
        /// </summary>
        private void MouseLeftDown_MouseSelect(MouseEventArgs e)
        {
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换

            List<PartitionLine> preSelectedLines = ListLine.GetSelectedLines();//得到所有已被选择的直线
            List<Rect> preSelectedRects = ListRect.GetSelectedRects();//得到所有已被选择之矩形
            List<PartitionLine> curSelectedLines = new List<PartitionLine>();//刚被选择的直线
            List<Rect> curSelectedRects = new List<Rect>();//刚被选择的矩形
            List<PartitionLine> disSelectedLines = new List<PartitionLine>();
            List<Rect> disSelectedRects = new List<Rect>();

            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            //初始化哪些对象应该被取消选择,哪些应该选择
            if (selectedRect == null)//如果选中的不是矩形,即为直线
            {
                if (selectedLine == null || DrawType == EnumDrawType.MouseSelectRect)
                {
                    return;
                }
                if (selectedLine.IsSelected)//本身已经被选择
                {
                    if ((DrawPanel.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        disSelectedLines.Add(selectedLine);
                    }
                    else//已经被选择且没有按下ctrl,则此命令无效
                    {
                        //如果有被选择之直线，则继续响应移动直线之命令
                        if (!_isRectSelecting)
                        {
                            MouseDown_MoveLine(e);
                        }
                        return;
                    }
                }
                else//本身还没有被选择
                {
                    if ((DrawPanel.ModifierKeys & Keys.Control) == Keys.Control)//本身还没有被选择且按下了ctrl
                    {
                        curSelectedLines.Add(selectedLine);
                    }
                    else//没有按下ctrl
                    {
                       curSelectedLines.Add(selectedLine);
                       disSelectedLines.AddRange(preSelectedLines);
                    }
                }
            }
            else//选中的是矩形
            {
                if (DrawType == EnumDrawType.MouseSelectLine)
                {
                    return;
                }
                _isRectSelecting = true;
            }
           
            SelectCommand selectCmd = new SelectCommand(TDPanel,
               curSelectedRects,
               disSelectedRects,
               curSelectedLines,
               disSelectedLines);
            selectCmd.Execute();
            CommandList.AddCommand(selectCmd);
           
            if (selectedLine != null)
            {
                MouseDown_MoveLine(e);
            }
        }

        /// <summary>
        /// 右键选择
        /// </summary>
        /// <param name="e"></param>
        private void MouseRightDown_Select(MouseEventArgs e)
        {
            List<PartitionLine> preSelectedLines = ListLine.GetSelectedLines();//得到所有已被选择的直线
            List<Rect> preSelectedRects = ListRect.GetSelectedRects();//得到所有已被选择之矩形
            List<PartitionLine> curSelectedLines = new List<PartitionLine>();//刚被选择的直线
            List<Rect> curSelectedRects = new List<Rect>();//刚被选择的矩形
            List<PartitionLine> disSelectedLines = new List<PartitionLine>();
            List<Rect> disSelectedRects = new List<Rect>();

            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            //初始化哪些对象应该被取消选择,哪些应该选择
            if (selectedRect == null)//如果选中的不是矩形,即为直线
            {
                if (selectedLine == null)
                {
                    return;
                }
                if (selectedLine.IsSelected)//本身已经被选择
                {
                    return;
                }
                else//本身还没有被选择
                {
                    curSelectedLines.Add(selectedLine);
                    disSelectedLines.AddRange(preSelectedLines);
                }
            }
            else//选中的是矩形
            {
                if (selectedRect.IsSelected)//本身已经被选择
                {
                    return;
                }
                else//本身还没有被选择
                {
                    curSelectedRects.Add(selectedRect);
                    disSelectedRects.AddRange(preSelectedRects);
                }
            }

            SelectCommand selectCmd = new SelectCommand(TDPanel,
                curSelectedRects,
                disSelectedRects,
                curSelectedLines,
                disSelectedLines);
            selectCmd.Execute();
            CommandList.AddCommand(selectCmd);
        }

        /// <summary>
        /// 鼠标选取并拖动以移动直线之响应函数
        /// </summary>
        /// <param name="e"></param>
        private void MouseDown_MoveLine(MouseEventArgs e)
        {
            SelectedLine = ListLine.GetSelectedLine();
            if (SelectedLine != null)
            {
                MoveToLine = new PartitionLine(
                    SelectedLine.Start,
                    SelectedLine.End,
                    SelectedLine.Position,
                    SelectedLine.IsRow);

                PartitionLine[] borderLines = ListLine.GetLineBorderLine(SelectedLine);
                FirstBorderLine = borderLines[0];
                SecondBorderLine = borderLines[1];

                if (SelectedLine.IsRow)
                {
                    this.Cursor = _hSplit;
                }
                else
                {
                    this.Cursor = _vSplit;
                }
                //查找包含FirstBorderLine和SecondBorderLine的之间的可能会和seletedLine吸附之直线
                AdsorbLines = GetAdsorbedLines(
                    SelectedLine, FirstBorderLine, SecondBorderLine);

                FirstRealPoint = BacktrackMouse(e);
                State = 1;
            }
            else
            {
                Cursor = _mouseSelectCursor;
                State = 0;
            }
        }

        /// <summary>
        /// 获取可被吸附的直线数组
        /// </summary>
        /// <param name="SelectedLine"></param>
        /// <param name="FirstBorderLine"></param>
        /// <param name="SecondBorderLine"></param>
        /// <returns></returns>
        protected List<PartitionLine> GetAdsorbedLines(PartitionLine selectedLine, PartitionLine firstBorderLine, PartitionLine secondBorderLine)
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            //先找到所有start或end相等之直线,再找其pos在firstBorderLine,secondBorderLine之间之直线
            FindLineByLine findByLine = new FindLineByLine(selectedLine);
            List<PartitionLine> adsorbedLines = allLines.FindAll(findByLine.PredicateStartEndEqual);

            findByLine = new FindLineByLine(firstBorderLine);
            adsorbedLines = adsorbedLines.FindAll(findByLine.PredicatePosGt);
            findByLine = new FindLineByLine(secondBorderLine);
            adsorbedLines = adsorbedLines.FindAll(findByLine.PredicatePosLt);

            //加上firstBorderLine,secondBorderLine本身也需要吸附:)
            adsorbedLines.Add(firstBorderLine);
            adsorbedLines.Add(secondBorderLine);

            return adsorbedLines;
        }

        /// <summary>
        /// 得到可被吸附之直线,如果没有,则为空
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected PartitionLine GetAdsorbableLine(Point pt)
        {
            AdsorbLines.Sort(new CompareLPDistance(pt));
            if (AdsorbLines[0].IsRow)
            {
                if (Math.Abs(AdsorbLines[0].Position - pt.Y) <= this.AutoAdsorb)
                {
                    return AdsorbLines[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (Math.Abs(AdsorbLines[0].Position - pt.X) <= this.AutoAdsorb)
                {
                    return AdsorbLines[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 画线命令下鼠标的移动
        /// </summary>
        /// <param name="e"></param>
        private void MouseMove_DrawLine(MouseEventArgs e)
        {
            Point realE = BacktrackMouse(e);//存储e中的坐标转换后的实际大小
            SecondInitPoint = new Point(realE.X, realE.Y);
            Rect _curRect = ListRect.GetSelectedRect(SecondInitPoint, 0);
            if ( _curRect !=null && (_curRect.IsLocked|| _curRect.HasSnip))
            {
                //MessageBox.Show("当前要分割的矩形已经被锁定(或含有页面片)!");
                MessageService.Show("${res:tmpltDesignTip.drawPanel}", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                State = 0;
                Invalidate();
                FirstDirectLineCtl.Hide();
                SecondDirectLineCtl.Hide();
                return;
            }

            Point secondP = SecondRealPoint;
            Point startP = StartRealPoint;
            Point endP = EndRealPoint;
            int oldMin, oldMax, newMin, newMax;
            bool isChangDirect = false;

            #region 当为横向直线时

            if (Math.Abs(realE.X - FirstRealPoint.X) >= Math.Abs(realE.Y - FirstRealPoint.Y))
            {
                FirstDirectLineCtl.IsRow = false;
                SecondDirectLineCtl.IsRow = false;
                //是否需要滚动
                if (DrawFrame.DfHScrollBar.Enabled)
                {
                    if (realE.X + Location.X > DrawFrame.Width - DrawFrame.DfVScrollBar.Width - DrawFrame.DfHScrollBar.SmallChange &&
                        DrawFrame.DfHScrollBar.Value <
                        DrawFrame.DfHScrollBar.Maximum + 1 - DrawFrame.DfHScrollBar.LargeChange - 1)
                    {
                        DrawFrame.DfHScrollBar.Value += 1;
                    }
                    else if (realE.X + Location.X < DesignPanel.RulerSize + DrawFrame.DfHScrollBar.SmallChange &&
                        DrawFrame.DfHScrollBar.Value > 1 + 1)
                    {
                        DrawFrame.DfHScrollBar.Value -= 1;
                    }
                }
                
                SecondRealPoint = new Point(realE.X, FirstRealPoint.Y);
               
                FindLineByPoint findPointPartion = new FindLineByPoint(FirstRealPoint);
                List<PartitionLine>  resultLines = VPartionLines.FindAll(findPointPartion.PredicatePointTo);
                resultLines.Sort(new CompareLPDistance(FirstRealPoint));

                FirstLine = resultLines[0];

                FirstDirectLineCtl.IsRow = FirstLine.IsRow;
                SecondDirectLineCtl.IsRow = FirstLine.IsRow;

                StartRealPoint = new Point(FirstLine.Position, FirstRealPoint.Y);
                //求得PointEnd;
                for (int i = 1; i < resultLines.Count; i++)
                {
                    if (resultLines[i].IsRow == FirstLine.IsRow &&
                        ((FirstLine.Position - FirstRealPoint.X >= 0 && resultLines[i].Position - FirstRealPoint.X <= 0) ||
                         (FirstLine.Position - FirstRealPoint.X <= 0 && resultLines[i].Position - FirstRealPoint.X >= 0)))
                    {
                        EndRealPoint = new Point(resultLines[i].Position, FirstRealPoint.Y);
                        SecondLine = resultLines[i];
                        break;
                    }
                }                
                
                if ((SecondRealPoint.X <= FirstLine.Position && SecondRealPoint.X >= SecondLine.Position) ||
                        (SecondRealPoint.X >= FirstLine.Position && SecondRealPoint.X <= SecondLine.Position))//如果实线第二点在两边界线之间
                {
                    EndRealPoint = new Point(SecondLine.Position, FirstRealPoint.Y);
                }
                else
                {
                    findPointPartion = new FindLineByPoint(SecondRealPoint);
                    resultLines = VPartionLines.FindAll(findPointPartion.PredicatePointTo);
                    resultLines.Remove(FirstLine);//终点中不能含有起点之直线
                    resultLines.Sort(new CompareLPDistance(SecondRealPoint));
                    EndRealPoint = new Point(resultLines[0].Position, FirstRealPoint.Y);
                }

                FirstDirectLineCtl.Location = new Point(FirstRealPoint.X, FirstRealPoint.Y - DirectionLineCtl.Length / 2);
                SecondDirectLineCtl.Location = new Point(SecondInitPoint.X, FirstInitPoint.Y - DirectionLineCtl.Length / 2);
                FirstDirectLineCtl.Show();
                SecondDirectLineCtl.Show();

            }

            #endregion

            #region 当为纵向线时

            else
            {
                FirstDirectLineCtl.IsRow = true;
                SecondDirectLineCtl.IsRow = true;
                //纵向滚动条
                if (DrawFrame.DfVScrollBar.Enabled)
                {
                    if (realE.Y + Location.Y > DrawFrame.Height - DrawFrame.DfVScrollBar.Width - DrawFrame.DfVScrollBar.SmallChange &&
                        DrawFrame.DfVScrollBar.Value <
                        DrawFrame.DfVScrollBar.Maximum + 1 - DrawFrame.DfVScrollBar.LargeChange - 1)
                    {
                        DrawFrame.DfVScrollBar.Value += 1;
                    }
                    else if (realE.Y + Location.Y < DesignPanel.RulerSize + DrawFrame.DfVScrollBar.SmallChange &&
                        DrawFrame.DfVScrollBar.Value > 1 + 1)
                    {
                        DrawFrame.DfVScrollBar.Value -= 1;
                    }
                }

                
                SecondRealPoint = new Point(FirstRealPoint.X, realE.Y);

                /////////////////////////////////////////////////////////////////////////////
                
                FindLineByPoint findPointPartion = new FindLineByPoint(FirstRealPoint);
                List<PartitionLine> resultLines = HPartionLines.FindAll(findPointPartion.PredicatePointTo);
                resultLines.Sort(new CompareLPDistance(FirstRealPoint));
                FirstLine = resultLines[0];

                FirstDirectLineCtl.IsRow = FirstLine.IsRow;
                SecondDirectLineCtl.IsRow = FirstLine.IsRow;

                StartRealPoint = new Point(FirstRealPoint.X, FirstLine.Position);
                //求得PointEnd;
                for (int i = 1; i < resultLines.Count; i++)
                {
                    if (resultLines[i].IsRow == FirstLine.IsRow &&
                        ((FirstLine.Position - FirstRealPoint.Y >= 0 && resultLines[i].Position - FirstRealPoint.Y <= 0) ||
                         (FirstLine.Position - FirstRealPoint.Y <= 0 && resultLines[i].Position - FirstRealPoint.Y >= 0)))
                    {
                        EndRealPoint = new Point(FirstRealPoint.X, resultLines[i].Position);
                        SecondLine = resultLines[i];
                        break;
                    }
                }
            
                //找到指示线之终点线并绘画出来
                if ((SecondRealPoint.Y <= FirstLine.Position && SecondRealPoint.Y >= SecondLine.Position) ||
                    (SecondRealPoint.Y >= FirstLine.Position && SecondRealPoint.Y <= SecondLine.Position))//如果实线第二点在两边界线之间
                {
                    EndRealPoint = new Point(FirstRealPoint.X, SecondLine.Position);
                }
                else
                {
                    //找到指示线之终点线并绘画出来
                    findPointPartion = new FindLineByPoint(SecondRealPoint);
                    resultLines = HPartionLines.FindAll(findPointPartion.PredicatePointTo);
                    resultLines.Remove(FirstLine);//终点中不能含有起点之直线
                    resultLines.Sort(new CompareLPDistance(SecondRealPoint));
                    EndRealPoint = new Point(FirstRealPoint.X, resultLines[0].Position);
                }
                
                FirstDirectLineCtl.Location = new Point(FirstRealPoint.X - DirectionLineCtl.Length / 2, FirstRealPoint.Y);
                SecondDirectLineCtl.Location = new Point(FirstInitPoint.X - DirectionLineCtl.Length / 2, SecondInitPoint.Y);
                FirstDirectLineCtl.Show();
                SecondDirectLineCtl.Show();
            }

            #endregion

            #region 找到old最大最小值
            if (FirstLine.IsRow)
            {
                oldMin = FirstRealPoint.Y < secondP.Y ? FirstRealPoint.Y : secondP.Y;
                oldMax = FirstRealPoint.Y > secondP.Y ? FirstRealPoint.Y : secondP.Y;
                oldMin = oldMin < startP.Y ? oldMin : startP.Y;
                oldMax = oldMax > startP.Y ? oldMax : startP.Y;

                oldMin = oldMin < endP.Y ? oldMin : endP.Y;
                oldMax = oldMax > endP.Y ? oldMax : endP.Y;
            }
            else
            {
                oldMin = FirstRealPoint.X < secondP.X ? FirstRealPoint.X : secondP.X;
                oldMax = FirstRealPoint.X > secondP.X ? FirstRealPoint.X : secondP.X;
                oldMin = oldMin < startP.X ? oldMin : startP.X;
                oldMax = oldMax > startP.X ? oldMax : startP.X;

                oldMin = oldMin < endP.X ? oldMin : endP.X;
                oldMax = oldMax > endP.X ? oldMax : endP.X;
            }
            #endregion

            #region 找到现在的最大最小值
            if (FirstLine.IsRow)
            {
                newMin = FirstRealPoint.Y < SecondRealPoint.Y ? FirstRealPoint.Y : SecondRealPoint.Y;
                newMax = FirstRealPoint.Y > SecondRealPoint.Y ? FirstRealPoint.Y : SecondRealPoint.Y;
                newMin = newMin < StartRealPoint.Y ? newMin : StartRealPoint.Y;
                newMax = newMax > StartRealPoint.Y ? newMax : StartRealPoint.Y;

                newMin = newMin < EndRealPoint.Y ? newMin : EndRealPoint.Y;
                newMax = newMax > EndRealPoint.Y ? newMax : EndRealPoint.Y;
            }
            else
            {
                newMin = FirstRealPoint.X < SecondRealPoint.X ? FirstRealPoint.X : SecondRealPoint.X;
                newMax = FirstRealPoint.X > SecondRealPoint.X ? FirstRealPoint.X : SecondRealPoint.X;
                newMin = newMin < StartRealPoint.X ? newMin : StartRealPoint.X;
                newMax = newMax > StartRealPoint.X ? newMax : StartRealPoint.X;

                newMin = newMin < EndRealPoint.X ? newMin : EndRealPoint.X;
                newMax = newMax > EndRealPoint.X ? newMax : EndRealPoint.X;
            }
            #endregion

            //如果不重叠，则刷新新区域
            if (!isChangDirect && newMax < oldMax)
            {
                if (FirstLine.IsRow)
                {
                    CommonFuns.Invalidate(this, PenSize, new Point(FirstRealPoint.X, oldMax), new Point(FirstRealPoint.X, newMax), CurZoom);
                }
                else
                {
                    CommonFuns.Invalidate(this, PenSize, new Point(oldMax, FirstRealPoint.Y), new Point(newMax, FirstRealPoint.Y), CurZoom);
                }
            }
            else if (!isChangDirect && newMin > oldMin)
            {
                if (FirstLine.IsRow)
                {
                    CommonFuns.Invalidate(this, PenSize, new Point(FirstRealPoint.X, oldMin), new Point(FirstRealPoint.X, newMin), CurZoom);
                }
                else
                {
                    CommonFuns.Invalidate(this, PenSize, new Point(oldMin, FirstRealPoint.Y), new Point(newMin, FirstRealPoint.Y), CurZoom);
                }
            }

            DrawMoveLine(secondP, startP, endP, FirstLine);
        }

        /// <summary>
        /// 绘画出实线与指示线
        /// </summary>
        /// <param name="secondP"></param>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        protected void DrawMoveLine(Point secondP, Point startP, Point endP, PartitionLine firstLine)
        {
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            Pen penDirect = new Pen(DirectLineColor, PenSize);
            penDirect.SetLineCap(LineCap.ArrowAnchor, LineCap.Flat, DashCap.Flat);
            Pen penLine = new Pen(PenColor, PenSize);

            g.DrawLine(penLine, FirstRealPoint, SecondRealPoint);
            g.DrawLine(penDirect, FirstRealPoint, StartRealPoint);
            g.DrawLine(penDirect, EndRealPoint, SecondRealPoint);

            if (firstLine.IsRow)
            {
                if ((FirstRealPoint.Y > secondP.Y && startP.Y < endP.Y) ||
                    (FirstRealPoint.Y < secondP.Y && startP.Y > endP.Y))
                {
                    g.DrawLine(penLine, FirstRealPoint, SecondRealPoint);
                }
            }
            else
            {
                if ((FirstRealPoint.X > secondP.X && startP.X < endP.X) ||
                    (FirstRealPoint.X < secondP.X && startP.X > endP.X))
                {
                    g.DrawLine(penLine, FirstRealPoint, SecondRealPoint);
                }
            }

            penDirect.Dispose();
            penLine.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 鼠标移动以拖动直线时的响应函数
        /// </summary>
        /// <param name="e"></param>
        private void MouseMove_MoveLine(MouseEventArgs e)
        {
            if (!SelectedLine.CanMove || GetRectsByLineIsLocked(SelectedLine))
            {
                return;
            }
            
            Point realE = BacktrackMouse(e);

            Graphics g = this.CreateGraphics();

            g.ScaleTransform(CurZoom, CurZoom);

            Pen showPen = new Pen(PenColor, PenSize);//用户用于绘画最新呈现出来的直线

            Point[] destPoints = new Point[3];

            bool hasSnip = GetRectsByLineIsHasSnip(SelectedLine);


            ///当为横向直线时
            if (SelectedLine.IsRow)
            {
                ///看看有无直线可能被吸附
                SecondRealPoint = new Point(realE.X, realE.Y);

                PartitionLine adsorbableLine = GetAdsorbableLine(SecondRealPoint);
                if ((DrawPanel.ModifierKeys & Keys.Control) != Keys.Control &&
                    adsorbableLine != null && !hasSnip)
                {
                    SecondRealPoint = new Point(realE.X, adsorbableLine.Position);
                }
                else///如果按了ctrl,则不吸附,或没有按ctrl却无可吸附之直线
                {
                    if (realE.Y > SecondBorderLine.Position)///当超出右或下边界时
                    {
                        SecondRealPoint = new Point(realE.X, SecondBorderLine.Position);
                    }
                    else if (realE.Y < FirstBorderLine.Position)
                    {
                        SecondRealPoint = new Point(realE.X, FirstBorderLine.Position);
                    }
                    else///没有与边界直线重合
                    {
                        SecondRealPoint = new Point(realE.X, realE.Y);
                    }
                }

                //已经打到正确的终点,如果和之前的不同
                if (MoveToLine.Position != SecondRealPoint.Y)
                {
                    //如果和界限不重合，则不覆盖以前的直线
                    if (MoveToLine.Position != FirstBorderLine.Position && MoveToLine.Position != SecondBorderLine.Position)
                    {

                        destPoints[0] = new Point(MoveToLine.Start + PenSize, MoveToLine.Position);
                        destPoints[1] = new Point(MoveToLine.End, MoveToLine.Position);
                        destPoints[2] = new Point(MoveToLine.Start + PenSize, MoveToLine.Position + 1);
                        Rectangle srcRect = new Rectangle(MoveToLine.Start, MoveToLine.Position, MoveToLine.End - MoveToLine.Start, 1);

                        this.Invalidate(srcRect);
                    }
                    MoveToLine.Position = SecondRealPoint.Y;
                    g.DrawLine(showPen, MoveToLine.Start + PenSize, MoveToLine.Position, MoveToLine.End, MoveToLine.Position);
                }
            }
            else
            {
                ///看看有无直线可能被吸附
                SecondRealPoint = new Point(realE.X, realE.Y);
                PartitionLine adsorbableLine = GetAdsorbableLine(SecondRealPoint);
                if ((DrawPanel.ModifierKeys & Keys.Control) != Keys.Control &&
                    adsorbableLine != null && !hasSnip)
                {
                    SecondRealPoint = new Point(adsorbableLine.Position, realE.Y);
                }
                else
                {
                    if (realE.X > SecondBorderLine.Position)///与右或下边界重合
                    {
                        SecondRealPoint = new Point(SecondBorderLine.Position, realE.Y);
                    }
                    else if (realE.X < FirstBorderLine.Position)///与上或左边界重合
                    {
                        SecondRealPoint = new Point(FirstBorderLine.Position, realE.Y);
                    }
                    else///没有与边界直线重合
                    {
                        SecondRealPoint = new Point(realE.X, realE.Y);
                    }
                }
                //已经打到正确的终点,如果和之前的不同
                if (MoveToLine.Position != SecondRealPoint.X)
                {
                    //如果和界限不重合，则不覆盖以前的直线
                    if (MoveToLine.Position != FirstBorderLine.Position && MoveToLine.Position != SecondBorderLine.Position)
                    {

                        destPoints[0] = new Point(MoveToLine.Position, MoveToLine.Start + PenSize);
                        destPoints[2] = new Point(MoveToLine.Position, MoveToLine.End);
                        destPoints[1] = new Point(MoveToLine.Position + 1, MoveToLine.Start + PenSize);
                        Rectangle srcRect = new Rectangle(MoveToLine.Position, MoveToLine.Start + PenSize, PenSize, MoveToLine.End - MoveToLine.Start - PenSize);

                        this.Invalidate(srcRect);
                    }

                    MoveToLine.Position = SecondRealPoint.X;
                    g.DrawLine(showPen, MoveToLine.Position, MoveToLine.Start + PenSize, MoveToLine.Position, MoveToLine.End);
                }
            }
            ///释放资源
            showPen.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 判断和选定线段相关的矩形是否有被锁定的
        /// </summary>
        /// <param name="SelectedLine"></param>
        /// <returns></returns>
        private bool GetRectsByLineIsLocked(PartitionLine SelectedLine)
        {
            PartitionLine selecedLine = null;
            List<PartitionLine> allLines = new List<PartitionLine>();
            allLines.AddRange(ListLine.VPartionLines);
            allLines.AddRange(ListLine.HPartionLines);
            foreach (PartitionLine line in allLines)
            {
                if (line.Equals(SelectedLine))
                {
                    selecedLine = line;
                }
            }

            if (selecedLine == null)
            {
                return false;
            }

            foreach (Rect _rect in ListRect.GetToUnLockedRects())
            {
                foreach (PartitionLine _line in _rect.GetBorderlines())
                {
                    if (selecedLine.Equals(_line))
                    {
                        return true;
                    }
                    if (selecedLine.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in selecedLine.ChildLines)
                        {
                            if (childLine.Equals(_line))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断和选定线段相关的矩形是否有含有页面片的
        /// </summary>
        /// <param name="SelectedLine"></param>
        /// <returns></returns>
        private bool GetRectsByLineIsHasSnip(PartitionLine SelectedLine)
        {
            PartitionLine selecedLine = null;
            List<PartitionLine> allLines = new List<PartitionLine>();
            allLines.AddRange(ListLine.VPartionLines);
            allLines.AddRange(ListLine.HPartionLines);
            foreach (PartitionLine line in allLines)
            {
                if (line.Equals(SelectedLine))
                {
                    selecedLine = line;
                }
            }

            if (selecedLine == null)
            {
                return false;
            }

            foreach (Rect _rect in ListRect.SnipRectList)
            {
                if (!_rect.HasSnip)
                {
                    break;
                }
                foreach (PartitionLine _line in _rect.GetBorderlines())
                {
                    if (selecedLine.Equals(_line))
                    {
                        return true;
                    }
                    if (selecedLine.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in selecedLine.ChildLines)
                        {
                            if (childLine.Equals(_line))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 鼠标在选择状态下的划过(未按下鼠标)
        /// </summary>
        /// <param name="e"></param>
        private void MouseMove_Selecting(MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            //初始化哪些对象应该被取消选择,哪些应该选择
            if (selectedRect == null)//如果选中的不是矩形,即为直线
            {
                if (selectedLine != null)
                {
                    FindLineByLine findLineByLine = new FindLineByLine(selectedLine);
                    List<PartitionLine> lines = new List<PartitionLine>();
                    List<PartitionLine> baseLines = selectedLine.IsRow ? VPartionLines : HPartionLines;
                    foreach (PartitionLine  line in baseLines)
                    {
                        if (findLineByLine.PredicateJoined(line))
                            lines.Add(line);
                    }
                    if(lines.Count == 0)
                        Cursor = selectedLine.IsRow ? _hSplit : _vSplit;
                }
                if (selectedLine != null && selectedLine != _lastSelectingLine && !selectedLine.IsSelected )
                {
                    //绘画鼠标划过的效果
                    if (selectedLine.IsRow)
                    { 
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_selectedLineColor,PenSize),selectedLine.Start,selectedLine.Position,selectedLine.End,selectedLine.Position);
                    }
                    else
                    {
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_selectedLineColor, PenSize), selectedLine.Position,selectedLine.Start,  selectedLine.Position,selectedLine.End );
                    }

                    _lastSelectingLine = selectedLine;
                    _lastSelectingRect = null;
                }
                else if (selectedLine != null && selectedLine.IsSelected)
                {
                    //刷新上次的划过效果
                    InvalidateSelecting();
                }
            }
            else//选中的是矩形
            {
                Cursor = _mouseSelectCursor;
                //if (!selectedRect.IsSelected && selectedRect != _lastSelectingRect)//本身已经被选择
                //{
                    _lastSelectingRect = selectedRect;
                    _curSelectingRect = selectedRect;
                    _lastSelectingLine = null;
                    Invalidate();
                    this.Update();
                //}
                //else if (selectedRect.IsSelected)
                //{
                //    InvalidateSelecting();
                //}
            }
            g.Dispose();
        }

        private void MouseMove_SelectingLine(MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            if (selectedRect == null)//如果选中的不是矩形,即为直线
            {
                if (selectedLine != null)
                {
                    FindLineByLine findLineByLine = new FindLineByLine(selectedLine);
                    List<PartitionLine> lines = new List<PartitionLine>();
                    List<PartitionLine> baseLines = selectedLine.IsRow ? VPartionLines : HPartionLines;
                    foreach (PartitionLine line in baseLines)
                    {
                        if (findLineByLine.PredicateJoined(line))
                            lines.Add(line);
                    }
                    if (lines.Count == 0)
                        Cursor = selectedLine.IsRow ? _hSplit : _vSplit;
                }
                if (selectedLine != null && selectedLine != _lastSelectingLine && !selectedLine.IsSelected)
                {
                    //绘画鼠标划过的效果
                    if (selectedLine.IsRow)
                    {
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_selectedLineColor, PenSize), selectedLine.Start, selectedLine.Position, selectedLine.End, selectedLine.Position);
                    }
                    else
                    {
                        InvalidateSelecting();
                        this.Update();
                        g.DrawLine(new Pen(_selectedLineColor, PenSize), selectedLine.Position, selectedLine.Start, selectedLine.Position, selectedLine.End);
                    }

                    _lastSelectingLine = selectedLine;
                    _lastSelectingRect = null;
                }
                else if (selectedLine != null && selectedLine.IsSelected)
                {
                    //刷新上次的划过效果
                    InvalidateSelecting();
                }
            }
            else
                Cursor = _mouseSelectCursor;
            g.Dispose();
        }

        private void MouseMove_SelectingRect(MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);
            FirstRealPoint = this.BacktrackMouse(e);//坐标转换
            PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
            Rectangle rectangle;
            Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            if (selectedRect != null)//如果选中的是矩形
            {
                //Cursor = Cursors.Arrow;
                if (!selectedRect.IsSelected && selectedRect != _lastSelectingRect)//本身已经被选择
                {
                    rectangle = new Rectangle(
                        selectedRect.X,
                        selectedRect.Y,
                        selectedRect.Width,
                        selectedRect.Height);

                    InvalidateSelecting();
                    this.Update();
                    g.FillRectangle(new SolidBrush(SelectingRectColor), rectangle);

                    _lastSelectingRect = selectedRect;
                    _lastSelectingLine = null;
                }
                else if (selectedRect.IsSelected)
                {
                    InvalidateSelecting();
                }
            }
            g.Dispose();
        }

        private void MouseMove_ZoomChangBigger(MouseEventArgs e)
        {
            
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {            
            base.OnMouseEnter(e);
        }

        protected void InvalidateSelecting()
        {
            Rectangle rectangle;
            if (_lastSelectingLine != null)
            {
                if (_lastSelectingLine.IsRow)
                {
                    rectangle = new Rectangle(
                        _lastSelectingLine.Start,
                        _lastSelectingLine.Position - SelectPrecision,
                        _lastSelectingLine.End - _lastSelectingLine.Start + PenSize,
                        SelectPrecision * 2 + PenSize
                        );
                }
                else
                {
                    rectangle = new Rectangle(
                        _lastSelectingLine.Position - SelectPrecision,
                        _lastSelectingLine.Start,
                         SelectPrecision * 2 + PenSize,
                        _lastSelectingLine.End - _lastSelectingLine.Start + PenSize
                        );
                }

                CommonFuns.Invalidate(this, rectangle, CurZoom);
            }
            else if (_lastSelectingRect != null)
            {
                rectangle = new Rectangle(
                    _lastSelectingRect.X,
                    _lastSelectingRect.Y,
                    _lastSelectingRect.Width,
                    _lastSelectingRect.Height);

                //rectangle = new Rectangle(
                //    _lastSelectingRect.X + SelectPrecision + 4 * PenSize,
                //    _lastSelectingRect.Y + SelectPrecision + 4 * PenSize,
                //    _lastSelectingRect.Width - 2 * SelectPrecision - 8 * PenSize,
                //    _lastSelectingRect.Height - 2 * SelectPrecision - 8 * PenSize);

                CommonFuns.Invalidate(this, rectangle, CurZoom);
            }
        }

        /// <summary>
        /// 矩形框选择对象
        /// </summary>
        private void MouseMove_RectSelect(MouseEventArgs e)
        {
            if (!_isRectSelecting)
                return;
            Rect starRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
            InvalidateSelecting();
            Update();
            SecondRealPoint = BacktrackMouse(e);
            DrawSelectRect();//画选择框
        }

        /// <summary>
        /// 鼠标移动面板
        /// </summary>
        private void MouseMove_PanelMove(MouseEventArgs e)
        {
            #region MyRegion
            //if (Math.Abs(e.X - SecondRealPoint.X) >= 1)
            //{
            //    int x = LastLocation.X -( e.X - SecondRealPoint.X);
            //    int xMin = TDPanel.DrawFrame.MinSpace - (TDPanel.DrawFrame.DfHScrollBar.Maximum - TDPanel.DrawFrame.DfHScrollBar.LargeChange + 1);
            //    x = x <= TDPanel.DrawFrame.MinSpace ? x : TDPanel.DrawFrame.MinSpace;
            //    x = x >= xMin ? x : xMin;
            //    if (x != Location.X)
            //    {
            //       // TDPanel.DrawFrame.DfHScrollBar.Value -= (x - Location.X);
            //        Location = new Point(x, Location.Y);

            //    }
            //}

            //int xMax = TDPanel.DrawFrame.MinSpace;
            //int yMax = xMax;
            //int xMin = TDPanel.DrawFrame.MinSpace - (TDPanel.DrawFrame.DfHScrollBar.Maximum - TDPanel.DrawFrame.DfHScrollBar.LargeChange + 1);
            //int yMin = TDPanel.DrawFrame.MinSpace - (TDPanel.DrawFrame.DfVScrollBar.Maximum - TDPanel.DrawFrame.DfVScrollBar.LargeChange + 1);

            //int x = Location.X + (e.X - SecondRealPoint.X);
            //int y = Location.Y + (e.Y - SecondRealPoint.Y);

            //if (Math.Abs(e.X - SecondRealPoint.X) >= 2 &&
            //    TDPanel.DrawFrame.Width < 
            //        Width + 2 * TDPanel.DrawFrame.MinSpace + TDPanel.DrawFrame.( DfVScrollBar.Width + DesignPanel.RulerSize ))
            //{
            //    x = x <= xMax ? x : xMax;
            //    x = x >= xMin ? x : xMin;

            //    if (x != Location.X)
            //    {
            //        TDPanel.DrawFrame.DfHScrollBar.Value -= (x - Location.X);
            //        Location = new Point(x, Location.Y);
            //    }
            //}

            //if (Math.Abs(e.Y - SecondRealPoint.Y) >= 2 &&
            //     TDPanel.DrawFrame.Height < 
            //        Height + 2 * TDPanel.DrawFrame.MinSpace + TDPanel.DrawFrame.( DfVScrollBar.Width + DesignPanel.RulerSize ))
            //{
            //    y = y <= yMax ? y : yMax;
            //    y = y >= yMin ? y : yMin;

            //    if (y!=Location.Y)
            //    {
            //        TDPanel.DrawFrame.DfVScrollBar.Value -= (y - Location.Y);
            //        Location = new Point(Location.X, y);
            //    }

            //}

            //if (x != Location.X && y!=Location.Y)
            //{
            //    TDPanel.DrawFrame.DfHScrollBar.Value += (x - Location.X);
            //    TDPanel.DrawFrame.DfVScrollBar.Value += (y - Location.Y);
            //    Location = new Point(x, y);
            //}
            //else if (x != Location.X)
            //{
            //    TDPanel.DrawFrame.DfHScrollBar.Value += (x - Location.X);
            //    Location = new Point(x, y);
            //}
            //else if (y!=Location.Y)
            //{
            //    TDPanel.DrawFrame.DfVScrollBar.Value += (y - Location.Y);
            //    Location = new Point(x, y);
            //}
            #endregion

            Cursor = _panelMovingCursor;

            int xValue = TDPanel.DrawFrame.DfHScrollBar.Value - (e.X - SecondRealPoint.X);
            int xOldValue = TDPanel.DrawFrame.DfHScrollBar.Value;
            int xMax = TDPanel.DrawFrame.DfHScrollBar.Maximum - TDPanel.DrawFrame.DfHScrollBar.LargeChange + 1;
            if (Math.Abs(e.X - SecondRealPoint.X) >= 2 &&
                 TDPanel.DrawFrame.Width < Width + 2 * TDPanel.DrawFrame.MinSpace + (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                xValue = xValue <= xMax ? xValue : xMax;
                xValue = xValue >= 0 ? xValue : 0;
                if (xValue != xOldValue)
                {
                    TDPanel.DrawFrame.DfHScrollBar.Value = xValue;
                }
            }

            int yValue = TDPanel.DrawFrame.DfVScrollBar.Value - (e.Y - SecondRealPoint.Y);
            int yMax = TDPanel.DrawFrame.DfVScrollBar.Maximum - TDPanel.DrawFrame.DfVScrollBar.LargeChange + 1;
            int yOldValue = TDPanel.DrawFrame.DfVScrollBar.Value;
            if (Math.Abs(e.Y - SecondRealPoint.Y) >= 2 &&
                 TDPanel.DrawFrame.Height < Height + 2 * TDPanel.DrawFrame.MinSpace + (TDPanel.DrawFrame.DfVScrollBar.Width + DesignPanel.RulerSize))
            {
                yValue = yValue <= yMax ? yValue : yMax;
                yValue = yValue >= 0 ? yValue : 0;

                if (yValue != yOldValue)
                {
                    TDPanel.DrawFrame.DfVScrollBar.Value = yValue;
                }
            }
            SecondRealPoint = new Point(e.X - (xOldValue - xValue), e.Y - (yOldValue - yValue));
        }

        /// <summary>
        /// 绘画矩形选择框
        /// </summary>
        protected void DrawSelectRect()
        {
            Graphics g = CreateGraphics();

            g.ScaleTransform(CurZoom, CurZoom);
            Invalidate();
            Update();

            Pen pen = new Pen(PenColor, PenSize);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawRectangle(pen, Utility.Draw.PointToRectangle(FirstRealPoint, SecondRealPoint));
            g.Dispose();

            Rectangle srcRect = Utility.Draw.PointToRectangle(FirstRealPoint, SecondRealPoint);
            srcRect.Location = new Point(srcRect.Location.X + PenSize, srcRect.Location.Y + PenSize);
            srcRect.Width -= (PenSize);
            srcRect.Height -= (PenSize);
            Invalidate(srcRect);
            Update();
        }

        /// <summary>
        /// 鼠标弹起时的响应代码
        /// </summary>
        /// <param name="e"></param>
        private void MouseUp_DrawLine(MouseEventArgs e)
        {
            if (State == 0)
            {
                FirstDirectLineCtl.Hide();
                SecondDirectLineCtl.Hide();
                return;
            }
            if (FirstLine == null)
            {
                return;
            }

            ///当为横向直线时
            if (!FirstLine.IsRow)
            {
                ///将直线插入切割容器中
                AddLine(StartRealPoint.X, EndRealPoint.X, StartRealPoint.Y, true);
            }
            else
            {
                ///将直线插入切割容器中
                AddLine(StartRealPoint.Y, EndRealPoint.Y, StartRealPoint.X, false);
            }
            CommonFuns.Invalidate(this, PenSize * BoldPenTimes, SecondRealPoint, EndRealPoint, CurZoom);
            CommonFuns.Invalidate(this, PenSize * BoldPenTimes, FirstRealPoint, StartRealPoint, CurZoom);

            FirstDirectLineCtl.Hide();
            SecondDirectLineCtl.Hide();
        }

        /// <summary>
        /// 移动直线时鼠标弹起之响应函数 
        /// </summary>
        private void MouseUp_MoveLine()
        {
            if (SelectedLine == null)
                return;
            if (MoveToLine.Position != SelectedLine.Position)///如果有偏移
            {
                ///生成并处理移动直线命令
                MoveLineCommand moveLineCommand =
                    new MoveLineCommand(TDPanel,
                    SelectedLine,
                    MoveToLine.Position,
                    FirstBorderLine,
                    SecondBorderLine);
                moveLineCommand.Execute();
                CommandList.AddCommand(moveLineCommand);
            }
        }

        private void KeyDown_MouseDeleteLine()
        {
            this.DrawType = EnumDrawType.MouseDeleteLine;
            _tempDrawType = EnumDrawType.MouseDeleteLine;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
                DrawType = _tempDrawType;
            if (e.KeyCode == Keys.Menu)
            {
                if (DrawType == EnumDrawType.ZoomBigger)
                {
                    Cursor = _bigZoom;
                }
            }
            base.OnKeyUp(e);
        }

        /// <summary>
        /// 删除所有被选择的直线
        /// </summary>
        protected void DeleteLines()
        {
            Graphics g = this.CreateGraphics();
            g.ScaleTransform(CurZoom, CurZoom);

            List<PartitionLine> selectedLines = ListLine.GetSelectedLines();
            List<PartitionLine> disSelecteableLines = ListLine.GetDisDeletableLine();
            if (disSelecteableLines.Count >= 1)///在已被选择的直线中,存在不能被删除的直线
            {
             //   MessageBox.Show("存在不能被删除之直线!");
                MessageService.Show("${res:tmpltDesignTip.deleteLine}", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                foreach (PartitionLine line in disSelecteableLines)
                {
                    DrawLineFlash(line, g);
                }
                //  g.Dispose();
                return;
            }
            if (selectedLines.Count >= 1)
            {
                for (int i = 0; i < selectedLines.Count; i++)
                {
                    DeleteLine(selectedLines[i]);
                }
            }
        }

        /// <summary>
        /// 绘画闪烁之直线
        /// </summary>
        /// <param name="line"></param>
        protected internal void DrawLineFlash(PartitionLine line, Graphics g)
        {
            int i = 0;

            System.Windows.Forms.Timer timerFlash = new System.Windows.Forms.Timer();
            timerFlash.Start();
            timerFlash.Interval = 100;

            Pen penHide = new Pen(this.BackColor, this.PenSize * BoldPenTimes);
            //Pen penDraw = new Pen(this.SelecteLinePenColor, this.PenSize * BoldPenTimes);

            timerFlash.Tick += delegate
            {
                i++;
                if (i == 5)
                {
                    timerFlash.Stop();
                    return;
                }
                if (i % 2 == 0)
                {
                    if (line.IsRow)
                    {
                        DrawLine(line);
                    }
                    else
                    {
                        DrawLine(line);
                    }
                }
                else
                {
                    if (line.IsRow)
                    {
                        g.DrawLine(penHide, line.Start, line.Position, line.End, line.Position);
                    }
                    else
                    {
                        g.DrawLine(penHide, line.Position, line.Start, line.Position, line.End);
                    }
                }
            };
            // penHide.Dispose();
            //penDraw.Dispose();
        }

        /// <summary>
        /// 打开右键菜单时响应
        /// </summary>
        protected virtual void RectMenu_Opening(object sender, CancelEventArgs e)
        {
            //放大
            if (CurZoom >= 16F)
            {
                this.ContextMenuStrip.Items["zoomIn"].Enabled = false;
            }
            else
            {
                this.ContextMenuStrip.Items["zoomIn"].Enabled = true;
            }

            //缩小
            if (CurZoom <= 0.5F)
            {
                this.ContextMenuStrip.Items["zoomOut"].Enabled = false;
            }
            else
            {
                this.ContextMenuStrip.Items["zoomOut"].Enabled = true;
            }

            ///撤销
            if (CommandList.IsExistUndo())
            {
                this.ContextMenuStrip.Items["undo"].Enabled = true;
                this.ContextMenuStrip.Items["undo"].Text = "撤销";// +this.CommandList.CurCommand.Value.GetCommandInfo();
            }
            else
            {
                this.ContextMenuStrip.Items["undo"].Enabled = false;
                this.ContextMenuStrip.Items["undo"].Text = "不能撤销";
            }

            ///重做
            if (CommandList.IsExistRedo())
            {
                this.ContextMenuStrip.Items["redo"].Enabled = true;

                LinkedListNode<BaseCommand> reDoCommandNode = CommandList.CurCommand;
                if (CommandList.CurCommand == null)
                {
                    reDoCommandNode = CommandList.Commands.First;
                }
                else
                {
                    reDoCommandNode = CommandList.CurCommand.Next;
                }
                this.ContextMenuStrip.Items["redo"].Text = "重做" + reDoCommandNode.Value.GetCommandInfo();
            }
            else
            {
                this.ContextMenuStrip.Items["redo"].Enabled = false;
                this.ContextMenuStrip.Items["redo"].Text = "不能重做";
            }

            ///删除直线
            if (ListLine.IsExistSelectedLine())
            {
                this.ContextMenuStrip.Items["deleteLine"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["deleteLine"].Enabled = false;
            }

            ///合并页面片
            if (ListRect.GetSelectedRects().Count >= 2)
            {
                this.ContextMenuStrip.Items["mergeRect"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["mergeRect"].Enabled = false;
            }
            ///分割页面片
            if (ListRect.IsSingleUnLocked())
            {
                this.ContextMenuStrip.Items["partRect"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["partRect"].Enabled = false;
            }

            ///锁定矩形
            if (ListRect.IsExistLockableRect())
            {
                this.ContextMenuStrip.Items["lockRect"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["lockRect"].Enabled = false;
            }

            ///解锁矩形
            if (ListRect.IsExistUnLockableRect())
            {
                this.ContextMenuStrip.Items["disLockRect"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["disLockRect"].Enabled = false;
            }
            ///删除内部页面片
            if (ListRect.IsHasSnipData())
            {
                this.ContextMenuStrip.Items["deleteSnip"].Enabled = true;
            }
            else
            {
                this.ContextMenuStrip.Items["deleteSnip"].Enabled = false;
            }
            //使用当前背景
            if (BackImage == null)
            {
                //this.ContextMenuStrip.Items["useCurrentImg"].Enabled = false;
            }
            else
            {
                //this.ContextMenuStrip.Items["useCurrentImg"].Enabled = true;
            }

        }

        #endregion

        #region 数据处理算法实现

        /// <summary>
        /// 添加一条直线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="position"></param>
        /// <param name="isRow"></param>
        protected void AddLine(int start, int end, int position, bool isRow)
        {
            ///线的合法性处理
            if (start == end) return;
            ///当终点比起点小时,交换
            if (start > end)
            {
                int tmp = start;
                start = end;
                end = tmp;
            }

            ///是否和已有直线重合
            PartitionLine line = new PartitionLine(start, end, position, isRow);
            FindLineByLine findByLine = new FindLineByLine(line);
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            List<PartitionLine> overLapLines = allLines.FindAll(findByLine.PredicateOverlap);
            List<PartitionLine> resultLines = new List<PartitionLine>();
            if (overLapLines.Count > 0)
            {
                resultLines = line.GetNotOverlapLine(overLapLines);
            }
            else///没有重合
            {
                resultLines.Add(line);
            }

            if (resultLines.Count < 1)///如果切割之后的直线为空,则无需处理,返回.
            {
                return;
            }

            AddLineCommand addLineCommand = new AddLineCommand(TDPanel, line, resultLines);

            addLineCommand.Execute();
            TDPanel.CommandList.AddCommand(addLineCommand);
        }

        /// <summary>
        /// 删除直线
        /// </summary>
        protected void DeleteLine(PartitionLine line)
        {
            ///先判断删除之直线是否有效
            if (line == null) return;

            FindRectByLine findRect = new FindRectByLine(line);
            ///找到所有以line做边界线的矩形
            List<Rect> resultRects = this.ListRect.SnipRectList.FindAll(findRect.BorderLinePredicate);
            List<NeighbourRect> neighbourRectList = this.ListRect.RectListToNeighbour(resultRects, line);
            DeleteLineEventArgs deleteLineEvent = new DeleteLineEventArgs(neighbourRectList);
            TDPanel.OnDeleteLine(deleteLineEvent);///触发事件的执行
            ///
            if (!deleteLineEvent.Cancel)///如果确定执行
            {
                ///执行删除操作,根据deleteLineEvent的返回值来初始化command.
                DeleteLineCommand deleteCommand =
                    new DeleteLineCommand(
                    TDPanel, line, deleteLineEvent.HoldedRectList, deleteLineEvent.RemovedRectList);

                deleteCommand.Execute();
                TDPanel.CommandList.AddCommand(deleteCommand);///添加命令
            }
            else///动作取消
            {

            }
        }
        #endregion

        #region 内部函数

        /// <summary>
        /// 鼠标移动画板
        /// </summary>
        private void KeyDown_PanelMove()
        {
            this.DrawType = EnumDrawType.PanelMove;
            _tempDrawType = EnumDrawType.PanelMove;
        }

        /// <summary>
        /// 快捷键切换到画线状态
        /// </summary>
        private void KeyDown_DrawLine()
        {
            this.DrawType = EnumDrawType.DrawLine;
            _tempDrawType = EnumDrawType.DrawLine;
        }

        /// <summary>
        /// 鼠标选择对象模式
        /// </summary>
        private void KeyDown_MouseSelect()
        {
            DrawType = EnumDrawType.MouseSelect;
            _tempDrawType = EnumDrawType.MouseSelect;
        }

        /// <summary>
        /// 鼠标选择对象为矩形模式
        /// </summary>
        private void KeyDown_MouseSelectRect()
        {
            DrawType = EnumDrawType.MouseSelectRect;
            _tempDrawType = EnumDrawType.MouseSelectRect;
        }

        /// <summary>
        /// 鼠标选择对象为线段模式
        /// </summary>
        private void KeyDown_MouseSelectLine()
        {
            DrawType = EnumDrawType.MouseSelectLine;
            _tempDrawType = EnumDrawType.MouseSelectLine;
        }

        /// <summary>
        /// 锁定对象命令
        /// </summary>
        private void KeyDown_Lock()
        {
            List<PartitionLine> selectedLines = ListLine.GetSelectedLines();
            List<Rect> selectedRects = ListRect.GetSelectedRects();

            if (selectedRects.Count <= 0)
            {
                return;
            }

            if (selectedRects[0].IsLocked)
            {
                DisLockRects();
            }
            else
            {
                LockRects();
            }
        }

        /// <summary>
        /// 启用鼠标点击放大（缩小）功能
        /// </summary>
        private void KeyDown_ZoomChang()
        {
            DrawType = EnumDrawType.ZoomChang;
        }

        /// <summary>
        /// 启用鼠标点击放大功能
        /// </summary>
        private void KeyDown_ZoomBigger()
        {
            DrawType = EnumDrawType.ZoomBigger;
            Cursor = _bigZoom;
            _tempDrawType = EnumDrawType.ZoomBigger;
        }

        /// <summary>
        /// 启用鼠标点击缩小功能
        /// </summary>
        private void KeyDown_ZoomSmaller()
        {
            DrawType = EnumDrawType.ZoomSmaller;
        }

        /// <summary>
        /// 得到矩形数组中的未被锁定之直线
        /// </summary>
        /// <param name="lockedRects"></param>
        /// <returns></returns>
        private List<PartitionLine> GetRectsUnLockedLines(List<Rect> rects)
        {
            List<PartitionLine> unLockedLines = new List<PartitionLine>();
            foreach (Rect rect in rects)
            {
                List<PartitionLine> borderLines = rect.GetBorderlines();
                foreach (PartitionLine line in borderLines)
                {
                    List<PartitionLine> findBorderLines = TDPanel.DrawPanel.ListLine.GetLine(line);
                    foreach (PartitionLine borderLine in findBorderLines)
                    {
                        if (!borderLine.IsLocked)
                        {
                            unLockedLines.Add(borderLine);
                        }
                    }
                }
            }
            return unLockedLines;
        }

        /// <summary>
        /// 得到矩形数组中的被锁定之直线
        /// </summary>
        /// <param name="lockedRects"></param>
        /// <returns></returns>
        private List<PartitionLine> GetRectsLockedLines(List<Rect> rects)
        {
            List<PartitionLine> lockedLines = new List<PartitionLine>();
            foreach (Rect rect in rects)
            {
                List<PartitionLine> borderLines = rect.GetBorderlines();
                foreach (PartitionLine line in borderLines)
                {
                    List<PartitionLine> findBorderLines = TDPanel.DrawPanel.ListLine.GetLine(line);
                    foreach (PartitionLine borderLine in findBorderLines)
                    {
                        if (borderLine.IsLocked)
                        {
                            lockedLines.Add(borderLine);
                        }
                    }
                }
            }
            return lockedLines;
        }

        #endregion

        #region 公共成员函数接口

        /// <summary>
        /// 合并当前选择矩形
        /// </summary>
        internal bool MergeSelectedRects(MergeRectCommand command)
        {
            bool value = true;
            if (!command.IsRedo)
            {
                List<Rect> selectedRects = ListRect.GetSelectedRects();

                MergeRectEventArgs mergeRectEvent = new MergeRectEventArgs(selectedRects);
                TDPanel.OnMergeRect(mergeRectEvent);

                if (!mergeRectEvent.Cancel)///如果确定执行
                {

                    Rect checkedRect = mergeRectEvent.HoldRect;

                    if (checkedRect == null)
                        return false;

                    ///保存以用于撤销
                    command.LeftRect = new Rectangle(
                        checkedRect.X,
                        checkedRect.Y,
                        checkedRect.Width,
                        checkedRect.Height
                        );

                    ///将先中保留的矩形大小放大
                    checkedRect.X = command.BoundaryRect.X;
                    checkedRect.Y = command.BoundaryRect.Y;
                    checkedRect.Width = command.BoundaryRect.Width;
                    checkedRect.Height = command.BoundaryRect.Height;

                    for (int i = 0; i < selectedRects.Count; i++)
                    {
                        if (selectedRects[i] == checkedRect)
                        {
                            selectedRects[i].IsDeleted = false;
                            command.AddedRects.Add(checkedRect);
                            continue;
                        }
                        selectedRects[i].IsDeleted = true;
                        command.RemovedRects.Add(selectedRects[i]);
                    }
                    CommandList.AddCommand(command);
                }
                else 
                { return false; }
            }
            else
            {
                command.AddedRects[0].MergeMutiRects(command);
            }
            return value;
        }

        /// <summary>
        /// 撤销合并矩形
        /// </summary>
        internal void UnMergeSelectedRects(MergeRectCommand command)
        {
            command.AddedRects[0].PartMutiRects(command);
        }

        /// <summary>
        /// 返回被选中的单选框索引
        /// </summary>
        /// <param name="groupBox"></param>
        /// <returns></returns>
        protected int GetChedIndex(GroupBox groupBox)
        {
            int i = 0;
            foreach (Control ctrl in groupBox.Controls)
            {
                if (((RadioButton)ctrl).Checked)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// 放大画板
        /// </summary>
        protected void ZoomIn()
        {
            if (CurZoom == ZoomData.MaxZoom)
            {
                SelectPrecision = CurZoom >= 1 ? (int)(CurrentPrecision / CurZoom) : (int)(CurrentPrecision * CurZoom);
                return;
            }
            //LastZoom = CurZoom;
            CurZoom = ZoomData.ZoomIn(CurZoom);
            SelectPrecision = CurZoom >= 1 ? (int)(CurrentPrecision / CurZoom) : (int)(CurrentPrecision * CurZoom);
        }

        /// <summary>
        /// 缩小画板
        /// </summary>
        protected void ZoomOut()
        {
            if (CurZoom == ZoomData.MinZoom)
            {
                SelectPrecision = CurZoom >= 1 ? (int)(CurrentPrecision / CurZoom) : (int)(CurrentPrecision * CurZoom);
                return;
            }
            CurZoom = ZoomData.ZoomOut(CurZoom);
            SelectPrecision = CurZoom >= 1 ? (int)(CurrentPrecision / CurZoom) : (int)(CurrentPrecision * CurZoom);
        }

        /// <summary>
        /// 实际大小
        /// </summary>
        protected void InitZoom()
        {
            CurZoom = 1;
            SelectPrecision = CurrentPrecision;
            this.Invalidate();
            this.Update();
        }

        /// <summary>
        /// 锁定被选择之矩形
        /// </summary>
        protected void LockRects()
        {
            List<Rect> lockedRects = ListRect.GetToLockedRects();
            if (lockedRects.Count > 0)
            {
                LockRectCommand lockRectCommand = new LockRectCommand(TDPanel, lockedRects);
                List<PartitionLine> lockedLines = GetRectsUnLockedLines(lockedRects);
                lockRectCommand.LockedLines = lockedLines;
                lockRectCommand.Execute();
                CommandList.AddCommand(lockRectCommand);
            }
        }

        /// <summary>
        /// 解除锁定矩形
        /// </summary>
        protected void DisLockRects()
        {
            List<Rect> unLockedRects = ListRect.GetToUnLockedRects();
            if (unLockedRects.Count > 0)
            {
                DisLockRectCommand disLockRectCommand = new DisLockRectCommand(TDPanel, unLockedRects);
                List<PartitionLine> unLockedLines = GetRectsLockedLines(unLockedRects);
                disLockRectCommand.UnLockedLines = unLockedLines;
                disLockRectCommand.Execute();
                CommandList.AddCommand(disLockRectCommand);
            }
        }

        /// <summary>
        /// 删除选中矩形
        /// </summary>
        protected void DeleteSnip()
        {
            List<Rect> selectRects = ListRect.GetSelectedRects();
            if (selectRects.Count > 0)
            {
                if (MessageService.Show(StringParserService.Parse("${res:tmpltDesign.DrawPanel.message.deleteSnip}"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (Rect rect in selectRects)
                    {
                        OnRectDeleted(new EventArgs<Rect>(rect));
                    }
                }
            }
        }

        //public void ResetCurRect()
        //{
        //    Point _moues = MousePosition;
        //    Graphics g = CreateGraphics();
        //    g.ScaleTransform(CurZoom, CurZoom);
        //    FirstRealPoint = this.BacktrackMouse(_moues.X,_moues.Y);//坐标转换
        //    Rectangle rectangle;
        //    Rect selectedRect = ListRect.GetSelectedRect(FirstRealPoint, SelectPrecision);
        //    PartitionLine selectedLine = ListLine.GetSelectedLine(FirstRealPoint, SelectPrecision, !((DrawPanel.ModifierKeys & Keys.Shift) == Keys.Shift));
        //    //初始化哪些对象应该被取消选择,哪些应该选择
        //    if (selectedRect == null)//如果选中的不是矩形,即为直线
        //    {
        //        if (selectedLine != null)
        //        {
        //            FindLineByLine findLineByLine = new FindLineByLine(selectedLine);
        //            List<PartitionLine> lines = new List<PartitionLine>();
        //            List<PartitionLine> baseLines = selectedLine.IsRow ? VPartionLines : HPartionLines;
        //            foreach (PartitionLine line in baseLines)
        //            {
        //                if (findLineByLine.PredicateJoined(line))
        //                    lines.Add(line);
        //            }
        //            if (lines.Count == 0)
        //                Cursor = selectedLine.IsRow ? _hSplit : _vSplit;
        //        }
        //        if (selectedLine != null && selectedLine != _lastSelectingLine && !selectedLine.IsSelected)
        //        {
        //            //绘画鼠标划过的效果
        //            if (selectedLine.IsRow)
        //            {
        //                InvalidateSelecting();
        //                this.Update();
        //                g.DrawLine(new Pen(_selectedLineColor, PenSize), selectedLine.Start, selectedLine.Position, selectedLine.End, selectedLine.Position);
        //            }
        //            else
        //            {
        //                InvalidateSelecting();
        //                this.Update();
        //                g.DrawLine(new Pen(_selectedLineColor, PenSize), selectedLine.Position, selectedLine.Start, selectedLine.Position, selectedLine.End);
        //            }

        //            _lastSelectingLine = selectedLine;
        //            _lastSelectingRect = null;
        //        }
        //        else if (selectedLine != null && selectedLine.IsSelected)
        //        {
        //            //刷新上次的划过效果
        //            InvalidateSelecting();
        //        }
        //    }
        //    else//选中的是矩形
        //    {
        //        Cursor = _mouseSelectCursor;
                
        //        {
        //            ////绘画鼠标划过的效果
        //            //rectangle = new Rectangle(
        //            //    selectedRect.X,
        //            //    selectedRect.Y,
        //            //    selectedRect.Width,
        //            //    selectedRect.Height);

        //            //InvalidateSelecting();
        //            //this.Update();

        //            //Zhenghao:2008.03.14, need repair

        //           // Color c = SelectingRectColor;// Color.FromArgb(70, Color.Blue);
        //            //if (BackImage != null)
        //            //{
        //            //    Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height);
        //            //    Graphics gBmp = Graphics.FromImage(bmp);
        //            //    gBmp.DrawImage(BackImage, new Rectangle(0, 0, bmp.Width, bmp.Height), rectangle.X, rectangle.Y, bmp.Width, bmp.Height, GraphicsUnit.Pixel);
        //            //    gBmp.FillRectangle(new SolidBrush(c), 0, 0, bmp.Width, bmp.Height);
        //            //    g.DrawImage(bmp, rectangle.X, rectangle.Y);

        //            //}
        //            //else
        //            //{
        //            //    Bitmap bmpNull = new Bitmap(rectangle.Width, rectangle.Height);
        //            //    Graphics gNull = Graphics.FromImage(bmpNull);
        //            //    gNull.FillRectangle(new SolidBrush(c), 0, 0, bmpNull.Width, bmpNull.Height);
        //            //    g.DrawImage(bmpNull, rectangle.X, rectangle.Y);
        //            //}

        //            //finish
        //            //g.FillRectangle(new SolidBrush(SelectingRectColor), rectangle);
        //            DrawSelectingRect(g);
        //            _lastSelectingRect = selectedRect;
        //            _curSelectingRect = selectedRect;
        //            _lastSelectingLine = null;
        //        }
                
        //    }
        //    g.Dispose();

        //}

        #endregion

        #region 事件声明与定义
        /// <summary>
        /// 删除所选矩形时
        /// </summary>
        public event EventHandler<EventArgs<Rect>> RectDeleted;
        /// <summary>
        /// 删除所选矩形时
        /// </summary>
        protected virtual void OnRectDeleted(EventArgs<Rect> e)
        {
            if (RectDeleted != null)
            {
                RectDeleted(this, e);
            }
        }

        // 2. 下面的委托类型定义了接受者必须实现的回调方法原型
        public delegate void ChangeZoomEventHandler(Object sender, EventArgs args);

        public delegate void ChangDrawTypeEventHandler(Object sender, EventArgs e);

        // 3. 事件成员
        public event ChangeZoomEventHandler ChangeZoomEvent;

        public event ChangDrawTypeEventHandler ChangDrawTypeEvent;

        public event EventHandler SelectingRectEvent;

        // 4. 下面的受保护虚方法负责通知事件的登记对象
        /// <summary>
        /// 执行zoomChange事件
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnChangeZoom(EventArgs e)
        {
            if (ChangeZoomEvent != null)
            {
                ChangeZoomEvent(this, e);
            }
        }

        /// <summary>
        /// 执行drawTypeChang事件
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnChangDrawType(EventArgs e)
        {
            if (DrawType != EnumDrawType.MouseSelectRect && DrawType != EnumDrawType.MouseSelect)
            {
                this.Invalidate();
            }
            if (ChangDrawTypeEvent!=null)
            {
                ChangDrawTypeEvent(this, e);
            }
        }

        /// <summary>
        /// 选择矩形时
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSelectingRect(EventArgs e)
        {
            if (SelectingRectEvent != null)
            {
                SelectingRectEvent(this, e);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursorFromFile(string fileName);
        public static Cursor LoadCursor(string fileName)
        {
            return new Cursor(LoadCursorFromFile(fileName));
        }

        #endregion

        #region 事件

        // 2. 下面的委托类型定义了接受者必须实现的回调方法原型
        public delegate void MouseRulerEventHandler(
            Object sender, MouseEventArgs args);

        // 3. 事件成员
        public event MouseRulerEventHandler MouseRulerEvent;//通知标尺画指示线事件


        // 4. 下面的受保护虚方法负责通知事件的登记对象
        /// <summary>
        /// 触发事件的执行
        /// 此函数将被DrawPenel中的deleteLine函数调用
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseRuler(MouseEventArgs e)
        {

            // 有对象登记事件吗?
            if (MouseRulerEvent != null)
            {
                // 如果有，则通知委托链表上的所有对象
                MouseRulerEvent(this, e);
            }
        }

        #endregion       

        //#region ResourcesReader
        //public string GetText(string key)
        //{
        //    return ResourcesReader.GetText(key, this);
        //}

        //public Icon GetIcon(string key)
        //{
        //    return ResourcesReader.GetIcon(key, this);
        //}

        //public Image GetImage(string key)
        //{
        //    return ResourcesReader.GetImage(key, this);
        //}

        //public Cursor GetCursor(string key)
        //{
        //    return ResourcesReader.GetCursor(key, this);
        //}

        //public byte[] GetBytes(string key)
        //{
        //    return ResourcesReader.GetBytes(key, this);
        //}
        //#endregion
    }
}