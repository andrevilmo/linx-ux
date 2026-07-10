using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using InteractivePreGeneratedViews;

namespace Linx.LinqExtensions
{
    public static class InteractiveViewsHelper
    {
        private static bool IsAttached(DbContext context)
        {
            var oCtx = ((IObjectContextAdapter)context).ObjectContext;
            var viewCache = (StorageMappingItemCollection)oCtx.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            return (viewCache.MappingViewCacheFactory is SqlServerViewCacheFactory);
        }

        public static void SetViewCacheFactory(DbContext context)
        {
            SetViewCacheFactory(context, context.Database.Connection.ConnectionString);
        }

        public static void SetViewCacheFactory(DbContext context, string connectionString)
        {
            if (!IsAttached(context))
            {
                InteractivePreGeneratedViews.InteractiveViews.SetViewCacheFactory(context,
                        new InteractivePreGeneratedViews.SqlServerViewCacheFactory(connectionString));
            }
        }

    }
}