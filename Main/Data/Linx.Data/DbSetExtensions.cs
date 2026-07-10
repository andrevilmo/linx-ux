using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Data
{
    public static class DbSetExtensions
    {
        public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet)
        where TEntity : class
        {
            object internalSet = dbSet
                .GetType()
                .GetField("_internalSet", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
            object internalContext = internalSet
                .GetType()
                .BaseType
                .GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(internalSet);
            return (DbContext)internalContext
                .GetType()
                .GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public)
                .GetValue(internalContext, null);
        }
        
        public static ObjectQuery<TEntity> Where<TEntity>(this DbSet<TEntity> dbSet, string predicate, params ObjectParameter[] parameters)
        where TEntity : class
        {
            return dbSet.Where<TEntity, TEntity>(predicate, parameters);
        }

        public static ObjectQuery<TEntity> Where<TEntity, TEntityBase>(this DbSet<TEntity> dbSet, string predicate, params ObjectParameter[] parameters)
            where TEntity : class
            where TEntityBase : class
        {
            var context = dbSet.GetContext();
            return ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntityBase>().OfType<TEntity>().Where(predicate, parameters);
        }
    }
}
