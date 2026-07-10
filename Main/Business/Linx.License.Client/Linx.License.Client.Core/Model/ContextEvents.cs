using System;
using System.Collections.Generic;
using System.Linq;
using Linx.Tools;
using Linx.LinqExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Linx.License.Client
{
    
    /// <summary>
    /// Events for executing rules before and after saving the context.
    /// e.g.: var addedEntities = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added);
    /// </summary>
    public partial class ContextEvents
    {
        public static bool BeforeSaveChanges(DbContext context)
        {
           return true;
        }
        
        public static void AfterSaveChanges(DbContext context)
        {
           
        }
    }
}
