using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    /// <summary>
    ///  针对一个下位机按照步骤顺序一步一步来
    /// </summary>
    public class SingleTalkTestCase:ITestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private ManualResetEvent _TestStepResetEvent = new ManualResetEvent(false);
        private int _ReplyWaitTimeout = 10000; //协议发送后，等待回复的超时时间，单位毫秒

        private TestCaseResult _MoniteredResult; //真实监控到的结果
        private TestCaseResult _RepliedResult; //从下位机读取到的结果

        #region ITestCase
        public void Start(IKernel kernel, object testCaseParam = default(ExecuteHardwareTestParam))
        {
            var param = testCaseParam as ExecuteHardwareTestParam;
            if (param == null)
            {
                OnTestCaseFinished(false,"测试参数不正确");
                return;
            }

            _MoniteredResult=new TestCaseResult();
            Task.Factory.StartNew(() =>
            {
                _logger.Info("启动测试案例：SingleTalkTestCase");
                _Kernel = kernel;
                _SeverHandler = kernel.ServerHandler;
                _SeverHandler.ProtocolReceived += OnProtocolReceived;
                var sessionList = _Kernel.ServerProtocolFilter.SessionList;

                if (sessionList.Count == 0)
                {
                    _logger.Warn("当前没有任何客户端连接，无法启动案例，本案例至少需要一个客户端连接");
                    OnTestCaseFinished(false);
                    return;
                }

                var sessionId = sessionList[0].Id;
                //第一步：执行初始化
                SetOnWaitProtocol(InitializeConnectionReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionId, new InitializeConnection(NangleProtocolUtility.ServerAddress));
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议InitializeTest后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了执行初始化的回复

                //第二步：执行测试用例
                SetOnWaitProtocol(ExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionId, new ExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                    (byte)NangleProtocolUtility.SendEnable.Enable, //发送使能
                    new byte[] { 0x00, 0x00, 0x00, 0x00 }, //发送目的地址
                    NangleProtocolUtility.GetSendInterval(param.SendInterval), //发送时间间隔
                    NangleProtocolUtility.GetTestDataLength(param.TestDataLength), //发送测试数据长度
                    NangleProtocolUtility.GetFrameCount(param.FrameCount) //发送帧数
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

                Thread.Sleep(1000*param.SendDuration); //持续5秒钟时间

                //第四步：调用停止执行测试用例

                SetOnWaitProtocol(StopExecuteTestCaseReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionId, new StopExecuteTestCase(
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

                //第五步：读取测试用例执行结果
                SetOnWaitProtocol(ReadTestCaseResultReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionId, new ReadTestCaseResult(
                    NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                    ));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议ReadTestCaseResult后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到测试用例结果

                //第六步，比对检测到的数据和返回的测试用例结果是否一致，进行分析
                string message = VerifyTestCaseResult();
                OnTestCaseFinished(true, message);
            });
        }

        private string VerifyTestCaseResult()
        {
            var monitored = string.Format("服务端监控测试数据：用例编号[{0}]发送帧数[{1}]接收帧数[{2}]接收丢失帧数[{3}]",
                _MoniteredResult.TestCaseIndex, _MoniteredResult.FrameSent, _MoniteredResult.FrameReceived,
                _MoniteredResult.FrameLost);
            var replied = string.Format("客户端返回测试数据：用例编号[{0}]发送帧数[{1}]接收帧数[{2}]接收丢失帧数[{3}]",
                _RepliedResult.TestCaseIndex, _RepliedResult.FrameSent, _RepliedResult.FrameReceived,
                _RepliedResult.FrameLost);
            return string.Format("{0}\r\n{1}", monitored, replied);
        }


        public void Abort()
        {
        }

        public event EventHandler<TestCaseResultEventArgs> Finished;
        public event EventHandler<TestCaseResultEventArgs> Aborted;
        #endregion

        private int _CurrentCommandIntValue;
        private void SetOnWaitProtocol(int commandIntValue)
        {
            _CurrentCommandIntValue = commandIntValue;
        }

        private void OnTestCaseFinished(bool result,string message="")
        {
            _SeverHandler.ProtocolReceived -= OnProtocolReceived;

            var handler = Finished;
            if(handler !=null)
                handler.Invoke(this,new TestCaseResultEventArgs()
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
            if (commandIntValue == ReadTestCaseResultReply.CommandIntValue) //收到读取测试用例结果回复协议
            {
                OnReadTestCaseResultReply(protocol);
            }
            else if (commandIntValue == TestRawData.CommandIntValue) //收到测试数据帧
            {
                _MoniteredResult.FrameReceived += 1;
                _logger.Debug(string.Format("收到测试数据帧第{0}条", _MoniteredResult.FrameReceived));
            }

            if (commandIntValue == _CurrentCommandIntValue)
            {
                _TestStepResetEvent.Set();
            }


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
