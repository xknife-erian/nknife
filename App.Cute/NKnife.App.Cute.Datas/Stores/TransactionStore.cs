using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Datas.Stores
{
    class TransactionStore : MongoStore<ITransaction, string>
    {
        public TransactionStore(string dbConn) :
            base(dbConn, "TransactionStore".ToLower(), "TransactionCollection".ToLower())
        {
        }
    }
}