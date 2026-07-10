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
using Linx.Demo.BM;

namespace VAREJO.BV.Diversos
{
	
	#region Automatic Authorization
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaUpdateCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DiversosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DiversosDomainService)) as DiversosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Update, "VAREJO.BV#VAREJO.BV.Diversos#VAREJO.BV.Diversos.Venda", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DiversosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaInsertCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DiversosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DiversosDomainService)) as DiversosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Insert, "VAREJO.BV#VAREJO.BV.Diversos#VAREJO.BV.Diversos.Venda", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DiversosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaDeleteCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DiversosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DiversosDomainService)) as DiversosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Delete, "VAREJO.BV#VAREJO.BV.Diversos#VAREJO.BV.Diversos.Venda", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DiversosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaQueryCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DiversosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DiversosDomainService)) as DiversosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Query, "VAREJO.BV#VAREJO.BV.Diversos#VAREJO.BV.Diversos.Venda", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DiversosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	#endregion Automatic Authorization
	
}
