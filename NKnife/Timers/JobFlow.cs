using System;
using System.Collections.Generic;
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
        protected readonly AutoResetEvent _Flow = new AutoResetEvent(false);

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

        public void Run()
        {
            RunMethod(Pool);
        }

        /// <summary>
        /// 当前Job的执行次数
        /// </summary>
        protected int _LoopNumber = 0;

        /// <summary>
        /// 递归完成内部所有的Job
        /// </summary>
        protected void RunMethod(IList<IJobPoolItem> list)
        {
            foreach (var jobItem in list)
            {
                if (!jobItem.IsPool)
                {
                    if (jobItem is Job job)
                    {
                        _LoopNumber = job.LoopNumber;
                        RunJob(job); //执行单个Job的运行
                    }
                }
                else
                {
                    if (jobItem is JobPool pool)
                        RunMethod(pool);//递归
                }
            }
        }

        /// <summary>
        /// 运行单个Job
        /// </summary>
        /// <param name="job">单个Job</param>
        protected void RunJob(Job job)
        {
            OnRunning(new EventArgs<Job>(job));
            var success = job.Func.Invoke();
            OnRan(new EventArgs<Job>(job));
            //当运行异常时，静置至超时时长，否则静默至间隔时长即结束
            _Flow.WaitOne(success ? job.Interval : job.Timeout);
            _LoopNumber++;
            //当该Job需要循环
            //当没有设置循环次数，即无限循环
            //当已设置循环次数，但是已循环次数小于设置值
            if (job.IsLoop && ((job.LoopNumber <= 0) || (job.LoopNumber > 0) && (_LoopNumber < job.LoopNumber)))
            {
                //循环执行本职工作 ;-)
                RunJob(job);
            }
        }

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
    }
}