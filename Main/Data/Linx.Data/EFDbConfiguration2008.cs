using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Linx.Data
{
    /// <summary>
    /// Adjust SQL Server compatibility with the version 2008.
    /// In web.config, change <entityFramework> to <entityFramework codeConfigurationType="Linx.Data.EFDbConfiguration2008, Linx.Data">
    /// </summary>
    internal sealed class EFDbConfiguration2008 : DbConfiguration
    {
        /// <summary>
        /// The provider manifest token to use for SQL Server.
        /// </summary>
        private const string SqlServerManifestToken = @"2008";

        /// <summary>
        /// Initializes a new instance of the <see cref="EFDbConfiguration2008"/> class.
        /// </summary>
        public EFDbConfiguration2008()
        {
            this.AddDependencyResolver(new SingletonDependencyResolver<IManifestTokenResolver>(new ManifestTokenService()));
        }

        /// <inheritdoc />
        private sealed class ManifestTokenService : IManifestTokenResolver
        {
            /// <summary>
            /// The default token resolver.
            /// </summary>
            private static readonly IManifestTokenResolver DefaultManifestTokenResolver = new DefaultManifestTokenResolver();

            /// <inheritdoc />
            public string ResolveManifestToken(DbConnection connection)
            {
                if (connection is SqlConnection)
                {
                    return SqlServerManifestToken;
                }

                return DefaultManifestTokenResolver.ResolveManifestToken(connection);
            }
        }
    }
}