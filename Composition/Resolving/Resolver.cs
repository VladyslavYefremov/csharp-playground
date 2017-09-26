using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Composition.Resolving
{	
	public class Resolver	
	{
		public T Get<T>()
		{
			return CompositionContainer.Container.Resolve<T>();
		}

		public IEnumerable<T> GetAll<T>()
		{
			return CompositionContainer.Container.ResolveAll<T>();
		}
	}
}