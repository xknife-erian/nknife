using System.Collections.Generic;

namespace SocketKnife.Interfaces
{
    public interface ISocketPolicy : ICollection<IFilter>
    {
        void AddAfter(IFilter filter, IFilter newfilter);
        void AddBefore(IFilter filter, IFilter newfilter);
        void AddFirst(IFilter filter);
        void AddLast(IFilter filter);
        void RemoveFirst();
        void RemoveLast();
    }
}