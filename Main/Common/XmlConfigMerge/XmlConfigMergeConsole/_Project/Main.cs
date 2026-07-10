using System;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

using Tools.XmlConfigMerge;

namespace Tools.XmlConfigMergeConsole
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class MainFunction
	{
		/// <summary>
		///	Merge one config files settings with another, and/or explicitly setting specified elements and/or attributes.
		/// </summary>
		/// <remarks>
		/// Usage:
		///		XmlConfigMerge masterConfigFile [-m mergeFromConfigFile] [-mwritten] [-r XPathRegexReplacement1] [-r XPathRegexReplacementN]
		///
		///		1) Any settings in optional mergeFromConfigFile are merged into masterConfigFile. Existing nodes / attributes are updated, otherwise nodes / attributes added.
		///		2) Optional list of XPathRegexReplacements applied to masterConfigFile
		///				Examples:
		///					-r //@value														QASERVER/QAVIRTDIR				http://(.*)/startpage.aspx	
		///					-r /configuration/appSettings/add[@key='OtherServerUrl']/@value	http://OTHERSERVER/SERVICE.ASHX
		///					-r //*[local-name()='bindingRedirect']/@newVersion				2.0.1.0
		///		Notes:
		///			x Changes are logged to console
		///			x A warning is logged if a mergeFromConfigFile is specified and it does not exist (since this is a likely scenario, e.g., a first-type push to a staging server).
		///			x If masterConfigFile does not exist, an error is returned.
		///			x if -mwritten is specified, the changes are written out to the mergeFromConfigFile. 
		///				Otherwise, they are written to the masterConfigFile.
		/// </remarks>
		[STAThread]
		static int Main(string[] args)
		{
			try 
			{
				//Parse cmd-line args
				ArrayList xPathRegexReplacements = new ArrayList();
				string masterConfigFile = null;
				string mergeFromConfigFile = null;
				bool mWrittenSpecified = false;
				
				const string MergeFromConfigFileFlag = "-m";
				const string XPathRegexReplacementFlag = "-r";
				const string MWrittenFlag = "-mwritten";

				int i = 0;
				while (i < args.Length) {
					string arg = args[i];
					
					switch (arg) {
						case MergeFromConfigFileFlag:
							if (mergeFromConfigFile != null) {
								throw new ApplicationException("More than one -m arg specified");
							}
							i++;
							if (i >= args.Length) {
								throw new ApplicationException(MergeFromConfigFileFlag + " requires following arg");
							}
							mergeFromConfigFile = args[i];
							i++;
							break;
						case XPathRegexReplacementFlag:
							i++;
							if (i >= args.Length) {							
								throw new ApplicationException(XPathRegexReplacementFlag + " requires at least 2 following args");
							}
							string xPath = args[i];
							i++;
							if (i >= args.Length) {							
								throw new ApplicationException(XPathRegexReplacementFlag + " requires at least 2 following args");
							}
							string replaceWith = args[i];
							i++;
							Regex replacePattern = null;
							if (i < args.Length) {
								if ( ! IsFlag(args[i] ) ) {
									replacePattern = new Regex(args[i], RegexOptions.IgnoreCase);
									i++;
								}
							}
							xPathRegexReplacements.Add(new XPathRegexReplacement(xPath, replaceWith, replacePattern) );
							break;	
						case MWrittenFlag:
							mWrittenSpecified = true;
							i++;
							break;
						default:
							if (masterConfigFile != null) {
								throw new ApplicationException("More than one masterConfigFile specified");
							}
							masterConfigFile = args[i];
							i++;
							break;
					}
				}				
				
				if (masterConfigFile == null) {
					throw new ApplicationException("Required masterConfigFile param not specified");
				}
				
				if (mWrittenSpecified && mergeFromConfigFile == null) {
					throw new ApplicationException("mergeFromConfigFile is required when " + MWrittenFlag + " is specified.");
				}
				
				//Merge mergeFromConfigFile if one specified
				bool mergedFromExists = (mergeFromConfigFile != null && File.Exists(mergeFromConfigFile) );
				ConfigFileManager config = new ConfigFileManager(masterConfigFile, mergeFromConfigFile,
						mWrittenSpecified);
				if (mergedFromExists) {
					Console.WriteLine("Merged existing '" + mergeFromConfigFile + "' settings with '" + masterConfigFile + "'");
				}

				//Process each specified XPathRegexReplacement
				foreach (XPathRegexReplacement repl in xPathRegexReplacements) {
					LogToConsole(config.ReplaceXPathValues(repl.XPath, repl.ReplaceWith, repl.ReplacePattern) );
				}
				
				//Save changes
				config.Save();
				if (mWrittenSpecified) {
					Console.WriteLine("Saved changes to '" + mergeFromConfigFile);
				} else {
					Console.WriteLine("Saved changes to '" + masterConfigFile);
				}
				return 0; //ok
			} 
			catch (Exception ex) {
				Console.WriteLine("**ERROR: " + ex.Message);
				return 1; //error
			}
		}
		
		private static bool IsFlag(string arg) {
			if (arg.Length >= "-x".Length) {
				if (arg.Substring(0, 1) == "-") {
					return true;
				}
			}
			
			return false;
		}
		
		private static void LogToConsole(string[] lines) {
			foreach (string line in lines) {
				Console.WriteLine(line);
			}
		}
	}
}
