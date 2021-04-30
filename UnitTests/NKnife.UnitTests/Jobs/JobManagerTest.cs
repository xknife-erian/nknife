using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.Interface;
using NKnife.Jobs;
using Xunit;

namespace NKnife.UnitTests.Jobs
{
    public class JobManagerTest
    {
        private class Job : IJob
        {
            public int Id { get; set; }
            public bool IsPool { get; } = false;
            public int Timeout { get; set; }
            public bool IsLoop { get; set; }
            public int Interval { get; set; }
            public int LoopCount { get; set; }
            public int CountOfCompleted { get; set; }
            public Func<IJob, bool> Run { get; set; }
            public Func<IJob, bool> Verify { get; set; }
        }

        private class JobPool : List<IJobPoolItem>, IJobPool
        {
            public bool IsPool { get; } = true;

            #region Implementation of IJobPool

            /// <summary>
            /// 工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。
            /// </summary>
            public bool IsOverall { get; set; } = false;

            #endregion
        }

        private int _number = 0;

        private bool CountFunc(IJob job)
        {
            _number++;
            return true;
        }

        #region 基础

        [Fact]
        public void Test_基础_单个工作_指定循环次数()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.Add(job);
            flow.Run();
            _number.Should().Be(job.LoopCount);
        }

        /// <summary>
        /// 单个工作，指定循环次数
        /// </summary>
        [Fact]
        public void Test_基础_两个工作_分别指定循环次数()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool();
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 1,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 500,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2});
            flow.Run();
            _number.Should().Be(job1.LoopCount + job2.LoopCount);
        }

        [Fact]
        public void Test_基础_多个工作_工作均无需循环()
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

        [Fact]
        public void Test_基础_三个工作_第1工作_第3工作无需循环_第2工作指定循环次数()
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
                LoopCount = 100,
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
            flow.Pool.AddRange(new[] {job1, job2, job3});
            flow.Run();
            _number.Should().Be(102);
        }

        #endregion

        #region 暂停与继续功能

        #region 暂停与继续

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

        [Fact]
        public void Test_暂停与继续功能_设置1个循环100次的工作_当完成到10的倍数时_暂停后继续_直到完成100项()
        {
            _count006 = 0;
            _number006 = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 100,
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

        #region 暂停

        private readonly JobManager _runTest006AManager = new JobManager();
        private int _count006A = 0;

        private bool OnPauseCountFuncA(IJob job)
        {
            _count006A++;
            if (_count006A == 5)
            {
                _runTest006AManager.Pause();
            }

            return true;
        }

        [Fact]
        public void Test_暂停与继续功能_设置1个循环20次的工作_当完成到5次时暂停_测试是否真正暂停()
        {
            _count006A = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 20,
                Interval = 2,
                Timeout = 5,
                Run = OnPauseCountFuncA
            };
            _runTest006AManager.Pool = new JobPool();
            _runTest006AManager.Pool.Add(job);
            //另起一个线程执行，当执行计数到5的时候，暂停
            Task.Factory.StartNew(() => _runTest006AManager.Run());
            //如果暂停成功，计数器计数应该是5，而无论再等待多少时间
            Thread.Sleep(100);
            //断言测试计数器
            _count006A.Should().Be(5);

            _runTest006AManager.Resume();
            _runTest006AManager.Break();
        }

        #endregion

        #endregion

        #region 递归

        [Fact]
        public void Test_递归_测试工作流中的工作本身就是工作组_即测试递归的有效性()
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

        #endregion

        #region 事件

        [Fact]
        public void Test_事件_测试工作流中的各个事件是否都被很好的触发()
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

        #endregion

        #region 循环

        #region 普通轮循

        private int _number_a1 = 0;
        private int _number_b1 = 0;
        private int _number_c1 = 0;

        private bool CountFunc1(IJob job)
        {
            _number_a1++;
            return true;
        }

        private bool CountFunc2(IJob job)
        {
            _number_b1++;
            return true;
        }

        private bool CountFunc3(IJob job)
        {
            _number_c1++;
            return true;
        }

        [Fact]
        public void Test_轮循模式_池中三个工作_每工作执行50次()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc3
            };
            flow.Pool.AddRange(new IJobPoolItem[] { job1, job2, job3 });
            flow.Run();
            _number_a1.Should().Be(job1.LoopCount);
            _number_b1.Should().Be(job2.LoopCount);
            _number_c1.Should().Be(job3.LoopCount);
        }

        #endregion

        #region 2

        private int _number_a2 = 0;
        private int _number_b2 = 0;
        private int _number_c2 = 0;

        private bool CountFuncA1(IJob job)
        {
            _number_a2++;
            return true;
        }

        private bool CountFuncA2(IJob job)
        {
            _number_b2++;
            return true;
        }

        private bool CountFuncA3(IJob job)
        {
            _number_c2++;
            return true;
        }

        [Fact]
        public void Test_轮循测试_池中三个工作_第一个工作无限循环_另两个工作执行5次_轮循()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 0,//无限循环
                Interval = 2,
                Timeout = 15,
                Run = CountFuncA1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFuncA2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 5,
                Interval = 2,
                Timeout = 15,
                Run = CountFuncA3
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            Task.Factory.StartNew(() => flow.Run());
            Thread.Sleep(500); //500毫秒足够每个工作各执行多次了
            flow.Pause();
            // 因为是轮循模式，所以虽然第一个工作是无限循环，但是后面的工作也都要启动，在500毫秒应该都完成计数。
            _number_c2.Should().Be(5);
            _number_b2.Should().Be(50);
            _number_a2.Should().BeGreaterThan(100);
        }

        #endregion

        #region 3

        private int _number_a3 = 0;
        private int _number_b3 = 0;
        private int _number_c3 = 0;

        private bool CountFuncB1(IJob job)
        {
            _number_a3++;
            return true;
        }

        private bool CountFuncB2(IJob job)
        {
            _number_b3++;
            return true;
        }

        private bool CountFuncB3(IJob job)
        {
            _number_c3++;
            return true;
        }

        /// <summary>
        /// 设定三个工作，第一个工作无限循环，其他两个工作各执行50次，采用单个工作执行完成再继续的模式
        /// </summary>
        [Fact]
        public void Test_轮循测试_池中三个工作_第一个工作无限循环_另两个工作执行50次_非轮循()
        {
            _number = 0;
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内不用整体轮循，而是一项一项完成
                IsOverall = false
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
                Interval = 2,
                Timeout = 15,
                Run = CountFuncB1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFuncB2
            };
            var job3 = new Job
            {
                IsLoop = true,
                LoopCount = 50,
                Interval = 2,
                Timeout = 15,
                Run = CountFuncB3
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            Task.Factory.StartNew(() => flow.Run());
            Thread.Sleep(500);
            flow.Pause();
            _number_a3.Should().BeGreaterThan(100);
            _number_b3.Should().Be(0);
            _number_c3.Should().Be(0);
        }
        
        #endregion

        #endregion

        #region 工作的间隔时间是否符合逻辑进行测试

        private const int LOOP_COUNT_13_14 = 3;
        private const int INTERVAL_13_14 = 500;
        private short _num13 = 0;
        private readonly Stopwatch _watch13 = new Stopwatch();

        private bool CountFunc13(IJob job)
        {
            if (_num13 < 1)
            {
                _num13++;
                _watch13.Reset();
                _watch13.Start();
                return true;
            }

            _watch13.Stop();
            long watchTime = _watch13.ElapsedMilliseconds;
            watchTime.Should().BeLessOrEqualTo(INTERVAL_13_14 + 2, $"【Job ID:{((Job) job).Id}】【Num:{_num13}】");
            watchTime.Should().BeGreaterOrEqualTo(INTERVAL_13_14 - 2, $"【Job ID:{((Job) job).Id}】【Num:{_num13}】");
            _watch13.Reset();
            _watch13.Start();
            return true;
        }

        [Fact]
        public void Test_针对工作的间隔时间是否符合逻辑进行测试1()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                Id = 1,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job2 = new Job
            {
                Id = 2,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job3 = new Job
            {
                Id = 3,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            flow.Run();
        }

        [Fact]
        public void Test_针对工作的间隔时间是否符合逻辑进行测试2()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：单项工作结束后再进行下一项工作，不在组内整体轮循
                IsOverall = false
            };
            var job1 = new Job
            {
                Id = 1,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job2 = new Job
            {
                Id = 2,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            var job3 = new Job
            {
                Id = 3,
                IsLoop = true,
                LoopCount = LOOP_COUNT_13_14,
                Interval = INTERVAL_13_14,
                Timeout = INTERVAL_13_14 * 2,
                Run = CountFunc13
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2, job3});
            flow.Run();
        }

        #endregion

        #region 中止工作流

        #region 中止事件以及中止前完成的数量

        private readonly JobManager _runTest009Manager = new JobManager();

        private bool On009BreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest009Manager.Break();
            return true;
        }

        [Fact]
        public void Test_中止事件以及中止前完成的数量()
        {
            var allWorkDone = false;
            _runTest009Manager.Pool = new JobPool();
            _runTest009Manager.AllWorkDone += (s, e) =>
            {
                allWorkDone = true; //应该无法进入该事件
            };
            _number = 0;
            var job = new Job
            {
                IsLoop = true,
                LoopCount = 2000,
                Interval = 2,
                Timeout = 15,
                Run = On009BreakCountFunc
            };
            _runTest009Manager.Pool.Add(job);
            _runTest009Manager.Run();
            _number.Should().Be(100);
            allWorkDone.Should().BeTrue();
        }

        #endregion

        #region 中止轮循

        private int _number014_1 = 0;
        private int _number014_2 = 0;

        private bool CountFunc014_1(IJob job)
        {
            _number014_1++;
            return true;
        }

        private bool CountFunc014_2(IJob job)
        {
            _number014_2++;
            return true;
        }

        [Fact]
        public void Test_中止_轮循模式()
        {
            var flow = new JobManager();
            flow.Pool = new JobPool()
            {
                //关键属性：在组内整体轮循
                IsOverall = true
            };
            var job1 = new Job
            {
                IsLoop = true,
                LoopCount = 500000,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc014_1
            };
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 500000,
                Interval = 2,
                Timeout = 15,
                Run = CountFunc014_2
            };
            flow.Pool.AddRange(new IJobPoolItem[] {job1, job2});
            Task.Factory.StartNew(() => flow.Run());
            Thread.Sleep(5);
            flow.Break();
            
            _number014_1.Should().BeLessThan(job1.LoopCount).And.BeGreaterThan(0);
            _number014_2.Should().BeLessThan(job2.LoopCount).And.BeGreaterThan(0);
        }

        #endregion

        #region 中止_一个无限循环的工作_当完成到100项时中止

        private readonly JobManager _runTest005Manager = new JobManager();

        private bool OnBreakCountFunc(IJob job)
        {
            _number++;
            if (_number >= 100)
                _runTest005Manager.Break();
            return true;
        }

        [Fact]
        public void Test_中止_一个无限循环的工作_当完成到100项时中止()
        {
            _number = 0;
            var job2 = new Job
            {
                IsLoop = true,
                LoopCount = 0,
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

        #endregion

    }
}