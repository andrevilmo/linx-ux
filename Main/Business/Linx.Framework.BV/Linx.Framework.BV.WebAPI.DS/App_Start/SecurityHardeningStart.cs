using System.Linq;
using System.Web.Http;
using Linx.Framework.BV.WebAPI.DS.Filters;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(Linx.Framework.BV.WebAPI.DS.App_Start.SecurityHardeningStart),
    "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{
    /// <summary>
    /// Registers global error hardening: never include exception details in responses,
    /// and catch unhandled exceptions with a generic client message + server log.
    /// </summary>
    public static class SecurityHardeningStart
    {
        public static void Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            if (config == null)
                return;

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            if (config.Filters == null)
                return;

            bool alreadyRegistered = config.Filters.Any(f =>
                f != null
                && f.Instance != null
                && f.Instance.GetType() == typeof(GenericApiExceptionFilterAttribute));

            if (!alreadyRegistered)
                config.Filters.Add(new GenericApiExceptionFilterAttribute());
        }
    }
}
