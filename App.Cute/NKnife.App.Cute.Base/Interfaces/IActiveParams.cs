using System;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>��Ĳ���
    /// </summary>
    public interface IActiveParams
    {
        /// <summary>���λ�������ߣ���ʵ����һ��Ҳ�Ǳ�ϵͳ���û���ֻ������ʱ��Ϊ�����ߡ�
        /// </summary>
        string Asker { get; set; }

        /// <summary>������ʱ����Դ�ı�ϵͳ�û���ID
        /// </summary>
        string UserId { get; set; }

        /// <summary>����ID��ԤԼ������ָ���ָ������
        /// </summary>
        string QueueId { get; set; }

        /// <summary>��������Ĳ������ϲ���䱾����
        /// </summary>
        /// <param name="args"></param>
        IActiveParams Parse(params object[] args);
    }
}