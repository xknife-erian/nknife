using System;
using System.Data;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class TestCaseResultEventArgs : EventArgs
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}