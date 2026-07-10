using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Channels;
using LINXDEMO.BM;

namespace Linx.Demo.BV.ModalExterna
{
	
	#region Automatic Authorization
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EstadoUpdateCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		ModalExternaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(ModalExternaDomainService)) as ModalExternaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Update, "Linx.Demo.BV#Linx.Demo.BV.ModalExterna#Linx.Demo.BV.ModalExterna.Estado", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(ModalExternaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EstadoInsertCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		ModalExternaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(ModalExternaDomainService)) as ModalExternaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Insert, "Linx.Demo.BV#Linx.Demo.BV.ModalExterna#Linx.Demo.BV.ModalExterna.Estado", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(ModalExternaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EstadoDeleteCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		ModalExternaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(ModalExternaDomainService)) as ModalExternaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Delete, "Linx.Demo.BV#Linx.Demo.BV.ModalExterna#Linx.Demo.BV.ModalExterna.Estado", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(ModalExternaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EstadoQueryCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		ModalExternaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(ModalExternaDomainService)) as ModalExternaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Query, "Linx.Demo.BV#Linx.Demo.BV.ModalExterna#Linx.Demo.BV.ModalExterna.Estado", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(ModalExternaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	#endregion Automatic Authorization
	
}
