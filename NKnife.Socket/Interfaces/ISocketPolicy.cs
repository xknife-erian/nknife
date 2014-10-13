using System.Collections.Generic;
using SocketKnife.Common;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface ISocketPolicy : ICollection<KnifeSocketFilter>
    {
        void AddAfter(KnifeSocketFilter filter, KnifeSocketFilter newfilter);
        void AddBefore(KnifeSocketFilter filter, KnifeSocketFilter newfilter);
        void AddFirst(KnifeSocketFilter filter);
        void AddLast(KnifeSocketFilter filter);
        void RemoveFirst();
        void RemoveLast();
    }
}