using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Attributes;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>��ϵͳ�����еĹؼ������Ĺ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"> </typeparam>
    public interface IFeaturePool<TKey, T> : IDictionary<TKey, T>
    {
    }
}