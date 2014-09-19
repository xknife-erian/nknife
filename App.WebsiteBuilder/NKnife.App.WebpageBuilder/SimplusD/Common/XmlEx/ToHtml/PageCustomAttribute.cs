using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed internal class PageCustomAttribute : Attribute
    {
        public PageCustomAttribute(bool isPageAttribute)
        {
            this.IsPageAttribute = isPageAttribute;
        }

        public bool IsPageAttribute { get; set; }
    }
}
