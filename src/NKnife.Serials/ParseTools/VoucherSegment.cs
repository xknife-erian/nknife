using System.Linq;
using NKnife.CRC;
using NKnife.Interface;

namespace NKnife.Serials.ParseTools
{
    /// <summary>
    ///     半包数据及相关属性
    /// </summary>
    public class VoucherSegment : Voucher
    {
        public VoucherSegment(FieldConfig fc)
        {
            _fieldConfig = fc;
            var n = fc.Begin.Item2 + fc.Attribute.Item2 + fc.Address.Item2 + fc.Command.Item2 + fc.DataFieldLength.Item2;
            _head = new byte[n];
            _tail = new byte[fc.CRC + fc.End];
        }

        /// <summary>
        ///     未解析完成的原因
        /// </summary>
        public UnresolvedCompletionReason UnresolvedCompletionReason { get; set; } = UnresolvedCompletionReason.Unknown;

        /// <summary>
        /// 当前已写入的数据量
        /// </summary>
        public int CountCurrentlyWritten
        {
            get
            {
                if (_data == null)
                    return _headWritePosition;
                return _headWritePosition + _data.Count + _tailWritePosition;
            }
        }

        /// <summary>
        /// 当前已写入的数据域的数据个数
        /// </summary>
        public int CountOfDataFieldCurrentlyWritten => _data?.Count ?? 0;

        private ICRCProvider _crcProvider;
        private void InitialiseCRCProvider()
        {
            var factory = new CRCFactory();
            _crcProvider = factory.CreateProvider(_fieldConfig.CRCProvider);
            _crcProvider.Endianness = _fieldConfig.Endianness;
        }

        /// <summary>
        ///     检查CRC校验码与尾部标记的正确性
        /// </summary>
        /// <returns>当true时，校验通过</returns>
        public bool Verify(bool skipCRC)
        {
            if (!skipCRC)
            {
                if (_crcProvider == null)
                    InitialiseCRCProvider();
                var array = _head.Concat(_data).ToArray();
                var crc = _crcProvider.CRCheck(array);
                // 注意：下方的比较有大小端之分
                if (crc[0] != _tail[0] || crc[1] != _tail[1])
                    return false;
                return _tail[2].Equals(0xCC);
            }
            return true;
        }
    }
}