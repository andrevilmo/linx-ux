using Microsoft.Owin.Hosting;
using NLog;
using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.ServiceProcess;

namespace Linx.Internet.Application.Service
{
    public partial class SyncSvc : ServiceBase
    {
        public static readonly Logger Logger = LogManager.GetLogger(SyncSvcInstaller.SVC_NAME);
        public static readonly Version VersaoDLL = Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly string VersaoDLLString = string.Concat("v",Assembly.GetExecutingAssembly().GetName().Version);
        private static IDisposable _owinHost;

        public SyncSvc()
        {
            //InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServicePointManager.MaxServicePointIdleTime = 500;

            try
            {
                //Logger.LoggerReconfigured += new EventHandler<EventArgs>(NLogger_LoggerReconfigured);
                Logger.Info("Iniciando servico '{0}' ({1})", SyncSvcInstaller.SVC_DISPLAYNAME, SyncSvcInstaller.SVC_NAME);

                var port = ConfigurationManager.AppSettings.GetValue<string>("port", "1700");
                StartOptions options = new StartOptions();

                options.Urls.Add("http://localhost:" + port);
                options.Urls.Add(string.Format("http://{0}:" + port, Environment.MachineName));
                options.Urls.Add(string.Format("http://+:{0}/", port));
                options.Urls.Add(string.Format("http://*:{0}/", port));
                foreach(var url in options.Urls)
                {
                    Logger.Info("Configurando endereco '{0}'", url);
                }

                _owinHost = WebApp.Start<OwinStartup>(options);
                Logger.Info("Servidor web iniciado com sucesso!");

                //_owinHost = WebApp.Start<Startup>(options);
            }
            catch (Exception ex)
            {
                this.ExitCode = -1;
                Logger.ErrorException("Erro na inicialização do servico", ex);
                this.Stop();

                throw ex;
            }
        }

        protected override void OnStop()
        {
            Logger.Info("Parando servico web...");
            _owinHost.Dispose();

            Logger.Info("Parando servico '{0}' ({1})", SyncSvcInstaller.SVC_DISPLAYNAME, SyncSvcInstaller.SVC_NAME);
        }

        internal void DebugStart()
        {
            OnStart(null);
        }

        internal void Start()
        {
            OnStart(null);
        }

        internal void Stop()
        {
            OnStop();
        }

    }
}
