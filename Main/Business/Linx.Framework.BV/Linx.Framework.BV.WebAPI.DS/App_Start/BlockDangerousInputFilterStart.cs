using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Linx.Framework.BV.WebAPI.DS.Filters;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(Linx.Framework.BV.WebAPI.DS.App_Start.BlockDangerousInputFilterStart),
    "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{
    /// <summary>
    /// Registers <see cref="BlockDangerousInputAttribute"/> globally so AutoGen controllers
    /// are covered without editing generated sources.
    /// </summary>
    public static class BlockDangerousInputFilterStart
    {
        public static void Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            if (config == null || config.Filters == null)
                return;

            bool alreadyRegistered = config.Filters.Any(f =>
                f != null
                && f.Instance != null
                && f.Instance.GetType() == typeof(BlockDangerousInputAttribute));

            if (alreadyRegistered)
                return;

            config.Filters.Add(new BlockDangerousInputAttribute
            {
                Mode = BlockDangerousInputAttribute.GuardMode.Soft,
                MaxQueryValueLength = 32768
            });
        }
    }
}
