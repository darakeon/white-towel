using System;
using System.Linq;
using WT.FileInterpreter;
using WT.Resources;

namespace WT.Presentation
{
	public class Program
	{
		private const String defaultFilename = "towel.txt";
		private const String rightAnswer = "42";
		private const String startFileAnswer = "================================================ start";
		private const String endFileAnswer = "================================================== end";

		public static void Main(string[] args)
		{
			String answer;
			Console.ForegroundColor = ConsoleColor.Green;

			do
			{
				Console.WriteLine(Messages.ProgramInitial, defaultFilename);
				var filename = Console.ReadLine();

				if (String.IsNullOrEmpty(filename))
				{
					filename = defaultFilename;
				}


				var interpreter = new Interpreter(filename);
				var result = interpreter.Execute();

				Console.WriteLine(startFileAnswer);
				result.ToList().ForEach(Console.WriteLine);
				Console.WriteLine(endFileAnswer);

				Console.WriteLine();
				Console.WriteLine(Messages.ProgramTerminateOrRunAgain);
				answer = Console.ReadLine();

				Console.Clear();

			} while (answer == rightAnswer);


		}
	}
}
