using System;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public class LineTranslatorNoIdea : BaseLineTranslator
	{
		public LineTranslatorNoIdea(Interpreter interpreter, Int32 order) : base(interpreter, TranslatorType.NoIdea, order)
		{
		}

		public override void Translate()
		{
			Interpreter.AddError(FileTranslation.AnswerNoIdea, Order);
		}
	}
}