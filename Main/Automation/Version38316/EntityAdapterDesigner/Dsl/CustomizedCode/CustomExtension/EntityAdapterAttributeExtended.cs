using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterAttribute : DslModeling::ModelElement
    {

        public static string GetEnumDefinitions(string indent, string targetNameSpace, string attrName, string domainName, string kpiName, string displayName)
        {
            string body = "";
            
            if (!domainName.IsNullOrEmpty())
            {
                body += "\r\n" + indent + "public Dictionary<string, string> Get" + attrName + "Values()";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "return " + targetNameSpace + ".Domains." + domainName + ".GetValues();";
                body += "\r\n" + indent + "}";
                
                body += "\r\n" + indent + "[DataMember(IsRequired = false, Name = \"" + attrName + "Name\", EmitDefaultValue = true)]";
	            body += "\r\n" + indent + "[XmlAttribute()]";
	            body += "\r\n" + indent + "[Editable(false)]";
                body += "\r\n" + indent + "[Display(Name = \"" + displayName + "\", Description=\"\", Order = 0, AutoGenerateField = true, GroupName=\"\", ResourceType= null)]";
	            body += "\r\n" + indent + "[RoundtripOriginal()]";
	            body += "\r\n" + indent + "[FunctionalPoint(\"Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]\")]";
	            body += "\r\n" + indent + "[LinxPublicationField(IsSuggestion=false, LookUpInfo=\"\", EdmKey=\"\")]";
                body += "\r\n" + indent + "public string " + attrName + "Name";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "get { if (this." + attrName + ".IsNullOrEmpty()) return String.Empty; string key = this." + attrName + ".ToString(); var dmValues = this.Get" + attrName + "Values(); return (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); }";
                body += "\r\n" + indent + "}";
            }

            if (!kpiName.IsNullOrEmpty())
            {
                body += "\r\n" + indent + "private static KpiInfo _" + attrName + "KPI;";
                body += "\r\n" + indent + "public static KpiInfo Get" + attrName + "KPI()";
                body += "\r\n" + indent + "{";
                body += "\r\n" + indent + indent + "if (_" + attrName + "KPI == null)";
                body += "\r\n" + indent + indent + "    _" + attrName + "KPI = new " + targetNameSpace + ".KPIs." + kpiName + "();";
                body += "\r\n" + indent + indent + "return _" + attrName + "KPI;";
                body += "\r\n" + indent + "}";
            }

            return body;
        }


        public string GetLookUpName()
        {
            EntityAdapter entity = null;
            if (this is EntityAdapterProperty)
                entity = ((EntityAdapterProperty)this).EntityAdapter;
            else if (this is EntityAdapterPublicationProperty)
                entity = ((EntityAdapterPublicationProperty)this).EntityAdapter;

            if (entity != null)
            {
                var lookUpsPropInfo = entity.GetAllLookUpPropertiesInfo(true);
                return LookUpAdapter.GetLookUpName(lookUpsPropInfo.ContainsKey(this.Name) ? lookUpsPropInfo[this.Name] : String.Empty);
            }
            else
                return String.Empty;
        }

        public string GetSubstituteProperties()
        {
            EntityAdapter entity = null;
            if (this is EntityAdapterProperty)
                entity = ((EntityAdapterProperty)this).EntityAdapter;
            else if (this is EntityAdapterPublicationProperty)
                entity = ((EntityAdapterPublicationProperty)this).EntityAdapter;

            if (entity != null)
            {
                var lookUpsPropInfo = entity.GetAllLookUpPropertiesInfo(true);
                return LookUpAdapter.GetSubstituteProperties(lookUpsPropInfo.ContainsKey(this.Name) ? lookUpsPropInfo[this.Name] : String.Empty);
            }
            else
                return String.Empty;
        }

        public string GetEdmPath(bool original = false)
        {
            string edmKey;

            if (this is EntityAdapterFormula)
                edmKey = ((EntityAdapterFormula)this).LinqDefinition;
            else if (this is EntityAdapterProperty)
                edmKey = (((EntityAdapterProperty)this).EntityAdapter.IsModelView ? ((EntityAdapterProperty)this).EntityAdapter.PrimaryEntity + "." + this.Name : ((EntityAdapterProperty)this).EdmKey);
            else
                edmKey = ((EntityAdapterPublicationProperty)this).EdmKey;

            return (original ? edmKey : MacroEngine.ReplaceMacros(edmKey, true));
        }

        public string GetModelViewSource()
        {
            if (this is EntityAdapterProperty)
            {
                var prop = (EntityAdapterProperty)this;
                if (prop.EntityAdapter.IsModelView)
                    return prop.ModelViewSource.Left("(");
                else
                    return "";
            }
            else 
                return "";
        }

        public DisplayControlType GetDisplayControlClass()
        {
            if (!this.Datatype.Contains("[]"))
            {
                if (this.IsFK || !this.LookUpSubscription.IsNullOrEmpty())
                    return DisplayControlType.LookUpTextBox;

                if (this is EntityAdapterProperty && ((EntityAdapterProperty)this).EntityAdapter.IsModelView && ((EntityAdapterProperty)this).EntityAdapter.GetCurrentDataModel() == null && ((EntityAdapterProperty)this).ModelViewSource.Left(".") != ((EntityAdapterProperty)this).EntityAdapter.PrimaryEntity)
                    return DisplayControlType.LookUpTextBox;

                if (this.Datatype.Contains("Boolean"))
                    return DisplayControlType.CheckBox;

                if (this.Datatype.Contains("DateTime"))
                    return DisplayControlType.DateTimeTextBox;

                if (this.Datatype.ToLower().Contains("byte") ||
                    this.Datatype.ToLower().Contains("int16") ||
                    this.Datatype.ToLower().Contains("int32") ||
                    this.Datatype.ToLower().Contains("int64") ||
                    this.Datatype.ToLower().Contains("sbyte") ||
                    this.Datatype.ToLower().Contains("uint16") ||
                    this.Datatype.ToLower().Contains("uint32") ||
                    this.Datatype.ToLower().Contains("uint64") ||
                    this.Datatype.ToLower().Contains("single") ||
                    this.Datatype.ToLower().Contains("double") ||
                    this.Datatype.ToLower().Contains("decimal"))
                    return DisplayControlType.NumericTextBox;

                if (this.Datatype.ToLower().Contains("string") && (!this.Precision.Left(":").IsNullOrEmpty() && (int.Parse(this.Precision.Left(":")) == 0 || (int.Parse(this.Precision.Left(":")) > 255))))
                    return DisplayControlType.EditBox;

            }
            return DisplayControlType.TextBox;
        }

        public void RemoveKpiPartner(string attributeName = "")
        {
            if (String.IsNullOrWhiteSpace(attributeName))
                attributeName = this.Name;

            EntityAdapter adapter = null;
            if (this is EntityAdapterProperty)
                adapter = ((EntityAdapterProperty)this).EntityAdapter;
            else if (this is EntityAdapterFormula)
                adapter = ((EntityAdapterFormula)this).EntityAdapter;

            if (adapter != null)
            {
                if (!String.IsNullOrWhiteSpace(attributeName))
                {
                    EntityAdapterFormula formula = adapter.GetAllAttributes().Where(e => e.KpiRelatedAttribute == attributeName).FirstOrDefault() as EntityAdapterFormula;
                    if (formula != null)
                        adapter.EntityAdapterFormulas.Remove(formula);
                }
            }

        }

        public string GetEdmEntityName()
        {
            string entityName = String.Empty;
            string edmPath = GetEdmPath(true);
            string[] parts = edmPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                if (this is EntityAdapterFormula)
                    entityName = ((EntityAdapterFormula)this).EntityAdapter.GetEntityNameByRelation(parts[parts.Length - 2]);
                else if (this is EntityAdapterProperty)
                    entityName = ((EntityAdapterProperty)this).EntityAdapter.GetEntityNameByRelation(parts[parts.Length - 2]);
                else
                    entityName = ((EntityAdapterPublicationProperty)this).EntityAdapter.GetEntityNameByRelation(parts[parts.Length - 2]);

                if (entityName.IsNullOrEmpty())
                    entityName = parts[parts.Length - 2];
            }

            return entityName;
        }


        public string GetEntityName()
        {
            string entityName = String.Empty;
            
            if (this is EntityAdapterFormula)
                entityName = ((EntityAdapterFormula)this).EntityAdapter.Name;
            else if (this is EntityAdapterProperty)
                entityName = ((EntityAdapterProperty)this).EntityAdapter.Name;
            else
                entityName = ((EntityAdapterPublicationProperty)this).EntityAdapter.Name;
            
            return entityName;
        }

        public string GetEdmPropertyName()
        {
            string edmPath = GetEdmPath(true);
            return edmPath.Right(".");
        }

        public void CreateKpiPartner()
        {
            EntityAdapter adapter = null;
            if (this is EntityAdapterProperty)
                adapter = ((EntityAdapterProperty)this).EntityAdapter;
            else if (this is EntityAdapterFormula)
                adapter = ((EntityAdapterFormula)this).EntityAdapter;

            if (adapter != null)
            {
                if (adapter.GetAllAttributes().Where(e => e.KpiRelatedAttribute == this.Name).Count() == 0)
                {
                    EntityAdapterFormula formula = adapter.EntityAdapterFormulas.AddNew() as EntityAdapterFormula;
                    if (formula != null)
                    {
                        formula.Name = this.Name + "KpiInfo";
                        formula.KpiRelatedAttribute = this.Name;
                        formula.Formula = "GetKpiFor" + this.Name + "()";
                        formula.Datatype = "System.String";
                        formula.DisplayName = (this.DisplayName.IsNullOrEmpty() ? this.Name : this.DisplayName + " (KPI)");
                        formula.DisplayControl = DisplayControlType.KpiBox;
                        formula.TriggerAttributes = this.Name;
                        formula.IsEditable = false;
                    }
                }
            }
        }

        public void CreateNormalizedKey()
        {
            EntityAdapter adapter = null;
            if (this is EntityAdapterProperty)
                adapter = ((EntityAdapterProperty)this).EntityAdapter;

            if (adapter != null && adapter.BaseEntityAdapter == null)
            {
                EntityAdapterProperty normalizedKey = adapter.EntityAdapterProperties.Where(e => e.Name == "NormalizedKey").FirstOrDefault();
                if (normalizedKey == null)
                {
                    normalizedKey = adapter.EntityAdapterProperties.AddNew() as EntityAdapterProperty;
                    normalizedKey.CopyInstanceFrom(this);
                    normalizedKey.Name = "NormalizedKey";
                    normalizedKey.DenormalizedDataInfo = String.Empty;
                    normalizedKey.DisplayName = "Normalized Key";
                }
                normalizedKey.IsEditable = false;
                normalizedKey.IsPK = true;
                normalizedKey.IsFK = false;
                normalizedKey.Datatype = "System.Int32";
                normalizedKey.DataFormatString = "N0";
                normalizedKey.IsCustomized = true;
                normalizedKey.EdmKey = "normalizedEntity" + this.Name + ".Id";
            }
        }

        public void RemoveNormalizedKey()
        {
            EntityAdapter adapter = null;
            if (this is EntityAdapterProperty)
                adapter = ((EntityAdapterProperty)this).EntityAdapter;

            if (adapter != null && adapter.BaseEntityAdapter == null)
            {
                EntityAdapterProperty normalizedKey = adapter.EntityAdapterProperties.Where(e => e.Name == "NormalizedKey").FirstOrDefault();
                if (normalizedKey != null)
                    adapter.EntityAdapterProperties.Remove(normalizedKey);

            }

        }

        public bool IsNullable()
        {
            return (this.IsNull || this.Datatype.Contains("Nullable<") || this.Datatype.Contains("?"));
        }
    }
}