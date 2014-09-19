using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class UnSelectPartCommand : PartCommand
    {
        public UnSelectPartCommand(SnipPagePart part)
            :base(part)
        {
        }
        public override void Execute()
        {
            Part.Designer.SelectedParts.RemoveCore(Part);

            base.Execute();
        }

        public override void Unexecute()
        {
            Part.Designer.SelectedParts.AddCore(Part);

            base.Unexecute();
        }
    }
}
