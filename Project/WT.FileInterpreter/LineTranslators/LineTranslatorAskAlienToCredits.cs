using System;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAskAlienToCredits : BaseLineTranslatorAsk
	{
		public LineTranslatorAskAlienToCredits(Interpreter interpreter, Int32 order) 
			: base(interpreter, TranslatorType.AskAlienToCredits, FileTranslation.AnswerAlienToCredits, order) { }

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