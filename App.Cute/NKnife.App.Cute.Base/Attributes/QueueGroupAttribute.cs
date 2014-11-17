using System;

namespace NKnife.App.Cute.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class QueueGroupAttribute : Attribute
    {
        public QueueGroupAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
