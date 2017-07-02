using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WT.FileInterpreter.LineTranslators;
using WT.Resources;

namespace WT.FileInterpreter
{
	public class Interpreter
	{
		private String fileName { get; set; }
		private IList<String> messages { get; set; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; }
		internal IDictionary<String, Decimal> ThingValueDictionary { get; private set; }

		public IList<BaseLineTranslator> LineTranslators = new List<BaseLineTranslator>();




		public Interpreter(String fileName)
		{
			this.fileName = fileName;
			messages = new List<String>();

			ConversionDictionary = new Dictionary<String, String>();
			ThingValueDictionary = new Dictionary<String, Decimal>();
		}



		public IList<String> Execute()
		{
			if (!File.Exists(fileName))
			{
				return new List<String> {Messages.NotFound};
			}

			return translate();
		}



		private IList<String> translate()
		{
			var lines = File.ReadAllLines(fileName);

			foreach (var line in lines)
			{
				var translator = BaseLineTranslator.GetTranslator(this, line);
				LineTranslators.Add(translator);
			}

			foreach (var translator in LineTranslators)
			{
				translator.Translate();
			}

			return messages;
		}

		private void translate(String line)
		{
			
		}

		internal void AddMessage(String message)
		{
			messages.Add(message);
		}
	}
}