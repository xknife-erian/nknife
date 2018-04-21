using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NKnife.Events;
using NKnife.Interface;

namespace NKnife.Timers
{
    /// <summary>
    /// 描述一个Job的顺序工作流，其中包括仅执行一次的Job，和需要循环执行的Job。
    /// </summary>
    public class JobFlow
    {
        private readonly AutoResetEvent _Flow = new AutoResetEvent(false);

        /// <summary>
        /// 中断工作流的标记。
        /// </summary>
        private bool _BreakFlag = false;

        /// <summary>
        /// 暂停工作流的标记
        /// </summary>
        private bool _PauseFlag = false;

        /// <summary>
        /// 构造函数。描述一个Job的顺序工作流，其中包括仅执行一次的Job,和需要循环执行的Job。
        /// </summary>
        public JobFlow()
        {
            _Flow.Set();
        }

        /// <summary>
        /// 工作池。工作池中的Job将会被顺序执行一次（当某Job设定为无限循环时如果没有外部打断，将不会全部执行完毕）。
        /// </summary>
        public JobPool Pool { get; } = new JobPool();

        /// <summary>
        /// 运行工作流
        /// </summary>
        public virtual void Run()
        {
            _BreakFlag = false;
            RunMethod(Pool);
        }

        /// <summary>
        /// 中断工作流
        /// </summary>
        public virtual void Break()
        {
            _BreakFlag = true;
        }

        /// <summary>
        /// 暂停工作流，工作流只是暂停，下次启动时，会从断点处继续。
        /// </summary>
        public virtual void Pause()
        {
            _PauseFlag = true;
        }

        /// <summary>
        /// 从断点处继续工作流
        /// </summary>
        public virtual void Resume()
        {
            _PauseFlag = false;
            _Flow.Set();
        }

        /// <summary>
        /// 当前Job的执行次数
        /// </summary>
        private int _LoopNumber = 0;

        /// <summary>
        /// 递归完成内部所有的Job
        /// </summary>
        protected virtual void RunMethod(IList<IJobPoolItem> list)
        {
            var hasBreak = false;

            foreach (var jobItem in list)
            {
                if (_BreakFlag)//当检测到中断信号时，不再运行Job
                {
                    hasBreak = true;
                    break;
                }

                if (!jobItem.IsPool)
                {
                    if (jobItem is Job job)
                    {
                        _LoopNumber = 0;
                        RunJob(job); //执行单个Job的运行
                    }
                }
                else
                {
                    if (jobItem is JobPool pool)
                        RunMethod(pool); //递归
                }
            }

            if (!hasBreak)//如是有中断信号，那么不算是所有工作完成
                OnAllWorkDone();
        }

        /// <summary>
        /// 运行单个Job
        /// </summary>
        /// <param name="job">单个Job</param>
        protected virtual void RunJob(Job job)
        {
            if (_BreakFlag)//当检测到中断信号时，不再运行Job
                return;
            OnRunning(new EventArgs<Job>(job));
            var success = job.Func.Invoke(job);
            OnRan(new EventArgs<Job>(job));
            //当运行异常时，静置至超时时长，否则静默至间隔时长即结束
            _Flow.WaitOne(success ? job.Interval : job.Timeout);
            if (_PauseFlag)//检测暂停标记
                _Flow.Reset();
            _LoopNumber++;
            //当该Job需要循环
            //当没有设置循环次数，即无限循环
            //当已设置循环次数，但是已循环次数小于设置值
            if (job.IsLoop && ((job.LoopNumber <= 0) || (job.LoopNumber > 0) && (_LoopNumber < job.LoopNumber)))
            {
                //递归循环执行本职工作 ;-)
                RunJob(job);
            }
        }

        /// <summary>
        /// 当所有工作均已完成时发生
        /// </summary>
        public event EventHandler AllWorkDone;

        /// <summary>
        /// 当Job即将被执行时发生
        /// </summary>
        public event EventHandler<EventArgs<Job>> Running;

        /// <summary>
        /// 当Job执行完成后发生
        /// </summary>
        public event EventHandler<EventArgs<Job>> Ran;

        protected virtual void OnRunning(EventArgs<Job> e)
        {
            Running?.Invoke(this, e);
        }

        protected virtual void OnRan(EventArgs<Job> e)
        {
            Ran?.Invoke(this, e);
        }

        protected virtual void OnAllWorkDone()
        {
            AllWorkDone?.Invoke(this, EventArgs.Empty);
        }
    }
}