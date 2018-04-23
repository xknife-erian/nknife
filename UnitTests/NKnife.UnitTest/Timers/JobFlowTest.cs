using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.Interface;
using NKnife.Timers;
using NUnit.Framework;

namespace NKnife.UnitTest.Timers
{
    [TestFixture]
    public class JobFlowTest
    {
        private class Job : IJob
        {
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopNumber { get; set; }
            public Func<IJob, bool> Func { get; set; }
        }

        private class JobPool : List<IJobPoolItem>, IJobPool
        {
            public bool IsPool { get; } = true;
        }

        private int _Number = 0;

        private bool CountFunc(IJob job)
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
            flow.Pool = new JobPool();
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
            flow.Pool = new JobPool();
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
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2});
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
            flow.Pool = new JobPool();

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
            flow.Pool = new JobPool();
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

        #region 005：测试中止功能

        private readonly JobFlow _RunTest005Flow = new JobFlow();
        
        private bool OnBreakCountFunc(IJob job)
        {
            _Number++;
            if (_Number >= 100)
                _RunTest005Flow.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能。一个无限循环的工作，当完成到100项时，中止。
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
            _RunTest005Flow.Pool = new JobPool();
            _RunTest005Flow.Pool.Add(job2);
            _RunTest005Flow.Run();
            _Number.Should().Be(100);
        }

        #endregion

        #region 006：测试暂停与继续功能

        private readonly JobFlow _RunTest006Flow = new JobFlow();
        private int _Count006 = 0;
        private int _Number006 = 0;

        private bool OnPauseCountFunc(IJob job)
        {
            _Number006++;
            if (_Number006 % 10 == 0)
            {
                _RunTest006Flow.Pause();
                _RunTest006Flow.Resume();
                _Count006++;
            }
            return true;
        }

        /// <summary>
        /// 006：测试暂停与继续功能。一个无限循环工作，当完成到10的倍数时，暂停，直到完成100项。
        /// </summary>
        [Test]
        public void RunTest006()
        {
            _Count006 = 0;
            _Number006 = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopNumber = 100,
                Interval = 2,
                Timeout = 15,
                Func = OnPauseCountFunc
            };
            _RunTest006Flow.Pool = new JobPool();
            _RunTest006Flow.Pool.Add(job);
            _RunTest006Flow.Run();
            _Count006.Should().Be(10);
            _Number006.Should().Be(100);
        }

        #endregion

        /// <summary>
        /// 测试递归：测试工作流中的工作本身就是工作组，即测试递归的有效性
        /// </summary>
        [Test]
        public void RunTest007()
        {
            _Number = 0;
            var flow = new JobFlow();
            flow.Pool = new JobPool();

            // 先创建一个简单工作，加入
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc
            };
            flow.Pool.Add(job1);

            // 共5个工作。再创建一个复杂工作组，工作组中有5个工作，加入
            var group1 = new JobPool();
            for (int i = 0; i < 5; i++)
            {
                var job2 = new Job
                {
                    IsLoop = false,
                    Interval = 2,
                    Timeout = 15,
                    Func = CountFunc
                };
                group1.Add(job2);
            }
            flow.Pool.Add(group1);

            // 共25个工作。再创建一个复杂工作组，工作组有五个复杂工作，每个复杂工作中有5个简单工作，加入。
            var group2 = new JobPool();
            for (int i = 0; i < 5; i++)
            {
                var subGroup2 = new JobPool();
                for (int j = 0; j < 5; j++)
                {
                    var job2 = new Job
                    {
                        IsLoop = false,
                        Interval = 2,
                        Timeout = 15,
                        Func = CountFunc
                    };
                    subGroup2.Add(job2);
                }
                group2.Add(subGroup2);
            }
            flow.Pool.Add(group2);

            flow.Run();
            _Number.Should().Be(31);
        }

        /// <summary>
        /// 测试各个事件是否都被很好的触发。
        /// </summary>
        [Test]
        public void RunTest008()
        {
            _Number = 0;
            var allWorkDone = false;
            var runEvent = 0;
            var runCount = 0;
            var flow = new JobFlow();
            flow.Pool = new JobPool();
            flow.AllWorkDone += (s, e) => { allWorkDone = true; };

            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc,
            };
            var job2 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc,
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Func = CountFunc,
            };
            flow.Running += (s, e) =>
            {
                runEvent.Should().Be(0);
                runEvent++;
                runCount++;
            };
            flow.Ran += (s, e) =>
            {
                runEvent.Should().Be(1);
                runEvent--;
            };
            flow.Pool.AddRange(new[] {job1, job2, job3});
            flow.Run();
            _Number.Should().Be(3);
            runEvent.Should().Be(0);
            runCount.Should().Be(3);
            allWorkDone.Should().BeTrue();
        }

        #region 009：测试中止功能的事件

        private readonly JobFlow _RunTest009Flow = new JobFlow();

        private bool On009BreakCountFunc(IJob job)
        {
            _Number++;
            if (_Number >= 100)
                _RunTest009Flow.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能的事件。
        /// </summary>
        [Test]
        public void RunTest009()
        {
            var allWorkDone = false;
            _RunTest009Flow.Pool = new JobPool();
            _RunTest009Flow.AllWorkDone += (s, e) =>
            {
                allWorkDone = true;//应该无法进入该事件
            };
            _Number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 0,
                Interval = 2,
                Timeout = 15,
                Func = On009BreakCountFunc
            };
            _RunTest009Flow.Pool.Add(job2);
            _RunTest009Flow.Run();
            _Number.Should().Be(100);
            allWorkDone.Should().BeFalse();
        }

        #endregion



    }
}