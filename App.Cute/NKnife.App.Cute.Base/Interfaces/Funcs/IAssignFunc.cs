using System.Collections.Generic;
using Didaku.Wrapper;
using Didaku.Engine.Timeaxis.Base.Entities;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>����һ���Ӷ����з����Ŷӽ��׵ķ����ӿ�
    /// </summary>
    /// <typeparam name="T">����ʱ�Ĳ���</typeparam>
    /// <remarks></remarks>
    public interface IAssignFunc<T>
    {
        /// <summary>�������(��ȡ����)�Ĺ�̨
        /// </summary>
        /// <value>The counter.</value>
        /// <remarks></remarks>
        IdName Counter { get; set; }

        /// <summary>�������(��ȡ����)��Ա��
        /// </summary>
        /// <value>
        /// The staff.
        /// </value>
        IdName Staff { get; set; }

        /// <summary>����ʱ����������
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>��һ����̨�ķ����߼��л�ȡ���׵ķ���
        /// </summary>
        /// <returns></returns>
        bool Execute(out Transaction transaction, ServiceQueueLogic serviceLogic, ICollection<ServiceQueue> queues);
    }
}