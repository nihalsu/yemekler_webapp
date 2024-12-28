using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Yemekler
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // Web API yapılandırması
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Web Forms rotalarını kaydet
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
