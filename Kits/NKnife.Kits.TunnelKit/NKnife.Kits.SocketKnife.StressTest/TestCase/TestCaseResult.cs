namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class TestCaseResult
    {
        /// <summary>
        /// �������
        /// </summary>
        public int TestCaseIndex { get; set; }
        /// <summary>
        /// ����֡��
        /// </summary>
        public long FrameSent { get; set; }
        /// <summary>
        /// ����֡��
        /// </summary>
        public long FrameReceived { get; set; }
        /// <summary>
        /// ���ն�ʧ֡��
        /// </summary>
        public long FrameLost { get; set; }
//        /// <summary>
//        /// ���մ���֡��
//        /// </summary>
//        public long FrameError { get; set; }
    }
}