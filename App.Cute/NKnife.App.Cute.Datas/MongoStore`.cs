using NKnife.MongoDb;

namespace NKnife.App.Cute.Datas
{
    public class MongoStore<T, TId> : AbstractMongoDbStore<T, TId>
    {
        public MongoStore(string connection, string database, string collection)
            : base(connection, database, collection)
        {
        }
    }
}
