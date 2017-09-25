using System;
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

			BuildInfrastructure(container);

			return container;
		}

		private static void BuildInfrastructure(IUnityContainer container)
		{
			// Register as singleton
			// container.RegisterType<ILogger, Logger>(null, new ContainerControlledLifetimeManager());

			container.RegisterType<ILogger, Logger>();
		}
	}
}