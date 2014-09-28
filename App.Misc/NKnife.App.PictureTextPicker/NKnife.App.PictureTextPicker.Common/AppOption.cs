using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;

namespace NKnife.App.PictureTextPicker.Common
{
    public class AppOption : BaseAppOption
    {
        public AppOption()
        {

            SetOption("ThumbNailDirectory", Path.Combine(Path.GetDirectoryName(typeof(AppOption).Assembly.Location), "thumbnail"));

            SetOption("PictureDirectory","");
            SetOption("ThumbWidth",180);
            SetOption("ThumbHeight",100);
            SetOption("PictureFileType","*.jpg");
            SetOption("FixThumbSize",false);
        }
    }
}
