using System;
using System.Collections.Generic;
using WT.Resources;

namespace WT.Generics
{
	public static class DictionaryMessageForRepetitionExtension
	{
		public static String HandleKeyValue<TK, TV>(this IDictionary<TK, TV> dic, TK key, TV value)
		{
			if (dic.ContainsKey(key))
			{
				return getRepeatedKeyMessage(key, value, dic[key]);
			}
			
			dic.Add(key, value);

			return null;
		}

		private static String getRepeatedKeyMessage<TK, TV>(TK key, TV ignoredValue, TV storedValue)
		{
			var format = ignoredValue.Equals(storedValue)
				? Messages.AlreadyStoredConversion
				: Messages.DuplicatedConversion;

			var message = String.Format(format, key, ignoredValue, storedValue);
			
			return message;
		}



	}
}