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
        public int FrameSent { get; set; }
        /// <summary>
        /// ����֡��
        /// </summary>
        public int FrameReceived { get; set; }
        /// <summary>
        /// ���ն�ʧ֡��
        /// </summary>
        public int FrameLost { get; set; }
        /// <summary>
        /// ���մ���֡��
        /// </summary>
        public int FrameError { get; set; }
    }
}