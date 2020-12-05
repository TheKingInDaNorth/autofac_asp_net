using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using EasyBlog.Data;
using EasyBlog.Web.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EasyBlog.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //IExtensibilityManager extensibilityManager = new ExtensibilityManager();

            //if (Application["ModuleEvents"] == null)
            //    Application["ModuleEvents"] = extensibilityManager.GetModuleEvents();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .InstancePerRequest()
                .PropertiesAutowired();
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            // builder.RegisterType<ExtensibilityManager>().As<IExtensibilityManager>().SingleInstance();

            //builder.RegisterModule<RepositoryRegistrationModule>();

            IConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");

            ConfigurationModule module = new ConfigurationModule(config.Build());
            builder.RegisterModule(module);

            builder.RegisterFilterProvider();
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            builder.RegisterType<LogMvcActionAttribute>().PropertiesAutowired();
            builder.RegisterType<LogWebApiAction>().PropertiesAutowired();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = 
                new AutofacWebApiDependencyResolver(container);

            IExtensibilityManager extensibilityManager = container.Resolve<IExtensibilityManager>();

            extensibilityManager.GetModuleEvents();

            GlobalFilters.Filters.Add(container.Resolve<LogMvcActionAttribute>());
            GlobalConfiguration.Configuration.Filters.Add(container.Resolve<LogWebApiAction>());
        }
    }
}
