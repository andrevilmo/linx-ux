using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Linx.Tools.WebApi
{
    /// <summary>
    /// Global Web API CORS handler with an explicit origin allowlist.
    /// Reads <c>Cors.AllowedOrigins</c> (semicolon/comma-separated). Empty list = no CORS allow headers.
    /// Optional <c>Cors.AllowedHeaders</c> and <c>Cors.AllowedMethods</c>.
    /// </summary>
    public class CorsHandler : DelegatingHandler
    {
        const string Origin = "Origin";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";

        static readonly string[] DefaultMethods = { "GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS", "HEAD" };
        static readonly string[] DefaultHeaders =
        {
            "Content-Type", "Accept", "Authorization", "X-Requested-With", "cacheHash"
        };

        /// <summary>Optional override for tests / SelfHost without appSettings.</summary>
        public static string[] AllowedOriginsOverride { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var isCorsRequest = request.Headers.Contains(Origin);
            var isPreflightRequest = request.Method == HttpMethod.Options;

            if (!isCorsRequest)
                return base.SendAsync(request, cancellationToken);

            var origin = request.Headers.GetValues(Origin).FirstOrDefault();
            var allowed = IsOriginAllowed(origin);

            if (isPreflightRequest)
            {
                var response = new HttpResponseMessage(allowed ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
                if (allowed)
                    ApplyCorsHeaders(response, origin, request, includeMethodsAndHeaders: true);

                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(response);
                return tcs.Task;
            }

            return base.SendAsync(request, cancellationToken).ContinueWith(t =>
            {
                var resp = t.Result;
                if (allowed)
                    ApplyCorsHeaders(resp, origin, request, includeMethodsAndHeaders: false);
                return resp;
            }, cancellationToken);
        }

        static void ApplyCorsHeaders(HttpResponseMessage response, string origin, HttpRequestMessage request, bool includeMethodsAndHeaders)
        {
            if (response.Headers.Contains(AccessControlAllowOrigin))
                response.Headers.Remove(AccessControlAllowOrigin);
            response.Headers.Add(AccessControlAllowOrigin, origin);
            response.Headers.Add(AccessControlAllowCredentials, "true");

            if (includeMethodsAndHeaders)
            {
                var methods = GetConfiguredList("Cors.AllowedMethods", DefaultMethods);
                var requestedMethod = request.Headers.Contains(AccessControlRequestMethod)
                    ? request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault()
                    : null;
                if (!string.IsNullOrEmpty(requestedMethod)
                    && methods.Any(m => string.Equals(m, requestedMethod, StringComparison.OrdinalIgnoreCase)))
                {
                    response.Headers.Add(AccessControlAllowMethods, string.Join(", ", methods));
                }
                else
                {
                    response.Headers.Add(AccessControlAllowMethods, string.Join(", ", methods));
                }

                var allowedHeaders = GetConfiguredList("Cors.AllowedHeaders", DefaultHeaders);
                if (request.Headers.Contains(AccessControlRequestHeaders))
                {
                    var requested = request.Headers.GetValues(AccessControlRequestHeaders)
                        .SelectMany(v => v.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(h => h.Trim())
                        .Where(h => allowedHeaders.Any(a => string.Equals(a, h, StringComparison.OrdinalIgnoreCase)))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToArray();
                    if (requested.Length > 0)
                        response.Headers.Add(AccessControlAllowHeaders, string.Join(", ", requested));
                    else
                        response.Headers.Add(AccessControlAllowHeaders, string.Join(", ", allowedHeaders));
                }
                else
                {
                    response.Headers.Add(AccessControlAllowHeaders, string.Join(", ", allowedHeaders));
                }
            }

            if (!response.Headers.Contains(AccessControlExposeHeaders))
                response.Headers.Add(AccessControlExposeHeaders, "cacheHash");
        }

        static bool IsOriginAllowed(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin))
                return false;

            var allowed = AllowedOriginsOverride ?? GetConfiguredList("Cors.AllowedOrigins", null);
            if (allowed == null || allowed.Length == 0)
                return false;

            if (allowed.Length == 1 && allowed[0] == "*")
                return true;

            return allowed.Any(a => string.Equals(a, origin, StringComparison.OrdinalIgnoreCase));
        }

        static string[] GetConfiguredList(string key, string[] fallback)
        {
            try
            {
                var raw = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(raw))
                    return fallback ?? new string[0];

                return raw
                    .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => s.Length > 0)
                    .ToArray();
            }
            catch
            {
                return fallback ?? new string[0];
            }
        }
    }
}
