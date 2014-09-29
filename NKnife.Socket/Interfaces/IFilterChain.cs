using System.Collections.Generic;

namespace SocketKnife.Interfaces
{
    public interface IFilterChain : IDictionary<string, IFilter>
    {
        IFilter AddLast(string key, IFilter filter);
        IFilter AddFirst(string key, IFilter filter);
        IFilter Insert(int index, string key, IFilter filter);
    }
}