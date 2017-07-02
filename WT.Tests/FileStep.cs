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
		private IDictionary<string, string> conversions;


		public void GivenIDontHaveThisFile(String fileName)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
		}

		public void GivenIHaveThisFile(String fileName, params String[] fileContent)
		{
			File.WriteAllLines(fileName, fileContent);
		}



		public void WhenICallTheInterpreterForFile(String fileName)
		{
			var interpreter = new Interpreter(fileName);

			result = interpreter.Execute();
			conversions = interpreter.ConversionDictionary;
		}



		public void ThenIWillHaveTheseAnswers(params String[] answers)
		{
			var message = String.Format(TestsErrors.AnswerCountError, answers.Length, result.Count);
			Assert.AreEqual(answers.Length, result.Count, message);

			for (var a = 0; a < answers.Length; a++)
			{
				message = String.Format(TestsErrors.AnswerLineError, a, answers[a], result[a]);
				Assert.AreEqual(answers[a], result[a], message);
			}
		}

		public void ThenIWillHaveTheseConversions(params Tuple<String, String>[] expectedConversions)
		{
			var message = String.Format(TestsErrors.ConversionCountError, expectedConversions.Length, conversions.Count);
			Assert.AreEqual(expectedConversions.Length, conversions.Count, message);

			for (var a = 0; a < expectedConversions.Length; a++)
			{
				var key = expectedConversions[a].Item1;
				var value = expectedConversions[a].Item2;

				message = String.Format(TestsErrors.ConversionNotFoundFor, key);
				Assert.IsTrue(conversions.ContainsKey(key), message);

				message = String.Format(TestsErrors.WrongConversionFor, key, value, conversions[key]);
				Assert.AreEqual(value, conversions[key], message);
			}
		}


	}
}