using System;

namespace NKnife.App.Cute.Base.Attributes
{
    /// <summary>一个描述交易类型的实现类型的定制特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class TransactionImplAttribute : Attribute
    {
        public TransactionImplAttribute(int kind, string description)
        {
            Kind = kind;
            Description = description;
        }

        public int Kind { get; private set; }
        public string Description { get; private set; }
    }
}