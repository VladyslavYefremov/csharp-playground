using System;
using System.Threading.Tasks;

namespace Examles.Async
{
	// As a general rule, async lambdas should only be used if they’re converted to a delegate type that returns Task (for example, Func<Task>)
	public class VoidProblem
	{
		public Task Run()
		{
			try
			{
				return Task.Factory.StartNew(function: async () =>
				{
					try
					{
						await Task.Delay(5);
						throw new Exception("An error inside the async Action delegate");
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						throw;
					}
					return true;
				});
			}
			catch(Exception ex)
			{
				Console.WriteLine($"{nameof(VoidProblem)} catch an exception: ");
				Console.WriteLine(ex.Message);
			}

			return null;
		}
	}
}