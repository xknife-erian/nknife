using System;

namespace Didaku.Engine.Timeaxis.Base.Attributes
{
    /// <summary>һ�������������͵�ʵ�����͵Ķ�������
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