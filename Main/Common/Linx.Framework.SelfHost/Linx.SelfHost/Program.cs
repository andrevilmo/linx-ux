using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linx.SelfHost
{
    static class Program
    {
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (!System.Environment.UserInteractive)
            {
                ServiceBase.Run(new ServiceBase[] { new SelfHostFRWService() });
                return;
            }

            if (args.Length == 0)
                return;

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-install":
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "-uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                    case "-c":
                    case "-console":
                    default:
                        {
                            var port = ConfigurationManager.AppSettings["PortSelfHost"];
                            StartSelfHost _startSelfHost = new StartSelfHost();
                            _startSelfHost.StartHost(port);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("Server listener port: " + port);
                            Console.ReadLine();
                            break;
                        }
                }
            }



#if DEBUG
            //var port = ConfigurationManager.AppSettings["PortSelfHost"];
            //StartSelfHost _startSelfHost = new StartSelfHost();
            //_startSelfHost.StartHost(port);
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("Server listener port: " + port);
            //Console.ReadLine();

#endif
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
