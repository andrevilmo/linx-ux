using Linx.Framework.BV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Linx.Business.Tools.OAuth.Extensions;
using Linx.Business.Tools.OAuth.Results;
using System.Web.Security;
using System.Web;
using Linx.Tools;


namespace Linx.Business.Tools
{
    //todo: change name ODataBasicAuthenticationFilter -> ODataAuthenticationAttribute

    public class ODataBasicAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }

        private void ClearPrincipal(HttpAuthenticationContext context)
        {
            context.Principal = new CustomPrincipal(Guid.NewGuid().ToString(), null);
            if (System.Web.HttpContext.Current.IsNull())
                LinxHttpContext.HttpContext.Current.User = context.Principal;
        }

        private void InvalidCredentials(HttpAuthenticationContext context, HttpRequestMessage request)
        {
            context.ErrorResult = new AuthenticationFailureResult("Invalid credentials!", request);
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            ICustomPrincipal principal = null;

            string uri = context.Request.RequestUri.AbsoluteUri.ToLower();
            if (uri.Contains("/getmetadata") || uri.Contains("/getsecurityinfo") || uri.Contains("/getentities") || uri.Contains("/getclientdomains")
                 || uri.Contains("/getclientservice") || uri.Contains("/getclientfactory") || uri.Contains("/getclientfactorycustomevents"))
            {
                ClearPrincipal(context);
                return;
            }

            //Check local service bus authentication
            if (Linx.Tools.LocalServiceBus.Enabled)
            {
                if (Linx.Tools.LocalServiceBus.DevMode)
                {
                    ClearPrincipal(context);
                    return;
                }

                AuthenticationResult localResult = null;

                //Local Host Authentication By EncodedSecret
                if (request.Headers != null && request.Headers.Contains("EncodedSecret") && IEnumerableHasValue(request.Headers.GetValues("EncodedSecret")) && request.Headers.Contains("DeviceId") && IEnumerableHasValue(request.Headers.GetValues("DeviceId")))
                {
                    localResult = Linx.Tools.LocalServiceBus.GetAuthenticationByDevice(request.Headers.GetValues("DeviceId").FirstOrDefault(), request.Headers.GetValues("EncodedSecret").FirstOrDefault());
                    if (localResult.IsOk)
                    {
                        ClearPrincipal(context);
                        SetCustomHeaders(context, request, localResult.Headers);
                    }
                    else
                    {
                        throw new Exception("Invalid trusted relation header: EncodedSecret!");
                    }
                }
                else
                {
                    if (authorization == null || authorization.Scheme != "Basic")
                    {
                        return;
                    }
                    else
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        //Local Host Authentication By User
                        if (!authorization.Parameter.IsNullOrEmpty())
                        {
                            var tokens = Encoding.Default.GetString(Convert.FromBase64String(authorization.Parameter)).Split(':');
                            if (tokens.Length >= 2)
                            {
                                localResult = Linx.Tools.LocalServiceBus.GetAuthenticationByUser(tokens[0], tokens[1]);
                                if (localResult.IsOk)
                                {
                                    principal = new CustomPrincipal(Linx.Tools.LocalServiceBus.CurrentUserName, localResult.Headers);
                                    SetCustomHeaders(context, request, localResult.Headers);
                                    context.Principal = principal;
                                }
                                else
                                {
                                    InvalidCredentials(context, request);
                                    return;
                                }
                            }
                            else
                            {
                                InvalidCredentials(context, request);
                                return;
                            }
                        }
                        else
                        {
                            InvalidCredentials(context, request);
                            return;
                        }
                    }
                }

                return;
            }

            //Default Authentication By Headers
            if (request.Headers != null && request.Headers.Contains("Application") && IEnumerableHasValue(request.Headers.GetValues("Application")))
            {
                ClearPrincipal(context);
                return;
            }

            //Auth Authentication
            if (authorization == null || authorization.Scheme != "Basic")
            {
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                InvalidCredentials(context, request);
                return;
            }

            Dictionary<int, string> headers = LinxBusinessODataAuthentication.ODataAuthentication(authorization.Parameter);

            if (headers == null)
            {
                InvalidCredentials(context, request);
                return;
            }

            principal = new CustomPrincipal(headers[10], headers);
            SetCustomHeaders(context, request, headers);
            context.Principal = principal;
        }

        private void SetCustomHeaders(HttpAuthenticationContext context, HttpRequestMessage request, Dictionary<int, string> headers)
        {
            request.Headers.Add("CurrentCompany", headers[1]);
            request.Headers.Add("AuthorizationToken", headers[2]);
            request.Headers.Add("CurrentUser", headers[3]);
            request.Headers.Add("AccessGroup", headers[4]);
            request.Headers.Add("EconomicGroup", headers[5]);
            request.Headers.Add("Environment", headers[6]);
            request.Headers.Add("Application", headers[8]);


            if (HttpContext.Current == null)
            {
                //SelfHost
                try
                {
                    var httpHeaders = LinxHttpContext.HttpContext.Current.Request.Inner.GetPropertyValue("Headers") as System.Net.Http.Headers.HttpRequestHeaders;
                    httpHeaders.Add("CurrentCompany", headers[1]);
                    httpHeaders.Add("AuthorizationToken", headers[2]);
                    httpHeaders.Add("CurrentUser", headers[3]);
                    httpHeaders.Add("AccessGroup", headers[4]);
                    httpHeaders.Add("EconomicGroup", headers[5]);
                    httpHeaders.Add("Environment", headers[6]);
                    httpHeaders.Add("Application", headers[8]);
                }
                catch (Exception exp)
                {
                    throw new Exception(exp.GetCompleteMessage());
                }
            }
            else
            {

                //WebDev não permite adicionar cabeçalhos.
                try
                {
                    var httpHeaders = HttpContext.Current.Request.Headers;
                    httpHeaders.Add("CurrentCompany", headers[1]);
                    httpHeaders.Add("AuthorizationToken", headers[2]);
                    httpHeaders.Add("CurrentUser", headers[3]);
                    httpHeaders.Add("AccessGroup", headers[4]);
                    httpHeaders.Add("EconomicGroup", headers[5]);
                    httpHeaders.Add("Environment", headers[6]);
                    httpHeaders.Add("Application", headers[8]);

                }
                catch
                {
                    throw new Exception("Operation is not supported by WebDev environment.\n Initialize the ServiceBus by executing WebIISExpress.bat.");
                }
            }
        }

        private bool IEnumerableHasValue(IEnumerable<string> enumerable)
        {
            return enumerable != null && enumerable.Count() > 0;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null && System.Web.HttpContext.Current.Request.Headers != null)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    Challenge(context);
            }
            else if (LinxHttpContext.HttpContext.Current != null && LinxHttpContext.HttpContext.Current.Request != null && LinxHttpContext.HttpContext.Current.Request.Inner != null)
            {
                if (!LinxHttpContext.HttpContext.Current.User.Identity.IsAuthenticated)
                    Challenge(context);
            }
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = "realm=\"" + (string.IsNullOrEmpty(Realm) ? context.Request.RequestUri.DnsSafeHost : Realm) + "\"";

            context.ChallengeWith("Basic", parameter);
        }

        public virtual bool AllowMultiple
        {
            get { return false; }
        }
    }

    public interface ICustomPrincipal : System.Security.Principal.IPrincipal
    {
        Dictionary<int, string> Headers { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username, Dictionary<int, string> headers)
        {
            this.Identity = new GenericIdentity(username);
            this.Headers = headers;
        }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
               !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
        }

        public Dictionary<int, string> Headers { get; set; }


    }
}
