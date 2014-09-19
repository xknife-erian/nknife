using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.Win
{
    public class DataGridViewEx : DataGridView
    {
        /* ��ʱ��ɵĲ��õġ����϶�������
        public DataGridViewEx()
        {
            this.CellMouseMove += new DataGridViewCellMouseEventHandler(DataGridViewEx_CellMouseMove);
            this.DragDrop += new DragEventHandler(DataGridViewEx_DragDrop);
            this.CellMouseDown += new DataGridViewCellMouseEventHandler(DataGridViewEx_CellMouseDown);
            this.SelectionChanged += new EventHandler(DataGridViewEx_SelectionChanged);
        }

        /// <summary>
        /// ����ڵ�Ԫ�����ƶ�ʱ�����ϷŹ��ܣ��������ж��˼�����ֻ�е�����ִ���Ϸţ�
        /// ˫����Ҫִ���������ܣ�����ֻ�е���ÿ�еı�ͷ��һ���϶����У������Ӱ��
        /// �༭������Ԫ���ֵ������ϣ�������κ�һ����Ԫ�񶼿����϶���ȥ���ж�����ŵ����������ˡ�
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
        /// �϶���Ŀ�ĵغ�Ĳ�����GetRowFromPoint�Ǹ�����갴�����ͷ�ʱ�����λ�ü�������š�
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
        /// ���������������ʵ�Ѿ���ɡ�ֻ����Щϸ����Ҫ������һ������ϣ���϶���ɺ�ѡ��
        /// �����Ǹղ��϶�����һ�У�����DataGridView���Զ�ѡ�ж��������϶�����һ��λ�õ���
        /// �У�Ҳ����˵�����������϶���3�е�����λ�ã������󣬱�ѡ�е�����Ȼ���µĵ�3�С�
        /// ��DragDrop����������Ҳû���ã� DataGridViewѡ�����е���Ϊ����DragDrop��������
        /// �����Ǳ����Լ���¼ѡ���е��кš����������һ���ֶ�selectionIdx��
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
