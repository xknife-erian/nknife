using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Client;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Generic;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Server;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class PointToPointDualTestCase : ITestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private ManualResetEvent _TestStepResetEvent = new ManualResetEvent(false);
        private int _ReplyWaitTimeout = 10000; //协议发送后，等待回复的超时时间，单位毫秒

        private TestCaseResult _MoniteredResult; //真实监控到的结果
        private TestCaseResult _RepliedResult; //从下位机读取到的结果

        private byte[] _CurrentInitializeRepliedSessionAddress = { 0x00, 0x00, 0x00, 0x00 };
        private Dictionary<long, long> _SessionAddressIdMap = new Dictionary<long, long>();
        #region ITestCase

        public void Start(IKernel kernel)
        {
            _MoniteredResult = new TestCaseResult();
            Task.Factory.StartNew(() =>
            {
                _logger.Info("启动测试案例：PointToPointTestCase");
                _Kernel = kernel;
                _SeverHandler = kernel.ServerHandler;
                _SeverHandler.ProtocolReceived += OnProtocolReceived;
                var sessionList = _Kernel.ServerProtocolFilter.SessionList;

                if (sessionList.Count < 2)
                {
                    _logger.Warn("当前客户端连接数不够，无法启动案例，本案例至少需要两个客户端连接");
                    OnTestCaseFinished(false);
                    return;
                }

                //取头两个session进行点对点连接测试
                var sessionIdA = sessionList[0].Id;
                var sessionIdB = sessionList[1].Id;
                var sessionAddressA = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                var sessionAddressB = new byte[] { 0x00, 0x00, 0x00, 0x00 };

                //第一步：执行初始化
                SetOnWaitProtocol(InitializeTestReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new InitializeTest(NangleProtocolUtility.ServerAddress));
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("向sessionIdA发送协议InitializeTest后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了sessionIdA执行初始化的回复
                Array.Copy(_CurrentInitializeRepliedSessionAddress, sessionAddressA, 4);
                _SessionAddressIdMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressA), sessionIdA);

                SetOnWaitProtocol(InitializeTestReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new InitializeTest(NangleProtocolUtility.ServerAddress));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("向sessionIdA发送协议InitializeTest后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了sessionIdB执行初始化的回复
                Array.Copy(_CurrentInitializeRepliedSessionAddress, sessionAddressB, 4);
                _SessionAddressIdMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressB), sessionIdB);

                //第二步：执行测试用例，使A向B发数据
                //向sessionB发执行测试用例指令，使其发送
                SetOnWaitProtocol(ExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new ExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                    (byte)NangleProtocolUtility.SendEnable.Enable, //发送使能
                    sessionAddressA, //发送目的地址
                    NangleProtocolUtility.GetSendInterval(50), //发送时间间隔
                    NangleProtocolUtility.GetTestDataLength(200), //发送测试数据长度
                    NangleProtocolUtility.GetFrameCount(10000) //发送帧数
                    ));
                _MoniteredResult.TestCaseIndex = 1;
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议ExecuteTestCase后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了执行测试用例的回复

                //向sessionA发执行测试用例指令，使其发送
                SetOnWaitProtocol(ExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new ExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                    (byte)NangleProtocolUtility.SendEnable.Enable, //发送使能
                    sessionAddressB, //发送目的地址
                    NangleProtocolUtility.GetSendInterval(50), //发送时间间隔
                    NangleProtocolUtility.GetTestDataLength(200), //发送测试数据长度
                    NangleProtocolUtility.GetFrameCount(10000) //发送帧数
                    ));
                _MoniteredResult.TestCaseIndex = 1;
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议ExecuteTestCase后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了执行测试用例的回复
                //第三步：记录接下来收到的数据，并持续一段时间

                Thread.Sleep(1000 * 5); //持续5秒钟时间

                //第四步：调用停止执行测试用例
                //停止A
                SetOnWaitProtocol(StopExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new StopExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                    ));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议StopExecuteTestCase后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了停止执行测试用例的回复
                //停止B
                SetOnWaitProtocol(StopExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new StopExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                    ));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议StopExecuteTestCase后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了停止执行测试用例的回复

                //第五步：读取SessionA测试用例执行结果
                SetOnWaitProtocol(ReadTestCaseResultReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new ReadTestCaseResult(
                    NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                    ));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议ReadTestCaseResult后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到来自 SessionA的测试用例结果
                var message = new StringBuilder(VerifyTestCaseResult("服务端监控", _MoniteredResult));
                message.Append(string.Format("\r\n{0}", VerifyTestCaseResult("发送端返回", _RepliedResult)));

                //读取SessionB测试用例执行结果
                SetOnWaitProtocol(ReadTestCaseResultReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new ReadTestCaseResult(
                    NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                    ));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议ReadTestCaseResult后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                message.Append(string.Format("\r\n{0}", VerifyTestCaseResult("接收端返回", _RepliedResult)));


                //第六步，比对检测到的数据和返回的测试用例结果是否一致，进行分析，发出报告
                OnTestCaseFinished(true, message.ToString());
            });
        }

        public void Abort()
        {

        }

        public event EventHandler<TestCaseResultEventArgs> Finished;
        public event EventHandler<TestCaseResultEventArgs> Aborted;

        #endregion

        //        private string VerifyTestCaseResult()
        //        {
        //            var monitored = string.Format("服务端监控测试数据：用例编号[{0}]发送帧数[{1}]接收帧数[{2}]接收丢失帧数[{3}]接收错误帧数[{4}]",
        //                _MoniteredResult.TestCaseIndex, _MoniteredResult.FrameSent, _MoniteredResult.FrameReceived,
        //                _MoniteredResult.FrameLost, _MoniteredResult.FrameError);
        //            var replied = string.Format("客户端返回测试数据：用例编号[{0}]发送帧数[{1}]接收帧数[{2}]接收丢失帧数[{3}]接收错误帧数[{4}]",
        //                _RepliedResult.TestCaseIndex, _RepliedResult.FrameSent, _RepliedResult.FrameReceived,
        //                _RepliedResult.FrameLost, _RepliedResult.FrameError);
        //            return string.Format("{0}\r\n{1}", monitored, replied);
        //        }
        private string VerifyTestCaseResult(string from, TestCaseResult testCaseResult)
        {
            return string.Format("{0}测试数据：用例编号[{1}]发送帧数[{2}]接收帧数[{3}]接收丢失帧数[{4}]接收错误帧数[{5}]",
                from, testCaseResult.TestCaseIndex, testCaseResult.FrameSent, testCaseResult.FrameReceived,
                testCaseResult.FrameLost, testCaseResult.FrameError);
        }

        private int _CurrentCommandIntValue;
        private void SetOnWaitProtocol(int commandIntValue)
        {
            _CurrentCommandIntValue = commandIntValue;
        }

        private void OnTestCaseFinished(bool result, string message = "")
        {
            _SeverHandler.ProtocolReceived -= OnProtocolReceived;

            var handler = Finished;
            if (handler != null)
                handler.Invoke(this, new TestCaseResultEventArgs()
                {
                    Result = result,
                    Message = message,
                });
        }

        private void OnProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            //TODO:验证当前收到的是正在等候的
            var protocol = nangleProtocolEventArgs.Protocol;
            var command = protocol.Command;
            var commandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(command);
            if (commandIntValue == InitializeTestReply.CommandIntValue) //收到了初始化回复
            {
                OnInitializeTestReply(protocol);
            }
            else if (commandIntValue == ReadTestCaseResultReply.CommandIntValue) //收到读取测试用例结果回复协议
            {
                OnReadTestCaseResultReply(protocol);
            }
            else if (commandIntValue == TestRawData.CommandIntValue) //收到测试数据帧
            {
                OnTestRawData(protocol);
                _logger.Debug(string.Format("收到测试数据帧第{0}条", _MoniteredResult.FrameReceived));
            }

            if (commandIntValue == _CurrentCommandIntValue)
            {
                _TestStepResetEvent.Set();
            }


        }

        private void OnTestRawData(BytesProtocol protocol)
        {
            _MoniteredResult.FrameReceived += 1;

            var targetAddress = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            if (TestRawData.Parse(ref targetAddress, protocol))
            {
                var key = NangleCodecUtility.ConvertFromFourBytesToInt(targetAddress);
                if (_SessionAddressIdMap.ContainsKey(key))
                {
                    var targetSessionId = _SessionAddressIdMap[key];
                    _Kernel.ServerHandler.WriteToSession(targetSessionId, protocol);
                    _MoniteredResult.FrameSent += 1;
                }
            }
        }

        /// <summary>
        /// 收到了初始化测试回复
        /// </summary>
        /// <param name="protocol"></param>
        private void OnInitializeTestReply(BytesProtocol protocol)
        {
            InitializeTestReply.Parse(ref _CurrentInitializeRepliedSessionAddress, protocol);
        }

        /// <summary>
        /// 收到读取测试用例结果
        /// </summary>
        /// <param name="protocol"></param>
        private void OnReadTestCaseResultReply(BytesProtocol protocol)
        {
            _RepliedResult = new TestCaseResult();
            if (ReadTestCaseResultReply.Parse(ref _RepliedResult, protocol))
            {
                _logger.Info("测试用例结果解析成功");
            }
        }
    }
}
