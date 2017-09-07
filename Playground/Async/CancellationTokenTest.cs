using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Async
{
	public class CancellationTokenTest
	{

		public void Run()
		{
			var ctc = new CancellationTokenSource();

			try
			{
				DoAsync(ctc.Token).Wait();

				Console.WriteLine("Done");
			}
			catch (OperationCanceledException ex)
			{
				
			}
			catch (AggregateException exception)
			{
				Console.WriteLine("Inside try-catch block");
				Console.WriteLine();
				Console.WriteLine(exception);

				exception.Handle(ex =>
				{
					Console.WriteLine(ex.Message);
					return true;
				});
			}
		}

		//private async Task TestSingle(CancellationTokenSource ctc)
		//{
		//	await DoAsync(1, ctc);
		//}

		//private async Task TestSequence(CancellationTokenSource ctc)
		//{
		//	foreach (var i in Enumerable.Range(1, 10))
		//	{
		//		await DoAsync(i, ctc);
		//	}	
		//}
		//private async Task TestSequenceAggregation(CancellationTokenSource ctc)
		//{
		//	var tasks = Enumerable.Range(1, 10).Select(i => DoAsync(i, ctc));

		//	await Task.WhenAll(tasks);
		//}

		private Task DoAsync(CancellationToken token)
		{
			Console.WriteLine("DoAsync started");

			return Task.Run(() =>
								Console.WriteLine("DoAsync Run"),
						token
					)
					.ContinueWith(antecedent =>
								Console.WriteLine("DoAsync Run cancelled"),
						TaskContinuationOptions.OnlyOnCanceled
					)
				; //.ContinueWith(antecedent => { });
		}
	}
}