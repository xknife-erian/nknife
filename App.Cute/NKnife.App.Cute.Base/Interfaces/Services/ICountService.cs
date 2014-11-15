using System.Collections.Generic;
using NKnife.App.Cute.Base.Common;
using NKnife.App.Cute.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Services
{
    public interface ICountService
    {
        /// <summary>�������Ӧ�Ĺ�̨�ȴ������ļ���
        /// </summary>
        CountMap Counters { get; }

        /// <summary>�������Ӧ�����ж��еȴ������ļ���
        /// </summary>
        CountMap Queues { get; }

        /// <summary>�������Ӧ�����ж���Ԫ�صļ���
        /// </summary>
        CountMap Elements { get; }

        /// <summary>��������
        /// </summary>
        void Start(IEnumerable<IServiceQueue> queues);

        /// <summary>�رշ���
        /// </summary>
        void Close();
    }
}