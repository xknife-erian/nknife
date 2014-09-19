using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class SetPropertyDesignerCommand<T> : Command
    {
        private SnipPageDesigner _designer;
        public SnipPageDesigner Designer
        {
            get { return _designer; }
        }
        private T _oldValue;
        public T OldValue
        {
            get { return _oldValue; }
        }
        private T _newValue;
        public T NewValue
        {
            get { return _newValue; }
        }
        private SetPropertyCore<T> _setCoreMethod;
        public SetPropertyCore<T> SetCoreMethod
        {
            get { return _setCoreMethod; }
        }
        private PartAction _action;
        public PartAction Action
        {
            get { return _action; }
        }

        public SetPropertyDesignerCommand(SnipPageDesigner designer,T oldValue,T newValue,SetPropertyCore<T> setCoreMethod,PartAction action)
        {
            this._designer = designer;
            this._oldValue = oldValue;
            this._newValue = newValue;
            this._setCoreMethod = setCoreMethod;
            this._action = action;
        }

        public override void Execute()
        {
            SetCoreMethod(NewValue);
            switch (Action)
            {
                case PartAction.Invalidate:
                    this.Designer.Invalidate();
                    break;
                case PartAction.Relayout:
                    this.Designer.LayoutParts();
                    break;
            }

            base.Execute();
        }

        public override void Unexecute()
        {
            SetCoreMethod(OldValue);
            switch (Action)
            {
                case PartAction.Invalidate:
                    this.Designer.Invalidate();
                    break;
                case PartAction.Relayout:
                    this.Designer.LayoutParts();
                    break;
            }

            base.Unexecute();
        }
    }
}
