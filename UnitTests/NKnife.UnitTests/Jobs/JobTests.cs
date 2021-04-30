using System;
using FluentAssertions;
using NKnife.Interface;
using Xunit;

namespace NKnife.UnitTests.Jobs
{
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

        [Fact]
        public void CtorTest()
        {
            var job = new Job
            {
                Interval = 1000,
                IsLoop = true,
                Timeout = 1200,
                LoopCount = 1234,
                Run = CotrTestFunc1,
                Verify = CotrTestFunc2
            };

            job.Should().NotBeNull();
            job.CountOfCompleted.Should().Be(0);
            job.IsPool.Should().BeFalse();
        }

        private class Job : IJob
        {
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopCount { get; set; }
            public int CountOfCompleted { get; set; }
            public Func<IJob, bool> Run { get; set; }
            public Func<IJob, bool> Verify { get; set; }
        }

    }
}
