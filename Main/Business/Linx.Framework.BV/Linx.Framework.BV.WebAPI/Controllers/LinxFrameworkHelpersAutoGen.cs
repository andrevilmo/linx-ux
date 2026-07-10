using Linx.Data;
using Linx.LinqExtensions.Dynamic;
using Linx.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.ServiceModel.DomainServices.Server;
using Linx.Business.Tools;
using System.ComponentModel.Composition;
using System.Web.Http;

namespace Linx.Framework.BV.WebAPI.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkHelpers/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkHelpers/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkHelpers/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkHelpers/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkHelpers/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkHelpers/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkHelpers/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkHelpers/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkHelpers
    [RoutePrefix("LinxFrameworkHelpers")]
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkHelpersController : ApiController
    {
    }
    public partial class LinxFrameworkHelpersControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Framework.BV", "LinxFrameworkHelpers", actionContext.ActionDescriptor.ActionName));
        }
    }
}
