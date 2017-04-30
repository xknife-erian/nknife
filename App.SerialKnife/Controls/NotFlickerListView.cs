using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Channels.SerialKnife.Controls
{
    internal class NotFlickerListView : ListView
    {
        public NotFlickerListView()
        {
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint,
                true);
            UpdateStyles();
            BuildLoggerInfoColumn();
            SizeChanged += (s, e) => { SetViewColumnSize(); };
        }

        private void BuildLoggerInfoColumn()
        {
            var columnHeaderTime = new ColumnHeader();
            var columnHeaderData = new ColumnHeader();

            columnHeaderTime.Text = "时间";
            columnHeaderTime.Width = 84;

            columnHeaderData.Text = "数据";
            columnHeaderData.Width = 500;
            Columns.AddRange(new[]
            {
                columnHeaderTime,
                columnHeaderData
            });
            Font = new Font(new FontFamily("Arial"), 8.25F);
            GridLines = true;
            MultiSelect = false;
            FullRowSelect = true;
            View = View.Details;
            ShowItemToolTips = true;
        }

        /// <summary>
        ///     设置Log显示的ListView中各列的宽度
        /// </summary>
        private void SetViewColumnSize()
        {
            if (Columns.Count >= 2)
            {
                Columns[1].Width = Width - Columns[0].Width - 22;
            }
        }
    }
}