namespace NKnife.Interface
{
    /// <summary>
    /// 描述一个可以被加入到工作集合的子项目
    /// </summary>
    public interface IJobPoolItem
    {
        /// <summary>
        /// 是否是工作集合，通过这个特点，以保证工作集合的树状
        /// </summary>
        bool IsPool { get; }
    }
}