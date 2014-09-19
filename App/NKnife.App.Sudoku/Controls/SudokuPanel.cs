using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NKnife.App.Sudoku.Common;

namespace NKnife.App.Sudoku.Controls
{
    /// <summary>
    /// Description of UserControl1.
    /// </summary>
    public class SudokuPanel : Control
    {
        private int _BoxWidth;
        private int _PanelOffsetX;
        private int _PanelOffsetY;

        internal SuDoTable DoTable
        {
            get { return _DoTable; }
        }
        private SuDoTable _DoTable;

        internal SudokuCell[,] SudokuCellArray
        {
            get { return _SudokuCellArray; }
            set { _SudokuCellArray = value; }
        }
        private SudokuCell[,] _SudokuCellArray;

        public SudokuPanel(Size partent)
        {
            this._SudokuCellArray = new SudokuCell[9, 9];
            this._BoxWidth = this.GetBoxWidth(partent, out this._PanelOffsetX, out this._PanelOffsetY);
            this._DoTable = new SuDoTable();
            int margin = 10;
            if (this._PanelOffsetX > this._PanelOffsetY)
            {
                margin = this._PanelOffsetX / 5;
            }
            else
            {
                margin = this._PanelOffsetY / 5;
            }
            this.Margin = new Padding(margin);
            InitializeComponent();
        }

        private int GetBoxWidth(Size partent, out int offsetX, out int offsetY)
        {
            int value = 0;
            int maxOffset = 40;
            if (partent.Width > partent.Height)
            {
                value = (partent.Height - maxOffset * 2) / 9;
                offsetX = (partent.Width - value * 9 - this.Margin.All * 2) / 2;
                offsetY = maxOffset - maxOffset / 5;
            }
            else
            {
                value = (partent.Width - maxOffset * 2) / 9;
                offsetY = (partent.Height - value * 9 - this.Margin.All * 2) / 2;
                offsetX = maxOffset - maxOffset / 5;
            }
            return value;
        }

        private void InitializeComponent()
        {
            int x = this._PanelOffsetX;
            int y = this._PanelOffsetY;
            for (int i = 1; i <= 9; i++)
            {
                x = this._PanelOffsetX;
                for (int j = 1; j <= 9; j++)
                {
                    SudokuCell squares = new SudokuCell(i - 1, j - 1, this._BoxWidth, this.BackColor);
                    squares.Location = new Point(x, y);
                    this.DoTable[i - 1, j - 1] = squares.DoCell;
                    this._SudokuCellArray[i - 1, j - 1] = squares;
                    squares.ParentPanel = this;
                    squares.TextChanged += new EventHandler(Squares_TextChanged);
                    this.Controls.Add(squares);
                    x = x + squares.Width;
                    if (j == 9)
                    {
                        x = x + this._PanelOffsetX;
                        y = y + squares.Height;
                    }
                }
            }
        }

        /// <summary>
        /// 设置Panel中每个Cell的Text值, 同时将值先进行校验后，保存入DoTabel
        /// </summary>
        /// <param name="doString">题目的字符串</param>
        public void SetPanelValue(string doString)
        {
            int[] doArrary = SuDoHelper.GetDoIntArrary(doString);
            int r = 0;
            int c = 0;
            for (int i = 0; i < doArrary.Length; i++)
            {
                r = i / 9;
                c = i % 9;
                int value = doArrary[i];
                this.DoTable.SetValue(r, c, value);//值先进行校验后，保存入DoTabel
                if (value != 0)//设置Cell的Text值
                {
                    this._SudokuCellArray[r, c].Text = value.ToString();
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.DrawBorder();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawBorder();
        }

        /// <summary>
        /// 画控件的外边框线
        /// </summary>
        private void DrawBorder()
        {
            this.CreateGraphics().DrawRectangle(Pens.Gray,
                                        this.ClientRectangle.Top,
                                        this.ClientRectangle.Left,
                                        this.ClientRectangle.Right - 1,
                                        this.ClientRectangle.Bottom - 1);
        }

        void Squares_TextChanged(object sender, EventArgs e)
        {
            SudokuCell control = (SudokuCell)sender;
            try
            {
                SuDoCell c = control.DoCell;
                this.DoTable.SetValue(c.Row, c.Column, Convert.ToInt32(control.Text));
            }
            catch// (ApplicationException ex)
            {
                //this.graphicSudokuBoard[row, col].BackColor = Color.Pink;
                //this.graphicSudokuBoard[row, col].Focus();
                //MessageBox.Show(ex.Message);
                return;
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
