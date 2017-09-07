using System;
using System.Threading.Tasks;

namespace Playground.Asynchronous
{
	public abstract class BaseAsyncExample
	{
		public abstract Task RunAsync();

		protected Profiler StartProviler(string name)
		{
			return new Profiler(name);
		}

		protected class Profiler : IDisposable
		{
			private readonly string _codeName;

			public Profiler(string name)
			{
				_codeName = name;

				Console.WriteLine($"{GetDateNowString()}: {_codeName} started");
			}

			public void Dispose()
			{
				Console.WriteLine($"{GetDateNowString()}: {_codeName} finished");
			}

			private static string GetDateNowString()
			{
				var now = DateTime.Now;

				return $"{now.Hour}:{now.Minute}:{now.Second}.{now.Millisecond}";
			}
		}
	}
	/**
	 * http://www.johnchukwuma.com/training/clr_via_c_4th_edition.pdf
	 * 
	 */
}