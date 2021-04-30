using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NKnife.Events;
using NKnife.Interface;

namespace NKnife.Jobs
{
    /// <summary>
    /// 描述针对JobPool的工作流的操作封装，其中包括仅执行一次的Job，和需要循环执行的Job。
    /// </summary>
    public class JobManager
    {
        private readonly AutoResetEvent _flowAutoResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 中断工作流的标记。
        /// </summary>
        private bool _breakFlag = false;

        /// <summary>
        /// 暂停工作流的标记
        /// </summary>
        private bool _pauseFlag = false;

        /// <summary>
        /// 构造函数。描述一个Job的顺序工作流，其中包括仅执行一次的Job,和需要循环执行的Job。
        /// </summary>
        public JobManager()
        {
            _flowAutoResetEvent.Reset();
        }

        /// <summary>
        /// 工作池。工作池中的Job将会被顺序执行一次（当某Job设定为无限循环时如果没有外部打断，将不会全部执行完毕）。
        /// </summary>
        public IJobPool Pool { get; set; }

        /// <summary>
        /// 运行工作流
        /// </summary>
        public virtual void Run()
        {
            _breakFlag = false;
            RunMethod(Pool);
        }

        /// <summary>
        /// 中断工作流
        /// </summary>
        public virtual void Break()
        {
            _breakFlag = true;
        }

        /// <summary>
        /// 暂停工作流，工作流只是暂停，下次启动时，会从断点处继续。
        /// </summary>
        public virtual void Pause()
        {
            _pauseFlag = true;
        }

        /// <summary>
        /// 从断点处继续工作流
        /// </summary>
        public virtual void Resume()
        {
            _pauseFlag = false;
            _flowAutoResetEvent.Set();
        }

        // /// <summary>
        // /// 当前Job的执行次数
        // /// </summary>
        // private int _loopNumber = 0;

        /// <summary>
        /// 递归完成内部所有的Job
        /// </summary>
        protected virtual void RunMethod(IJobPool jobPool)
        {
            if (jobPool.IsOverall) //是否整组轮循
            {   //整组轮循
                //给出一个当前池子的所有项目的标记，都置为未完成
                var all = new List<bool>();
                for (int i = 0; i < jobPool.Count; i++)
                    all.Add(false);
                while (all.Contains(false))//当所有工作完成后，循环结束
                {
                    for (int i = 0; i < jobPool.Count; i++)
                    {
                        if (_breakFlag) break; //当检测到中断信号时，不再运行Job
                        IJobPoolItem jobItem = jobPool.ElementAt(i);
                        if (jobItem is IJob job)
                        {
                            if (job.LoopCount == 0 || job.CountOfCompleted < job.LoopCount)
                            {
                                RunJob(job, jobPool.IsOverall); //执行单个Job的运行
                            }
                            else
                            {
                                all[i] = true;//置为已完成标记
                            }
                        }
                    }
                }
            }
            else
            {   //执行工作流的每一项工作，将本职工作脚踏实地的完成，才进行下一项工作
                foreach (IJobPoolItem jobItem in jobPool)
                {
                    if (_breakFlag) break; //当检测到中断信号时，不再运行Job
                    if (!jobItem.IsPool)
                    {
                        if (jobItem is IJob job)
                            RunJob(job, jobPool.IsOverall); //执行单个Job的运行
                    }
                    else
                    {
                        if (jobItem is IJobPool subPool)
                            RunMethod(subPool); //递归
                    }
                }
            }

            OnAllWorkDone();
        }
        
        /// <summary>
        /// 运行单个Job
        /// </summary>
        /// <param name="job">单个Job</param>
        /// <param name="isOverall">工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。</param>
        protected virtual void RunJob(IJob job, bool isOverall)
        {
            if (_breakFlag) //当检测到中断信号时，不再运行Job
                return;
            if (job.LoopCount > 0 && job.CountOfCompleted >= job.LoopCount) //已运行到指定次数的将不再运行
                return;
            OnRunning(new EventArgs<IJob>(job));
            var success = job.Run.Invoke(job);
            OnRan(new EventArgs<IJob>(job));
            //**当运行异常时，静置至超时时长，否则静默至间隔时长即结束**
            _flowAutoResetEvent.WaitOne(success ? job.Interval : job.Timeout);
            if (_pauseFlag) //检测暂停标记
                _flowAutoResetEvent.WaitOne();
            job.CountOfCompleted++;

            if (ConditionsForJob(job, isOverall))
            {
                //递归循环执行本职工作 ;-)
                RunJob(job, false);
            }
        }

        /// <summary>
        /// 当该Job需要循环
        /// 当没有设置循环次数，即无限循环
        /// 当已设置循环次数，但是已循环次数小于设置值
        /// </summary>
        /// <param name="job">单个Job</param>
        /// <param name="isOverall">工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。</param>
        /// <returns>指定的<see cref="job"/>需要循环处理</returns>
        private static bool ConditionsForJob(IJob job, bool isOverall)
        {
            return !isOverall && job.IsLoop && ((job.LoopCount <= 0) || (job.LoopCount > 0) && (job.CountOfCompleted < job.LoopCount));
        }

        /// <summary>
        /// 当所有工作均已完成时发生
        /// </summary>
        public event EventHandler AllWorkDone;

        /// <summary>
        /// 当Job即将被执行时发生
        /// </summary>
        public event EventHandler<EventArgs<IJob>> Running;

        /// <summary>
        /// 当Job执行完成后发生
        /// </summary>
        public event EventHandler<EventArgs<IJob>> Ran;

        protected virtual void OnRunning(EventArgs<IJob> e)
        {
            Running?.Invoke(this, e);
        }

        protected virtual void OnRan(EventArgs<IJob> e)
        {
            Ran?.Invoke(this, e);
        }

        protected virtual void OnAllWorkDone()
        {
            AllWorkDone?.Invoke(this, EventArgs.Empty);
        }
    }
}