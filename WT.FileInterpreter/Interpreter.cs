using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WT.Resources;

namespace WT.FileInterpreter
{
    public class Interpreter
    {
	    private String fileName { get; set; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; } 
		internal IDictionary<String, Decimal> ValueThingDictionary { get; private set; } 



	    public Interpreter(String fileName)
	    {
		    this.fileName = fileName;
			ConversionDictionary = new Dictionary<String, String>();
			ValueThingDictionary = new Dictionary<String, Decimal>();
        }



	    public IList<String> Execute()
	    {
		    if (!File.Exists(fileName))
		    {
				return new List<String> { Messages.NotFound };
		    }

		    return translate();
	    }



	    private IList<String> translate()
	    {
		    var lines = File.ReadAllLines(fileName);

		    return lines
				.Select(translate)
				.Where(r => r != null)
				.ToList();
	    }

		private String translate(String line)
		{
			if (translateAlienToRoman(line))
				return null;

			return null;
		}

	    private Boolean translateAlienToRoman(string line)
	    {
		    var match = Regex.Match(line, FileTranslation.TranslateAlienToRoman);

		    if (!match.Success)
				return false;

		    var key = match.Groups[1].Value;
		    var value = match.Groups[2].Value;

		    ConversionDictionary.Add(key, value);

		    return true;
	    }
    }
}
