using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.ShareResources;
using NKnife.Utility;

namespace NKnife.NLog
{
    public class NotFlickerListView : ListView
    {
        public NotFlickerListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            BuildLoggerInfoColumn();
        }

        private void BuildLoggerInfoColumn()
        {
            var timeHeader = new ColumnHeader();
            timeHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Time_Header");
            timeHeader.Width = 80;

            var logMessageHeader = new ColumnHeader();
            logMessageHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Info_Header");
            logMessageHeader.Width = 380;

            var loggerNameHeader = new ColumnHeader();
            loggerNameHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Source_Header");
            loggerNameHeader.Width = 200;

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
