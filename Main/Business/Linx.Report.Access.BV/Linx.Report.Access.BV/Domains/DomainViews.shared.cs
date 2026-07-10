	

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Report.Access.BV.Domains
{

	public partial class DomainHelper
    {
		public static string[] GetDomainsInfo(string domainNames)
        {
            List<string> result = new List<string>();

            foreach (string domainName in domainNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var values = GetDomainValues(domainName);
                if (values.Count > 0)
                {
                    foreach(var value in values)
                    {
                        result.Add(domainName + "#" + value.Key + "#" + value.Value.Replace("\"", "").Replace("'", ""));
                    }
                }
            }

            return result.ToArray();
        }

		public static Dictionary<string, Dictionary<string, string>> GetAllDomainsInfo()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> values;
            return result;
        }

        public static Dictionary<string, string> GetDomainValues(string domainName)
        {
            Dictionary<string, string> result;
            switch (domainName)
            {


                default:
                    result = new Dictionary<string, string>();
                    break;
            }

            return result;
        }
    }


}