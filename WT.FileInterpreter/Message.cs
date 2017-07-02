using System;

namespace WT.FileInterpreter
{
	public class Message
	{
		private Message(String text, MessageType type)
		{
			Text = text;
			Type = type;
		}

		public String Text { get; private set; }
		public MessageType Type { get; private set; }

		public enum MessageType
		{
			Info,
			Warning,
			Error
		}



		public static Message Info(String text)
		{
			return new Message(text, MessageType.Info);
		}

		public static Message Warning(String text)
		{
			return new Message(text, MessageType.Warning);
		}

		public static Message Error(String text)
		{
			return new Message(text, MessageType.Error);
		}



		public override string ToString()
		{
			return Text;
		}


	}
}