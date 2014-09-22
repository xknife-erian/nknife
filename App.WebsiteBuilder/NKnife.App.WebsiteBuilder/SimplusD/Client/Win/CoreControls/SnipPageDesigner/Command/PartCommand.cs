using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    abstract internal class PartCommand : Command
    {
        protected SnipPagePart _part;
        public SnipPagePart Part
        {
            get { return _part; }
        }

        internal PartAction _action;
        internal PartAction Action
        {
            get { return _action; }
        }

        internal PartCommand(SnipPagePart part)
            :this(part,PartAction.None)
        {
        }
        internal PartCommand(SnipPagePart part,PartAction action)
        {
            this._action = action;
            this._part = part;
        }

        public override void Execute()
        {
            switch (Action)
            {
                case PartAction.Invalidate:
                    Part.Invalidate();
                    break;

                case PartAction.Relayout:
                    if (Part.ParentContainer != null)
                    {
                        Part.ParentContainer.LayoutParts();
                    }
                    break;
            }

            base.Execute();
        }

        public override void Unexecute()
        {
            switch (Action)
            {
                case PartAction.Invalidate:
                    Part.Invalidate();
                    break;

                case PartAction.Relayout:
                    if (Part.ParentContainer != null)
                    {
                        Part.ParentContainer.LayoutParts();
                    }
                    break;
            }

            base.Unexecute();
        }
    }
}
