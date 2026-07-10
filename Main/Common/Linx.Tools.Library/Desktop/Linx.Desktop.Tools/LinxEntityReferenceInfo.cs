using System.Collections.Generic;

namespace Linx.Tools
{
    public class LinxEntityReferenceInfo
    {
        public string ClassName { get; set; }
        public string NameSpace { get; set; }
        public string ParentClassName { get; set; }
        public string DisplayName { get; set; }
        public string ClearMethodName { get; set; }
        public string QueryMethodName { get; set; }
        public string CountingMethodName { get; set; }
        public string SubQueryInfo { get; set; }
        public string EdmEntityName { get; set; }
        public string EdmParentEntityName { get; set; }
        public bool HasQuickSearch { get; set; }
        public List<Linx.Tools.PropertyDefinitions> Properties { get; set; }

        public override string ToString()
        {
            return ClassName;
        }
    }
}
