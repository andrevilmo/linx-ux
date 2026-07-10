using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;
using Linx.EntityAdapterDesigner.CustomizedCode;

namespace Linx.EntityAdapterDesigner
{
    internal interface IEntityProperty
    {
        string Datatype { get; set; }
        string EdmKey { get; set; }
        string GetEntityNameByRelation(string entityRelationName);
    }

    public partial class LookUpProperty : IEntityProperty
    {
        public string GetEntityNameByRelation(string entityRelationName)
        {
            string entityName = String.Empty;

            if (this.LookUpAdapter != null)
                entityName = this.LookUpAdapter.GetEntityNameByRelation(entityRelationName);

            return entityName.IsNullOrEmpty() ? entityRelationName : entityName;
        }
    }
    
    public partial class EntityAdapterProperty : IEntityProperty
    {

        public string GetModelViewLookUpInfo(List<PublicationEntity> modelClasses)
        {
            if (!this.ModelViewSource.IsNullOrEmpty() && this.ModelViewSource.Left(".") != this.EntityAdapter.PrimaryEntity)
            {
                string lookupEntityName = this.ModelViewSource.Left(".");
                string sourcePropName = this.ModelViewSource.Extract(".", "(");

                var lookupEntity = modelClasses.FirstOrDefault(e => e.Name == lookupEntityName);
                if (lookupEntity != null)
                {
                    var lookupProp = lookupEntity.Properties.FirstOrDefault(e => e.Name == sourcePropName);
                    if (lookupProp != null)
                    {
                        var precision = lookupProp.Precision;
                        precision = (precision + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).Left(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator) + ":" + (precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).IsNullOrEmpty() ? "0" : precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
                        return this.Datatype + "#" + sourcePropName + "#" + lookupProp.IsPrimaryKey.ToString().ToLower() + "##" + precision + "#" + this.DomainName + "#" + this.DisplayName + "#" + lookupEntity.Properties.IndexOf(lookupProp).ToString() + "#true#::" + lookupEntityName + "##false#false##" + lookupEntityName + "#" + this.EntityAdapter.EntityAdapterDesignerRoot.TargetNamespace + "#IQueryable#";
                    }
                }
            }

            return String.Empty;
        }

        public string GetPrefixLinqMethod()
        {
            if (this.LinqMethod == "Date")
                return "System.Data.Entity.DbFunctions.TruncateTime";
            return String.Empty;
        }

        public string GetSuffixLinqMethod()
        {
            if (this.LinqMethod == "Date")
                return (this.IsNullable() ? "" : "Value");
            return this.LinqMethod;
        }

        public string GetEntityNameByRelation(string entityRelationName)
        {
            string entityName = String.Empty;

            if (this.EntityAdapter != null)
                entityName = this.EntityAdapter.GetEntityNameByRelation(entityRelationName);

            return entityName.IsNullOrEmpty() ? entityRelationName : entityName;
        }

        public bool IsEdmKeyProperty()
        {
            if (this.EdmKey.IsNullOrEmpty())
                return false;

            if (!this.IsCustomized)
                return true;

            string[] edmParts = this.EdmKey.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (edmParts.Length <= 1)
                return false;

            if (edmParts[0] != this.EntityAdapter.PrimaryEntity)
                return false;

            var validator = new System.Text.RegularExpressions.Regex(@"^[_a-zA-Z0-9]*$");
            foreach (var edmPart in edmParts)
            {                
                if (!validator.IsMatch(edmPart))
                    return false;
            }

            return true;
        }

        public string GetDisplayValueValue()
        {
            string result;

            switch (this.EntityAdapter.PropertyOrder)
            {
                case AttributeOrder.Name:
                    result = this.Name;
                    break;
                case AttributeOrder.DisplayName:
                    result = this.DisplayName;
                    break;
                case AttributeOrder.EdmKey:
                    result = this.EdmKey;
                    break;
                default:
                    result = this.Name;
                    break;
            }

            return (this.DenormalizedDataInfo.IsNullOrEmpty() ? "" : ((char)8660).ToString()) + result + (this.IsNull || this.Datatype.Contains("Nullable<") || this.Datatype.Contains("?") ? " (Null)" : "");
        }
    }
}