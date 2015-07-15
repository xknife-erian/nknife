using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Generic;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Server;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    /// <summary>
    ///  针对一个下位机按照步骤顺序一步一步来
    /// </summary>
    public class SingleTalkTestCase:ITestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private ManualResetEvent _TestStepResetEvent = new ManualResetEvent(false);
        private int _ReplyWaitTimeout = 1000; //协议发送后，等待回复的超时时间，单位毫秒
        #region ITestCase
        public bool Start(IKernel kernel)
        {
            _logger.Info("启动测试案例：SingleTalkTestCase");
            _Kernel = kernel;
            _SeverHandler = kernel.ServerHandler;
            _SeverHandler.ProtocolReceived += OnProtocolReceived;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;

            if (sessionList.Count == 0)
            {
                _logger.Warn("当前没有任何客户端连接，无法启动案例，本案例至少需要一个客户端连接");
                ResetTestCase();
                return false;
            }

            var sessionId = sessionList[0].Id;
            //第一步执行初始化
            _Kernel.ServerHandler.WriteToSession(sessionId, new InitializeTest(NangleProtocolUtility.EmptyBytes4, NangleProtocolUtility.ServerAddress));
            if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议InitializeTest后，等待回复超时");
                ResetTestCase();
                return false;
            }

            return true;
        }



        public bool Abort()
        {
            return true;
        }

        public event EventHandler Finished;
        public event EventHandler Aborted;
        #endregion

        private void ResetTestCase()
        {
            _SeverHandler.ProtocolReceived -= OnProtocolReceived;
        }
        private void OnProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            _TestStepResetEvent.Set();
        }
    }
}
