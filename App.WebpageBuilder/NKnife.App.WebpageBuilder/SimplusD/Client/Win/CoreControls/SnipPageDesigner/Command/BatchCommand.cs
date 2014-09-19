using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    internal class BatchCommand : Command
    {
        public BatchCommand(IList<Command> commands)
        {
            this._childCommands = commands;
        }

        private IList<Command> _childCommands;
        public IList<Command> ChildCommands
        {
            get
            {
                return _childCommands;
            }
        }

        public override void Execute()
        {
            foreach (Command cmd in ChildCommands)
            {
                cmd.Execute();
            }
        }

        public override void Unexecute()
        {
            for (int i = ChildCommands.Count - 1; i >= 0; i--)
            {
                ChildCommands[i].Unexecute();
            }
        }
    }
}
