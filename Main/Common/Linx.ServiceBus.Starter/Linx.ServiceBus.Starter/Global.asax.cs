using Grc.Core.Integration;
using Linx.ServiceBus.Starter.Areas.HelpPage;
using Linx.Tools;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling.Storage;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.Reporting.Services.WebApi;

namespace Linx.ServiceBus.Starter
{
    public class Global : HttpApplication
    {
        private bool MiniProfilerEnabled = ConfigurationManager.AppSettings.GetValue<bool>("MiniProfiler.Enabled", false);

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                //Start local service bus
                Linx.Tools.LocalServiceBus.Start();

                BusinessModelInstructionHelper.LoadInstructions();

                if (MiniProfilerEnabled)
                {
                    //MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();
                    //MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(true);
                    MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.VerboseSqlServerFormatter(true);
                    WebRequestProfilerProvider.Settings.UserProvider = new MiniProfilerCustomUser();


                    MiniProfiler.Settings.MaxJsonResponseSize = int.MaxValue;

                    MiniProfiler.Settings.Results_List_Authorize = (request) =>
                    {
                        return true; // all requests are kosher
                    };
                    if (ConfigurationManager.ConnectionStrings["MiniProfiler"] != null)
                    {
                        MiniProfiler.Settings.Storage = new SqlServerStorage(ConfigurationManager.ConnectionStrings["MiniProfiler"]);
                    }


                    MiniProfilerEF6.Initialize();
                }

                GlobalConfiguration.Configure(Register);

                RegisterHelps();

                GlobalConfiguration.Configuration.MessageHandlers.Add(new Linx.Tools.WebApi.CorsHandler());

                //Fiscal Hub Instance
                var fiscalHubImp = Linx.Tools.ImplementationHelper<IIntegration>.GetInstance("HubFiscalDataIntegration", "HubFiscal.Integration.Data");
                if (fiscalHubImp != null)
                {
                    GlobalConfiguration.Configuration.DependencyResolver = fiscalHubImp.DependencyResolver() as System.Web.Http.Dependencies.IDependencyResolver;

                }

                HandlingErrosManager();
            }
            catch (ReflectionTypeLoadException loaderEx)
            {
                if (loaderEx.LoaderExceptions.Length > 0) {
                    var msg = string.Empty;
                    foreach(var le in loaderEx.LoaderExceptions)
                    {
                        msg += string.Format("{0}{1}", le.Message, Environment.NewLine);
                    }
                    throw new Exception(msg);
                }
                throw loaderEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            ReportsControllerConfiguration.RegisterRoutes(GlobalConfiguration.Configuration);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

        private void RegisterHelps()
        {
            AreaRegistration.RegisterAllAreas();

            var helpDir = HttpContext.Current.Server.MapPath("~/Help_WebApi/");
            if (!Directory.Exists(helpDir)) return;
            foreach (var path in Directory.GetFiles(helpDir, "*.xml"))
            {
                SetXmlDocumentationProvider(GlobalConfiguration.Configuration, path);
            }
        }

        private void SetXmlDocumentationProvider(HttpConfiguration config, string documentationProvider)
        {
            config.Services.Replace(typeof(IDocumentationProvider), new XmlDocumentationProvider(documentationProvider));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (MiniProfilerEnabled)
            {
                if ((new MiniProfilerCustomUser()).GetUser(System.Web.HttpContext.Current.Request) == "anonymous")
                    return;

                if (Request.HttpMethod.Equals("GET", StringComparison.InvariantCultureIgnoreCase) || Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
                {
                    MiniProfiler.Start(string.Concat("[API] ", Request.Url.PathAndQuery));
                }
            }
        }

        protected void Application_EndRequest()
        {
            if (MiniProfilerEnabled)
            {
                //Necessário no momento porque algumas requisições estão ultrapassando o tamanho máximo do Json.
                try
                {
                    MiniProfiler.Stop();
                }
                catch { }
            }
        }

        private void HandlingErrosManager()
        {
            bool HandlingErrorsEnabled = ConfigurationManager.AppSettings.GetValue<bool>("HandlingErrors.Enabled", false);

            //somente habilita o tratamento/salvamento dos erros se estiver em modo diferente de DEV e o "HandlingErrors.Enabled" for "true"
            if (HandlingErrorsEnabled && !LocalServiceBus.DevMode)
            {
                GlobalConfiguration.Configuration.Filters.Add(
                    new ErrorHandling.GenericExceptionFilterAttribute());
            }
        }
    }
}