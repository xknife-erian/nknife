using System;

namespace NKnife.App.Cute.Base.Exceptions
{
    /// <summary>���������������ʱ����ת��Ϊ���������ڲ�����ʱ�쳣
    /// </summary>
    public class ActiveParamsDataConvertErrorException : TimeaxisException
    {
        public ActiveParamsDataConvertErrorException(Exception e)
            : base("���������������ʱ����ת��Ϊ���������ڲ�����ʱ�쳣", e)
        {
        }
    }
}