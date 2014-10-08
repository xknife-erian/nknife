using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.App.SocketKit.Common
{
    public class SocketMessage
    {
        public SocketDirection SocketDirection { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
    }

    public enum SocketDirection
    {
        Send,Receive
    }
}
