﻿namespace NKnife.CRC
{
    /// <summary>
    /// CRC计算器。
    /// 循环冗余校验（Cyclic Redundancy Check， CRC）是一种根据网络数据包或计算机文件等数据产生简短固定位数校验码的一种信道编码技术，主要用来检测或校验数据传输或者保存后可能出现的错误。它是利用除法及余数的原理来作错误侦测的。
    /// </summary>
    public interface ICRCProvider
    {
        /// <summary>
        /// 返回的校验码的大小端模式
        /// </summary>
        Endianness Endianness { get; set; }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="source">待校验的数据</param>
        /// <returns>循环冗余校验码（CRC）</returns>
        // ReSharper disable once InconsistentNaming
        byte[] CRCheck(byte[] source);
    }
}