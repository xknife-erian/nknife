namespace NKnife.Upgrade4Github.Base
{
    /// <summary>
    /// 升级状态枚举
    /// </summary>
    enum UpdateStatus
    {
        Start,
        /// <summary>
        /// 从GitHub更新点获取更新已完成
        /// </summary>
        GetLatestReleaseCompleted,
        FileDownloading,
        Update,
        Done,
        Error,
        Cancelled
    }
}