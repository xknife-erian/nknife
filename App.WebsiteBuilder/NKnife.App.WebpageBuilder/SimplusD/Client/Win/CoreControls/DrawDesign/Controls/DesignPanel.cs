using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 设计器画板
    /// </summary>
    public partial class DesignPanel : UserControl
    {
        #region 字段成员定义

        private bool _isRulerHide = false;

        #endregion

        #region 属性定义

        /// <summary>
        /// 获取或设置画板的文档
        /// </summary>
        public XmlDocument TmpltDoc { get; set; }

        /// <summary>
        /// 获取或设置是否被修改
        /// </summary>
        public bool Modified 
        { get; set; }

        /// <summary>
        /// 获取画板控件
        /// </summary>
        public DrawPanel DrawPanel
        {
            get { return DrawFrame.DrawPanel; }
        }

        /// <summary>
        /// 获取画框控件
        /// </summary>
        public DrawFrame DrawFrame { get; private set; }

        /// <summary>
        /// 标尺是否隐藏
        /// </summary>
        public bool IsRulerHide
        {
            get { return _isRulerHide; }
            set
            {
                if (value && _isRulerHide == false)
                {
                    HideRuler();
                    _isRulerHide = value;
                }
                else if (value == false && _isRulerHide)
                {
                    ShowRuler();
                    _isRulerHide = value;
                }
            }
        }

        /// <summary>
        /// 横向标尺
        /// </summary>
        internal HRuler HRuler { get; private set; }

        /// <summary>
        /// 纵向标尺
        /// </summary>
        internal VRuler VRuler { get; private set; }

        /// <summary>
        /// 横向宽度方向画板的空白距离
        /// </summary>
        public int WidthSpace
        {
            get
            {
                return Width - DrawPanel.Width - 2 * RulerSize;
            }
        }

        /// <summary>
        /// 纵高高度方向画板的空白距离
        /// </summary>
        public int HeightSpace
        {
            get
            {
                return Height - DrawPanel.Height - 2 * RulerSize;
            }
        }

        /// <summary>
        /// 标尺的厚度:即垂直标尺的宽度;水平标尺的高度
        /// </summary>
        public static int RulerSize { get; private set; }

        /// <summary>
        /// 命令历史记录
        /// </summary>
        public CommandList CommandList { get; set; }

        /// <summary>
        /// 绘画类型状态,0为非绘画状态,1为画直线,2为鼠标选择对象,3为矩形拖动以选择对象选择
        /// </summary>
        public EnumDrawType DrawType
        {
            get { return DrawPanel.DrawType; }
            set { DrawPanel.DrawType = value; }
        }

        /// <summary>
        /// 获取或设置是否包含正文型的页面片
        /// </summary>
        public bool HasContentSnip { get; set; }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DesignPanel(int drawPanelWidth, int drawPanelHeight, Image backImage)
        {
            #region 设置控件之显示属性

            this.SetStyle(ControlStyles.Selectable, true);
            this.Select();///获取焦点    

            #endregion

            #region 初始化动态生成之成员

            //设定属性的初始值
            Modified = false;
            CommandList = new CommandList();
            RulerSize = 15;
            HasContentSnip = false;

            //drawFrame初始化
            DrawFrame = CreateDrawFrame(this, drawPanelWidth, drawPanelHeight, backImage);
            DrawFrame.Dock = DockStyle.Fill;
            this.Controls.Add(DrawFrame);

            //HRuler
            HRuler = CreateHRuler(this);
            HRuler.BringToFront();
            HRuler.BackColor = Color.White;
            HRuler.Height = RulerSize;
            HRuler.Width = 500;
            //      HRuler.Dock = DockStyle.Top;
            HRuler.Location = new Point(DesignPanel.RulerSize, 0);
            this.Controls.Add(HRuler);


            // VRuler
            VRuler = CreateVRuler(this);
            VRuler.BringToFront();
            VRuler.BackColor = Color.White;
            VRuler.Width = RulerSize;
            VRuler.Height = 100;
            VRuler.Location = new Point(0, DesignPanel.RulerSize);
            // VRuler.Dock = DockStyle.Left;

            this.Controls.Add(VRuler);

           // DrawFrame.DfVScrollBar.BrinoFront();
            DrawFrame.DfHScrollBar.BringToFront();

            InitEvents();

            InitializeComponent();

            IsRulerHide = !SoftwareOption.TmpltDesigner.ShowRuler;

            #endregion
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvents()
        {
            DrawFrame.SizeChanged += new EventHandler(HRuler.DrawFrame_SizeChanged);
            DrawFrame.SizeChanged += new EventHandler(VRuler.DrawFrame_SizeChanged);

            DrawFrame.DfHScrollBar.ValueChanged += new EventHandler(HRuler.DrawFrame_ScrollValueChanged);
            DrawFrame.DfVScrollBar.ValueChanged += new EventHandler(VRuler.DrawFrame_ScrollValueChanged);

            DrawPanel.MouseRulerEvent += new DrawPanel.MouseRulerEventHandler(HRuler.DrawPanel_MouseRulerEvent);
            DrawPanel.MouseRulerEvent += new DrawPanel.MouseRulerEventHandler(VRuler.DrawPanel_MouseRulerEvent);

            DrawPanel.ChangeZoomEvent += new DrawPanel.ChangeZoomEventHandler(HRuler.DrawPanel_ChangeZoomEvent);
            DrawPanel.ChangeZoomEvent += new DrawPanel.ChangeZoomEventHandler(VRuler.DrawPanel_ChangeZoomEvent);
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 显示标尺
        /// </summary>
        private void ShowRuler()
        {
            HRuler.Show();
            VRuler.Show();
            DesignPanel.RulerSize = 15;
            DrawPanel.Location = new Point(
                DrawPanel.Location.X + DesignPanel.RulerSize,
                DrawPanel.Location.Y + DesignPanel.RulerSize);
        }

        /// <summary>
        /// 隐藏标尺
        /// </summary>
        private void HideRuler()
        {
            HRuler.Hide();
            VRuler.Hide();
            DrawPanel.Location = new Point(
                DrawPanel.Location.X - 15,
                DrawPanel.Location.Y - 15);
            DesignPanel.RulerSize = 0;
        }

        #endregion

        #region 消息处理

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            //g.ScaleTransform(1,1,MatrixOrder.

            //绘画左上角的那个正方形
            g.DrawRectangle(Pens.Black, 0, 0, 13, 13);

            g.Dispose();
            // 调用基类 OnPaint
            base.OnPaint(pe);

            g.Dispose();
        }

        /// <summary>
        /// 鼠标按下响应
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Select();
            base.OnMouseDown(e);
        }

        #endregion

        #region 工厂模式的动态生成函数

        /// <summary>
        /// 将被其派生类重写
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public virtual Rect CreateRect(int x, int y, int width, int height)
        {
            return new Rect(x, y, width, height);
        }

        public virtual Rect CreateRect(int x, int y, int width, int height, Rect rect)
        {
            return new Rect(x, y, width, height);
        }

        /// <summary>
        /// 工厂模式动态创建DrawFrame
        /// </summary>
        protected virtual DrawFrame CreateDrawFrame(DesignPanel tD, int width, int height, Image backImage)
        {
            return new DrawFrame(tD, width, height, backImage);
        }

        protected virtual HRuler CreateHRuler(DesignPanel tD)
        {
            return new HRuler(tD);
        }

        protected virtual VRuler CreateVRuler(DesignPanel tD)
        {
            return new VRuler(tD);
        }

        #endregion

        #region 事件声明与定义

        // 2. 下面的委托类型定义了接受者必须实现的回调方法原型
        public delegate void DeleleLineEventHandler(
            Object sender, DeleteLineEventArgs args);
        public delegate void MergeRectEventHandler(
            Object sender, MergeRectEventArgs args);
        public delegate void PartRectEventHandler(
            Object sender, PartRectEventArgs args);

        // 3. 事件成员
        public event DeleleLineEventHandler DeleteLineEvent;
        public event MergeRectEventHandler MergeRectEvent;
        public event PartRectEventHandler PartRectEvent;

        // 4. 下面的受保护虚方法负责通知事件的登记对象

        /// <summary>
        /// 触发事件的执行
        /// 此函数将被DrawPenel中的deleteLine函数调用
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnDeleteLine(DeleteLineEventArgs e)
        {

            // 有对象登记事件吗?
            if (DeleteLineEvent != null)
            {
                // 如果有，则通知委托链表上的所有对象
                DeleteLineEvent(this, e);
            }
        }

        /// <summary>
        /// 执行MergeRect事件
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnMergeRect(MergeRectEventArgs e)
        {
            if (MergeRectEvent != null)
            {
                MergeRectEvent(this, e);
            }
        }

        /// <summary>
        /// 执行分割矩形事件
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnPartRect(PartRectEventArgs e)
        {
            if (PartRectEvent != null)
            {
                PartRectEvent(this, e);
            }
        }
        
        #endregion
    }

    #region 事件声明与定义

    // 1. 传递给事件接受者的类型定义信息
    public class DeleteLineEventArgs : CancelEventArgs
    {
        private List<NeighbourRect> _neighbourRectList = new List<NeighbourRect>();
        private List<Rect> _holdedRectList = new List<Rect>();
        private List<Rect> _removedRectList = new List<Rect>();



        public List<NeighbourRect> NeighbourRectList
        {
            get { return _neighbourRectList; }
            set { _neighbourRectList = value; }
        }

        public List<Rect> HoldedRectList
        {
            get { return _holdedRectList; }
            set { _holdedRectList = value; }
        }

        public List<Rect> RemovedRectList
        {
            get { return _removedRectList; }
            set { _removedRectList = value; }
        }

        public DeleteLineEventArgs(List<NeighbourRect> neighbourRectList)
        {
            Cancel = true;
            NeighbourRectList = neighbourRectList;
        }
    }

    public class MergeRectEventArgs : CancelEventArgs
    {
        List<Rect> _selectedRects;
        

        public Rect HoldRect
        {
            get;
            set;
        }

        public List<Rect> SelectedRects
        {
            get { return _selectedRects; }
            set { _selectedRects = value; }
        }

        public MergeRectEventArgs(List<Rect> selectedRects)
        {
            Cancel = true;
            SelectedRects = selectedRects;
        }
    }

    public class PartRectEventArgs : CancelEventArgs
    {
        Rect _selectedRect;
        private int _partNum = 2;
        bool _isRow = true;

        /// <summary>
        /// 分割数
        /// </summary>
        public int PartNum
        {
            get { return _partNum; }
            set { _partNum = value; }
        }

        public Rect SelectedRect
        {
            get { return _selectedRect; }
            set { _selectedRect = value; }
        }

        public bool IsRow
        {
            get { return _isRow; }
            set { _isRow = value; }
        }

        public PartRectEventArgs(Rect selectedRect)
        {
            Cancel = true;
            SelectedRect = selectedRect;
        }
    }

    #endregion
}
