using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed public class SitePropertyAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string positionalString;

        // This is a positional argument
        public SitePropertyAttribute(string positionalString)
        {
            this.positionalString = positionalString;

            // TODO: Implement code here
            throw new NotImplementedException();
        }

        public string PositionalString { get; private set; }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
