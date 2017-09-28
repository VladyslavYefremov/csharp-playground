using System;
using Infrastructure.Logging;

namespace Infrastructure
{
	public abstract class BaseExample
	{
		public ILogger Logger { get; protected set; }

		protected Profiler StartProviler(string name, ILogger logger)
		{
			return new Profiler(name, logger);
		}

		protected class Profiler : IDisposable
		{
			private readonly string _codeName;
			private readonly ILogger _logger;

			public Profiler(string name, ILogger logger)
			{
				_codeName = name;
				_logger = logger;

				_logger.Write($"{GetDateNowString()}: {_codeName} started");
			}

			public void Dispose()
			{
				_logger.Write($"{GetDateNowString()}: {_codeName} finished");
			}

			private static string GetDateNowString()
			{
				var now = DateTime.Now;

				return $"{now.Hour}:{now.Minute}:{now.Second}.{now.Millisecond}";
			}
		}
	}
}