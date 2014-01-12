using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace NKnife.Platform.Timing
{
    public class AlarmClock : IDisposable
    {
        private readonly ManualResetEvent ClockAliveEvent = new ManualResetEvent(false);
        private readonly long _Freq;
        private readonly bool _IsRunnig;
        private double _DurationValue;
        private bool _IsTiming;
        private double _MilSecondCount;

        private long _StartTime, _StopTime;

        public AlarmClock()
        {
            if (DurationMethods.QueryPerformanceFrequency(out _Freq) == false)
            {
                throw new Win32Exception();
            }
            _IsRunnig = true;
            var TimingThread = new Thread(WaitAWaken);
            TimingThread.IsBackground = true;
            TimingThread.Start();
        }

        public double DurationValue
        {
            get { return _DurationValue; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            ResetAlarm();
        }

        #endregion

        public event EventHandler TimeoutAlarm;

        /// <summary>
        /// 设置闹钟xx毫秒后发出提醒
        /// </summary>
        /// <param name="secondCount"></param>
        public void SetAlarmAfter(double milSecondCount)
        {
            if (milSecondCount <= 10)
            {
                throw new Exception("时长必须大于10，单位毫秒");
            }
            ResetAlarm();
            Start();
            //设置参数
            _MilSecondCount = milSecondCount;
            _IsTiming = true;
            //唤醒线程
            AWake();
        }

        /// <summary>
        /// 重置闹钟，状态回到初始
        /// </summary>
        public void ResetAlarm()
        {
            _IsTiming = false;
            _MilSecondCount = 0;
            _DurationValue = 0;
        }

        private void WaitAWaken()
        {
            while (_IsRunnig)
            {
                //阻塞线程
                Block();
                //开始计时
                StartTiming();
            }
        }

        private void AWake()
        {
            ClockAliveEvent.Set();
        }

        private void Block()
        {
            ClockAliveEvent.Reset();
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="secondCount"></param>
        private void StartTiming()
        {
            try
            {
                ClockAliveEvent.WaitOne();
                while (_DurationValue < _MilSecondCount)
                {
                    Thread.Sleep(10);
                    Stop();
                }
                Alarm();
            }
            catch
            {
                _IsTiming = false;
            }
        }

        private void Alarm()
        {
            if (_IsTiming)
            {
                EventHandler handler = TimeoutAlarm;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        private bool Start()
        {
            Thread.Sleep(0);
            DurationMethods.QueryPerformanceCounter(out _StartTime);
            return true;
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        private bool Stop()
        {
            DurationMethods.QueryPerformanceCounter(out _StopTime);
            _DurationValue = (_StopTime - _StartTime)/(double) _Freq*1000;
            return true;
        }

        #region Nested type: DurationMethods

        internal static class DurationMethods
        {
            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

            [DllImport("Kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryPerformanceFrequency(out long lpFrequency);
        }

        #endregion
    }
}