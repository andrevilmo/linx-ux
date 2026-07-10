using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Linx.LinqExtensions
{
    public class DbEntityEntryCloned
    {
        public EntityState State { get; internal set; }
        public DbEntityEntry CurrentDbEntityEntry { get; internal set; }
        public DbPropertyValues OriginalValues { get; internal set; }
        public string TypeFullName { get; internal set; }

        public object Entity { get; internal set; }
    }
}
