using System;
using System.Text.RegularExpressions;

namespace Tools.XmlConfigMergeConsole
{
	/// <summary>
	/// Summary description for XPathRegexReplacements.
	/// </summary>
	public class XPathRegexReplacement
	{
		public XPathRegexReplacement(string xPath, string replaceWith, Regex replacePattern)
		{
			XPath = xPath;
			ReplaceWith = replaceWith;
			ReplacePattern = replacePattern;
		}

		public string XPath = null;
		public string ReplaceWith = null;
		public Regex ReplacePattern = null;
	}
}
