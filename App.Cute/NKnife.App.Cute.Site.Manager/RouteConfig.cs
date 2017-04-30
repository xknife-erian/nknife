using System.Web.Routing;

namespace NKnife.App.Cute.Site.Manager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

//            routes.MapPageRoute(
//                routeName: "Default",
//                routeUrl: "{controller}/{action}/{id}",
//                defaults: ""
//            );
        }
    }
}