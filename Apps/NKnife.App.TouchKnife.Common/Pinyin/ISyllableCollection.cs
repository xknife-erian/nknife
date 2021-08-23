namespace NKnife.App.TouchInputKnife.Commons.Pinyin
{
    public interface ISyllableCollection
    {
        /// <summary>
        /// </summary>
        /// <param name="py"></param>
        /// <returns>
        ///     0 是一个完整的合法音节时
        ///     1 不是一个完整的合法音节，但是它是至少一个合法音节的前缀时
        ///     2 不是一个完整的合法音节
        /// </returns>
        int Check(string py);
    }
}