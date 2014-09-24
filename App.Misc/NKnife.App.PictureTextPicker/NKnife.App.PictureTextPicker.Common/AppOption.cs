using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;

namespace NKnife.App.PictureTextPicker.Common
{
    public class AppOption : IAppOption
    {
        
        public AppOption()
        {
            
        }

        public void SetOption<T>(string type, T value)
        {
            throw new NotImplementedException();
        }

        public T GetOption<T>(string type)
        {
            throw new NotImplementedException();
        }
    }
}
