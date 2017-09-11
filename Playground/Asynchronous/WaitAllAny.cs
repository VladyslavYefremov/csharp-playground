using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.Asynchronous
{
	public class WaitAllAny : BaseAsyncExample
	{
		public override Task RunAsync()
		{
			Console.WriteLine("Run async started");

			var identifiers = Enumerable.Range(1, 10);

			Console.WriteLine(Environment.NewLine + "Getting all random strings by id: ");
			/**
			 * All the tasks started by GetAllRandomStringsByIds are finished
			 * after method is completed
			 */
			GetAllRandomStringsByIds(identifiers);

			Console.WriteLine(Environment.NewLine + "Getting first calculated random string: ");
			var firstCalculatedString =  GetFirstRandomStringsByIds(identifiers);

			Console.WriteLine(Environment.NewLine + "First calculated string was: " + firstCalculatedString);

			/**
			 * RunAsync is completed, but started tasks by GetFirstRandomStringsByIds
			 * can still be running
			 */
			Console.WriteLine(Environment.NewLine + "Run async completed");

			return Task.FromResult(true);
		}
			
		private static IEnumerable<string> GetAllRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(GetRandomStringByIdAsync).ToList();

			Task.WaitAll(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList.Select(it => it.Result);
		}	
			
		private static string GetFirstRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(GetRandomStringByIdAsync).ToList();

			// returns the index of the completed Task object in the tasks array.
			var completedTaskIndex = Task.WaitAny(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList[completedTaskIndex].Result;
		}

		private static async Task<string> GetRandomStringByIdAsync(int id)
		{
			await Task.Delay(100); // simulate super difficult calculations

			Console.WriteLine("A random string was generated for id: " + id);

			return $"{id}. {Guid.NewGuid()}";
		}
	}
}