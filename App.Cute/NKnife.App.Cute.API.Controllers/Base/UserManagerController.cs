using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using AttributeRouting.Web.Http;
using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel.IoC;

namespace Didaku.Engine.Timeaxis.API.Controllers.Base
{
    public class UserManagerController : ApiController
    {
        [POST("User/List")]
        public JqGridResult List()
        {
            var json = new JqGridResult();
            var users = Core.Singleton<UserPool>().Values;
            json.Data = users;
            json.records = users.Count();
            json.total = json.records / 10;
            return json;
        }
    }
}
