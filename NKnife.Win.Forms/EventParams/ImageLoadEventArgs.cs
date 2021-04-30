using System.Drawing;
using NKnife.Events;

namespace NKnife.Win.Forms.EventParams
{
    public class ImageLoadEventArgs : ChangedEventArgs<Image>
    {
        public ImageLoadEventArgs(Image oldItem, Image newItem)
            : base(oldItem, newItem)
        {
        }
    }
}
