using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

public static class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        var settings = new FriendlyUrlSettings();
        settings.AutoRedirectMode = RedirectMode.Permanent;
        routes.EnableFriendlyUrls(settings);

        // Web Forms i�in rotalar
        routes.Ignore("{resource}.axd/{*pathInfo}");

        // API rotalar�n� Web Forms y�nlendirmesinden hari� tut
        routes.Ignore("api/{*pathInfo}");

        // Web Forms rotalar�n� buraya ekleyin (e�er gerekiyorsa)
        routes.MapPageRoute(
            "DefaultRoute",
            "",
            "~/Default.aspx"
        );
    }
}
