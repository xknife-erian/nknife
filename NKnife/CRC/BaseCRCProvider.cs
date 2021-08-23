using NKnife.Interface;

namespace NKnife.CRC
{
    /// <summary>
    /// CRC计算器的抽象实现
    /// </summary>
    public abstract class BaseCRCProvider : ICRCProvider
    {
        /// <summary>
        /// 返回的校验码的大小端模式
        /// </summary>
        public Endianness Endianness { get; set; } = Endianness.LittleEndian;

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="source">待校验的数据</param>
        /// <returns>CRC校验码</returns>
        public abstract byte[] CRCheck(byte[] source);

    }
}