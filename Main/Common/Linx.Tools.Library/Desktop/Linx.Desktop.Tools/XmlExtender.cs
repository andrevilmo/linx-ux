using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Linx.Tools
{
	#region Xml Extender
	public static class XmlExtender
	{

		public static string GetNameSpaceFromType(this System.Xml.Linq.XElement xmlExpr, Type type)
		{
			System.Xml.Linq.XElement element;
			string[] nsParts;
			string entitiesNS = String.Empty;

			//Get NameSpace to change
			foreach (var item in xmlExpr.DescendantNodesAndSelf())
			{
				element = item as System.Xml.Linq.XElement;
				if (element != null && element.NodeType.ToString() == "Element")
				{
					if (element.HasAttributes)
					{
						foreach (System.Xml.Linq.XAttribute attr in element.Attributes()) // Read attributes
						{
							nsParts = attr.Value.Split(new char[] { '.' });
							if (entitiesNS == String.Empty && nsParts.Length > 0 && nsParts[nsParts.Length - 1] == type.Name)
							{
								entitiesNS = (attr.Value.Trim() + ".").Replace("." + nsParts[nsParts.Length - 1] + ".", "");
								return entitiesNS;
							}
						}
					}

					if (element.HasElements)
					{
						foreach (System.Xml.Linq.XElement itemElement in element.Elements()) // Read attributes
						{
							entitiesNS = GetNameSpaceFromType(itemElement, type);
							if (!entitiesNS.IsNullOrEmpty())
								return entitiesNS;
						}
					}
				}
			}

			return entitiesNS;
		}

		public static void ReplaceNameSpaceFromType(this System.Xml.Linq.XElement xmlExpr, Type type)
		{
			ReplaceNameSpaceFromType(xmlExpr, type, "");
		}

		public static void ReplaceNameSpaceFromType(this System.Xml.Linq.XElement xmlExpr, Type type, string entitiesNS)
		{
			if (entitiesNS.IsNullOrEmpty())
				entitiesNS = GetNameSpaceFromType(xmlExpr, type); ;

			//Changing the NameSpace
			if (entitiesNS != String.Empty)
			{
				string literalElement = xmlExpr.ToString();
				literalElement = literalElement.Replace("[" + entitiesNS + ".", "[" + type.Namespace + ".");
				literalElement = literalElement.Replace(@"""" + entitiesNS + ".", @"""" + type.Namespace + ".");
				System.Xml.Linq.XElement result = System.Xml.Linq.XElement.Parse(literalElement);
				xmlExpr.RemoveNodes();
				foreach (var node in result.Nodes())
					xmlExpr.Add(node);
			}
		}
	}
	#endregion Xml Extender
}
