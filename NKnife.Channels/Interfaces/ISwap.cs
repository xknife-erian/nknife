using NKnife.Interface;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    /// 代表数据交换的一个方向的描述。如 PC 向 Instrument 询问时，本接口将询问的内容，询问的通道，谁询问，问谁等等信息封装在一起。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISwap<T> : IJob
    {
        /// <summary>
        /// 本次交换的数据
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// 被问询的设备
        /// </summary>
        IId Instrument { get; }
    }
}