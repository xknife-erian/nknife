namespace NKnife.Channels.Interfaces.Channels
{
    /// <summary>
    /// 代表数据交换的一个方向的描述。如PC向Device询问时，本接口将询问的内容，询问的通道，谁询问，问谁等等信息封装在一起。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISwap<T>
    {
        /// <summary>
        /// 本次交换的数据
        /// </summary>
        T Data { get; set; }

        /// <summary>
        ///     该问询流向的通道
        /// </summary>
        IChannel<T> Channel { get; }

        /// <summary>
        ///     被问询的设备
        /// </summary>
        IDevice Device { get; }

        /// <summary>
        ///     被问询的设备可能工作于不同的子对象,子工作
        /// </summary>
        IExhibit Exhibit { get; }
    }
}