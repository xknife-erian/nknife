namespace Jeelu.SimplusD
{
    /// <summary>
    /// 页面片断枚举：当sdpage在生成成真实页面的时候，
    /// 当该页对应有正文内容时，头部信息，正文内容，会生成成一个片断文件
    /// design by lukan, 2008-6-26 14:18:10
    /// </summary>
    public enum PageSection
    {
        /// <summary>
        /// 页面主体
        /// </summary>
        MainPage,
        /// <summary>
        /// 页面头部
        /// </summary>
        Head,
        /// <summary>
        /// 页面正文
        /// </summary>
        Content,
        /// <summary>
        /// 什么都不是
        /// </summary>
        None,
    }
}
