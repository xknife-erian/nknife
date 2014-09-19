using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.Property|AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    sealed public class PropertyPadAttribute : AutoLayoutPanelAttribute
    {
        public PropertyPadAttribute(int groupBoxIndex, int mainControlIndex, string labelLeft)
            : base(groupBoxIndex, mainControlIndex, labelLeft)
        {
        }
    }
}