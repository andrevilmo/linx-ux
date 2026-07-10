using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Linx.LinqExtensions
{
    public static class DbTrackerExtensions
    {
        #region DbPropertyValues Extensions
        public static string GetStringValue(this DbPropertyValues entity, string propertyName)
        {
            return entity == null || entity[propertyName] == null ? "NULL" : entity[propertyName].ToString();
        }
        public static Dictionary<string, string> GetDictionaryValues(this DbPropertyValues dbPropertyValues)
        {
            return dbPropertyValues?.PropertyNames.ToDictionary(n => n, n => dbPropertyValues.GetStringValue(n));
        }
        #endregion

        #region EntityState Extensions
        public static char GetCharValue(this EntityState state)
        {
            return GetStringValue(state)[0];
        }

        public static string GetStringValue(this EntityState state)
        {
            string stateReturn = string.Empty;
            switch (state)
            {
                case EntityState.Added:
                    stateReturn = "I";
                    break;
                case EntityState.Deleted:
                    stateReturn = "D";
                    break;
                case EntityState.Modified:
                    stateReturn = "E";
                    break;
                default:
                    stateReturn = "U";
                    break;
            }
            return stateReturn;
        }
        #endregion

        #region DBEntityEntry Extensions
        public static DbEntityEntryCloned GetEntriesCloned(this DbEntityEntry entry)
        {
            if (entry.State == EntityState.Unchanged)
                return new DbEntityEntryCloned { TypeFullName = entry.Entity.GetType().FullName, State = EntityState.Unchanged };
            else
                return new DbEntityEntryCloned
                {
                    CurrentDbEntityEntry = entry,
                    Entity = entry.Entity,
                    State = entry.State,
                    OriginalValues = entry.State == EntityState.Added ? null : entry.OriginalValues.Clone(),
                    TypeFullName = entry.Entity.GetType().FullName
                };

        }
        #endregion

    }

   

}
