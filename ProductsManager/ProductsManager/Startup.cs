using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(ProductsManager.Startup))]

namespace ProductsManager
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage("/");

            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}