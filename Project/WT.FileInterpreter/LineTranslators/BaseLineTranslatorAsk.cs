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

		public override void Translate()
		{
			var value = Match.Groups[1].Value.Trim();
			var parts = value.Split(' ');

			var roman = String.Empty;
			var thing = 1m;
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
					thing *= Interpreter.ThingValueDictionary[thingName];
				}
				else
				{
					var message = String.Format(Messages.UnknownAlienNumberOrThing, thingName);
					Interpreter.AddError(message, Order);
					return;
				}
			}

			var arabic = RomanConversor.Convert(roman);

			if (arabic.HasValue)
			{
				var message = String.Format(rightAnswer, value, (int) (arabic*thing));
				Interpreter.AddInfo(message, Order);
			}
			else
			{
				var message = String.Format(Messages.UnknownRomanNumber, roman);
				Interpreter.AddError(message, Order);
			}
		}

	}
}