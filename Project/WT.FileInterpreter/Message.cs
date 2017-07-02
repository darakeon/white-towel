using System;

namespace WT.FileInterpreter
{
	public class Message
	{
		private Message(String text, MessageType type, Int32 order)
		{
			Text = text;
			Type = type;
			Order = order;
		}

		public String Text { get; private set; }
		public MessageType Type { get; private set; }
		public Int32 Order { get; set; }

		public enum MessageType
		{
			Info,
			Warning,
			Error
		}



		public static Message Info(String text, Int32 order)
		{
			return new Message(text, MessageType.Info, order);
		}

		public static Message Warning(String text, Int32 order)
		{
			return new Message(text, MessageType.Warning, order);
		}

		public static Message Error(String text, Int32 order)
		{
			return new Message(text, MessageType.Error, order);
		}



		public override string ToString()
		{
			return Text;
		}


	}
}