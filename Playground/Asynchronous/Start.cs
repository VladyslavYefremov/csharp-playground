using System;
using System.Threading.Tasks;
using Infrastructure;

namespace Playground.Asynchronous
{
	public class Start : BaseAsyncExample
	{
		public override Task RunAsync()
		{
			return Task.WhenAll(
				Manual(),
				TaskRun(),
				TaskFactoryStartNew()
			);
		}

		/** You should not create a Task from constructor without a compelling reason. 
		 * TPL takes a lot of care with synchronization and concurrency.
		 * It hides those complexity from client code. 
		 * If you construct a Task and Start it, you need to think about synchronization and concurrency which obviously hard to handle.
		 */
		private Task Manual()
		{
			var task = new Task(() => Console.WriteLine("Manual creation of task"));   

			task.Start();

			return task;
		}
			
		private Task TaskRun()
		{
			/**
			 * Task.Run is a shorthand for Task.Factory.StartNew with specific safe arguments:
			 * 	Task.Factory.StartNew(
			 *		action,
			 *		CancellationToken.None, 
			 *		TaskCreationOptions.DenyChildAttach, 
			 *		TaskScheduler.Default);
			 *		
			 *	It was added in .Net 4.5 to help with the increasingly frequent usage of async.
			 */
			return Task.Run(() => Console.WriteLine("Task.Run - task"));
		}

		/** Task.Factory.StartNew (added with TPL in .Net 4.0) is much more robust. 
		 * You should only use it if  Task.Run isn't enough, 
		 * for example when you want to use TaskCreationOptions.LongRunning
		 */
		private Task TaskFactoryStartNew()
		{
			return Task.Factory.StartNew(() => Console.WriteLine("Task.Factory.StartNew - task"));
		}
	}
}
 