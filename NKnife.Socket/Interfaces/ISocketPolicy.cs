using System.Collections.Generic;
using SocketKnife.Common;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface ISocketPolicy : ICollection<KnifeSocketServerFilter>
    {
        void AddAfter(KnifeSocketServerFilter filter, KnifeSocketServerFilter newfilter);
        void AddBefore(KnifeSocketServerFilter filter, KnifeSocketServerFilter newfilter);
        void AddFirst(KnifeSocketServerFilter filter);
        void AddLast(KnifeSocketServerFilter filter);
        void RemoveFirst();
        void RemoveLast();
    }
}