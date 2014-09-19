using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    sealed public class EditorAttribute : AutoLayoutPanelAttribute
    {
        public EditorAttribute(int groupBoxIndex, int mainControlIndex, string labelLeft)
            : base(groupBoxIndex, mainControlIndex, labelLeft)
        {
        }
    }
}