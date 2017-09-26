using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;

namespace Examles.Async
{
	public class LongRunning
	{
		public void Run()
		{
			try
			{
				Task.Run(async () => await LongJobWithExceptionAsync())
					.ContinueWith((e) =>
					{
						Console.WriteLine($"An exception occured: {string.Join(", ", e?.Exception?.InnerExceptions?.Select(it => it.Message) ?? new List<string>())}");
					}, TaskContinuationOptions.OnlyOnFaulted)
					.ConfigureAwait(false);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}	

		private static async Task LongJobAsync()
		{
			Console.WriteLine("LongJobAsync");
			await Task.Delay(2000);
		}	

		private static async Task LongJobWithExceptionAsync()
		{
			Console.WriteLine("LongJobWithExceptionAsync");

			await Task.Delay(2000);

			throw new Exception("wtf man!?");
		}
	}
}