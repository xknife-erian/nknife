using System.Collections.Generic;

namespace Gean
{
    public interface ITree
    {
        object Parent { get; set; }
        bool HasChildren { get; }
        IList<IItem> Items { get; set; }
    }
}
