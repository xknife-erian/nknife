using NKnife.CRC;

namespace NKnife.Serials.ParseTools
{
    /// <summary>
    ///     凭据中各字段的配置
    /// </summary>
    public class FieldConfig
    {
        private short _headLength = -1;
        private short _tailLength = -1;

        public FieldConfig()
        {
        }

        public FieldConfig(
            byte[] beginChars,
            byte[] endChars,
            CRCProviderMode provider,
            Endianness endianness,
            int dataFieldMaxLength,
            (short, short) begin,
            (short, short) attribute,
            (short, short) address,
            (short, short) command,
            (short, short) dataFieldLength,
            short crc = 2, short end = 1)
        {
            BeginChars = beginChars;
            EndChars = endChars;
            CRCProvider = provider;
            Endianness = endianness;
            DataFieldMaxLength = dataFieldMaxLength;
            Begin = begin;
            Attribute = attribute;
            Address = address;
            Command = command;
            DataFieldLength = dataFieldLength;
            CRC = crc;
            End = end;
        }

        /// <summary>
        ///     起始字节
        /// </summary>
        public byte[] BeginChars { get; set; } = {0xAA};

        /// <summary>
        ///     结束字符
        /// </summary>
        public byte[] EndChars { get; set; } = {0xCC};

        /// <summary>
        ///     凭据起始字符的属性。Item1是在凭据中的索引；Item2是在凭据中所占字节数。
        /// </summary>
        public (short, short) Begin { get; set; } = (0, 1);

        /// <summary>
        ///     凭据的属性。Item1是在凭据中的索引；Item2是在凭据中所占字节数。
        /// </summary>
        public (short, short) Attribute { get; set; } = (-1, 0);

        /// <summary>
        ///     凭据的地址的属性。Item1是在凭据中的索引；Item2是在凭据中所占字节数。
        /// </summary>
        public (short, short) Address { get; set; } = (-1, 0);

        /// <summary>
        ///     凭据的命令字属性。Item1是在凭据中的索引；Item2是在凭据中所占字节数。
        /// </summary>
        public (short, short) Command { get; set; } = (1, 1);

        /// <summary>
        ///     凭据的实际数据域的长度属性。Item1是在凭据中的索引；Item2是在凭据中所占字节数。
        /// </summary>
        public (short, short) DataFieldLength { get; set; } = (2, 2);

        /// <summary>
        ///     凭据的校验属性。在凭据中所占字节数。
        /// </summary>
        public short CRC { get; set; } = 2;

        /// <summary>
        ///     凭据的结束符属性。在凭据中所占字节数。
        /// </summary>
        public short End { get; set; } = 1;

        /// <summary>
        ///     CRC校验方式
        /// </summary>
        public CRCProviderMode CRCProvider { get; set; } = CRCProviderMode.CRC16_Modbus;

        /// <summary>
        ///     大小端模式
        /// </summary>
        public Endianness Endianness { get; set; } = Endianness.LittleEndian;

        /// <summary>
        ///     数据域允许的最大长度
        /// </summary>
        public int DataFieldMaxLength { get; set; } = 1024;

        /// <summary>
        ///     获取头部的总体长度
        /// </summary>
        public short GetHeadLength()
        {
            if (_headLength == -1)
                _headLength = (short) (Begin.Item2 + Attribute.Item2 + Address.Item2 + Command.Item2 + DataFieldLength.Item2);
            return _headLength;
        }

        /// <summary>
        ///     获取尾部的总体长度
        /// </summary>
        public int GetTailLength()
        {
            if (_tailLength == -1)
                _tailLength = (short) (CRC + End);
            return _tailLength;
        }
    }
}