using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			steps.ThenIWillHaveTheseAnswers("File does not exist");
		}



		[TestMethod]
		public void FileTowelEmpty()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile("towel.txt");
			steps.WhenICallTheInterpreterForFile("towel.txt");
			steps.ThenIWillHaveTheseAnswers();
		}



		[TestMethod]
		public void OtherFileEmpty()
		{
			var steps = new FileStep();

			steps.GivenIHaveThisFile(@"C:\Temp\purple_towel.txt");
			steps.WhenICallTheInterpreterForFile(@"C:\Temp\purple_towel.txt");
			steps.ThenIWillHaveTheseAnswers();
		}





	}
}
