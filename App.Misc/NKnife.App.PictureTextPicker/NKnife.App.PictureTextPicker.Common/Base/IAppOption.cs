using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public interface IAppOption
    {
        void SetOption<T>(string type, T value);
        T GetOption<T>(string type);
    }
}
