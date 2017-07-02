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
		private IDictionary<String, String> conversionDictionary;
		private IDictionary<String, Decimal> thingValueDictionary;


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
			conversionDictionary = interpreter.ConversionDictionary;
			thingValueDictionary = interpreter.ThingValueDictionary;
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
			thenIWillHaveThese(conversionDictionary, expectedConversions);
		}

		public void ThenIWillHaveTheseThingValues(params Tuple<String, Decimal>[] expectedThingValues)
		{
			thenIWillHaveThese(thingValueDictionary, expectedThingValues);
		}

		private static void thenIWillHaveThese<T>(IDictionary<String, T> received, Tuple<String, T>[] expected)
		{
			var message = String.Format(TestsErrors.ConversionCountError, expected.Length, received.Count);
			Assert.AreEqual(expected.Length, received.Count, message);

			for (var a = 0; a < expected.Length; a++)
			{
				var key = expected[a].Item1;
				var value = expected[a].Item2;

				message = String.Format(TestsErrors.ConversionNotFoundFor, key);
				Assert.IsTrue(received.ContainsKey(key), message);

				message = String.Format(TestsErrors.WrongConversionFor, key, value, received[key]);
				Assert.AreEqual(value, received[key], message);
			}
		}
		
		public void ThenIWillHaveNoConversions()
		{
			ThenIWillHaveTheseConversions();
		}

		public void ThenIWillHaveNoThingValues()
		{
			ThenIWillHaveTheseThingValues();
		}



	}
}