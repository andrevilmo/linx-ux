using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.LinqExtensions
{

    public static class EFUpdateProperties
    {
        public static void UpdateProperties(this EntityEntry entry, object dto)
        {
            var dtoProps = dto.GetType().GetProperties().Where(p => !p.GetMethod.IsVirtual).ToArray();

            foreach (PropertyInfo dtoProp in dtoProps)
            {
                var propEntry = entry.Property(dtoProp.Name);
                if (propEntry != null)
                {
                    propEntry.CurrentValue = dto.GetPropertyValue(dtoProp.Name);
                }                
            }
        }
    }


}
