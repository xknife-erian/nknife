using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class SetPropertyPartCommand<T> : PartCommand
    {
        protected T _oldValue;
        public T OldValue
        {
            get { return _oldValue; }
        }

        protected T _newValue;
        public T NewValue
        {
            get { return _newValue; }
        }

        internal SetPropertyCore<T> _setCoreMethod;
        internal SetPropertyCore<T> SetCoreMethod
        {
            get { return _setCoreMethod; }
        }

        public SetPropertyPartCommand(SnipPagePart part,T oldValue, T newValue, SetPropertyCore<T> setCoreMethod)
            :this(part,oldValue,newValue,setCoreMethod,PartAction.None)
        {
        }

        public SetPropertyPartCommand(SnipPagePart part, T oldValue, T newValue, SetPropertyCore<T> setCoreMethod,PartAction action)
            : base(part,action)
        {
            _oldValue = oldValue;
            _newValue = newValue;
            this._setCoreMethod = setCoreMethod;
        }

        public override void Execute()
        {
            SetCoreMethod(NewValue);

            base.Execute();
        }

        public override void Unexecute()
        {
            SetCoreMethod(OldValue);

            base.Unexecute();
        }
    }
}
