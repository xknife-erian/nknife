using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Attributes;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>本系统领域中的关键特征的管理池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"> </typeparam>
    public interface IFeaturePool<TKey, T> : IDictionary<TKey, T>
    {
    }
}