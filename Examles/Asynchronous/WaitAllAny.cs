using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Logging;

namespace Examples.Asynchronous
{
	public class WaitAllAny : BaseAsyncExample
	{
		public WaitAllAny(ILogger logger)
		{
			Logger = logger;
		}

		public override Task RunAsync()
		{
			Logger.Write("Run async started");

			var identifiers = Enumerable.Range(1, 5);

			Logger.Write("Getting all random strings by id: ");
			/**
			 * All the tasks started by GetAllRandomStringsByIds are finished
			 * after method is completed
			 */
			GetAllRandomStringsByIds(identifiers);

			Logger.Write("Getting first calculated random string: ");
			var firstCalculatedString =  GetFirstRandomStringsByIds(identifiers);

			Logger.Write("First calculated string was: " + firstCalculatedString);

			/**
			 * RunAsync is completed, but started tasks by GetFirstRandomStringsByIds
			 * can still be running
			 */
			Logger.Write("Run async completed");

			return Task.FromResult(true);
		}
			
		private IEnumerable<string> GetAllRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 1)).ToList();

			Task.WaitAll(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList.Select(it => it.Result);
		}	
			
		private string GetFirstRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 2)).ToList();

			// returns the index of the completed Task object in the tasks array.
			var completedTaskIndex = Task.WaitAny(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList[completedTaskIndex].Result;
		}

		private async Task<string> GetRandomStringByIdAsync(int id, int seed)
		{
			await Task.Delay(150); // simulate super difficult calculations

			Logger.Write($"[{seed}] A random string was generated for id: {id}");

			return $"{id}. {Guid.NewGuid()}";
		}
	}
}