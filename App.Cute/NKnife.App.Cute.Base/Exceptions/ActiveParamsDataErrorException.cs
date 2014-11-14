using System;

namespace Didaku.Engine.Timeaxis.Base.Exceptions
{
    public class ActiveParamsDataErrorException : TimeaxisException
    {
        public ActiveParamsDataErrorException(string msg)
            : base(msg)
        {
        }
    }
}