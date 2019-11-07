using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Store.ViewModel;

namespace Store
{
    public static class WebApiConfig2
    {
        public static void Register(HttpConfiguration config)
        {


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ProductViewModel>("ProductViewModels");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
