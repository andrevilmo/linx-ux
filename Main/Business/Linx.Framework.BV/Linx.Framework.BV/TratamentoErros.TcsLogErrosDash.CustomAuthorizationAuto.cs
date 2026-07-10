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
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.TratamentoErros
{
	
	#region Automatic Authorization
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class TcsLogErrosDashUpdateCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		TratamentoErrosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(TratamentoErrosDomainService)) as TratamentoErrosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Framework.BV.LinxBusinessAutorization.ValidateAuthorization(AuthorizationType.Update, "Linx.Framework.BV#Linx.Framework.BV.TratamentoErros#Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(TratamentoErrosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class TcsLogErrosDashInsertCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		TratamentoErrosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(TratamentoErrosDomainService)) as TratamentoErrosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Framework.BV.LinxBusinessAutorization.ValidateAuthorization(AuthorizationType.Insert, "Linx.Framework.BV#Linx.Framework.BV.TratamentoErros#Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(TratamentoErrosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class TcsLogErrosDashDeleteCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		TratamentoErrosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(TratamentoErrosDomainService)) as TratamentoErrosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Framework.BV.LinxBusinessAutorization.ValidateAuthorization(AuthorizationType.Delete, "Linx.Framework.BV#Linx.Framework.BV.TratamentoErros#Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(TratamentoErrosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class TcsLogErrosDashQueryCustomAuthorizationAutoAttribute : AuthorizationAttribute
	{
	
		TratamentoErrosDomainService _domainService = null;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(TratamentoErrosDomainService)) as TratamentoErrosDomainService;
				return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : Linx.Framework.BV.LinxBusinessAutorization.ValidateAuthorization(AuthorizationType.Query, "Linx.Framework.BV#Linx.Framework.BV.TratamentoErros#Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));
		}
		
		public AuthorizationResult Authorize(TratamentoErrosDomainService domainService = null)
		{
				_domainService = domainService;
				return IsAuthorized(null, null);
		}
	
	}
	
	#endregion Automatic Authorization
	
}
