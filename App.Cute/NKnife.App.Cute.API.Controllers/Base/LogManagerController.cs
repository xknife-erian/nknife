using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AttributeRouting.Web.Http;
using Didaku.Engine.Timeaxis.Data;
using Didaku.Engine.Timeaxis.Kernel;
using Didaku.Engine.Timeaxis.Kernel.IoC;
using MongoDB.Driver.Builders;
using NKnife.Database;

namespace Didaku.Engine.Timeaxis.API.Controllers.Base
{
    public class LogManagerController : ApiController
    {
        [POST("Log/List")]
        public JqGridResult List(JqGridParam param)
        {
            var logs = Core.Singleton<Datas>().Logs;

            var query = Query.Null;
            var pager = new PagerInfo
                            {
                                CurrentPage = (uint) param.page,
                                Count = (uint) param.rows
                            };
            var sort = (param.sord== Sord.asc) ? SortBy.Ascending("Id") : SortBy.Descending("Id");
            var loglist = logs.Find(query, pager, sort);
            var json = new JqGridResult {Data = loglist};
            return json;
        }
    }
}
