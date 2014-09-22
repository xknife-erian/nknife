using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class RemovePartCommand : PartCommand
    {
        private IPartContainer _parent;
        public IPartContainer Parent
        {
            get { return _parent; }
        }
        int _position;

        public RemovePartCommand(SnipPagePart part)
            :base(part)
        {
            this._parent = Part.ParentContainer;
            _position = Parent.ChildParts.IndexOf(Part);
        }

        public override void Execute()
        {
            Parent.ChildParts.RemoveCore(Part);

            base.Execute();
        }

        public override void Unexecute()
        {
            Parent.ChildParts.InsertCore(_position, Part);

            base.Unexecute();
        }
    }
}
