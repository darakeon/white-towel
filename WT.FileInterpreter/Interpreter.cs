using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WT.Resources;

namespace WT.FileInterpreter
{
    public class Interpreter
    {
	    private String fileName { get; set; }

		internal IDictionary<String, String> ConversionDictionary { get; private set; } 



	    public Interpreter(String fileName)
	    {
		    this.fileName = fileName;
			ConversionDictionary = new Dictionary<String, String>();
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
			return null;
		}



	}
}
