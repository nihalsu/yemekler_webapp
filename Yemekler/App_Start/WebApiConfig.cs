using System.Web.Http.Cors;
using System.Web.Http;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // CORS ayarları
        var cors = new EnableCorsAttribute("*", "*", "*");
        config.EnableCors(cors);

        // Attribute routing
        config.MapHttpAttributeRoutes();

        // Varsayılan Web API rotası
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
    }
}
