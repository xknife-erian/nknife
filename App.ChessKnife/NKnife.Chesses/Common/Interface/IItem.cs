using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public interface IItem
    {
        string ItemType { get; }
        string Value { get; }
    }
}
