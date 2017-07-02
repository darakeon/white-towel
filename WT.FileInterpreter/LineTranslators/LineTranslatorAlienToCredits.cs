using System;
using System.Linq;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAlienToCredits : BaseLineTranslator
	{
		public LineTranslatorAlienToCredits(Interpreter interpreter) 
			: base(interpreter, TranslatorType.TranslateAlienToCredits) { }



		public override void Translate()
		{
			var key = Match.Groups[1].Value.Trim();
			var value = (Decimal?) Decimal.Parse(Match.Groups[2].Value);

			if (key.Contains(" "))
			{
				var numbers = key.Split(' ');
				key = numbers.Last();
				var romanDivisor = String.Empty;

				for (var n = 0; n < numbers.Length - 1; n++)
				{
					if (!Interpreter.ConversionDictionary.ContainsKey(numbers[n]))
					{
						var message = String.Format(Messages.UnknownAlienToRomanConversion, numbers[n]);

						Interpreter.AddMessage(message);
						return;
					}

					romanDivisor += Interpreter.ConversionDictionary[numbers[n]];
				}

				var divisor = RomanConversor.Convert(romanDivisor);

				if (divisor == null)
				{
					value = null;

					var message = String.Format(Messages.UnknownAlienToRomanConversion, romanDivisor);
					Interpreter.AddMessage(message);
				}
				else
				{
					value /= divisor.Value;
				}
			}

			if (value.HasValue)
			{
				var message = Interpreter.ThingValueDictionary.HandleKeyValue(key, value.Value);

				if (!String.IsNullOrEmpty(message))
					Interpreter.AddMessage(message);
			}
		}


	}
}