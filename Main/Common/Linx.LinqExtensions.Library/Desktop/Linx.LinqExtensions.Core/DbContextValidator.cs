using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Linx.LinqExtensions
{
    public static class DbContextValidator
    {
        public static void ValidateEntities(this DbContext dbContext)
        {
            var serviceProvider = dbContext.GetService<IServiceProvider>();

            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var items = new Dictionary<object, object>();
                items.Add("Context", dbContext);
                items.Add("State", entry.State);
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}
