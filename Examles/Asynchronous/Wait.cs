using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Logging;

namespace Examles.Asynchronous
{
	public class Wait: BaseAsyncExample
	{
		public Wait(ILogger logger)
		{
			Logger = logger;
		}

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
			using (StartProviler(nameof(DoAsync), Logger))
			{
				/**
				 * awaits newly created task that completes in 1000ms
				 */
				await Task.Delay(1000); 
			}
		}
	}
}