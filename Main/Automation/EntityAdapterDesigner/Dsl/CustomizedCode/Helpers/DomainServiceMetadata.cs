using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public class DomainServiceMetadata
    {
        public DomainServiceModel[] Models { get; set; }
        public DomainServiceKPI[] KPIs { get; set; }
        public DomainServiceDomain[] Domains { get; set; }

    }

    public class DomainServiceModel
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public bool AuthorizationEnabled { get; set; }
        public DomainServiceEntity[] Entities { get; set; }

        public string GetFullName()
        {
            return this.Namespace + "." + this.Name;
        }

        public static string GetCustomAttributeValue(string attrDef, string propertyName)
        {
            var innerProp = attrDef.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(p => p.Contains(propertyName + " ") || p.Contains(propertyName + "="));
            if (innerProp.IsNullOrEmpty())
                return "";
            else
                return innerProp.Right("=");
        }
    }

    public class DomainServiceEntity
    {
        public string Name { get; set; }
        public string BaseTypeName { get; set; }
        public string[] Decorators { get; set; }
        public DomainServiceProperty[] Properties { get; set; }
        
        public bool IsBaseTypeOf(DomainServiceEntity entity)
        {
            return entity.BaseTypeName == this.Name;
        }
        
        public string[] GetCustomAttributes(string name)
        {
            return this.Decorators.Where(d => d.Contains(name)).Select(d => d.Extract(name + "(", ")").Replace("'", "").Replace("\"", "").Replace("\\", "")).ToArray();
        }

        public string GetCustomAttribute(string name)
        {
            return GetCustomAttributes(name).FirstOrDefault();
        }
    }

    public class DomainServiceProperty
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
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
    }

    public class DomainServiceDomain
    {
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public DomainServiceDomainValue[] Values { get; set; }
    }

    public class DomainServiceDomainValue
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }

    public class DomainServiceKPI
    {
        public string Name { get; set; }
        public string NameSpace { get; set; }
        public string Description { get; set; }
        public string ShowType { get; set; }
    }

}
