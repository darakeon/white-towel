using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WT.Resources;

namespace WT.Tests
{
	[TestClass]
	public class FileParser
	{
		[TestMethod]
		public void FileEmpty()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt");
			steps.WhenICallTheInterpreterForFile("towel.txt");
			steps.ThenIWillHaveTheseAnswers();
		}



		[TestMethod]
		public void FileWithRomanSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "prok is V"
				, "pish is X"
				, "tegj is L"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers();

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);
		}



		[TestMethod]
		public void FileWithDuplicateRomanSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "glob is I"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			var answer = String.Format(Messages.AlreadyStoredConversion, "glob", "I");
			steps.ThenIWillHaveTheseAnswers(answer);

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I")
			);
		}



		[TestMethod]
		public void FileWithChangeInRomanSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "glob is L"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			var answer = String.Format(Messages.DuplicatedConversion, "glob", "L", "I");
			steps.ThenIWillHaveTheseAnswers(answer);

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I")
			);
		}





	}
}
