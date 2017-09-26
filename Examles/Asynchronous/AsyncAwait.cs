using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Logging;

namespace Examles.Asynchronous
{
	[Run]
	public class AsyncAwait : BaseAsyncExample
	{
		public AsyncAwait(ILogger logger)
		{
			Logger = logger;
		}

		public override async Task RunAsync()
		{
			Logger.Write("Run async started");

			var identifiers = Enumerable.Range(1, 5);

			Logger.Write("Getting all random strings by id: ");
			/**
			 * As GetAllRandomStringsByIds is not awaited so the completion of 
			 * tasks will continue until await key word
			 */
			var taskWaitingForAllStrings = GetAllRandomStringsByIds(identifiers);

			Logger.Write("Getting first calculated random string: ");
			var firstCalculatedString = await GetFirstRandomStringsByIds(identifiers);

			Logger.Write("First calculated string was: " + firstCalculatedString);

			/**
			 * RunAsync is completed, but started tasks by GetFirstRandomStringsByIds
			 * can still be running
			 */
			await taskWaitingForAllStrings;
			Logger.Write("Run async completed");
		}

		private async Task<IEnumerable<string>> GetAllRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 1)).ToList();

			await Task.WhenAll(gettingRandomStingTasksList.ToArray<Task>());

			return gettingRandomStingTasksList.Select(it => it.Result);
		}

		private async Task<string> GetFirstRandomStringsByIds(IEnumerable<int> identifiers)
		{
			var gettingRandomStingTasksList = identifiers.Select(id => GetRandomStringByIdAsync(id, 2)).ToList();

			// returns the index of the completed Task object in the tasks array.
			var completedTask = await Task.WhenAny(gettingRandomStingTasksList.ToArray<Task<string>>());

			return completedTask.Result;	
		}

		private async Task<string> GetRandomStringByIdAsync(int id, int seed)
		{
			await Task.Delay(150); // simulate super difficult calculations

			Logger.Write($"[{seed}] A random string was generated for id: {id}");

			return $"{id}. {Guid.NewGuid()}";
		}
	}
}