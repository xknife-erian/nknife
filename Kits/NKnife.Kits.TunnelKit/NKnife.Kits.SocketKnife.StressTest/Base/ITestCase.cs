using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public interface ITestCase
    {
        bool Start(IKernel kernel);
        bool Abort();

        event EventHandler Finished;
        event EventHandler Aborted;

    }
}
