using System.Collections.Generic;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>��ϵͳ�����еĹؼ������Ĺ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"> </typeparam>
    public interface IFeaturePool<TKey, T> : IDictionary<TKey, T>
    {
    }
}