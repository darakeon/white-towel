using System;
using System.Text.RegularExpressions;
using WT.Generics;
using WT.Resources;

namespace WT.FileInterpreter.LineTranslators
{
	public abstract class BaseLineTranslator
	{
		protected BaseLineTranslator(Interpreter interpreter, TranslatorType type)
		{
			Interpreter = interpreter;
			Type = type;
		}

		protected Interpreter Interpreter { get; private set; }
		protected Match Match { get; set; }
		public TranslatorType Type { get; private set; }

		

		public abstract void Translate();



		public static BaseLineTranslator GetTranslator(Interpreter interpreter, String line)
		{
			return getTranslator<LineTranslatorAlienToRoman>(interpreter, line)
				?? getTranslator<LineTranslatorAlienToCredits>(interpreter, line);
		}

		private static BaseLineTranslator getTranslator<T>(Interpreter interpreter, String line)
			where T : BaseLineTranslator
		{
			var translator = Instance.Create<T>(interpreter);
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
			AskAlienToCredits,
			AskAlienToNumber,
			TranslateAlienToCredits,
			TranslateAlienToRoman,
		}


	}
}