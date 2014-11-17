using System.Linq;
using System.Web.Http;
using AttributeRouting.Web.Http;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Kernel.IoC;

namespace NKnife.App.Cute.API.Controllers.Base
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
