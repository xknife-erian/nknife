namespace Performance
{
    using System;
    using System.Runtime.InteropServices;

    public class HighResolutionTimeSpan
    {
        private long m_Frequency;
        private float m_Miliseconds;

        public HighResolutionTimeSpan()
        {
            this.m_Frequency = 0L;
            this.m_Miliseconds = 0f;
            QueryPerformanceFrequency(ref this.m_Frequency);
        }

        public HighResolutionTimeSpan(long Ticks)
        {
            this.m_Frequency = 0L;
            this.m_Miliseconds = 0f;
            QueryPerformanceFrequency(ref this.m_Frequency);
            this.m_Miliseconds = 1000f * (((float) Ticks) / ((float) this.m_Frequency));
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);
        public override string ToString()
        {
            return string.Format("{0}", this.m_Miliseconds);
        }

        public long Frequency
        {
            get
            {
                return this.m_Frequency;
            }
        }

        public float Miliseconds
        {
            get
            {
                return this.m_Miliseconds;
            }
        }
    }
}

