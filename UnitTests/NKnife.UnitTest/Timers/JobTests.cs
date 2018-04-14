using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.Timers;
using NUnit.Framework;

namespace NKnife.UnitTest.Timers
{
    [TestFixture]
    public class JobTests
    {
        private bool CotrTestFunc1(Job job)
        {
            return true;
        }

        [Test]
        public void CtorTest()
        {
            var job = new Job();
            job.Interval = 1000;
            job.IsLoop = true;
            job.Timeout = 1200;
            job.LoopNumber = 1234;
            job.Func = CotrTestFunc1;
            job.Should().NotBeNull();
            job.IsPool.Should().BeFalse();
        }

    }
}
