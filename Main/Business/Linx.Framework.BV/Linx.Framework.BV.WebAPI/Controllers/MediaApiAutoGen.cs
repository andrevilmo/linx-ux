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
using BusinessNS = Linx.Framework.BV.Multimidia;

namespace Linx.Framework.BV.WebAPI.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/MediaApi/[ActionName]
    // Security Information Call: http://localhost:1710/MediaApi/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/MediaApi/GetEntities
    // Entity MetaData Call: http://localhost:1710/MediaApi/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/MediaApi/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/MediaApi/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/MediaApi/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/MediaApi/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/MediaApi
    [RoutePrefix("MediaApi")]
    public partial class MediaApiController : ApiController
    {
    }
    public partial class MediaApiControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}
