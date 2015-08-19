using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public abstract class AbstractTestCase:ITestCase
    {
        public EventHandler<TestCaseResultEventArgs> Finished { get; set; }
        public EventHandler<TestCaseResultEventArgs> Aborted { get; set; }

        protected bool _AbortFlag;

        public abstract void Start(IKernel kernel, object testCaseParam);

        public virtual void Abort()
        {
            _AbortFlag = true;
        }



        protected void KeepRunning(int duration)
        {
            _AbortFlag = false;
            if (duration <= 0) //不计时
            {
                while (!_AbortFlag)
                {
                    Thread.Sleep(1000); //1秒检查一次
                }
            }
            else
            {
                Thread.Sleep(1000 * duration); //持续5秒钟时间
            }
        }
    }
}
