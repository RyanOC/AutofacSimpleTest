using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Autofac.Integration.Mvc;

namespace DI.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerBuilder builder = new ContainerBuilder();


            string[] assemblyScanerPattern = new[] { @"DI.*.dll" };
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.AddRange(
                Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "DI.*.dll", SearchOption.AllDirectories)
                    .Where(filename => assemblyScanerPattern.Any(pattern => Regex.IsMatch(filename, pattern)))
                    .Select(Assembly.LoadFrom)
            );
            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces();
            }


            //builder.RegisterType<Framework.CommerceEngine>().As<Core.Abstractions.ICommerceEngine>();
            //builder.RegisterType<Framework.CustomerProcessor>().As<Core.Abstractions.ICustomerProcessor>();



            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Controller"));

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);         
        }
    }
}
