using System;

namespace Infrastructure.Logging
{
	public interface ILogger
	{	
		event Action<Message> OnNewMessage;
		void Write(string text);
		void Write(string text, MessageColor color);
		void Write(string text, LogType type);
		void Write(string text, LogType type, DateTime time);
		void Write(Message message);
	}
}