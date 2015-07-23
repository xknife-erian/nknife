namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class ExecuteTestCaseParam
    {
        public int TestCaseIndex { get; set; }
        public bool SendEnable { get; set; }
        public byte[] TargetAddress { get; set; }
        public int SendInterval { get; set; }
        public int FrameDataLength { get; set; }
        public long FrameCount { get; set; }
        public ExecuteTestCaseParam()
        {
            TestCaseIndex = 1;
            SendEnable = true;
            TargetAddress = new byte[]{ 0x00, 0x00, 0x00, 0x00 };
            SendInterval = 100;
            FrameDataLength = 20;
            FrameCount = 10000;
        }


    }
}