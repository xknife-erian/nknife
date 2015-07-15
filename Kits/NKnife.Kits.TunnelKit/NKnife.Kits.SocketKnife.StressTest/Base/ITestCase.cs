using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public interface ITestCase
    {
        void Start(IKernel kernel);
        void Abort();

        event EventHandler<TestCaseResultEventArgs> Finished;
        event EventHandler<TestCaseResultEventArgs> Aborted;

    }
}
