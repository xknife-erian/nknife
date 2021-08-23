using System;
using System.ComponentModel;
using NKnife.CRC.CRCProvider;
using NKnife.Interface;

namespace NKnife.CRC
{
    /// <summary>
    ///     从CRC模式工厂获取指定模式的CRC校验器
    /// </summary>
    public class CRCFactory
    {
        /// <summary>
        ///     从CRC模式工厂获取指定模式的CRC校验器
        /// </summary>
        /// <param name="provider">指定的校验模式</param>
        public ICRCProvider CreateProvider(CRCProviderMode provider)
        {
            if (!Enum.IsDefined(typeof(CRCProviderMode), provider)) 
                throw new InvalidEnumArgumentException(nameof(provider), (int) provider, typeof(CRCProviderMode));
            switch (provider)
            {
                case CRCProviderMode.CRC16:
                    return new CRC16();
                case CRCProviderMode.CRC32:
                    return new CRC32();
                case CRCProviderMode.CRC8:
                    return new CRC8();
                case CRCProviderMode.CRC8_DS18B20:
                    return new CRC8(0x8C);
                case CRCProviderMode.CRC8_CCITT:
                    return new CRC8(0x07);
                case CRCProviderMode.CRC8_DALLASMAXIM:
                    return new CRC8(0x31);
                case CRCProviderMode.CRC8_SAEJ1850:
                    return new CRC8(0x1D);
                case CRCProviderMode.CRC8_WCDMA:
                    return new CRC8(0x9B);
                case CRCProviderMode.CRC16_Modbus:
                    return new CRC16Modbus();
                case CRCProviderMode.CRC16_CCITT_0x0000:
                    return new CRC16CCITT();
                case CRCProviderMode.CRC16_CCITT_0xFFFF:
                    return new CRC16CCITT(0xFFFF);
                case CRCProviderMode.CRC16_CCITT_0x1D0F:
                    return new CRC16CCITT(0x1D0F);
                case CRCProviderMode.CRC16_Kermit:
                    return new CRC16Kermit();
                default:
                    return new CRC16Modbus();
            }
        }
    }
}