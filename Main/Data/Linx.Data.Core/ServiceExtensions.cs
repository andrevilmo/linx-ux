using Linx.DS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Data
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Initializes the domain service by creating a new <see cref="DomainServiceContext"/>
        /// and calling the base DomainService.Initialize(DomainServiceContext) method.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static TService Initialize<TService>(this TService service)
            where TService : DomainService
        {
            var context = CreateDomainServiceContext();
            service.Initialize(context);
            return service;
        }

        /// <summary>
        /// Create an instance of AuthorizationContext.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static System.ComponentModel.DataAnnotations.AuthorizationContext CreateAuthorizationContext<TService>(this TService service)
            where TService : DomainService
        {
            return new System.ComponentModel.DataAnnotations.AuthorizationContext(new DomainServiceProvider(service));
        }

        private static DomainServiceContext CreateDomainServiceContext()
        {
            var provider = new ServiceProvider(new System.Security.Claims.ClaimsPrincipal(System.Security.Principal.GenericPrincipal.Current));
            return new DomainServiceContext(provider, DomainOperationType.Submit);
        }
    }

    public partial class ServiceProvider : IServiceProvider
    {
        private System.Security.Principal.IPrincipal instance;

        public ServiceProvider(System.Security.Principal.IPrincipal serviceInstance)
        {
            instance = serviceInstance;
        }

        public object GetService(Type serviceType)
        {
            return instance;
        }
    }

    public partial class DomainServiceProvider : IServiceProvider
    {
        private DomainService instance;

        public DomainServiceProvider(DomainService serviceInstance)
        {
            instance = serviceInstance;
        }

        public object GetService(Type serviceType)
        {
            return instance;
        }
    }
}
