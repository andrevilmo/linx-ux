using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace Linx.SelfHost.App
{
    partial class SelfHostAppFRWService : ServiceBase
    {
        private IDisposable _owinHost;
        private Thread _threadRestartSelfHost;
        public SelfHostAppFRWService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var port = ConfigurationManager.AppSettings["PortSelfHostApp"];
                Microsoft.Owin.Hosting.StartOptions options = new Microsoft.Owin.Hosting.StartOptions();
                options.Urls.Add(string.Format("http://*:{0}/", port));
                _owinHost = WebApp.Start<Startup>(options);
                SelfHostAppFRWEventLog.WriteEntry("SelfHostAppFRW iniciado com sucesso!\n\r Path: " + Assembly.GetExecutingAssembly().Location);

                ThreadStart _threadSelfHost = new ThreadStart(RestartSelfHost);
                _threadRestartSelfHost = new Thread(_threadSelfHost);
                _threadRestartSelfHost.Name = "Thread restart selfhostApp";
                _threadRestartSelfHost.Priority = ThreadPriority.Lowest;
                _threadRestartSelfHost.Start();
            }
            catch (Exception ex)
            {
                ServicesException(this, ex);
            }

        }

        private void RestartSelfHost()
        {
            Thread.Sleep(new TimeSpan(00, 01, 00));

            while (true)
            {
                ConfigurationManager.RefreshSection("appSettings");
                var valueRestartService = ConfigurationManager.AppSettings["RestartService"].ToString();
                if (Convert.ToBoolean(valueRestartService))
                {
                    try
                    {
                        RestartService("LinxOmniSelfHostAppFRW");
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["RestartService"].Value = "false";
                        config.Save(ConfigurationSaveMode.Modified);
                    }
                    catch (Exception ex)
                    {
                        ServicesException(this, ex);
                    }
                }
                Thread.Sleep(new TimeSpan(00, 30, 00));
            }
        }

        protected override void OnStop()
        {
            try
            {
                _owinHost.Dispose();
                //_threadRestartSelfHost.Abort();
                SelfHostAppFRWEventLog.WriteEntry("SelfHostApp OmniFRW fechado com sucesso!");
            }
            catch (Exception ex)
            {
                ServicesException(this, ex);
            }
        }

        private void ServicesException(object sender, Exception e)
        {
            SelfHostAppFRWEventLog.WriteEntry((string.Format("Exceção: {0}\r\n{1}\r\n{2}", sender.GetType().Name ?? "",
                e.Message ?? "", e.StackTrace ?? "") ?? "Null value on ServicesException."));
        }

        private void RestartService(string serviceName, int timeoutMilliseconds = 10000)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Refresh();
                SelfHostAppFRWEventLog.WriteEntry("Status: " + service.Status);
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    SelfHostAppFRWEventLog.WriteEntry("Inicio stop serviço !");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    SelfHostAppFRWEventLog.WriteEntry("Fim stop serviço !");
                    // conta o resto do timeout
                    int millisec2 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
                else
                {
                    service.Start();
                }
            }
            catch (Exception ex)
            {
                ServicesException(this, ex);
            }

        }
    }
}
