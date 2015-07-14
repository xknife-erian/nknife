using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    /// <summary>
    ///  针对一个下位机按照步骤顺序一步一步来
    /// </summary>
    public class SingleTalkTestCase:ITestCase
    {
        #region ITestCase
        public bool Start(IKernel kernel)
        {

            return true;
        }

        public bool Abort()
        {
            return true;
        }

        public event EventHandler Finished;
        public event EventHandler Aborted;
        #endregion
    }
}
