using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>�����������׷����Ľӿ�
    /// </summary>
    public interface IModifyFunc<T>
    {
        /// <summary>Ϊ�������׶���׼��������
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>ִ�е������׶�������
        /// </summary>
        /// <param name="queues"> </param>
        /// <returns></returns>
        bool Execute(ICollection<ServiceQueue> queues);
    }
}