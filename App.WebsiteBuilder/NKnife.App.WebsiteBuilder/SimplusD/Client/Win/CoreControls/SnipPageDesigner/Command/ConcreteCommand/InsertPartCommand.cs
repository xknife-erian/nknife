using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class InsertPartCommand : PartCommand
    {
        private int _index;
        public int Index
        {
            get { return _index; }
        }

        private IPartContainer _parent;
        public IPartContainer Parent
        {
            get { return _parent; }
        }

        public InsertPartCommand(SnipPagePart part,IPartContainer parent, int index)
            :base(part)
        {
            this._parent = parent;
            this._index = index;
        }

        public override void Execute()
        {
            Parent.ChildParts.InsertCore(Index, Part);

            base.Execute();
        }

        public override void Unexecute()
        {
            Parent.ChildParts.RemoveCore(Part);

            base.Unexecute();
        }
    }
}
