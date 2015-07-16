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
        public int FrameSent { get; set; }
        /// <summary>
        /// 接收帧数
        /// </summary>
        public int FrameReceived { get; set; }
        /// <summary>
        /// 接收丢失帧数
        /// </summary>
        public int FrameLost { get; set; }
        /// <summary>
        /// 接收错误帧数
        /// </summary>
        public int FrameError { get; set; }
    }
}