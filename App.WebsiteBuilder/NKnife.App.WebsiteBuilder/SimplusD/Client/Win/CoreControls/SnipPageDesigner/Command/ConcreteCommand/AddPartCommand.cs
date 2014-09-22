using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class AddPartCommand : PartCommand
    {
        private IPartContainer _parent;
        public IPartContainer Parent
        {
            get { return _parent; }
        }

        public AddPartCommand(IPartContainer parent, SnipPagePart part)
            :base(part)
        {
            this._parent = parent;
        }

        public override void Execute()
        {
            Parent.ChildParts.AddCore(Part);

            base.Execute();
        }

        public override void Unexecute()
        {
            Parent.ChildParts.RemoveCore(Part);

            base.Unexecute();
        }
    }
}
