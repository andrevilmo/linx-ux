using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;


namespace Linx.DataService
{
    public class ODataRouteByController : IODataRoutingConvention
    {
        string controller;

        public ODataRouteByController(string controller)
        {
            this.controller = controller;
        }

        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (controllerContext.Request.Method == HttpMethod.Get && odataPath.PathTemplate.StartsWith("~/entityset"))
            {
                EntitySetPathSegment entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
                return "Get" + entitySetSegment.EntitySetName + "NoAssociations";
            }

            return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate.StartsWith("~/entityset"))
                return this.controller;

            return null;
        }
    }

}
