using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;

namespace Playground.Async
{
	[Run]
	public class ForEach : BaseSynchronousExample
	{
		public override void Run()
		{
			Task.Run(async () =>
			{
				try
				{
					await RunAll().ContinueWith((task) =>
					{
						Console.WriteLine("On continuation");
						task?.Exception?.Handle((e) =>
						{
							Console.WriteLine(e.Message);
							return true;
						});
					}, TaskContinuationOptions.OnlyOnFaulted);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Inside the task exception handler: " + ex.Message);
				}
			});

		}

		private async Task RunAll()
		{
			var list = Enumerable.Range(1, 10).ToList();

			Console.WriteLine("Run started");

			var tasks = list.Select(Invoke);

			await Task.WhenAll(tasks);

			Console.WriteLine("Run finished");
		}

		private async Task Invoke(int a)
		{
			if (a == 5)
			{
				throw new ArgumentException("a == 5 is not supported");
			}

			Console.WriteLine($"[{a}] Invoke started");

			await Task.Delay(500);

			Console.WriteLine($"[{a}] Invoke finished");
		}
	}
}