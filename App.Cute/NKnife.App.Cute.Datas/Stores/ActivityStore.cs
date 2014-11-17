using NKnife.App.Cute.Base.Attributes;

namespace NKnife.App.Cute.Datas.Stores
{
    class ActivityStore : MongoStore<ActivityImplAttribute, string>
    {
        public ActivityStore(string dbConn)
            : base(dbConn, "Configurations".ToLower(), "ActivityCollection".ToLower())
        {
        }
    }
}
