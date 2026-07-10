using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace Linx.LinqExtensions
{
    public static class DbExtensions
    {
        public static object Find(this IQueryable set, Type eType, params object[] keyValues)
        {
            //return typeof(DbExtensions).InvokeMember("Find", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public,
            //null, null, new object[] { set, keyValues });

            //var support = new DbExtensionsSupport<TEntity>();
            MethodInfo method = typeof(DbExtensions).GetMethod("FindEntity", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.NonPublic);
            MethodInfo generic = method.MakeGenericMethod(eType);
            List<object> parameters = new List<object>();
            parameters.Add(set);
            parameters.Add(keyValues);
            return generic.Invoke(null, parameters.ToArray());
        }
        
        public static TEntity Find<TEntity>(this IQueryable<TEntity> set, params object[] keyValues) where TEntity : class
        {
            return FindEntity(set, keyValues);            
        }

        private static TEntity FindEntity<TEntity>(IQueryable<TEntity> set, params object[] keyValues) where TEntity : class
        {
            if (!(set is IInfrastructure<IServiceProvider>))
            {
                return default(TEntity);
            }

            DbContext context;
            if (set is IContext)
                context = ((IContext)set).Context;
            else
                context = ((IInfrastructure<IServiceProvider>)set).GetService<DbContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            IEnumerable<TEntity> entries = set;

            string filter = "";
            List<object> parameters = new List<object>();
            var iProp = 0;
            foreach (var property in key.Properties)
            {
                var value = keyValues[iProp];
                filter += (filter == "" ? "" : " && ") + "it." + property.Name + " = @" + iProp.ToString();
                parameters.Add(value);
                iProp++;
            }
            
            var entry = set.Where(filter, parameters.ToArray()).FirstOrDefault();

            // Return the local object if it exists.
            return entry;
        }

    }
}
