using System.Web.Http;
using NKnife.App.Cute.API.Controllers;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (AttributeRoutingHttp), "Start")]

namespace NKnife.App.Cute.API.Controllers
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