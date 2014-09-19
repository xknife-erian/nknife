using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseCommand
    {
        abstract public void Execute();
        abstract public void UnExecute();

        protected string CommandInfo { get; set; }

        protected DesignPanel TDPanel { get; set; }

        virtual public string  GetCommandInfo()
        {
            return CommandInfo;
        }
    }
}
