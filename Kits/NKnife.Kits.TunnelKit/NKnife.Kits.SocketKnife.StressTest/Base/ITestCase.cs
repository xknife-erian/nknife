using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public interface ITestCase
    {
        void Start(IKernel kernel, object testCaseParam);
        void Abort();

        EventHandler<TestCaseResultEventArgs> Finished { get; set; }
        EventHandler<TestCaseResultEventArgs> Aborted { get; set; }

    }
}
