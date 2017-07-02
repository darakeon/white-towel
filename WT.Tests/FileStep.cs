using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WT.FileInterpreter;
using WT.Resources;

namespace WT.Tests
{
	public class FileStep
	{
		private IList<String> result;


		public void GivenIDontHaveThisFile(String fileName)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
		}

		public void GivenIHaveThisFile(String fileName, String fileContent)
		{
			File.WriteAllText(fileName, fileContent);
		}



		public void WhenICallTheInterpreterForFile(String fileName)
		{
			var interpreter = new Interpreter(fileName);

			result = interpreter.Execute();
		}



		public void ThenIWillHaveTheseAnswers(params String[] answers)
		{
			var message = String.Format(TestsErrors.AnswerCountError, answers.Length, result.Count);
			Assert.AreEqual(answers.Length, result.Count, message);

			for (var a = 0; a < answers.Length; a++)
			{
				var messageLine = String.Format(TestsErrors.AnswerLineError, a, answers[a], result[a]);
                Assert.AreEqual(answers[a], result[a], messageLine);
			}
		}


	}
}