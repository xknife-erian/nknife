using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NKnife.Events;

namespace NKnife.Draws.Common.Event
{
    public class ImageLoadEventArgs : ChangedEventArgs<Image>
    {
        public ImageLoadEventArgs(Image oldItem, Image newItem)
            : base(oldItem, newItem)
        {
        }
    }
}
