using System;

namespace Infrastructure
{
	public abstract class BaseExample
	{

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
}