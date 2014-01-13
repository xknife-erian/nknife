using System.Text;

namespace Gean.Wrapper.Logger
{
    /// <summary>
    /// 组装一些较易阅读的日志字符串
    /// </summary>
    public class LogString
    {
        private const string ARROW = " -->> ";

        /// <summary>
        /// 正常...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string Normal(string str)
        {
            var sb = new StringBuilder();
            return sb.Append("(正常)").Append(ARROW).Append(str).ToString();
        }
        /// <summary>
        /// 正常启动某...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string NormalStart(string str)
        {
            var sb = new StringBuilder();
            return sb.Append("(正常启动)").Append(ARROW).Append(str).ToString();
        }
        /// <summary>
        /// 任务已完成...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string TaskIsDone(string str)
        {
            var sb = new StringBuilder();
            return sb.Append("(任务已完成)").Append(ARROW).Append(str).ToString();
        }
    }
}
