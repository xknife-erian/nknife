using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Didaku.Engine.Timeaxis.Data.Base;

namespace Didaku.Engine.Timeaxis.Data.Stores
{
    class LogEventInfoStore : MongoStore<LogInfo, string>
    {
        public LogEventInfoStore(string connection)
            : base(connection, "LogInfos".ToLower(), "LogCollection".ToLower())
        {
        }
    }
}
