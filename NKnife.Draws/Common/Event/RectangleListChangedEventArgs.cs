using System;
using System.Drawing;

namespace NKnife.Draws.Common.Event
{
    public class RectangleListChangedEventArgs : EventArgs
    {
        public RectangleListChangedEventArgs(RectangleChangedMode changedMode, RectangleList rectangleList, RectangleF activedRectangle)
        {
            RectangleList = rectangleList;
            ActivedRectangle = activedRectangle;
            ChangedMode = changedMode;
        }

        public RectangleChangedMode ChangedMode { get; private set; }

        public RectangleList RectangleList { get; private set; }
        public RectangleF ActivedRectangle { get; private set; }

        public int IndexOfList
        {
            get { return RectangleList.IndexOf(ActivedRectangle); }
        }

        public enum RectangleChangedMode
        {
            Created,Updated,Removed
        }
    }


}