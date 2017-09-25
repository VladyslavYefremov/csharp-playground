using System;

namespace Infrastructure.Logging
{
	public class Message
	{
		public string Text { get; set; } = string.Empty;
		public LogType Type { get; set; } = LogType.Default;
		public DateTime Time { get; set; } = DateTime.Now;

		public override string ToString()
		{
			return $"{Time.ToShortTimeString()}: {Text} ({Type})";
		}
	}
}