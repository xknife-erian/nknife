using System;

namespace Gean.Configuring.CoderSetting
{
    /// <summary>
    /// 选项值发生改变时的包含事件数据的类
    /// </summary>
    public class CoderSettingChangeEventArgs : EventArgs
    {
        public string CoderSettingValueName { get; private set; }

        public Object CoderSettingValue { get; private set; }

        public CoderSettingChangeEventArgs(string key, object value)
        {
            this.CoderSettingValueName = key;
            this.CoderSettingValue = value;
        }
    }
}