using System;
using System.Collections.Generic;
using FluentAssertions;
using NKnife.Interface;
using NKnife.Jobs;
using NUnit.Framework;

namespace NKnife.UnitTest.Jobs
{
    [TestFixture]
    public class JobManagerTest
    {
        private class Job : IJob
        {
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopNumber { get; set; }
            public Func<IJob, bool> Run { get; set; }
            public Func<byte[], bool> Verify { get; set; }
        }

        private class JobPool : List<IJobPoolItem>, IJobPool
        {
            public bool IsPool { get; } = true;
        }

        private int _number = 0;

        private bool CountFunc(IJob job)
        {
            _number++;
            return true;
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Test]
        public void RunTest001()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job = new Job
            {
                IsLoop = true,
                LoopNumber = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.Add(job);
            flow.Run();
            _number.Should().Be(job.LoopNumber);
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Test]
        public void RunTest002()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job1 = new Job
            {
                IsLoop = true,
                LoopNumber = 1,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 500,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2});
            flow.Run();
            _number.Should().Be(job1.LoopNumber + job2.LoopNumber);
        }

        /// <summary>
        /// 多个工作，工作均无需循环
        /// </summary>
        [Test]
        public void RunTest003()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();

            for (int i = 0; i < 300; i++)
            {
                var job = new Job
                {
                    IsLoop = false,
                    Interval = 2,
                    Timeout = 15,
                    Run = CountFunc
                };
                flow.Pool.Add(job);
            }
            flow.Run();
            _number.Should().Be(300);
        }

        /// <summary>
        /// 三个工作，第1工作，第3工作无需循环，第2工作指定循环次数
        /// </summary>
        [Test]
        public void RunTest004()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 100,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.AddRange(new []{job1,job2,job3});
            flow.Run();
            _number.Should().Be(102);
        }

        #region 005：测试中止功能

        private readonly JobManager _runTest005Manager = new JobManager();
        
        private bool OnBreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest005Manager.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能。一个无限循环的工作，当完成到100项时，中止。
        /// </summary>
        [Test]
        public void RunTest005()
        {
            _number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 0,
                Interval = 2,
                Timeout = 15,
                Run = OnBreakCountFunc
            };
            _runTest005Manager.Pool = new JobPool();
            _runTest005Manager.Pool.Add(job2);
            _runTest005Manager.Run();
            _number.Should().Be(100);
        }

        #endregion

        #region 006：测试暂停与继续功能

        private readonly JobManager _runTest006Manager = new JobManager();
        private int _count006 = 0;
        private int _number006 = 0;

        private bool OnPauseCountFunc(IJob job)
        {
            _number006++;
            if (_number006 % 10 == 0)
            {
                _runTest006Manager.Pause();
                _runTest006Manager.Resume();
                _count006++;
            }
            return true;
        }

        /// <summary>
        /// 006：测试暂停与继续功能。一个无限循环工作，当完成到10的倍数时，暂停，直到完成100项。
        /// </summary>
        [Test]
        public void RunTest006()
        {
            _count006 = 0;
            _number006 = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopNumber = 100,
                Interval = 2,
                Timeout = 15,
                Run = OnPauseCountFunc
            };
            _runTest006Manager.Pool = new JobPool();
            _runTest006Manager.Pool.Add(job);
            _runTest006Manager.Run();
            _count006.Should().Be(10);
            _number006.Should().Be(100);
        }

        #endregion

        /// <summary>
        /// 测试递归：测试工作流中的工作本身就是工作组，即测试递归的有效性
        /// </summary>
        [Test]
        public void RunTest007()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();

            // 先创建一个简单工作，加入
            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
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
                    Run = CountFunc
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
                        Run = CountFunc
                    };
                    subGroup2.Add(job2);
                }
                group2.Add(subGroup2);
            }
            flow.Pool.Add(group2);

            flow.Run();
            _number.Should().Be(31);
        }

        /// <summary>
        /// 测试各个事件是否都被很好的触发。
        /// </summary>
        [Test]
        public void RunTest008()
        {
            _number = 0;
            var allWorkDone = false;
            var runEvent = 0;
            var runCount = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            flow.AllWorkDone += (s, e) => { allWorkDone = true; };

            var job1 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
            };
            var job2 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
            };
            var job3 = new Job
            {
                IsLoop = false,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc,
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
            _number.Should().Be(3);
            runEvent.Should().Be(0);
            runCount.Should().Be(3);
            allWorkDone.Should().BeTrue();
        }

        #region 009：测试中止功能的事件

        private readonly JobManager _runTest009Manager = new JobManager();

        private bool On009BreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest009Manager.Break();
            return true;
        }

        /// <summary>
        /// 测试中止功能的事件。
        /// </summary>
        [Test]
        public void RunTest009()
        {
            var allWorkDone = false;
            _runTest009Manager.Pool = new JobPool();
            _runTest009Manager.AllWorkDone += (s, e) =>
            {
                allWorkDone = true;//应该无法进入该事件
            };
            _number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopNumber = 0,
                Interval = 2,
                Timeout = 15,
                Run = On009BreakCountFunc
            };
            _runTest009Manager.Pool.Add(job2);
            _runTest009Manager.Run();
            _number.Should().Be(100);
            allWorkDone.Should().BeFalse();
        }

        #endregion



    }
}