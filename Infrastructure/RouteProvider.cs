using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Misc.FreshAddressIntegration.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority => -1; // run last to allow manipulation of core routes

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            var existingNewsletterRoute = routeBuilder.Routes.FirstOrDefault(x => ((Route)x).Name == "SubscribeNewsletter");

            routeBuilder.Routes.Remove(existingNewsletterRoute);

            routeBuilder.MapLocalizedRoute("SubscribeNewsletter", "subscribenewsletter",
                new { controller = "CustomNewsletter", action = "SubscribeNewsletter" });
        }
    }
}
