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
using LinxTraining002.BM;

namespace LinxTraining001.BV.DetalhamentoVenda
{
	
	#region Automatic Authorization
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaDetalheViewUpdateCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DetalhamentoVendaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DetalhamentoVendaDomainService)) as DetalhamentoVendaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Update, "LinxTraining001.BV#LinxTraining001.BV.DetalhamentoVenda#LinxTraining001.BV.DetalhamentoVenda.VendasView", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DetalhamentoVendaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaDetalheViewInsertCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DetalhamentoVendaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DetalhamentoVendaDomainService)) as DetalhamentoVendaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Insert, "LinxTraining001.BV#LinxTraining001.BV.DetalhamentoVenda#LinxTraining001.BV.DetalhamentoVenda.VendasView", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DetalhamentoVendaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaDetalheViewDeleteCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DetalhamentoVendaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DetalhamentoVendaDomainService)) as DetalhamentoVendaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Delete, "LinxTraining001.BV#LinxTraining001.BV.DetalhamentoVenda#LinxTraining001.BV.DetalhamentoVenda.VendasView", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DetalhamentoVendaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class VendaDetalheViewQueryCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		DetalhamentoVendaDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(DetalhamentoVendaDomainService)) as DetalhamentoVendaDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Business.Tools.LinxAutorization.ValidateAuthorization(AuthorizationType.Query, "LinxTraining001.BV#LinxTraining001.BV.DetalhamentoVenda#LinxTraining001.BV.DetalhamentoVenda.VendasView", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(DetalhamentoVendaDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	#endregion Automatic Authorization
	
}
