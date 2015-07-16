using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Kernel;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class PointToPointTestCase:ITestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private ManualResetEvent _TestStepResetEvent = new ManualResetEvent(false);
        private int _ReplyWaitTimeout = 10000; //协议发送后，等待回复的超时时间，单位毫秒

        private TestCaseResult _MoniteredResult; //真实监控到的结果
        private TestCaseResult _RepliedResult; //从下位机读取到的结果


        #region ITestCase

        public void Start(IKernel kernel)
        {

        }

        public void Abort()
        {

        }

        public event EventHandler<TestCaseResultEventArgs> Finished;
        public event EventHandler<TestCaseResultEventArgs> Aborted;

        #endregion

    }
}
