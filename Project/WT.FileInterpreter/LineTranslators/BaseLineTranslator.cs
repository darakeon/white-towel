using System;
using System.Text.RegularExpressions;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public abstract class BaseLineTranslator
	{
		protected BaseLineTranslator(Interpreter interpreter, TranslatorType type, Int32 order)
		{
			Interpreter = interpreter;
			Type = type;
			Order = order;
		}

		protected Interpreter Interpreter { get; private set; }
		protected Match Match { get; set; }
		public TranslatorType Type { get; private set; }

		public Int32 Order { get; private set; }



		public abstract void Translate();



		public static BaseLineTranslator GetTranslator(Interpreter interpreter, String line, Int32 order)
		{
			return getTranslator<LineTranslatorAlienToRoman>(interpreter, line, order)
				?? getTranslator<LineTranslatorAlienToCredits>(interpreter, line, order)
				?? getTranslator<LineTranslatorAskAlienToNumber>(interpreter, line, order)
				?? getTranslator<LineTranslatorAskAlienToCredits>(interpreter, line, order)
				?? new LineTranslatorNoIdea(interpreter, order);
		}

		private static BaseLineTranslator getTranslator<T>(Interpreter interpreter, String line, Int32 order)
			where T : BaseLineTranslator
		{
			var translator = Instance.Create<T>(interpreter, order);
			return translator.isMatch(line) ? translator : null;
		}

		private Boolean isMatch(String line)
		{
			var pattern = FileTranslation.ResourceManager.GetString(Type.ToString());

			if (pattern == null)
				return false;

			Match = Regex.Match(line, pattern);

			return Match.Success;
		}



		public enum TranslatorType
		{
			TranslateAlienToRoman = 1,
			TranslateAlienToCredits = 2,
			AskAlienToNumber = 3,
			AskAlienToCredits = 4,
			NoIdea = Int32.MaxValue,
		}

	}
}