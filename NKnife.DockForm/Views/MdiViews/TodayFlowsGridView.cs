using System;
using System.ComponentModel;
using NKnife.DockForm.Common;

namespace NKnife.DockForm.Views.MdiViews
{
    class TodayFlowsGridView : GridViewDockContent
    {
        public TodayFlowsGridView()
        {
            TabText = string.Format("本日({0})业务明细表", DateTime.Now.ToShortDateString());
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