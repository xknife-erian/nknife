using System.Windows.Forms;

namespace NKnife.NLog.WinForm.Util
{
    internal class NotFlickerListView : ListView
    {
        public const int DefaultTimeHeaderWidth = 95;
        public const int DefaultLogMessageHeader = 365;
        public const int DefaultLoggerNameHeader = 200;

        public NotFlickerListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            BuildLoggerInfoColumn();
        }

        private void BuildLoggerInfoColumn()
        {
            var timeHeader = new ColumnHeader();
            timeHeader.Text = "发生时间";
            timeHeader.Width = DefaultTimeHeaderWidth;

            var logMessageHeader = new ColumnHeader();
            logMessageHeader.Text = "日志信息";
            logMessageHeader.Width = DefaultLogMessageHeader;

            var loggerNameHeader = new ColumnHeader();
            loggerNameHeader.Text = "日志源";
            loggerNameHeader.Width = DefaultLoggerNameHeader;

            Columns.AddRange(
                new[]
                {
                    timeHeader,
                    logMessageHeader,
                    loggerNameHeader
                });
            GridLines = true;
            MultiSelect = false;
            FullRowSelect = true;
            View = View.Details;
            ShowItemToolTips = true;
        }
    }
}