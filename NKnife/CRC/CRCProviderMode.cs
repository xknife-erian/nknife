namespace NKnife.CRC
{
    /// <summary>
    /// CRC计算器的计算模式
    /// </summary>
    public enum CRCProviderMode : uint
    {
        /// <summary>正序。</summary>
        CRC8 = 0xD5,
        /// <summary>逆序。适用于DS18B20。</summary>
        CRC8_DS18B20 = 0x8C,
        /// <summary></summary>
        CRC8_CCITT = 0x07,
        /// <summary></summary>
        CRC8_DALLASMAXIM = 0x31,
        /// <summary></summary>
        CRC8_SAEJ1850 = 0x1D,
        /// <summary></summary>
        CRC8_WCDMA = 0x9B,
        /// <summary></summary>
        CRC16 = 0xA000,
        /// <summary></summary>
        CRC16_CCITT_XModem = 0x0000,
        /// <summary></summary>
        CRC16_CCITT_0x0000 = 0x0000,
        /// <summary></summary>
        CRC16_CCITT_0xFFFF = 0xFFFF,
        /// <summary></summary>
        CRC16_CCITT_0x1D0F = 0x1D0F,
        /// <summary></summary>
        CRC16_Kermit = 0x8408,
        /// <summary></summary>
        CRC16_Modbus = 0xA001,
        /// <summary>适用于文件校验</summary>
        CRC32 = 0x04C11DB7,
    }
}