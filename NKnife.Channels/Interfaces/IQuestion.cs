namespace NKnife.Channels.Interfaces
{
    /// <summary>
    /// 本接口描述向一个观察点进行问询的数据封装
    /// </summary>
    public interface IQuestion<T> : ISwap<T>
    {
        /// <summary>
        /// 该询问是否需要循环。true时需要,false时仅问询一次。
        /// </summary>
        bool IsLoop { get; set; }

        /// <summary>
        /// 当循环时的间隔
        /// </summary>
        int LoopInterval { get; set; }

        int GetTimeout();
    }
}
