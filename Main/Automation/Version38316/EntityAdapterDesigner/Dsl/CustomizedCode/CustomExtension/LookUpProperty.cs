using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{
    
    public partial class LookUpProperty
    {

        public string GetSubstituteProperties()
        {
            string result = String.Empty;
            if (!this.SubstituteProperties.IsNullOrEmpty())
            {
                foreach (var propName in this.SubstituteProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var prop = this.LookUpAdapter.LookUpProperties.FirstOrDefault(e => e.Name == propName.Trim());
                    if (prop != null)
                    {
                        result += (result.IsNullOrEmpty() ? String.Empty : ",") + prop.Name + ":" + prop.DisplayName;
                    }
                }                
            }
            return result;
        }

        public string GetEdmEntityName()
        {
            string entityName = String.Empty;
            string[] parts = this.EdmKey.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                entityName = this.LookUpAdapter.EntityAdapter.GetEntityNameByRelation(parts[parts.Length - 2]);
                if (entityName.IsNullOrEmpty())
                    entityName = parts[parts.Length - 2];
            }

            return entityName;
        }
    }
}
