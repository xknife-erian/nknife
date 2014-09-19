namespace Jeelu.SimplusD
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SequenceType
    {
        /// <summary>
        /// 没方式
        /// </summary>
        None = 0,
        /// <summary>
        /// 按照时间排序（最近的在前）
        /// </summary>
        Recent = 1,
        /// <summary>
        /// 自定义关键字匹配排序
        /// </summary>
        CustomKeyWord = 2,
        /// <summary>
        /// 自动根据关键字匹配排序
        /// </summary>
        AutoKeyWord=3
    }
}