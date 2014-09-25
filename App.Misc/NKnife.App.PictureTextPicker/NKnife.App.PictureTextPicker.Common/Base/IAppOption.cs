using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public interface IAppOption
    {
        void SetOption<T>(string key, T value);
        T GetOption<T>(string key) where T : class, new();
        int GetOption(string key, int defaultValue = 0);
        string GetOption(string key, string defaultValue = "");
        bool GetOption(string key, bool defaultValue = false);
    }
}
