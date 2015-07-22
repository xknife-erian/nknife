namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class TestCaseResult
    {
        /// <summary>
        /// 用例编号
        /// </summary>
        public int TestCaseIndex { get; set; }
        /// <summary>
        /// 发送帧数
        /// </summary>
        public long FrameSent { get; set; }
        /// <summary>
        /// 接收帧数
        /// </summary>
        public long FrameReceived { get; set; }
        /// <summary>
        /// 接收丢失帧数
        /// </summary>
        public long FrameLost { get; set; }
//        /// <summary>
//        /// 接收错误帧数
//        /// </summary>
//        public long FrameError { get; set; }
    }
}