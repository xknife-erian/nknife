using System;
using FluentAssertions;
using NKnife.Interface;
using NUnit.Framework;

namespace NKnife.UnitTest.Jobs
{
    [TestFixture]
    public class JobTests
    {
        private bool CotrTestFunc1(IJob job)
        {
            return true;
        }
        private bool CotrTestFunc2(IJob data)
        {
            return true;
        }

        [Test]
        public void CtorTest()
        {
            var job = new Job
            {
                Interval = 1000,
                IsLoop = true,
                Timeout = 1200,
                LoopNumber = 1234,
                Run = CotrTestFunc1,
                Verify = CotrTestFunc2
            };
            job.Should().NotBeNull();
            job.IsPool.Should().BeFalse();
        }

        private class Job : IJob
        {
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopNumber { get; set; }
            public Func<IJob, bool> Run { get; set; }
            public Func<IJob, bool> Verify { get; set; }
        }

    }
}
