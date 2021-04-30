using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.App.Sudoku.Common;

namespace NKnife.App.Sudoku.Controls
{
    /// <summary>
    /// Description of SudokuBox.
    /// </summary>
    internal class SudokuCell : Control
    {
        /// <summary>
        /// 每单元格的宽度，一般来讲单元格均为正方形，故高度也与宽度相等
        /// </summary>
        private int _CellWidth;

        /// <summary>
        /// 本控件在无事件状态下显示，保存为一个Bitmap
        /// </summary>
        private Bitmap _ControlBmp;

        /// <summary>
        /// 单元格所在界面的背景颜色，以便于投影渐变更加自然
        /// </summary>
        private Color _ParentBackColor;

        /// <summary>
        /// 获取单元格所对应数据封装对象
        /// </summary>
        /// <value>The DoCell.</value>
        public SuDoCell DoCell
        {
            get { return _DoCell; }
        }
        /// <summary>
        /// 单元格所对应数据封装对象
        /// </summary>
        private SuDoCell _DoCell;

        /// <summary>
        /// 获取单元格所在父级面板
        /// </summary>
        /// <value>The parent panel.</value>
        public SudokuPanel ParentPanel
        {
            get { return _ParentPanel; }
            set { _ParentPanel = value; }
        }
        /// <summary>
        /// 单元格所在父级面板
        /// </summary>
        private SudokuPanel _ParentPanel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row">单元格所在行</param>
        /// <param name="col">单元格所在列</param>
        /// <param name="boxWidth">单元格的宽度(与高度相等)</param>
        /// <param name="parentBackColor">单元格所在界面的背景颜色，以便于投影渐变更加自然</param>
        public SudokuCell(int row, int col, int cellWidth, Color parentBackColor)
        {
            this._DoCell = new SuDoCell(row, col);
            this._CellWidth = cellWidth;
            this._ParentBackColor = parentBackColor;

            InitializeComponent();

            this.InitializeContextMenuStrip();
        }

        /// <summary>
        /// 初始化SudokuCell控件
        /// </summary>
        private void InitializeComponent()
        {
            _Components = new Container();
            this.Size = new Size(_CellWidth, _CellWidth);
        }

        /// <summary>
        /// 初始化SudokuCell控件的上下文菜单
        /// </summary>
        private void InitializeContextMenuStrip()
        {
            if (this.ContextMenuStrip == null)
            {
                this.ContextMenuStrip = new ContextMenuStrip();
                this.ContextMenuStrip.SuspendLayout();

                //铅笔菜单
                ToolStripMenuItem item = new ToolStripMenuItem("Pencil");
                for (int i = 1; i <= 9; i++)
                {
                    ToolStripMenuItem numberItem = new ToolStripMenuItem(i.ToString());
                    item.DropDownItems.Add(numberItem);
                }
                this.ContextMenuStrip.Items.Add(item);

                //分隔线
                this.ContextMenuStrip.Items.Add(new ToolStripSeparator());

                //填写数字
                for (int i = 1; i <= 9; i++)
                {
                    ToolStripMenuItem numberItem = new ToolStripMenuItem(i.ToString());
                    numberItem.Click += new EventHandler(NumberItem_Click);
                    this.ContextMenuStrip.Items.Add(numberItem);
                }

                //分隔线
                this.ContextMenuStrip.Items.Add(new ToolStripSeparator());

                //清除数字
                ToolStripMenuItem clearItem = new ToolStripMenuItem("Clear");
                clearItem.Click += new EventHandler(ClearItem_Click);
                this.ContextMenuStrip.Items.Add(clearItem);

                this.ContextMenuStrip.ResumeLayout(false);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            GraphicsHelper.CellTextPaint(this.CreateGraphics(), this.Text, Brushes.Blue, this.ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle currRect = this.ClientRectangle;
            GraphicsHelper.CellPaint(e.Graphics, currRect);
            GraphicsHelper.CellTextPaint(e.Graphics, this.Text, Brushes.Blue, currRect);
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            this.Height = this.Width;
            base.OnResize(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            this.DrawToOldBitmap();

            //绘制鼠标进入后控件后，控件重绘
            this.CreateGraphics().DrawRectangle(Pens.Gray,
                new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            this.Focus();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //将鼠标进入前的图片重绘，再释放保存图片的变量
            this.CreateGraphics().DrawImage(_ControlBmp, new Point(0, 0));
            this._ControlBmp = null;

            base.OnMouseLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                    break;
                case Keys.D1:
                    this.InputNumber("1");
                    break;
                case Keys.D2:
                    this.InputNumber("2");
                    break;
                case Keys.D3:
                    this.InputNumber("3");
                    break;
                case Keys.D4:
                    this.InputNumber("4");
                    break;
                case Keys.D5:
                    this.InputNumber("5");
                    break;
                case Keys.D6:
                    this.InputNumber("6");
                    break;
                case Keys.D7:
                    this.InputNumber("7");
                    break;
                case Keys.D8:
                    this.InputNumber("8");
                    break;
                case Keys.D9:
                    this.InputNumber("9");
                    break;
            }
            this.DrawToOldBitmap();
        }

        /// <summary>
        /// 首先先将控件原图保存到指定的位图，以便鼠标离开后重绘
        /// </summary>
        private void DrawToOldBitmap()
        {
            //首先先将控件原图保存，以便鼠标离开后重绘
            this._ControlBmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(this._ControlBmp, this.ClientRectangle);
        }


        /// <summary>
        /// 清除数字的事件方法
        /// </summary>
        void ClearItem_Click(object sender, EventArgs e)
        {
            this.Text = string.Empty;
        }

        /// <summary>
        /// 填写数字的事件方法
        /// </summary>
        void NumberItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.InputNumber(item.Text);            
        }

        /// <summary>
        /// 填写数字
        /// </summary>
        /// <param name="code">被填写的数字的文本</param>
        private void InputNumber(string code)
        {
            try
            {
                this.ParentPanel.DoTable.SetValue(_DoCell.Row, _DoCell.Column, Convert.ToInt16(code));
                this.Text = code;
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private IContainer _Components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Components != null)
                {
                    _Components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    }
}
