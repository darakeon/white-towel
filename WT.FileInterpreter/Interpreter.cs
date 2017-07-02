using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WT.Resources;

namespace WT.FileInterpreter
{
	public class Interpreter
	{
		private String fileName { get; set; }
		private IList<String> messages { get; set; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; }
		internal IDictionary<String, Decimal> ValueThingDictionary { get; private set; }



		public Interpreter(String fileName)
		{
			this.fileName = fileName;
			messages = new List<String>();

			ConversionDictionary = new Dictionary<String, String>();
			ValueThingDictionary = new Dictionary<String, Decimal>();
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
				translate(line);
			}

			return messages;
		}

		private void translate(String line)
		{
			if (translateAlienToRoman(line))
				return;

		}



		private Boolean translateAlienToRoman(String line)
		{
			var match = Regex.Match(line, FileTranslation.TranslateAlienToRoman);

			if (!match.Success)
				return false;

			var key = match.Groups[1].Value;
			var value = match.Groups[2].Value;

			addIfNewKey(ConversionDictionary, key, value);

			return true;
		}



		private void addIfNewKey<T>(IDictionary<String, T> dic, String key, T value)
		{
			if (dic.ContainsKey(key))
			{
				repeatedKey(key, value, dic[key]);
			}
			else
			{
				dic.Add(key, value);
			}
		}

		private void repeatedKey(string key, object ignoredValue, object storedValue)
		{
			var format = ignoredValue.Equals(storedValue)
				? Messages.AlreadyStoredConversion
				: Messages.DuplicatedConversion;

			var message = String.Format(format, key, ignoredValue, storedValue);
			messages.Add(message);
		}



	}
}