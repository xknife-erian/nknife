namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    public interface IDeleteFunc<T>
    {
        /// <summary>ɾ�����׵�ɸѡ����
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>ִ��ɾ��
        /// </summary>
        /// <returns></returns>
        bool Execute();
    }
}