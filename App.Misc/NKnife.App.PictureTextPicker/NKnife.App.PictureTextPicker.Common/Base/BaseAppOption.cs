using System;
using System.Collections.Concurrent;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public class BaseAppOption : IAppOption
    {
        private ConcurrentDictionary<string,object> _OptionMap = new ConcurrentDictionary<string, object>();
        public BaseAppOption()
        {
            
        }

        public void SetOption<T>(string key, T value)
        {
            _OptionMap.TryAdd(key,value);
        }

        public T GetOption<T>(string key) where T : class, new()
        {
            object value;
            if (_OptionMap.TryGetValue(key, out value))
            {
                return value as T;
            }
            return default(T);
        }

        public int GetOption(string key, int defaultValue = 0)
        {
            object value;
            if (_OptionMap.TryGetValue(key, out value))
            {
                return Convert.ToInt32(value);
            }
            return defaultValue;
        }

        public string GetOption(string key, string defaultValue = "")
        {
            object value;
            if (_OptionMap.TryGetValue(key, out value))
            {
                return value.ToString();
            }
            return defaultValue;
        }

        public bool GetOption(string key, bool defaultValue = false)
        {
            object value;
            if (_OptionMap.TryGetValue(key, out value))
            {
                return Convert.ToBoolean(value);
            }
            return defaultValue;
        }
    }
}
