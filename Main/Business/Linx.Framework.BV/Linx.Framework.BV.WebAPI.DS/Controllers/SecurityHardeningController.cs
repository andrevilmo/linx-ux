using System;
using System.Configuration;
using System.Web.Http;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    /// <summary>
    /// Optional probe to demonstrate generic 500 responses (no stack to client).
    /// Enabled only when appSetting SecurityHardening.ErrorProbeEnabled=true.
    /// </summary>
    [RoutePrefix("SecurityHardening")]
    public class SecurityHardeningController : ApiController
    {
        [HttpGet]
        [Route("Throw")]
        public IHttpActionResult Throw()
        {
            if (!IsProbeEnabled())
                return NotFound();

            throw new InvalidOperationException(
                "SecurityHardening probe exception — must be logged server-side and not returned to the client.");
        }

        [HttpGet]
        [Route("Ping")]
        public IHttpActionResult Ping()
        {
            return Ok(new { status = "ok", probeEnabled = IsProbeEnabled() });
        }

        static bool IsProbeEnabled()
        {
            try
            {
                var raw = ConfigurationManager.AppSettings["SecurityHardening.ErrorProbeEnabled"];
                return string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase)
                    || raw == "1";
            }
            catch
            {
                return false;
            }
        }
    }
}
