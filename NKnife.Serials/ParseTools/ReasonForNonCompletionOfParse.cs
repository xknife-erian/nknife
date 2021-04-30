namespace NKnife.Serials.ParseTools
{
    /// <summary>
    ///     未解析完成的原因
    /// </summary>
    public enum ReasonForNonCompletionOfParse
    {
        Unknown,

        /// <summary>
        ///     仅有起始字符
        /// </summary>
        OnlyStartChar,

        /// <summary>
        ///     仅有起始字符、地址
        /// </summary>
        OnlyStartAndAddressChar,

        /// <summary>
        ///     仅有起始字符、地址、命令字
        /// </summary>
        OnlyStartAndAddressCharAndCommandChar,

        /// <summary>
        ///     起始字符、地址和命令字是正确的，但是长度数据(多个字节)不完整
        /// </summary>
        IncompleteLengthData,

        /// <summary>
        ///     不完整的数据域
        /// </summary>
        IncompleteDataFields,

        /// <summary>
        ///     不完整的CRC校验标记
        /// </summary>
        IncompleteCRCMarks,

        /// <summary>
        ///     缺少CRC校验标记
        /// </summary>
        MissingCRCMarks,

        /// <summary>
        ///     缺少尾部标记
        /// </summary>
        MissingEndMarks
    }
}