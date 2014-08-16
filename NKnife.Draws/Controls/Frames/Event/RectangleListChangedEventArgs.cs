using System;
using System.Drawing;

namespace NKnife.Draws.Controls.Frames.Event
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