using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WT.Tests
{
	[TestClass]
	public class FileQuestionsParser
	{
		public FileQuestionsParser()
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
		}



		[TestMethod]
		public void FileAskingJustForNumbers()
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
				, "how much is pish tegj glob glob ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("pish tegj glob glob is 42");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileAskingJustForNumbersWithWrongQuestion()
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
				, "how much is pish tegj pish glob ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Not in roman numbers format: 'XLXI'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileAskingJustForNumbersUnknownRoman()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "prok is V"
				, "pish is X"
				, "glob glob Silver is 34 Credits"
				, "glob prok Gold is 57800 Credits"
				, "pish pish Iron is 3910 Credits"
				, "how much is pish tegj glob glob ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Unknown alien number or thing: 'tegj'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}



		[TestMethod]
		public void FileAskingJustForCredit()
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
				, "how many Credits is glob prok Silver ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("glob prok Silver is 68 Credits");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileAskingJustForCreditWithWrongQuestion()
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
				, "how many Credits is glob tegj Silver ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Not in roman numbers format: 'IL'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileAskingJustForCreditUnknownRoman()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "pish is X"
				, "tegj is L"
				, "glob glob Silver is 34 Credits"
				, "pish pish Iron is 3910 Credits"
				, "how many Credits is glob prok Silver ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Unknown alien number or thing: 'prok'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Iron", 195.5m)
			);
		}

		[TestMethod]
		public void FileAskingJustForCreditUnknownThing()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt"
				, "glob is I"
				, "prok is V"
				, "pish is X"
				, "tegj is L"
				, "glob prok Gold is 57800 Credits"
				, "pish pish Iron is 3910 Credits"
				, "how many Credits is glob prok Silver ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers("Unknown alien number or thing: 'Silver'");

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}



		[TestMethod]
		public void FileAskingForNumberAndCredit()
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
				, "how much is pish tegj glob glob ?"
				, "how many Credits is glob prok Silver ?"
			);

			steps.WhenICallTheInterpreterForFile(@"towel.txt");

			steps.ThenIWillHaveTheseAnswers(
				"pish tegj glob glob is 42",
				"glob prok Silver is 68 Credits"
			);

			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);

			steps.ThenIWillHaveTheseThingValues(
				Tuple.Create("Silver", 17m),
				Tuple.Create("Gold", 14450m),
				Tuple.Create("Iron", 195.5m)
			);
		}

	}
}
