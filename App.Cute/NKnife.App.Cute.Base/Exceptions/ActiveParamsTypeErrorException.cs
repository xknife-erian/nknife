using System;

namespace Didaku.Engine.Timeaxis.Base.Exceptions
{
    /// <summary>����һ���쳣�������Activity�����������
    /// </summary>
    public class ActiveParamsTypeErrorException : TimeaxisException
    {
        /// <summary>�쳣�������Activity�����������
        /// </summary>
        /// <param name="expectType">����������</param>
        /// <param name="actualType">ʵ�ʵ�����</param>
        public ActiveParamsTypeErrorException(Type expectType, Type actualType)
            : base(string.Format("�����Activity����������ͣ�����ֵ��:{0},ʵ����{1}", expectType.Name, actualType.Name))
        {
        }
    }
}