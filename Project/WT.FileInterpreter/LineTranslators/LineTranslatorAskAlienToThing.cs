using System;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAskAlienToThing : BaseLineTranslatorAsk
	{
		public LineTranslatorAskAlienToThing(Interpreter interpreter, Int32 order)
			: base(interpreter, TranslatorType.AskAlienToThing, FileTranslation.AnswerAlienToThing, order)
		{ }

		public override String GetValue()
		{
			return Match.Groups[2].Value.Trim();
		}

		public override String GetThing()
		{
			return Match.Groups[1].Value.Trim();
		}


	}
}