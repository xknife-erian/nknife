using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace NKnife.Wrapper
{
    /// <summary>
    /// 一个用来提醒任务时间节点的类型，象个小闹钟一样。
    /// </summary>
    public class TaskAlarm : IDisposable
    {
        readonly ManualResetEvent _clockAliveEvent = new ManualResetEvent(false);
        
        private readonly bool _isRunnig = false;
        private bool _isTiming = false;
        private long _startTime, _stopTime;
        private readonly long _freq;
        private double _milSecondCount = 0;

        /// <summary>
        /// Gets 任务持续时间
        /// </summary>
        /// <value>The duration.</value>
        public double Duration
        {
            get { return _duration; }
        }
        private double _duration = 0;

        public TaskAlarm()
        {
            if (AlarmEventMethods.QueryPerformanceFrequency(out _freq) == false)
            {
                throw new Win32Exception();
            }
            _isRunnig = true;
            var timingThread = new Thread(new ThreadStart(this.WaitAWaken));
            timingThread.IsBackground = true;
            timingThread.Start();
        }

        public void Dispose()
        {
            ResetAlarm();
        }

        /// <summary>
        /// 设置闹钟xx毫秒后发出提醒
        /// </summary>
        /// <param name="milSecondCount"></param>
        public void SetAlarmAfter(double milSecondCount)
        {
            if (milSecondCount <= 10)
            {
                throw new Exception("时长必须大于10，单位毫秒");
            }
            ResetAlarm();
            Start();
            //设置参数
            _milSecondCount = milSecondCount;
            _isTiming = true;
            //唤醒线程
            AWake();
        }

        /// <summary>
        /// 重置闹钟，状态回到初始
        /// </summary>
        public void ResetAlarm()
        {
            _isTiming = false;
            _milSecondCount = 0;
            _duration = 0;
        }

        private void WaitAWaken()
        {
            while (_isRunnig)
            {
                //阻塞线程
                Block();
                //开始计时
                StartTiming();
            }
        }

        private void AWake()
        {
            _clockAliveEvent.Set();
        }

        private void Block()
        {
            _clockAliveEvent.Reset();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        private void StartTiming()
        {
            try
            {
                _clockAliveEvent.WaitOne();
                while (_duration < _milSecondCount)
                {
                    Thread.Sleep(10);
                    Stop();
                }
                if (_isTiming)
                    OnTimeoutAlarm(EventArgs.Empty);
            }
            catch
            {
                _isTiming = false;
            }
        }

        /// <summary>
        /// 当时间到了的时候发生的事件
        /// </summary>
        public event EventHandler TimeoutAlarmEvent;
        /// <summary>
        /// 当时间到了的时候,Raises the <see cref="E:TimeoutAlarmEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnTimeoutAlarm(EventArgs e)
        {
            if (TimeoutAlarmEvent != null)
                TimeoutAlarmEvent(this, e);
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        private bool Start()
        {
            Thread.Sleep(0);
            AlarmEventMethods.QueryPerformanceCounter(out _startTime);
            return true;
        }
        /// <summary>
        /// 停止计时器
        /// </summary>
        private bool Stop()
        {
            AlarmEventMethods.QueryPerformanceCounter(out _stopTime);
            this._duration = (double)(_stopTime - _startTime) / (double)_freq * 1000;
            return true;
        }

        private static class AlarmEventMethods
        {
            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceFrequency(out long lpFrequency);
        }
    }
}
