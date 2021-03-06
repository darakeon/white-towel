using System;
using WT.Generics;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAlienToRoman : BaseLineTranslator
	{
		public LineTranslatorAlienToRoman(Interpreter interpreter, Int32 order) 
			: base(interpreter, TranslatorType.TranslateAlienToRoman, order) { }


		public override void Translate()
		{
			var key = Match.Groups[1].Value;
			var value = Match.Groups[2].Value;

			var message = Interpreter.ConversionDictionary.HandleKeyValue(key, value);

			if (!String.IsNullOrEmpty(message))
				Interpreter.AddWarning(message, Order);
		}

	}
}