using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;

namespace Linx.DS.Core.Data
{
    /// <summary>
    /// Represents the execution context for a <see cref="DomainService"/> request.
    /// </summary>
    public class DomainServiceContext : IServiceProvider
    {
        private DomainOperationEntry _operation;
        private DomainOperationType _operationType;
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the DomainServiceContext class
        /// </summary>
        /// <param name="serviceProvider">A service provider.</param>
        /// <param name="operationType">The type of operation that is being executed.</param>
        public DomainServiceContext(IServiceProvider serviceProvider, DomainOperationType operationType)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            this._serviceProvider = serviceProvider;
            this._operationType = operationType;
        }

        /// <summary>
        /// Copy constructor that creates a new context of the specified type copying
        /// the rest of the context from the provided instance.
        /// </summary>
        /// <param name="serviceContext">The service context to copy from.</param>
        /// <param name="operationType">The type of operation that is being executed.</param>
        internal DomainServiceContext(DomainServiceContext serviceContext, DomainOperationType operationType)
        {
            if (serviceContext == null)
            {
                throw new ArgumentNullException("serviceContext");
            }
            this._serviceProvider = serviceContext._serviceProvider;
            this._operationType = operationType;
        }

        /// <summary>
        /// Gets the operation that is being executed.
        /// </summary>
        public DomainOperationEntry Operation
        {
            get
            {
                return this._operation;
            }
            internal set
            {
                this._operation = value;
            }
        }

        /// <summary>
        /// Gets the type of operation that is being executed.
        /// </summary>
        public DomainOperationType OperationType
        {
            get
            {
                return this._operationType;
            }
        }

        /// <summary>
        /// The user for this context instance.
        /// </summary>
        public IPrincipal User
        {
            get
            {
                return (IPrincipal)this._serviceProvider.GetService(typeof(IPrincipal));
            }
        }

        #region IServiceProvider Members

        /// <summary>
        /// See <see cref="IServiceProvider.GetService(Type)"/>.
        /// When the <see cref="ServiceContainer"/> is in use, it will be used
        /// first to retrieve the requested service.  If the <see cref="ServiceContainer"/>
        /// is not being used or it cannot resolve the service, then the
        /// <see cref="IServiceProvider"/> provided to this <see cref="DomainServiceContext"/>
        /// will be queried for the service type.
        /// </summary>
        /// <param name="serviceType">The type of the service needed.</param>
        /// <returns>An instance of that service or null if it is not available.</returns>
        public object GetService(Type serviceType)
        {
            object service = null;

            if (service == null && this._serviceProvider != null)
            {
                service = this._serviceProvider.GetService(serviceType);
            }

            return service;
        }

        #endregion
        
    }
}
