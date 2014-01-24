using System;

namespace NKnife.Configuring.Interfaces
{
    /// <summary>
    /// 这是一个描述CoderSetting（程序员配置）的接口。
    /// </summary>
    public interface ICoderSetting : IComparable
    {
        /// <summary>
        /// 当所有Option加载时的顺序，越小越先加载
        /// </summary>
        /// <value>The order.</value>
        int Order { get; set; }

        /// <summary>
        /// 当选项载入(Load)完成后发生的事件
        /// </summary>
        event CoderSettingLoadedEventHandler CoderSettingLoadedEvent;
        /// <summary>
        /// 当选项改变后发生的事件
        /// </summary>
        event CoderSettingChangedEventHandler CoderSettingChangedEvent;
        /// <summary>
        /// 当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性)
        /// </summary>
        event CoderSettingChangingEventHandler CoderSettingChangingEvent;
    }

}