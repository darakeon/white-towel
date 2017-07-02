using System.Globalization;
using System.Reflection;

namespace WT.Generics
{
	public class Instance
	{
		public static T Create<T>(params object[] args)
		{
			var type = typeof(T);

			return (T)type.Assembly.CreateInstance(
				type.FullName, 
				false, BindingFlags.CreateInstance, null, 
				args, CultureInfo.CurrentCulture, null);
		}
	}
}