namespace NKnife.Channels.Interfaces.Channels
{
    /// <summary>
    ///     一个描述对指定输入数据进行实时处理的算法的接口
    /// </summary>
    public interface IAlgorithm<in T>
    {
        /// <summary>
        ///     小数位数
        /// </summary>
        ushort DecimalDigit { get; set; }

        /// <summary>
        ///     输出数据
        /// </summary>
        double Output { get; }

        /// <summary>
        ///     清除数据
        /// </summary>
        void Clear();

        /// <summary>
        ///     输入数据
        /// </summary>
        /// <param name="src">指定的输入数据</param>
        void Input(T src);
    }
}