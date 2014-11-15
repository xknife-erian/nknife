using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>������߼���һ���߼��л�����������:<see cref="IServiceQueue"/>��
    /// </summary>
    public interface IServiceLogic : IList<IServiceQueue>, IXmlSerializable, IEquatable<IServiceLogic>
    {
        /// <summary>���ԴӶ������з���һ������
        /// </summary>
        /// <param name="transaction">���:һ���ŶӵĽ���</param>
        /// <returns>����п��õĽ��ף������档��֮�����ط�</returns>
        bool TryAssign(out ITransaction transaction);

        /// <summary>�ж϶������Ƿ񼤻(���׳Ƶı��ö��еļ�������)
        /// </summary>
        bool IsActive();
    }
}