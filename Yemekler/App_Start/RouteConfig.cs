using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

public static class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        var settings = new FriendlyUrlSettings();
        settings.AutoRedirectMode = RedirectMode.Permanent;
        routes.EnableFriendlyUrls(settings);

        // Web Forms için rotalar
        routes.Ignore("{resource}.axd/{*pathInfo}");

        // API rotalarýný Web Forms yönlendirmesinden hariç tut
        routes.Ignore("api/{*pathInfo}");

        // Web Forms rotalarýný buraya ekleyin (eðer gerekiyorsa)
        routes.MapPageRoute(
            "DefaultRoute",
            "",
            "~/Default.aspx"
        );
    }
}
