using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public class PicutureEventArgs : EventArgs
    {
        public string PictureName { get; set; }

        public PicutureEventArgs(string pictureName)
        {
            PictureName = pictureName;
        }
    }
}
