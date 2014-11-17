using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>�����߼�����ʱ�ķ����ӿ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public interface IChangeLogicFunc<T>
    {
        /// <summary>�߼�����ʱ����������
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>ִ���߼������Ķ���
        /// </summary>
        /// <param name="sourceLogics">���ο����߼����Դ</param>
        /// <param name="targetLogics">��������߼���</param>
        /// <returns></returns>
        bool Execute(IDictionary<string, ServiceQueueLogic> sourceLogics, IDictionary<string, ServiceQueueLogic> targetLogics);
    }
}