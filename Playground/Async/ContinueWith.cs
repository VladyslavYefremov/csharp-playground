using System;
using System.Threading.Tasks;

namespace Playground.Async
{
	public class ContinueWith
	{
		public void Run()
		{
			var task = Task.Run(async () => await Level1())
				.ContinueWith(antecedent => { Console.WriteLine("Exception occured!"); }, TaskContinuationOptions.OnlyOnFaulted)
				.ContinueWith( antecedent => { Console.WriteLine("Looks good!"); }, TaskContinuationOptions.NotOnFaulted)
				.ContinueWith((antecedent) => Level1());

			task.Wait();
		}

		private async Task Level1()
		{
			Console.WriteLine("Level1: executing Level2");

			await Level2();
				//.ContinueWith(
				//	(antecedent) => Console.WriteLine("An error occured during executing Level2"),
				//	TaskContinuationOptions.OnlyOnFaulted
				//);
		}
		private async Task Level2()
		{
			await Task.Delay(100);

			Console.WriteLine("Level2: finished!");
		}
	}
}