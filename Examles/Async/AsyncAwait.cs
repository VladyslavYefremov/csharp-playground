using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Async
{
	/**
	 * Wait, WaitAll, WaitAny
	 * Task.Run, Task.Factory.StartNew
	 * async, await
	 * WhenAll, WhenAny
	 * ContinueWith
	 * Continuation options
	 * TaskScheduler
	 * CancellationToken: IsCancelled, ThrowIfCancellationRequested
	 * Error handling
	 * Dead locks
	 * ConfigureAwait
	 * Synchronization context
	 * async void problem
	 **/

	public class AsyncAwait
	{
		public async Task Run()
		{
			await Level1();

			Console.WriteLine($"Run: {Thread.CurrentThread.ThreadState}");
		}

		async Task Level1()
		{
			try
			{
				Console.WriteLine($"Level1: {Thread.CurrentThread.Name}");
				await Task.WhenAll(new List<Task> {Level2()});
			}
			catch
			{
				Console.WriteLine("Error handler");
			}
		}
		
		async Task Level2()
		{
			Console.WriteLine($"Level2: {Thread.CurrentThread.Name}");

			if (false)
			{
				return;
			}

			throw new Exception("Manual exc");

			await Task.Delay(1000);

			Console.WriteLine("Level2 finished");
		}
	}
}