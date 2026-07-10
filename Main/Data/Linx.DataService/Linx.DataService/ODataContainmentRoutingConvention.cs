using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Linx.Tools;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;


namespace Linx.DataService
{
    public class ODataContainmentRoutingConvention : IODataRoutingConvention
    {
        string controller;

        public ODataContainmentRoutingConvention(string controller)
        {
            this.controller = controller;
        }


        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            //Query by jEntitySearch
            if (odataPath.PathTemplate == "~/entityset" &&
                (controllerContext != null && controllerContext.Request != null && controllerContext.Request.RequestUri != null && controllerContext.Request.RequestUri.Query.Contains("jEntitySearch")))
            {
                return "Get" + odataPath.EntitySet.Name + "ByEntitySearch";
            }
            if (odataPath.Segments.Count > 0 && (odataPath.PathTemplate == "~/entityset/key/navigation" || odataPath.PathTemplate == "~/entityset/key"))
            {
                string keyValue;
                if (odataPath.PathTemplate == "~/entityset/key/navigation")
                {
                    var keys = (odataPath.Segments[1] as KeyValuePathSegment).Value.Split(new char[] { ',' });
                    for (int idx = 0; idx < keys.Length; idx++)
                    {
                        keyValue = (keys[idx].Contains("=") ? keys[idx].Right("=") : keys[idx]);
                        if (keyValue.Contains("guid'"))
                            keyValue = keyValue.Extract("guid'", "'");
                        controllerContext.RouteData.Values["key" + idx.ToString()] = keyValue;
                    }
                    controllerContext.RouteData.Values["navigation"] = odataPath.Segments[2].ToString();
                    return "Get" + odataPath.Segments[0] + "__" + odataPath.EntitySet.Name;
                }
                else if (odataPath.PathTemplate == "~/entityset/key")
                {
                    var keys = (odataPath.Segments[1] as KeyValuePathSegment).Value.Split(new char[] { ',' });
                    for (int idx = 0; idx < keys.Length; idx++)
                    {
                        keyValue = (keys[idx].Contains("=") ? keys[idx].Right("=") : keys[idx]);
                        if (keyValue.Contains("guid'"))
                            keyValue = keyValue.Extract("guid'", "'");
                        controllerContext.RouteData.Values["key" + idx.ToString()] = keyValue;
                    }
                    return "Get" + odataPath.EntitySet.Name + "ById";
                }
            }

            return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate.StartsWith("~/entityset") || odataPath.PathTemplate == "~/entityset/key/navigation")
            {
                return this.controller;
            }

            return null;
        }
    }
}
