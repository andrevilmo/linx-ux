using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Linx.Internet.Application.Service
{
    static class Program
    {
        static int Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            bool debug = false,
                install = false,
                uninstall = false,
                console = false;

            try
            {
                foreach (string arg in args)
                {
                    #region trata os argumentos passados
                    switch (arg)
                    {
                        case "-d":
                        case "-debug":
                            debug = true;
                            break;
                        case "-i":
                        case "-install":
                            install = true;
                            break;
                        case "-u":
                        case "-uninstall":
                            uninstall = true;
                            break;
                        case "-c":
                        case "-console":
                            console = true;
                            break;
                    }
                    #endregion
                }

                if (debug)
                {
                    SyncSvc svc = new SyncSvc();
                    svc.DebugStart();

                    if (svc.ExitCode == 0)
                        Thread.Sleep(Timeout.Infinite);

                    return svc.ExitCode;
                }

                if (uninstall)
                {
                    SyncSvcInstaller.Install(true, args);
                }

                if (install)
                {
                    SyncSvcInstaller.Install(false, args);
                }

                if (console)
                {
                    Console.WriteLine("Starting...");

                    SyncSvc svc = new SyncSvc();
                    svc.Start();

                    if (svc.ExitCode == 0)
                    {
                        Console.WriteLine("System running; press any key to stop");
                        Console.ReadKey(true);


                        Console.WriteLine("System stoping...");
                        //svc.Stop();
                    }
                    Console.WriteLine("System stopped");
                }

                else if (!(install || uninstall))
                {
                    //// verifica se o servico esta instalado
                    //ServiceController[] servicesController = ServiceController.GetServices();
                    //var service = servicesController.Where(r => r.ServiceName == SyncSvcInstaller.SVC_NAME).FirstOrDefault();

                    ////instala e inicia o servico
                    //if (service == null)
                    //{
                    //    SyncSvcInstaller.Install(false, args);
                    //}
                    //else
                    //{
                        ServiceBase[] services = { new SyncSvc() };
                        ServiceBase.Run(services);
                    //}
                }
            }
            catch (Exception ex)
            {
                if (console)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadKey(true);
                }
                else
                    throw;

                return -1;
            }
            return 0;
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            
            Console.WriteLine(e.Data);
        }
    }
}
