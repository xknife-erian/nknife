using NKnife.App.Cute.Datas.Base;

namespace NKnife.App.Cute.Datas.Stores
{
    class LogEventInfoStore : MongoStore<LogInfo, string>
    {
        public LogEventInfoStore(string connection)
            : base(connection, "LogInfos".ToLower(), "LogCollection".ToLower())
        {
        }
    }
}
