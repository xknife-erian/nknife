using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Data.Stores
{
    class TransactionStore : MongoStore<ITransaction, string>
    {
        public TransactionStore(string dbConn) :
            base(dbConn, "TransactionStore".ToLower(), "TransactionCollection".ToLower())
        {
        }
    }
}