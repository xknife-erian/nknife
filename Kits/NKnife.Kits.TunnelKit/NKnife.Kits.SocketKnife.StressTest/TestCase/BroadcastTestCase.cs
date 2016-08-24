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
    public class BroadcastTestCase : AbstractTestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private int _ReplyWaitTimeout = 10000; //协议发送后，等待回复的超时时间，单位毫秒

        private Dictionary<long, ManualResetEvent> _SessionIdResetMap = new Dictionary<long, ManualResetEvent>();
        private Dictionary<long, long> _SessionIdWaitCommandMap = new Dictionary<long, long>();
        private Dictionary<long, long> _SessionAddressIdMap = new Dictionary<long, long>();
        private Dictionary<long, byte[]> _SessionIdAddressMap = new Dictionary<long, byte[]>();
        private Dictionary<long, TestCaseResult> _SessionIdTestResultMap = new Dictionary<long, TestCaseResult>();
        private Dictionary<int, string> _TestResultMap = new Dictionary<int, string>();
        private ExecuteHardwareTestParam param;

        #region ITestCase

        public override void Start(IKernel kernel, object testCaseParam = default(ExecuteHardwareTestParam))
        {
            param = testCaseParam as ExecuteHardwareTestParam;
            if (param == null)
            {
                OnTestCaseFinished(false, "测试参数不正确");
                return;
            }

            _logger.Info("启动测试案例：BroadcastTestCase");
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

            Task.Factory.StartNew(() =>
            {
                //第一步，广播接收者进入数据接收状态
                List<Task<bool>> tskList = new List<Task<bool>>();
                //
                for (int i = 1; i< sessionList.Count; i++) //第一个session作为广播发出者
                {
                    var tsk = new Task<bool>(TaskWorker, i, TaskCreationOptions.PreferFairness);
                    tsk.Start();
                    tskList.Add(tsk);
                }

                Task.WaitAll(tskList.ToArray()); //等待都进入接听广播状态

                //第二步，广播发出者进入数据发送状态
                var tskSendReady = new Task<bool>(TaskWorker1, 0, TaskCreationOptions.PreferFairness);
                tskSendReady.Start();
                Task.WaitAll(new Task[] {tskSendReady}); //等待发送者进入发送广播状态
                
                //持续一段时间，进行数据发送

                //第三步：记录接下来收到的数据，并持续一段时间
                KeepRunning(param.SendDuration);

                //第四步：调用停止执行测试用例
                //停止发送
                var tskSendStop = new Task<bool>(TaskWorker2, 0, TaskCreationOptions.PreferFairness);
                tskSendStop.Start();
                Task.WaitAll(new Task[] { tskSendStop }); //等待发送者停止发送状态

                //停止接收
                tskList.Clear();
                //
                for (int i = 1; i < sessionList.Count; i++) //第一个session作为广播发出者
                {
                    var tsk = new Task<bool>(TaskWorker3, i, TaskCreationOptions.PreferFairness);
                    tsk.Start();
                    tskList.Add(tsk);
                }
                Task.WaitAll(tskList.ToArray()); //等待都停止接听广播状态

                //第五步：读取测试记录
                var tskReadSend = new Task<bool>(TaskWorker4, 0, TaskCreationOptions.PreferFairness);
                tskReadSend.Start();
                Task.WaitAll(new Task[] { tskReadSend }); //等待读取发送者测试结果

                //读取接收者测试结果
                tskList.Clear();
                //
                for (int i = 1; i < sessionList.Count; i++) //第一个session作为广播发出者
                {
                    var tsk = new Task<bool>(TaskWorker5, i, TaskCreationOptions.PreferFairness);
                    tsk.Start();
                    tskList.Add(tsk);
                }
                Task.WaitAll(tskList.ToArray()); //等待读取接收者测试结果

                //形成测试结论
                var result = new StringBuilder();
                foreach (var key in _TestResultMap.Keys)
                {
                    var msg = _TestResultMap[key];
                    result.Append(string.Format("{0}\r\n", msg));
                }
                OnTestCaseFinished(true, result.ToString());
            });
        }

        /// <summary>
        /// 进入接听状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool TaskWorker(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;

            //取头两个session进行点对点连接测试
            var sessionIdA = sessionList[index].Id;
            var sessionAddressA = new byte[] { 0x00, 0x00, 0x00, 0x00 };

            _SessionIdResetMap.Add(sessionIdA, new ManualResetEvent(false));

            //第一步：执行初始化
            SetOnWaitProtocol(sessionIdA, InitializeConnectionReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new InitializeConnection(NangleProtocolUtility.ServerAddress));
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn(string.Format("向session{0}发送协议InitializeTest后，等待回复超时", sessionIdA));
                return false;
            }
            //收到了sessionIdA执行初始化的回复
            if (!_SessionIdAddressMap.ContainsKey(sessionIdA))
            {
                _logger.Warn(string.Format("向session{0}发送协议InitializeTest后，未能解析地址", sessionIdA));
                return false;
            }

            Array.Copy(_SessionIdAddressMap[sessionIdA], sessionAddressA, 4);
            _logger.Debug(string.Format("收到客户端初始化回复，地址[{0}]", sessionAddressA.ToHexString()));
            _SessionAddressIdMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressA), sessionIdA);

            //第二步：执行测试用例
            //向sessionA发执行测试用例指令，使其只能接收
            SetOnWaitProtocol(sessionIdA, ExecuteTestCaseReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new ExecuteTestCase(
                NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                (byte)NangleProtocolUtility.SendEnable.Disable, //发送使能
                sessionAddressA, //发送目的地址
                NangleProtocolUtility.GetSendInterval(param.SendInterval), //发送时间间隔
                NangleProtocolUtility.GetTestDataLength(param.TestDataLength), //发送测试数据长度
                NangleProtocolUtility.GetFrameCount(param.FrameCount) //发送帧数
                ));
            _SessionIdResetMap[sessionIdA].Reset();
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议ExecuteTestCase后，等待回复超时");
                return false;
            }
            //收到了执行测试用例的回复
            return true;
        }

        /// <summary>
        /// 进入发送状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool TaskWorker1(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;

            var sessionIdA = sessionList[index].Id;
            var sessionAddressA = new byte[] { 0x00, 0x00, 0x00, 0x00 };

            _SessionIdResetMap.Add(sessionIdA, new ManualResetEvent(false));

            //第一步：执行初始化
            SetOnWaitProtocol(sessionIdA, InitializeConnectionReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new InitializeConnection(NangleProtocolUtility.ServerAddress));
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn(string.Format("向session{0}发送协议InitializeTest后，等待回复超时", sessionIdA));
                return false;
            }
            //收到了sessionIdA执行初始化的回复
            if (!_SessionIdAddressMap.ContainsKey(sessionIdA))
            {
                _logger.Warn(string.Format("向session{0}发送协议InitializeTest后，未能解析地址", sessionIdA));
                return false;
            }

            Array.Copy(_SessionIdAddressMap[sessionIdA], sessionAddressA, 4);
            _logger.Debug(string.Format("收到客户端初始化回复，地址[{0}]", sessionAddressA.ToHexString()));
            _SessionAddressIdMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressA), sessionIdA);

            //第二步：执行测试用例
            //向sessionA发执行测试用例指令，使其只能接收
            SetOnWaitProtocol(sessionIdA, ExecuteTestCaseReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new ExecuteTestCase(
                NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                (byte)NangleProtocolUtility.SendEnable.Enable, //发送使能
                sessionAddressA, //发送目的地址
                NangleProtocolUtility.GetSendInterval(param.SendInterval), //发送时间间隔
                NangleProtocolUtility.GetTestDataLength(param.TestDataLength), //发送测试数据长度
                NangleProtocolUtility.GetFrameCount(param.FrameCount) //发送帧数
                ));
            _SessionIdResetMap[sessionIdA].Reset();
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议ExecuteTestCase后，等待回复超时");
                return false;
            }
            //收到了执行测试用例的回复
            return true;
        }

        /// <summary>
        /// 发送者退出发送状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool TaskWorker2(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;

            var sessionIdA = sessionList[index].Id;

            SetOnWaitProtocol(sessionIdA, StopExecuteTestCaseReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new StopExecuteTestCase(
                NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                ));
            _SessionIdResetMap[sessionIdA].Reset();
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议StopExecuteTestCase后，等待回复超时");
                return false;
            }
            //收到了停止执行测试用例的回复
            return true;
        }

        private bool TaskWorker3(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;
            var sessionIdA = sessionList[index].Id;

            SetOnWaitProtocol(sessionIdA, StopExecuteTestCaseReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new StopExecuteTestCase(
                NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                ));
            _SessionIdResetMap[sessionIdA].Reset();
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议StopExecuteTestCase后，等待回复超时");
                return false;
            }
            //收到了停止执行测试用例的回复
            return true;
        }

        private bool TaskWorker4(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;
            var sessionIdA = sessionList[index].Id;

            SetOnWaitProtocol(sessionIdA, ReadTestCaseResultReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdA, new ReadTestCaseResult(
                NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                ));
            _SessionIdResetMap[sessionIdA].Reset();
            if (!_SessionIdResetMap[sessionIdA].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议ReadTestCaseResult后，等待回复超时");
                return false;
            }
            //收到来自 SessionA的测试用例结果
            var message = new StringBuilder();
            if (!_SessionIdTestResultMap.ContainsKey(sessionIdA))
            {
                _logger.Warn(string.Format("向session{0}发送协议ReadTestCaseResult后，未能解析结果", sessionIdA));
                return false;
            }
            message.Append(string.Format("{0}\r\n", VerifyTestCaseResult("发送端返回", _SessionIdTestResultMap[sessionIdA])));

            _TestResultMap.Add(index, message.ToString());
            return true;
        }

        private bool TaskWorker5(object state)
        {
            int index = (int)state;
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;
            var sessionIdB = sessionList[index].Id;

            //读取SessionB测试用例执行结果
            SetOnWaitProtocol(sessionIdB, ReadTestCaseResultReply.CommandIntValue);
            _Kernel.ServerHandler.WriteToSession(sessionIdB, new ReadTestCaseResult(
                NangleProtocolUtility.GetTestCaseIndex(1) //用例编号);
                ));
            _SessionIdResetMap[sessionIdB].Reset();
            if (!_SessionIdResetMap[sessionIdB].WaitOne(_ReplyWaitTimeout))
            {
                _logger.Warn("发送协议ReadTestCaseResult后，等待回复超时");
                return false;
            }
            if (!_SessionIdTestResultMap.ContainsKey(sessionIdB))
            {
                _logger.Warn(string.Format("向session{0}发送协议ReadTestCaseResult后，未能解析结果", sessionIdB));
                return false;
            }

            var message = new StringBuilder();
            message.Append(string.Format("{0}\r\n", VerifyTestCaseResult("接收端返回", _SessionIdTestResultMap[sessionIdB])));

            _TestResultMap.Add(index, message.ToString());
            return true;
        }
        #endregion


        private string VerifyTestCaseResult(string from, TestCaseResult testCaseResult)
        {
            return string.Format("{0}测试数据：用例编号[{1}]发送帧数[{2}]接收帧数[{3}]接收丢失帧数[{4}]",
                from, testCaseResult.TestCaseIndex, testCaseResult.FrameSent, testCaseResult.FrameReceived,
                testCaseResult.FrameLost);
        }

        private void SetOnWaitProtocol(long sessionId, int commandIntValue)
        {
            if (_SessionIdWaitCommandMap.ContainsKey(sessionId))
            {
                _SessionIdWaitCommandMap[sessionId] = commandIntValue;
            }
            else
            {
                _SessionIdWaitCommandMap.Add(sessionId, commandIntValue);
            }
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
            var protocol = nangleProtocolEventArgs.Protocol;
            var sessionId = nangleProtocolEventArgs.SessionId;
            var command = protocol.Command;
            var commandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(command);
            if (commandIntValue == InitializeConnectionReply.CommandIntValue) //收到了初始化回复
            {
                OnInitializeTestReply(sessionId, protocol);
            }
            else if (commandIntValue == ReadTestCaseResultReply.CommandIntValue) //收到读取测试用例结果回复协议
            {
                OnReadTestCaseResultReply(sessionId, protocol);
            }
            else if (commandIntValue == TestRawData.CommandIntValue) //收到测试数据帧
            {
                OnTestRawData(sessionId, protocol);
            }

            if (_SessionIdWaitCommandMap.ContainsKey(sessionId) && _SessionIdResetMap.ContainsKey(sessionId))
            {
                if (commandIntValue == _SessionIdWaitCommandMap[sessionId])
                {
                    _SessionIdResetMap[sessionId].Set();
                }
            }
        }

        private void OnTestRawData(long sessionId, BytesProtocol protocol)
        {
            var sessionList = _Kernel.ServerProtocolFilter.SessionList;
            foreach (var sessionWrapper in sessionList)
            {
                if (sessionWrapper.Id != sessionId)
                {
                    _Kernel.ServerHandler.WriteToSession(sessionWrapper.Id, protocol);
                }
            }
        }

        /// <summary>
        /// 收到了初始化测试回复
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        private void OnInitializeTestReply(long sessionId, BytesProtocol protocol)
        {
            var address = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            if (InitializeConnectionReply.Parse(ref address, protocol))
            {
                if (_SessionIdAddressMap.ContainsKey(sessionId))
                {
                    _SessionIdAddressMap[sessionId] = address;
                }
                else
                {
                    _SessionIdAddressMap.Add(sessionId, address);
                }
            }
        }

        /// <summary>
        /// 收到读取测试用例结果
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="protocol"></param>
        private void OnReadTestCaseResultReply(long sessionId, BytesProtocol protocol)
        {
            var repliedResult = new TestCaseResult();
            if (ReadTestCaseResultReply.Parse(ref repliedResult, protocol))
            {
                //_logger.Info("测试用例结果解析成功");
                if (_SessionIdTestResultMap.ContainsKey(sessionId))
                {
                    _SessionIdTestResultMap[sessionId] = repliedResult;
                }
                else
                {
                    _SessionIdTestResultMap.Add(sessionId, repliedResult);
                }
            }
        }
    }
}
