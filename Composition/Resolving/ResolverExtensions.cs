using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Composition.Resolving
{
	public static class ResolverExtensions
	{
		public static IEnumerable<BaseAsyncExample> GetAllAsyncExamples(this Resolver resolver)
		{
			return resolver
				.GetAll<BaseExample>()
				.OfType<BaseAsyncExample>();
		}
		public static IEnumerable<BaseSynchronousExample> GetAllSyncExamples(this Resolver resolver)
		{
			return resolver
				.GetAll<BaseExample>()
				.OfType<BaseSynchronousExample>();	
		}
	}
}