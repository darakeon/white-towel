using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WT.FileInterpreter.LineTranslators;
using WT.Resources;

namespace WT.FileInterpreter
{
	public class Interpreter
	{
		private String fileName { get; }
		private IList<Message> messages { get; set; }

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
				AddError(Messages.FileNotFound, 0);
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
				AddError(message, 0);
				return messages;
			}

			var translators = new List<BaseLineTranslator>();

			for (var l = 0; l < lines.Length; l++)
			{
				var line = lines[l]?.Trim();
				var translator = BaseLineTranslator.GetTranslator(this, line, l);
				translators.Add(translator);
			}

			translators = translators.OrderBy(t => t.Type).ToList();

			foreach (var translator in translators)
			{
				translator?.Translate();
			}

			messages = messages.OrderBy(m => m.Order).ToList();

			return messages;
		}



		internal void AddInfo(String text, Int32 order)
		{
			messages.Add(Message.Info(text, order));
		}

		internal void AddWarning(String text, Int32 order)
		{
			messages.Add(Message.Warning(text, order));
		}

		internal void AddError(String text, Int32 order)
		{
			messages.Add(Message.Error(text, order));
		}

	}
}