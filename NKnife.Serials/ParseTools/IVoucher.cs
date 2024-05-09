using System;

namespace NKnife.Serials.ParseTools
{
    /// <summary>
    /// 数据凭据。一般用在通讯中。
    /// </summary>
    public interface IVoucher
    {
        /// <summary>
        /// 起始标记
        /// </summary>
        ArraySegment<byte> Begin { get; }
        /// <summary>
        /// 凭据属性
        /// </summary>
        ArraySegment<byte> Attribute { get; }
        /// <summary>
        /// 地址
        /// </summary>
        ArraySegment<byte> Address { get; }

        /// <summary>
        ///     已解析出的协议的命令字
        /// </summary>
        ArraySegment<byte> Command { get; }

        /// <summary>
        /// 数据域长度
        /// </summary>
        ArraySegment<byte> DataFieldLength { get; }
        /// <summary>
        /// 数据域
        /// </summary>
        ArraySegment<byte> DataField { get; }
        /// <summary>
        /// 校验
        /// </summary>
        ArraySegment<byte> CRC { get; }
        /// <summary>
        /// 结尾标记
        /// </summary>
        ArraySegment<byte> End { get; }

        /// <summary>
        /// 
        /// </summary>
        IVoucher ToVoucher();
    }
}