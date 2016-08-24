﻿using System;
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
    public class SpeechTalkTestCase : AbstractTestCase
    {
        private static readonly ILog _logger = LogManager.GetLogger<SingleTalkTestCase>();
        private IKernel _Kernel;
        private ServerHandler _SeverHandler;
        private ManualResetEvent _TestStepResetEvent = new ManualResetEvent(false);
        private int _ReplyWaitTimeout = 10000; //协议发送后，等待回复的超时时间，单位毫秒

        private byte[] _CurrentInitializeRepliedSessionAddress = { 0x00, 0x00, 0x00, 0x00 };
        private Dictionary<long, long> _SessionAddressIdMap = new Dictionary<long, long>();
        private Dictionary<long, long> _SessionAddressFrameCountMap = new Dictionary<long, long>();
        private bool _OnDataTransfer;

        #region ITestCase

        public override void Start(IKernel kernel, object testCaseParam = default(SpeechTestParam))
        {
            var param = testCaseParam as SpeechTestParam;
            if (param == null)
            {
                OnTestCaseFinished(false, "测试参数不正确");
                return;
            }
            Task.Factory.StartNew(() =>
            {
                _logger.Info("启动测试案例：SpeechTalkTestCase");
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
                SetOnWaitProtocol(InitializeConnectionReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new InitializeConnection(NangleProtocolUtility.ServerAddress));
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("向sessionIdA发送协议InitializeTest后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了sessionIdA执行初始化的回复
                Array.Copy(_CurrentInitializeRepliedSessionAddress, sessionAddressA, 4);
                _SessionAddressIdMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressA), sessionIdA);
                _SessionAddressFrameCountMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressA),0);
                SetOnWaitProtocol(InitializeConnectionReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new InitializeConnection(NangleProtocolUtility.ServerAddress));
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
                _SessionAddressFrameCountMap.Add(NangleCodecUtility.ConvertFromFourBytesToInt(sessionAddressB),0);
                
                //第二步：执行设置语音模式，使A和B均进入对讲模式
                //向sessionB发执行设置语音模式，使其进入对讲模式
                SetOnWaitProtocol(SetSpeechModeReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new SetSpeechMode(
                    (byte)NangleProtocolUtility.SpeechMode.Talk,
                    sessionAddressA));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议SetSpeechMode后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了设置语音模式的回复

                //向sessionA发执行设置语音模式，使其进入对讲模式
                SetOnWaitProtocol(SetSpeechModeReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new SetSpeechMode(
                    (byte)NangleProtocolUtility.SpeechMode.Talk,
                    sessionAddressB));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议SetSpeechMode后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了设置语音模式的回复
                //第三步：记录接下来收到的数据，并持续一段时间
                _logger.Info("开始语音对讲");
                _OnDataTransfer = true;
                KeepRunning(param.SpeechDuration);
                _OnDataTransfer = false;
                //第四步：执行设置语音模式，使A和B均进入idle模式
                //向sessionB发执行设置语音模式，使其进入idle模式
                SetOnWaitProtocol(SetSpeechModeReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdB, new SetSpeechMode(
                    (byte)NangleProtocolUtility.SpeechMode.Idle,
                    sessionAddressA));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议SetSpeechMode后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了设置语音模式的回复

                //向sessionA发执行设置语音模式，使其进入idle模式
                SetOnWaitProtocol(SetSpeechModeReply.CommandIntValue);
                _Kernel.ServerHandler.WriteToSession(sessionIdA, new SetSpeechMode(
                    (byte)NangleProtocolUtility.SpeechMode.Idle,
                    sessionAddressB));
                _TestStepResetEvent.Reset();
                if (!_TestStepResetEvent.WaitOne(_ReplyWaitTimeout))
                {
                    _logger.Warn("发送协议SetSpeechMode后，等待回复超时");
                    OnTestCaseFinished(false);
                    return;
                }
                //收到了设置语音模式的回复

                //第五步：读取SessionA测试用例执行结果，进行分析，发出报告
                OnTestCaseFinished(true, VerifyTestCaseResult(param));
            });
        }

        #endregion

        private string VerifyTestCaseResult(SpeechTestParam param)
        {
            var sb = new StringBuilder();
            foreach (var key in _SessionAddressIdMap.Keys)
            {
                var sessionId = _SessionAddressIdMap[key];
                var count = _SessionAddressFrameCountMap[key];
                sb.Append(string.Format("收到来自Session{0}的语音数据帧{1}个，", sessionId, count));
            }
            return string.Format("服务端{0}，通话时长{1}秒", sb, param.SpeechDuration);
        }

        private int _CurrentCommandIntValue;
        private void SetOnWaitProtocol(int commandIntValue)
        {
            _CurrentCommandIntValue = commandIntValue;
        }

        private void OnTestCaseFinished(bool result, string message = "")
        {
            _OnDataTransfer = false;
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
            if (commandIntValue == InitializeConnectionReply.CommandIntValue) //收到了初始化回复
            {
                OnInitializeTestReply(protocol);
            }
            else if (commandIntValue == SpeechRawData.CommandIntValue) //收到语音数据帧
            {
                OnSpeechRawData(protocol);
                
            }

            if (commandIntValue == _CurrentCommandIntValue)
            {
                _TestStepResetEvent.Set();
            }


        }

        private void OnSpeechRawData(BytesProtocol protocol)
        {
            if (!_OnDataTransfer) return;
            var targetAddress = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            if (SpeechRawData.Parse(ref targetAddress, protocol))
            {
                var key = NangleCodecUtility.ConvertFromFourBytesToInt(targetAddress);
                if (_SessionAddressIdMap.ContainsKey(key))
                {
                    var targetSessionId = _SessionAddressIdMap[key];
                    _Kernel.ServerHandler.WriteToSession(targetSessionId, protocol);
                    _SessionAddressFrameCountMap[key] += 1;
                    _logger.Debug(string.Format("收到语音数据帧第{0}条", _SessionAddressFrameCountMap[key]));
                }
            }
        }

        /// <summary>
        /// 收到了初始化测试回复
        /// </summary>
        /// <param name="protocol"></param>
        private void OnInitializeTestReply(BytesProtocol protocol)
        {
            InitializeConnectionReply.Parse(ref _CurrentInitializeRepliedSessionAddress, protocol);
        }
    }
}
