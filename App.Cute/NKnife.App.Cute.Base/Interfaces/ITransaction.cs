using System;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>��¼һ��ʱ������ÿ�������Ľ�����Ϣ
    /// </summary>
    public interface ITransaction
    {
        string Id { get; set; }

        /// <summary>ʶ���롣���ʶ������޶��ڶ��ݵ�ʱ����(���죬�����)�ǲ��ظ��ġ����Ŷ�ϵͳ�У�����һ���Ŷӵ�Ʊ�š�
        /// </summary>
        string Identifier { get; set; }

        /// <summary>ԤԼ�Ķ��е�Id
        /// </summary>
        string Queue { get; set; }

        /// <summary>��¼���׵Ŀ�ʼʱ�䡣
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>�û�Id
        /// </summary>
        string User { get; set; }

        /// <summary>�����������׵�Activity��Kind
        /// </summary>
        int Owner { get; set; }

        /// <summary>ǰ�õ�Transaction��ID�ļ���
        /// </summary>
        string[] Previous { get; set; }
    }
}