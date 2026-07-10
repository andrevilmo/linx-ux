using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Web.Http.Description;

namespace Linx.ServiceBus.Starter.Areas.HelpPage
{
    public static class Extensions
    {
        public static TextSample ToTextSample(this string stringSample)
        {
            return new TextSample(stringSample);
        }

        public static TextSample GetDescription(this Type type)
        {
            var attr = type.GetCustomAttribute<DescriptionAttribute>();

            if (attr != null)
                return attr.Description.ToTextSample();

            return null;
        }


        public static TextSample GetControllerDescription(this Collection<ApiDescription> collection)
        {
            if (collection == null) return null;

            var item = collection.FirstOrDefault();

            if (item == null) return null;

            if (item.ActionDescriptor != null && item.ActionDescriptor.ControllerDescriptor != null &&
                item.ActionDescriptor.ControllerDescriptor.ControllerType != null)
                return item.ActionDescriptor.ControllerDescriptor.ControllerType.GetDescription();

            return null;
        }
    }
}