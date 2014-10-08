using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Mvvm;

namespace NKnife.App.SocketKit.Views
{
    public class TcpServerViewViewModel : NotificationObject
    {
        /// <summary>
        /// 时间列的标题
        /// </summary>
        public string TimeHeader { get; set; }

        public TcpServerViewViewModel()
        {
            TimeHeader = "时间";
        }
    }
}
