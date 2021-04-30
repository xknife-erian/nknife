using System;
using System.Collections.Generic;

namespace NKnife.Serials.ParseTools
{
    public interface IVoucherTool
    {
        /// <summary>
        ///     凭据中各字段的属性
        /// </summary>
        FieldConfig Field { get; set; }

        /// <summary>
        /// 是否跳过CRC校验。
        /// </summary>
        bool SkipCRC { get; set; } 

        /// <summary>
        ///     针对红外通讯，解析收到的数据
        /// </summary>
        /// <param name="message">指定的字节数据</param>
        /// <param name="vouchers">已解析完成的凭据集合</param>
        bool TryParse(ArraySegment<byte> message, ref List<IVoucher> vouchers);
    }
}