using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Async
{
	public class CancellationTest
	{
		public void Run()
		{
			var ctc = new CancellationTokenSource();

			var cancellable = ExecuteLongCancellableAdvancedMethod(ctc.Token);

			var cancelationTask = Task.Run(() =>
			{
				Thread.Sleep(2000);

				Console.WriteLine("[Before cancellation]");

				ctc.Cancel();
			});

			try
			{
				Task.WaitAll(cancellable, cancelationTask);
			}
			catch (Exception e)
			{
				Console.WriteLine($"An exception occured with type {e.GetType().Name}");
			}
		}

		private static Task ExecuteLongCancellableMethod(CancellationToken token)
		{
			return Task.Run(() =>
			{
				token.ThrowIfCancellationRequested();

				Console.WriteLine("1st");

				Thread.Sleep(1000);

				Console.WriteLine("2nd");

				Thread.Sleep(1000);

				Console.WriteLine("3rd");

				Thread.Sleep(1000);

				Console.WriteLine("4th");

				Thread.Sleep(1000);

				Console.WriteLine("[Completed]");

			}, token);
		}

		private static Task ExecuteLongCancellableAdvancedMethod(CancellationToken token)
		{
			return Task.Run(() =>
			{
				var actions = new List<Action>
				{
					() => Console.WriteLine("1st"),
					() => Console.WriteLine("2nd"),
					() => Console.WriteLine("3rd"),
					() => Console.WriteLine("4th"),
					() => Console.WriteLine("[Completed]")
				};

				foreach (var action in actions)
				{
					token.ThrowIfCancellationRequested();

					action.Invoke();

					Thread.Sleep(1000);
				}

			}, token);
		}
	}
}