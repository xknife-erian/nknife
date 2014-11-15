using System.Linq;
using System.Web.Http;
using AttributeRouting.Web.Http;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Kernel.IoC;

namespace NKnife.App.Cute.API.Controllers.Base
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