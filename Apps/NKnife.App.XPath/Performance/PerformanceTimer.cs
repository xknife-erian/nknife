using System.Runtime.InteropServices;

namespace NKnife.App.XPath.Performance
{
    public class PerformanceTimer
    {
        private readonly long m_Frequency;
        private long m_StartCount;
        private long m_StopCount;

        public PerformanceTimer()
        {
            Elapsed = 0L;
            m_Frequency = 0L;
            QueryPerformanceFrequency(ref m_Frequency);
        }

        public long Elapsed { get; private set; }

        public HighResolutionTimeSpan ElapsedTime
        {
            get { return new HighResolutionTimeSpan(Elapsed); }
        }

        public long Frequency
        {
            get { return m_Frequency; }
        }

        public float Seconds
        {
            get { return (Elapsed/((float) m_Frequency)); }
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);

        public void Reset()
        {
            m_StartCount = 0L;
            m_StopCount = 0L;
            Elapsed = 0L;
        }

        public void Start()
        {
            m_StartCount = 0L;
            QueryPerformanceCounter(ref m_StartCount);
        }

        public void Stop()
        {
            m_StopCount = 0L;
            QueryPerformanceCounter(ref m_StopCount);
            Elapsed = m_StopCount - m_StartCount;
        }
    }
}