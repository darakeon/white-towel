using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAskAlienToCredits : BaseLineTranslatorAsk
	{
		public LineTranslatorAskAlienToCredits(Interpreter interpreter) 
			: base(interpreter, TranslatorType.AskAlienToCredits, FileTranslation.AnswerAlienToCredits) { }			
	}
}