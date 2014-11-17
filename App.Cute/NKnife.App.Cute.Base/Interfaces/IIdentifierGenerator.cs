namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>描述一个识别符的生成器
    /// </summary>
    public interface IIdentifierGenerator
    {
        /// <summary>识别符的分类
        /// </summary>
        int Kind { get; set; }

        /// <summary>识别符名称。一般用来在配置界面供显示给操作配置的人员观察。
        /// </summary>
        string Name { get; set; }

        /// <summary>获取一个交易标识符。在通过排队机取号时，一般是一个票号。
        /// </summary>
        /// <returns></returns>
        string GetIdentifier();
    }
}