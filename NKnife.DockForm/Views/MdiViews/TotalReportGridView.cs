using System;
using System.ComponentModel;
using NKnife.DockForm.Common;

namespace NKnife.DockForm.Views.MdiViews
{
    class TotalReportGridView : GridViewDockContent
    {

        public TotalReportGridView()
        {
            TabText = string.Format("服务综合数据统计表");

            Shown += TotalReportGridViewShown;
        }

        private void TotalReportGridViewShown(object sender, EventArgs e)
        {
        }

        private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Table":
                    break;
            }
        }
    }
}