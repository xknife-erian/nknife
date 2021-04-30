using System;
using System.Drawing;

namespace NKnife.Win.Forms.EventParams
{
    public class RectangleListChangedEventArgs : EventArgs
    {
        public RectangleListChangedEventArgs(RectangleChangedMode changedMode, RectangleF activedRectangle)
        {
            ActivedRectangle = activedRectangle;
            ChangedMode = changedMode;
        }

        public RectangleChangedMode ChangedMode { get; private set; }
        public RectangleF ActivedRectangle { get; private set; }

        public enum RectangleChangedMode
        {
            Created,Updated,Removed
        }
    }


}