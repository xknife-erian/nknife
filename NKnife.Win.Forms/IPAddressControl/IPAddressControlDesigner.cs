using System.Collections;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace NKnife.Win.Forms.IPAddressControl
{
    internal class IPAddressControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                var control = (IpAddressControl) Control;

                if (control.AutoHeight)
                    return SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
        }

        public override IList SnapLines
        {
            get
            {
                var control = (IpAddressControl) Control;
                var snapLines = base.SnapLines;
                snapLines?.Add(new SnapLine(SnapLineType.Baseline, control.Baseline));
                return snapLines;
            }
        }
    }
}