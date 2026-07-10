using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;


namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public class ContextMetadata
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string ConnectionName { get; set; }
        public bool AuthorizationEnabled { get; set; }
        public ContextEntity[] Entities { get; set; }
        public ContextDomain[] Domains { get; set; }

        public string GetFullName()
        {
            return this.Namespace + "." + this.Name;
        }

    }

    public class ContextEntity
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string Table { get; set; }
        public string BaseTypeName { get; set; }
        public string StructureType { get; set; }
        public ContextProperty[] Properties { get; set; }

        public bool IsBaseTypeOf(ContextEntity entity)
        {
            return entity.BaseTypeName == this.Name;
        }
    }

    public class ContextProperty
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public bool IsNavigation { get; set; }
        public bool IsCollection { get; set; }
        public string[] Decorators { get; set; }

        public string GetCustomAttribute(string name)
        {
            var decorator = this.Decorators.FirstOrDefault(d => d.Contains(name));
            if (!String.IsNullOrWhiteSpace(decorator))
            {
                return decorator.Extract(name + "(", ")").Replace("'", "").Replace("\"", "").Replace("\\", "");
            }
            else
                return String.Empty;
        }

        public bool IsPrimaryKey()
        {
            return this.Decorators.Any(d => d.Contains("[Key]") || d.Contains("[Key()]"));
        }

        public bool IsForeignKey()
        {
            return this.Decorators.Any(e => e.Contains("ForeignKey"));
        }        
        public bool IsRequired()
        {
            return this.Decorators.Any(p => p.Contains("Required"));
        }
        
    }

    public class ContextDomain
    {
        public string Name { get; set; }
        public ContextDomainValue[] Values { get; set; }
    }

    public class ContextDomainValue
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    
}
