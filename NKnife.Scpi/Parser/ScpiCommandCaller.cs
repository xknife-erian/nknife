using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    public class ScpiCommandCaller
    {
        readonly SCPIParser.IScpiCommandHandler _Handler;
        readonly string[] _Args;

        public ScpiCommandCaller(SCPIParser.IScpiCommandHandler handler, string[] args)
        {
            this._Handler = handler;
            this._Args = args;
        }

        public string Execute()
        {
            return _Handler.handle(_Args);
        }
    }
}
