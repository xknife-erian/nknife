using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Server;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Base;
using SerialKnife.Pan.Common;

namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class MockClientHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MockClientHandler>();
        public override List<byte[]> Commands { get; set; }

        public EventHandler<NangleProtocolEventArgs> ProtocolReceived;

        public long ClientAddressValue { get; set; }

        private bool _OnTask = false;
        public int FrameSent { get; set; }
        public int FrameReceived { get; set; }
        public int FrameLost { get; set; }

        public void StopTask()
        {
            _OnTask = false;
        }

        private void OnProtocolReceived(IProtocol<byte[]> protocol)
        {
            var handler = ProtocolReceived;
            if (handler != null)
            {
                handler.Invoke(this,new NangleProtocolEventArgs
                {
                    Protocol = (BytesProtocol)protocol,
                });
            }
        }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            ProcessProtocol(protocol);
            OnProtocolReceived(protocol);
        }

        private void ProcessProtocol(IProtocol<byte[]> protocol)
        {
            int intValue = NangleCodecUtility.ConvertFromTwoBytesToInt(protocol.Command);
            if (intValue == TestRawData.CommandIntValue) //收到测试数据
            {
                FrameReceived += 1;
            }
            else if (intValue == ExecuteTestCase.CommandIntValue) //收到执行测试用例的命令
            {
                var param = new ExecuteTestCaseParam();

                if (ExecuteTestCase.Parse(ref param, (BytesProtocol) protocol))
                {
                    ResetCount();

                    if (param.SendEnable)
                    {
                        _OnTask = true;
                        var tsk = new Task<bool>(TaskWorker, param, TaskCreationOptions.PreferFairness);
                        tsk.Start();
                    }
                }
            }
            else if (intValue == StopExecuteTestCase.CommandIntValue) //收到停止测试用例的命令
            {
                _logger.Debug("收到停止测试用例的命令");
                _OnTask = false;
            }
        }


        private bool TaskWorker(object arg)
        {
            try
            {
                var param = (ExecuteTestCaseParam) arg;
                var data = GenerateBytesData(param.FrameDataLength);

                for (int i = 0; i < param.FrameCount; i++)
                {
                    if (!_OnTask)
                        return true;
                    WriteToAllSession(new TestRawData(param.TargetAddress,(byte)(i % 256),data));

                    //_logger.Debug(string.Format("客户端发出数据{0}",i));
                    FrameSent += 1;
                    Thread.Sleep(param.SendInterval);
                }
                _OnTask = false;
                return true;
            }
            catch (Exception ex)
            {
                _OnTask = false;
                return false;
            }

        }

        private void ResetCount()
        {
            FrameSent = 0;
            FrameReceived = 0;
            FrameLost = 0;
        }

        private byte[] GenerateBytesData(long frameDataLength)
        {
            var result = new byte[frameDataLength];
            for (int i = 0; i < frameDataLength; i++)
            {
                result[i] = (byte)((i%2 == 0)? 0x01:0x02);
            }
            return result;
        }
    }
}
