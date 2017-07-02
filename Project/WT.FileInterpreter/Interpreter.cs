using System;
using System.Collections.Generic;
using System.IO;
using WT.FileInterpreter.LineTranslators;
using WT.Resources;

namespace WT.FileInterpreter
{
	public class Interpreter
	{
		private String fileName { get; }
		private IList<Message> messages { get; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; }
		internal IDictionary<String, Decimal> ThingValueDictionary { get; private set; }




		public Interpreter(String fileName)
		{
			this.fileName = fileName;
			messages = new List<Message>();

			ConversionDictionary = new Dictionary<String, String>();
			ThingValueDictionary = new Dictionary<String, Decimal>();
		}



		public IList<Message> Execute()
		{
			if (!File.Exists(fileName))
			{
				AddError(Messages.FileNotFound);
				return messages;
			}

			return translate();
		}

		private IList<Message> translate()
		{
			String[] lines;

			try
			{
				lines = File.ReadAllLines(fileName);
			}
			catch (IOException e)
			{
				var message = String.Format(Messages.FileWithProblem, e.Message);
				AddError(message);
				return messages;
			}

			foreach (var line in lines)
			{
				var translator = BaseLineTranslator.GetTranslator(this, line);
				translator?.Translate();
			}

			return messages;
		}



		internal void AddInfo(String text)
		{
			messages.Add(Message.Info(text));
		}

		internal void AddWarning(String text)
		{
			messages.Add(Message.Warning(text));
		}

		internal void AddError(String text)
		{
			messages.Add(Message.Error(text));
		}

	}
}