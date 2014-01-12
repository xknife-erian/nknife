using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

[assembly: CLSCompliant(true)]
namespace NKnife.Wrapper
{
    /// <summary>
    /// 高精度计时器，主要为衡量“持续时间”而封装的类
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
    public class Duration
    {
        public static Duration GetDuration(DateTime beginTime, DateTime endTime)
        {
            Duration dura = new Duration();
            dura.Begin = beginTime;
            dura.End = endTime;
            return dura;
        }

        public static Duration Stop(DateTime beginTime)
        {
            Duration dur = new Duration();
            dur.Begin = beginTime;
            dur.End = DateTime.Now;
            dur.DurationValue = (dur.End - dur.Begin).TotalMilliseconds;
            return dur;
        }

        public static bool operator ==(Duration aTime, Duration bTime)
        {
            return aTime.DurationValue.Equals(bTime.DurationValue);
        }
        public static bool operator !=(Duration aTime, Duration bTime)
        {
            return !(aTime.DurationValue.Equals(bTime.DurationValue));
        }
        public static Duration operator -(Duration aTime, Duration bTime)
        {
            throw new NotImplementedException();
        }
        public static Duration operator +(Duration aTime, Duration bTime)
        {
            throw new NotImplementedException();
        }

        private long _StartTime, _StopTime;
        private long _Freq;

        public Duration()
        {
            if (DurationMethods.QueryPerformanceFrequency(out _Freq) == false)
            {
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// 开始点
        /// </summary>
        public DateTime Begin { get; set; }
        /// <summary>
        /// 结束点
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 获取与设置持续时间的值，以毫秒为单位
        /// </summary>
        public virtual double DurationValue
        {
            get
            {
                if (this._DurationValue <= 0)
                {
                    this._DurationValue = (this.End - this.Begin).TotalMilliseconds;
                }
                return this._DurationValue;
            }
            set { this._DurationValue = value; }
        }
        private double _DurationValue = 0;

        /// <summary>
        /// 开始计时器
        /// </summary>
        public bool Start()
        {
            if (this.RunFlag)
            {
                return false;
            }
            this.RunFlag = true;
            Thread.Sleep(0);
            this.Begin = DateTime.Now;
            DurationMethods.QueryPerformanceCounter(out _StartTime);
            return true;
        }
        /// <summary>
        /// 停止计时器
        /// </summary>
        public bool Stop()
        {
            if (!this.RunFlag)
            {
                return false;
            }
            this.RunFlag = false;
            this.End = DateTime.Now;
            DurationMethods.QueryPerformanceCounter(out _StopTime);
            this._DurationValue = (double)(_StartTime - _StopTime) / (double)_Freq * 1000;
            return true;
        }
        /// <summary>
        /// 高精度计时器是否启动的标记
        /// </summary>
        private bool RunFlag = false;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Begin:").Append(this.Begin.ToLongTimeString());
            sb.Append('|');
            sb.Append("End:").Append(this.End.ToLongTimeString());
            sb.Append('|');
            sb.Append("Duration:").Append(this.DurationValue.ToString());
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            Duration inputDur = (Duration)obj;
            if (!this.Begin.Equals(inputDur.Begin))
            {
                return false;
            }
            if (!this.End.Equals(inputDur.End))
            {
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(27 * Begin.GetHashCode() + End.GetHashCode() + DurationValue.GetHashCode());
        }

        internal static class DurationMethods
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
