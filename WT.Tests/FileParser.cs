using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		public void FileWithSpecifications()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt", "glob is I", "prok is V", "pish is X", "tegj is L");
			steps.WhenICallTheInterpreterForFile(@"C:\Temp\purple_towel.txt");
			steps.ThenIWillHaveTheseAnswers();
			
			steps.ThenIWillHaveTheseConversions(
				Tuple.Create("glob", "I"),
				Tuple.Create("prok", "V"),
				Tuple.Create("pish", "X"),
				Tuple.Create("tegj", "L")
			);
		}





	}
}
