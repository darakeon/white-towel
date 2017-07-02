using System;
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
			Console.ForegroundColor = ConsoleColor.White;

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
				foreach (var message in result)
				{
					Console.ForegroundColor = getColor(message.Type);
					Console.WriteLine(message);
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine(endFileAnswer);


				Console.WriteLine();
				Console.WriteLine(Messages.ProgramTerminateOrRunAgain);
				answer = Console.ReadLine();

				Console.Clear();

			} while (answer == rightAnswer);


		}

		private static ConsoleColor getColor(Message.MessageType type)
		{
			switch (type)
			{
				case Message.MessageType.Info:
					return ConsoleColor.Green;
				case Message.MessageType.Warning:
					return ConsoleColor.Yellow;
				case Message.MessageType.Error:
					return ConsoleColor.Red;
				default:
					return ConsoleColor.Black;
			}
		}


	}
}
