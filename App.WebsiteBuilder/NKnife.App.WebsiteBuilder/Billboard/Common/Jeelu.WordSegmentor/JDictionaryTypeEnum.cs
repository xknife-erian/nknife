namespace Jeelu.Billboard
{
    /// <summary>
    /// Jeelu分词组件的词库类型，枚举名将做为该类型的词库文件的后缀名
    /// </summary>
    public enum JDictionaryTypeEnum
    {
        /// <summary>
        /// 主词库
        /// </summary>
        Dictionary,
        /// <summary>
        ///频道(按关键词分类)词库，比如： "北京交通大学"词条属于 教育 - 大学 频道
        /// </summary>
        ChannelDictionary,
        /// <summary>
        /// 网站特许高权重词库(2008.7暂未使用)
        /// </summary>
        SiteDictionary,
        /// <summary>
        /// 主停止词库
        /// </summary>
        StopDictionary,
        /// <summary>
        /// 网站特许停止词库
        /// </summary>
        SiteStopDictionary,
        /// <summary>
        /// 嘛也不是
        /// </summary>
        None,
    }

}
