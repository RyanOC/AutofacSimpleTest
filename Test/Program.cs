using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;
using DI.Framework;

namespace Test
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
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
           
            builder.RegisterType<CommerceEngine>(); // in webapi these are done by the DependencyResolver in Global.asax

            _container = builder.Build();

            var commerce = _container.Resolve<CommerceEngine>(); // in webapi this is injected in the contoller constructor via the resolver
            
            commerce.ProcessOrder();

            Console.Read();
        }
    }
}
