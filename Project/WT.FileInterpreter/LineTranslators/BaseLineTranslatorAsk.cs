using System;
using System.Linq;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public abstract class BaseLineTranslatorAsk : BaseLineTranslator
	{
		private readonly string rightAnswer;

		protected BaseLineTranslatorAsk(Interpreter interpreter, TranslatorType type, String rightAnswer, Int32 order)
			: base(interpreter, type, order)
		{
			this.rightAnswer = rightAnswer;
		}

		public abstract String GetValue();
		public abstract String GetThing();

		public override void Translate()
		{
			var value = GetValue();
			var parts = value.Split(' ');

			var roman = String.Empty;
			var thingValue = 1m;
			var lastRoman = parts.Length;

			for (var p = 0; p < parts.Length; p++)
			{
				var part = parts[p];
				
				if (Interpreter.ConversionDictionary.ContainsKey(part))
				{
					roman += Interpreter.ConversionDictionary[part];
				}
				else
				{
					lastRoman = p;
                    break;
				}
			}

			parts = parts.Skip(lastRoman).ToArray();

			if (parts.Any())
			{
				var thingName = String.Join(" ", parts);

				if (Interpreter.ThingValueDictionary.ContainsKey(thingName))
				{
					thingValue *= Interpreter.ThingValueDictionary[thingName];
				}
				else
				{
					var message = String.Format(Messages.UnknownAlienNumberOrThing, thingName);
					Interpreter.AddError(message, Order);
					return;
				}
			}

			var arabic = RomanConversor.Convert(roman);
			var thingToBeValued = GetThing();

			var finalValue = getFinalValue(arabic, thingValue, thingToBeValued);

			if (finalValue.HasValue)
			{
				var message = String.Format(rightAnswer, value, finalValue, thingToBeValued);
				Interpreter.AddInfo(message, Order);
			}
			else
			{
				var message = String.Format(Messages.UnknownRomanNumber, roman);
				Interpreter.AddError(message, Order);
			}

		}

		private int? getFinalValue(int? arabic, decimal thingValue, string thingToBeValued)
		{
			if (!arabic.HasValue)
				return null;

			var finalValue = (int?) (arabic*thingValue);

			if (!String.IsNullOrEmpty(thingToBeValued))
			{
				if (!Interpreter.ThingValueDictionary.ContainsKey(thingToBeValued))
				{
					return null;
				}
				
				var thingDivisor = Interpreter.ThingValueDictionary[thingToBeValued];
				finalValue = (int) (finalValue.Value/thingDivisor);
			}

			return finalValue;
		}


	}
}