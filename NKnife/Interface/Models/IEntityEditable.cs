using System;

namespace NKnife.Interface.Models
{
    public interface IEntityEditable<T>
        where T : class, new()
    {
        /// <summary>是否是新建实体
        /// </summary>
        bool IsNewBuild { get; set; }

        /// <summary>获取当前的实体对象
        /// </summary>
        T Entity { get; set; }

        /// <summary>当实体发生变化时的事件
        /// </summary>
        event EventHandler EntityChangedEvent;
    }
}