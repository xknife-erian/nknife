using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AttributeRouting.Web.Http;
using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel.IoC;

namespace Didaku.Engine.Timeaxis.API.Controllers.Base
{
    public class ActivityManagerController : ApiController
    {
        [POST("Activity/List")]
        public JqGridResult List()
        {
            var json = new JqGridResult();
            var attrs = Core.Singleton<ActivityPool>().Attributes;
            json.Data = attrs;
            json.records = attrs.Count();
            json.total = json.records / 10;
            return json;
        }
    }
}