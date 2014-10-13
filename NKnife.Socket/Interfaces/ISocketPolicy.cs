using System.Collections.Generic;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface ISocketPolicy : ICollection<FilterBase>
    {
        void AddAfter(FilterBase filter, FilterBase newfilter);
        void AddBefore(FilterBase filter, FilterBase newfilter);
        void AddFirst(FilterBase filter);
        void AddLast(FilterBase filter);
        void RemoveFirst();
        void RemoveLast();
    }
}