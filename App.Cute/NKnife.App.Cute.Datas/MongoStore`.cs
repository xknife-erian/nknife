using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.MongoDb;

namespace Didaku.Engine.Timeaxis.Data
{
    public class MongoStore<T, TId> : AbstractMongoDbStore<T, TId>
    {
        public MongoStore(string connection, string database, string collection)
            : base(connection, database, collection)
        {
        }
    }
}
