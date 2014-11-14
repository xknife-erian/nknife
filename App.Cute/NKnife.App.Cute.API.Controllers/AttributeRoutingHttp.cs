using System.Web.Http;
using Didaku.Engine.Timeaxis.API.Controllers;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (AttributeRoutingHttp), "Start")]

namespace Didaku.Engine.Timeaxis.API.Controllers
{
    public static class AttributeRoutingHttp
    {
        public static void RegisterRoutes(HttpRouteCollection routes)
        {
            //routes.MapHttpAttributeRoutes();
        }

        public static void Start()
        {
            RegisterRoutes(GlobalConfiguration.Configuration.Routes);
        }
    }
}