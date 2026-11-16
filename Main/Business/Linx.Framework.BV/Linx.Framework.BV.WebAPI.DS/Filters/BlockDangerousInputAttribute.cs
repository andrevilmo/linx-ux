using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Linx.Framework.BV.WebAPI.DS.Filters
{
    /// <summary>
    /// Edge guard for Web API actions: path traversal + soft query checks.
    /// Does not replace parameterized SQL or HTML encoding on views.
    /// Register globally (preferred) or apply as [BlockDangerousInput] on a controller/action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BlockDangerousInputAttribute : ActionFilterAttribute
    {
        public enum GuardMode
        {
            /// <summary>Only path-like parameters (reportPath, etc.).</summary>
            PathOnly = 0,
            /// <summary>Path checks + null-byte/length + obvious script markers on query values.</summary>
            Soft = 1
        }

        private static readonly string[] PathParameterNames =
        {
            "reportpath",
            "filepath",
            "templatepath",
            "path",
            "filename",
            "file"
        };

        private static readonly string[] SoftSkipParameterNames =
        {
            "authenticateparameters",
            "ticket",
            "mfaticket",
            "jentitysearch",
            "translatedjentitysearch",
            "jqueryexpression",
            "password",
            "senha",
            "secret",
            "authorizationtoken",
            "otpauthuri",
            "qrcodepngbase64"
        };

        private static readonly Regex ObviousScriptPattern = new Regex(
            @"<\s*script\b|javascript\s*:|<\s*iframe\b|on\w+\s*=",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public GuardMode Mode { get; set; }

        public int MaxQueryValueLength { get; set; }

        public BlockDangerousInputAttribute()
        {
            Mode = GuardMode.Soft;
            MaxQueryValueLength = 32768;
            Order = -100;
        }

        private const string RequestPropertyKey = "Linx.BlockDangerousInput.Validated";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext == null)
                return;

            if (HasAllowDangerousInput(actionContext))
                return;

            // Avoid double work when registered globally and also applied as [BlockDangerousInput].
            if (actionContext.Request != null
                && actionContext.Request.Properties != null
                && actionContext.Request.Properties.ContainsKey(RequestPropertyKey))
                return;

            IEnumerable<KeyValuePair<string, string>> values = EnumerateIncomingValues(actionContext);
            foreach (KeyValuePair<string, string> pair in values)
            {
                string name = pair.Key ?? string.Empty;
                string value = pair.Value;
                if (string.IsNullOrEmpty(value))
                    continue;

                string nameKey = name.Trim().ToLowerInvariant();
                bool isPathParam = IsPathParameter(nameKey);

                string pathError;
                if (isPathParam && !IsSafePathValue(value, out pathError))
                {
                    Reject(actionContext, pathError);
                    return;
                }

                if (Mode == GuardMode.PathOnly)
                    continue;

                if (IsSoftSkipParameter(nameKey))
                    continue;

                string softError;
                if (!IsSoftSafeValue(value, out softError))
                {
                    Reject(actionContext, softError);
                    return;
                }
            }

            if (actionContext.Request != null && actionContext.Request.Properties != null)
                actionContext.Request.Properties[RequestPropertyKey] = true;
        }

        private static bool HasAllowDangerousInput(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor != null
                && actionContext.ActionDescriptor.GetCustomAttributes<AllowDangerousInputAttribute>().Any())
                return true;

            if (actionContext.ControllerContext != null
                && actionContext.ControllerContext.ControllerDescriptor != null
                && actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowDangerousInputAttribute>().Any())
                return true;

            return false;
        }

        private static IEnumerable<KeyValuePair<string, string>> EnumerateIncomingValues(HttpActionContext actionContext)
        {
            var list = new List<KeyValuePair<string, string>>();

            if (actionContext.ActionArguments != null)
            {
                foreach (KeyValuePair<string, object> arg in actionContext.ActionArguments)
                {
                    if (arg.Value == null || arg.Value is byte[])
                        continue;
                    // Only primitive/string arguments — do not walk SaveChanges JObject bodies.
                    Type t = arg.Value.GetType();
                    if (t.IsPrimitive || arg.Value is string || arg.Value is Guid || arg.Value is DateTime || arg.Value is decimal)
                        list.Add(new KeyValuePair<string, string>(arg.Key, Convert.ToString(arg.Value)));
                }
            }

            try
            {
                var query = actionContext.Request.GetQueryNameValuePairs();
                if (query != null)
                {
                    foreach (KeyValuePair<string, string> q in query)
                        list.Add(q);
                }
            }
            catch
            {
                // Ignore malformed query enumeration; action binding already failed or will fail.
            }

            return list;
        }

        private static bool IsPathParameter(string nameKey)
        {
            for (int i = 0; i < PathParameterNames.Length; i++)
            {
                if (nameKey == PathParameterNames[i] || nameKey.EndsWith(PathParameterNames[i], StringComparison.Ordinal))
                    return true;
            }
            return false;
        }

        private static bool IsSoftSkipParameter(string nameKey)
        {
            for (int i = 0; i < SoftSkipParameterNames.Length; i++)
            {
                if (nameKey == SoftSkipParameterNames[i])
                    return true;
            }
            return false;
        }

        internal static bool IsSafePathValue(string value, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                error = "Caminho de arquivo vazio.";
                return false;
            }

            string decoded = Uri.UnescapeDataString(value.Replace('+', ' '));
            if (decoded.IndexOf('\0') >= 0)
            {
                error = "Caminho de arquivo inválido (null byte).";
                return false;
            }

            string normalized = decoded.Replace('/', '\\');
            if (normalized.IndexOf("..", StringComparison.Ordinal) >= 0)
            {
                error = "Caminho de arquivo inválido (path traversal).";
                return false;
            }

            if (normalized.StartsWith("\\", StringComparison.Ordinal)
                || Regex.IsMatch(normalized, @"^[A-Za-z]:\\"))
            {
                error = "Caminho de arquivo absoluto não permitido.";
                return false;
            }

            if (normalized.IndexOf(':') >= 0)
            {
                error = "Caminho de arquivo inválido.";
                return false;
            }

            return true;
        }

        private bool IsSoftSafeValue(string value, out string error)
        {
            error = null;
            if (value.IndexOf('\0') >= 0)
            {
                error = "Entrada inválida (null byte).";
                return false;
            }

            if (MaxQueryValueLength > 0 && value.Length > MaxQueryValueLength)
            {
                error = "Entrada excede o tamanho máximo permitido.";
                return false;
            }

            if (ObviousScriptPattern.IsMatch(value))
            {
                error = "Entrada contém marcadores de script/HTML não permitidos.";
                return false;
            }

            return true;
        }

        private static void Reject(HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest,
                string.IsNullOrWhiteSpace(message) ? "Entrada rejeitada pelo filtro de segurança." : message);
        }
    }
}
