using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.ServiceBus.Starter.Areas.HelpPage.Models
{
    public class HttpParameterDescriptorFake : System.Web.Http.Controllers.HttpParameterDescriptor
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object _DefaultValue { get; set; }

        public HttpParameterDescriptorFake(string name, Type type, object defaultValue)
        {
            this.Name = name;
            this.Type = type;
            this._DefaultValue = defaultValue;
        }
        public static HttpParameterDescriptorFake StringValue(string defaultValue = null)
        {
            return new HttpParameterDescriptorFake(null, typeof(string), defaultValue);
        }
        public static HttpParameterDescriptorFake GuidValue(Guid? defaultValue = null)
        {
            return new HttpParameterDescriptorFake(null, typeof(Guid), defaultValue);
        }

        public override string ParameterName { get { return this.Name; } }
        public override Type ParameterType { get { return this.Type; } }
        public override object DefaultValue { get { return _DefaultValue; } }
    }
}