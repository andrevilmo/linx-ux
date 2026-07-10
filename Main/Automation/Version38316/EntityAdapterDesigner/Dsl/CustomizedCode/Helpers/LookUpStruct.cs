using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner.CustomizedCode;

namespace Linx.EntityAdapterDesigner
{
    
    /// <summary>
    /// Support for LookUps generation.
    /// </summary>
    public class LookUpStruct
    {
        public string Name { get; set; }
        public string SpecializedLookUp { get; set; }
        public bool IsMultiSelection { get; set; }
        public bool ReplaceAllOnClearState { get; set; }
        public string RelationName { get; set; }
        public string EntitySource { get; set; }
        public string NameSpace { get; set; }
        public string QueryReturnType { get; set; }
        public string SubQueryFields { get; set; }
        public string ClientFilters { get; set; }
        public bool ApplyClientFilterOnClear { get; set; }
        public bool CheckExistence { get; set; }

        public static List<LookUpStruct> GetLookUpStructures(Type dataType)
        {
            var lookupDict = new Dictionary<string, string>();

            if (dataType != null)
            {
                foreach (var member in dataType.GetProperties().OrderBy(e => e.Name))
                {
                    string relatedName = member.Name.PrepareName();
                    if (!lookupDict.ContainsKey(relatedName))
                    {
                        var value = Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(member, typeof(LinxPublicationFieldAttribute), "LookUpInfo") as string;
                        if (!value.IsNullOrEmpty())
                        {
                            lookupDict.Add(relatedName, value);
                        }
                    }
                }
            }

            return GetLookUpStructures(lookupDict, true);
        }


        public static List<LookUpStruct> GetModelViewLookUpStructures(EntityAdapter entity, List<PublicationEntity> sourceEntities)
        {
            var lookupDict = new Dictionary<string, string>();

            if (sourceEntities != null && sourceEntities.Count > 0)
            {
                foreach (var member in entity.EntityAdapterProperties.Where(e => !e.ModelViewSource.IsNullOrEmpty()).OrderBy(e => e.Name))
                {
                    string relatedName = member.Name.PrepareName();
                    if (!lookupDict.ContainsKey(relatedName))
                    {
                        var value = member.GetModelViewLookUpInfo(sourceEntities);
                        if (!value.IsNullOrEmpty())
                        {
                            lookupDict.Add(relatedName, value);
                        }
                    }
                }
            }

            return GetLookUpStructures(lookupDict, true);
        }

        public static List<LookUpStruct> GetLookUpStructures(Dictionary<string, string> lookUps, bool loadProperties)
        {
            List<LookUpStruct> result = new List<LookUpStruct>();

            foreach (string lookUpDef in lookUps.Select(e => e.Value.Right("::")).Distinct().ToArray())
            {
                string[] parts = lookUpDef.Split(new char[] { '#' });
                LookUpStruct lookUp = result.FirstOrDefault(e => e.Name == parts[0]);
                if (lookUp == null)
                {
                    lookUp = new LookUpStruct()
                    {
                        Name = parts[0],
                        SpecializedLookUp = parts[1],
                        IsMultiSelection = parts[2] == "true",
                        ReplaceAllOnClearState = parts[3] == "true",
                        RelationName = parts[4],
                        EntitySource = parts[5],
                        NameSpace = parts[6],
                        QueryReturnType = parts[7],
                        SubQueryFields = (parts.Length > 8 ? parts[8] : ""),
                        ClientFilters = (parts.Length > 9 ? parts[9] : ""),
                        ApplyClientFilterOnClear = (parts.Length > 10 ? parts[10] == "true" : false),
                        CheckExistence = (parts.Length > 11 ? parts[11] == "true" : false)
                    };
                }

                if (lookUp.RelationName.IsNullOrEmpty())
                    lookUp.RelationName = lookUp.EntitySource;

                if (loadProperties)
                {
                    int order = 0;
                    foreach (string lookUpDefProp in lookUps.Where(e => e.Value.Right("::") == lookUpDef).Select(e => e.Key + "#" + e.Value.Left("::")).ToArray())
                    {
                        string[] propParts = lookUpDefProp.Split(new char[] { '#' });
                        LookUpStructProperty lookUpProp = new LookUpStructProperty()
                        {
                            EntityPropertyRelated = ("=" + propParts[0]).Right("="),
                            Datatype = propParts[1],
                            Name = propParts[2],
                            IsPrimaryKey = propParts[3] == "true",
                            Suffix = propParts[4],
                            Precision = (propParts.Length > 5 ? propParts[5] : ""),
                            DomainName = (propParts.Length > 6 ? propParts[6] : ""),
                            DisplayName = (propParts.Length > 7 ? propParts[7] : ""),
                            Order = (propParts.Length > 8 ? int.Parse(propParts[8]) : order),
                            IsBrowsable = (propParts.Length > 9 ? propParts[9] != "false" : true),
                            SubstituteProperties = (propParts.Length > 10 ? propParts[10] : ""),
                            DependencyProperty = (propParts.Length > 11 ? propParts[11] : "")
                        };
                        lookUp.Properties.Add(lookUpProp);
                    }
                    order++;
                }

                result.Add(lookUp);
            }

            return result;
        }

