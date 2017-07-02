using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WT.FileInterpreter;
using WT.Generics;

namespace WT.Tests
{
	[TestClass]
	public class RomanConversion
	{
		[TestMethod]
		public void OneChar()
		{
			AssertEqual("M", 1000);
			AssertEqual("D", 500);
			AssertEqual("C", 100);
			AssertEqual("L", 50);
			AssertEqual("X", 10);
			AssertEqual("V", 5);
			AssertEqual("I", 1);
		}



		[TestMethod]
		public void RepeatedTwice()
		{
			AssertEqual("MM", 2000);
			AssertEqual("DD", null);
			AssertEqual("CC", 200);
			AssertEqual("LL", null);
			AssertEqual("XX", 20);
			AssertEqual("VV", null);
			AssertEqual("II", 2);
		}

		[TestMethod]
		public void RepeatedThreeTimes()
		{
			AssertEqual("MMM", 3000);
			AssertEqual("DDD", null);
			AssertEqual("CCC", 300);
			AssertEqual("LLL", null);
			AssertEqual("XXX", 30);
			AssertEqual("VVV", null);
			AssertEqual("III", 3);
		}

		[TestMethod]
		public void RepeatedFourTimes()
		{
			AssertEqual("MMMM", null);
			AssertEqual("DDDD", null);
			AssertEqual("CCCC", null);
			AssertEqual("LLLL", null);
			AssertEqual("XXXX", null);
			AssertEqual("VVVV", null);
			AssertEqual("IIII", null);
		}



		[TestMethod]
		public void SimpleSubtraction()
		{
			AssertEqual("DM", null);
			AssertEqual("CM", 900);
			AssertEqual("CD", 400);
			AssertEqual("LC", null);
			AssertEqual("XC", 90);
			AssertEqual("XL", 40);
			AssertEqual("VX", null);
			AssertEqual("IX", 9);
			AssertEqual("IV", 4);
		}

		[TestMethod]
		public void ComplexSubtraction()
		{
			AssertEqual("MMMCM", 3900);
			AssertEqual("CCCXC", 390);
			AssertEqual("XXXIX", 39);

			AssertEqual("DCD", null);
			AssertEqual("LXL", null);
			AssertEqual("VIV", null);

			AssertEqual("DCM", null);
			AssertEqual("LXC", null);
			AssertEqual("VIX", null);

			AssertEqual("IVI", null);
			AssertEqual("IXI", null);
			AssertEqual("XLX", null);
			AssertEqual("XCX", null);
			AssertEqual("CDC", null);
			AssertEqual("CMC", null);
        }



		[TestMethod]
		public void Order()
		{
			AssertEqual("MDCLXVI", 1666);
			AssertEqual("IVXLCDM", null);

			AssertEqual("MDCLXV", 1665);
			AssertEqual("IVXLCD", null);

			AssertEqual("MDCLX", 1660);
			AssertEqual("IVXLC", null);

			AssertEqual("MDCL", 1650);
			AssertEqual("IVXL", null);

			AssertEqual("MDC", 1600);
			AssertEqual("IVX", null);

			AssertEqual("MD", 1500);
			AssertEqual("IV", 4);
		}




		private void AssertEqual(String roman, Int32? arabic)
		{
			Assert.AreEqual(arabic, RomanConversor.Convert(roman));
		}
	}
}