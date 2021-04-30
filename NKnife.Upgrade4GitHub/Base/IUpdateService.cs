namespace NKnife.Upgrade4Github.Base
{
    public interface IUpdateService
    {
        /// <summary>
        ///     从GitHub的releases/latest获取最新的更新信息，是否需要更新，由用户决定
        /// </summary>
        void GetLatestReleaseInfo();

        /// <summary>
        ///     运行从GitHub的releases/latest更新过程
        /// </summary>
        void StartAsync();
    }
}