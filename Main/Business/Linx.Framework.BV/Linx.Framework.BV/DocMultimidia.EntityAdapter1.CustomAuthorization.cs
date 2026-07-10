using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
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
using System.Data.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using Linx.TCS0101.EDM;

namespace Linx.TCS0101.BO.DocMultimidia
{
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EntityAdapter1UpdateCustomAuthorizationAttribute : AuthorizationAttribute
	{
	
		///Insert here all checks for authorization.
		///Example checking the user's role and the first letter of the user's name:
		///	if (principal.IsInRole("Role Name") && principal.Identity.Name.StartsWith("B"))
		///			return new AuthorizationResult("Error message");
		///	else
		///			return AuthorizationResult.Allowed;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				return AuthorizationResult.Allowed;
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EntityAdapter1InsertCustomAuthorizationAttribute : AuthorizationAttribute
	{
	
		///Insert here all checks for authorization.
		///Example checking the user's role and the first letter of the user's name:
		///	if (principal.IsInRole("Role Name") && principal.Identity.Name.StartsWith("B"))
		///			return new AuthorizationResult("Error message");
		///	else
		///			return AuthorizationResult.Allowed;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				return AuthorizationResult.Allowed;
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EntityAdapter1DeleteCustomAuthorizationAttribute : AuthorizationAttribute
	{
	
		///Insert here all checks for authorization.
		///Example checking the user's role and the first letter of the user's name:
		///	if (principal.IsInRole("Role Name") && principal.Identity.Name.StartsWith("B"))
		///			return new AuthorizationResult("Error message");
		///	else
		///			return AuthorizationResult.Allowed;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				return AuthorizationResult.Allowed;
		}
	
	}
	
	
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public partial class EntityAdapter1QueryCustomAuthorizationAttribute : AuthorizationAttribute
	{
	
		///Insert here all checks for authorization.
		///Example checking the user's role and the first letter of the user's name:
		///	if (principal.IsInRole("Role Name") && principal.Identity.Name.StartsWith("B"))
		///			return new AuthorizationResult("Error message");
		///	else
		///			return AuthorizationResult.Allowed;
		protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)
		{
				return AuthorizationResult.Allowed;
		}
	
	}
	
	
}
