using System.Threading.Tasks;
using Infrastructure;

namespace Playground.Asynchronous
{
	public class Wait: BaseAsyncExample
	{
		public override Task RunAsync()
		{
			/**
			 * Synchronously waits for DoAsync to be completed
			 * Current thread is blocked !
			 */
			DoAsync().Wait();

			/**
			 * Returns task that completes successfully 
			 * with the specified result
			 */
			return Task.FromResult<bool>(true);
		}

		private async Task DoAsync()
		{
			using (StartProviler(nameof(DoAsync)))
			{
				/**
				 * awaits newly created task that completes in 1000ms
				 */
				await Task.Delay(1000); 
			}
		}
	}
}