using System.ComponentModel;

namespace NKnife.Tests
{
    /// <summary>����ִ�й��̲���
    /// </summary>
    public class TestMode
    {
        private uint _Count = 1;

        private uint _Interval = 200;

        [Category("����ִ�й��̲���")]
        [Description("���д���")]
        public uint Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        [Category("����ִ�й��̲���")]
        [Description("���м��")]
        public uint Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }
    }
}