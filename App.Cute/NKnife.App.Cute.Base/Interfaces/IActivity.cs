using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>����һ�����������Ϊһ��ԤԼ��ԤԼ�����Ĺ����ĸ����ڵ�Ĵ�������������һ�����������Ķ���
    /// ���Ŷ�ϵͳ�У���������һ�������С�����ת�ơ��ȶ�����
    /// </summary>
    public interface IActivity
    {
        /// <summary>�򱾻��������
        /// </summary>
        /// <param name="param">��Ĳ���</param>
        /// <param name="transaction">������Ľ�����Ϣ</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        bool Ask<T>(T param, out ITransaction transaction) where T : IActiveParams;

        /// <summary>�ҵ��뱾Activtyƥ��Ĳ�������
        /// </summary>
        /// <returns>һ���յĲ�������ʵ��</returns>
        IActiveParams Find();
    }
}