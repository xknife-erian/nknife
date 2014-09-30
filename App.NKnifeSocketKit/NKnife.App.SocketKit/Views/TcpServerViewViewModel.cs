using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Mvvm;

namespace NKnife.App.SocketKit.Views
{
    class TcpServerViewViewModel : NotificationObject
    {
        public string TimeHeader { get; set; }

        public TcpServerViewViewModel()
        {
            TimeHeader = "时间";
        }
    }
}
