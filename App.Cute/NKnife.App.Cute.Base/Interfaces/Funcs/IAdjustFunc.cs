using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>����������������з����Ľӿ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAdjustFunc<T>
    {
        /// <summary>Ϊ������������еĶ���׼��������
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>ִ�е�����������еĶ���
        /// </summary>
        /// <returns></returns>
        bool Execute(IDictionary<string, ServiceQueueLogic> logicMap);
    }
}