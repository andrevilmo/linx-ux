using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Linx.ServiceBus.Starter.Areas.HelpPage.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Description;
using Linx.Tools;


namespace Linx.ServiceBus.Starter.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public partial class HelpController : Controller
    {
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiId.StartsWith("POST-") && apiId.EndsWith("SaveChanges"))
                {
                    SaveChangesDescription(apiModel);
                }
                
                if (apiModel != null)
                {
                    GetQuery(apiModel);
                    return View(apiModel);
                }
            }

            return View("Error");
        }
    }
}