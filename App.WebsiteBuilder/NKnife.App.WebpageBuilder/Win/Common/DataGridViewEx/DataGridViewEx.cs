using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.Win
{
    public class DataGridViewEx : DataGridView
    {
        /* 暂时完成的不好的“行拖动”功能
        public DataGridViewEx()
        {
            this.CellMouseMove += new DataGridViewCellMouseEventHandler(DataGridViewEx_CellMouseMove);
            this.DragDrop += new DragEventHandler(DataGridViewEx_DragDrop);
            this.CellMouseDown += new DataGridViewCellMouseEventHandler(DataGridViewEx_CellMouseDown);
            this.SelectionChanged += new EventHandler(DataGridViewEx_SelectionChanged);
        }

        /// <summary>
        /// 鼠标在单元格里移动时激活拖放功能，我这里判定了假如是只有单击才执行拖放，
        /// 双击我要执行其他功能，而且只有点在每行的表头那一格拖动才行，否则会影响
        /// 编辑其他单元格的值。假如希望点在任何一个单元格都可以拖动，去掉判定列序号的条件就行了。
        /// </summary>
        void DataGridViewEx_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))

                    this.DoDragDrop(this.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }

        /// <summary>
        /// 拖动到目的地后的操作，GetRowFromPoint是根据鼠标按键被释放时的鼠标位置计算行序号。
        /// </summary>
        void DataGridViewEx_DragDrop(object sender, DragEventArgs e)
        {
            int idx = GetRowFromPoint(e.X, e.Y);

            if (idx < 0) return;

            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                this.Rows.Remove(row);
                this.Rows.Insert(idx, row);
                _selectionIdx = idx;
            }
        }

        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < this.RowCount; i++)
            {
                Rectangle rec = this.GetRowDisplayRectangle(i, false);

                if (this.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }

        int _selectionIdx;
        /// <summary>
        /// 到这里基本功能其实已经完成。只是有些细节需要调整。一般我们希望拖动完成后被选中
        /// 的行是刚才拖动的那一行，但是DataGridView会自动选中顶替我们拖动的那一行位置的新
        /// 行，也就是说，比如我们拖动第3行到其他位置，结束后，被选中的行仍然是新的第3行。
        /// 在DragDrop方法里设置也没有用， DataGridView选中新行的行为是在DragDrop结束后。所
        /// 以我们必须自己记录选中行的行号。在类中添加一个字段selectionIdx。
        /// </summary>
        void DataGridViewEx_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                _selectionIdx = e.RowIndex;
        }

        void DataGridViewEx_SelectionChanged(object sender, EventArgs e)
        {
            if ((this.Rows.Count > 0) && (this.SelectedRows.Count > 0) && (this.SelectedRows[0].Index != _selectionIdx))
            {
                if (this.Rows.Count <= _selectionIdx)
                    _selectionIdx = this.Rows.Count - 1;
                this.Rows[_selectionIdx].Selected = true;
                this.CurrentCell = this.Rows[_selectionIdx].Cells[0];
            }
        }

        void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }
        */

        internal class DragForm : Form
        {
            private DragForm()
            {
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.BackColor = Color.BlueViolet;
                //this.TransparencyKey = Color.BlueViolet;
                this.Opacity = 1;
                this.FormBorderStyle = FormBorderStyle.None;
                this.ShowInTaskbar = false;
            }

            static private DragForm _dragForm = null;
            static public DragForm Creator()
            {
                if (_dragForm == null)
                {
                    _dragForm = new DragForm();
                }
                return _dragForm;
            }
        }
    }
}
