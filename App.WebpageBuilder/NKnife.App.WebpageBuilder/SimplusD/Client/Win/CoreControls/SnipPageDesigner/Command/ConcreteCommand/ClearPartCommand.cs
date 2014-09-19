using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class ClearPartCommand : Command
    {
        private SnipPageDesigner _designer;
        public SnipPageDesigner Designer
        {
            get { return _designer; }
        }

        private SnipPagePart[] _partLists;
        public SnipPagePart[] PartLists
        {
            get { return _partLists; }
        }

        public ClearPartCommand(SnipPageDesigner designer)
        {
            _designer = designer;

            _partLists = _designer.ChildParts.ToArray();
        }

        public override void Execute()
        {
            _designer.ChildParts.ClearCore();

            base.Execute();
        }

        public override void Unexecute()
        {
            foreach (SnipPagePart part in PartLists)
            {
                _designer.ChildParts.AddCore(part);
            }

            base.Unexecute();
        }
    }
}
