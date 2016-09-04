﻿using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoDB.Driver.Builders;
using NKnife.App.Cute.Datas;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.Databases;
using NKnife.IoC;

namespace NKnife.App.Cute.API.Controllers.Base
{
    public class LogManagerController : ApiController
    {
        [POST("Log/List")]
        public JqGridResult List(JqGridParam param)
        {
            var logs = DI.Get<DataService>().Logs;

            var query = Query.Null;
            var pager = new PagerInfo
                            {
                                CurrentPage = (uint) param.page,
                                Count = (uint) param.rows
                            };
            var sort = (param.sord== Sord.asc) ? SortBy.Ascending("Id") : SortBy.Descending("Id");
            //var loglist = logs.Find(query, pager, sort);
            //var json = new JqGridResult {Data = loglist};
            return null;
        }
    }
}
