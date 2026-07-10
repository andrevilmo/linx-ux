using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using System.Xml;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapterExtendedFilter
    {
        public EntityAdapterExtendedFilter2 ToCustomFilter()
        {
            EntityAdapterExtendedFilter2 result = new EntityAdapterExtendedFilter2();

            result.Name = this.Name;
            result.DisplayName = this.DisplayName;
            result.EntityName = this.EntityName;
            result.IsUsedInTheLinq = this.IsUsedInTheLinq;
            result.RelationName = this.RelationName;
            foreach (var prop in this.EntityAdapterPropertyExtendedFilters)
            {
                EntityAdapterPropertyExtendedFilter2 propFilter = new EntityAdapterPropertyExtendedFilter2();
                propFilter.Name = prop.Name;
                propFilter.DisplayName = prop.DisplayName;
                propFilter.DataType = prop.DataType;
                propFilter.IsEnabled = prop.IsEnabled;
                propFilter.EdmKey = prop.EdmKey;
                result.EntityAdapterPropertyExtendedFilters.Add(propFilter);
            }

            return result;
        }
    }

    public partial class EntityAdapterExtendedFilter2
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string EntityName { get; set; }
        public bool IsUsedInTheLinq { get; set; }
        public string RelationName { get; set; }
        private List<EntityAdapterPropertyExtendedFilter2> _entityAdapterPropertyExtendedFilters;
        public List<EntityAdapterPropertyExtendedFilter2> EntityAdapterPropertyExtendedFilters
        {
            get
            {
                if (_entityAdapterPropertyExtendedFilters == null)
                    _entityAdapterPropertyExtendedFilters = new List<EntityAdapterPropertyExtendedFilter2>();
                return _entityAdapterPropertyExtendedFilters;
            }
        }
        
    }

    public partial class EntityAdapterPropertyExtendedFilter2
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public bool IsEnabled { get; set; }
        public string EdmKey { get; set; }
    }

}
