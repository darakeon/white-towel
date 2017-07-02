using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WT.Resources;

namespace WT.FileInterpreter
{
	public class Interpreter
	{
		private String fileName { get; set; }
		private IList<String> messages { get; set; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; }
		internal IDictionary<String, Decimal> ThingValueDictionary { get; private set; }




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
				translate(line);
			}

			return messages;
		}

		private void translate(String line)
		{
			if (translateAlienToRoman(line))
				return;

			if (translateAlienToCredits(line))
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



		private Boolean translateAlienToCredits(String line)
		{
			var match = Regex.Match(line, FileTranslation.TranslateAlienToCredits);

			if (!match.Success)
				return false;

			var key = match.Groups[1].Value.Trim();
			var value = (Decimal?)Decimal.Parse(match.Groups[2].Value);

			if (key.Contains(" "))
			{
				var numbers = key.Split(' ');
				key = numbers.Last();
				var romanDivisor = String.Empty;

				for (var n = 0; n < numbers.Length - 1; n++)
				{
					if (!ConversionDictionary.ContainsKey(numbers[n]))
					{
						var message = String.Format(Messages.UnknownAlienToRomanConversion, numbers[n]);

						messages.Add(message);
						return true;
					}

					romanDivisor += ConversionDictionary[numbers[n]];
				}

				var divisor = RomanConversor.Convert(romanDivisor);

				if (divisor == null)
				{
					value = null;

					var message = String.Format(Messages.UnknownAlienToRomanConversion, romanDivisor);
					messages.Add(message);
				}
				else
				{
					value /= divisor.Value;
				}
			}

			if (value.HasValue)
			{
				addIfNewKey(ThingValueDictionary, key, value.Value);
			}

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