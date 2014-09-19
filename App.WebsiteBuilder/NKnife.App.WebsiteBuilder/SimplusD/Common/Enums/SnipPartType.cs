namespace Jeelu.SimplusD
{
    /// <summary>
    /// 页面片组成部分的类型
    /// </summary>
    public enum SnipPartType
    {
        /// <summary>
        /// 不是页面片的组成部分
        /// </summary>
        None = 0,

        /// <summary>
        /// 静态类型的组成部分(Html代码)
        /// </summary>
        Static = 2,

        /// <summary>
        /// 导航类型的组成部分
        /// </summary>
        Navigation = 3,

        /// <summary>
        /// List类型的组成部分
        /// </summary>
        List = 4,

        /// <summary>
        /// List类型Part中的特定类型（类容器型）
        /// </summary>
        ListBox = 5,
        
        /// <summary>
        /// 导航容器类型的组成部分
        /// </summary>
        Box = 6,

        /// <summary>
        /// 通过定制特性创建的SNIP类型
        /// </summary>
        Attribute = 7,
        
        /// <summary>
        /// 面包渣
        /// </summary>
        Path = 8
    }
}