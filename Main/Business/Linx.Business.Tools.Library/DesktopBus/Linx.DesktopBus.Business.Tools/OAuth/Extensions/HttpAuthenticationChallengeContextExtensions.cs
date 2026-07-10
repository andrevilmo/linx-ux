using Linx.Business.Tools.OAuth.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Linx.Business.Tools.OAuth.Extensions
{
    public static class HttpAuthenticationChallengeContextExtensions
    {
        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, string scheme)
        {
            ChallengeWith(context, new AuthenticationHeaderValue(scheme));
        }

        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, string scheme, string parameter)
        {
            ChallengeWith(context, new AuthenticationHeaderValue(scheme, parameter));
        }

        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, AuthenticationHeaderValue challenge)
        {
            ChallengeWith(context, new AuthenticationHeaderValue[] { challenge });

            context.Result = new System.Web.Http.Results.UnauthorizedResult(
                new AuthenticationHeaderValue[] { challenge },
                new System.Net.Http.HttpRequestMessage(context.Request.Method, context.Request.RequestUri));
        }
        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, IEnumerable<AuthenticationHeaderValue> challenges)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (context.Request == null)
                throw new ArgumentNullException("context.Request");
            if (challenges == null)
                throw new ArgumentNullException("challenges");

            context.Result = new System.Web.Http.Results.UnauthorizedResult(
                challenges,
                new System.Net.Http.HttpRequestMessage(context.Request.Method, context.Request.RequestUri));
        }
    }
}
