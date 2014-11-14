using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Data.Stores
{
    class ActivityStore : MongoStore<ActivityImplAttribute, string>
    {
        public ActivityStore(string dbConn)
            : base(dbConn, "Configurations".ToLower(), "ActivityCollection".ToLower())
        {
        }
    }
}
