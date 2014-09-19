using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class SoftOptionClassAttribute : Attribute
    {
        private string _panelName;
        public string PanelName
        {
            get { return _panelName; }
        }

        public SoftOptionClassAttribute(string panelName)
        {
            _panelName = panelName;
        }
    }
}
