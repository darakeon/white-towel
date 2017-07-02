using System;
using System.Collections.Generic;
using System.Linq;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAlienToCredits : BaseLineTranslator
	{
		public LineTranslatorAlienToCredits(Interpreter interpreter, Int32 order) 
			: base(interpreter, TranslatorType.TranslateAlienToCredits, order) { }



		public override void Translate()
		{
			var key = Match.Groups[1].Value.Trim();
			var value = (Decimal?) Decimal.Parse(Match.Groups[2].Value);
			String name;

			if (!key.Contains(" "))
			{
				name = key;
			}
			else
			{
				var numbers = key.Split(' ');
				var names = new List<String>();

				for (var n = 0; n < numbers.Length; n++)
				{
					if (numbers[n][0].ToString() == numbers[n][0].ToString().ToUpper())
					{
						names.Add(numbers[n]);
						numbers[n] = String.Empty;
					}
				}

				name = String.Join(" ", names);
				var romanDivisor = String.Empty;

				foreach (var number in numbers)
				{
					if (String.IsNullOrEmpty(number))
						continue;

					if (!Interpreter.ConversionDictionary.ContainsKey(number))
					{
						var message = String.Format(Messages.UnknownAlienToRomanConversion, number);

						Interpreter.AddError(message, Order);
						return;
					}

					romanDivisor += Interpreter.ConversionDictionary[number];
				}

				var divisor = RomanConversor.Convert(romanDivisor);

				if (divisor == null)
				{
					value = null;

					var message = String.Format(Messages.UnknownRomanNumber, romanDivisor);
					Interpreter.AddError(message, Order);
				}
				else if (divisor.Value != 0)
				{
					value /= divisor.Value;
				}
			}

			if (value.HasValue)
			{
				var message = Interpreter.ThingValueDictionary.HandleKeyValue(name, value.Value);

				if (!String.IsNullOrEmpty(message))
					Interpreter.AddWarning(message, Order);
			}
		}


	}
}