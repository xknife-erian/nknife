using Gean.Configuring.CoderSetting;
using Gean.Configuring.Common;
using Gean.Configuring.Interfaces;

namespace Gean.Configuring.CoderSetting
{
    /// <summary>应用程序 CoderSetting（程序员配置）框架的选项节点类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CoderSetting<T> : ICoderSetting
    {
        protected CoderSetting()
        {
            
        }

        /// <summary>当所有Option加载时的顺序，越小越先加载
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; set; }

        /// <summary>Initializeses本类型的具体数据。该方法将被反射调用。
        /// </summary>
        /// <param name="source">The source.</param>
        protected virtual void Initializes(T source)
        {
            this.Load(source);
            OnCoderSettingLoaded(new CoderSettingLoadedEventArgs(this, source));
        }

        /// <summary>从指定的源(一般是一个XmlElement，也可以是任何类型)载入本类型的各个属性值
        /// </summary>
        /// <param name="source">指定的源(一般是一个XmlElement)</param>
        protected abstract void Load(T source);

        /// <summary>当选项载入(Load)完成后发生的事件
        /// </summary>
        public event CoderSettingLoadedEventHandler CoderSettingLoadedEvent;

        /// <summary>Raises the <see cref="CoderSettingLoadedEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="CoderSettingLoadedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCoderSettingLoaded(CoderSettingLoadedEventArgs e)
        {
            if (CoderSettingLoadedEvent != null)
                CoderSettingLoadedEvent(this, e);
        }

        /// <summary>当选项改变后发生的事件
        /// </summary>
        public event CoderSettingChangedEventHandler CoderSettingChangedEvent;

        /// <summary>Raises the <see cref="CoderSettingChangedEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="CoderSettingChangeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCoderSettingChanged(CoderSettingChangeEventArgs e)
        {
            if (CoderSettingChangedEvent != null)
                CoderSettingChangedEvent(this, e);
        }

        /// <summary>当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性，请注意处理)
        /// </summary>
        public event CoderSettingChangingEventHandler CoderSettingChangingEvent;

        /// <summary>Raises the <see cref="CoderSettingChangingEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="CoderSettingChangeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCoderSettingChanging(CoderSettingChangeEventArgs e)
        {
            if (CoderSettingChangingEvent != null)
                CoderSettingChangingEvent(this, e);
        }

        /// <summary>定义本类型的通用比较方法
        /// </summary>
        public int CompareTo(object obj)
        {
            return this.Order - ((ICoderSetting)obj).Order;
        }
    }
}