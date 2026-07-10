using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TelerikReport.Publish
{
    public class Utils
    {
        public static XmlDocument GetReport(string path)
        {
            var document = new XmlDocument();

            document.Load(path);
            Utils.RemoveParameters(document);

            return document;
        }

        private static void RemoveParameters(XmlDocument report)
        {
            var toRemove = new List<XmlNode>();
            var parameters = report.GetElementsByTagName("ReportParameter");            

            foreach (XmlNode item in parameters)
            {
                var attributeName = item.Attributes["Name"].InnerText.ToLower();

                if (attributeName == "username" || attributeName == "password")
                    toRemove.Add(item);
            }

            foreach (var item in toRemove)
                report["Report"]["ReportParameters"].RemoveChild(item);
        }
    }
}
