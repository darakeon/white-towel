using Microsoft.VisualStudio.TestTools.UnitTesting;
using WT.Resources;

namespace WT.Tests
{
	[TestClass]
	public class FileReader
	{
		[TestMethod]
		public void FileDoNotExists()
		{
			var steps = new FileStep();

			steps.GivenIDontHaveThisFile("towel.txt");
			steps.WhenICallTheInterpreterForFile("towel.txt");
			steps.ThenIWillHaveTheseAnswers(Messages.NotFound);
		}



		[TestMethod]
		public void FileTowelEmpty()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt", "");
			steps.WhenICallTheInterpreterForFile("towel.txt");
			steps.ThenIWillHaveTheseAnswers();
		}



		[TestMethod]
		public void OtherFileEmpty()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile(@"C:\Temp\purple_towel.txt", "");
			steps.WhenICallTheInterpreterForFile(@"C:\Temp\purple_towel.txt");
			steps.ThenIWillHaveTheseAnswers();
		}





	}
}