        public string GetDomainService()
        {
            return this.NameSpace + "." + this.NameSpace.Right(".") + "DomainService";
        }

        public string GetDomainContext()
        {
            return this.NameSpace + "." + this.NameSpace.Right(".") + "DomainContext";
        }

        public string GetLookUpReplaces(string indent, EntityAdapter entityAdapter)
        {
            string body = "\r\n" + indent + entityAdapter.Name + " replaceTo = this;";
            body += "\r\n" + indent + "foreach(var selectedElement in selectedElements)";
            body += "\r\n" + indent + "{";
            body += "\r\n" + indent + indent + @"if (replaceTo == null)";
            if (entityAdapter.TargetEntityAdapter == null)
                body += "\r\n" + indent + indent + @"   break;";
            else
            {
                body += "\r\n" + indent + indent + @"{";
                body += "\r\n" + indent + indent + @"     replaceTo = new " + entityAdapter.Name + "();";
                body += "\r\n" + indent + indent + @"     replaceTo.CopyInstanceFrom(this);";
                body += "\r\n" + indent + indent + @"     this." + entityAdapter.TargetEntityAdapter.Name + "." + entityAdapter.Name + "List.Add(replaceTo);";
                body += "\r\n" + indent + indent + @"     replaceTo.OnAfterAdd();";
                body += "\r\n" + indent + indent + @"}";
            }


            foreach (var property in this.Properties)
            {
                if (!this.ReplaceAllOnClearState && !property.IsPrimaryKey)
                {
                    body += "\r\n" + indent + indent + @"if (propertyName.IsNullOrEmpty() || propertyName == """ + property.EntityPropertyRelated + @""")";
                    body += "\r\n" + indent + indent + @"{";
                }

                body += "\r\n" + indent + indent + indent + @"if (selectedElement.ExistsProperty(""" + property.Name + @"""))";
                body += "\r\n" + indent + indent + indent + @"     replaceTo.SetPropertyValue(""" + property.EntityPropertyRelated + @""", selectedElement.GetPropertyValue(""" + property.Name + @"""));";


                if (!property.Suffix.IsNullOrEmpty() && property.Name.Length > property.Suffix.Length && property.Name.Right(property.Suffix.Length) == property.Suffix)
                {
                    body += "\r\n" + indent + indent + indent + @"else if (selectedElement.ExistsProperty(""" + property.Name.Left(property.Name.Length - property.Suffix.Length) + @"""))";
                    body += "\r\n" + indent + indent + indent + @"     replaceTo.SetPropertyValue(""" + property.EntityPropertyRelated + @""", selectedElement.GetPropertyValue(""" + property.Name.Left(property.Name.Length - property.Suffix.Length) + @"""));";
                }

                if (!this.ReplaceAllOnClearState && !property.IsPrimaryKey)
                {
                    body += "\r\n" + indent + indent + @"}";
                }

            }

            string lookupedName = this.GetEntityOnLookedUp(entityAdapter);
            if (!lookupedName.IsNullOrEmpty())
                body += "\r\n" + indent + indent + @"replaceTo." + lookupedName + "(selectedElement);";

            body += "\r\n" + indent + indent + @"replaceTo = null;";
            body += "\r\n" + indent + "}";

            return body;
        }

        public string GetEntityOnLookedUp(EntityAdapter entityAdapter)
        {
            string result = String.Empty;
            if (entityAdapter != null)
            {
                if (entityAdapter.ExistsEvent("OnLookedUp" + this.Name))
                    result = "OnLookedUp" + this.Name;
                else if (!this.RelationName.IsNullOrEmpty() && entityAdapter.ExistsEvent("OnLookedUp" + this.RelationName))
                    result = "OnLookedUp" + this.RelationName;
                else if (!this.EntitySource.IsNullOrEmpty() && entityAdapter.ExistsEvent("OnLookedUp" + this.EntitySource))
                    result = "OnLookedUp" + this.EntitySource;
                else if (entityAdapter.ExistsEvent("OnLookUped" + this.Name))
                    result = "OnLookUped" + this.Name;
                else if (!this.RelationName.IsNullOrEmpty() && entityAdapter.ExistsEvent("OnLookUped" + this.RelationName))
                    result = "OnLookUped" + this.RelationName;
                else if (!this.EntitySource.IsNullOrEmpty() && entityAdapter.ExistsEvent("OnLookUped" + this.EntitySource))
                    result = "OnLookUped" + this.EntitySource;
            }

            return result;
        }

        public string GetLookUpForClear(string indent)
        {
            string body = "";

            foreach (var property in this.Properties)
            {
                body += "\r\n" + indent + indent + "this." + property.EntityPropertyRelated + " = default(" + property.Datatype + ");";
            }

            return body;
        }

        private List<LookUpStructProperty> _properties;
        public List<LookUpStructProperty> Properties
        {
            get
            {
                if (_properties == null)
                    _properties = new List<LookUpStructProperty>();
                return _properties;
            }
        }

        internal string GetQueryGroupColumns(string propertyName)
        {
            string result = String.Empty;

            if (!this.SubQueryFields.IsNullOrEmpty())
            {
                var qFields = this.SubQueryFields.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var fieldListDef in qFields)
                {
                    if (fieldListDef.Left("[").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(propertyName))
                    {
                        result = fieldListDef.Extract("[", "]");
                        break;
                    }
                }
            }

            return result;
        }

        internal string GetSubQueryClientFilters(string propertyName)
        {
            string result = String.Empty;

            if (!this.ClientFilters.IsNullOrEmpty())
            {
                var cFilterss = this.ClientFilters.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var fieldListDef in cFilterss)
                {
                    if (fieldListDef.Left("[") == propertyName)
                    {
                        result = fieldListDef.Extract("[", "]");
                        break;
                    }
                }
            }
            return result;
        }

    }

    public class LookUpStructProperty
    {
        public string Name { get; set; }
        public string EntityPropertyRelated { get; set; }
        public string Datatype { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string Suffix { get; set; }
        public string Precision { get; set; }
        public string DomainName { get; set; }
        public string DisplayName { get; set; }
        public int Order { get; set; }
        public bool IsBrowsable { get; set; }
        public string SubstituteProperties { get; set; }
        public string DependencyProperty { get; set; }

        public string GetDataFormatString()
        {
            if (!this.Datatype.Contains("[]") && this.Datatype.ToLower().Contains("datetime"))
                return "d";

            if (!this.Datatype.Contains("[]") && (this.Datatype.ToLower().Contains("decimal") || this.Datatype.ToLower().Contains("float") || this.Datatype.ToLower().Contains("double")))
                return "N" + (this.Precision.Right(":").IsNullOrEmpty() ? "0" : this.Precision.Right(":"));

            return String.Empty;
        }
    }



}
