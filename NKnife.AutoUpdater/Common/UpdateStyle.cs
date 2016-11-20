namespace NKnife.AutoUpdater.Common
{
    public enum UpdateStyle
    {
        /// <summary>
        /// 不自动更新
        /// </summary>
        None,
        /// <summary>
        /// 程序启动时更新
        /// </summary>
        First,

        /// <summary>
        /// 程序运行时定时更新
        /// </summary>
        Timer,

        /// <summary>
        /// 手动更新
        /// </summary>
        Manual
    }
}