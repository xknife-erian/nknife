using System.Runtime.InteropServices;

namespace NKnife.App.XPath.Performance
{
    public class HighResolutionTimeSpan
    {
        private readonly long m_Frequency;

        public HighResolutionTimeSpan()
        {
            m_Frequency = 0L;
            Miliseconds = 0f;
            QueryPerformanceFrequency(ref m_Frequency);
        }

        public HighResolutionTimeSpan(long Ticks)
        {
            m_Frequency = 0L;
            Miliseconds = 0f;
            QueryPerformanceFrequency(ref m_Frequency);
            Miliseconds = 1000f*(Ticks/((float) m_Frequency));
        }

        public long Frequency
        {
            get { return m_Frequency; }
        }

        public float Miliseconds { get; private set; }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);

        public override string ToString()
        {
            return string.Format("{0}", Miliseconds);
        }
    }
}