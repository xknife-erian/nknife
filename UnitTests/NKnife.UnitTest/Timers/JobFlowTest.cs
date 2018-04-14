using System;
using System.CodeDom;
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
    public class JobFlowTest
    {
        private int _Number = 0;

        private bool CountFunc(Job job)
        {
            _Number++;
            return true;
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Test]
        public void RunTest001()
        {
            _Number = 0;
            var flow = new JobFlow();
            var job = new Job
            {
                IsLoop = true,
                LoopNumber = 50,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            flow.Pool.Add(job);
            flow.Run();
            _Number.Should().Be(job.LoopNumber);
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Test]
        public void RunTest002()
        {
            _Number = 0;
            var flow = new JobFlow();
            var job1 = new Job
            {
                IsLoop = true,
                LoopNumber = 1,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 500,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            flow.Pool.AddRange(new[] {job1, job2});
            flow.Run();
            _Number.Should().Be(job1.LoopNumber + job2.LoopNumber);
        }

        /// <summary>
        /// 多个工作，工作均无需循环
        /// </summary>
        [Test]
        public void RunTest003()
        {
            _Number = 0;
            var flow = new JobFlow();
            for (int i = 0; i < 300; i++)
            {
                var job = new Job
                {
                    IsLoop = false,
                    Interval = 2,
                    Timeout = 15,
                    Func = CountFunc
                };
                flow.Pool.Add(job);
            }
            flow.Run();
            _Number.Should().Be(300);
        }

        /// <summary>
        /// 三个工作，第1工作，第3工作无需循环，第2工作指定循环次数
        /// </summary>
        [Test]
        public void RunTest004()
        {
            _Number = 0;
            var flow = new JobFlow();
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 100,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            flow.Pool.AddRange(new []{job1,job2,job3});
            flow.Run();
            _Number.Should().Be(102);
        }

        private readonly JobFlow _RunTest005Flow = new JobFlow();

        private bool OnBreakCountFunc(Job job)
        {
            _Number++;
            if (_Number >= 100)
                _RunTest005Flow.Break();
            return true;
        }

        /// <summary>
        /// 一个无限循环工作
        /// </summary>
        [Test]
        public void RunTest005()
        {
            _Number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 0,
                Interval = 2,
                Timeout = 15,
                Func = OnBreakCountFunc
            };
            _RunTest005Flow.Pool.Add(job2);
            _RunTest005Flow.Run();
            _Number.Should().Be(100);
        }
    }
}