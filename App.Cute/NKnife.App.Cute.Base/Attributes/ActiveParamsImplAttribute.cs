using System;

namespace NKnife.App.Cute.Base.Attributes
{
    /// <summary>一个描述活动请求参数类型的实现类型的定制特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ActiveParamsImplAttribute : Attribute
    {
        public ActiveParamsImplAttribute(int kind, string description)
        {
            Kind = kind;
            Description = description;
        }

        public int Kind { get; private set; }
        public string Description { get; private set; }
    }
}
