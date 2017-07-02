using System;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public abstract class BaseLineTranslatorAsk : BaseLineTranslator
	{
		private readonly string rightAnswer;

		protected BaseLineTranslatorAsk(Interpreter interpreter, TranslatorType type, String rightAnswer)
			: base(interpreter, type)
		{
			this.rightAnswer = rightAnswer;
		}

		public override void Translate()
		{
			var value = Match.Groups[1].Value.Trim();
			var parts = value.Split(' ');

			var roman = String.Empty;
			var thing = 1m;

			foreach (var part in parts)
			{
				if (Interpreter.ConversionDictionary.ContainsKey(part))
				{
					roman += Interpreter.ConversionDictionary[part];
				}
				else if (Interpreter.ThingValueDictionary.ContainsKey(part))
				{
					thing *= Interpreter.ThingValueDictionary[part];
				}
				else
				{
					var message = String.Format(Messages.UnknownAlienNumberOrThing, part);
					Interpreter.AddMessage(message);
					return;
				}
			}

			var arabic = RomanConversor.Convert(roman);

			var successMessage = arabic.HasValue
				? String.Format(rightAnswer, value, arabic * thing)
				: String.Format(Messages.UnknownRomanNumber, roman);

			Interpreter.AddMessage(successMessage);
		}
	}
}