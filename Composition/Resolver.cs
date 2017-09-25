using Microsoft.Practices.Unity;

namespace Composition
{	
	public static class Resolver	
	{
		public static T Get<T>()
		{
			return CompositionContainer.Container.Resolve<T>();
		}
	}
}