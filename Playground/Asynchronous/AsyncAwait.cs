using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.Asynchronous
{
	public class AsyncAwait : BaseAsyncExample
	{	
		public override async Task RunAsync()
		{
			Console.WriteLine("Run async started");

			var identifiers = Enumerable.Range(1, 5);

			Console.WriteLine(Environment.NewLine + "Getting all random strings by id: ");
			/**
			 * As GetAllRandomStringsByIds is not awaited so the completion of 
			 * tasks will continue until await key word
			 */
			var taskWaitingForAllStrings = GetAllRandomStringsByIds(identifiers);

			Console.WriteLine(Environment.NewLine + "Getting first calculated random string: ");
			var firstCalculatedString = await GetFirstRandomStringsByIds(identifiers);

			Console.WriteLine(Environment.NewLine + "First calculated string was: " + firstCalculatedString);

			/**
			 * RunAsync is completed, but started tasks by GetFirstRandomStringsByIds
			 * can still be running
			 */
			await taskWaitingForAllStrings;
			Console.WriteLine(Environment.NewLine + "Run async completed");
		}

		private static async Task<IEnumerable<string>> GetAllRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 1)).ToList();

			await Task.WhenAll(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList.Select(it => it.Result);
		}

		private static async Task<string> GetFirstRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 2)).ToList();

			// returns the index of the completed Task object in the tasks array.
			var completedTask = await Task.WhenAny(gettingRandomStingTasksList.ToArray<Task<string>>());

			return completedTask.Result;	
		}

		private static async Task<string> GetRandomStringByIdAsync(int id, int seed)
		{
			await Task.Delay(150); // simulate super difficult calculations

			Console.WriteLine($"[{seed}] A random string was generated for id: {id}");

			return $"{id}. {Guid.NewGuid()}";
		}
	}
}