using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorAskAlienToNumber : BaseLineTranslatorAsk
	{
		public LineTranslatorAskAlienToNumber(Interpreter interpreter) 
			: base(interpreter, TranslatorType.AskAlienToNumber, FileTranslation.AnswerAlienToNumber) { }
	}
}