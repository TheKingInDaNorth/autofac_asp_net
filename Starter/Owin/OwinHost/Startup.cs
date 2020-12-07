using Autofac;
using Autofac.Integration.WebApi;
using Lib;
using Lib.Abstractions;
using Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace OwinHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<LoggerMiddleware>().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            IContainer container = builder.Build();

            appBuilder
                .UseAutofacWebApi(config)
                .UseAutofacMiddleware(container)
                .UseWebApi(config);
            //appBuilder
            //    .UseAutofacWebApi(config)
            //    .UseMiddlewareFromContainer<LoggerMiddleware>()
            //    .UseWebApi(config);
        }
    }
}
