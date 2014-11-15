namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>����һ��ʶ�����������
    /// </summary>
    public interface IIdentifierGenerator
    {
        /// <summary>ʶ����ķ���
        /// </summary>
        int Kind { get; set; }

        /// <summary>ʶ������ơ�һ�����������ý��湩��ʾ���������õ���Ա�۲졣
        /// </summary>
        string Name { get; set; }

        /// <summary>��ȡһ�����ױ�ʶ������ͨ���Ŷӻ�ȡ��ʱ��һ����һ��Ʊ�š�
        /// </summary>
        /// <returns></returns>
        string GetIdentifier();
    }
}