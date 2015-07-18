using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class SpeechTestParam
    {
        [DisplayName("通话时长（秒）")]
        public int SpeechDuration { get; set; }

        public SpeechTestParam()
        {
            SpeechDuration = 5;
        }
    }
}
