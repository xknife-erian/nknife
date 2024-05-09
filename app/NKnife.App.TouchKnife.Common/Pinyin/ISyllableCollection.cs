namespace NKnife.App.TouchInputKnife.Commons.Pinyin
{
    public interface ISyllableCollection
    {
        /// <summary>
        /// </summary>
        /// <param name="py"></param>
        /// <returns>
        ///     0 ��һ�������ĺϷ�����ʱ
        ///     1 ����һ�������ĺϷ����ڣ�������������һ���Ϸ����ڵ�ǰ׺ʱ
        ///     2 ����һ�������ĺϷ�����
        /// </returns>
        int Check(string py);
    }
}