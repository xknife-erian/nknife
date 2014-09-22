using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class ClearSelectCommand : Command
    {
        private SnipPageDesigner _designer;
        public SnipPageDesigner Designer
        {
            get { return _designer; }
        }

        private IList<SnipPagePart> _selectedParts;
        public IList<SnipPagePart> SelectedParts
        {
            get { return _selectedParts; }
        }

        public ClearSelectCommand(SnipPageDesigner designer)
        {
            this._designer = designer;
            this._selectedParts = designer.SelectedParts.ToArray();
        }

        public override void Execute()
        {
            this.Designer.SelectedParts.ClearCore();

            base.Execute();
        }

        public override void Unexecute()
        {
            foreach (SnipPagePart part in SelectedParts)
            {
                this.Designer.SelectedParts.AddCore(part);
            }

            base.Unexecute();
        }
    }
}
