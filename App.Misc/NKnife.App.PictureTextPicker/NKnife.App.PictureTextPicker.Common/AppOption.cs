using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;

namespace NKnife.App.PictureTextPicker.Common
{
    public class AppOption : BaseAppOption
    {
        public AppOption()
        {
            SetOption("ThumbWidth",180);
            SetOption("ThumbHeight",100);
            SetOption("PictureFileType","jpg");
            SetOption("FixThumbSize",false);
        }
    }
}
