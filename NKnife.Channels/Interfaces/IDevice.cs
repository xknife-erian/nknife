namespace NKnife.Channels.Interfaces
{
    /// <summary>
    ///     一个描述设备的接口
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        ///     生产厂商
        /// </summary>
        string Manufacturer { get; set; }

        /// <summary>
        ///     型号
        /// </summary>
        string Model { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     设备常用简称
        /// </summary>
        string AbbrName { get; }

        /// <summary>
        ///     设备地址
        /// </summary>
        int Address { get; set; }

        /// <summary>
        ///     设备说明或信息
        /// </summary>
        string Information { get; set; }
    }
}