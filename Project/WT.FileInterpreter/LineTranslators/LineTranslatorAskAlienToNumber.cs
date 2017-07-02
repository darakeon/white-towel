using System;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAskAlienToNumber : BaseLineTranslatorAsk
	{
		public LineTranslatorAskAlienToNumber(Interpreter interpreter, Int32 order)
			: base(interpreter, TranslatorType.AskAlienToNumber, FileTranslation.AnswerAlienToNumber, order)
		{ }

		public override String GetValue()
		{
			return Match.Groups[1].Value.Trim();
		}

		public override String GetThing()
		{
			return null;
		}
	}
}