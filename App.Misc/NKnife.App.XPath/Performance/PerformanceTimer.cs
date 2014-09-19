namespace Performance
{
    using System;
    using System.Runtime.InteropServices;

    public class PerformanceTimer
    {
        private long m_ElapsedCount = 0L;
        private long m_Frequency = 0L;
        private long m_StartCount = 0L;
        private long m_StopCount = 0L;

        public PerformanceTimer()
        {
            this.m_Frequency = 0L;
            QueryPerformanceFrequency(ref this.m_Frequency);
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);
        public void Reset()
        {
            this.m_StartCount = 0L;
            this.m_StopCount = 0L;
            this.m_ElapsedCount = 0L;
        }

        public void Start()
        {
            this.m_StartCount = 0L;
            QueryPerformanceCounter(ref this.m_StartCount);
        }

        public void Stop()
        {
            this.m_StopCount = 0L;
            QueryPerformanceCounter(ref this.m_StopCount);
            this.m_ElapsedCount = this.m_StopCount - this.m_StartCount;
        }

        public long Elapsed
        {
            get
            {
                return this.m_ElapsedCount;
            }
        }

        public HighResolutionTimeSpan ElapsedTime
        {
            get
            {
                return new HighResolutionTimeSpan(this.m_ElapsedCount);
            }
        }

        public long Frequency
        {
            get
            {
                return this.m_Frequency;
            }
        }

        public float Seconds
        {
            get
            {
                return (((float) this.m_ElapsedCount) / ((float) this.m_Frequency));
            }
        }
    }
}

