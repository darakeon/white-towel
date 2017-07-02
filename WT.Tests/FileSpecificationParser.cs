using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WT.Tests
{
	[TestClass]
	public class FileSpecificationParser
	{
		public FileSpecificationParser()
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
		}

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

			steps.ThenIWillHaveTheseAnswers(@"Value 'I' for 'glob' already stored");

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

			steps.ThenIWillHaveTheseAnswers(@"Value 'L' for 'glob' ignored (kept as 'I')");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I")
			);
		}



		[TestMethod]
		public void FileWithCreditSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "Silver is 17 Credits"
				, "Gold is 14450 Credits"
				, "Iron is 195.5 Credits"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers();

			steps.ThenIWillHaveTheseThingsValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileWithDuplicateCreditSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "Silver is 17 Credits"
				, "Silver is 17 Credits"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Value '17' for 'Silver' already stored");

			steps.ThenIWillHaveTheseThingsValues(
				Tuple.Create("Silver", 17m)
			);
		}

		[TestMethod]
		public void FileWithChangeInCreditSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "Silver is 17 Credits"
				, "Silver is 27 Credits"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Value '27' for 'Silver' ignored (kept as '17')");

			steps.ThenIWillHaveTheseThingsValues(
				Tuple.Create("Silver", 17m)
			);
		}



		[TestMethod]
		public void FileWithWrongAlienRomanCreditSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "tegj is L"
				, "glob tegj Silver is 34 Credits"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Unknown alien to roman conversion: 'IL'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveNoThingValues();
		}



		[TestMethod]
		public void FileWithAlienRomanCreditSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "prok is V"
				, "pish is X"
				, "tegj is L"
				, "glob glob Silver is 34 Credits"
				, "glob prok Gold is 57800 Credits"
				, "pish pish Iron is 3910 Credits"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers();

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingsValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

	}
}
