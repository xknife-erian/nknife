using System.Collections.Generic;
using Gean.Configuring.Interfaces;

namespace Gean.Configuring.Interfaces
{
    public interface IOptionItemManager
    {
        IEnumerable<IOptionControl> Initialize(IEnumerable<IOptionListItem> optionItems);
    }
}
