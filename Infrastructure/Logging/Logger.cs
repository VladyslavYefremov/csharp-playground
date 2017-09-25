using System;

namespace Infrastructure.Logging
{
	public class Logger : ILogger
	{
		public event Action<Message> OnNewMessage;

		public void Write(string text)
		{
			Write(new Message {Text = text});
		}	

		public void Write(string text, LogType type)
		{
			Write(new Message { Text = text, Type = type });
		}

		public void Write(string text, LogType type, DateTime time)
		{
			Write(new Message { Text = text, Type = type, Time = time });
		}

		public void Write(Message message)
		{
			OnNewMessage?.Invoke(message);
		}
	}
}