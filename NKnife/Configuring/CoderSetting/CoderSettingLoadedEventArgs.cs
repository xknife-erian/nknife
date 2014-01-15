using System;
using Gean.Configuring.Interfaces;

namespace NKnife.Configuring.CoderSetting
{
    /// <summary>
    /// CoderSetting（程序员配置）值发生改变时的包含事件数据的类
    /// </summary>
    public class CoderSettingLoadedEventArgs : EventArgs
    {
        public ICoderSetting CoderSettingObject { get; private set; }

        public object CoderSettingValue { get; private set; }

        public CoderSettingLoadedEventArgs(ICoderSetting optionObject, object source)
        {
            this.CoderSettingObject = optionObject;
            this.CoderSettingValue = source;
        }
    }
}