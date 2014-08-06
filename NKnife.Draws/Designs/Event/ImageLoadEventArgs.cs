using System.Drawing;
using NKnife.Events;

namespace NKnife.Draws.Designs.Event
{
    public class ImageLoadEventArgs : ChangedEventArgs<Image>
    {
        public ImageLoadEventArgs(Image oldItem, Image newItem)
            : base(oldItem, newItem)
        {
        }
    }
}
