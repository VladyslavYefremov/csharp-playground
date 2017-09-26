using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Infrastructure;
using Infrastructure.Logging;
using Microsoft.Practices.Unity;

namespace Composition
{
	public static class CompositionContainer
	{
		private static readonly Lazy<IUnityContainer> ContainerLazy = new Lazy<IUnityContainer>(Build);

		public static IUnityContainer Container => ContainerLazy.Value;

		private static IUnityContainer Build()
		{
			var container = new UnityContainer();

			RegisterInfrastructure(container);
			RegisterExamples(container);

			return container;
		}

		private static void RegisterInfrastructure(IUnityContainer container)
		{
			/**
			 * For registering as singleton
			 * Use ContainerControlledLifetimeManager as a life time manager
			 */

			container.RegisterType<ILogger, Logger>();
		}

		private static void RegisterExamples(IUnityContainer container)
		{
			var assemblies = GetAssemblies().ToList();

			foreach (var assembly in assemblies)
			{
				RegisterAllTypesForBaseTypeWithAttribute(assembly, container, typeof(BaseExample), typeof(RunAttribute));
			}
		}	
			
		private static void RegisterAllTypesForBaseTypeWithAttribute(Assembly assembly, 
			IUnityContainer container, Type typeToRegister, Type attributeType)
		{
			foreach (var exportedType in assembly.GetExportedTypes())
			{
				if (exportedType.IsAbstract) continue;

				var customAttributes = exportedType.GetCustomAttributes(attributeType).ToList();

				if (!customAttributes.Any()) continue;	

				if (!typeToRegister.IsAssignableFrom(exportedType)) continue;

				container.RegisterType(typeToRegister, exportedType, typeToRegister.FullName);
			}
		}

		private static IEnumerable<Assembly> GetAssemblies()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

			return Directory.GetFiles(path, "*.dll")
				.Select(Assembly.LoadFile);
		}
	}
}