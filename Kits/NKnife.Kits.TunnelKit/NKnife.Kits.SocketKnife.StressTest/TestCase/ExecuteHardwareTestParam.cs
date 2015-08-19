using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{

    public class ExecuteHardwareTestParam
    {
        [DisplayName("发送时间间隔（毫秒）")]
        public int SendInterval { get; set; } //发送时间间隔
        [DisplayName("发送测试数据长度")]
        public int TestDataLength { get; set; } //发送测试数据长度
        [DisplayName("发送帧数")]
        public int FrameCount { get; set; }
        [DisplayName("数据发送持续时长（秒），0表示不计时，只能手动停止")]
        public int SendDuration { get; set; }
        public ExecuteHardwareTestParam()
        {
            SendInterval = 20;
            TestDataLength = 100;
            FrameCount = 10000;
            SendDuration = 0;
        }
    }
}
