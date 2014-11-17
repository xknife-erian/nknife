namespace NKnife.App.Cute.Datas.Base
{
    /// <summary>对应NLog的日志体对象的一个简单对象。
    /// </summary>
    public class LogInfo
    {
        public LogInfo()
        {
            Stack = string.Empty;
        }

        public long Id { get; set; }
        public string Info { get; set; }
        public string Src { get; set; }
        public string Stack { get; set; }
        public string Level { get; set; }


    }
}
