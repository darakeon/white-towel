using System;
using System.Collections.Generic;
using System.Linq;

namespace WT.Generics
{
	public class RomanConversor
	{
		private static readonly IDictionary<Char, Int32> romanDictionary =
			new Dictionary<Char, Int32>
			{
				{'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
			};

		private readonly Int32? arabic;
		private List<Int32?> numbers = new List<Int32?>();

		private Int32 last => numbers.Count - 1;


		public static Int32? Convert(String roman)
		{
			var romanConversor = new RomanConversor(roman);

			return romanConversor.arabic;
		}



		private RomanConversor(String roman)
		{
			makeNumbers(roman);

			if (numbers != null)
				arabic = convert();
		}



		private void makeNumbers(string roman)
		{
			foreach (var digit in roman)
			{
				if (!romanDictionary.ContainsKey(digit))
				{
					numbers = null;
					return;
				}

				numbers.Add(romanDictionary[digit]);
			}
		}


		private Int32? convert()
		{
			var count5 = numbers.Count(n => n == 5);
			var count50 = numbers.Count(n => n == 50);
			var count500 = numbers.Count(n => n == 500);

			if (count5 > 1 || count50 > 1 || count500 > 1)
				return null;

			var lastNumber = Int32.MaxValue;

			for (var n = 0; n < numbers.Count; n++)
			{
				if (numbers[n] == 0)
				{
					continue;
				}

				var factor = ifTwoMeansOneGetFactor(n);

				if (factor.HasValue)
				{
					numbers[n] *= factor.Value;
					numbers[n + 1] = 0;
				}
				else
				{
					numbers[n] = sumNext(n, 1);

					if (numbers[n] == null)
						return null;
				}

				if (lastNumber < numbers[n] || lastNumber / 4m == numbers[n] || lastNumber / 9m == numbers[n])
					return null;

				if (numbers[n].HasValue)
					lastNumber = numbers[n].Value;
			}

			var result = numbers.Where(n => n != 0).ToList();

			return result.Sum();
		}



		private Int32? ifTwoMeansOneGetFactor(Int32 n)
		{
			var notTheLast = n != last;
			var number = numbers[n];
			var allowedToSubtract = number == 1 || number == 10 || number == 100;

			if (notTheLast && allowedToSubtract)
			{
				var next = numbers[n + 1];

				if (number * 5 == next)
				{
					return 4;
				}
				
				if (number * 10 == next)
				{
					return 9;
				}
			}

			return null;
		}



		private Int32? sumNext(Int32 index, Int32 times)
		{
			var result = numbers[index];
			var nextIndex = index + 1;

			if (nextIndex <= last && numbers[index] == numbers[nextIndex])
			{
				if (times == 3 || forbiddenRepeat(index))
					return null;

				var next = sumNext(nextIndex, times + 1);

				if (next == null)
					return null;

				result += next;
				numbers[nextIndex] = 0;
			}

			return result;
		}

		private bool forbiddenRepeat(Int32 position)
		{
			return new List<Int32?> { 5, 50, 500 }
				.Contains(numbers[position]);
		}


	}
}